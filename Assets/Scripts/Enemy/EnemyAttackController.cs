
using System.Linq;
using Animation;
using Attack;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttackController : MonoBehaviour,IAttackController
    {
        private PlayerAttackController _playerAttackController;
        private EnemyController _enemyController;
        private AnimationController _animationController;
        private PlayerManager _playerManager;
        private bool _isTriggerStayDisabled;

        private void Start()
        {
            _enemyController = transform.GetComponent<EnemyController>();
            _animationController = AnimationController.Instance;
            _playerManager = PlayerManager.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out _playerAttackController)) return;
            StartAttack();
        }

        private void OnTriggerStay(Collider other)
        {
            if(_isTriggerStayDisabled) return;
            CheckAttackCollision(other.gameObject);
            CheckAttackFinished(other.gameObject);
            
        }

        public void CheckAttackCollision(GameObject collidingObject)
        {
            if (!collidingObject.gameObject.TryGetComponent(out _playerAttackController)) return;
            
            TargetAttack(collidingObject.transform);
        }

        public void StartAttack()
        {
            
            foreach (var enemy in _enemyController.enemyGroupList)
            {
                 Animator animator = enemy.GetComponent<Animator>();
                _animationController.ChangeAnimation(AnimationController.AnimationType.Run,animator);
            }
        }

        public void CheckAttackFinished(GameObject collidingObject)
        {
            if (!collidingObject.gameObject.TryGetComponent(out _playerAttackController)) return;
            if (_playerManager.playerList.Count != 0) return;
            _isTriggerStayDisabled = true;
            FinishAttack();
            

        }

        public void FinishAttack()
        {
            
            foreach (var enemy in _enemyController.enemyGroupList)
            {
                Animator animator = enemy.GetComponent<Animator>();
                _animationController.ChangeAnimation(AnimationController.AnimationType.Idle,animator);
            }
        }

        public void TargetAttack(Transform targetTransform)
        {
            GameObject targetObject = _playerManager.playerList.OrderByDescending(obj => obj.transform.position.z).FirstOrDefault();
            foreach (var player in _enemyController.enemyGroupList.Where(player => targetObject != null))
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, targetObject.transform.position, 1*Time.deltaTime);
            }
        }
        
    }
}
