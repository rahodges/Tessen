using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoTestListJournalEntryPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoTestListJournalEntryGrid1.ToolbarClicked += new ToolbarEventHandler(DojoTestListJournalEntryGrid1_ToolbarClicked);
			DojoTestListJournalEntryEditor1.Cancelled += new EventHandler(showGrid);
			DojoTestListJournalEntryEditor1.Updated += new EventHandler(showGrid);
			DojoTestListJournalEntryView1.OkClicked += new EventHandler(showGrid);
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
			DojoTestListJournalEntryGrid1.Visible = false;
			DojoTestListJournalEntryEditor1.Visible = false;
			DojoTestListJournalEntryView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoTestListJournalEntryGrid1.Visible = true;
			DojoTestListJournalEntryEditor1.Visible = false;
			DojoTestListJournalEntryView1.Visible = false;
		}

		private void DojoTestListJournalEntryGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoTestListJournalEntryEditor1.DojoTestListJournalEntryID = 0;
					DojoTestListJournalEntryEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoTestListJournalEntryView1.DojoTestListJournalEntryID = DojoTestListJournalEntryGrid1.SelectedID;
					DojoTestListJournalEntryView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoTestListJournalEntryEditor1.DojoTestListJournalEntryID = DojoTestListJournalEntryGrid1.SelectedID;
					DojoTestListJournalEntryEditor1.Visible = true;
					break;
			}
		}
	}
}
