using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;

        private NavMeshAgent _agent;
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _agent.destination = target.transform.position;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }
    }
}