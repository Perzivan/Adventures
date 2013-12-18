using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
	public class ScoreBoard
	{
		public enum ScoreType {
			Ones = 0,
			Twos = 1,
			Threes = 2,
			Fours = 3,
			Five = 4,
			Six = 5,
			Pair = 6,
			TwoPair = 7,
			ThreeOfAKind = 8,
			FourOfAKind = 9,
			SmallStraight = 10,
			BigStraight = 11,
			FullHouse = 12,
			Chance = 13
		}

		public List<Player> Players { get; private set;}
		private const int NumberOfScores = 16; 

		public ScoreBoard (List<string> playerNames)
		{
			int numberOfPlayers = playerNames.Count;
			Players = new List<Player> (numberOfPlayers);

			for (int i = 0; i < numberOfPlayers; i++) {
				Players.Add (new Player (playerNames [i], NumberOfScores));
			}

		}

		private Player GetPlayer(string playerName) {
			Player player =  Players.Find (p => p.Name == playerName);

			if (player == null) {
				throw new ArgumentException ("The player was not found!");
			}

			return player;
		}

		public int SumWantedNumber (List<IDie> dies, int wantedNumber)
		{
			List<IDie> Wanted = new List<IDie> ((from Die in dies 
				where Die.GetLastRolledNumber() == wantedNumber 
				select Die));
	
			return Wanted.Sum (Die => Die.GetLastRolledNumber());
		}

		public int SumYatzy(List<IDie> dies) {
			int wantedNumber = dies [0].GetLastRolledNumber();
			int sumOfYatzy = wantedNumber * dies.Count;

			if (SumWantedNumber (dies, wantedNumber) != sumOfYatzy) {
				return 0;
			}

			return sumOfYatzy;
		}

		public int SumWantedNumber(List<IDie> dies,int wantedNumber, int frequency) {

			List<IDie> result = dies.FindAll (Die => Die.GetLastRolledNumber() == wantedNumber);

			if(result.Any() & result.Count >= frequency) {
					return wantedNumber * frequency;
			}

			return 0;
		}

		public int SumStraights(List<IDie> dies,int first) {
			dies.Sort((die1, die2) => die1.GetLastRolledNumber().CompareTo(die2.GetLastRolledNumber()));
			IEnumerable<int> straight = Enumerable.Range (first, dies.Count);
	
			for (int i = 0; i < dies.Count; i++) {
				if (dies [i].GetLastRolledNumber() != straight.ElementAt(i)) {
					return 0;
				}
			}

			return straight.Sum ();
		}

		public int SumFullHouse(List<IDie> dies) {

			dies.Sort((die1, die2) => die1.GetLastRolledNumber().CompareTo(die2.GetLastRolledNumber()));

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

		private int SumPair(List<IDie> dies,int wantedNumber) {
			const int twoOfAKind = 2;
			return SumWantedNumber (dies, wantedNumber, twoOfAKind);
		}

		private int SumThreeOfAKind(List<IDie> dies,int wantedNumber) {
			const int threeOfAKind = 3;
			return SumWantedNumber (dies, wantedNumber, threeOfAKind);
		}

		public int SumChance(IEnumerable<IDie> dies) {
			return dies.Sum (Die=> Die.GetLastRolledNumber());
		}

		public void SetScore(string playerName, int score, ScoreType scoreType) {
			Player player = Players.Find (p => p.Name == playerName);
			if (player == null) {
				throw new ArgumentException ("The player " + playerName + " was not found.");
			}

			int index = Convert.ToInt32 (scoreType);
			player.Scores.Insert (index, score);
		}
	}
}

