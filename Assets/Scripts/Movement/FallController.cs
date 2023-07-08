using System;
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
        private void OnTriggerEnter(Collider other)
        {
            ObjectTypes objectTypes;
            if (!other.gameObject.TryGetComponent(out objectTypes) || objectTypes.objectType != ObjectTypes.Type.Character) return;
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
            SetCharacterProperties(playerObject, useGravity: true, isTrigger: false);
        }

        private void ExitFallPreparation(GameObject playerObject)
        {
            SetCharacterProperties(playerObject, useGravity: false, isTrigger: true);
        }

        private void SetCharacterProperties(GameObject playerObject, bool useGravity, bool isTrigger)
        {
            var rigidBody = playerObject.GetComponent<Rigidbody>();
            var capsuleCollider = playerObject.GetComponent<CapsuleCollider>();
            var navMeshAgent = playerObject.GetComponent<NavMeshAgent>();

            navMeshAgent.enabled = !useGravity;
            rigidBody.useGravity = useGravity;
            capsuleCollider.isTrigger = isTrigger;
        }

        private void DetectFallingObjects()
        {
            // float threshold = referenceHeight * 2f;
            //
            // if (transform.position.y < threshold)
            // {
            //     Debug.Log("Y Position: " + transform.position.y);
            // }
        }
    }
}