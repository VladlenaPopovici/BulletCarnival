using System;
using Enemy;
using UnityEngine;

namespace WeaponAndBullet
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy;
        [HideInInspector] public WeaponManager weapon;
        private EnemyManager _enemyManager;

        private void Start()
        {
            Destroy(gameObject, timeToDestroy);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<EnemyManager>())
            {
                
            }
            Destroy(gameObject);
        }
    }
}
