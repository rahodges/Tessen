using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// <summary>
	/// A delete dialog for DojoSeminarReservation.
	/// </summary>
	/// </summary>
	[ToolboxData("<DojoSeminarReservation:DojoSeminarReservationDeleteDialog runat=server></{0}:DojoSeminarReservationDeleteDialog>")]
	public class DojoSeminarReservationDeleteDialog : TableWindow
	{

		private int dojoSeminarReservationID = -1;
		private DojoSeminarReservation dojoSeminarReservation;

		private Button btOk = new Button();
		private Button btCancel = new Button();

		#region Public Properties
		[Bindable(false),
			Category("Behavior"),
			DefaultValue(0)]
		public int DojoSeminarReservationID
		{
			get
			{
				return dojoSeminarReservationID;
			}
			set
			{
				dojoSeminarReservationID = value;
			}
		}

		#endregion

		// Child Controls
		protected override void CreateChildControls()
		{
			Controls.Clear();

			btOk.Text = "OK";
			btOk.EnableViewState = false;
			btOk.Click += new EventHandler(btOk_Click);
			Controls.Add(btOk);

			btCancel.Text = "Cancel";
			btCancel.EnableViewState = false;
			btCancel.Click += new EventHandler(btCancel_Click);
			Controls.Add(btCancel);

			ChildControlsCreated = true;
		}
		protected void btOk_Click(object sender, EventArgs e)
		{
			DojoSeminarReservation dojoSeminarReservation = new DojoSeminarReservation(dojoSeminarReservationID);
			dojoSeminarReservation.Delete();

			dojoSeminarReservationID = 0;

			OnDeleted(EventArgs.Empty);
		}

		protected void btCancel_Click(object sender, EventArgs e)
		{
			this.OnCancelled(EventArgs.Empty);
		}

			#region Public Control Events

			public event EventHandler Cancelled;
		protected virtual void OnCancelled(EventArgs e)
		{
			if(Cancelled != null)
				Cancelled(this, e);
		}

		public event EventHandler Deleted;
		protected virtual void OnDeleted(EventArgs e)
		{
			if(Deleted != null)
				Deleted(this, e);
		}

		#endregion

		protected override void OnInit(EventArgs e)
		{
			columnCount = 2;
			features = TableWindowFeatures.DisableContentSeparation;
		}

		protected override void OnPreRender(EventArgs e)
		{
			if(dojoSeminarReservationID != 0)
			{
				dojoSeminarReservation = new DojoSeminarReservation(dojoSeminarReservationID);
				text = "Delete - " + dojoSeminarReservation.ToString();
			}
			else
			{
				text = "Delete ";
			}
			EnsureWindowScripts();
		}

		protected override void RenderContent(HtmlTextWriter output)
		{
			if(dojoSeminarReservationID == 0)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", "row1");
				output.WriteAttribute("colspan", "2");
				output.Write(HtmlTextWriter.TagRightChar);

				output.Write("The DojoSeminarReservation selected does not exist.");

				output.WriteEndTag("td");
				output.WriteEndTag("tr");
				return;
			}

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);

			output.Write("<strong>Warning:</strong> This action deletes the selected ." +
			"<em>Use with caution, this option cannot be undone.</em>");

			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");			
			output.WriteAttribute("nowrap", "true");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(" to delete: ");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("width", "100%");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(dojoSeminarReservation.ToString());
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			btOk.RenderControl(output);
			output.Write("&nbsp;");
			btCancel.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
		}

		#region ViewState Methods

		protected override void LoadViewState(object savedState) 
		{
			if(savedState != null)
			{
				// Load State from the array of objects that was saved at ;
				// SavedViewState.
				object[] myState = (object[])savedState;
				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					dojoSeminarReservationID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoSeminarReservationID;
			return myState;
		}

		#endregion

	}
}
