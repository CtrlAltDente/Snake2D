using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Snake
{
	public class SnakeTail : MonoBehaviour
	{
		[SerializeField]
		private GameManager _gameManager;

		[SerializeField]
		private Text _tailLenght;

		private TrailRenderer _trail;

		[SerializeField]
		private GameObject[] _mouth;

		[SerializeField]
		private Vector3[] _trailPointsPosition;

		private bool _collisionFinded = false;

		private float _distanceToContact = 0.1f;

		private void Start()
		{
			_trail = GetComponent<TrailRenderer>();
		}

		public void SetGameManager(GameManager gameManager)
		{
			_gameManager = gameManager;
			_tailLenght = _gameManager.GetLenghtTextComponent();
		}

		public void GrowingUp(float value)
		{
			_trail.time += value;
		}

		private void Update()
		{
			GetPointsPositions();
			CheckToLose();
			CalculateLenght();
		}

		private void CheckToLose()
		{
			for (int i = 0; i < _trailPointsPosition.Length; i++)
			{
				for (int j = 0; j < _mouth.Length; j++)
				{

					if (i > 3 && Vector3.Distance(_trailPointsPosition[i], _mouth[j].transform.position) < _distanceToContact)
					{
						_collisionFinded = true;
						_gameManager.GameOver();
						break;
					}
					if (_collisionFinded)
					{
						break;
					}
				}
				if (_collisionFinded)
				{
					break;
				}
			}
		}

		private void GetPointsPositions()
		{
			if (_trail.positionCount > 0)
			{
				Vector3[] pos = new Vector3[_trail.positionCount];
				_trail.GetPositions(pos);
				_trailPointsPosition = pos;
			}
		}

		private void CalculateLenght()
		{
			_tailLenght.text = "Lenght:" + (_trail.time / 0.25f).ToString("0");
		}
	}
}