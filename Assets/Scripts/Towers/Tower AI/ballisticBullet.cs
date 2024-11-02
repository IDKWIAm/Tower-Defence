using UnityEngine;

public class ballisticBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem particles;

    private float targetY = Mathf.Infinity;
        
    private void Update()
    {
        if (rb.velocity.y < 0 && transform.position.y <= targetY)
            Explode();
    }

    public void InitTargetPoint(float y)
    {
        targetY = y;
    }

    private void Explode()
    {
        particles.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
    }
}
