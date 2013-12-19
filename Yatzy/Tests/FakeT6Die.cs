using System;

namespace Yatzy
{
	public class FakeT6Die : Die
	{
		private int LastRolledNumber { get; set;}

		public FakeT6Die ()
		{
			LastRolledNumber = 1;
		}

		public int GetMaxNumber() {
			return 6;
		}

		public int GetMinNumber() { 
			return 1;
		}

		public int GetLastRolledNumber() {
			return LastRolledNumber;
		}

		public int Roll() {
			return LastRolledNumber;
		}

		public void SetLastRolledNumber(int number) {
			LastRolledNumber = number;
		}
	}
}

