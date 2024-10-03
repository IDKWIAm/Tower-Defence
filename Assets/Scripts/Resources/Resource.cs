using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefence.Resources
{
    /// <summary>
    /// Represents a resource with a type, an amount and a minimum/maximum amount.
    /// </summary>
    [Serializable]
    public class Resource
    {
        /// <summary>
        /// The resource type.
        /// 
        /// <seealso cref="ResourceType"/>
        /// </summary>
        [SerializeField]
        private ResourceType type;

        /// <summary>
        /// The minimum amount of resource.
        /// </summary>
        [SerializeField]
        private int minAmount;

        /// <summary>
        /// The current amount of resource.
        /// </summary>
        [SerializeField]
        private int amount;

        /// <summary>
        /// The maximum amount of resource.
        /// </summary>
        [SerializeField]
        private int maxAmount;

        public ResourceType Type => type;
        public int Amount => amount;
        public int MinAmount => minAmount;
        public int MaxAmount => maxAmount;

        /// <summary>
        /// Modifies the current amount of resource.
        /// </summary>
        /// <param name="howMuch">Delta amount.</param>
        public void ModifyAmount(int howMuch)
        {
            amount += howMuch;
        }

        /// <summary>
        /// Clamps the current amount of resource between <see cref="minAmount"/> and <see cref="maxAmount"/> and returns the clamped amount.
        /// </summary>
        /// <returns>The clamped amount.</returns>
        public int ClampAmount()
        {
            amount = Math.Clamp(Amount, 0, MaxAmount);
            return amount;
        }

        /// <summary>
        /// Equality comparer for <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource},System.Collections.Generic.IEqualityComparer{TSource})"/>
        /// </summary>
        public class ResourceEqualityComparer : EqualityComparer<Resource>
        {
            /// <summary>
            /// compares resources by type
            /// </summary>
            /// <param name="x">first resource</param>
            /// <param name="y">second resource</param>
            /// <returns>result of comparison</returns>
            public override bool Equals(Resource x, Resource y)
            {
                return x?.Type == y?.Type;
            }
            /// <summary>
            /// computes the hashcode of resource
            /// </summary>
            /// <param name="resource">the resource</param>
            /// <returns>no fucking clue</returns>
            public override int GetHashCode(Resource resource)
            {
                return resource.Type.GetHashCode();
            }
        }
    }
}