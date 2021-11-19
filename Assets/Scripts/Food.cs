using Managers;
using Snake;
using UnityEngine;

namespace Foods
{
	public class Food : MonoBehaviour
	{
		public delegate void SpawnCallback();
		public event SpawnCallback SpawnEvent;

		private GameObject _snake;
		private SnakeTail _snakeTail;

		private TrailRenderer _snakeTrailComponent;

		private Vector3[] _snakeTailPointsPositions;

		private GameManager _gameManager;

		[SerializeField]
		private int _score;
		private int _bonusScore;

		[SerializeField]
		private TypeOfFood _typeOfFood;

		private float _distanceToTail = 0.5f;

		private float _minPosY = -4f, _minPosX = -9f, _maxPosY = 4f, _maxPosX = 9f;

		private void Start()
		{
			GetComponentsAndValues();
			CheckPosition();
		}

		public void SetComponents(GameManager gameManager, SnakeTail snakeTail)
		{
			_gameManager = gameManager;
			_snakeTail = snakeTail;
		}

		public void SetBonusScore(float newScore)
		{
			_bonusScore = (int)newScore;
		}

		public void FoodIsEaten()
		{
			switch (_typeOfFood)
			{
				case TypeOfFood.Apple:
					{
						_gameManager.AddScore(_score);
						SpawnEvent?.Invoke();
						SpawnEvent = null;
						Destroy(gameObject);
						break;
					}
				case TypeOfFood.Bonus:
					{
						_gameManager.AddScore(_bonusScore);
						Destroy(gameObject);
						break;
					}
			}
		}

		private void GetComponentsAndValues()
		{
			//_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

			_snakeTrailComponent = _snakeTail.GetComponent<TrailRenderer>();

			Vector3[] pos = new Vector3[_snakeTrailComponent.positionCount];
			_snakeTrailComponent.GetPositions(pos);
			_snakeTailPointsPositions = pos;
		}

		private void CheckPosition()
		{
			if (_snakeTrailComponent.positionCount > 0)
			{
				for (int i = 0; i < _snakeTailPointsPositions.Length; i++)
				{
					if (Vector3.Distance(transform.position, _snakeTailPointsPositions[i]) < _distanceToTail)
					{
						ResetPosition();
					}
				}
			}
		}

		private void ResetPosition()
		{
			transform.position = new Vector2(Random.Range(_minPosX, _maxPosX), Random.Range(_minPosY, _maxPosY));
			CheckPosition();
		}
	}
}