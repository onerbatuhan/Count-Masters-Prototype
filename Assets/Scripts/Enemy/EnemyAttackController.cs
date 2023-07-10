using System;
using System.Collections.Generic;
using Animation;
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
        private AnimationController _animationController;

        private void Start()
        {
            _enemyController = transform.GetComponent<EnemyController>();
            _animationController = AnimationController.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckAttackCollision(other.gameObject);
        }
        
        public void CheckAttackCollision(GameObject collidingObject)
        {
            if (collidingObject.gameObject.TryGetComponent(out _playerAttackController))
            {
                StartAttack();
                TargetAttack(collidingObject.transform);
            }
        }

        public void StartAttack()
        {
            foreach (var enemy in _enemyController.enemyGroupList)
            {
                 Animator animator = enemy.GetComponent<Animator>();
                _animationController.ChangeAnimation(AnimationController.AnimationType.Run,animator);
            }
        }

        public void TargetAttack(Transform targetTransform)
        {
            Transform closestPlayer = GetClosestTransform();
            if (closestPlayer != null)
            {
                foreach (var enemy in _enemyController.enemyGroupList)
                {
                    enemy.GetComponent<NavMeshAgent>().SetDestination(closestPlayer.position);
                }
            }
        }

        public Transform GetClosestTransform(EnemyController enemyController = null)
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
