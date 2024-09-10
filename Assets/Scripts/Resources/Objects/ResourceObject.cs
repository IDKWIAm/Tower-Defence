using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Resources.Objects
{
    [RequireComponent(typeof(ResourceChanger))]
    public class ResourceObject : MonoBehaviour
    {
        [SerializeField]
        private int amount;

        [SerializeField]
        protected UnityEvent onCollect;

        private ResourceChanger _resourceChanger;
        protected virtual void Awake()
        {
            _resourceChanger = GetComponent<ResourceChanger>();
        }
        protected void CollectResource()
        {
            _resourceChanger.AddAmount(amount);
            onCollect.Invoke();
        }
    }
}