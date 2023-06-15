using System;
using Enemy;
using UnityEngine;

namespace WeaponAndBullet
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy;
        [HideInInspector] public WeaponManager weapon;

        private void Start()
        {
            Destroy(gameObject, timeToDestroy);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponentInParent<EnemyHealth>())
            {
                var enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
                enemyHealth.TakeDamage(weapon.damage);
            }
            Destroy(gameObject);
        }
    }
}
