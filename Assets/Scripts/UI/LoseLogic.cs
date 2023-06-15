using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoseLogic : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }
    }
}
