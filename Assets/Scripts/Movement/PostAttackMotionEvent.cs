using System.Linq;
using DG.Tweening;
using Player;
using UnityEngine;

namespace Movement
{
    public class PostAttackMotionEvent : MonoBehaviour
    {
        [SerializeField] private float moveDuration = .3f;
        private PlayerManager _playerManager;
        private SwerveController _swerveController;
        private PlayerAttackController _playerAttackController;
        
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
             _swerveController = SwerveController.Instance;
             _playerAttackController = _playerManager.playersMovedObject.GetComponent<PlayerAttackController>();
        }

        public void Execute() //UnityEvent
        {
            SetPlayersParent(null);

            GameObject targetPlayer = FindFarthestPlayer();
            Vector3 targetPosition = GetTargetPosition(targetPlayer);
            
            _playerManager.playersMovedObject.transform.DOMove(targetPosition, moveDuration).OnUpdate(() =>
            {
                MovePlayersToTarget(targetPosition);
            }).OnComplete(() =>
            {
                
                _playerAttackController.isTriggerStayDisabled = false;
                _swerveController.UpdateTargetSwerve();
                SetPlayersParent(_playerManager.playersMovedObject);
                _swerveController.UpdateMovedObjectLimit();
                _swerveController.ResetToOriginalValues();
                _swerveController.SwervingOpening();
            });
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

        private void SetPlayersParent(Transform parent)
        {
            foreach (GameObject player in _playerManager.playerList)
            {
                player.transform.SetParent(parent);
            }
        }
    }
}