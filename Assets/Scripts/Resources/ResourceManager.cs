using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefence.Resources
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField]
        private List<Resource> resources;

        private readonly List<ResourceChangeListener> _changeListeners = new();

        private void Awake()
        {
            RemoveDuplicates();
            resources.ForEach(resource => resource.ClampAmount());
        }

        public void AddAmount(ResourceType type, int amount)
        {
            var resource = GetResource(type);
            resource.AddAmount(amount);
            _changeListeners.Where(listener => listener.Type == type).ToList()
                .ForEach(listener => listener.OnChange.Invoke(resource.Amount));
        }

        public Resource GetResource(ResourceType type)
        {
            RemoveDuplicates();
            if (resources.All(r => r.Type != type))
            {
                throw new Exception($"Resource type {type.ToString()} does not exist");
            }
            return resources.First(resource => resource.Type == type);
        }

        public void AddListener(ResourceChangeListener listener)
        {
            _changeListeners.Add(listener);
        }
        private void RemoveDuplicates()
        {
            resources = resources.DistinctBy(res => res.Type).ToList();
        }
    }
}