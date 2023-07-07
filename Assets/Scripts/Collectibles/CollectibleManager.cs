using System;
using Movement;
using Player;
using UnityEngine;
using Utilities;

namespace Collectibles
{
    public class CollectibleManager : Singleton<CollectibleManager>
    {
        private ObjectPool _objectPool;
        [SerializeField] private PlayerSetup playerSetup;
        private void Start()
        {
            _objectPool = ObjectPool.Instance;
        }

        public void AddAmount(int currentValue)
        {
            for (int i = 0; i < currentValue; i++)
            {
                playerSetup.PlayerConfigure(_objectPool.GetPooledObject(ObjectTypes.Type.Character));
                
            }
            SwerveController.Instance.Invoke("UpdateMovedObjectLimit",.1f);
        }
    }
}
