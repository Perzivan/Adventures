using Appracatappra.ActionComponents.ActionTable;
using System.Drawing;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace AwesomeYatzy
{
	public class ActionBoardView : UIView
	{

		public UIActionTableViewController ComponentTable { get; private set;}
		public ScoreBoard Board { get; private set;}

		public ActionBoardView (List<string> players)
		{
			Board = new ScoreBoard (players);
			ComponentTable = new UIActionTableViewController (UITableViewStyle.Grouped,new RectangleF (0, 20, 318, 305));

			ComponentTable.dataSource.RequestData += PopulateData;

			ComponentTable.LoadData ();
		}

		void PopulateData (UIActionTableViewDataSource dataSource)
		{
			UIActionTableSection section = new UIActionTableSection ("Yatzy");
			string[] scoreNames = Enum.GetNames (typeof(Common.ScoreType));
			for (int i = 0; i < scoreNames.Length; i++) {
				Common.ScoreType type = (Common.ScoreType)Enum.Parse (typeof(Common.ScoreType), scoreNames [i]);
				int score = Board.GetDisplayScore (Board.CurrentPlayer.Name, type);
				string scoreText = score.ToString ();
				string scoreName = Common.RemoveUnderScores(scoreNames [i]);
				int rawScore = Board.GetRawScore (Board.CurrentPlayer.Name, type);
				if (IsCheckMarkItem (rawScore, type)) {
					scoreText = ((char)0x221A) + " " + scoreText;
				}
				UIActionTableItem item;
				if (CanSelect (type)) {
					item = new UIActionTableItem (scoreName, scoreText, UITableViewCellStyle.Value1, true);
				}
				else {
					item = new UIActionTableItem (true, scoreName, scoreText, UITableViewCellStyle.Subtitle, UITableViewCellAccessory.DetailButton, false);
				}
				section.AddItem (item);
			}

			dataSource.AddSection (section);
		}

		private bool CanSelect(Common.ScoreType type) {
			return type != Common.ScoreType.Bonus &&  
				type != Common.ScoreType.Sum &&  
				type != Common.ScoreType.Total_Score;
		}

		private bool IsCheckMarkItem(int number, Common.ScoreType type) {
			switch (type) {
			case Common.ScoreType.Sum:
			case Common.ScoreType.Bonus:
			case Common.ScoreType.Total_Score:
				return false;
			default:
				return number != -1;
			}
		}
	}
}

