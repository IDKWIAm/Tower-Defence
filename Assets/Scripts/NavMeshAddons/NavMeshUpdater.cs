using NavMeshPlus.Components;
using UnityEngine;

namespace TowerDefence.NavMeshAddons
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
            _navMeshSurface.BuildNavMesh();
        }

        public void UpdateNavMesh()
        {
            _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
        }
    }
}
