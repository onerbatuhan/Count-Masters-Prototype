using UnityEngine;

namespace Collectibles
{
    [CreateAssetMenu(fileName = "NewCollectibleItem", menuName = "Data/Collectible/New Collectible Item")]
    public class CollectibleItem : ScriptableObject
    {
        public enum CollectibleType
        {
            None,
            Positive,
            Multiplier
            
        }

        public CollectibleType collectibleType;
        public int collectibleValue;
    }
    
    }
