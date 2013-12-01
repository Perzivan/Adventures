using System;

namespace Yatzy
{
	public class Die
	{
		public int MaxNumber { get; private set;}
		public int MinNumber { get; private set;}
		public int LastRolledNumber { get; private set;}

		public Die (int maxNumber, int minNumber)
		{
			if (maxNumber < 1 || minNumber < 1) {
				throw new ArgumentException ("The maxValue or the minValue is to low."); 
			}

			MaxNumber = maxNumber;
			MinNumber = minNumber;
		}

		public int roll() {
			int result = new Random ().Next (MinNumber, MaxNumber); 
			LastRolledNumber = result;
			return LastRolledNumber;
		}

	}
}

