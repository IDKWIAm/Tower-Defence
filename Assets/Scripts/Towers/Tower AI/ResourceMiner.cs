using TowerDefence.Resources;
using TowerDefence.Towers.TowerAI;
using UnityEngine;

namespace TowerDefence
{
    public class ResourceMiner : BaseTower
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private int amount;

        private ResourceManager _resourceManager;

        private void Awake()
        {
             _resourceManager = GameObject.FindGameObjectWithTag("Resource Manager").GetComponent<ResourceManager>();
        }

        public void Mine()
        {
            if (placed)
                _resourceManager.ModifyAmount(ResourceType.Wood, amount);
        }
    }
}
