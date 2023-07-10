using System;
using Attack;
using Enemy;
using Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour,IAttack
    {
        private EnemyAttackController _enemyAttackController;
        private PlayerManager _playerManager;
        private SwerveController _swerveController;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _swerveController = SwerveController.Instance;
        }

        private void OnTriggerEnter(Collider other)
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
            _swerveController.StopSwerving();
        }
        
        public void TargetAttack(Transform targetTransform)
        {
            
            foreach (var player in  _playerManager.playerList)
            {
                player.GetComponent<NavMeshAgent>().SetDestination(targetTransform.position);
                
            }
           
        }
    }
}
