using System.Linq;
using TowerDefence.Resources;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Towers.Grid
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private UnityEvent onUpdate;
        [SerializeField] private Vector2Int _gridSize;
        private RectInt _gridRect;
        private GridObject[,] _grid;

        private void Awake()
        {
            _gridRect = new RectInt(-_gridSize.x / 2, -_gridSize.y / 2, _gridSize.x, _gridSize.y);

            _grid = new GridObject[_gridRect.xMax - _gridRect.xMin, _gridRect.yMax - _gridRect.yMin];
        }

        public void AddTowerCard(Tower tower, Vector2Int position)
        {
            _grid[position.x - _gridRect.xMin, position.y - _gridRect.yMin] = tower;
            onUpdate.Invoke();
        }

        public bool CanPlace(TowerCard card, Vector2Int position)
        {
            // ReSharper disable once ReplaceWithSingleAssignment.True
            var returnValue = true;

            if (!InsideGrid(position))
            {
                returnValue = false;
            }
            else if (IsOccupied(position))
            {
                returnValue = false;
            }
            else if (!card.Cost.All(cost =>
                    {
                        var resource = resourceManager.GetResource(cost.Type);
                        return resource.Amount - resource.MinAmount >= cost.Amount;
                    })
                )
            {
                returnValue = false;
            }
            return returnValue;

        }
        private bool InsideGrid(Vector2Int position)
        {
            var fitsX = position.x >= _gridRect.xMin && position.x < _gridRect.xMax;
            var fitsY = position.y >= _gridRect.yMin && position.y < _gridRect.yMax;
            
            return fitsX && fitsY;
        }
        private bool IsOccupied(Vector2Int position)
        {
            return _grid[position.x - _gridRect.xMin, position.y - _gridRect.xMin] != null;
        }
    }
}