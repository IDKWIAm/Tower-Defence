using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Resources
{
    public class ResourceChangeListener : MonoBehaviour
    {
        [SerializeField]
        private ResourceManager resourceManager;

        [SerializeField]
        private ResourceType type;

        [SerializeField]
        private UnityEvent<int> onChange;

        public ResourceType Type => type;
        public UnityEvent<int> OnChange => onChange;

        private void Awake()
        {
            resourceManager.AddListener(this);
        }
    }
}