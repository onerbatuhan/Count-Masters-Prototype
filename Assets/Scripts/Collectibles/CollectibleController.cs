using System;
using UnityEngine;

namespace Collectibles
{
    public class CollectibleController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
           
            Collect(other.transform);
        }

        private void Collect(Transform currentCollectible)
        {
           CollectibleItem collectibleItem = currentCollectible.GetComponent<Collectible>().collectibleItem;
           CalculateCollectible(collectibleItem);
        }

        private void CalculateCollectible(CollectibleItem collectibleItem)
        {
            switch (collectibleItem.collectibleType)
            {
                case CollectibleItem.CollectibleType.None:
                    break;
                case CollectibleItem.CollectibleType.Positive:
                    CollectibleManager.Instance.AddAmount(collectibleItem.collectibleValue); //Şu anki listedeki human sayısı + bu değer.
                    break;
                case CollectibleItem.CollectibleType.Multiplier:
                    CollectibleManager.Instance.AddAmount(collectibleItem.collectibleValue); //Şu anki listedeki human sayısı * bu değer.
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
  }
}
