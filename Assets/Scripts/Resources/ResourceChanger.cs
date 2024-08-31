using UnityEngine;

namespace TowerDefence.Resources
{
    public class ResourceChanger : MonoBehaviour
    {
        [SerializeField]
        private ResourceManager resourceManager;
        [SerializeField]
        private ResourceType type;

        public void AddAmount(int amount)
        {
            resourceManager.AddAmount(type, amount);
        }
        public void SubtractAmount(int amount)
        {
            AddAmount(-amount);
        }
    }
}