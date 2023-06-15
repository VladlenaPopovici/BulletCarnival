using System;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private float damage = 20;
        [SerializeField] private float health = 60;
        private Animator _animator;

        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        private static readonly int Dead = Animator.StringToHash("Dead");

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == player)
            {
                player.GetComponent<PlayerManager>().Hit(damage);
                _animator.SetBool(IsAttacking, true);
            }
            else
            {
                _animator.SetBool(IsAttacking, false);
            }
        }

        public void Hit(float damageEnemy)
        {
            health -= damageEnemy;

            if (health <= 0)
            {
                _animator.SetTrigger(Dead);
                
                Destroy(gameObject, 5);
            }
        }
    }
}
