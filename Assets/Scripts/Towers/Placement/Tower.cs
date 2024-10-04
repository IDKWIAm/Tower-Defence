using UnityEngine;

namespace TowerDefence.Towers.Grid
{
    public class Tower : GridObject
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Color canPlaceColor;
        [SerializeField] private Color cannotPlaceColor;


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