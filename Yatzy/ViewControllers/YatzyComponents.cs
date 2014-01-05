using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using System.Drawing;

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
			CreateYatzyDies ();
		}

		public void HideDies() {
			DiceViewList.ForEach(dice=>dice.Hidden = true);
		}

		public void ShowDies() {
			DiceViewList.ForEach(dice=>dice.Hidden = false);
		}

		//Todo: Maybe I should put this somewhere else. 
		private void CreateYatzyDies() {

			int y = Convert.ToInt32(UIScreen.MainScreen.Bounds.Height - 80);

			DiceViewList.Add(new DieView (MakeRectangle(40,y)));
			DiceViewList.Add(new DieView (MakeRectangle(100,y)));
			DiceViewList.Add(new DieView (MakeRectangle(160,y)));
			DiceViewList.Add(new DieView (MakeRectangle(220,y)));
			DiceViewList.Add(new DieView (MakeRectangle(280,y)));
			HideDies ();
		}

		private RectangleF MakeRectangle(int x, int y) {
			PointF location = new PointF (x, y);
			SizeF size = new SizeF (50, 50);
			return new RectangleF (location, size);		
		}

		public void AdjustToScreenSize(UIView view) {
			RectangleF rectangle =view.Frame;
			rectangle.Y = UIScreen.MainScreen.ApplicationFrame.Height- 124;
			view.Frame = rectangle;		
		}

		public void PopulateValue(string playerName, Common.ScoreType type, int sum) {
			YatzyTable.Board.SetScore(playerName,sum,type);
			ResetForNextTurn();
		}

		public void ResetForNextTurn() {
			YatzyTurn = 0;
			DiceViewList.ForEach(dice=>dice.Unselect());
			HideDies ();
		}
	}
}

