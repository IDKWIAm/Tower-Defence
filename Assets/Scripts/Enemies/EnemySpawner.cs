using UnityEngine;

namespace TowerDefence.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private int enemyCount;

        [SerializeField]
        private GameObject house;

        [SerializeField]
        private GameObject enemyPrefab;

        [SerializeField]
        private Transform[] spawningPoints;

        public void SpawnEnemies()
        {
            foreach (Transform point in spawningPoints)
            {
                for (var i = 0; i < enemyCount; i++)
                {
                    var enemy = Instantiate(enemyPrefab, (Vector2)point.position + Random.insideUnitCircle, Quaternion.identity);
                    enemy.GetComponent<EnemyAI>()?.SetTarget(house);
                }
            }
            enemyCount += Mathf.Max(enemyCount / 5, 2);
        }
    }
}