using AimStates;
using Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private Canvas loseCanvas;
        
        void Start()
        {
            
        }

        void Update()
        {
        
        }

        public void Hit(float damage)
        {
            health -= damage;

            if (health <= 0)
            {
                LoseGameUI();
            }
        }

        private void LoseGameUI()
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            loseCanvas.gameObject.SetActive(true);
        }
    }
}
