using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Player;
using UnityEngine;
using Utilities;

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
            Collectible collectible;
            if (other.gameObject.TryGetComponent(out collectible))
            {
                Collect(other.transform);
            }
           
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
                    _collectibleManager.AddAmount(collectibleItem.collectibleValue); 
                    break;
                case CollectibleItem.CollectibleType.Multiplier:
                    _collectibleManager.AddAmount(collectibleItem.collectibleValue * PlayerManager.Instance.playerList.Count);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
  }
}
