using UnityEngine;

public class TargetedFire : MonoBehaviour
{
    [SerializeField] private float maxRange;

    [SerializeField] private float firerate;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSourse;

    [SerializeField] private LayerMask enemyLayer;

    private float fireTimer;

    private Transform target;

    private void Update()
    {
        target = FindNearestEnemy()?.transform;

        if (target && fireTimer <= 0)
        {
            Fire();
            fireTimer = firerate;
        }

        fireTimer -= Time.deltaTime;
    }

    private void Fire()
    {
        var bullet = Instantiate(bulletPrefab, bulletSourse.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().InitTarget(target);
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
