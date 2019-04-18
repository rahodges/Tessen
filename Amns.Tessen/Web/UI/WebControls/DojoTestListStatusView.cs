using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoTestListStatus.
	/// </summary>
	[DefaultProperty("ConnectionString"), 
		ToolboxData("<{0}:DojoTestListStatusView runat=server></{0}:DojoTestListStatusView>")]
	public class DojoTestListStatusView : TableWindow, INamingContainer
	{
		private int dojoTestListStatusID;
		private DojoTestListStatus dojoTestListStatus;
		private string connectionString;

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for General Folder

		private Literal ltName = new Literal();
		private Literal ltDescription = new Literal();
		private Literal ltOrderNum = new Literal();

		#endregion

		#region Private Control Fields for Flags Folder

		private Literal ltIsDraft = new Literal();
		private Literal ltIsFinal = new Literal();
		private Literal ltIsComplete = new Literal();
		private Literal ltTeacherEditingEnabled = new Literal();

		#endregion

		#region Private Control Fields for Status Changes Folder

		private Literal ltOnFinalized = new Literal();
		private Literal ltOnCompleted = new Literal();

		#endregion

		private Button btOk = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoTestListStatusID
		{
			get
			{
				return dojoTestListStatusID;
			}
			set
			{
				dojoTestListStatusID = value;
			}
		}

		[Bindable(false),
			Category("Data"),
			DefaultValue("")]
		public string ConnectionString
		{
			get
			{
				return connectionString;
			}
			set
			{
				// Parse Connection String
				if(value.StartsWith("<jet40virtual>") & Context != null)
					connectionString = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" +
						Context.Server.MapPath(value.Substring(14, value.Length - 14));
				else if(value.StartsWith("<jet40config>") & Context != null)
					connectionString = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" +
						Context.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get(value.Substring(13, value.Length - 13)));
				else
					connectionString = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();

			#region Child Controls for General Folder

			ltName.EnableViewState = false;
			Controls.Add(ltName);

			ltDescription.EnableViewState = false;
			Controls.Add(ltDescription);

			ltOrderNum.EnableViewState = false;
			Controls.Add(ltOrderNum);

			#endregion

			#region Child Controls for Flags Folder

			ltIsDraft.EnableViewState = false;
			Controls.Add(ltIsDraft);

			ltIsFinal.EnableViewState = false;
			Controls.Add(ltIsFinal);

			ltIsComplete.EnableViewState = false;
			Controls.Add(ltIsComplete);

			ltTeacherEditingEnabled.EnableViewState = false;
			Controls.Add(ltTeacherEditingEnabled);

			#endregion

			#region Child Controls for Status Changes Folder

			ltOnFinalized.EnableViewState = false;
			Controls.Add(ltOnFinalized);

			ltOnCompleted.EnableViewState = false;
			Controls.Add(ltOnCompleted);

			#endregion

			btOk.Text = "OK";
			btOk.Width = Unit.Pixel(72);
			btOk.EnableViewState = false;
			btOk.Click += new EventHandler(ok_Click);
			Controls.Add(btOk);

			btDelete.Text = "Delete";
			btDelete.Width = Unit.Pixel(72);
			btDelete.EnableViewState = false;
			btDelete.Click += new EventHandler(delete_Click);
			Controls.Add(btDelete);

			ChildControlsCreated = true;
		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			OnOkClicked(EventArgs.Empty);
		}

		#endregion

		protected void delete_Click(object sender, EventArgs e)
		{
			this.OnDeleteClicked(EventArgs.Empty);
		}

		public event EventHandler OkClicked;
		protected virtual void OnOkClicked(EventArgs e)
		{
			if(OkClicked != null)
				OkClicked(this, e);
		}

		public event EventHandler DeleteClicked;
		protected virtual void OnDeleteClicked(EventArgs e)
		{
			if(DeleteClicked != null)
			DeleteClicked(this, e);
		}

		protected override void OnInit(EventArgs e)
		{
			columnCount = 2;
			features = TableWindowFeatures.DisableContentSeparation | 
				TableWindowFeatures.WindowPrinter;
		}

		protected override void OnPreRender(EventArgs e)
		{
			if(dojoTestListStatusID != 0)
			{
				dojoTestListStatus = new DojoTestListStatus(dojoTestListStatusID);

				#region Bind _system Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//


				#endregion

				#region Bind General Folder

				//
				// Set Field Entries
				//

				ltName.Text = dojoTestListStatus.Name.ToString();
				ltDescription.Text = dojoTestListStatus.Description.ToString();
				ltOrderNum.Text = dojoTestListStatus.OrderNum.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Flags Folder

				//
				// Set Field Entries
				//

				ltIsDraft.Text = dojoTestListStatus.IsDraft.ToString();
				ltIsFinal.Text = dojoTestListStatus.IsFinal.ToString();
				ltIsComplete.Text = dojoTestListStatus.IsComplete.ToString();
				ltTeacherEditingEnabled.Text = dojoTestListStatus.TeacherEditingEnabled.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Status Changes Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// OnFinalized

				if(dojoTestListStatus.OnFinalized != null)
					ltOnFinalized.Text = dojoTestListStatus.OnFinalized.ToString();
				else
					ltOnFinalized.Text = string.Empty;

				// OnCompleted

				if(dojoTestListStatus.OnCompleted != null)
					ltOnCompleted.Text = dojoTestListStatus.OnCompleted.ToString();
				else
					ltOnCompleted.Text = string.Empty;


				#endregion

				text = "View  - " + dojoTestListStatus.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoTestListStatus ID", dojoTestListStatusID.ToString());
			output.WriteEndTag("tr");

			render_systemFolder(output);

			renderGeneralFolder(output);

			renderFlagsFolder(output);

			renderStatus_ChangesFolder(output);

			//
			// Render OK/Cancel Buttons
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			btOk.RenderControl(output);
			if(DeleteClicked != null)
			{
				output.Write(" ");
				btDelete.RenderControl(output);
			}
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
		}

		#region Render _system Folder

		private void render_systemFolder(HtmlTextWriter output)
		{
			//
			// Render _system Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("System Folder");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render General Folder

		private void renderGeneralFolder(HtmlTextWriter output)
		{
			//
			// Render General Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("General");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Name
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Name");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltName.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Description
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Description");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDescription.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OrderNum
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OrderNum");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOrderNum.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Flags Folder

		private void renderFlagsFolder(HtmlTextWriter output)
		{
			//
			// Render Flags Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Flags");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsDraft
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsDraft");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsDraft.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsFinal
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsFinal");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsFinal.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsComplete
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsComplete");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsComplete.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render TeacherEditingEnabled
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("TeacherEditingEnabled");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltTeacherEditingEnabled.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Status Changes Folder

		private void renderStatus_ChangesFolder(HtmlTextWriter output)
		{
			//
			// Render Status Changes Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Status Changes");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnFinalized
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnFinalized");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOnFinalized.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnCompleted
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnCompleted");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOnCompleted.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		protected override void LoadViewState(object savedState)
		{
			if(savedState != null)
			{
				object[] myState = (object[]) savedState;
				if(myState[0] != null)
					base.LoadViewState(myState[0]);
				if(myState[1] != null)
					dojoTestListStatusID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoTestListStatusID;
			return myState;
		}
	}
}
