using System;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        private ObjectPool _objectPool;
        [SerializeField] private float enemyCloneRadiusValue;
        private void Start()
        {
            _objectPool = ObjectPool.Instance;
        }
        
        public void EnemyGroupSpawn(int enemyCount, Transform enemyGroupTransform)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                EnemySetTransform(_objectPool.GetPooledObject(ObjectTypes.Type.Enemy), enemyGroupTransform);
            }
           
            
        }
        
        private void EnemySetTransform(GameObject currentEnemyObject,Transform enemyGroupTransform)
        {
            EnemyController  enemyController = enemyGroupTransform.GetComponent<EnemyController>();
            enemyController.AddEnemy(currentEnemyObject);
            
            currentEnemyObject.transform.SetParent(enemyGroupTransform);
            
            Vector3 randomPosition = Random.insideUnitSphere*enemyCloneRadiusValue;
            Vector3 newPosition = enemyGroupTransform.position + randomPosition;
            currentEnemyObject.transform.position = newPosition;
        }
        
    }
}
