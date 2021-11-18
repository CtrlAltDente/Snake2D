using Managers;
using UnityEngine;

namespace Foods
{
	public class Food : MonoBehaviour
	{
		public delegate void SpawnCallback();
		public event SpawnCallback SpawnEvent;

		private GameObject _snake;

		private TrailRenderer _snakeTail;

		private Vector3[] _snakeTailPointsPositions;

		private GameManager _gameManager;

		private enum TypeOfFood : int { Apple = 1, Bonus };

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
			_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

			_snake = GameObject.FindGameObjectWithTag("Player");
			_snakeTail = GameObject.FindGameObjectWithTag("SnakeTail").GetComponent<TrailRenderer>();

			Vector3[] pos = new Vector3[_snakeTail.positionCount];
			_snakeTail.GetPositions(pos);
			_snakeTailPointsPositions = pos;
		}
		private void CheckPosition()
		{
			if (_snakeTail.positionCount > 0)
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