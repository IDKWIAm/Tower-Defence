using NavMeshPlus.Components;
using UnityEngine;

namespace TowerDefence.Enemies
{
    [RequireComponent(typeof(NavMeshSurface))]
    public class NavMeshUpdater : MonoBehaviour
    {
        private NavMeshSurface _navMeshSurface;
        private void Awake()
        {
            _navMeshSurface = GetComponent<NavMeshSurface>();
        }

        private void Start()
        {
            _navMeshSurface.BuildNavMeshAsync();
        }

        public void UpdateNavMesh()
        {
            _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
        }
    }
}
