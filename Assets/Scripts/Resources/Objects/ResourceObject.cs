using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Resources.Objects
{
    /// <summary>
    /// Represents an object with the ability to change a resource amount.
    ///
    /// <seealso cref="ResourceChanger"/>
    /// </summary>
    [RequireComponent(typeof(ResourceChanger))]
    public class ResourceObject : MonoBehaviour
    {
        /// <summary>
        /// The delta amount of resource.
        /// </summary>
        [SerializeField]
        private int amount;

        /// <summary>
        /// Invoked when collected
        /// </summary>
        [SerializeField]
        protected UnityEvent onCollect;

        private ResourceChanger _resourceChanger;
        protected virtual void Awake()
        {
            _resourceChanger = GetComponent<ResourceChanger>();
        }
        
        /// <summary>
        /// Collects <see cref="amount"/> amount of resource.
        /// </summary>
        protected void CollectResource()
        {
            _resourceChanger.AddAmount(amount);
            onCollect.Invoke();
        }
    }
}