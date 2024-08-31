using System;
using UnityEngine;

namespace TowerDefence.Resources
{
    [Serializable]
    public class Resource
    {
        [SerializeField]
        private ResourceType type;
        [SerializeField]
        private int amount;

        public ResourceType Type => type;
        public int Amount => amount;
        public void AddAmount(int howMuch)
        {
            amount += howMuch;
        }
    }
}