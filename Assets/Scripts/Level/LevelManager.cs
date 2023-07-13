using System;
using Animation;
using Movement;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using Random = UnityEngine.Random;

namespace Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private GameObject levelStartUI;
        [SerializeField] private GameObject levelSuccessUI;
        [SerializeField] private GameObject levelFailUI;
        private SwerveController _swerveController;

        private void Start()
        {
            _swerveController = SwerveController.Instance;
        }

        public void LevelSuccess()
        {
            levelSuccessUI.SetActive(true);
            _swerveController.SwervingClosing();
        }

        public void LevelFail()
        {
            _swerveController.enabled = false;
            levelFailUI.SetActive(true);
        }

        public void StartLevelClickEvent()
        {
            levelStartUI.SetActive(false);
            _swerveController.enabled = true;
            AnimationController.Instance.ChangeAnimationPlayers(AnimationController.AnimationType.Run);
        }
        
        public void NextLevelClickEvent()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                int randomSceneIndex = Random.Range(0, SceneManager.sceneCountInBuildSettings);
                SceneManager.LoadScene(randomSceneIndex);
            }
        }

        public void ReturnLevelClickEvent()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }
}
