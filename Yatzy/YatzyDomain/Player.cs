using System;
using System.Collections.Generic;
using System.Linq;

namespace AwesomeYatzy
{
	public class Player
	{
		public string Name { get; private set;}
		public List<int> Scores { get; private set;}

		public Player (string name, int numberOfScores)
		{
			Name = name;
			Scores = new List<int> (numberOfScores);

			for(int i = 0; i < numberOfScores; i++) {
					Scores.Add (-1);
			}

			Scores [(int)Common.ScoreType.Bonus] = 0;
		}
	}
}

