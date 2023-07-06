using System;
using DesignPattern;
using UnityEngine;

namespace Animation
{
    public class AnimationController : Singleton<AnimationController>
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Attack = Animator.StringToHash("Attack");

        public enum AnimationType
        {
            Idle,
            Run,
            Attack
        }
        
        public void ChangeAnimation(AnimationType animationType, Animator animator)
        {
            switch (animationType)
            {
                case AnimationType.Idle:
                    animator.SetTrigger(Idle);
                    break;
                case AnimationType.Run:
                    animator.SetTrigger(Run);
                    break;
                case AnimationType.Attack:
                    animator.SetTrigger(Attack);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
            }
        }
    }
}
