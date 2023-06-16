using System;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class WinLogic : MonoBehaviour
    {
        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }
}