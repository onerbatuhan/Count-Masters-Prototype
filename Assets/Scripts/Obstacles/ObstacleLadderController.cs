using System;
using System.Linq;
using Cinemachine;
using Collectibles;
using Movement;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Obstacles
{
    public class ObstacleLadderController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        private CollectibleController _collectibleController;
        public UnityEvent pyramidMotionEvent;
        private bool _cameraTarget;
        private bool _canTrigger = true;
        private void OnTriggerEnter(Collider other)
        {
            if (!_canTrigger) return;
            if (other.gameObject.TryGetComponent(out _collectibleController))
            {
                _canTrigger = false;
                cinemachineVirtualCamera.Priority = 11;
                SwerveController.Instance.SwervingClosing();
                SwerveController.Instance.SwerveValuesRefresh();
                pyramidMotionEvent.Invoke();
                CameraTargetStart();
            }
        }

        private void CameraTargetStart()
        {
            _cameraTarget = true;
        }
        
        private void LateUpdate()
        {
            if (_cameraTarget)
            {
                
                GameObject lowestOnYAxis = PlayerManager.Instance.playerList.OrderByDescending(p => p.transform.position.y).FirstOrDefault();
                if(lowestOnYAxis ==null) return;
                cinemachineVirtualCamera.Follow = lowestOnYAxis.transform;
                
            }
            
        }
    }
}
