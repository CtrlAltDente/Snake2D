using System.Collections;
using UnityEngine;
namespace Snake
{
	public class SnakeHead : MonoBehaviour
	{
		[SerializeField]
		private float _normalSpeed;

		private float _speed;

		[SerializeField]
		private GameObject _headSprite;

		[SerializeField]
		private TrailRenderer _tail;
		[SerializeField]
		private MeshCollider _tailCollider;

		private Vector3 _selectedDirection;

		private float _previousHorizontalInput = 0f;
		private float _previousVerticalInput = 0f;

		[SerializeField]
		private float _timeBlockTurn;

		private float _timeToTurn = 0f;

		[SerializeField]
		private float BonusTime;

		private void Awake()
		{
			_speed = _normalSpeed;
			_selectedDirection = Vector3.right;
			_previousHorizontalInput = 1f;
			_headSprite.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
		}

		private void Update()
		{
			Move();
			DisableBlockTurn();
		}

		public void SpeedUp()
		{
			_speed *= 2f;
			StartCoroutine("NormalizeSpeed");
		}

		public void SpeedDown()
		{
			_speed /= 2f;
			StartCoroutine("NormalizeSpeed");
		}

		private void DisableBlockTurn()
		{
			if (_timeToTurn >= 0)
			{
				_timeToTurn -= Time.deltaTime;
			}
		}

		private void Move()
		{
			float horizontalInput = Input.GetAxisRaw("Horizontal");
			float verticalInput = Input.GetAxisRaw("Vertical");
			Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f);
			if (_timeToTurn <= 0f)
			{
				if (direction.x != 0)
				{
					if (_previousHorizontalInput == direction.x || _previousHorizontalInput == 0f)
					{
						_selectedDirection = Vector3.right * direction.x;
						Rotate(_selectedDirection.x * 90f);
						_previousVerticalInput = 0f;
						_previousHorizontalInput = direction.x;

						_timeToTurn = _timeBlockTurn;
					}
				}
				if (direction.y != 0)
				{
					if (_previousVerticalInput == direction.x || _previousVerticalInput == 0f)
					{
						_selectedDirection = Vector3.up * direction.y;
						if (direction.y > 0)
						{
							Rotate(180f);
						}
						if (direction.y < 0)
						{
							Rotate(0f);
						}
						_previousHorizontalInput = 0f;
						_previousVerticalInput = direction.y;
						_timeToTurn = _timeBlockTurn;
					}
				}
			}

			transform.Translate(_selectedDirection * _speed * Time.deltaTime);
		}

		private void Rotate(float rotation)
		{
			_headSprite.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
		}

		private IEnumerator NormalizeSpeed()
		{
			yield return new WaitForSeconds(BonusTime);
			_speed = _normalSpeed;
		}
	}
}