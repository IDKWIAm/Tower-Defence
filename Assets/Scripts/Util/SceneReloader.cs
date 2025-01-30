using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefence.Util
{
    public class SceneReloader : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}