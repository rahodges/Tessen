using System;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;

namespace TessenWeb.Administration
{
	/// <summary>
	/// Default DbModel Class Page Codebehind
	/// </summary>
	public partial class DojoClassDefinitionPage : System.Web.UI.Page
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			DojoClassDefinitionGrid1.ToolbarClicked += new ToolbarEventHandler(DojoClassDefinitionGrid1_ToolbarClicked);
			DojoClassDefinitionEditor1.Cancelled += new EventHandler(showGrid);
			DojoClassDefinitionEditor1.Updated += new EventHandler(showGrid);
			DojoClassDefinitionView1.OkClicked += new EventHandler(showGrid);
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
			DojoClassDefinitionGrid1.Visible = false;
			DojoClassDefinitionEditor1.Visible = false;
			DojoClassDefinitionView1.Visible = false;
		}

		private void showGrid(object sender, EventArgs e)
		{
			DojoClassDefinitionGrid1.Visible = true;
			DojoClassDefinitionEditor1.Visible = false;
			DojoClassDefinitionView1.Visible = false;
		}

		private void DojoClassDefinitionGrid1_ToolbarClicked(object sender, ToolbarEventArgs e)
		{
			switch(e.SelectedToolbarItem.Command)
			{
				case "new":
					resetControls();
					DojoClassDefinitionEditor1.DojoClassDefinitionID = 0;
					DojoClassDefinitionEditor1.Visible = true;
					break;
				case "view":
					resetControls();
					DojoClassDefinitionView1.DojoClassDefinitionID = DojoClassDefinitionGrid1.SelectedID;
					DojoClassDefinitionView1.Visible = true;
					break;
				case "edit":
					resetControls();
					DojoClassDefinitionEditor1.DojoClassDefinitionID = DojoClassDefinitionGrid1.SelectedID;
					DojoClassDefinitionEditor1.Visible = true;
					break;
			}
		}
	}
}
