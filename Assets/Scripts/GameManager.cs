using UnityEngine;

namespace Manager
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private GameObject _apple;

		[SerializeField]
		private GameObject _slowTime;
		[SerializeField]
		private GameObject _growUp;
		[SerializeField]
		private GameObject _speedUp;

		private float _timeToCreateBonus;

		private bool _gameStarted = false;

		private bool _paused = false;
		enum Bonus { SlowTime = 1, GrowUp, SpeedUp };

		private void Start()
		{
			_timeToCreateBonus = Random.Range(5f, 10f);
			SpawnApple();
		}

		void Update()
		{
			SpawnApple();
			SpawnBonus();
		}

		private void SpawnBonus()
		{
			if (_timeToCreateBonus >= 0)
			{
				_timeToCreateBonus -= Time.deltaTime;
			}
			else
			{
				Bonus bonus = (Bonus)Random.Range(1, 3);
				switch (bonus)
				{
					case Bonus.GrowUp:
						{
							Vector2 pos = new Vector2(Random.Range(-9f, 9f), Random.Range(-4f, 4f));
							Instantiate(_growUp, pos, Quaternion.identity);
							break;
						}
					case Bonus.SlowTime:
						{
							Vector2 pos = new Vector2(Random.Range(-9f, 9f), Random.Range(-4f, 4f));
							Instantiate(_slowTime, pos, Quaternion.identity);
							break;
						}
					case Bonus.SpeedUp:
						{
							Vector2 pos = new Vector2(Random.Range(-9f, 9f), Random.Range(-4f, 4f));
							Instantiate(_speedUp, pos, Quaternion.identity);
							break;
						}
				}
				_timeToCreateBonus = Random.Range(5f, 10f);
			}
		}

		private void SpawnApple()
		{
			if (!GameObject.FindGameObjectWithTag("Apple"))
			{
				Vector2 pos = new Vector2(Random.Range(-9f, 9f), Random.Range(-4f, 4f));
				Instantiate(_apple, pos, Quaternion.identity);
			}
		}
	}
}