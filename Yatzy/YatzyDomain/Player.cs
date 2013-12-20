using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
	public class Player
	{
		public string Name { get; private set;}
		public List<int> Scores { get; private set;}

		private const int NumberOfScores = 16; 

		public Player (string name)
		{
			Scores = new List<int> (NumberOfScores);

			for(int i = 0; i < NumberOfScores; i++) { 
				Scores.Add (-1);
			}
		
		}
	}
}

