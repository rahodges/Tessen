using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoTestListStatusPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoTestListStatusGrid1.ToolbarClicked += new ToolbarEventHandler(DojoTestListStatusGrid1_ToolbarClicked);
			DojoTestListStatusEditor1.Cancelled += new EventHandler(showGrid);
			DojoTestListStatusEditor1.Updated += new EventHandler(showGrid);
			DojoTestListStatusView1.OkClicked += new EventHandler(showGrid);
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
			DojoTestListStatusGrid1.Visible = false;
			DojoTestListStatusEditor1.Visible = false;
			DojoTestListStatusView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoTestListStatusGrid1.Visible = true;
			DojoTestListStatusEditor1.Visible = false;
			DojoTestListStatusView1.Visible = false;
		}

		private void DojoTestListStatusGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoTestListStatusEditor1.DojoTestListStatusID = 0;
					DojoTestListStatusEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoTestListStatusView1.DojoTestListStatusID = DojoTestListStatusGrid1.SelectedID;
					DojoTestListStatusView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoTestListStatusEditor1.DojoTestListStatusID = DojoTestListStatusGrid1.SelectedID;
					DojoTestListStatusEditor1.Visible = true;
					break;
			}
		}
	}
}
