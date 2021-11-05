using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Text _gamePausedText;
        [SerializeField]
        private Text _gameNameText;
        [SerializeField]
        private Button _startGameButton;
        [SerializeField]
        private Button _exitGameButton;
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


        private enum Bonus : int { SlowTime = 1, GrowUp = 2, SpeedUp = 3 };

        private void Update()
        {
            ShowScore();
            SpawnApple();
            SpawnBonus();
            VisualControl();
        }

        public void StartGame()
        {
            _gameStarted = true;
            _gameNameText.gameObject.SetActive(false);
            _gamePausedText.gameObject.SetActive(false);
            _exitGameButton.gameObject.SetActive(false);
            _startGameButton.gameObject.SetActive(false);
            
            _score = 0;
            _scoreText.gameObject.SetActive(true);
            
            Instantiate(_snake);

            _timeToCreateBonus = Random.Range(5f, 10f);
            SpawnApple();
        }

        public void Restart()
        {
            ClearAll();
            StartGame();
            ContinueGame();
            StartCoroutine(ClearScore());
        }

        private IEnumerator ClearScore()
        {
            yield return new WaitForSeconds(0.0f);
            _score = 0;
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
                    Vector2 pos = new Vector2(Random.Range(-9f, 9f), Random.Range(-4f, 4f));
                    Instantiate(_apple, pos, Quaternion.identity);
                }
            }
        }

        private void SpawnBonus()
        {
            if (_gameStarted && !_paused)
            {
                if (BonusDontFinded())
                {
                    if (_timeToCreateBonus >= 0)
                    {
                        _timeToCreateBonus -= Time.deltaTime / Time.timeScale;
                    }
                    else
                    {
                        Vector2 pos = new Vector2(Random.Range(-8.7f, 9f), Random.Range(-3.5f, 4f));

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

        private void VisualControl()
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
            _paused = true;
            Debug.Log("Paused");
            _gameSpeed = Time.timeScale;
            _gamePausedText.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        private void ContinueGame()
        {
            _paused = false;
            Time.timeScale = _gameSpeed;
            _gamePausedText.gameObject.SetActive(false);
        }

        private void ClearAll()
        {
            Destroy(GameObject.Find("Snake(Clone)"));
            Destroy(GameObject.FindGameObjectWithTag("Apple"));
            Destroy(GameObject.FindGameObjectWithTag("Bonus_GrowUp"));
            Destroy(GameObject.FindGameObjectWithTag("Bonus_SpeedUp"));
            Destroy(GameObject.FindGameObjectWithTag("Bonus_SlowTime"));
        }
    }
}