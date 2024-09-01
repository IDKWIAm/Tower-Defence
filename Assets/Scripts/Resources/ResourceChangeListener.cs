using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Resources
{
    [DisallowMultipleComponent]
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

        public ResourceManager Manager => resourceManager;

        private void Awake()
        {
            Manager.AddListener(this);
        }
    }
}