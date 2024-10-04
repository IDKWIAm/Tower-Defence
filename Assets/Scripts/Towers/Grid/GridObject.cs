using UnityEngine;

namespace TowerDefence.Towers.Grid
{
    public class GridObject : MonoBehaviour
    {
        [SerializeField] private Vector2Int size;
        
        public Vector2Int Size
        {
            get => size;
            set => size = value;
        }
    }
}