using TowerDefence.Health;

namespace TowerDefence.Towers.Health
{
    public class TowerHealth : BaseHealth
    {
        public void Kill()
        {
            Destroy(gameObject);
        }
    }
}
