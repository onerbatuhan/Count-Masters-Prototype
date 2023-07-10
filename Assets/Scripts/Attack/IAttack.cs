using UnityEngine;

namespace Attack
{
    public interface IAttack
    {
        void CollisionCheck(GameObject collisionObject);
        void LaunchAttack(Transform objectToAttack);
    }
}
