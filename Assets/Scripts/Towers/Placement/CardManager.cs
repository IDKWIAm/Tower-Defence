using TowerDefence.Resources;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

namespace TowerDefence.Towers.Grid
{
    public class CardManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public TowerCard CardObject { get; set; }
        public TowerCardHolder ParentHolder { get; set; }

        public Tilemap tilemap { get; set; }

        private ResourceManager _resourceManager;

        private GameObject _draggingTower;
        private Tower _tower;
        private bool _canPlace;

        private Camera _camera;

        private bool _dragging;

        private RectTransform _towerCardBar;
        private float _savedAnchPos;

        private void Awake()
        {
            _resourceManager = FindObjectOfType<ResourceManager>();

            _camera = Camera.main!;
        }

        private void Start()
        {
            _towerCardBar = ParentHolder.transform.parent.GetComponent<RectTransform>();
            _savedAnchPos = _towerCardBar.anchoredPosition.y;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                Cancel();

            if (Input.GetButtonDown("Fire1"))
                if (_dragging)
                    Place();

            if (_draggingTower != null)
                UpdateDragPosition();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _draggingTower = Instantiate(CardObject.Prefab, Vector3.zero, Quaternion.identity);

            UpdateDragPosition();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _dragging = true;

            var anchoredPos = _towerCardBar.anchoredPosition;
            anchoredPos = new Vector2(_towerCardBar.anchoredPosition.x, 5000);
            _towerCardBar.anchoredPosition = anchoredPos;
        }

        private void Place()
        {
            if (_canPlace)
            {
                CardObject.Cost.ForEach(cost =>
                    {
                        _resourceManager.ModifyAmount(cost.Type, -cost.Amount);
                    });

                Vector2Int gridPosition = (Vector2Int)(tilemap.WorldToCell(_draggingTower.transform.position));

                ParentHolder.GridController.AddTowerCard(_tower, gridPosition);
                _tower.ResetColor();

                Vector3 position = tilemap.GetCellCenterWorld(((Vector3Int)gridPosition));

                var child = _tower.transform.Find("sprite");
                child.position = new Vector3(child.position.x, child.position.y, 0);
                _draggingTower.transform.position = position;
                _draggingTower = null;
                _dragging = false;

                var anchoredPos = _towerCardBar.anchoredPosition;
                anchoredPos = new Vector2(_towerCardBar.anchoredPosition.x, _savedAnchPos);
                _towerCardBar.anchoredPosition = anchoredPos;
            }
        }

        private void UpdateDragPosition()
        {
            _tower = _draggingTower.GetComponent<Tower>();

            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2Int gridPosition = ((Vector2Int)tilemap.WorldToCell(mousePosition));

            _canPlace = ParentHolder.GridController.CanPlace(CardObject, gridPosition, _tower.size);
            _tower.ChangeColor(_canPlace);

            Vector3 position = tilemap.GetCellCenterWorld(((Vector3Int)gridPosition));

            float offsetX = 0.5f * (_tower.size.x - 1);
            float offsetY = 0.5f * (_tower.size.y - 1);

            _tower.transform.position = position;
            _tower.transform.Find("sprite").position = 
                new Vector3(_tower.transform.position.x + offsetX, _tower.transform.position.y + offsetY, -1);
        }

        private void Cancel()
        {
            if (_draggingTower != null)
                Destroy(_draggingTower);
            _dragging = false;

            var anchoredPos = _towerCardBar.anchoredPosition;
            anchoredPos = new Vector2(_towerCardBar.anchoredPosition.x, _savedAnchPos);
            _towerCardBar.anchoredPosition = anchoredPos;
        }
    }
}