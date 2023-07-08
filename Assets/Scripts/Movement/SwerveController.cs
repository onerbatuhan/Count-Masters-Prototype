using System;
using System.Linq;
using Player;
using UnityEngine;
using Utilities;

namespace Movement
{
    public class SwerveController : Singleton<SwerveController>
    {
       
        [SerializeField] private float swerveSpeed;
        [SerializeField] private float swerveAmount;
        [SerializeField] private float transitionSpeed;
        public float speed;
        public float clampLimitRight;
        public float clampLimitLeft;
        public float clamLimitOriginal;
        
        private float _targetSwerve;
        private bool _isSwerving;
        private Vector3 _lastMousePosition;
        private PlayerManager _playerManager;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            clamLimitOriginal = _playerManager.playerMovementPath.localScale.x / 2;
            clampLimitLeft = clamLimitOriginal;
            clampLimitRight = clamLimitOriginal;
        }

        private void Update()
        {
            HandleInput();
            MoveObject();
            UpdateSwerve();
        }

        
        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartSwerving();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopSwerving();
            }
        }

        private void StartSwerving()
        {
            _isSwerving = true;
            _lastMousePosition = Input.mousePosition;
        }

        private void StopSwerving()
        {
            _isSwerving = false;
        }

        private void UpdateSwerve()
        {
            if (!_isSwerving) return;
            float mouseDeltaX = (Input.mousePosition.x - _lastMousePosition.x) * Time.deltaTime * swerveSpeed;
            float objectPosX = _playerManager.playersMovedObject.position.x;
            if (objectPosX < clampLimitRight && objectPosX > -clampLimitLeft || (objectPosX == clampLimitRight && mouseDeltaX < 0) || (objectPosX == -clampLimitLeft && mouseDeltaX > 0))
            {
                float targetSwerveClampLimitRight = (_playerManager.playerMovementPath.localScale.x + clampLimitRight) / 100;
                float targetSwerveClampLimitLeft = (_playerManager.playerMovementPath.localScale.x + clampLimitLeft) / 100;
                _targetSwerve = Mathf.Clamp(_targetSwerve, -targetSwerveClampLimitLeft,  targetSwerveClampLimitRight);
                _targetSwerve += mouseDeltaX / Screen.width;
            }
            

            _lastMousePosition = Input.mousePosition;
        }

        private void MoveObject()
        {
            _playerManager.playersMovedObject.Translate(Vector3.forward * speed * Time.deltaTime);
            Vector3 newPosition = _playerManager.playersMovedObject.position;
            newPosition.x = Mathf.Lerp(newPosition.x, _targetSwerve * swerveAmount, Time.deltaTime * transitionSpeed);
            newPosition.x = Mathf.Clamp(newPosition.x, -clampLimitLeft, clampLimitRight);
            _playerManager.playersMovedObject.position = newPosition;
        }
        
        // TODO: Takım boyutu arttığında veya azaldığında, x yönünde sağa veya sola kaydırma değer limiti güncellenir.
        public void UpdateMovedObjectLimit()
        {
            GameObject objectWithHighestXRight = _playerManager.playerList.OrderByDescending(obj => obj.transform.localPosition.x).First();
            GameObject objectWithHighestXLeft = _playerManager.playerList.OrderByDescending(obj => obj.transform.localPosition.x).Last();

            clampLimitRight = clamLimitOriginal;
            clampLimitLeft = clamLimitOriginal;
            clampLimitRight -=  Mathf.Abs(objectWithHighestXRight.transform.position.x - _playerManager.playersMovedObject.transform.position.x);
            clampLimitLeft -=  Mathf.Abs(objectWithHighestXLeft.transform.position.x - _playerManager.playersMovedObject.transform.position.x);
        }
    }
}