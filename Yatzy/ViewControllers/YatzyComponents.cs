using System;
using System.Collections.Generic;

namespace AwesomeYatzy
{
	public class YatzyComponents
	{
		public readonly List<DieView> DiceViewList = new List<DieView> ();

		public ActionBoardView YatzyTable { get; private set; }

		public string PlayerName { get; private set; }

		public int YatzyTurn { get; set; }

		public const int MaxTurns = 3;

		public int GetMaxTurns ()
		{
			return MaxTurns;
		}

		public YatzyComponents (string playerName)
		{
			PlayerName = playerName;

			List<string> players = new List<string> (){ PlayerName };
			YatzyTable = new ActionBoardView (players);
		}
	}
}

