using MonoTouch.CoreAnimation;
using MonoTouch.UIKit;
using System.Drawing;

namespace AwesomeYatzy
{
	public class DieView : CALayer {

		private const string FileEnding = ".png";
		public bool Enabled  {get;private set;}
		public DieImpl Die {get;private set;}

		public DieView(RectangleF rect) {
			Enabled = true;
			Position = rect.Location;
			const int lowRoll = 1;
			const int highRoll = 6;
			Die = new DieImpl (lowRoll, highRoll);
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

		public void MarkAsSelected() {
			BorderColor = UIColor.Green.CGColor;
			BorderWidth = 2;
			CornerRadius = 5;
			Enabled = false;
		}

		public void Unselect() {
			BorderWidth = 0;
			CornerRadius = 0;
			Enabled = true;
		}

		public void Roll() {
			if (!Enabled) {
				return;
			}
			Contents = UIImage.FromBundle (Die.Roll () + FileEnding).CGImage;
		}
	}
}


