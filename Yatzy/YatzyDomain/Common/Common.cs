using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace AwesomeYatzy
{
	public class Common
	{
		public enum ScoreType {
			Ones = 0,
			Twos = 1,
			Threes = 2,
			Fours = 3,
			Fives = 4,
			Sixes = 5,
			Sum = 6,
			Bonus = 7,
			One_Pair = 8,
			Two_Pairs = 9,
			Three_Of_A_Kind = 10,
			Four_Of_A_Kind = 11,
			Small_Straight = 12,
			Big_Straight = 13,
			Full_House = 14,
			Chance = 15,
			Yatzy = 16,
			Total_Score = 17
		}

		public static string AddUnderScores(string text) {
			return text.Replace (" ", "_");
		}

		public static string RemoveUnderScores(string text) {
			return text.Replace ("_", " ");
		}

		public static UIColor GetYatzyButtonUIColor() {
			return UIColor.FromRGBA (125, 170, 167, 255); 
		}

		public static UIColor GetAlternateYatzyBackgroundUIColor() {
			return UIColor.FromPatternImage(UIImage.FromBundle ("TabItemImage"));
		}

		public static UIColor GetYatzyBackgroundUIColor() {
			return UIColor.FromRGBA (238, 223, 161, 255); 
		}
	}
}

