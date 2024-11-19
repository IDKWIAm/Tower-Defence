using TowerDefence.Resources.Objects;
using UnityEngine;

namespace Player
{
    public class ResourceFinder : MonoBehaviour
    {
        public float searchRadius = 5f;
        public DamageableResourceObject _currentResource;
        [SerializeField] LayerMask resourceLayer;
        private LaserVisualizer _laserVisualizer;
        void Awake()
        {
            _laserVisualizer = GetComponentInChildren<LaserVisualizer>();
        }
        void Update()
        {
            _currentResource = GetNearestResource();
            if (Input.GetButton("Fire1"))
            {
                if (_currentResource != null)
                {
                    ResourceAttacker attacker = GetComponent<ResourceAttacker>();
                    attacker?.Attack(_currentResource);
                    _laserVisualizer.ShowLaser(transform.position, _currentResource.transform.position);
                }
                else _laserVisualizer.HideLaser();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                _laserVisualizer.HideLaser();
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

