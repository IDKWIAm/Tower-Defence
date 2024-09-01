using System;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefence.Resources
{
    [Serializable]
    public class Resource
    {
        [SerializeField]
        private ResourceType type;

        [SerializeField]
        private int minAmount, amount, maxAmount;

        public ResourceType Type => type;
        public int Amount => amount;
        public int MinAmount => minAmount;
        public int MaxAmount => maxAmount;

        public void ModifyAmount(int howMuch)
        {
            amount += howMuch;
        }
        public int ClampAmount()
        {
            amount = Math.Clamp(Amount, 0, MaxAmount);
            return amount;
        }
    }
}