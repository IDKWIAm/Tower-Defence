using UnityEngine;

namespace TowerDefence.Towers.Grid
{
    public class GridObject : MonoBehaviour
    {
        [SerializeField] private Vector2Int _size;
        
        public Vector2Int size
        {
            get => _size;
            set => _size = value;
        }
    }
}