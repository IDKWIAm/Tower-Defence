using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Health
{
    public class BaseHealth : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth;

        [SerializeField]
        protected UnityEvent onDeath;

        private int _currentHealth;

        public void Damage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                onDeath.Invoke();
            }
        }

        private void Start()
        {
            _currentHealth = maxHealth;
        }
    }
}