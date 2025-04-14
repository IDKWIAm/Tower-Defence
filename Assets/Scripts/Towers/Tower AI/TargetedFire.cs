using UnityEngine;
using TowerDefence.Towers.Projectiles;

namespace TowerDefence.Towers.TowerAI
{
    public class TargetedFire : BaseTower
    {
        [SerializeField] private float firerate;

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSourse;

        private float _fireTimer;

        private Transform _target;

        private void Update()
        {
            _target = FindNearestEnemy()?.transform;

            if (_target != null && _fireTimer <= 0 && placed)
            {
                Fire();
                _fireTimer = firerate;
            }

            _fireTimer -= Time.deltaTime;
        }

        private void Fire()
        {
            var bullet = Instantiate(bulletPrefab, bulletSourse.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().InitTarget(_target);
        }
    }
}
