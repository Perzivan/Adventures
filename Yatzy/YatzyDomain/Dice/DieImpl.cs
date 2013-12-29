using System;
using System.Threading;

namespace AwesomeYatzy
{
	public class DieImpl : Die
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

		public DieImpl (int minNumber, int maxNumber)
		{
			if (maxNumber < 1 || minNumber < 1) {
				throw new ArgumentException ("The maxValue or the minValue is to low."); 
			}
			MaxNumber = maxNumber+1;
			MinNumber = minNumber;
		}

		public int Roll() {
			int result = new Random(Guid.NewGuid().GetHashCode()).Next(MinNumber,MaxNumber);
			LastRolledNumber = result;
			return LastRolledNumber;
		}

	}
}

