using Attack;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour, IAttack
    {
        public void CollisionCheck(GameObject collisionObject)
        {
            throw new System.NotImplementedException();
        }

        public void LaunchAttack(Transform objectToAttack)
        {
            throw new System.NotImplementedException();
        }
    }
}
