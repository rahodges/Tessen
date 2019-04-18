using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoMemberPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoMemberGrid1.ToolbarClicked += new ToolbarEventHandler(DojoMemberGrid1_ToolbarClicked);
			DojoMemberEditor1.Cancelled += new EventHandler(showGrid);
			DojoMemberEditor1.Updated += new EventHandler(showGrid);
			DojoMemberView1.OkClicked += new EventHandler(showGrid);
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
			DojoMemberGrid1.Visible = false;
			DojoMemberEditor1.Visible = false;
			DojoMemberView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoMemberGrid1.Visible = true;
			DojoMemberEditor1.Visible = false;
			DojoMemberView1.Visible = false;
		}

		private void DojoMemberGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoMemberEditor1.DojoMemberID = 0;
					DojoMemberEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoMemberView1.DojoMemberID = DojoMemberGrid1.SelectedID;
					DojoMemberView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoMemberEditor1.DojoMemberID = DojoMemberGrid1.SelectedID;
					DojoMemberEditor1.Visible = true;
					break;
			}
		}
	}
}
