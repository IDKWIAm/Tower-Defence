using TowerDefence.Enemies;
using UnityEngine;

namespace TowerDefence.Towers.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float speed;
        [SerializeField] private float lifeTime;

        private Transform _target;
        private Vector2 _direction;

        void Start()
        {
            Invoke("selfDestroy", lifeTime);
            _direction = (_target.position - transform.position).normalized;
        }

        private void Update()
        {
            MoveBullet();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.Damage(damage);
            }
            selfDestroy();
        }

        private void MoveBullet()
        {
            transform.Translate(_direction * speed * Time.deltaTime);
        }

        private void selfDestroy()
        {
            Destroy(gameObject);
        }

        public void InitTarget(Transform t)
        {
            _target = t;
        }

    }
}
