using System;
using Animation;
using Level;
using Player;
using UnityEngine;

namespace Obstacles
{
    public class ObstacleStepController : MonoBehaviour
    {
        private PlayerController _playerController;
        private PlayerManager _playerManager;
        private AnimationController _animationController;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _animationController = AnimationController.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (!other.gameObject.TryGetComponent(out _playerController)) return;
            QueryPlayerScore();
            if (PlayerManager.Instance.playerList.Count !< 1) return;
            other.transform.SetParent(null);
            _playerManager.RemovePlayerList(other.gameObject);
            Animator animator = other.gameObject.GetComponent<Animator>();
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            _animationController.ChangeAnimation(AnimationController.AnimationType.Idle,animator);
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            

        }

        private void QueryPlayerScore()
        {
            if (PlayerManager.Instance.playerList.Count == 1)
            {
                LevelManager.Instance.LevelSuccess();
            }
        }
    }
}
