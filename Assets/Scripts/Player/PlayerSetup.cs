using System;
using Animation;
using UnityEditor.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerSetup : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }

        public void PlayerConfigure(GameObject currentPlayerObject)
        {
             Transform playersMovedObject = _playerManager.playersMovedObject.transform;
             Animator playerAnimator = currentPlayerObject.GetComponent<Animator>();
            _playerManager.AddPlayer(currentPlayerObject);
            AnimationController.Instance.ChangeAnimation(AnimationController.AnimationType.Run,playerAnimator);
            currentPlayerObject.transform.SetParent(playersMovedObject);
            PlayerSetTransform(currentPlayerObject,playersMovedObject);

        }

        private void PlayerSetTransform(GameObject currentPlayerObject, Transform currentPlayersMovedObject)
        {
            Vector3 randomPosition = Random.insideUnitSphere*_playerManager.playerCloneRadiusValue;
            Vector3 newPosition = currentPlayersMovedObject.position + randomPosition;
            currentPlayerObject.transform.position = newPosition;
        }
        
    }
}

