using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Yatzy
{
	[Register ("LimitedTextView")]
	public class CustomTextView : UITextView
	{
		public int MaxCharacters { get; set; }

		public CustomTextView() {
			Initialize ();
		}

		public CustomTextView(RectangleF frame) : base(frame) {
			Initialize ();
		}

		void Initialize ()
		{
			MaxCharacters = 16;
			ShouldChangeText = ShouldLimit;
			Changed += (object sender, EventArgs e) => {

				if(this.Text.Contains("\n")) {
					Text = Text.Replace("\n",string.Empty);
				}

				if(this.Text.Contains(" ")) {
					Text = Text.Replace(" ",string.Empty);
				}
			};

		}

		static bool ShouldLimit (UITextView view, NSRange range, string text)
		{
			var textView = (CustomTextView)view;
			var limit = textView.MaxCharacters;

			int newLength = (view.Text.Length - range.Length) + text.Length;
			if (newLength <= limit)
				return true;

			// This will clip pasted text to include as many characters as possible
			// See http://stackoverflow.com/a/5897912/458193

			var emptySpace = Math.Max (0, limit - (view.Text.Length - range.Length));
			var beforeCaret = view.Text.Substring (0, range.Location) + text.Substring (0, emptySpace);
			var afterCaret = view.Text.Substring (range.Location + range.Length);

			view.Text = beforeCaret + afterCaret;
			view.SelectedRange = new NSRange (beforeCaret.Length, 0);

			return false;
		}
	}
}

