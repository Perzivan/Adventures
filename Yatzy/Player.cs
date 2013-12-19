using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
	public class Player
	{
		public string Name { get; private set;}
		public List<int> Scores { get; private set;}

		public Player (string name, int numberOfScores)
		{
			Scores = new List<int> (numberOfScores);

			for(int i = 0; i < numberOfScores; i++) { 
				Scores.Add (-1);
			}
		
		}
	}
}

