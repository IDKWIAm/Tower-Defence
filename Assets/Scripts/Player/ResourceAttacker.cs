using TowerDefence.Resources.Objects;
using UnityEngine;

namespace TowerDefence.Player
{
    public class ResourceAttacker : MonoBehaviour
    {
        public float attackCooldown = 1f;
        private float _currentCooldown;
        private Coroutine _attackCoroutine;
        private void Update()
        {
            Timer();
        }
        private void Timer()
        {
            _currentCooldown -= Time.deltaTime;
        }
        public void Attack(DamageableResourceObject resource)
        {
            if (_currentCooldown <= 0)
            {
                _currentCooldown = attackCooldown;
                resource.Hit();
            }
        }
    }
}
