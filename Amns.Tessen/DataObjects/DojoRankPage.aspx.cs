using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoRankPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoRankGrid1.ToolbarClicked += new ToolbarEventHandler(DojoRankGrid1_ToolbarClicked);
			DojoRankEditor1.Cancelled += new EventHandler(showGrid);
			DojoRankEditor1.Updated += new EventHandler(showGrid);
			DojoRankView1.OkClicked += new EventHandler(showGrid);
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
			DojoRankGrid1.Visible = false;
			DojoRankEditor1.Visible = false;
			DojoRankView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoRankGrid1.Visible = true;
			DojoRankEditor1.Visible = false;
			DojoRankView1.Visible = false;
		}

		private void DojoRankGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoRankEditor1.DojoRankID = 0;
					DojoRankEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoRankView1.DojoRankID = DojoRankGrid1.SelectedID;
					DojoRankView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoRankEditor1.DojoRankID = DojoRankGrid1.SelectedID;
					DojoRankEditor1.Visible = true;
					break;
			}
		}
	}
}
