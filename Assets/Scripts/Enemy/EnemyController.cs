using System;
using Attack;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private AttackController _attackController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out _attackController))
            {
                Debug.Log("player takım trigger");
            }
        }
    }
}
