using UnityEngine;
using TowerDefence.Towers.Projectiles;

namespace TowerDefence.Towers.TowerAI
{
    public class ChainLightning : Tower
    {
        [SerializeField] private float firerate;
        [SerializeField] private int chainLength;
        [SerializeField] private float lightningHitDelay;
        [SerializeField] private int lightningDamage;

        [SerializeField] private Transform bulletSource;
        [SerializeField] private GameObject projectilePrefab;

        private GameObject _target;

        private float _fireTimer;

        private void Start()
        {
            if (bulletSource == null)
            {
                bulletSource = transform;
            }
        }

        void Update()
        {
            _target = FindNearestEnemy();
            if (_target == null) return;

            var distance = Vector2.Distance(transform.position, _target.transform.position);

            if (_target != null && _fireTimer <= 0)
            {
                Fire();
                _fireTimer = firerate;
            }
            _fireTimer -= Time.deltaTime;
        }

        void Fire()
        {
            GameObject lightning = Instantiate(projectilePrefab, bulletSource.position, Quaternion.identity);
            lightning.GetComponent<Lightning>().Init(chainLength, lightningHitDelay, lightningDamage);
        }
    }
}
