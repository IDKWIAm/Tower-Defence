using System.Collections.Generic;
using TowerDefence.Resources;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence.Towers.Placement
{
    public class CardManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public TowerCard CardObject { get; set; }

        private ResourceManager _resourceManager;
        private List<ResourceChanger> _resourceChangers;

        private GameObject _draggingTower;
        private Tower _tower;
        private Vector2Int _gridSize = new(10, 10);
        private bool _canPlace;

        private RectInt _gridRect;
        private Tower[,] _grid;
        private Camera _camera;

        private void Awake()
        {
            _resourceManager = FindObjectOfType<ResourceManager>();

            _camera = Camera.main!;

            _gridRect = new RectInt(-_gridSize.x / 2, -_gridSize.y / 2, _gridSize.x, _gridSize.y);

            _grid = new Tower[_gridRect.xMax - _gridRect.xMin, _gridRect.yMax - _gridRect.yMin];
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (_draggingTower != null)
            {
                UpdateDragPosition();
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _draggingTower = Instantiate(CardObject.Prefab, Vector3.zero, Quaternion.identity);

            UpdateDragPosition();
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (_canPlace)
            {
                var towerPosition = _draggingTower.transform.position;
                _grid[Mathf.FloorToInt(towerPosition.x) - _gridRect.xMin,
                    Mathf.FloorToInt(towerPosition.y) - _gridRect.xMin] = _tower;
                _tower.ResetColor();
            }
            else
            {
                Destroy(_draggingTower);
            }
        }

        private void UpdateDragPosition()
        {
            _tower = _draggingTower.GetComponent<Tower>();

            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int position = new(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y));

            if (position.x <= _gridRect.xMin || position.x >= _gridRect.xMax)
            {
                _canPlace = false;
            }
            else if (position.y <= _gridRect.yMin || position.y >= _gridRect.yMax)
            {
                _canPlace = false;
            }
            else if (IsPlaceTaken(position))
            {
                _canPlace = false;
            }
            else
            {
                _canPlace = true;
            }
            _tower.ChangeColor(_canPlace);

            _draggingTower.transform.position =
                new Vector3(position.x, position.y, _draggingTower.transform.position.z);
        }

        private bool IsPlaceTaken(Vector2Int position)
        {
            return _grid[position.x - _gridRect.xMin, position.y - _gridRect.yMin] != null;
        }
    }
}