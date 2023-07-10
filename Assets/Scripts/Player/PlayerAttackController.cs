using System;
using System.Collections.Generic;
using Attack;
using Enemy;
using Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour,IAttackController
    {
        private EnemyAttackController _enemyAttackController;
        private PlayerManager _playerManager;
        private SwerveController _swerveController;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _swerveController = SwerveController.Instance;
        }

        private void OnTriggerStay(Collider other)
        {
          
            CheckAttackCollision(other.gameObject);
            
        }
        public void CheckAttackCollision(GameObject collidingObject)
        {
            if (collidingObject.TryGetComponent(out _enemyAttackController))
            {
                StartAttack();
                TargetAttack(collidingObject.transform);
            }
        }

        public void StartAttack()
        {
            _swerveController.speed = 0;
            // _swerveController.StopSwerving();
        }

        

        public void TargetAttack(Transform targetTransform)
        {
           
            EnemyController enemyController = targetTransform.GetComponent<EnemyController>();
            Transform closestPlayer = GetClosestPlayer(enemyController);
            if (closestPlayer != null)
            {
                foreach (var player in _playerManager.playerList)
                {
                    player.GetComponent<NavMeshAgent>().SetDestination(closestPlayer.position);
                }
            }
           
        }

        private Transform GetClosestPlayer(EnemyController enemyController)
        {
            List<GameObject> players = enemyController.enemyGroupList;

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
