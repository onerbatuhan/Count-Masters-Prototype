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
        private EnemyController _enemyController;
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
            CheckAttackFinished(other.gameObject);

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
            _swerveController.StopSwerving();
        }

        public void CheckAttackFinished(GameObject collidingObject)
        {
            if (!collidingObject.TryGetComponent(out _enemyAttackController)) return;
            _enemyController = _enemyAttackController.GetComponent<EnemyController>();
            if (_enemyController.enemyGroupList.Count == 0)
            {
                _enemyController.transform.gameObject.SetActive(false);
                    FinishAttack();
                    
            }
        }

        public void FinishAttack()
        {
            _swerveController.ResetToOriginalValues();
            _swerveController.StartSwerving();
            _swerveController.UpdateMovedObjectLimit();
            foreach (var player in _playerManager.playerList)
            {
                NavMeshAgent navMeshAgent = player.GetComponent<NavMeshAgent>();
                navMeshAgent.ResetPath();
            }

        }


        public void TargetAttack(Transform targetTransform)
        {
           
            EnemyController enemyController = targetTransform.GetComponent<EnemyController>();
            Transform closestPlayer = GetClosestTransform(enemyController);
            if (closestPlayer != null)
            {
                foreach (var player in _playerManager.playerList)
                {
                    player.GetComponent<NavMeshAgent>().SetDestination(closestPlayer.position);
                }
            }
           
        }

        public Transform GetClosestTransform(EnemyController enemyController)
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
