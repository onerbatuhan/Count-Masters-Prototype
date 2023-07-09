using System;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Movement
{
    public class FallController : MonoBehaviour
    {
        
        private enum FallState
        {
            Preparation,
            ExitPreparation
        }
        [SerializeField] private FallState fallState;
        private PlayerManager _playerManager;
        private ObjectTypes _objectTypes;
        private PlayerController _playerController;
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (!other.gameObject.TryGetComponent(out _objectTypes) || _objectTypes.objectType != ObjectTypes.Type.Character) return;
            switch (fallState)
            {
                case FallState.Preparation:
                    PrepareForFall(other.gameObject);
                    break;
                case FallState.ExitPreparation:
                    ExitFallPreparation(other.gameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out _objectTypes) || _objectTypes.objectType != ObjectTypes.Type.Character) return;
            switch (fallState)
            {
                case FallState.Preparation:
                    PrepareForFall(other.gameObject);
                    
                    break;
                case FallState.ExitPreparation:
                    ExitFallPreparation(other.gameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PrepareForFall(GameObject playerObject)
        {
            _playerController = playerObject.GetComponent<PlayerController>();
            _playerController.fallIsControllable = true;
            _playerManager.SetPlayerProperties(playerObject, navmeshEnable: false, useGravity: true, isTrigger: false);
        }

        private void ExitFallPreparation(GameObject playerObject)
        {
            _playerController = playerObject.GetComponent<PlayerController>();
            _playerController.fallIsControllable = false;
            _playerManager.SetPlayerProperties(playerObject,navmeshEnable: true, useGravity: false, isTrigger: true);
           
        }

        
        
    }
}