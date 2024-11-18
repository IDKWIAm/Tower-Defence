using UnityEngine;

public class YSort : MonoBehaviour
{
    [SerializeField] private bool isDynamic;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (!isDynamic && spriteRenderer != null)
            SortSprite();
    }

    private void Update()
    {
        if (isDynamic && spriteRenderer != null)
            SortSprite();
    }

    private void SortSprite()
    {
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 100 - transform.localScale.y / 2);
    }
}
