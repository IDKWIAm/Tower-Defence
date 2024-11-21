using TowerDefence.Resources.Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class ResourceFinder : MonoBehaviour
    {
        public float searchRadius = 5f;
        public DamageableResourceObject _currentResource;
        [SerializeField] LayerMask resourceLayer;
        private LaserVisualizer _laserVisualizer;
        private ResourceRenderer _resourceRenderer;
        private bool isCollecting;

        void Awake()
        {
            _laserVisualizer = GetComponentInChildren<LaserVisualizer>();
            _resourceRenderer = GetComponent<ResourceRenderer>();
        }
        void Update()
        {
            if (isCollecting) Collecting();
            else HandleResourceHighlight();
        }
        public void OnCollect(InputAction.CallbackContext context)
        {
            isCollecting = !isCollecting;
            if (!isCollecting) 
            {
                _laserVisualizer.HideLaser();
                _resourceRenderer.ClearHighlight();
            }
        }
        private void Collecting()
        {
            _currentResource = GetNearestResource();
            if (_currentResource != null)
            {
                ResourceAttacker attacker = GetComponent<ResourceAttacker>();
                attacker?.Attack(_currentResource);
                _laserVisualizer.ShowLaser(transform.position, _currentResource.transform.position);
            }
            else _laserVisualizer.HideLaser();
        }
        private void HandleResourceHighlight()
        {
            DamageableResourceObject nearestResource = GetNearestResource();
            if (nearestResource != null)
            {
                if (_currentResource != nearestResource)
                {
                    if (_currentResource != null) _resourceRenderer.ClearHighlight();
                    _currentResource = nearestResource;
                    _resourceRenderer.HighlightResource(_currentResource);
                }
            }
            else if (_currentResource != null)
            {
                _resourceRenderer.ClearHighlight();
                _currentResource = null;
            }
        }
        DamageableResourceObject GetNearestResource()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, resourceLayer);
            DamageableResourceObject nearestResource = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider2D collider in colliders)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestResource = collider.GetComponent<DamageableResourceObject>();
                }
            }
            return nearestResource;
        }
    }
}


