using System;
using System.Linq;
using Player;
using UnityEngine;
using Utilities;

namespace Movement
{
    public class SwerveController : Singleton<SwerveController>
    {

         public MovementItem movementData;
         private float _swerveSpeed;
         private float _swerveAmount;
         private float _transitionSpeed;
         private float _targetSwerve;
         private bool _isSwerving;
         private Vector3 _lastMousePosition;
         private PlayerManager _playerManager;
         private bool _canSwerve = true;
        [HideInInspector] public float speed;
        [HideInInspector] public float clampLimitRight;
        [HideInInspector] public float clampLimitLeft;
        [HideInInspector] public float clamLimitOriginal;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            clamLimitOriginal = _playerManager.playerMovementPath.localScale.x / 2;
            clampLimitLeft = clamLimitOriginal;
            clampLimitRight = clamLimitOriginal;
            _swerveSpeed = movementData.swerveSpeed;
            _swerveAmount = movementData.swerveAmount;
            _transitionSpeed = movementData.transitionSpeed;
            speed = movementData.playerSpeed;
        }

        private void Update()
        {
            MoveObject();
            if (!_canSwerve) return;
            HandleInput();
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

        public void StartSwerving()
        {
            _isSwerving = true;
            _lastMousePosition = Input.mousePosition;
        }

        public void StopSwerving()
        {
            _isSwerving = false;
        }

        public void SwervingClosing()
        {
            _canSwerve = false;
            StopSwerving();
        }

        public void SwervingOpening()
        {
            _canSwerve = true;
            StartSwerving();
        }

        public void SwerveValuesRefresh()
        {
            _targetSwerve = 0;
        }
        public void ResetToOriginalValues()
        {
            speed = movementData.playerSpeed;
        }

        private void UpdateSwerve()
        {
            if (!_isSwerving) return;
            float mouseDeltaX = (Input.mousePosition.x - _lastMousePosition.x) * Time.deltaTime * _swerveSpeed;
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
                newPosition.x = Mathf.Lerp(newPosition.x, _targetSwerve * _swerveAmount, Time.deltaTime * _transitionSpeed);
                newPosition.x = Mathf.Clamp(newPosition.x, -clampLimitLeft, clampLimitRight);
                _playerManager.playersMovedObject.position = newPosition;
        }
        
        
        
        // TODO: _targetSwerve değerini, pozisyonu güncellenen _playerManager.playersMovedObject objesinin konumuna gelecek şekilde güncellenir.
        public void UpdateTargetSwerve()
        {
            float newTargetSwerve = _playerManager.playersMovedObject.transform.position.x / _swerveAmount;
            _targetSwerve = newTargetSwerve;
        }
        
        
        
        
        // TODO: Takım boyutu arttığında veya azaldığında, x yönünde sağa veya sola kaydırma değer limiti güncellenir.
        public void UpdateMovedObjectLimit()
        {
            if(_playerManager.playerList.Count <= 0) return;
            GameObject objectWithHighestXRight = _playerManager.playerList.OrderByDescending(obj => obj.transform.localPosition.x).First();
            GameObject objectWithHighestXLeft = _playerManager.playerList.OrderByDescending(obj => obj.transform.localPosition.x).Last();

            clampLimitRight = clamLimitOriginal;
            clampLimitLeft = clamLimitOriginal;
            clampLimitRight -=  Mathf.Abs(objectWithHighestXRight.transform.position.x - _playerManager.playersMovedObject.transform.position.x);
            clampLimitLeft -=  Mathf.Abs(objectWithHighestXLeft.transform.position.x - _playerManager.playersMovedObject.transform.position.x);
        }
    }
}