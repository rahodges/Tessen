using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoBulkAttendanceEntryPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoBulkAttendanceEntryGrid1.ToolbarClicked += new ToolbarEventHandler(DojoBulkAttendanceEntryGrid1_ToolbarClicked);
			DojoBulkAttendanceEntryEditor1.Cancelled += new EventHandler(showGrid);
			DojoBulkAttendanceEntryEditor1.Updated += new EventHandler(showGrid);
			DojoBulkAttendanceEntryView1.OkClicked += new EventHandler(showGrid);
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
			DojoBulkAttendanceEntryGrid1.Visible = false;
			DojoBulkAttendanceEntryEditor1.Visible = false;
			DojoBulkAttendanceEntryView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoBulkAttendanceEntryGrid1.Visible = true;
			DojoBulkAttendanceEntryEditor1.Visible = false;
			DojoBulkAttendanceEntryView1.Visible = false;
		}

		private void DojoBulkAttendanceEntryGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoBulkAttendanceEntryEditor1.DojoBulkAttendanceEntryID = 0;
					DojoBulkAttendanceEntryEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoBulkAttendanceEntryView1.DojoBulkAttendanceEntryID = DojoBulkAttendanceEntryGrid1.SelectedID;
					DojoBulkAttendanceEntryView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoBulkAttendanceEntryEditor1.DojoBulkAttendanceEntryID = DojoBulkAttendanceEntryGrid1.SelectedID;
					DojoBulkAttendanceEntryEditor1.Visible = true;
					break;
			}
		}
	}
}
