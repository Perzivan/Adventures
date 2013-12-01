using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
	public class ScoreBoard
	{
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

		public int SumWantedNumber (List<Die> Dies, int wantedNumber)
		{
			List<Die> Wanted = (from Die in Dies 
				                	where Die.LastRolledNumber == wantedNumber 
								select Die).ToList();
	
			return Wanted.Sum (Die => Die.LastRolledNumber);
		}

		public void SetScore(string playerName) {
		
		}
	}
}

