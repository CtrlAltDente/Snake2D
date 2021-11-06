using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        private float _seconds;
        private float _minutes;
        private float _hours;

        [SerializeField]
        private GameObject _time;
        [SerializeField]
        private Text _secondsText;
        [SerializeField]
        private Text _minutesText;
        [SerializeField]
        private Text _hoursText;

        [SerializeField]
        private GameObject _tailLenght;
        [SerializeField]
        private GameObject _gamePausedMenu;
        [SerializeField]
        private GameObject _gameMainMenu;
        [SerializeField]
        private GameObject _gameOverMenu;
        [SerializeField]
        private Text _scoreText;

        [SerializeField]
        private int _score = 0;

        [SerializeField]
        private GameObject _snake;

        [SerializeField]
        private GameObject _apple;

        [SerializeField]
        private GameObject _slowTime;
        [SerializeField]
        private GameObject _growUp;
        [SerializeField]
        private GameObject _speedUp;

        [SerializeField]
        private float _minTimeToCreateBonus;
        [SerializeField]
        private float _maxTimeToCreateBonus;

        [SerializeField]
        private float _timeToCreateBonus;

        private bool _startCreatingBonus = false;

        private float _gameSpeed;
        
        private bool _gameStarted = false;

        private bool _paused = false;

        private bool _gameOver = false;

        private enum Bonus : int { SlowTime = 1, GrowUp = 2, SpeedUp = 3 };

        private void Update()
        {
            ShowScore();
            SpawnApple();
            SpawnBonus();
            GameControl();
            CalculateTime();
        }

        public void StartGame()
        {
            _gameStarted = true;
            _gameOver = false;

            _gamePausedMenu.SetActive(false);
            _gameMainMenu.SetActive(false);
            _gameOverMenu.SetActive(false);

            _tailLenght.SetActive(true);
            _time.gameObject.SetActive(true);
            _seconds = 0;
            _minutes = 0;
            _hours = 0;
            
            _score = 0;
            _scoreText.gameObject.SetActive(true);
            
            Instantiate(_snake);

            Time.timeScale = 1;

            _timeToCreateBonus = Random.Range(5f, 10f);
            SpawnApple();
        }

        public void Restart()
        {
            ClearAll();
            StartGame();
        }

        public void GameOver()
		{
            Time.timeScale = 0;
            _gameOverMenu.SetActive(true);
            _gameOver = true;
		}

        public void ExitGame()
        {
            Application.Quit();
        }

        public void AddScore(int value)
        {
            _score += value;
        }

        private void ShowScore()
        {
            _scoreText.text = "Score: " + _score.ToString();
        }

        private void SpawnApple()
        {
            if (_gameStarted)
            {
                if (!GameObject.FindGameObjectWithTag("Apple"))
                {
                    Vector2 pos = new Vector2(Random.Range(-9f, 9f), Random.Range(-3f, 4f));
                    Instantiate(_apple, pos, Quaternion.identity);
                }
            }
        }

        private void SpawnBonus()
        {
            if (_gameStarted && !_paused && !_gameOver)
            {
                if (BonusDontFinded())
                {
                    if (_timeToCreateBonus >= 0)
                    {
                        _timeToCreateBonus -= Time.deltaTime / Time.timeScale;
                    }
                    else
                    {
                        Vector2 pos = new Vector2(Random.Range(-9f, 9f), Random.Range(-3f, 4f));
                        System.Random rnd = new System.Random();
                        Bonus bonus = (Bonus)Random.Range(1, 4);

                        switch (bonus)
                        {
                            case Bonus.GrowUp:
                                {
                                    Instantiate(_growUp, pos, Quaternion.identity);
                                    break;
                                }
                            case Bonus.SlowTime:
                                {
                                    Instantiate(_slowTime, pos, Quaternion.identity);
                                    break;
                                }
                            case Bonus.SpeedUp:
                                {
                                    Instantiate(_speedUp, pos, Quaternion.identity);
                                    break;
                                }
                        }
                        _startCreatingBonus = false;
                    }
                }
            }
        }

        private bool BonusDontFinded()
        {
            if (!GameObject.FindGameObjectWithTag("Bonus_SlowTime") & !GameObject.FindGameObjectWithTag("Bonus_SpeedUp") & !GameObject.FindGameObjectWithTag("Bonus_GrowUp"))
            {
                if (!_startCreatingBonus)
                {
                    _timeToCreateBonus = Random.Range(_minTimeToCreateBonus, _maxTimeToCreateBonus);
                    _startCreatingBonus = true;
                }
                return true;
            }
            return false;
        }

        private void GameControl()
        {
            if (_gameStarted)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (!_paused)
                    {
                        PauseGame();
                    }
                    else
                    {
                        ContinueGame();
                    }
                }
            }
        }

        private void PauseGame()
        {
            if (!_gameOver)
            {
                _paused = true;
                _gameSpeed = Time.timeScale;
                _gamePausedMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }

        private void ContinueGame()
        {
            _paused = false;
            Time.timeScale = _gameSpeed;
            _gamePausedMenu.gameObject.SetActive(false);
        }

        private void ClearAll()
        {
            Destroy(GameObject.Find("Snake(Clone)"));
            Destroy(GameObject.FindGameObjectWithTag("Apple"));
            Destroy(GameObject.FindGameObjectWithTag("Bonus_GrowUp"));
            Destroy(GameObject.FindGameObjectWithTag("Bonus_SpeedUp"));
            Destroy(GameObject.FindGameObjectWithTag("Bonus_SlowTime"));
        }

        private void CalculateTime()
        {
            if (Time.timeScale != 0)
            {
                _seconds += (Time.deltaTime / Time.timeScale);

                if (_seconds > 59)
                {
                    _seconds = 0;
                    _minutes += 1;
                }

                if (_minutes >= 60)
                {
                    _minutes = 0;
                    _hours += 1;
                }

                _secondsText.text = _seconds.ToString("0");
                _minutesText.text = _minutes.ToString("0") + ":";
                _hoursText.text = _hours.ToString("0") + ":";
            }
        }
    }
}