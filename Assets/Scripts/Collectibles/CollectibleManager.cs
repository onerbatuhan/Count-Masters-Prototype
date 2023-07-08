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
        private PlayerManager _playerManager;
        private SwerveController _swerveController;
        [SerializeField] private PlayerSetup playerSetup;
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _objectPool = ObjectPool.Instance;
            _swerveController = SwerveController.Instance;
        }

        public void AddAmount(int currentValue)
        {
            for (int i = 0; i < currentValue; i++)
            {
                playerSetup.PlayerConfigure(_objectPool.GetPooledObject(ObjectTypes.Type.Character));
                
            }

            _playerManager.UpdatePlayerCounter();
            _swerveController.Invoke("UpdateMovedObjectLimit",.1f);
        }
    }
}
