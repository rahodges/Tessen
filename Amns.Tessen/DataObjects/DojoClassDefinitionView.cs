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
	/// Default web editor for DojoClassDefinition.
	/// </summary>
	[ToolboxData("<DojoClassDefinition:DojoClassDefinitionView runat=server></{0}:DojoClassDefinitionView>")]
	public class DojoClassDefinitionView : TableWindow, INamingContainer
	{
		private int dojoClassDefinitionID;
		private DojoClassDefinition dojoClassDefinition;

		#region Private Control Fields for Default Folder

		private Literal ltName = new Literal();
		private Literal ltDescription = new Literal();
		private Literal ltIsDisabled = new Literal();
		private Literal ltOccupancyAvg = new Literal();
		private Literal ltOccupancyAvgDate = new Literal();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Access Control Folder

		private Literal ltAccessControlGroup = new Literal();

		#endregion

		#region Private Control Fields for Recurrency Folder

		private Literal ltRecurrenceType = new Literal();
		private Literal ltRecurrenceCount = new Literal();
		private Literal ltRecurrenceEnd = new Literal();
		private Literal ltRecurrenceSpan = new Literal();

		#endregion

		#region Private Control Fields for Next Class Folder

		private Literal ltNextSigninStart = new Literal();
		private Literal ltNextSigninEnd = new Literal();
		private Literal ltNextClassStart = new Literal();
		private Literal ltNextClassEnd = new Literal();
		private Literal ltInstructor = new Literal();
		private Literal ltLocation = new Literal();

		#endregion

		private Button btOk = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoClassDefinitionID
		{
			get
			{
				return dojoClassDefinitionID;
			}
			set
			{
				dojoClassDefinitionID = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();

			#region Child Controls for Default Folder

			ltName.EnableViewState = false;
			Controls.Add(ltName);

			ltDescription.EnableViewState = false;
			Controls.Add(ltDescription);

			ltIsDisabled.EnableViewState = false;
			Controls.Add(ltIsDisabled);

			ltOccupancyAvg.EnableViewState = false;
			Controls.Add(ltOccupancyAvg);

			ltOccupancyAvgDate.EnableViewState = false;
			Controls.Add(ltOccupancyAvgDate);

			#endregion

			#region Child Controls for Access Control Folder

			ltAccessControlGroup.EnableViewState = false;
			Controls.Add(ltAccessControlGroup);

			#endregion

			#region Child Controls for Recurrency Folder

			ltRecurrenceType.EnableViewState = false;
			Controls.Add(ltRecurrenceType);

			ltRecurrenceCount.EnableViewState = false;
			Controls.Add(ltRecurrenceCount);

			ltRecurrenceEnd.EnableViewState = false;
			Controls.Add(ltRecurrenceEnd);

			ltRecurrenceSpan.EnableViewState = false;
			Controls.Add(ltRecurrenceSpan);

			#endregion

			#region Child Controls for Next Class Folder

			ltNextSigninStart.EnableViewState = false;
			Controls.Add(ltNextSigninStart);

			ltNextSigninEnd.EnableViewState = false;
			Controls.Add(ltNextSigninEnd);

			ltNextClassStart.EnableViewState = false;
			Controls.Add(ltNextClassStart);

			ltNextClassEnd.EnableViewState = false;
			Controls.Add(ltNextClassEnd);

			ltInstructor.EnableViewState = false;
			Controls.Add(ltInstructor);

			ltLocation.EnableViewState = false;
			Controls.Add(ltLocation);

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
			if(dojoClassDefinitionID != 0)
			{
				dojoClassDefinition = new DojoClassDefinition(dojoClassDefinitionID);

				#region Bind Default Folder

				//
				// Set Field Entries
				//

				ltName.Text = dojoClassDefinition.Name.ToString();
				ltDescription.Text = dojoClassDefinition.Description.ToString();
				ltIsDisabled.Text = dojoClassDefinition.IsDisabled.ToString();
				ltOccupancyAvg.Text = dojoClassDefinition.OccupancyAvg.ToString();
				ltOccupancyAvgDate.Text = dojoClassDefinition.OccupancyAvgDate.ToString();

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

				#region Bind Access Control Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// AccessControlGroup

				if(dojoClassDefinition.AccessControlGroup != null)
					ltAccessControlGroup.Text = dojoClassDefinition.AccessControlGroup.ToString();
				else
					ltAccessControlGroup.Text = string.Empty;


				#endregion

				#region Bind Recurrency Folder

				//
				// Set Field Entries
				//

				ltRecurrenceType.Text = dojoClassDefinition.RecurrenceType.ToString();
				ltRecurrenceCount.Text = dojoClassDefinition.RecurrenceCount.ToString();
				ltRecurrenceEnd.Text = dojoClassDefinition.RecurrenceEnd.ToString();
				ltRecurrenceSpan.Text = dojoClassDefinition.RecurrenceSpan.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Next Class Folder

				//
				// Set Field Entries
				//

				ltNextSigninStart.Text = dojoClassDefinition.NextSigninStart.ToString();
				ltNextSigninEnd.Text = dojoClassDefinition.NextSigninEnd.ToString();
				ltNextClassStart.Text = dojoClassDefinition.NextClassStart.ToString();
				ltNextClassEnd.Text = dojoClassDefinition.NextClassEnd.ToString();

				//
				// Set Children Selections
				//

				// Instructor

				if(dojoClassDefinition.Instructor != null)
					ltInstructor.Text = dojoClassDefinition.Instructor.ToString();
				else
					ltInstructor.Text = string.Empty;

				// Location

				if(dojoClassDefinition.Location != null)
					ltLocation.Text = dojoClassDefinition.Location.ToString();
				else
					ltLocation.Text = string.Empty;


				#endregion

				text = "View  - " + dojoClassDefinition.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoClassDefinition ID", dojoClassDefinitionID.ToString());
			output.WriteEndTag("tr");

			renderDefaultFolder(output);

			render_systemFolder(output);

			renderAccess_ControlFolder(output);

			renderRecurrencyFolder(output);

			renderNext_ClassFolder(output);

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
			// Render Description
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Definition Description");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDescription.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsDisabled
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsDisabled");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsDisabled.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OccupancyAvg
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyAvg");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOccupancyAvg.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OccupancyAvgDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyAvgDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOccupancyAvgDate.RenderControl(output);
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

		#region Render Access Control Folder

		private void renderAccess_ControlFolder(HtmlTextWriter output)
		{
			//
			// Render Access Control Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Access Control");
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

		#region Render Recurrency Folder

		private void renderRecurrencyFolder(HtmlTextWriter output)
		{
			//
			// Render Recurrency Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Recurrency");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RecurrenceType
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("The recurrence type for scheduling.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRecurrenceType.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RecurrenceCount
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("The remaining count for recurrences.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRecurrenceCount.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RecurrenceEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Date to end class definition.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRecurrenceEnd.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RecurrenceSpan
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Time span to calculate recurring classes.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRecurrenceSpan.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Next Class Folder

		private void renderNext_ClassFolder(HtmlTextWriter output)
		{
			//
			// Render Next Class Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Next Class");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render NextSigninStart
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Next Signin Start");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltNextSigninStart.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render NextSigninEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Next Signin End");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltNextSigninEnd.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render NextClassStart
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Next Class Start");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltNextClassStart.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render NextClassEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Next Class End");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltNextClassEnd.RenderControl(output);
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
					dojoClassDefinitionID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoClassDefinitionID;
			return myState;
		}
	}
}
