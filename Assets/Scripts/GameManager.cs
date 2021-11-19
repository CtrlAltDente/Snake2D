using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
	public class GameManager : MonoBehaviour
	{
		public delegate void ClearAndSpawnCallback();

		public event ClearAndSpawnCallback ClearEvent;
		public event ClearAndSpawnCallback SpawnEvent;

		private float _seconds;
		private float _minutes;
		private float _hours;

		[SerializeField]
		private Text _secondsText;
		[SerializeField]
		private Text _minutesText;
		[SerializeField]
		private Text _hoursText;

		[SerializeField]
		private Text _lenght;

		[SerializeField]
		private Text _scoreText;

		[SerializeField]
		private int _score = 0;

		[SerializeField]
		private UIManager _uiManager;
		[SerializeField]
		private SpawnManager _spawnManager;

		private float _gameSpeed;

		private bool _gameStarted = false;

		private bool _paused = false;

		private bool _gameOver = false;

		private void Update()
		{
			ShowScore();
			GameControl();
			CalculateTime();
		}

		public Text GetLenghtTextComponent()
		{
			return _lenght;
		}

		public void Restart()
		{
			ClearAll();
			StartGame();
		}

		private void ClearAll()
		{
			ClearEvent?.Invoke();
			_spawnManager.ClearEvents();
		}

		public void StartGame()
		{
			_gameStarted = true;
			_gameOver = false;
			_paused = false;

			_seconds = 0;
			_minutes = 0;
			_hours = 0;
			_score = 0;

			_spawnManager.SetEvents();

			SpawnEvent?.Invoke();

			_uiManager.ShowStartedGameUI();
		}

		public void GameOver()
		{
			_uiManager.ShowGameOverUI();
			Time.timeScale = 0;
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

		public bool IsGameActive()
		{
			if (_gameStarted && !_paused && !_gameOver)
			{
				return true;
			}
			return false;
		}

		public bool IsGamePaused()
		{
			if (_paused)
			{
				return true;
			}
			return false;
		}

		public bool IsGameOver()
		{
			if (_gameOver)
			{
				return true;
			}
			return false;
		}


		private void ShowScore()
		{
			_scoreText.text = "Score: " + _score.ToString();
		}

		private void GameControl()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (_gameStarted)
				{
					if (!IsGamePaused())
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
				_uiManager.ShowPauseGameUI();
				_paused = true;
				_gameSpeed = Time.timeScale;
				Time.timeScale = 0;
			}
		}

		private void ContinueGame()
		{
			_uiManager.HidePauseGameUI();
			_paused = false;
			Time.timeScale = _gameSpeed;
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