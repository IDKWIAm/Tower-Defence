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
        private GameObject[] enemyPrefabs;

        [SerializeField]
        private Transform[] spawningPoints;

        public void SpawnEnemies()
        {
            foreach (Transform point in spawningPoints)
            {
                for (var i = 0; i < enemyCount; i++)
                {
                    int enemyIdx = Random.Range(0, enemyPrefabs.Length);
                    var enemy = Instantiate(enemyPrefabs[enemyIdx], 
                        (Vector2)point.position + Random.insideUnitCircle, Quaternion.identity);

                    enemy.GetComponent<EnemyAI>()?.SetTarget(house.transform);
                }
            }
            enemyCount += Mathf.Max(enemyCount / 5, 2);
        }
    }
}