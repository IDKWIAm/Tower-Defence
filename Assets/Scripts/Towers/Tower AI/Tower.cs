using UnityEngine;

namespace TowerDefence.Towers.TowerAI
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float maxRange;

        [SerializeField] private LayerMask enemyLayer;

        public GameObject FindNearestEnemy()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, maxRange, enemyLayer);

            GameObject nearestEnemy = null;
            float closestDistance = Mathf.Infinity;
            foreach (Collider2D collider in colliders)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = collider.gameObject;
                }
            }

            return nearestEnemy;
        }
    }
}
