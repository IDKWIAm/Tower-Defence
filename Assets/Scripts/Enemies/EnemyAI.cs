﻿using TowerDefence.Health;
using TowerDefence.House;
using TowerDefence.Towers.Health;
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
        private int damage;

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
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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
            BaseHealth structureHealth = other.gameObject.GetComponent<HouseHealth>();
            if (!structureHealth) structureHealth = other.gameObject.GetComponent<TowerHealth>();

            if (structureHealth is not null && _timer >= damagePeriod)
            {
                structureHealth.Damage(damage);
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