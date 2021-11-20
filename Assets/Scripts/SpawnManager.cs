using Bonuses;
using Foods;
using Snake;
using UnityEngine;

namespace Managers
{
	public class SpawnManager : MonoBehaviour
	{
		[SerializeField]
		private GameManager _gameManager;

		[SerializeField]
		private GameObject _applePrefab;
		private GameObject _appleObject;

		[SerializeField]
		private SnakeHead _snake;
		private SnakeHead _spawnedPlayer;

		[SerializeField]
		private SliderValueManager _snakeSpeedSlider;

		[SerializeField]
		private GameObject _slowTimePrefab;
		[SerializeField]
		private GameObject _growUpPrefab;
		[SerializeField]
		private GameObject _speedUpPrefab;

		private GameObject _bonusObject;

		[SerializeField]
		private SliderValueManager _delayBetweenBonusSlider;
		private float _delayBetweenBonuses;

		[SerializeField]
		private SliderValueManager _bonusScorePointsSlider;

		private float _timeToCreateBonus;

		private bool _startCreatingBonus = false;

		private Bonus _bonusType;

		[SerializeField]
		private float _minPosY;
		[SerializeField]
		private float _minPosX;
		[SerializeField]
		private float _maxPosY;
		[SerializeField]
		private float _maxPosX;

		private void Start()
		{

		}

		private void Update()
		{
			SpawnBonus();
		}

		public void SetEvents()
		{
			_gameManager.ClearEvent += ClearAll;
			_gameManager.SpawnEvent += StartGame;
		}

		public void ClearEvents()
		{
			_gameManager.ClearEvent -= ClearAll;
			_gameManager.SpawnEvent -= StartGame;
		}

		private void ClearAll()
		{
			Destroy(_appleObject);
			Destroy(_bonusObject);
			Destroy(_spawnedPlayer.gameObject);
		}

		private void StartGame()
		{
			SetDelayBetweenBonuses();
			SpawnPlayer();
			SpawnApple();
		}

		private void SpawnPlayer()
		{
			_spawnedPlayer = Instantiate(_snake) as SnakeHead;
			_spawnedPlayer.SetGameManager(_gameManager);
			_spawnedPlayer.SetSpeed(_snakeSpeedSlider.GetValue());
		}

		private void SetDelayBetweenBonuses()
		{
			_delayBetweenBonuses = _delayBetweenBonusSlider.GetValue();
			_timeToCreateBonus = _delayBetweenBonuses;
		}

		private void SpawnApple()
		{
			if (_gameManager.IsGameActive())
			{
				Vector2 pos = new Vector2(Random.Range(-9f, 9f), Random.Range(-3f, 4f));
				_appleObject = Instantiate(_applePrefab, pos, Quaternion.identity);
				_appleObject.GetComponent<Food>().SetPositions(_minPosX, _minPosY, _maxPosX, _maxPosY);
				_appleObject.GetComponent<Food>().SpawnEvent += SpawnApple;
				_appleObject.GetComponent<Food>().SetComponents(_gameManager, _spawnedPlayer.GetTail());
			}
		}

		private void SpawnBonus()
		{
			if (_gameManager.IsGameActive())
			{
				if (BonusDontFinded())
				{
					if (_timeToCreateBonus >= 0)
					{
						_timeToCreateBonus -= Time.deltaTime / Time.timeScale;
					}
					else
					{
						Vector2 pos = new Vector2(Random.Range(_minPosX, _maxPosX), Random.Range(_minPosY, _maxPosY));
						System.Random rnd = new System.Random();

						_bonusType = (Bonus)Random.Range(1, 4);

						switch (_bonusType)
						{
							case Bonus.GrowUp:
								{
									_bonusObject = Instantiate(_growUpPrefab, pos, Quaternion.identity);
									break;
								}
							case Bonus.SlowTime:
								{
									_bonusObject = Instantiate(_slowTimePrefab, pos, Quaternion.identity);
									break;
								}
							case Bonus.SpeedUp:
								{
									_bonusObject = Instantiate(_speedUpPrefab, pos, Quaternion.identity);
									break;
								}
						}
						_bonusObject.GetComponent<Food>().SpawnEvent += SpawnBonus;
						_bonusObject.GetComponent<Food>().SetBonusScore(_bonusScorePointsSlider.GetValue());
						_bonusObject.GetComponent<Food>().SetComponents(_gameManager, _spawnedPlayer.GetTail());
						_startCreatingBonus = false;
					}
				}
			}
		}

		private bool BonusDontFinded()
		{
			if (_bonusObject == null)
			{
				if (!_startCreatingBonus)
				{
					SetDelayBetweenBonuses();
					_startCreatingBonus = true;
				}
				return true;
			}
			return false;
		}
	}
}