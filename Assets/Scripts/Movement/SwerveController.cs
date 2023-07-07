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
        public float speed;
        public float clampLimitRight;
        public float clampLimitLeft;
        public float clamLimitOriginal;
        private Transform _movedObject;
        private float _targetSwerve;
        private bool _isSwerving;
        private Vector3 _lastMousePosition;

        private void Start()
        {
            _movedObject = PlayerManager.Instance.playersMovedObject;
            clamLimitOriginal = platformRoadObject.localScale.x / 2;
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
            float objectPosX = _movedObject.position.x;
            if (objectPosX < clampLimitRight && objectPosX > -clampLimitLeft || (objectPosX == clampLimitRight && mouseDeltaX < 0) || (objectPosX == -clampLimitLeft && mouseDeltaX > 0))
            {
                float targetSwerveClampLimitRight = (platformRoadObject.localScale.x + clampLimitRight) / 100;
                float targetSwerveClampLimitLeft = (platformRoadObject.localScale.x + clampLimitLeft) / 100;
                _targetSwerve = Mathf.Clamp(_targetSwerve, -targetSwerveClampLimitLeft,  targetSwerveClampLimitRight);
                _targetSwerve += mouseDeltaX / Screen.width;
            }
            

            _lastMousePosition = Input.mousePosition;
        }

        private void MoveObject()
        {
            _movedObject.Translate(Vector3.forward * speed * Time.deltaTime);
            Vector3 newPosition = _movedObject.position;
            newPosition.x = Mathf.Lerp(newPosition.x, _targetSwerve * swerveAmount, Time.deltaTime * transitionSpeed);
            newPosition.x = Mathf.Clamp(newPosition.x, -clampLimitLeft, clampLimitRight);
            _movedObject.position = newPosition;
        }
        
        // TODO: Takım boyutu arttığında veya azaldığında, x yönünde sağa veya sola kaydırma değer limiti güncellenir.
        public void UpdateMovedObjectLimit()
        {
            GameObject objectWithHighestXRight = PlayerManager.Instance.playerList.OrderByDescending(obj => obj.transform.localPosition.x).First();
            GameObject objectWithHighestXLeft = PlayerManager.Instance.playerList.OrderByDescending(obj => obj.transform.localPosition.x).Last();

            clampLimitRight = clamLimitOriginal;
            clampLimitLeft = clamLimitOriginal;
            clampLimitRight -=  Mathf.Abs(objectWithHighestXRight.transform.position.x - _movedObject.transform.position.x);
            clampLimitLeft -=  Mathf.Abs(objectWithHighestXLeft.transform.position.x - _movedObject.transform.position.x);
        }
    }
}