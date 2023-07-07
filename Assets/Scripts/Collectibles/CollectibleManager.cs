using UnityEngine;
using Utilities;

namespace Collectibles
{
    public class CollectibleManager : Singleton<CollectibleManager>
    {
        
        public void AddAmount(int currentValue)
        {
            for (int i = 0; i < currentValue; i++)
            {
                ObjectPool.Instance.GetPooledObject(ObjectTypes.Type.Character);
            }
        }
    }
}
