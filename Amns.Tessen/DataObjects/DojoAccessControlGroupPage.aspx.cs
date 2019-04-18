using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoAccessControlGroupPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoAccessControlGroupGrid1.ToolbarClicked += new ToolbarEventHandler(DojoAccessControlGroupGrid1_ToolbarClicked);
			DojoAccessControlGroupEditor1.Cancelled += new EventHandler(showGrid);
			DojoAccessControlGroupEditor1.Updated += new EventHandler(showGrid);
			DojoAccessControlGroupView1.OkClicked += new EventHandler(showGrid);
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
			DojoAccessControlGroupGrid1.Visible = false;
			DojoAccessControlGroupEditor1.Visible = false;
			DojoAccessControlGroupView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoAccessControlGroupGrid1.Visible = true;
			DojoAccessControlGroupEditor1.Visible = false;
			DojoAccessControlGroupView1.Visible = false;
		}

		private void DojoAccessControlGroupGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoAccessControlGroupEditor1.DojoAccessControlGroupID = 0;
					DojoAccessControlGroupEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoAccessControlGroupView1.DojoAccessControlGroupID = DojoAccessControlGroupGrid1.SelectedID;
					DojoAccessControlGroupView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoAccessControlGroupEditor1.DojoAccessControlGroupID = DojoAccessControlGroupGrid1.SelectedID;
					DojoAccessControlGroupEditor1.Visible = true;
					break;
			}
		}
	}
}
