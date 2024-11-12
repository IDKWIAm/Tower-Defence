using TowerDefence.House;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;

        [SerializeField]
        private float damagePeriod;

        private float _timer = 0;
        private NavMeshAgent _agent;
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _agent.destination = target?.transform.position ?? transform.position;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var houseHealth = other.gameObject.GetComponent<HouseHealth>();

            if (houseHealth is not null && _timer >= damagePeriod)
            {
                _timer = damagePeriod;
            }
            
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            var houseHealth = other.gameObject.GetComponent<HouseHealth>();

            if (houseHealth is not null && _timer >= damagePeriod)
            {
                houseHealth.Damage(5);
                _timer = 0;
            }

            _timer += Time.deltaTime;
        }

        public void SetTarget(GameObject newTarget)
        {
            target = newTarget;
        }
    }
}