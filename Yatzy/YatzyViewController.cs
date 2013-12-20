using System.Drawing;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;


namespace Yatzy
{
	public partial class YatzyViewController : UIViewController
	{
		readonly List<DieView> Dice = new List<DieView> ();

		public YatzyViewController () : base ("YatzyViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();	
			// Release any cached data, images, etc that aren't in use.
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);
			// get the touch
			UITouch touch = touches.AnyObject as UITouch;

			if (touch != null) {
				PointF Location = touch.LocationInView(View);

				foreach(DieView die in Dice) {
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

			foreach(DieView die in Dice) {
				View.Layer.AddSublayer (die);
			}

			Roll.TouchUpInside += (sender, e) => Dice.ForEach(die=>die.Roll());
		}

		private void CreateYatzyDies() {
			Dice.Add(new DieView (MakeRectangle(40,400)));
			Dice.Add(new DieView (MakeRectangle(100,400)));
			Dice.Add(new DieView (MakeRectangle(160,400)));
			Dice.Add(new DieView (MakeRectangle(220,400)));
			Dice.Add(new DieView (MakeRectangle(280,400)));		
		}

		private RectangleF MakeRectangle(int x, int y) {
			PointF location = new PointF (x, y);
			SizeF size = new SizeF (50, 50);
			return new RectangleF (location, size);		
		}
	}
}

