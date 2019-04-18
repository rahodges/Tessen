using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoOrganizationPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoOrganizationGrid1.ToolbarClicked += new ToolbarEventHandler(DojoOrganizationGrid1_ToolbarClicked);
			DojoOrganizationEditor1.Cancelled += new EventHandler(showGrid);
			DojoOrganizationEditor1.Updated += new EventHandler(showGrid);
			DojoOrganizationView1.OkClicked += new EventHandler(showGrid);
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
			DojoOrganizationGrid1.Visible = false;
			DojoOrganizationEditor1.Visible = false;
			DojoOrganizationView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoOrganizationGrid1.Visible = true;
			DojoOrganizationEditor1.Visible = false;
			DojoOrganizationView1.Visible = false;
		}

		private void DojoOrganizationGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoOrganizationEditor1.DojoOrganizationID = 0;
					DojoOrganizationEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoOrganizationView1.DojoOrganizationID = DojoOrganizationGrid1.SelectedID;
					DojoOrganizationView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoOrganizationEditor1.DojoOrganizationID = DojoOrganizationGrid1.SelectedID;
					DojoOrganizationEditor1.Visible = true;
					break;
			}
		}
	}
}
