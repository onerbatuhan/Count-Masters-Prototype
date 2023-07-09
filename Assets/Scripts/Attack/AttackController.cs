using System;
using Enemy;
using UnityEngine;

namespace Attack
{
    public class AttackController : MonoBehaviour
    {
        private EnemyController _enemyController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out _enemyController))
            {
                Debug.Log("Düşman takım trigger");
            }
        }
        
    }
}
