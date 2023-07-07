using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerSetup : MonoBehaviour
    {
        private PlayerManager _playerManager;
        [SerializeField] private float radius = 1f;
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }

        public void PlayerConfigure(GameObject currentPlayerObject)
        {
            currentPlayerObject.transform.SetParent(_playerManager.playersMovedObject);
            currentPlayerObject.transform.position = _playerManager.playersMovedObject.position;
        }
    }
}
