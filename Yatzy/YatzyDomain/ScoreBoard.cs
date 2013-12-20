using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
	public class ScoreBoard
	{
		public List<Player> Players { get; private set;}
	
		public ScoreBoard (List<string> playerNames)
		{
			int numberOfPlayers = playerNames.Count;
			Players = new List<Player> (numberOfPlayers);

			for (int i = 0; i < numberOfPlayers; i++) {
				Players.Add (new Player (playerNames [i]));
			}
		}

		private Player GetPlayer(string playerName) {
			Player player =  Players.Find (p => p.Name == playerName);

			if (player == null) {
				throw new ArgumentException ("The player was not found!");
			}

			return player;
		}


		public void SetScore(string playerName, int score, Common.ScoreType scoreType) {
			Player player = Players.Find (p => p.Name == playerName);
			if (player == null) {
				throw new ArgumentException ("The player " + playerName + " was not found.");
			}

			int index = Convert.ToInt32 (scoreType);
			player.Scores.Insert (index, score);
		}
	}
}

