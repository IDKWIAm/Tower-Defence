using UnityEngine;

namespace TowerDefence.Towers.Placement
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Vector2Int size;
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Color canPlaceColor;
        [SerializeField] private Color cannotPlaceColor;

        public Vector2Int Size
        {
            get => size;
            set => size = value;
        }

        public void ChangeColor(bool canPlaceThere)
        {
            renderer.material.color = canPlaceThere ? canPlaceColor : cannotPlaceColor;
        }
        public void ResetColor()
        {
            renderer.material.color = Color.white;
        }
    }
}