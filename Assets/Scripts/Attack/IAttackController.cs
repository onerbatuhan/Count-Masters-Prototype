using Enemy;
using UnityEngine;

namespace Attack
{
    public interface IAttackController
    {
        
        void CheckAttackCollision(GameObject collidingObject);
        void StartAttack();
        void CheckAttackFinished(GameObject collidingObject);
        void FinishAttack();

        void TargetAttack(Transform targetTransform);
       

    }
}