using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Resources.Objects.DamageTriggers
{
    /// <summary>
    /// Damages all <see cref="DamageableResourceObject"/>s stored in list when triggered.
    /// </summary>
    public abstract class DamageTrigger : MonoBehaviour
    {
        [SerializeField]
        private List<DamageableResourceObject> objects = new();

        protected void Trigger()
        {
            objects.ForEach(@object => @object.Hit());
        }
    }
}