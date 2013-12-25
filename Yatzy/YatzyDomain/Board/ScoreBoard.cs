using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
	public class ScoreBoard
	{
		public List<Player> Players { get; private set;}
		public Player CurrentPlayer { get;set;}
	
		public ScoreBoard (List<string> playerNames)
		{
			int numberOfPlayers = playerNames.Count;
			Players = new List<Player> (numberOfPlayers);

			for (int i = 0; i < numberOfPlayers; i++) {
				int numberOfScores = GetTotalIndex () + 1;
				Players.Add (new Player (playerNames [i],numberOfScores));
			}

			CurrentPlayer = Players [0];
		}


		public void ChangePlayer() {
			throw new NotImplementedException ();
		}

		private int GetTotalIndex() {
			return Enum.GetNames (typeof(Common.ScoreType)).Count()-1;
		}

		public void AddScoreToTotal(string playerName, int score) {
			int playerIndex = GetIndexForPlayer (playerName);
			RemoveDefaultValue (Players [playerIndex].Scores, GetTotalIndex ());
			Players [playerIndex].Scores [GetTotalIndex ()] += score;
		}

		public void SetScore(string playerName, int score, Common.ScoreType scoreType) {
			int playerIndex = GetIndexForPlayer (playerName);
			int scoreIndex = Convert.ToInt32 (scoreType);
			RemoveDefaultValue (Players [playerIndex].Scores, scoreIndex);

			int currentScore = GetScore (playerName,scoreType);

			if (currentScore == 0) {
				Players [playerIndex].Scores [scoreIndex] = score;
				AddScoreToTotal (playerName, score);
			}

		}

		public bool IsUnpopulated(string playerName, Common.ScoreType scoreType) {
			int playerIndex = GetIndexForPlayer (playerName);
			int index = Convert.ToInt32 (scoreType);
			int score = Players [playerIndex].Scores [index];
			return score == -1;
		}

		public bool HasBonus(string playerName) {
			return GetSum (playerName) >= 63;
		}

		public void SetBonus(string playerName) {
			const int bonus = 50;
			int bonusIndex = Convert.ToInt32 (Common.ScoreType.Bonus);
			int playerIndex = GetIndexForPlayer (playerName);
			List<int> scores = Players [playerIndex].Scores;
			if (scores [bonusIndex] == 0) {
				scores [bonusIndex] = bonus;
				AddScoreToTotal (playerName, bonus);
			}
		}

		public bool HasRoundsLeft(string playerName) {
			int playerIndex = GetIndexForPlayer (playerName);
			const int unpopulated = -1;
			return Players [playerIndex].Scores.FindAll (number=> number == unpopulated).Count > 0;
		}

		public int GetSum(string playerName) {
			int playerIndex = GetIndexForPlayer (playerName);
			return Players [playerIndex].Scores [(int)Common.ScoreType.Sum];
		}

		public void SetSum(string playerName) {
			int sumIndex = Convert.ToInt32 (Common.ScoreType.Sum);
			int playerIndex = GetIndexForPlayer (playerName);

			int sum = 0;
			for(int i = 0; i < (int) Common.ScoreType.Sum; i++) {

				int currentScore = Players [playerIndex].Scores [i];
				if (currentScore == -1) {
					currentScore = 0;
				}
				sum += currentScore;
			}

			Players [playerIndex].Scores [sumIndex] = sum;
		}

		private int GetScore (string playerName, Common.ScoreType scoreType) {
			int playerIndex = GetIndexForPlayer (playerName);
			int index = Convert.ToInt32 (scoreType);
			return Players [playerIndex].Scores [index];
		}

		public int GetDisplayScore (string playerName, Common.ScoreType scoreType) {
			int score = GetScore (playerName, scoreType);
			return score == -1 ? 0 : score;	
		}

		public int GetRawScore(string playerName, Common.ScoreType scoreType) {
			return GetScore (playerName, scoreType);
		}

		private int GetIndexForPlayer(string playerName) {
			for (int i = 0; i < this.Players.Count; i++) {
				if (Players [i].Name == playerName) {
					return i;
				}
			}

			return 0;		
		}

		private void RemoveDefaultValue(List<int> scores, int position) {
			if (scores [position] == -1) {
				scores [position] = 0;
			}
		}
	}
}

