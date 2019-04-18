using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoTestListJournalEntryTypePage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoTestListJournalEntryTypeGrid1.ToolbarClicked += new ToolbarEventHandler(DojoTestListJournalEntryTypeGrid1_ToolbarClicked);
			DojoTestListJournalEntryTypeEditor1.Cancelled += new EventHandler(showGrid);
			DojoTestListJournalEntryTypeEditor1.Updated += new EventHandler(showGrid);
			DojoTestListJournalEntryTypeView1.OkClicked += new EventHandler(showGrid);
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
			DojoTestListJournalEntryTypeGrid1.Visible = false;
			DojoTestListJournalEntryTypeEditor1.Visible = false;
			DojoTestListJournalEntryTypeView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoTestListJournalEntryTypeGrid1.Visible = true;
			DojoTestListJournalEntryTypeEditor1.Visible = false;
			DojoTestListJournalEntryTypeView1.Visible = false;
		}

		private void DojoTestListJournalEntryTypeGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoTestListJournalEntryTypeEditor1.DojoTestListJournalEntryTypeID = 0;
					DojoTestListJournalEntryTypeEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoTestListJournalEntryTypeView1.DojoTestListJournalEntryTypeID = DojoTestListJournalEntryTypeGrid1.SelectedID;
					DojoTestListJournalEntryTypeView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoTestListJournalEntryTypeEditor1.DojoTestListJournalEntryTypeID = DojoTestListJournalEntryTypeGrid1.SelectedID;
					DojoTestListJournalEntryTypeEditor1.Visible = true;
					break;
			}
		}
	}
}
