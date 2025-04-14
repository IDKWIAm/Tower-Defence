using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Resources;
using UnityEngine;

namespace TowerDefence.Towers.Grid
{
    [CreateAssetMenu(fileName = "TowerCard", menuName = "Towers/New TowerColorController Card")]
    public class TowerCard : ScriptableObject
    {
        [SerializeField]
        private Sprite icon;

        public Sprite Icon => icon;

        [SerializeField]
        private GameObject prefab;

        public GameObject Prefab => prefab;

        [SerializeField]
        private List<ResourceCost> cost;

        public List<ResourceCost> Cost => cost;


        public string FormatCost()
        {
            return string.Join(" ", cost.Select(map => map.Format()));
        }

        [Serializable]
        public class ResourceCost
        {
            [SerializeField]
            private ResourceType type;

            public ResourceType Type => type;

            [SerializeField]
            private int amount;

            public int Amount => amount;

            public string Format()
            {
                const string format = @"<sprite name=""{0}""> {1}";
                return string.Format(format, type.GetSpriteName(), amount);
            }
        }
    }
}