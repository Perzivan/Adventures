using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace AwesomeYatzy
{
	public partial class StartViewController : UIViewController
	{
		private UIButton AddPlayers { get;set;}
		private UIButton AddPlayer { get;set;}
		private UIButton StartGame { get;set;}
		private UIButton Reset { get;set;}
		private CustomTextView NameEntryElement {get;set;}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			UIImage image = UIImage.FromBundle ("FirstScreenBackground.png");
			image = image.Scale(new SizeF (302, 305));

			View.BackgroundColor = UIColor.FromPatternImage (image);

			AddGameControls ();
		}

		private void AddGameControls() {
			SetupAddPlayersButton ();
			NameEntryElement = new CustomTextView (new RectangleF (55, 150, 100, 30));
			NameEntryElement.Text = "Enter a name";

			NameEntryElement.Started += (object sender, EventArgs e) => NameEntryElement.Text = "";

			NameEntryElement.BackgroundColor = Common.GetYatzyBackgroundUIColor ();
			SetUpAddPlayerButton ();
		}

		private void SetupAddPlayersButton() {
			UIButton addPlayers;
			SetupGameButton (out addPlayers, new RectangleF (115, 203, 100, 30),"Add Players",true);
			AddPlayers = addPlayers;

			AddPlayers.TouchUpInside += delegate (object sender, EventArgs e) {
				Add (NameEntryElement);
				Add (AddPlayer);
				AddPlayers.Hidden = true;
			};
		}

		private void SetupResetButton() {
			UIButton reset;
			SetupGameButton (out reset, new RectangleF (115, 243, 100, 30),"Reset",true);
			Reset = reset;
			Reset.TouchUpInside += (object sender, EventArgs e) => {
				TabBarController.ViewControllers = new UIViewController[] {
					this
				};
				StartGame.Hidden = true;
				Reset.Hidden = true;
			};
		}

		private void SetUpAddPlayerButton() {

			UIButton addPlayer;
			SetupGameButton (out addPlayer, new RectangleF (165, 150, 50, 30),"Add",false);
			AddPlayer = addPlayer;
			AddPlayer.TouchUpInside += PlayerWasAdded;
		}

		private void SetupStartGameButton() {
			if (StartGame != null) {
				StartGame.Hidden = false;
				return;
			} 
			UIButton startGame;
			SetupGameButton (out startGame, new RectangleF (115, 203, 100, 30), "Start Game", true);
			StartGame = startGame;
			StartGame.TouchUpInside += (object sender, EventArgs e) =>{ 
				HideStartControls ();
			};
		}

		private void AddViewControllerToTabBar(UIViewController viewController) {
			List<UIViewController> controllerList;
			if (TabBarController.ViewControllers != null) {
				controllerList = new List<UIViewController> (TabBarController.ViewControllers);
			}
			else {
				controllerList = new List<UIViewController> ();
			}
			controllerList.Add (viewController);

			controllerList.ForEach(controller=>controller.TabBarItem.Enabled = false);

			TabBarController.ViewControllers = controllerList.ToArray ();
		}

		private void PlayerWasAdded(object sender, EventArgs e)
		{
			const string helpText = "Enter a name";
			string nameText = NameEntryElement.Text;

			if (!string.IsNullOrEmpty (nameText) && nameText != helpText) {
				YatzyViewController viewController = new YatzyViewController (nameText);
				AddViewControllerToTabBar (viewController);
				SetupStartGameButton ();
				SetupResetButton ();

				const int maxNumberOfPlayers = 5;
				if (TabBarController.ViewControllers.Length >= maxNumberOfPlayers) {
					HideStartControls ();
				}
			}

			NameEntryElement.Text = helpText;
			NameEntryElement.EndEditing (true);
		}

		private void HideStartControls() {
			AddPlayers.Hidden = true;
			AddPlayer.Hidden = true;
			StartGame.Hidden = true;
			NameEntryElement.Hidden = true;
			RemoveFromTabBarController ();
		}

		private void RemoveFromTabBarController() {
			var list = from view in new List<UIViewController> (TabBarController.ViewControllers)
				where view != this select view;
			TabBarController.ViewControllers = list.ToArray ();

		}

		private void SetupGameButton(out UIButton button,RectangleF rectangle, string text, bool addToView) {
			button =  new UIButton (UIButtonType.RoundedRect);
			button.Frame = rectangle;
			button.SetTitle (text, UIControlState.Normal);
			button.SetTitleColor (UIColor.White, UIControlState.Normal);
			button.BackgroundColor = Common.GetYatzyButtonUIColor ();
			if (addToView) {
				Add (button);
			}			
		}
	}
}

