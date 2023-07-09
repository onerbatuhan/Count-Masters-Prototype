using System;
using Attack;
using UnityEditor;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private AttackController _attackController;
        public int enemyCount;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out _attackController))
            {
                Debug.Log("player takım trigger");
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
