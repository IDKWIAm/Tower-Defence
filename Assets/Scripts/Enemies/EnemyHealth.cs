using TowerDefence.Health;

namespace TowerDefence.Enemies
{
    public class EnemyHealth : BaseHealth
    {
        private void Awake()
        {
            onDeath.AddListener(OnDeath);
        }
        private void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}