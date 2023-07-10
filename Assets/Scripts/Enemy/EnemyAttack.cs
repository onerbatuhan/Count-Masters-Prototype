using System;
using Attack;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour, IAttack
    {
        private PlayerAttack _playerAttack;
        private void OnTriggerEnter(Collider other)
        {
            CollisionCheck(other.gameObject);
        }

        public void CollisionCheck(GameObject collisionObject)
        {
            if (collisionObject.gameObject.TryGetComponent(out _playerAttack))
            {
                LaunchAttack(collisionObject.transform);
            }
        }

        public void LaunchAttack(Transform objectToAttack)
        {
            throw new System.NotImplementedException();
        }
    }
}
