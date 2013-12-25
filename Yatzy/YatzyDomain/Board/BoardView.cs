using System;
using MonoTouch.UIKit;
using DSoft.Datatypes.Grid.Data;
using DSoft.UI.Grid;
using System.Drawing;

namespace Yatzy
{
	//Use action pack instead?
	public class GridFormatter : DSoft.Datatypes.Base.DSFormatter {

		public override DSoft.Datatypes.Types.DSSize Size { get; set;}

		public GridFormatter() {
			Size = new DSoft.Datatypes.Types.DSSize (5, 5);
		}
	}

	public class BoardView : DSGridView
	{
		public BoardView () : base (new RectangleF (0, 20, 318, 345))
		{

			BackgroundColor = UIColor.Brown;

			//create the datatable object and set a name
			DSDataTable aDataSource = new DSDataTable("ADT");
	
			GridFormatter formatter = new GridFormatter ();

			//add a column
			DSDataColumn title = new DSDataColumn("Title");
			title.Caption = "Yatzy";
			title.ReadOnly = true;
			title.DataType = typeof(String);
			title.AllowSort = false;
			title.Width = 70;
			title.Formatter = new GridFormatter();
		
			aDataSource.Columns.Add(title);

			var player = new DSDataColumn("player");
			player.Caption = "JEO";
			player.ReadOnly = false;
			player.DataType = typeof(String);
			player.AllowSort = false;
			player.Width = 58;
			player.Formatter = formatter;
			aDataSource.Columns.Add(player);		


			string[] scoreNames = Enum.GetNames (typeof(Common.ScoreType));

			for (int i = 0; i < scoreNames.Length; i++) {
				//add a row to the datatable
				string columnName = scoreNames [i];
				var dr = new DSDataRow ();
				dr ["ID"] = i;
				dr ["Title"] = columnName;
				dr ["Description"] = columnName;
				dr ["Date"] = DateTime.Now.ToShortDateString ();
				dr ["Value"] = "10000.00";
	
				dr ["ID"] = i;
				dr ["player"] = 0;
				dr ["Description"] = columnName;
				dr ["Date"] = DateTime.Now.ToShortDateString ();
				dr ["Value"] = "10000.00";
	
				aDataSource.Rows.Add (dr);
			}

			DataSource = aDataSource;
			//Create the grid view, assign the datasource and add the view as a subview


		}
	}
}

