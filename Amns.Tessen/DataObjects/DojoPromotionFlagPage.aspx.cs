using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoPromotionFlagPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoPromotionFlagGrid1.ToolbarClicked += new ToolbarEventHandler(DojoPromotionFlagGrid1_ToolbarClicked);
			DojoPromotionFlagEditor1.Cancelled += new EventHandler(showGrid);
			DojoPromotionFlagEditor1.Updated += new EventHandler(showGrid);
			DojoPromotionFlagView1.OkClicked += new EventHandler(showGrid);
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
			DojoPromotionFlagGrid1.Visible = false;
			DojoPromotionFlagEditor1.Visible = false;
			DojoPromotionFlagView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoPromotionFlagGrid1.Visible = true;
			DojoPromotionFlagEditor1.Visible = false;
			DojoPromotionFlagView1.Visible = false;
		}

		private void DojoPromotionFlagGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoPromotionFlagEditor1.DojoPromotionFlagID = 0;
					DojoPromotionFlagEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoPromotionFlagView1.DojoPromotionFlagID = DojoPromotionFlagGrid1.SelectedID;
					DojoPromotionFlagView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoPromotionFlagEditor1.DojoPromotionFlagID = DojoPromotionFlagGrid1.SelectedID;
					DojoPromotionFlagEditor1.Visible = true;
					break;
			}
		}
	}
}
