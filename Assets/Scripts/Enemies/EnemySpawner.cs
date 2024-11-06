using UnityEngine;

namespace TowerDefence.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject house;

        [SerializeField]
        private GameObject enemyPrefab;

        public void SpawnEnemies()
        {
            for (var i = 0; i < 3; i++)
            {
                var enemy = Instantiate(enemyPrefab, new Vector2(10, 10) + Random.insideUnitCircle, Quaternion.identity);
                enemy.GetComponent<EnemyAI>()?.SetTarget(house);
            }
        }
    }
}