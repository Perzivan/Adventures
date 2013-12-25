using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
	public class CheckScore
	{
		private int GetMaxNumber(List<Die> dies) {
			return dies [0].GetMaxNumber()-1;
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
			return 50;
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
			return SumSequence (dies, 3);
		}

		public int SumFourOfAKind (List<Die> dies)
		{
			return SumSequence (dies, 4);
		}

		private int SumSequence(List<Die> dies,int  frequency) {
			int result = 0;
			for (int i = GetMaxNumber(dies); i >= 0 && result == 0; i--) {
				result = SumWantedNumber (dies,i,frequency);
			}
			return result;
		}

		public int SumTwoPair(List<Die> dies) {
			SortDies(dies);
			int sum = 0;
			int pairCount = 0;

			for (int i = GetMaxNumber (dies); i >= 0; i--) {

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

		//Bleh.. Refactor this! 
		public int SumScoreForType(List<Die> dies,Common.ScoreType scoreType) {

			CheckScore Check = new Yatzy.CheckScore ();

			switch (scoreType) {
			case Common.ScoreType.Ones:
			case Common.ScoreType.Twos:
			case Common.ScoreType.Threes:
			case Common.ScoreType.Fours:
			case Common.ScoreType.Fives:
			case Common.ScoreType.Sixes:
				return Check.SumWantedNumber (dies,(int) scoreType + 1);
			case Common.ScoreType.One_Pair: 
				return Check.SumPair (dies);
			case Common.ScoreType.Two_Pairs:
				return Check.SumTwoPair (dies);
			case Common.ScoreType.Three_Of_A_Kind:
				return Check.SumThreeOfAKind (dies);
			case Common.ScoreType.Four_Of_A_Kind:
				return Check.SumFourOfAKind (dies);
			case Common.ScoreType.Small_Straight:
				return Check.SumStraights (dies, 1);
			case Common.ScoreType.Big_Straight:
				return Check.SumStraights (dies, 2);
			case Common.ScoreType.Full_House:
				return Check.SumFullHouse (dies);
			case Common.ScoreType.Chance:
				return Check.SumChance (dies);
			case Common.ScoreType.Yatzy:
				return Check.SumYatzy (dies);
			}
			return 0;
		}
	}
}

