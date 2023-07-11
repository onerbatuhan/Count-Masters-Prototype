using System.Linq;
using DG.Tweening;
using Player;
using UnityEngine;
using Utilities;

namespace Movement
{
    public class MovementManager : Singleton<MovementManager>
    {
        
        private PlayerManager _playerManager;
        private SwerveController _swerveController;
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
             _swerveController = SwerveController.Instance;
        }

        public void KeepPlayersClose()
        {
            DetachPlayersFromParent();

            GameObject targetPlayer = FindFarthestPlayer();
            Vector3 targetPosition = GetTargetPosition(targetPlayer);

            float moveDuration = 0.5f;
            _playerManager.playersMovedObject.transform.DOMove(targetPosition, moveDuration).OnUpdate(() =>
            {
                MovePlayersToTarget(targetPosition);
            }).OnComplete(() =>
            {
                _swerveController.UpdateTargetSwerve();
                SetPlayersParent();
                _swerveController.UpdateMovedObjectLimit();
                _swerveController.ResetToOriginalValues();
            });
        }

        private void DetachPlayersFromParent()
        {
            foreach (GameObject player in _playerManager.playerList)
            {
                player.transform.SetParent(null);
            }
        }

        private GameObject FindFarthestPlayer()
        {
            return _playerManager.playerList.OrderByDescending(p => p.transform.position.z).FirstOrDefault();
        }

        private Vector3 GetTargetPosition(GameObject targetPlayer)
        {
            return new Vector3(targetPlayer.transform.position.x, _playerManager.playersMovedObject.position.y, targetPlayer.transform.position.z);
        }

        private void MovePlayersToTarget(Vector3 targetPosition)
        {
            foreach (GameObject player in _playerManager.playerList)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, 1f * Time.deltaTime);
            }
        }

        private void SetPlayersParent()
        {
            foreach (GameObject player in _playerManager.playerList)
            {
                player.transform.SetParent(_playerManager.playersMovedObject);
            }
        }
    }
}