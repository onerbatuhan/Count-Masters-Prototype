using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Collectibles
{
    public class Collectible : MonoBehaviour
    {
         public CollectibleItem collectibleItem;
         public CollectibleGroupController collectibleGroupController;
         private CollectibleController _collectibleController;

         private void Awake()
         {
             collectibleGroupController = transform.parent.GetComponent<CollectibleGroupController>();
         }

         private void OnTriggerEnter(Collider other)
         {
            
             if (other.gameObject.TryGetComponent(out _collectibleController) && collectibleGroupController.canCollected)
             {
                 foreach (Transform child in transform)
                 {
                     child.gameObject.SetActive(false);
                 }
             }
             
             
         }

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
