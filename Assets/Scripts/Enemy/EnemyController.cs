using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro enemyCounterText;
        private EnemyManager _enemyManager;
        private bool _hasEnteredUpdate = false;
        public int enemyCount;
        public List<GameObject> enemyGroupList;
        private void Start()
        {
            _enemyManager = EnemyManager.Instance;
            UpdateEnemyCounter();
        }

        
       
        private void UpdateEnemyCounter()
        {
            enemyCounterText.text = enemyGroupList.Count.ToString();
        }

        public void AddEnemy(GameObject currentEnemyObject)
        {
            enemyGroupList.Add(currentEnemyObject);
            UpdateEnemyCounter();
        }

        public void RemoveEnemy(GameObject currentEnemyObject)
        {
            enemyGroupList.Remove(currentEnemyObject);
        }
        private void LateUpdate()
        {
            if (_hasEnteredUpdate) return;
            CheckVisibilityInCameraView();
            
        }


        private void CheckVisibilityInCameraView()
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            if (screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0)
            {
                _hasEnteredUpdate = true;
                _enemyManager.EnemyGroupSpawn(enemyCount,transform);
            }
        }
        
        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Handles.color = Color.green;
            Handles.DrawSolidDisc(transform.position, transform.up, 2f);
#endif
        }
    }
    
    
    
    
    
    
}
