using System.Collections.Generic;
using System.Linq;
using Movement;
using TMPro;
using UnityEngine;
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
         public void UpdatePlayerCounter()
         {
             playerCounterText.text = playerList.Count.ToString();
         }


    }
}
