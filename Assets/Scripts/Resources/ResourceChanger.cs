using UnityEngine;

namespace TowerDefence.Resources
{
    /// <summary>
    /// Changes a resource with a given type in a given resource manager.
    /// </summary>
    public class ResourceChanger : MonoBehaviour
    {
        /// <summary>
        /// The resource manager.
        /// </summary>
        [SerializeField]
        private ResourceManager resourceManager;

        /// <summary>
        /// The resource type to change.
        /// </summary>
        [SerializeField]
        private ResourceType type;

        /// <summary>
        /// Increases the resource amount.
        /// </summary>
        /// <param name="amount">How much to increase.</param>
        public void AddAmount(int amount)
        {
            resourceManager.ModifyAmount(type, amount);
        }

        /// <summary>
        /// Decreases the resource amount.
        /// </summary>
        /// <param name="amount">How much to decrease.</param>
        public void SubtractAmount(int amount)
        {
            AddAmount(-amount);
        }

        public void SetResourceManager(ref ResourceManager newManager)
        {
            resourceManager = newManager;
        }
        public void SetResourceType(ResourceType newType)
        {
            type = newType;
        }
    }
}