using TowerDefence.Enemies;
using UnityEngine;

namespace TowerDefence.Towers.Projectiles
{
    public class ballisticBullet : MonoBehaviour
    {
        [SerializeField] private float startSpeed;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float explosionRadius;
        [SerializeField] private int explosionDamage;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ParticleSystem particles;

        private GameObject _target;

        private float _targetX = Mathf.Infinity;
        private float _targetY = Mathf.Infinity;

        private float _startingX = Mathf.Infinity;
        private float _startingY = Mathf.Infinity;

        private float _fireLerp = 1;
        private bool _exploded;

        private void Start()
        {
            _startingX = transform.position.x;
            _startingY = transform.position.y;
            _targetX = _target.transform.position.x;
            _targetY = _target.transform.position.y;
            _fireLerp = 0;
        }

        private void Update()
        {
            if (_target)
            {
                _targetX = _target.transform.position.x;
                _targetY = _target.transform.position.y;
            }


            if (_fireLerp < 1)
            {
                Vector3 newProjectilePos = CalculateTrajectory(new Vector3(_startingX, _startingY, 0), new Vector3(_targetX, _targetY, 0), _fireLerp);
                transform.position = newProjectilePos;

                _fireLerp += projectileSpeed * Time.deltaTime;
            }

            if (_fireLerp >= 1 && !_exploded)
            {
                _exploded = true;
                Explode();
            }
        }

        public void InitTarget(GameObject targetGameObject)
        {
            _target = targetGameObject;
        }

        Vector3 CalculateTrajectory(Vector3 firePos, Vector3 targetPos, float t)
        {
            Vector3 linearProgress = Vector3.Lerp(firePos, targetPos, t);
            float perspectiveOffset = Mathf.Sin(t * Mathf.PI) * startSpeed;

            Vector3 trajectoryPos = linearProgress + (Vector3.up * perspectiveOffset);
            return trajectoryPos;
        }

        private void Explode()
        {
            particles.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);

            if (!_target) return;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach (Collider2D collider in colliders)
            {
                var enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth is not null)
                {
                    enemyHealth.Damage(explosionDamage);
                }
            }
        }
    }
}
