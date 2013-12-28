// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Yatzy
{
	[Register ("YatzyViewController")]
	partial class YatzyViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel PresentRollNumber { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ReplacementButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Roll { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton RollButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PresentRollNumber != null) {
				PresentRollNumber.Dispose ();
				PresentRollNumber = null;
			}

			if (ReplacementButton != null) {
				ReplacementButton.Dispose ();
				ReplacementButton = null;
			}

			if (Roll != null) {
				Roll.Dispose ();
				Roll = null;
			}

			if (RollButton != null) {
				RollButton.Dispose ();
				RollButton = null;
			}
		}
	}
}
