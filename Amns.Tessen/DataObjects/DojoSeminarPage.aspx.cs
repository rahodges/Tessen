using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoSeminarPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoSeminarGrid1.ToolbarClicked += new ToolbarEventHandler(DojoSeminarGrid1_ToolbarClicked);
			DojoSeminarEditor1.Cancelled += new EventHandler(showGrid);
			DojoSeminarEditor1.Updated += new EventHandler(showGrid);
			DojoSeminarView1.OkClicked += new EventHandler(showGrid);
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
			DojoSeminarGrid1.Visible = false;
			DojoSeminarEditor1.Visible = false;
			DojoSeminarView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoSeminarGrid1.Visible = true;
			DojoSeminarEditor1.Visible = false;
			DojoSeminarView1.Visible = false;
		}

		private void DojoSeminarGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoSeminarEditor1.DojoSeminarID = 0;
					DojoSeminarEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoSeminarView1.DojoSeminarID = DojoSeminarGrid1.SelectedID;
					DojoSeminarView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoSeminarEditor1.DojoSeminarID = DojoSeminarGrid1.SelectedID;
					DojoSeminarEditor1.Visible = true;
					break;
			}
		}
	}
}
