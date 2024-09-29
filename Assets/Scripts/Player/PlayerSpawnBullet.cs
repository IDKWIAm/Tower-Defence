using System.Collections;
using UnityEngine;

public class PlayerSpawnBullet : MonoBehaviour
{
    public float shootRate;
    public GameObject bulletPrefab;
    void Start()
    {
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {
        while (true)
        {
            while ((Input.GetButton("Fire1")))
            {
                Instantiate(bulletPrefab, transform.position, transform.rotation);
                yield return new WaitForSeconds(shootRate);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
