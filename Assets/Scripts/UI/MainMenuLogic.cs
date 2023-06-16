using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuLogic : MonoBehaviour
    {
        public void StartButton()
        {
            SceneManager.LoadScene(1);
        }

        public void QuitButton()
        {
            Application.Quit();
            Debug.Log("Quit");
        }

        public void ResultButton()
        {
            SceneManager.LoadScene(2);
        }
    }
}