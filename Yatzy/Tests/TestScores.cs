using System;
using System.Collections.Generic;

namespace Yatzy
{
	public class TestScores
	{
		readonly CheckScore _CheckScore = new CheckScore ();

		public TestScores (string playerName)
		{
			List<string> players = SetupPlayer (playerName);
		}

		private List<string> SetupPlayer(string name) {
			List<string> playerNames = new List<string> ();
			playerNames.Add (name);
			return playerNames;
		}

		private ScoreBoard SetupBoard(List<string> playerNames) {
			return new ScoreBoard (playerNames);
		}

		public bool ShouldBeFullHouse() {
			List<Die> dies = new List<Die> ();
			PopulateDies (dies, 3, 6);
			PopulateDies (dies, 2, 5);
			return _CheckScore.SumFullHouse (dies) == 28;
		}

		public bool ShouldNotBeFullHouse() {
			List<Die> dies = new List<Die> ();
			PopulateDies (dies, 4, 1);
			PopulateDies (dies, 1, 5);
			return _CheckScore.SumFullHouse (dies) == 0;		
		}

		public bool ShouldBeSmallStraight() {
			List<Die> dies = new List<Die> ();
			PopulateIterative (dies, 1,5);
			return _CheckScore.SumStraights (dies,1) == 15;
		}

		public bool ShouldBeBigStraight() {
			List<Die> dies = new List<Die> ();
			PopulateIterative (dies, 2,6);
			return _CheckScore.SumStraights (dies,2) == 20;		
		}

		public bool ShouldNotBeSmallStraight() {
			List<Die> dies = new List<Die> ();
			PopulateDies (dies, 5, 5);
			return _CheckScore.SumStraights (dies, 1) == 0;
		}

		public bool ShouldBeYatzy() {
			List<Die> dies = new List<Die> ();
			PopulateDies (dies, 5, 6);
			return _CheckScore.SumYatzy(dies) == 30;
		}

		private void PopulateIterative(List<Die> dies, int minValue, int maxValue) {
			for (int i = minValue; i <= maxValue; i++) {
				AddDieToList (dies,i);	
			}
		} 

		private void PopulateDies(List<Die> dies, int numberOfDies, int dieValue) {
			for (int i = 0; i < numberOfDies; i++) {
				AddDieToList (dies,dieValue);
			}
		}

		private void AddDieToList(List<Die> dies,int dieValue) {
			FakeT6Die die = DieFactory.CreateFakeDie (dieValue);
			dies.Add (die);
		}
	}
}



