using System.Linq;
using TowerDefence.Resources;
using UnityEngine;

namespace TowerDefence.Towers.Placement
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private ResourceManager resourceManager;
        private Vector2Int _gridSize = new(10, 10);
        private RectInt _gridRect;
        private Tower[,] _grid;

        private void Awake()
        {
            _gridRect = new RectInt(-_gridSize.x / 2, -_gridSize.y / 2, _gridSize.x, _gridSize.y);

            _grid = new Tower[_gridRect.xMax - _gridRect.xMin, _gridRect.yMax - _gridRect.yMin];
        }

        public void AddTowerCard(Tower tower, Vector2Int position)
        {
            _grid[position.x - _gridRect.xMin, position.y - _gridRect.yMin] = tower;
        }

        public bool CanPlace(TowerCard card, Vector2Int position)
        {
            if (position.x <= _gridRect.xMin || position.x >= _gridRect.xMax)
            {
                return false;
            }
            if (position.y <= _gridRect.yMin || position.y >= _gridRect.yMax)
            {
                return false;
            }
            if (_grid[position.x - _gridRect.xMin, position.y - _gridRect.yMin] != null)
            {
                return false;
            }
            if (!card.Cost.All(cost =>
                    {
                        var resource = resourceManager.GetResource(cost.Type);
                        return resource.Amount - resource.MinAmount >= cost.Amount;
                    })
                )
            {
                return false;
            }
            return true;

        }
    }
}