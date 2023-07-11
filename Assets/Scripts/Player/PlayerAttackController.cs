using System;
using System.Collections.Generic;
using System.Linq;
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
            
            foreach (var player in _playerManager.playerList)
            {
                NavMeshAgent navMeshAgent = player.GetComponent<NavMeshAgent>();
                navMeshAgent.ResetPath();
            }
            MovementManager.Instance.KeepPlayersClose();
            
        }


        public void TargetAttack(Transform targetTransform)
        {
           
            EnemyController enemyController = targetTransform.GetComponent<EnemyController>();
            foreach (var player in _playerManager.playerList)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position,
                        enemyController.transform.position, 1* Time.deltaTime);
            }

           
           
        }

        
    }
}
