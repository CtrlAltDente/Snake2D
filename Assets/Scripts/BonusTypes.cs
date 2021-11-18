namespace Bonuses
{
	public class BonusTypes
	{
		public enum Bonus : int { GrowUp = 1, SlowTime = 2, SpeedUp = 3 };

		private Bonus _bonus;

		public void SetBonusType(int b)
		{
			_bonus = (Bonus)b;
		}

		public Bonus GetBonusType()
		{
			return _bonus;
		}
	}
}