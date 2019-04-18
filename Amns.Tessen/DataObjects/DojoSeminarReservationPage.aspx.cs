using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoSeminarReservationPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoSeminarReservationGrid1.ToolbarClicked += new ToolbarEventHandler(DojoSeminarReservationGrid1_ToolbarClicked);
			DojoSeminarReservationEditor1.Cancelled += new EventHandler(showGrid);
			DojoSeminarReservationEditor1.Updated += new EventHandler(showGrid);
			DojoSeminarReservationView1.OkClicked += new EventHandler(showGrid);
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
			DojoSeminarReservationGrid1.Visible = false;
			DojoSeminarReservationEditor1.Visible = false;
			DojoSeminarReservationView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoSeminarReservationGrid1.Visible = true;
			DojoSeminarReservationEditor1.Visible = false;
			DojoSeminarReservationView1.Visible = false;
		}

		private void DojoSeminarReservationGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoSeminarReservationEditor1.DojoSeminarReservationID = 0;
					DojoSeminarReservationEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoSeminarReservationView1.DojoSeminarReservationID = DojoSeminarReservationGrid1.SelectedID;
					DojoSeminarReservationView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoSeminarReservationEditor1.DojoSeminarReservationID = DojoSeminarReservationGrid1.SelectedID;
					DojoSeminarReservationEditor1.Visible = true;
					break;
			}
		}
	}
}
