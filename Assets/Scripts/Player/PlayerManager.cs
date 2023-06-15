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
            Time.timeScale = 0;
            loseCanvas.gameObject.SetActive(true);
        }
    }
}
