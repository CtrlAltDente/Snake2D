using UnityEngine;

namespace Snake
{
	public class SnakeMouth : MonoBehaviour
	{
		private enum Bonus { SlowTime = 1, GrowUp, SpeedUp };

		[SerializeField]
		private SnakeTail _snakeTail;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			Debug.Log("Trigger!");

			switch (collision.gameObject.tag)
			{
				case "Apple":
					{
						Destroy(collision.gameObject);
						_snakeTail.EatApple();
						break;
					}
				case "Bonus_GrowUp":
					{
						Destroy(collision.gameObject);
						_snakeTail.EatBonus((int)Bonus.GrowUp);
						break;
					}
				case "Bonus_SpeedUp":
					{
						Destroy(collision.gameObject);
						_snakeTail.EatBonus((int)Bonus.SpeedUp);
						break;
					}
				case "Bonus_SlowTime":
					{
						Destroy(collision.gameObject);
						_snakeTail.EatBonus((int)Bonus.SlowTime);
						break;
					}
			}
		}
	}
}