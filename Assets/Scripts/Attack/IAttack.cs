using UnityEngine;

namespace Attack
{
    public interface IAttack
    {
        void CheckAttackCollision(GameObject collidingObject);
        void StartAttack();

        void TargetAttack(Transform targetTransform);

    }
}