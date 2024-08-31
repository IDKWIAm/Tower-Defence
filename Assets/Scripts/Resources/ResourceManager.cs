using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefence.Resources
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField]
        private List<Resource> resources;

        private readonly List<ResourceChangeListener> _changeListeners = new();


        public int GetAmount(ResourceType type)
        {
            return GetResource(type).Amount;
        }
        public void AddAmount(ResourceType type, int amount)
        {
            var resource = GetResource(type);
            resource.AddAmount(amount);
            _changeListeners.Where(listener => listener.Type == type).ToList()
                .ForEach(listener => listener.OnChange.Invoke(resource.Amount));
        }

        private Resource GetResource(ResourceType type)
        {
            return resources.FirstOrDefault(resource => resource.Type == type);
        }

        public void AddListener(ResourceChangeListener listener)
        {
            _changeListeners.Add(listener);
        }
    }
}