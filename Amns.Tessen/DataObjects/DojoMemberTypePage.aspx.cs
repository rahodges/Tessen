using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoMemberTypePage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoMemberTypeGrid1.ToolbarClicked += new ToolbarEventHandler(DojoMemberTypeGrid1_ToolbarClicked);
			DojoMemberTypeEditor1.Cancelled += new EventHandler(showGrid);
			DojoMemberTypeEditor1.Updated += new EventHandler(showGrid);
			DojoMemberTypeView1.OkClicked += new EventHandler(showGrid);
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
			DojoMemberTypeGrid1.Visible = false;
			DojoMemberTypeEditor1.Visible = false;
			DojoMemberTypeView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoMemberTypeGrid1.Visible = true;
			DojoMemberTypeEditor1.Visible = false;
			DojoMemberTypeView1.Visible = false;
		}

		private void DojoMemberTypeGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoMemberTypeEditor1.DojoMemberTypeID = 0;
					DojoMemberTypeEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoMemberTypeView1.DojoMemberTypeID = DojoMemberTypeGrid1.SelectedID;
					DojoMemberTypeView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoMemberTypeEditor1.DojoMemberTypeID = DojoMemberTypeGrid1.SelectedID;
					DojoMemberTypeEditor1.Visible = true;
					break;
			}
		}
	}
}
