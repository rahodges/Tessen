using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoClass.
	/// </summary>
	[ToolboxData("<DojoClass:DojoClassView runat=server></{0}:DojoClassView>")]
	public class DojoClassView : TableWindow, INamingContainer
	{
		private int dojoClassID;
		private DojoClass dojoClass;

		#region Private Control Fields for Default Folder

		private Literal ltName = new Literal();
		private Literal ltInstructor = new Literal();
		private Literal ltParentSeminar = new Literal();
		private Literal ltParentDefinition = new Literal();
		private Literal ltLocation = new Literal();
		private Literal ltAccessControlGroup = new Literal();

		#endregion

		#region Private Control Fields for Occupancy Folder

		private Literal ltOccupancyMax = new Literal();
		private Literal ltOccupancyTarget = new Literal();
		private Literal ltOccupancyCurrent = new Literal();
		private Literal ltOccupancyCheckDate = new Literal();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Schedule Folder

		private Literal ltSigninStart = new Literal();
		private Literal ltSigninEnd = new Literal();
		private Literal ltClassStart = new Literal();
		private Literal ltClassEnd = new Literal();

		#endregion

		private Button btOk = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoClassID
		{
			get
			{
				return dojoClassID;
			}
			set
			{
				dojoClassID = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();

			#region Child Controls for Default Folder

			ltName.EnableViewState = false;
			Controls.Add(ltName);

			ltInstructor.EnableViewState = false;
			Controls.Add(ltInstructor);

			ltParentSeminar.EnableViewState = false;
			Controls.Add(ltParentSeminar);

			ltParentDefinition.EnableViewState = false;
			Controls.Add(ltParentDefinition);

			ltLocation.EnableViewState = false;
			Controls.Add(ltLocation);

			ltAccessControlGroup.EnableViewState = false;
			Controls.Add(ltAccessControlGroup);

			#endregion

			#region Child Controls for Occupancy Folder

			ltOccupancyMax.EnableViewState = false;
			Controls.Add(ltOccupancyMax);

			ltOccupancyTarget.EnableViewState = false;
			Controls.Add(ltOccupancyTarget);

			ltOccupancyCurrent.EnableViewState = false;
			Controls.Add(ltOccupancyCurrent);

			ltOccupancyCheckDate.EnableViewState = false;
			Controls.Add(ltOccupancyCheckDate);

			#endregion

			#region Child Controls for Schedule Folder

			ltSigninStart.EnableViewState = false;
			Controls.Add(ltSigninStart);

			ltSigninEnd.EnableViewState = false;
			Controls.Add(ltSigninEnd);

			ltClassStart.EnableViewState = false;
			Controls.Add(ltClassStart);

			ltClassEnd.EnableViewState = false;
			Controls.Add(ltClassEnd);

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
			if(dojoClassID != 0)
			{
				dojoClass = new DojoClass(dojoClassID);

				#region Bind Default Folder

				//
				// Set Field Entries
				//

				ltName.Text = dojoClass.Name.ToString();

				//
				// Set Children Selections
				//

				// Instructor

				if(dojoClass.Instructor != null)
					ltInstructor.Text = dojoClass.Instructor.ToString();
				else
					ltInstructor.Text = string.Empty;

				// ParentSeminar

				if(dojoClass.ParentSeminar != null)
					ltParentSeminar.Text = dojoClass.ParentSeminar.ToString();
				else
					ltParentSeminar.Text = string.Empty;

				// ParentDefinition

				if(dojoClass.ParentDefinition != null)
					ltParentDefinition.Text = dojoClass.ParentDefinition.ToString();
				else
					ltParentDefinition.Text = string.Empty;

				// Location

				if(dojoClass.Location != null)
					ltLocation.Text = dojoClass.Location.ToString();
				else
					ltLocation.Text = string.Empty;

				// AccessControlGroup

				if(dojoClass.AccessControlGroup != null)
					ltAccessControlGroup.Text = dojoClass.AccessControlGroup.ToString();
				else
					ltAccessControlGroup.Text = string.Empty;


				#endregion

				#region Bind Occupancy Folder

				//
				// Set Field Entries
				//

				ltOccupancyMax.Text = dojoClass.OccupancyMax.ToString();
				ltOccupancyTarget.Text = dojoClass.OccupancyTarget.ToString();
				ltOccupancyCurrent.Text = dojoClass.OccupancyCurrent.ToString();
				ltOccupancyCheckDate.Text = dojoClass.OccupancyCheckDate.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind _system Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//


				#endregion

				#region Bind Schedule Folder

				//
				// Set Field Entries
				//

				ltSigninStart.Text = dojoClass.SigninStart.ToString();
				ltSigninEnd.Text = dojoClass.SigninEnd.ToString();
				ltClassStart.Text = dojoClass.ClassStart.ToString();
				ltClassEnd.Text = dojoClass.ClassEnd.ToString();

				//
				// Set Children Selections
				//


				#endregion

				text = "View  - " + dojoClass.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoClass ID", dojoClassID.ToString());
			output.WriteEndTag("tr");

			renderDefaultFolder(output);

			renderOccupancyFolder(output);

			render_systemFolder(output);

			renderScheduleFolder(output);

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

		#region Render Default Folder

		private void renderDefaultFolder(HtmlTextWriter output)
		{
			//
			// Render Default Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Default");
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
			output.Write("Class Name");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltName.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Instructor
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Instructor");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltInstructor.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentSeminar
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentSeminar");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltParentSeminar.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentDefinition
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentDefinition");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltParentDefinition.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Location
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Location");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltLocation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AccessControlGroup
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AccessControlGroup");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAccessControlGroup.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Occupancy Folder

		private void renderOccupancyFolder(HtmlTextWriter output)
		{
			//
			// Render Occupancy Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Occupancy");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OccupancyMax
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyMax");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOccupancyMax.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OccupancyTarget
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyTarget");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOccupancyTarget.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OccupancyCurrent
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyCurrent");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOccupancyCurrent.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OccupancyCheckDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyCheckDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOccupancyCheckDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

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

		#region Render Schedule Folder

		private void renderScheduleFolder(HtmlTextWriter output)
		{
			//
			// Render Schedule Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Schedule");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render SigninStart
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Signin Start");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltSigninStart.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render SigninEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Signin End");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltSigninEnd.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ClassStart
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class Start");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClassStart.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ClassEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class End");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClassEnd.RenderControl(output);
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
					dojoClassID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoClassID;
			return myState;
		}
	}
}
