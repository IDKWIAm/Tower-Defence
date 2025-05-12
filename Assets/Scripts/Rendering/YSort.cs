using UnityEngine;

namespace TowerDefence.Util
{
    public class YSort : MonoBehaviour
    {
        [SerializeField] private bool isDynamic;

        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (!isDynamic && _spriteRenderer != null)
                SortSprite();
        }

        private void Update()
        {
            if (isDynamic && _spriteRenderer != null)
                SortSprite();
        }

        private void SortSprite()
        {
            _spriteRenderer.sortingOrder = -(int)(transform.position.y * 100 - transform.localScale.y / 2);
        }
    }
}
