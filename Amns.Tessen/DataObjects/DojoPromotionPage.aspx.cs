using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoPromotionPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoPromotionGrid1.ToolbarClicked += new ToolbarEventHandler(DojoPromotionGrid1_ToolbarClicked);
			DojoPromotionEditor1.Cancelled += new EventHandler(showGrid);
			DojoPromotionEditor1.Updated += new EventHandler(showGrid);
			DojoPromotionView1.OkClicked += new EventHandler(showGrid);
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
			DojoPromotionGrid1.Visible = false;
			DojoPromotionEditor1.Visible = false;
			DojoPromotionView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoPromotionGrid1.Visible = true;
			DojoPromotionEditor1.Visible = false;
			DojoPromotionView1.Visible = false;
		}

		private void DojoPromotionGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoPromotionEditor1.DojoPromotionID = 0;
					DojoPromotionEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoPromotionView1.DojoPromotionID = DojoPromotionGrid1.SelectedID;
					DojoPromotionView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoPromotionEditor1.DojoPromotionID = DojoPromotionGrid1.SelectedID;
					DojoPromotionEditor1.Visible = true;
					break;
			}
		}
	}
}
