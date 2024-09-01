using UnityEngine;

namespace TowerDefence.Resources.Modifiers
{
    public class PercentModifier : Modifier<int, float>
    {
        [SerializeField]
        private ResourceManager resourceManager;

        [SerializeField]
        private ResourceType type;
        
        protected override float Modify(int value)
        {
            return (float)value / resourceManager.GetResource(type).MaxAmount;
        }
    }
}