using System;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Collectibles
{
    public class Collectible : MonoBehaviour
    {
         public CollectibleItem collectibleItem;
         
       
        
         private void OnValidate()
         {
#if UNITY_EDITOR
             if (collectibleItem == null) return;
             TextMeshPro textMeshPro = GetComponentInChildren<TextMeshPro>();
             switch (collectibleItem.collectibleType)
             {
                 case CollectibleItem.CollectibleType.None:
                     break;
                 case CollectibleItem.CollectibleType.Positive:
                     textMeshPro.text = "+" + collectibleItem.collectibleValue;
                     break;
                 case CollectibleItem.CollectibleType.Multiplier:
                     textMeshPro.text = "x" + collectibleItem.collectibleValue;
                     break;
                 default:
                     throw new ArgumentOutOfRangeException();
             }
#endif
         }
    }
}
