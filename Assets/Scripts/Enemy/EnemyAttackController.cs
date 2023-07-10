using System;
using System.Collections.Generic;
using Attack;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyAttackController : MonoBehaviour,IAttackController
    {
        private PlayerAttackController _playerAttackController;
        private EnemyController _enemyController;

        private void Start()
        {
            _enemyController = transform.GetComponent<EnemyController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckAttackCollision(other.gameObject);
        }
        
        public void CheckAttackCollision(GameObject collidingObject)
        {
            if (collidingObject.gameObject.TryGetComponent(out _playerAttackController))
            {
                TargetAttack(collidingObject.transform);
            }
        }

        public void StartAttack()
        {
            
        }

        public void TargetAttack(Transform targetTransform)
        {
            Transform closestPlayer = GetClosestPlayer();
            if (closestPlayer != null)
            {
                foreach (var enemy in _enemyController.enemyGroupList)
                {
                    enemy.GetComponent<NavMeshAgent>().SetDestination(closestPlayer.position);
                }
            }
        }

        private Transform GetClosestPlayer()
        {
            List<GameObject> players = PlayerManager.Instance.playerList;

            Transform closestPlayer = null;
            float closestDistance = Mathf.Infinity;
            Vector3 enemyPosition = transform.position;

            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(player.transform.position, enemyPosition);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = player.transform;
                }
            }

            return closestPlayer;
        }
    }
}
