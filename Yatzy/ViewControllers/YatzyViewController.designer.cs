// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace AwesomeYatzy
{
	[Register ("YatzyViewController")]
	partial class YatzyViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton ReplacementButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Roll { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton RollButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
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
