using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoTestListPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoTestListGrid1.ToolbarClicked += new ToolbarEventHandler(DojoTestListGrid1_ToolbarClicked);
			DojoTestListEditor1.Cancelled += new EventHandler(showGrid);
			DojoTestListEditor1.Updated += new EventHandler(showGrid);
			DojoTestListView1.OkClicked += new EventHandler(showGrid);
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
			DojoTestListGrid1.Visible = false;
			DojoTestListEditor1.Visible = false;
			DojoTestListView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoTestListGrid1.Visible = true;
			DojoTestListEditor1.Visible = false;
			DojoTestListView1.Visible = false;
		}

		private void DojoTestListGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoTestListEditor1.DojoTestListID = 0;
					DojoTestListEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoTestListView1.DojoTestListID = DojoTestListGrid1.SelectedID;
					DojoTestListView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoTestListEditor1.DojoTestListID = DojoTestListGrid1.SelectedID;
					DojoTestListEditor1.Visible = true;
					break;
			}
		}
	}
}
