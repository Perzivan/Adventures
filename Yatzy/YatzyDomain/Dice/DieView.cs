using MonoTouch.CoreAnimation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Yatzy
{
	public class DieView : CALayer {

		public bool Enabled  { get;private set;}

		public void ToggleEnabled() {
			Enabled = !Enabled;
		} 

		public DieView(RectangleF rect) {
			Enabled = true;
			Bounds = new RectangleF (rect.Location.X,rect.Location.Y,50,50);
			Position = rect.Location;
			ContentsGravity = CALayer.GravityResizeAspectFill;
			Roll ();
		}

		public void MarkAsSelected(DieView die) {
			BorderColor = UIColor.Green.CGColor;
			BorderWidth = 2;
		}

		public void Roll() {
			if (!Enabled) {
				return;
			}
			DieImpl die = new DieImpl (1, 6);
			Contents = UIImage.FromBundle (die.Roll () + ".png").CGImage;
		}
	}
}


