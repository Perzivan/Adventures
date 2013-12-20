using MonoTouch.CoreAnimation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Yatzy
{
	public class DieView : CALayer {

		public bool Enabled  {get;private set;}
		private const string FileEnding = ".png";

		public DieView(RectangleF rect) {
			Enabled = true;
			Position = rect.Location;
			Bounds = new RectangleF (rect.Location.X,rect.Location.Y,50,50);
			ContentsGravity = CALayer.GravityResizeAspectFill;
			Roll ();
		}

		public void ToggleSelect() {
			if (Enabled) {
				MarkAsSelected ();
			} else {
				Unselect ();
			}
		}

		private void MarkAsSelected() {
			BorderColor = UIColor.Green.CGColor;
			BorderWidth = 2;
			Enabled = false;
		}

		private void Unselect() {
			BorderWidth = 0;
			Enabled = true;
		}

		public void Roll() {
			if (!Enabled) {
				return;
			}

			const int lowRoll = 1;
			const int highRoll = 6;
			DieImpl die = new DieImpl (lowRoll, highRoll);
			Contents = UIImage.FromBundle (die.Roll () + FileEnding).CGImage;
		}
	}
}


