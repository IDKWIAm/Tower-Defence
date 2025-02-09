using TowerDefence.Health;

namespace TowerDefence.Towers.Health
{
    public class TowerHealth : BaseHealth
    {
        private void Awake()
        {
            onDeath.AddListener(Kill);
        }

        private void Kill()
        {
            Destroy(gameObject);
        }
    }
}
