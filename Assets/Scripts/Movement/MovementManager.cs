using System;
using System.Linq;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Movement
{
    public class MovementManager : Singleton<MovementManager>
    {
        private PlayerManager _playerManager;
      
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }

       
        public void KeepPlayersClose()
        {
           
                foreach (GameObject gameObj in _playerManager.playerList)
                {
                    gameObj.transform.SetParent(null);
                }
                 
                GameObject middleObject = _playerManager.playerList.OrderBy(obj => Mathf.Abs(obj.transform.position.x - 0f)).FirstOrDefault();
                Vector3 targetPosition = new Vector3(middleObject.transform.position.x, _playerManager.playersMovedObject.position.y, middleObject.transform.position.z);
                _playerManager.playersMovedObject.transform.DOMove(targetPosition, .5f).OnUpdate((() =>
                {
                    foreach (GameObject gameObj in _playerManager.playerList)
                    {
                        
                        gameObj.transform.position = Vector3.MoveTowards(gameObj.transform.position,
                            _playerManager.playersMovedObject.position, 1*Time.deltaTime);
                       

                    }
                    
                })).OnComplete((
                    () =>
                    {
                        foreach (GameObject gameObj in _playerManager.playerList)
                        {
                            
                            gameObj.transform.SetParent(_playerManager.playersMovedObject);
                        }
                        SwerveController.Instance.UpdateMovedObjectLimit();
                        SwerveController.Instance.ResetToOriginalValues();
                    }));
        }
        
       

        

        
    }
}
