using Pathfinding;
using TowerDefence.Health;
using TowerDefence.House;
using TowerDefence.Towers.Health;
using UnityEngine;

namespace TowerDefence.Enemies
{
    [RequireComponent(typeof(AIDestinationSetter))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private int damage;

        [SerializeField]
        private float damagePeriod;

        private float _timer = 0;

        private AIDestinationSetter _destinationSetter;

        private void Awake()
        {
            _destinationSetter = GetComponent<AIDestinationSetter>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag != "Sprite") return;

            var houseHealth = other.transform.parent.gameObject.GetComponent<HouseHealth>();

            if (houseHealth is not null && _timer >= damagePeriod)
            {
                _timer = damagePeriod;
            }
            
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.tag != "Sprite") return;

            BaseHealth structureHealth = other.transform.parent.gameObject.GetComponent<BaseHealth>();

            if (structureHealth is not null && _timer >= damagePeriod)
            {
                structureHealth.Damage(damage);
                _timer = 0;
            }

            _timer += Time.deltaTime;
        }

        public void SetTarget(Transform newTarget)
        {
            _destinationSetter.target = newTarget;
        }
    }
}