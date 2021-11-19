using Managers;
using UnityEngine;

namespace Snake
{
	public class SnakeHead : MonoBehaviour
	{
		private float _normalSpeed = 3f;
		private float _changedSpeed;

		private float _minSpeed = 0.6f;
		private float _speedScale = 0.2f;

		[SerializeField]
		private GameObject _headSprite;

		private Vector3 _selectedDirection;
		private Vector3 _direction;

		private float _verticalInput;
		private float _horizontalInput;

		private float _previousHorizontalInput = 0f;
		private float _previousVerticalInput = 0f;

		[SerializeField]
		private float _timeBlockTurn;

		private float _timeToTurn = 0f;

		[SerializeField]
		private SnakeTail _snakeTail;
		[SerializeField]
		private SnakeMouth _snakeMouth;

		private void Start()
		{
			_selectedDirection = Vector3.right;
			_previousHorizontalInput = 1f;
			_headSprite.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
		}

		private void Update()
		{
			InputController();
			MovementController();
			DisableBlockTurn();
		}

		public void SetGameManager(GameManager gameManager)
		{
			_snakeTail.SetGameManager(gameManager);
			_snakeMouth.SetGameManager(gameManager);
		}

		public SnakeTail GetTail()
		{
			return _snakeTail;
		}

		public void SetSpeed(float newSpeed)
		{
			_changedSpeed = newSpeed;
			Time.timeScale = _minSpeed + _changedSpeed * _speedScale;
			Debug.Log(Time.timeScale);
		}

		private void DisableBlockTurn()
		{
			if (_timeToTurn >= 0 && Time.timeScale != 0f)
			{
				_timeToTurn -= Time.deltaTime;
			}
		}

		private void InputController()
		{
			_horizontalInput = Input.GetAxisRaw("Horizontal");
			_verticalInput = Input.GetAxisRaw("Vertical");
			_direction = new Vector3(_horizontalInput, _verticalInput, 0f);
		}

		private void MovementController()
		{
			if (_timeToTurn <= 0f && Time.timeScale != 0f)
			{
				if (_direction.x != 0 && _direction.y == 0)
				{
					if (_previousHorizontalInput == _direction.x || _previousHorizontalInput == 0f)
					{
						_selectedDirection = Vector3.right * _direction.x;
						Rotate(_selectedDirection.x * 90f);
						_previousVerticalInput = 0f;
						_previousHorizontalInput = _direction.x;

						_timeToTurn = _timeBlockTurn;
					}
				}
				if (_direction.y != 0 && _direction.x == 0)
				{
					if (_previousVerticalInput == _direction.x || _previousVerticalInput == 0f)
					{
						_selectedDirection = Vector3.up * _direction.y;
						if (_direction.y > 0)
						{
							Rotate(180f);
						}
						if (_direction.y < 0)
						{
							Rotate(0f);
						}
						_previousHorizontalInput = 0f;
						_previousVerticalInput = _direction.y;
						_timeToTurn = _timeBlockTurn;
					}
				}
			}
			if (Time.timeScale != 0f)
			{
				transform.Translate(_selectedDirection * _normalSpeed * Time.deltaTime);
			}
		}

		private void Rotate(float rotation)
		{
			_headSprite.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
		}
	}
}