using System.Collections.Generic;
using TowerDefence.Enemies;
using TowerDefence.Resources.Objects;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float range;

    [SerializeField] private LayerMask enemyLayer;

    private List<GameObject> hitEnemies = new List<GameObject>();

    private GameObject target;

    private int damage;
    private float hitDelay;
    private int maxChains;
    private float savedRange;

    private int chainCounter;
    private float cnt;

    private bool initedLastFrame;
    private bool stopped;
    
    void Update()
    {
        if (stopped) return;

        if (initedLastFrame)
        {
            initedLastFrame = false;
            return;
        }


        if (cnt < 0)
        {
            if (maxChains > chainCounter)
            {
                target = FindNextNearestEnemy();

                if (target == null)
                {
                    StopMoving();
                    return;
                }
                transform.position = target.transform.position;
                DamageCurrentTarget();
                chainCounter += 1;
                cnt = hitDelay;
            }
            else StopMoving();
        }
        else cnt -= Time.deltaTime;
    }

    private GameObject FindNextNearestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

        GameObject nearestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            float distance = Vector2.Distance(transform.position, collider.transform.position);
            if (distance < closestDistance && hitEnemies.Contains(collider.gameObject) == false)
            {
                closestDistance = distance;
                nearestEnemy = collider.gameObject;
            }
        }
        range = savedRange;
        return nearestEnemy;
    }

    private void DamageCurrentTarget()
    {
        if (target.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            enemyHealth.Damage(damage);
            hitEnemies.Add(target);
        }
    }

    private void StopMoving()
    {
        Invoke("SelfDestroy", 5);
        stopped = true;
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    public void Init(int maxChains, float hitDelay, int damage, float towerRange)
    {
        savedRange = range;
        this.maxChains = maxChains;
        this.hitDelay = hitDelay;
        this.damage = damage;
        target = gameObject;
        range = towerRange;
        initedLastFrame = true;
        DamageCurrentTarget();
    }
}
