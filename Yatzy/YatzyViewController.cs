using System.Drawing;
using Appracatappra.ActionComponents.ActionTable;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using System.Linq;
using System;


namespace Yatzy
{
	public partial class YatzyViewController : UIViewController
	{
		readonly List<DieView> DiceViewList = new List<DieView> ();
		ActionBoardView YatzyTable;
		private int YatzyTurn { get; set;}
		private const int MaxTurns = 3;

		public YatzyViewController () : base ("YatzyViewController", null)
		{
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);
			// get the touch
			UITouch touch = touches.AnyObject as UITouch;

			if (touch != null) {
				PointF Location = touch.LocationInView(View);

				foreach(DieView die in DiceViewList) {
					if (die.Frame.Contains (Location)) {
						die.ToggleSelect ();
					}
				}
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			CreateYatzyDies ();

			foreach(DieView die in DiceViewList) {
				View.Layer.AddSublayer (die);
			}

			List<string> players = new List<string> ();
			players.Add ("Maria");

			YatzyTable = new ActionBoardView (players);
			View.AddSubview (YatzyTable.ComponentTable.TableView);

			RollButton.TouchUpInside += (sender, e) => {
				string playerName = YatzyTable.Board.CurrentPlayer.Name;

				if(YatzyTable.Board.HasRoundsLeft(playerName)) {
					PrepareRoll();
					DiceViewList.ForEach (DoRoll);
					SetPresentRollNumber();
				}
			};

			YatzyTable.ComponentTable.ItemsSelected += delegate(UIActionTableItem item)
			{
				Common.ScoreType type = (Common.ScoreType)Enum.Parse (typeof(Common.ScoreType), AddUnderScores(item.text));

				if(!IsSelectable(type)) 
				{
					return;
				}

				CheckScore check = new CheckScore ();
				List<Die> diceList = new List<Die> ();
				DiceViewList.ForEach (dieView => diceList.Add (dieView.Die));
				int sum = check.SumScoreForType (diceList,type);

				string playerName = YatzyTable.Board.CurrentPlayer.Name;

				if(YatzyTable.Board.IsUnpopulated(playerName,type)) {
					PopulateValue(playerName,type,sum);
					YatzyTable.Board.SetSum(playerName);
	
					if(YatzyTable.Board.HasBonus(playerName)) {
						YatzyTable.Board.SetBonus(playerName);
					}

					YatzyTable.ComponentTable.LoadData();	
				}
			};

			ResetForNextTurn ();
			SetPresentRollNumber ();
		}

		private bool IsSelectable(Common.ScoreType type) {
			return YatzyTurn > 0 && type != Common.ScoreType.Sum && type != Common.ScoreType.Bonus;	
		}

		private void PopulateValue(string playerName, Common.ScoreType type, int sum) {
			YatzyTable.Board.SetScore(playerName,sum,type);
			ResetForNextTurn();
			SetPresentRollNumber ();
		}

		private void HideDies() {
			DiceViewList.ForEach(dice=>dice.Hidden = true);
		}

		private void ShowDies() {
			DiceViewList.ForEach(dice=>dice.Hidden = false);
		}

		private void SetPresentRollNumber() {
			if (YatzyTurn == 0) {
				PresentRollNumber.Text = string.Empty;
			} else if(YatzyTurn <= 3) {
				PresentRollNumber.Text = YatzyTurn.ToString ();
			}
		}

		private void DoRoll(DieView die) {
			if (YatzyTurn <= MaxTurns) {
				die.Roll ();
			} 	
		}

		private void PrepareRoll() {
			if (YatzyTurn <= MaxTurns) {
				YatzyTurn = YatzyTurn + 1;
				ShowDies ();
			} 
		}

		private void ResetForNextTurn() {
			YatzyTurn = 0;
			DiceViewList.ForEach(dice=>dice.Unselect());
			HideDies ();
		}

		private string AddUnderScores(string text) {
			return text.Replace (" ", "_");
		}

		//Todo: Maybe I should put this somewhere else. 
		private void CreateYatzyDies() {
			DiceViewList.Add(new DieView (MakeRectangle(40,430)));
			DiceViewList.Add(new DieView (MakeRectangle(100,430)));
			DiceViewList.Add(new DieView (MakeRectangle(160,430)));
			DiceViewList.Add(new DieView (MakeRectangle(220,430)));
			DiceViewList.Add(new DieView (MakeRectangle(280,430)));
			HideDies ();
		}

		private RectangleF MakeRectangle(int x, int y) {
			PointF location = new PointF (x, y);
			SizeF size = new SizeF (50, 50);
			return new RectangleF (location, size);		
		}
	}
}

