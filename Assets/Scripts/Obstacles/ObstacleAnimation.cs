using DG.Tweening;
using UnityEngine;

namespace Obstacles
{
    public class ObstacleAnimation : MonoBehaviour
    {
        
        public float endValue;
        public float duration;

        private void Start()
        {
            transform.DOLocalMoveX(transform.position.x +endValue, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }

     
    }
}
