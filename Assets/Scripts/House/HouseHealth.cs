using TowerDefence.Health;
using UnityEngine;
using TowerDefence.Util;

namespace TowerDefence.House
{
    [RequireComponent(typeof(SceneReloader))]
    public class HouseHealth : BaseHealth
    {
        private SceneReloader _sceneReloader;
        private void Awake()
        {
            _sceneReloader = GetComponent<SceneReloader>();
            onDeath.AddListener(GameOver);
        }

        private void GameOver()
        {
            _sceneReloader.ReloadScene();
        }
    }
}