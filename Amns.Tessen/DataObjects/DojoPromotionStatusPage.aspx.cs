using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoPromotionStatusPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoPromotionStatusGrid1.ToolbarClicked += new ToolbarEventHandler(DojoPromotionStatusGrid1_ToolbarClicked);
			DojoPromotionStatusEditor1.Cancelled += new EventHandler(showGrid);
			DojoPromotionStatusEditor1.Updated += new EventHandler(showGrid);
			DojoPromotionStatusView1.OkClicked += new EventHandler(showGrid);
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
			DojoPromotionStatusGrid1.Visible = false;
			DojoPromotionStatusEditor1.Visible = false;
			DojoPromotionStatusView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoPromotionStatusGrid1.Visible = true;
			DojoPromotionStatusEditor1.Visible = false;
			DojoPromotionStatusView1.Visible = false;
		}

		private void DojoPromotionStatusGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoPromotionStatusEditor1.DojoPromotionStatusID = 0;
					DojoPromotionStatusEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoPromotionStatusView1.DojoPromotionStatusID = DojoPromotionStatusGrid1.SelectedID;
					DojoPromotionStatusView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoPromotionStatusEditor1.DojoPromotionStatusID = DojoPromotionStatusGrid1.SelectedID;
					DojoPromotionStatusEditor1.Visible = true;
					break;
			}
		}
	}
}
