using Pathfinding;
using System.Collections.Generic;
using TowerDefence.Health;
using TowerDefence.House;
using TowerDefence.Towers.TowerAI;
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

        private List<BaseTower> _towersInSight = new List<BaseTower>();

        private Transform _mainBaseTarget;
        private Transform _target;

        private void Awake()
        {
            _destinationSetter = GetComponent<AIDestinationSetter>();
        }

        private void Start()
        {
            _mainBaseTarget = _destinationSetter.target;
        }

        private void Update()
        {
            if (_target == null)
            {
                _towersInSight = CleanList(_towersInSight);
                FindNewTarget();
            }
        }

        private List<BaseTower> CleanList(List<BaseTower> list)
        {
            List<BaseTower> oldList = new List<BaseTower>(list);
            list.Clear();

            foreach (BaseTower item in oldList)
            {
                if (item)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        private void FindNewTarget()
        {
            int highestPriority = 0;

            foreach (BaseTower tower in _towersInSight)
            {
                if (tower.enemyPriority > highestPriority)
                {
                    highestPriority = tower.enemyPriority;
                    _destinationSetter.target = tower.transform.Find("sprite");
                    _target = tower.transform.Find("sprite");
                }
            }

            if (highestPriority == 0)
            {
                _destinationSetter.target = _mainBaseTarget;
                _target = null;
            }
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Sprite")
            {
                if (collision.transform.parent.TryGetComponent<BaseTower>(out BaseTower tower))
                {
                    if (_towersInSight == null) _towersInSight = new List<BaseTower>();
                    _towersInSight.Add(tower);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<BaseTower>(out BaseTower tower))
            {
                if (_towersInSight.Contains(tower))
                {
                    _towersInSight.Remove(tower);
                }
            }
        }

        public void SetTarget(Transform newTarget)
        {
            _destinationSetter.target = newTarget;
        }
    }
}