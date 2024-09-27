using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefence.Resources
{
    /// <summary>
    /// Maps resource types to resources and invokes resource change listeners.
    /// </summary>
    public class ResourceManager : MonoBehaviour
    {
        /// <summary>
        /// All defined resources.
        /// </summary>
        [SerializeField]
        private List<Resource> resources;

        private readonly List<ResourceChangeListener> _changeListeners = new();

        private void Awake()
        {
            RemoveDuplicates();
            resources.ForEach(resource => resource.ClampAmount());
        }

        /// <summary>
        /// Modifies the amount of a resource with a given type and invokes change listeners for the resource with the given type.
        /// </summary>
        /// <param name="type">The resource type.</param>
        /// <param name="delta">Delta amount.</param>
        public void ModifyAmount(ResourceType type, int delta)
        {
            var resource = GetResource(type);
            resource.ModifyAmount(delta);
            _changeListeners.Where(listener => listener.Type == type).ToList()
                .ForEach(listener => listener.OnChange.Invoke(resource.Amount));
        }

        /// <summary>
        /// Gets a resource from type.
        /// </summary>
        /// <param name="type">The resource type to get.</param>
        /// <returns>The resource mapped to the given type</returns>
        /// <exception cref="Exception">When given a type that does not have a mapping to a resource.</exception>
        public Resource GetResource(ResourceType type)
        {
            RemoveDuplicates();
            if (resources.All(res => res.Type != type))
            {
                throw new Exception(
                    $@"Resource type {type.DisplayName()} not defined in Resource Manager ""{gameObject.name}""");
            }
            return resources.First(resource => resource.Type == type);
        }

        /// <summary>
        /// Adds a resource change listener.
        /// </summary>
        /// <seealso cref="ResourceChangeListener"/>
        /// <param name="listener">The listener to add.</param>
        public void AddListener(ResourceChangeListener listener)
        {
            _changeListeners.Add(listener);
        }
        
        /// <summary>
        /// Removes duplicate resource types.
        /// </summary>
        private void RemoveDuplicates()
        {
            resources = resources.DistinctBy(res => res.Type).ToList();
        }
    }
}