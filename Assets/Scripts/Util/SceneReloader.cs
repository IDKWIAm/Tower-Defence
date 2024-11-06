using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class SceneReloader : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}