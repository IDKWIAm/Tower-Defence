using UnityEngine;
using TowerDefence.Towers.Projectiles;

namespace TowerDefence.Towers.TowerAI
{
    public class BallisticFire : BaseTower
    {
        [SerializeField] private float firerate;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform bulletSource;

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

            if (_target != null && _fireTimer <= 0 && placed)
            {
                LaunchProjectile();
                _fireTimer = firerate;
            }
            _fireTimer -= Time.deltaTime;
        }

        void LaunchProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, bulletSource.position, Quaternion.identity);
            projectile.GetComponent<ballisticBullet>().InitTarget(_target);
        }
    }
}
