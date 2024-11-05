using UnityEngine;

public class BallisticFire : MonoBehaviour
{
    [SerializeField] private float maxRange;

    [SerializeField] private float firerate;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private LayerMask enemyLayer;

    private GameObject target;

    private float fireTimer;

    void Update()
    {
        target = FindNearestEnemy();
        if (target == null) return;
        
        var distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance <= maxRange && fireTimer <= 0)
        {
            LaunchProjectile();
            fireTimer = firerate;
        }
        fireTimer -= Time.deltaTime;
    }
    
    void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<ballisticBullet>().InitTarget(target);
    }

    GameObject FindNearestEnemy()
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
