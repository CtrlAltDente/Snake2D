using Foods;
using Managers;
using System.Collections;
using UnityEngine;

namespace Snake
{
	public class SnakeMouth : MonoBehaviour
	{
		private GameManager _gameManager;

		[SerializeField]
		private float _growing;

		[SerializeField]
		private float _slowTimeOnSeconds;

		private float _timeScale;

		private AudioSource _audioSource;

		private enum Bonus { SlowTime = 1, GrowUp, SpeedUp };

		[SerializeField]
		private SnakeTail _snakeTail;

		private void Start()
		{
			_audioSource = GetComponent<AudioSource>();
			_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			switch (collision.gameObject.name)
			{
				case "Border":
					{
						_gameManager.GameOver();
						break;
					}

				case "Apple(Clone)":
					{
						collision.gameObject.GetComponent<Food>().FoodIsEaten();
						EatApple();
						break;
					}
				case "Bonus_GrowUp(Clone)":
					{
						collision.gameObject.GetComponent<Food>().FoodIsEaten();
						Destroy(collision.gameObject);
						EatBonus(Bonus.GrowUp);
						break;
					}
				case "Bonus_SpeedUp(Clone)":
					{
						collision.gameObject.GetComponent<Food>().FoodIsEaten();
						Destroy(collision.gameObject);
						EatBonus(Bonus.SpeedUp);
						break;
					}
				case "Bonus_SlowTime(Clone)":
					{
						collision.gameObject.GetComponent<Food>().FoodIsEaten();
						Destroy(collision.gameObject);
						EatBonus(Bonus.SlowTime);
						break;
					}
			}
		}

		public void EatApple()
		{
			_snakeTail.GrowingUp(_growing);
			_audioSource.Play();
		}

		private void EatBonus(Bonus bonus)
		{
			switch (bonus)
			{
				case Bonus.GrowUp:
					{
						EatGrowUpBonus();
						break;
					}
				case Bonus.SlowTime:
					{
						EatSlowTimeBonus();
						break;
					}
				case Bonus.SpeedUp:
					{
						EatSpeedUpBonus();
						break;
					}
			}
			_audioSource.Play();
		}

		private void EatGrowUpBonus()
		{
			_snakeTail.GrowingUp(_growing * 4);
		}

		private void EatSlowTimeBonus()
		{
			if (Time.timeScale >= 1)
			{
				_timeScale = Time.timeScale;
			}
			_snakeTail.GrowingUp(_growing);
			if (Time.timeScale > 0.5f)
			{
				Time.timeScale /= 2;
			}
			StartCoroutine("CancelSlowTime");
		}

		private IEnumerator CancelSlowTime()
		{
			yield return new WaitForSeconds(_slowTimeOnSeconds * Time.timeScale);
			Time.timeScale = _timeScale;
		}

		private void EatSpeedUpBonus()
		{
			_snakeTail.GrowingUp(_growing);
			Time.timeScale += 0.2f;
		}
	}
}