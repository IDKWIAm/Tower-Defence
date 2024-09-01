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
        private int minAmount, maxAmount;

        [SerializeField]
        private int amount;

        public ResourceType Type => type;
        public int Amount => amount;
        public int MinAmount => minAmount;
        public int MaxAmount => maxAmount;

        public void AddAmount(int howMuch)
        {
            amount += howMuch;
            ClampAmount();
        }
        public void ClampAmount()
        {
            amount = Math.Clamp(amount, 0, maxAmount);
        }
    }
}