using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerStats;
        [SerializeField]
        private GameObject _playerSettings;

        [SerializeField]
        private GameObject _gamePausedMenu;
        [SerializeField]
        private GameObject _gameMainMenu;
        [SerializeField]
        private GameObject _gameOverMenu;

        

        public void ShowStartedGameUI()
		{
            _gamePausedMenu.SetActive(false);
            _gameMainMenu.SetActive(false);
            _gameOverMenu.SetActive(false);
            _playerSettings.gameObject.SetActive(false);

            _playerStats.gameObject.SetActive(true);
        }

        public void ShowGameOverUI()
        {
            _gameOverMenu.SetActive(true);
            _playerSettings.gameObject.SetActive(true);
        }

        public void ShowPauseGameUI()
		{
            _gamePausedMenu.gameObject.SetActive(true);
            _playerSettings.gameObject.SetActive(true);
        }

        public void HidePauseGameUI()
		{
            _gamePausedMenu.gameObject.SetActive(false);
            _playerSettings.gameObject.SetActive(false);
        }
    }
}