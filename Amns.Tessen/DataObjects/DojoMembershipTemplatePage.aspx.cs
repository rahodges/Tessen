using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoMembershipTemplatePage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoMembershipTemplateGrid1.ToolbarClicked += new ToolbarEventHandler(DojoMembershipTemplateGrid1_ToolbarClicked);
			DojoMembershipTemplateEditor1.Cancelled += new EventHandler(showGrid);
			DojoMembershipTemplateEditor1.Updated += new EventHandler(showGrid);
			DojoMembershipTemplateView1.OkClicked += new EventHandler(showGrid);
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
			DojoMembershipTemplateGrid1.Visible = false;
			DojoMembershipTemplateEditor1.Visible = false;
			DojoMembershipTemplateView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoMembershipTemplateGrid1.Visible = true;
			DojoMembershipTemplateEditor1.Visible = false;
			DojoMembershipTemplateView1.Visible = false;
		}

		private void DojoMembershipTemplateGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoMembershipTemplateEditor1.DojoMembershipTemplateID = 0;
					DojoMembershipTemplateEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoMembershipTemplateView1.DojoMembershipTemplateID = DojoMembershipTemplateGrid1.SelectedID;
					DojoMembershipTemplateView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoMembershipTemplateEditor1.DojoMembershipTemplateID = DojoMembershipTemplateGrid1.SelectedID;
					DojoMembershipTemplateEditor1.Visible = true;
					break;
			}
		}
	}
}
