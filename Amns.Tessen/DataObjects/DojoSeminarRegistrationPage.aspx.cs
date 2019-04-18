using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoSeminarRegistrationPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoSeminarRegistrationGrid1.ToolbarClicked += new ToolbarEventHandler(DojoSeminarRegistrationGrid1_ToolbarClicked);
			DojoSeminarRegistrationEditor1.Cancelled += new EventHandler(showGrid);
			DojoSeminarRegistrationEditor1.Updated += new EventHandler(showGrid);
			DojoSeminarRegistrationView1.OkClicked += new EventHandler(showGrid);
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
			DojoSeminarRegistrationGrid1.Visible = false;
			DojoSeminarRegistrationEditor1.Visible = false;
			DojoSeminarRegistrationView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoSeminarRegistrationGrid1.Visible = true;
			DojoSeminarRegistrationEditor1.Visible = false;
			DojoSeminarRegistrationView1.Visible = false;
		}

		private void DojoSeminarRegistrationGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoSeminarRegistrationEditor1.DojoSeminarRegistrationID = 0;
					DojoSeminarRegistrationEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoSeminarRegistrationView1.DojoSeminarRegistrationID = DojoSeminarRegistrationGrid1.SelectedID;
					DojoSeminarRegistrationView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoSeminarRegistrationEditor1.DojoSeminarRegistrationID = DojoSeminarRegistrationGrid1.SelectedID;
					DojoSeminarRegistrationEditor1.Visible = true;
					break;
			}
		}
	}
}
