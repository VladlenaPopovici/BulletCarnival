using AimStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LoseLogic : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }
}
