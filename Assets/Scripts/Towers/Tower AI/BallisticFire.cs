using UnityEngine;

public class BallisticFire : MonoBehaviour
{
    [SerializeField] private float maxRange;

    [SerializeField] private float firerate;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private Transform target;

    private float startingPower;
    private float grav;
    private float fireTimer;

    private void Start()
    {
        grav = 9.8f;
        startingPower = maxRange + 1;
    }

    void Update()
    {
        var distance = Vector2.Distance(transform.position, target.position);

        if (distance <= maxRange && fireTimer <= 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<ballisticBullet>().InitTargetPoint(target.position.y);
            LaunchProjectile(projectile);
            fireTimer = firerate;
        }
        fireTimer -= Time.deltaTime;
    }

    void LaunchProjectile(GameObject projectile)
    {
        float yDir = projectile.transform.position.y - target.position.y;
        float xDir = target.position.x - transform.position.x;
        
        Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
        projectileBody.velocity = CalculateVelocity(xDir, yDir);

    }

    Vector2 CalculateVelocity(float xDir, float yDir)
    {
        Vector2 displacementX = new Vector2(xDir, 0);

        Vector2 velocityY = Vector2.up * Mathf.Sqrt(2 * grav * (startingPower - yDir));
        Vector2 velocityX = displacementX / (Mathf.Sqrt(2 * (startingPower - yDir) / grav) + Mathf.Sqrt(2 * startingPower / grav));

        Vector2 velocity = velocityX + velocityY;
        return velocity;
    }
}
