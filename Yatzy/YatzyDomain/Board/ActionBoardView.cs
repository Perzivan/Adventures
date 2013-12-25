using Appracatappra.ActionComponents.ActionTable;
using System.Drawing;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace Yatzy
{
	public class ActionBoardView : UIView
	{

		public UIActionTableViewController ComponentTable { get; private set;}
		public ScoreBoard Board { get; private set;}


		public ActionBoardView (List<string> players)
		{
			Board = new ScoreBoard (players);
			ComponentTable = new UIActionTableViewController (UITableViewStyle.Grouped,new RectangleF (0, 20, 318, 310));

			ComponentTable.dataSource.RequestData += (dataSource) => {
				UIActionTableSection section = new UIActionTableSection("Yatzy " + Board.CurrentPlayer.Name ,"");

				string[] scoreNames = Enum.GetNames (typeof(Common.ScoreType));

				for (int i = 0; i < scoreNames.Length; i++) {
					Common.ScoreType type = (Common.ScoreType)Enum.Parse (typeof(Common.ScoreType), scoreNames [i]);
					int score = Board.GetDisplayScore(Board.CurrentPlayer.Name,type);
					string scoreText = score.ToString();
					string scoreName = scoreNames[i].Replace("_", " ");

					int rawScore =  Board.GetRawScore(Board.CurrentPlayer.Name,type);

					if(IsCheckMarkItem(rawScore,type)) {
						scoreText += " " + ((char)0x221A);
					}

					section.AddItem(scoreName,scoreText,true);
				}

				dataSource.AddSection(section);
			};

			ComponentTable.LoadData ();
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

