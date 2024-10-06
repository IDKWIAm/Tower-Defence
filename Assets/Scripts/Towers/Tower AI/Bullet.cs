using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    void Start()
    {
        Invoke("selfDestroy", lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void selfDestroy()
    {
        Destroy(gameObject);
    }
}
