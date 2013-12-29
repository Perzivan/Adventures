using System.Drawing;
using Appracatappra.ActionComponents.ActionTable;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using System.Linq;
using System;
using System.Threading.Tasks;
using Appracatappra.ActionComponents.ActionAlert;
using MonoTouch.CoreAnimation;


namespace AwesomeYatzy
{
	public partial class YatzyViewController : UIViewController
	{
		readonly List<DieView> DiceViewList = new List<DieView> ();
		ActionBoardView YatzyTable;
		private int YatzyTurn { get; set;}
		private const int MaxTurns = 3;
		private string PlayerName { get; set;}

		public YatzyViewController (string playerName) : base ("YatzyViewController", null)
		{
			PlayerName = PlayerName;
			Title = NSBundle.MainBundle.LocalizedString (playerName, playerName);
			TabBarItem.Image = UIImage.FromBundle ("first");
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

		void TableItemSelected (UIActionTableItem item)
		{
			Common.ScoreType type = (Common.ScoreType)Enum.Parse (typeof(Common.ScoreType), Common.AddUnderScores (item.text));
			if (!CanSelect ()) {
				return;
			}
			CheckScore check = new CheckScore ();
			List<Die> diceList = new List<Die> ();
			DiceViewList.ForEach (dieView => diceList.Add (dieView.Die));
			int sum = check.SumScoreForType (diceList, type);
			string playerName = YatzyTable.Board.CurrentPlayer.Name;
			if (YatzyTable.Board.IsUnpopulated (playerName, type)) {
				PopulateValue (playerName, type, sum);
				YatzyTable.Board.SetSum (playerName);
				if (YatzyTable.Board.HasBonus (playerName)) {
					YatzyTable.Board.SetBonus (playerName);
				}
				YatzyTable.ComponentTable.LoadData ();

				RollButton.SetTitle ("Press to roll!", UIControlState.Normal);

				if (!CanSelect ()) {
					//Switch Player and disable all players other than the one that is currently playing.
					ChangePlayerDirty (playerName);
				}
			}
		}
		void RollTheDice (object sender, EventArgs e)
		{
			string playerName = YatzyTable.Board.CurrentPlayer.Name;
			if (YatzyTable.Board.HasRoundsLeft (playerName)) {
				PrepareRoll ();

				if (YatzyTurn == MaxTurns) {
					RollButton.SetTitle ("Select score category!", UIControlState.Normal);
				}

				DiceViewList.ForEach (DoRoll);
				SetPresentRollNumber ();
			}
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			SetupControllers ();
			ResetForNextTurn ();
			SetPresentRollNumber ();
		}

		private void SetupControllers () {
			CreateYatzyDies ();

			foreach(DieView die in DiceViewList) {
				View.Layer.AddSublayer (die);
			}

			//Disable all tabs except the first
			for (int i = 0; i < TabBarController.ViewControllers.Length; i++) {
				TabBarController.ViewControllers [i].TabBarItem.Enabled = i == 0;
			}

			List<string> players = new List<string> ();
			players.Add ("Player");

			YatzyTable = new ActionBoardView (players);
			View.AddSubview (YatzyTable.ComponentTable.TableView);

			AdjustToScreenSize (RollButton);
			AdjustToScreenSize (ReplacementButton);
			AdjustToScreenSize (PresentRollNumber);

			RollButton.TouchUpInside += RollTheDice;

			YatzyTable.ComponentTable.ItemsSelected += TableItemSelected;		
		}

		private void AdjustToScreenSize(UIView view) {
			RectangleF rectangle =view.Frame;
			rectangle.Y = UIScreen.MainScreen.ApplicationFrame.Height- 124;
			view.Frame = rectangle;		
		}

		private void ChangePlayerDirty(string playerName) {
			RectangleF startFrame = RollButton.Frame;
			//A superdirty way to win some time before the change of players 
			//so that the player has a chance to see the score. 
			UIView.Animate (0.5, 0.2, UIViewAnimationOptions.TransitionNone, ()=>{
				RollButton.Hidden = true;
				ReplacementButton.Hidden = false;
				RollButton.Frame = new RectangleF(startFrame.X-50,startFrame.Y-50,startFrame.Width,startFrame.Height);
			}, () =>{ 
				RollButton.Frame = startFrame;
				ReplacementButton.Hidden = true;
				RollButton.Hidden = false;
				MoveToNext(playerName);
			});	

				
		}

		private void MoveToNext(string playerName) {
			UIViewController selected = TabBarController.SelectedViewController;

			if (YatzyTable.Board.HasRoundsLeft (playerName)) {
				selected.TabBarItem.Enabled = false;
			} else {
				selected.TabBarItem.Enabled = true;
			}

			//select the next controller. 
			if (TabBarController.SelectedIndex < TabBarController.ViewControllers.Count () - 1) {
				TabBarController.SelectedIndex += 1;
			} else {
				TabBarController.SelectedIndex = 0;
			}
			TabBarController.SelectedViewController.TabBarItem.Enabled = true;	
		}

		private bool CanSelect() {
			return YatzyTurn > 0;	
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
			} else if(YatzyTurn <= MaxTurns) {
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
	}
}

