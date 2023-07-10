using System;
using Attack;
using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour, IAttack
    {
        private EnemyAttack _enemyAttack;
        private void OnTriggerEnter(Collider other)
        {
            CollisionCheck(other.gameObject);
        }

        public void CollisionCheck(GameObject collisionObject)
        {
            if (collisionObject.gameObject.TryGetComponent(out _enemyAttack))
            {
                LaunchAttack(collisionObject.transform);
            }
        }

        public void LaunchAttack(Transform objectToAttack)
        {
            EnemyController enemyController = objectToAttack.transform.parent.GetComponent<EnemyController>();
            enemyController.RemoveEnemy(objectToAttack.gameObject);
        }
    }
}
