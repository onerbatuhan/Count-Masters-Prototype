using System;
using System.Collections.Generic;
using System.Linq;
using Level;
using Movement;
using Obstacles;
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
         private SwerveController _swerveController;
         private void Start()
         {
             _swerveController = SwerveController.Instance;
         }


         public void RemovePlayer(GameObject currentPlayerObject)
         {
             playerList.Remove(currentPlayerObject);
             ObjectPool.Instance.ReturnToPool(currentPlayerObject);
             SetPlayerProperties(currentPlayerObject,true,false,false);
             UpdatePlayerCounter();
             _swerveController.UpdateMovedObjectLimit();
         }

         public void RemovePlayerList(GameObject currentPlayerObject)
         {
             playerList.Remove(currentPlayerObject);
         }

         public void AddPlayer(GameObject currentPlayerObject)
         {
             playerList.Add(currentPlayerObject);
             UpdatePlayerCounter();
         }
         
         private void UpdatePlayerCounter()
         {
             if (playerList.Count == 0)
             {
                 LevelManager.Instance.LevelFail();
             }
             playerCounterText.text = playerList.Count.ToString();
         }

         public void SetPlayerProperties(GameObject playerObject,bool navmeshEnable, bool useGravity, bool isTrigger)
         {
             Rigidbody rigidBody = playerObject.GetComponent<Rigidbody>();
             CapsuleCollider capsuleCollider = playerObject.GetComponent<CapsuleCollider>();
             NavMeshAgent navMeshAgent = playerObject.GetComponent<NavMeshAgent>();
             navMeshAgent.enabled = navmeshEnable;
             rigidBody.useGravity = useGravity;
             capsuleCollider.isTrigger = isTrigger;

         }



    }
}
