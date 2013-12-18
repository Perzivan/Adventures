using System;

namespace Yatzy
{
	public class Die : IDie
	{
		private int MaxNumber { get; set;}
		private int MinNumber { get; set;}
		private int LastRolledNumber { get; set;}

		public int GetMaxNumber() {
			return MaxNumber;
		}

		public int GetMinNumber() { 
			return MinNumber;
		}

		public int GetLastRolledNumber() {
			return LastRolledNumber;
		}

		public Die (int minNumber, int maxNumber)
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

