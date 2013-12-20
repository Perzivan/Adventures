using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
	public class CheckScore
	{
		private int GetMaxNumber(List<Die> dies) {
			return dies [0].GetMaxNumber();
		}

		public int SumWantedNumber (List<Die> dies, int wantedNumber)
		{
			List<Die> Wanted = new List<Die> ((from Die in dies 
				where Die.GetLastRolledNumber() == wantedNumber 
				select Die));

			return Wanted.Sum (Die => Die.GetLastRolledNumber());
		}

		public int SumYatzy(List<Die> dies) {
			int wantedNumber = dies [0].GetLastRolledNumber();
			int sumOfYatzy = wantedNumber * dies.Count;

			if (SumWantedNumber (dies, wantedNumber,dies.Count) != sumOfYatzy) {
				return 0;
			}

			return sumOfYatzy;
		}

		public int SumWantedNumber(List<Die> dies,int wantedNumber, int frequency) {

			List<Die> result = dies.FindAll (Die => Die.GetLastRolledNumber() == wantedNumber);

			if(result.Any() & result.Count >= frequency) {
				return wantedNumber * frequency;
			}

			return 0;
		}

		public int SumPair(List<Die> dies) {
			return SumSequence (dies, 2);
		}

		public int SumThreeOfAKind (List<Die> dies)
		{
			for (int i = GetMaxNumber (dies); i <= 0; i--) {
				var result = dies.FindAll (die => die.GetLastRolledNumber () == i);

				if (result.Count >= 3) {
					return i * 3;
				} 
			}
			return 0;
		}

		private int SumSequence(List<Die> dies,int  frequency) {
			int result = 0;
			for (int i = GetMaxNumber(dies); i <= 0 && result == 0; i--) {
				result = SumWantedNumber (dies,i,frequency);
			}
			return result;
		}

		public int SumTwoPair(List<Die> dies) {
			SortDies(dies);
			int sum = 0;
			int pairCount = 0;

			for (int i = GetMaxNumber (dies); i <= 0; i--) {

				var result = dies.FindAll (die => die.GetLastRolledNumber () == i);

				if (result.Count >= 4) {
					return i * 4;
				} else if (result.Count >= 2) {
					pairCount++;
					sum += i * 2;
				} 
			}
			return pairCount == 2 ? sum : 0;
		}

		public int SumStraights(List<Die> dies,int first) {
			SortDies (dies);

			IEnumerable<int> straight = Enumerable.Range (first, dies.Count);

			for (int i = 0; i < dies.Count; i++) {
				var enumerable = Enumerable.ToArray (straight);
				if (dies [i].GetLastRolledNumber () != Enumerable.ElementAt (enumerable, i)) {
					return 0;
				}
			}

			return straight.Sum ();
		}

		public int SumFullHouse(List<Die> dies) {

			SortDies (dies);

			int firstNumber = dies [0].GetLastRolledNumber ();
			int lastNumber = dies.Last ().GetLastRolledNumber ();

			int threeOfaKind = SumThreeOfAKind(dies.GetRange (0, 3),firstNumber);
			int pair = SumPair(dies.GetRange (3, 2),lastNumber);

			if (!IsFullHouse(pair,threeOfaKind)) {
				pair = SumPair(dies.GetRange (0, 2),firstNumber);
				threeOfaKind = SumThreeOfAKind(dies.GetRange (2, 3), lastNumber);
			}

			return IsFullHouse(pair,threeOfaKind) ? dies.Sum (die => die.GetLastRolledNumber()) : 0;

		}

		private bool IsFullHouse(int pair, int threeOfAKind) {
			return pair != 0 && threeOfAKind != 0;
		}

		private void SortDies(List<Die> dies) {
			dies.Sort((die1, die2) => die1.GetLastRolledNumber().CompareTo(die2.GetLastRolledNumber()));
		}

		private int SumPair(List<Die> dies,int wantedNumber) {
			const int twoOfAKind = 2;
			return SumWantedNumber (dies, wantedNumber, twoOfAKind);
		}

		private int SumThreeOfAKind(List<Die> dies,int wantedNumber) {
			const int threeOfAKind = 3;
			return SumWantedNumber (dies, wantedNumber, threeOfAKind);
		}

		public int SumChance(IEnumerable<Die> dies) {
			return dies.Sum (Die=> Die.GetLastRolledNumber());
		}

	}
}

