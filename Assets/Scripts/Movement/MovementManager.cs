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
                GameObject farthestObject = _playerManager.playerList.OrderByDescending(obj => obj.transform.position.z).FirstOrDefault();
                _playerManager.playersMovedObject.transform.DOMove(new Vector3(farthestObject.transform.position.x,_playerManager.playersMovedObject.position.y,farthestObject.transform.position.z), .2f).OnComplete((
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
