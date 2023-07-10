using System;
using Attack;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyAttackControllerController : MonoBehaviour,IAttackController
    {
        private PlayerAttackControllerController _playerAttackControllerController;
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
            if (collidingObject.gameObject.TryGetComponent(out _playerAttackControllerController))
            {
                TargetAttack(collidingObject.transform);
            }
        }

        public void StartAttack()
        {
            
        }

        public void TargetAttack(Transform targetTransform)
        {
            foreach (var enemy in _enemyController.enemyGroupList)
            {
                enemy.GetComponent<NavMeshAgent>().SetDestination(targetTransform.position);
            }
        }
    }
}
