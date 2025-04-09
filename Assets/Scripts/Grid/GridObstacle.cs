using TowerDefence.Towers.Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerDefence.Grid
{
    public class GridObstacle : GridObject
    {
        private GridController _gridController;

        private Tilemap _tilemap;

        void Start()
        {
            _gridController = 
                GameObject.FindGameObjectWithTag("Grid Controller").GetComponent<GridController>();
            _tilemap =
                GameObject.FindGameObjectWithTag("Placeholder Tilemap").GetComponent<Tilemap>();

            Vector2Int gridPosition = ((Vector2Int)_tilemap.WorldToCell(transform.position));

            if (_gridController.CanPlace(null, gridPosition, size))
            {
                int bottom = gridPosition.x - (size.x / 2 - (1 - size.x % 2));
                int left = gridPosition.y - (size.y / 2 - (1 - size.y % 2));

                Vector2Int leftBottomCorner = new Vector2Int(bottom, left);

                _gridController.AddTowerCard(this, leftBottomCorner);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
