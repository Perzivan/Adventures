using System;

namespace Yatzy
{
	public class FakeT6Die : IDie
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

		public int roll() {
			return LastRolledNumber;
		}

		public void SetLastRolledNumber(int number) {
			LastRolledNumber = number;
		}
	}
}

