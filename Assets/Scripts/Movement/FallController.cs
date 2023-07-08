using System;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Movement
{
    public class FallController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            ObjectTypes objectTypes;
            if (other.gameObject.TryGetComponent(out objectTypes) && objectTypes.objectType == ObjectTypes.Type.Character)
            {
                PrepareForFall(other.gameObject);
            }
        }

        private void PrepareForFall(GameObject playerObject)
        {
            Rigidbody rigidBody = playerObject.GetComponent<Rigidbody>();
            CapsuleCollider capsuleCollider = playerObject.GetComponent<CapsuleCollider>();
            NavMeshAgent navMeshAgent = playerObject.GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;
            rigidBody.useGravity = true;
            capsuleCollider.isTrigger = false;
            
        }
    }
}
