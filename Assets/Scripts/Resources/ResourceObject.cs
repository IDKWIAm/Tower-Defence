using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Resources
{
    [RequireComponent(typeof(ResourceChanger))]
    public class ResourceObject : MonoBehaviour
    {
        [SerializeField]
        private int hits;

        [SerializeField]
        private int amount;

        [SerializeField]
        private UnityEvent onHit;

        [SerializeField]
        private UnityEvent onCollect;

        private ResourceChanger _resourceChanger;
        private void Awake()
        {
            _resourceChanger = GetComponent<ResourceChanger>();
            onHit.AddListener(ProcessHit);
        }
        private void OnMouseDown()
        {
            onHit.Invoke();
        }
        private void ProcessHit()
        {
            hits--;
            if (hits <= 0)
            {
                CollectResource();
            }
        }
        private void CollectResource()
        {
            _resourceChanger.AddAmount(amount);
            Destroy(gameObject);
        }
    }
}