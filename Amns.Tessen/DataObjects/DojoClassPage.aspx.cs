using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoClassPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoClassGrid1.ToolbarClicked += new ToolbarEventHandler(DojoClassGrid1_ToolbarClicked);
			DojoClassEditor1.Cancelled += new EventHandler(showGrid);
			DojoClassEditor1.Updated += new EventHandler(showGrid);
			DojoClassView1.OkClicked += new EventHandler(showGrid);
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
			DojoClassGrid1.Visible = false;
			DojoClassEditor1.Visible = false;
			DojoClassView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoClassGrid1.Visible = true;
			DojoClassEditor1.Visible = false;
			DojoClassView1.Visible = false;
		}

		private void DojoClassGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoClassEditor1.DojoClassID = 0;
					DojoClassEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoClassView1.DojoClassID = DojoClassGrid1.SelectedID;
					DojoClassView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoClassEditor1.DojoClassID = DojoClassGrid1.SelectedID;
					DojoClassEditor1.Visible = true;
					break;
			}
		}
	}
}
