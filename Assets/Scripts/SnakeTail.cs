using System.Collections;
using UnityEngine;

namespace Snake
{
	public class SnakeTail : MonoBehaviour
	{
		private TrailRenderer _trail;

		[SerializeField]
		private SnakeHead _head;
		[SerializeField]
		private GameObject _mouth;

		[SerializeField]
		private Vector3[] _trailPointsPosition;

		[SerializeField]
		private float BonusTime;

		private enum Bonus { SlowTime = 1, GrowUp, SpeedUp };

		private void Awake()
		{
			_trail = GetComponent<TrailRenderer>();
		}

		private void Update()
		{
			CheckMouth();
			GetPointsPositions();
		}

		public void EatApple()
		{
			_trail.time += 0.25f;
		}

		public void EatBonus(int b)
		{
			Bonus bonus = (Bonus)b;
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
		}

		private void EatGrowUpBonus()
		{
			_trail.time += 0.5f;
		}

		private void EatSlowTimeBonus()
		{
			_trail.time *= 2f;
			_head.SpeedDown();
			StartCoroutine("NormalizeTailAfterSlowBonus");

		}

		private void EatSpeedUpBonus()
		{
			_trail.time /= 2f;
			_head.SpeedUp();
			StartCoroutine("NormalizeTailAfterSpeedUpBonus");
		}

		private void CheckMouth()
		{
			for (int i = 0; i < _trailPointsPosition.Length; i++)
			{
				if (Vector3.Distance(_trailPointsPosition[i], _mouth.transform.position) < 0.1f)
				{
					Debug.Log("Lose");
				}
			}
		}

		private void GetPointsPositions()
		{
			if (_trail.positionCount > 2)
			{
				Vector3[] pos = new Vector3[_trail.positionCount];
				_trail.GetPositions(pos);
				_trailPointsPosition = pos;
			}
		}

		private IEnumerator NormalizeTailAfterSlowBonus()
		{
			yield return new WaitForSeconds(BonusTime);
			_trail.time /= 2f;
		}

		private IEnumerator NormalizeTailAfterSpeedUpBonus()
		{
			yield return new WaitForSeconds(BonusTime);
			_trail.time *= 2f;
		}
	}
}