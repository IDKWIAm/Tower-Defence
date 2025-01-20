using UnityEngine;

public class ChainLightning : MonoBehaviour
{

    [SerializeField] private float fireRange;
    [SerializeField] private float firerate;
    [SerializeField] private int chainLength;
    [SerializeField] private float lightningHitDelay;
    [SerializeField] private int lightningDamage;

    [SerializeField] private Transform bulletSource;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private LayerMask enemyLayer;

    private GameObject target;

    private float fireTimer;

    private void Start()
    {
        if (bulletSource == null)
        {
            bulletSource = transform;
        }
    }

    void Update()
    {
        target = FindNearestEnemy();
        if (target == null) return;

        var distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance <= fireRange && fireTimer <= 0)
        {
            Fire();
            fireTimer = firerate;
        }
        fireTimer -= Time.deltaTime;
    }

    void Fire()
    {
        GameObject lightning = Instantiate(projectilePrefab, bulletSource.position, Quaternion.identity);
        lightning.GetComponent<Lightning>().Init(chainLength, lightningHitDelay, lightningDamage, fireRange);
    }

    private GameObject FindNearestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, fireRange, enemyLayer);

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
