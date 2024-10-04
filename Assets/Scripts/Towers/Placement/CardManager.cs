using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Resources;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence.Towers.Placement
{
    public class CardManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public TowerCard CardObject { get; set; }
        public TowerCardHolder ParentHolder { get; set; }

        private ResourceManager _resourceManager;

        private GameObject _draggingTower;
        private Tower _tower;
        private bool _canPlace;

        private Camera _camera;

        private void Awake()
        {
            _resourceManager = FindObjectOfType<ResourceManager>();

            _camera = Camera.main!;

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
                CardObject.Cost.ForEach(cost =>
                    {
                        _resourceManager.ModifyAmount(cost.Type, -cost.Amount);
                    });
                var towerPosition = _draggingTower.transform.position;
                ParentHolder.GridController.AddTowerCard(_tower,
                    new Vector2Int(Mathf.FloorToInt(towerPosition.x), Mathf.FloorToInt(towerPosition.y)));
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
            _canPlace = ParentHolder.GridController.CanPlace(CardObject, position);
            _tower.ChangeColor(_canPlace);

            _draggingTower.transform.position =
                new Vector3(position.x, position.y, _draggingTower.transform.position.z);
        }
    }
}