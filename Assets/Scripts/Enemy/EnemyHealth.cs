using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float health;
        
        public void TakeDamage(float damage)
        {
            if (!(health > 0)) return;
            
            health -= damage;
            if (health <= 0) EnemyDeath();
            Debug.Log("Hit");
        }

        private void EnemyDeath()
        {
            Debug.Log("Death");
        }
    }
}
