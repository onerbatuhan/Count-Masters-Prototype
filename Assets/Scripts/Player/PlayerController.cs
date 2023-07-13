using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        
        [HideInInspector] public bool fallIsControllable;
        private PlayerManager _playerManager;
       
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }

        private void LateUpdate()
        {
            if (fallIsControllable)
            {
                CheckPlayerFalling();
            }
            
        }

        private void CheckPlayerFalling()
        {
            if (!(_playerManager.playerMovementPath.position.y > transform.position.y)) return;
            fallIsControllable = false;
            _playerManager.RemovePlayer(gameObject);
            
        }
    }
}
