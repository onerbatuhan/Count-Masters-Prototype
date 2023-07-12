using System;
using Player;
using UnityEngine;
using Utilities;

namespace Obstacles
{
    public class ObstacleFallController : MonoBehaviour
    {
        
        private enum FallState
        {
            Preparation,
            ExitPreparation
        }
        private enum FallPhysicsType
        {
            Refresh,
            Restore
        }
        [SerializeField] private FallState fallState;
        private PlayerManager _playerManager;
        private ObjectTypes _objectTypes;
        private PlayerController _playerController;
        private int _triggerCounterLimit;
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            
        }

        private void OnTriggerEnter(Collider other)
        {
            PreparationFilter(other.gameObject);
        }

        private void PreparationFilter(GameObject currentObject)
        {
            if (!currentObject.TryGetComponent(out _objectTypes) || _objectTypes.objectType != ObjectTypes.Type.Player) return;
            if (_triggerCounterLimit < _playerManager.playerList.Count)
            {
                switch (fallState)
                {
                    case FallState.Preparation:
                        PrepareForFall(currentObject);
                        break;
                    case FallState.ExitPreparation:
                        ExitFallPreparation(currentObject);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                _triggerCounterLimit++;
            }
        }
        
        private void PrepareForFall(GameObject playerObject)
        {
            
            _playerController = playerObject.GetComponent<PlayerController>();
            _playerController.fallIsControllable = true;
            _playerManager.SetPlayerProperties(playerObject, navmeshEnable: false, useGravity: true, isTrigger: false);
            UpdateSpecificPhysics(FallPhysicsType.Refresh, playerObject);
        }

        private void ExitFallPreparation(GameObject playerObject)
        {
            _playerController = playerObject.GetComponent<PlayerController>();
            _playerController.fallIsControllable = false;
            _playerManager.SetPlayerProperties(playerObject,navmeshEnable: true, useGravity: false, isTrigger: true);
            UpdateSpecificPhysics(FallPhysicsType.Restore, playerObject);

        }


        private void UpdateSpecificPhysics(FallPhysicsType fallPhysicsType, GameObject currentObject)
        {
            CapsuleCollider capsuleCollider = currentObject.GetComponent<CapsuleCollider>();
            Rigidbody rigidbody = currentObject.GetComponent<Rigidbody>();
            switch (fallPhysicsType)
            {
                case FallPhysicsType.Refresh:
                    capsuleCollider.radius /= 2;
                    rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                    rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                    break;
                case FallPhysicsType.Restore:
                    capsuleCollider.radius *= 2;
                    rigidbody.constraints = RigidbodyConstraints.FreezePosition;
                    rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fallPhysicsType), fallPhysicsType, null);
            }
        }
        
        
    }
}