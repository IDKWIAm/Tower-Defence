using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Resources.Objects.DamageTriggers
{
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