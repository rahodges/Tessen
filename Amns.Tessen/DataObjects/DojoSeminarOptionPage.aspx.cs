using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoSeminarOptionPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoSeminarOptionGrid1.ToolbarClicked += new ToolbarEventHandler(DojoSeminarOptionGrid1_ToolbarClicked);
			DojoSeminarOptionEditor1.Cancelled += new EventHandler(showGrid);
			DojoSeminarOptionEditor1.Updated += new EventHandler(showGrid);
			DojoSeminarOptionView1.OkClicked += new EventHandler(showGrid);
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
			DojoSeminarOptionGrid1.Visible = false;
			DojoSeminarOptionEditor1.Visible = false;
			DojoSeminarOptionView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoSeminarOptionGrid1.Visible = true;
			DojoSeminarOptionEditor1.Visible = false;
			DojoSeminarOptionView1.Visible = false;
		}

		private void DojoSeminarOptionGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoSeminarOptionEditor1.DojoSeminarOptionID = 0;
					DojoSeminarOptionEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoSeminarOptionView1.DojoSeminarOptionID = DojoSeminarOptionGrid1.SelectedID;
					DojoSeminarOptionView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoSeminarOptionEditor1.DojoSeminarOptionID = DojoSeminarOptionGrid1.SelectedID;
					DojoSeminarOptionEditor1.Visible = true;
					break;
			}
		}
	}
}
