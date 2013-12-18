using System;

namespace Yatzy
{
	public static class DieFactory {
		public static FakeT6Die CreateFakeDie(int value) {
			FakeT6Die die = new FakeT6Die();
			die.SetLastRolledNumber (value);
			return die;	
		}
	}
}

