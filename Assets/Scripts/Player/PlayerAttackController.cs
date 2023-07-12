using System;
using System.Collections.Generic;
using System.Linq;
using Attack;
using Enemy;
using Movement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour,IAttackController
    {
        private EnemyAttackController _enemyAttackController;
        private EnemyController _enemyController;
        private PlayerManager _playerManager;
        private SwerveController _swerveController;
        public bool isTriggerStayDisabled;
        public UnityEvent postAttackMovementEvent;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _swerveController = SwerveController.Instance;
        }
        

        private void OnTriggerStay(Collider other)
        {
            if (!isTriggerStayDisabled)
            {
                CheckAttackCollision(other.gameObject);
                CheckAttackFinished(other.gameObject);
            }
            
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
           _swerveController.SwervingClosing();
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
            isTriggerStayDisabled = true;
            postAttackMovementEvent.Invoke();
        }


        public void TargetAttack(Transform targetTransform)
        {
            EnemyController enemyController = targetTransform.GetComponent<EnemyController>();
            GameObject lowestZObject = enemyController.enemyGroupList.OrderByDescending(obj => obj.transform.position.z).FirstOrDefault();
            foreach (var player in _playerManager.playerList.Where(player => lowestZObject != null))
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position,
                    lowestZObject.transform.position, 1* Time.deltaTime);
            }
        }
    }
}
