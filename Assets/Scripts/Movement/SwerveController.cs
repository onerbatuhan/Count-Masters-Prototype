using System;
using UnityEngine;

namespace Movement
{
    public class SwerveController : MonoBehaviour
    {
        [SerializeField] private Transform movedObject;
        [SerializeField] private Transform platformRoadObject;
        [SerializeField] private float swerveSpeed;
        [SerializeField] private float swerveAmount;
        [SerializeField] private float transitionSpeed;
        public float clampLimit;
        public float speed;
        private float _targetSwerve;
        private bool _isSwerving;
        private Vector3 _lastMousePosition;

        private void Start()
        {
            clampLimit = platformRoadObject.localScale.x / 2;
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
            float objectPosX = movedObject.position.x;
            if (objectPosX < clampLimit && objectPosX > -clampLimit || (objectPosX == clampLimit && mouseDeltaX < 0) || (objectPosX == -clampLimit && mouseDeltaX > 0))
            {
                float targetSwerveClampLimit = (platformRoadObject.localScale.x + clampLimit) / 100;
                Debug.Log(targetSwerveClampLimit);
                _targetSwerve = Mathf.Clamp(_targetSwerve, -targetSwerveClampLimit,  targetSwerveClampLimit);
                _targetSwerve += mouseDeltaX / Screen.width;
            }
            

            _lastMousePosition = Input.mousePosition;
        }

        private void MoveObject()
        {
            movedObject.Translate(Vector3.forward * speed * Time.deltaTime);
            Vector3 newPosition = movedObject.position;
            newPosition.x = Mathf.Lerp(newPosition.x, _targetSwerve * swerveAmount, Time.deltaTime * transitionSpeed);
            newPosition.x = Mathf.Clamp(newPosition.x, -clampLimit, clampLimit);
            movedObject.position = newPosition;
        }
    }
}