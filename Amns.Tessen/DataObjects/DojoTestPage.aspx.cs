using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoTestPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoTestGrid1.ToolbarClicked += new ToolbarEventHandler(DojoTestGrid1_ToolbarClicked);
			DojoTestEditor1.Cancelled += new EventHandler(showGrid);
			DojoTestEditor1.Updated += new EventHandler(showGrid);
			DojoTestView1.OkClicked += new EventHandler(showGrid);
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
			DojoTestGrid1.Visible = false;
			DojoTestEditor1.Visible = false;
			DojoTestView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoTestGrid1.Visible = true;
			DojoTestEditor1.Visible = false;
			DojoTestView1.Visible = false;
		}

		private void DojoTestGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoTestEditor1.DojoTestID = 0;
					DojoTestEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoTestView1.DojoTestID = DojoTestGrid1.SelectedID;
					DojoTestView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoTestEditor1.DojoTestID = DojoTestGrid1.SelectedID;
					DojoTestEditor1.Visible = true;
					break;
			}
		}
	}
}
