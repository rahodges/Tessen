using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoMembershipPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoMembershipGrid1.ToolbarClicked += new ToolbarEventHandler(DojoMembershipGrid1_ToolbarClicked);
			DojoMembershipEditor1.Cancelled += new EventHandler(showGrid);
			DojoMembershipEditor1.Updated += new EventHandler(showGrid);
			DojoMembershipView1.OkClicked += new EventHandler(showGrid);
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
			DojoMembershipGrid1.Visible = false;
			DojoMembershipEditor1.Visible = false;
			DojoMembershipView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoMembershipGrid1.Visible = true;
			DojoMembershipEditor1.Visible = false;
			DojoMembershipView1.Visible = false;
		}

		private void DojoMembershipGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoMembershipEditor1.DojoMembershipID = 0;
					DojoMembershipEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoMembershipView1.DojoMembershipID = DojoMembershipGrid1.SelectedID;
					DojoMembershipView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoMembershipEditor1.DojoMembershipID = DojoMembershipGrid1.SelectedID;
					DojoMembershipEditor1.Visible = true;
					break;
			}
		}
	}
}
