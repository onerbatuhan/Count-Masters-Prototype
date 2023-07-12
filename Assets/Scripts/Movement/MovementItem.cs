using UnityEngine;

namespace Movement
{
    [CreateAssetMenu(fileName = "NewMovementItem", menuName = "Data/Movement/New Movement Item")]
    public class MovementItem : ScriptableObject
    {
        public float playerSpeed;
        public float swerveSpeed;
        public float swerveAmount;
        public float transitionSpeed;
        
    }
}

