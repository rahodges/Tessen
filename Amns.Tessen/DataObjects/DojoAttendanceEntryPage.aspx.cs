using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoAttendanceEntryPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoAttendanceEntryGrid1.ToolbarClicked += new ToolbarEventHandler(DojoAttendanceEntryGrid1_ToolbarClicked);
			DojoAttendanceEntryEditor1.Cancelled += new EventHandler(showGrid);
			DojoAttendanceEntryEditor1.Updated += new EventHandler(showGrid);
			DojoAttendanceEntryView1.OkClicked += new EventHandler(showGrid);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void resetControls()
		{
			DojoAttendanceEntryGrid1.Visible = false;
			DojoAttendanceEntryEditor1.Visible = false;
			DojoAttendanceEntryView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoAttendanceEntryGrid1.Visible = true;
			DojoAttendanceEntryEditor1.Visible = false;
			DojoAttendanceEntryView1.Visible = false;
		}

		private void DojoAttendanceEntryGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoAttendanceEntryEditor1.DojoAttendanceEntryID = 0;
					DojoAttendanceEntryEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoAttendanceEntryView1.DojoAttendanceEntryID = DojoAttendanceEntryGrid1.SelectedID;
					DojoAttendanceEntryView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoAttendanceEntryEditor1.DojoAttendanceEntryID = DojoAttendanceEntryGrid1.SelectedID;
					DojoAttendanceEntryEditor1.Visible = true;
					break;
			}
		}
	}
}
