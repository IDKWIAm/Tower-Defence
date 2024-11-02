using UnityEngine;

public class TowerAI : MonoBehaviour
{
    [SerializeField] private float maxRange;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float firerate;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSourse;

    [SerializeField] private Transform target;

    private float fireTimer;

    private void Update()
    {
        fireTimer -= Time.deltaTime;
        Aim();
    }

    private void Aim() 
    {
        if (target == null) return;

        var distance = Vector2.Distance(transform.position, target.position);

        if (distance <= maxRange)
        {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            var targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

            if (transform.rotation == targetRotation && fireTimer <= 0)
            {
                Fire();
                fireTimer = firerate;
            }
        }
    }

    private void Fire()
    {
        var bullet = Instantiate(bulletPrefab, bulletSourse.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce((target.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
    }
}
