using System;
using Attack;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour, IAttack
    {
        private void OnTriggerEnter(Collider other)
        {
            
        }

        public void CollisionCheck(GameObject collisionObject)
        {
            throw new NotImplementedException();
        }

        public void LaunchAttack(Transform objectToAttack)
        {
            throw new NotImplementedException();
        }
    }
}
