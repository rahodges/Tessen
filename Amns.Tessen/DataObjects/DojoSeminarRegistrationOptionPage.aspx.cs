using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoSeminarRegistrationOptionPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoSeminarRegistrationOptionGrid1.ToolbarClicked += new ToolbarEventHandler(DojoSeminarRegistrationOptionGrid1_ToolbarClicked);
			DojoSeminarRegistrationOptionEditor1.Cancelled += new EventHandler(showGrid);
			DojoSeminarRegistrationOptionEditor1.Updated += new EventHandler(showGrid);
			DojoSeminarRegistrationOptionView1.OkClicked += new EventHandler(showGrid);
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
			DojoSeminarRegistrationOptionGrid1.Visible = false;
			DojoSeminarRegistrationOptionEditor1.Visible = false;
			DojoSeminarRegistrationOptionView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoSeminarRegistrationOptionGrid1.Visible = true;
			DojoSeminarRegistrationOptionEditor1.Visible = false;
			DojoSeminarRegistrationOptionView1.Visible = false;
		}

		private void DojoSeminarRegistrationOptionGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoSeminarRegistrationOptionEditor1.DojoSeminarRegistrationOptionID = 0;
					DojoSeminarRegistrationOptionEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoSeminarRegistrationOptionView1.DojoSeminarRegistrationOptionID = DojoSeminarRegistrationOptionGrid1.SelectedID;
					DojoSeminarRegistrationOptionView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoSeminarRegistrationOptionEditor1.DojoSeminarRegistrationOptionID = DojoSeminarRegistrationOptionGrid1.SelectedID;
					DojoSeminarRegistrationOptionEditor1.Visible = true;
					break;
			}
		}
	}
}
