using Appracatappra.ActionComponents.ActionTable;
using System.Collections.Generic;
using MonoTouch.CoreAnimation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using System.Linq;
using System;

namespace AwesomeYatzy
{
	public partial class YatzyViewController : UIViewController
	{
		private readonly YatzyComponents Components = null;

		public YatzyViewController (string playerName) : base ("YatzyViewController", null)
		{
			Components = new YatzyComponents(playerName);
			Title = NSBundle.MainBundle.LocalizedString (playerName, playerName);

			UIImage image =  UIImage.FromBundle ("TabItemImage");
			TabBarItem.SetFinishedImages(image,image);
			TabBarItem.Image = image;
			TabBarItem.SelectedImage = image;

			UITextAttributes attributes = new UITextAttributes ();
			attributes.TextColor = UIColor.Brown;
			TabBarItem.SetTitleTextAttributes (attributes,UIControlState.Normal);
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);
			// get the touch
			UITouch touch = touches.AnyObject as UITouch;

			if (touch != null) {
				PointF Location = touch.LocationInView(View);

				foreach(DieView die in Components.DiceViewList) {
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

			Components.DiceViewList.ForEach (dieView => diceList.Add (dieView.Die));
			int sum = check.SumScoreForType (diceList, type);

			string playerName = Components.YatzyTable.Board.CurrentPlayer.Name;

			if (Components.YatzyTable.Board.IsUnpopulated (playerName, type)) {
				Components.PopulateValue (playerName, type, sum);
				Components.YatzyTable.Board.SetSum (playerName);

				if (Components.YatzyTable.Board.HasBonus (playerName)) {
					Components.YatzyTable.Board.SetBonus (playerName);
				}

				Components.YatzyTable.ComponentTable.LoadData ();
				RollButton.SetTitle ("Press to roll!", UIControlState.Normal);

				if (!CanSelect ()) {
					//Switch Player and disable all players other than the one that is currently playing.
					ChangePlayerDirty (playerName);
				}
			}
		}
		void RollTheDice (object sender, EventArgs e)
		{
			string playerName = Components.YatzyTable.Board.CurrentPlayer.Name;

			if (Components.YatzyTable.Board.HasRoundsLeft (playerName)) {
				PrepareRoll ();

				if (Components.YatzyTurn == Components.GetMaxTurns()) {
					RollButton.SetTitle ("Select a score category!", UIControlState.Normal);
				}

				Components.DiceViewList.ForEach (DoRoll);
			}
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			SetupControllers ();
			Components.ResetForNextTurn ();
		}

		private void SetupControllers () {
			foreach(DieView die in Components.DiceViewList) {
				View.Layer.AddSublayer (die);
			}

			//Disable all tabs except the first
			for (int i = 0; i < TabBarController.ViewControllers.Length; i++) {
				TabBarController.ViewControllers [i].TabBarItem.Enabled = i == 0;
			}

			View.AddSubview (Components.YatzyTable.ComponentTable.TableView);

			Components.AdjustToScreenSize (RollButton);
			Components.AdjustToScreenSize (ReplacementButton);
			RollButton.TouchUpInside += RollTheDice;

			Components.YatzyTable.ComponentTable.ItemsSelected += TableItemSelected;		
		}

		private void ChangePlayerDirty(string playerName) {
			RectangleF startFrame = RollButton.Frame;
			//A superdirty way to win some time before the change of players 
			//so that the player has a chance to see the score. 
			UIView.Animate (0.3, 0, UIViewAnimationOptions.TransitionNone, ()=>{
				RollButton.Hidden = true;
				ReplacementButton.Hidden = false;
				RollButton.Frame = new RectangleF(startFrame.X-50,startFrame.Y-50,startFrame.Width,startFrame.Height);
			}, () =>{ 
				RollButton.Frame = startFrame;
				ReplacementButton.Hidden = true;
				RollButton.Hidden = false;
				MoveToNextPlayer(playerName);
			});	

				
		}

		private void MoveToNextPlayer(string playerName) {
			//Is this the last controller?
			if (TabBarController.SelectedIndex == TabBarController.ViewControllers.Count ()-1) {
				TabBarController.SelectedIndex = 0;
				//if the first player has no rounds left. Enable all. 
				if (!Components.YatzyTable.Board.HasRoundsLeft (playerName)) {
					EnableAll ();
					return;
				}
			} else {
				//Else move to the next. 
				TabBarController.SelectedIndex = TabBarController.SelectedIndex + 1;
			}
			DisableAllExcept (TabBarController.SelectedIndex);

		}

		private void EnableAll() {
			for (int i = 0; i < TabBarController.ViewControllers.Count(); i++) {
				TabBarController.ViewControllers [i].TabBarItem.Enabled = true;
			}		
		}

		private void DisableAllExcept(int enabledPosition) {
			for (int i = 0; i < TabBarController.ViewControllers.Count(); i++) {
				TabBarController.ViewControllers [i].TabBarItem.Enabled = i == enabledPosition;
			}
		}

		private bool CanSelect() {
			return Components.YatzyTurn > 0;	
		}

		private void DoRoll(DieView die) {
			if (Components.YatzyTurn <= Components.GetMaxTurns()) {
				die.Roll ();
			} 
		}

		private void PrepareRoll() {
			if (Components.YatzyTurn <= Components.GetMaxTurns()) {
				Components.YatzyTurn = Components.YatzyTurn + 1;

				if (Components.YatzyTurn < Components.GetMaxTurns ()) {
					string title = "Press to roll! (" + Components.YatzyTurn + " out of " + Components.GetMaxTurns () + ")";
					RollButton.SetTitle (title, UIControlState.Normal);
				}

				Components.ShowDies ();
			}
		}
	}
}

