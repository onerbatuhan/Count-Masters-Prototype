using System.Collections.Generic;
using System.Linq;
using Movement;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
         public Transform playersMovedObject;
         public Transform playerMovementPath;
         public float playerCloneRadiusValue;
         public List<GameObject> playerList;
         public TextMeshPro playerCounterText;



         public void RemovePlayer(GameObject currentPlayerObject)
         {
             playerList.Remove(currentPlayerObject);
             ObjectPool.Instance.ReturnToPool(currentPlayerObject);
             SetPlayerProperties(currentPlayerObject,true,false,true);
             UpdatePlayerCounter();
         }

         public void AddPlayer(GameObject currentPlayerObject)
         {
             playerList.Add(currentPlayerObject);
             UpdatePlayerCounter();
         }
         
         private void UpdatePlayerCounter()
         {
             playerCounterText.text = playerList.Count.ToString();
         }

         public void SetPlayerProperties(GameObject playerObject,bool navmeshEnable, bool useGravity, bool isTrigger)
         {
             var rigidBody = playerObject.GetComponent<Rigidbody>();
             var capsuleCollider = playerObject.GetComponent<CapsuleCollider>();
             var navMeshAgent = playerObject.GetComponent<NavMeshAgent>();

             navMeshAgent.enabled = navmeshEnable;
             rigidBody.useGravity = useGravity;
             capsuleCollider.isTrigger = isTrigger;
         }



    }
}
