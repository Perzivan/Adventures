using System;
using System.Collections.Generic;

namespace Yatzy
{
	public class TestScores
	{
		readonly ScoreBoard _ScoreBoard;

		public TestScores (string playerName)
		{
			List<string> players = SetupPlayer (playerName);
			_ScoreBoard = SetupBoard (players);
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
			List<IDie> dies = new List<IDie> ();
			PopulateDies (dies, 3, 6);
			PopulateDies (dies, 2, 5);
			return _ScoreBoard.SumFullHouse (dies) == 28;
		}

		public bool ShouldNotBeFullHouse() {
			List<IDie> dies = new List<IDie> ();
			PopulateDies (dies, 4, 1);
			PopulateDies (dies, 1, 5);
			return _ScoreBoard.SumFullHouse (dies) == 0;		
		}

		public bool ShouldBeSmallStraight() {
			List<IDie> dies = new List<IDie> ();
			PopulateIterative (dies, 1,5);
			return _ScoreBoard.SumStraights (dies,1) == 15;
		}

		public bool ShouldBeBigStraight() {
			List<IDie> dies = new List<IDie> ();
			PopulateIterative (dies, 2,6);
			return _ScoreBoard.SumStraights (dies,2) == 20;		
		}

		public bool ShouldNotBeSmallStraight() {
			List<IDie> dies = new List<IDie> ();
			PopulateDies (dies, 5, 5);
			return _ScoreBoard.SumStraights (dies, 1) == 0;
		}

		public bool ShouldBeYatzy() {
			List<IDie> dies = new List<IDie> ();
			PopulateDies (dies, 5, 6);
			return _ScoreBoard.SumYatzy(dies) == 30;
		}

		private void PopulateIterative(List<IDie> dies, int minValue, int maxValue) {
			for (int i = minValue; i <= maxValue; i++) {
				AddDieToList (dies,i);	
			}
		} 

		private void PopulateDies(List<IDie> dies, int numberOfDies, int dieValue) {
			for (int i = 0; i < numberOfDies; i++) {
				AddDieToList (dies,dieValue);
			}
		}

		private void AddDieToList(List<IDie> dies,int dieValue) {
			FakeT6Die die = DieFactory.CreateFakeDie (dieValue);
			dies.Add (die);
		}
	}
}



