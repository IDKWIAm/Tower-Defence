using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Resources
{
    /// <summary>
    /// Invoked when a resource manager changes the amount of resource with a given type
    /// </summary>
    [DisallowMultipleComponent]
    public class ResourceChangeListener : MonoBehaviour
    {
        /// <summary>
        /// The resource manager to bind to.
        /// </summary>
        [SerializeField]
        private ResourceManager resourceManager;

        /// <summary>
        /// The resource type to listen for changes to.
        /// </summary>
        [SerializeField]
        private ResourceType type;

        /// <summary>
        /// Invoked when a resource with the type is changed.
        /// </summary>
        [SerializeField]
        private UnityEvent<int> onChange;

        public ResourceManager Manager => resourceManager;
        public ResourceType Type => type;
        public UnityEvent<int> OnChange => onChange;

        private void Awake()
        {
            Manager.AddListener(this);
        }
    }
}