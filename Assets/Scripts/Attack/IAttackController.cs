using Enemy;
using UnityEngine;

namespace Attack
{
    public interface IAttackController
    {
        void CheckAttackCollision(GameObject collidingObject);
        void StartAttack();

        void TargetAttack(Transform targetTransform);
        public Transform GetClosestTransform(EnemyController enemyController = null);

    }
}