using System.Collections.Generic;
using TowerDefence.Enemies;
using UnityEngine;

namespace TowerDefence.Towers.Projectiles
{
    public class Lightning : MonoBehaviour
    {
        [SerializeField] private float range;
        [SerializeField] private float lifetime;

        [SerializeField] private LayerMask enemyLayer;

        private LineRenderer _lineRenderer;

        private List<GameObject> _hitEnemies = new List<GameObject>();

        private GameObject _target;

        private int _damage;
        private float _hitDelay;
        private int _maxChains;
        private float _savedRange;

        private int _chainCounter;
        private float _cnt;

        private bool _initedLastFrame;
        private bool _stopped;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();

            _lineRenderer.SetPositions(new Vector3[] { transform.position, _target.transform.position });
            transform.position = _target.transform.position;

            DamageCurrentTarget();
            _chainCounter += 1;
        }

        void Update()
        {
            if (_stopped) return;

            if (_initedLastFrame)
            {
                _initedLastFrame = false;
                return;
            }

            if (_cnt < 0)
            {
                for (float i = Time.deltaTime; i > 0; i -= _hitDelay)
                {
                    if (_maxChains > _chainCounter)
                        Move();
                    else 
                        StopMoving();
                }
            }
            else _cnt -= Time.deltaTime;
        }

        private GameObject FindNextNearestEnemy()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

            GameObject nearestEnemy = null;
            float closestDistance = Mathf.Infinity;
            foreach (Collider2D collider in colliders)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance && _hitEnemies.Contains(collider.gameObject) == false)
                {
                    closestDistance = distance;
                    nearestEnemy = collider.gameObject;
                }
            }
            range = _savedRange;
            return nearestEnemy;
        }

        private void Move()
        {
            _target = FindNextNearestEnemy();

            if (_target == null)
            {
                StopMoving();
                return;
            }

            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _target.transform.position);

            transform.position = _target.transform.position;
            DamageCurrentTarget();
            _chainCounter += 1;
            _cnt = _hitDelay;
        }

        private void DamageCurrentTarget()
        {
            if (_target.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.Damage(_damage);
                _hitEnemies.Add(_target);
            }
        }

        private void StopMoving()
        {
            Invoke("SelfDestroy", lifetime);
            _stopped = true;
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

        public void Init(int maxChains, float hitDelay, int damage, GameObject target)
        {
            _savedRange = range;
            this._maxChains = maxChains;
            this._hitDelay = hitDelay;
            this._damage = damage;
            _target = target;
            _initedLastFrame = true;
        }
    }
}
