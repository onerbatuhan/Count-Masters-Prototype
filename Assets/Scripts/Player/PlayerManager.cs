using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
         public Transform playersMovedObject;
         public float playerCloneRadiusValue;
         public List<GameObject> playerList;
    }
}
