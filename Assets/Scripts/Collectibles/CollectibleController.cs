using System;
using UnityEngine;

namespace Collectibles
{
    public class CollectibleController : MonoBehaviour
    {
        private CollectibleManager _collectibleManager;
        private void Start()
        {
            _collectibleManager = CollectibleManager.Instance;
        }

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
                    _collectibleManager.AddAmount(collectibleItem.collectibleValue); //Şu anki listedeki human sayısı + bu değer.
                    break;
                case CollectibleItem.CollectibleType.Multiplier:
                    _collectibleManager.AddAmount(collectibleItem.collectibleValue); //Şu anki listedeki human sayısı * bu değer.
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
  }
}
