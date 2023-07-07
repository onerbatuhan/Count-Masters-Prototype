using System;
using System.Linq;
using Player;
using UnityEngine;
using Utilities;

namespace Movement
{
    public class SwerveController : Singleton<SwerveController>
    {
        [SerializeField] private Transform platformRoadObject;
        [SerializeField] private float swerveSpeed;
        [SerializeField] private float swerveAmount;
        [SerializeField] private float transitionSpeed;
        public float clampLimitPositive;
        public float clampLimitNegative;
        public float speed;
        private Transform _movedObject;
        private float _targetSwerve;
        private bool _isSwerving;
        private Vector3 _lastMousePosition;

        private void Start()
        {
            _movedObject = PlayerManager.Instance.playersMovedObject;
            clampLimitPositive = platformRoadObject.localScale.x / 2;
            clampLimitNegative = clampLimitPositive;
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
            float objectPosX = _movedObject.position.x;
            if (objectPosX < clampLimitPositive && objectPosX > -clampLimitNegative || (objectPosX == clampLimitPositive && mouseDeltaX < 0) || (objectPosX == -clampLimitNegative && mouseDeltaX > 0))
            {
                float targetSwerveClampLimitPositive = (platformRoadObject.localScale.x + clampLimitPositive) / 100;
                float targetSwerveClampLimitNegative = (platformRoadObject.localScale.x + clampLimitNegative) / 100;
                _targetSwerve = Mathf.Clamp(_targetSwerve, -targetSwerveClampLimitNegative,  targetSwerveClampLimitPositive);
                _targetSwerve += mouseDeltaX / Screen.width;
            }
            

            _lastMousePosition = Input.mousePosition;
        }

        private void MoveObject()
        {
            _movedObject.Translate(Vector3.forward * speed * Time.deltaTime);
            Vector3 newPosition = _movedObject.position;
            newPosition.x = Mathf.Lerp(newPosition.x, _targetSwerve * swerveAmount, Time.deltaTime * transitionSpeed);
            newPosition.x = Mathf.Clamp(newPosition.x, -clampLimitNegative, clampLimitPositive);
            _movedObject.position = newPosition;
        }
        
        public void UpdateMovedObjectLimit()
        {
            GameObject objectWithHighestXRight = PlayerManager.Instance.playerList.OrderByDescending(obj => obj.transform.localPosition.x).First();
            GameObject objectWithHighestXLeft = PlayerManager.Instance.playerList.OrderByDescending(obj => obj.transform.localPosition.x).Last();
            clampLimitPositive -=  Mathf.Abs(objectWithHighestXRight.transform.position.x - _movedObject.transform.position.x);
            clampLimitNegative -=  Mathf.Abs(objectWithHighestXLeft.transform.position.x - _movedObject.transform.position.x);
        }
    }
}