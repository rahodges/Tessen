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
	[ToolboxData("<{0}:DojoClassDefinitionEditor runat=server></{0}:DojoClassDefinitionEditor>")]
	public class DojoClassDefinitionEditor : TableWindow, INamingContainer
	{
		private int dojoClassDefinitionID;
		private DojoClassDefinition obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for Default Folder

		private TextBox tbName = new TextBox();
		private TextBox tbDescription = new TextBox();
		private CheckBox cbIsDisabled = new CheckBox();
		private TextBox tbOccupancyAvg = new TextBox();
		private RegularExpressionValidator revOccupancyAvg = new RegularExpressionValidator();
		private TextBox tbOccupancyAvgDate = new TextBox();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Access_Control Folder

		private MultiSelectBox msAccessControlGroup = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Recurrency Folder

		private TextBox tbRecurrenceType = new TextBox();
		private TextBox tbRecurrenceCount = new TextBox();
		private RegularExpressionValidator revRecurrenceCount = new RegularExpressionValidator();
		private TextBox tbRecurrenceEnd = new TextBox();
		private TextBox tbRecurrenceSpan = new TextBox();

		#endregion

		#region Private Control Fields for Next_Class Folder

		private TextBox tbNextSigninStart = new TextBox();
		private TextBox tbNextSigninEnd = new TextBox();
		private TextBox tbNextClassStart = new TextBox();
		private TextBox tbNextClassEnd = new TextBox();
		private MultiSelectBox msInstructor = new MultiSelectBox();
		private MultiSelectBox msLocation = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
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
				loadFlag = true;
				dojoClassDefinitionID = value;
			}
		}

		[Bindable(true), Category("Behavior"), DefaultValue(false)]
		public bool ResetOnAdd
		{
			get
			{
				return resetOnAdd;
			}
			set
			{
				resetOnAdd = value;
			}
		}

		[Bindable(true), Category("Behavior"), DefaultValue(false)]
		public bool EditOnAdd
		{
			get
			{
				return editOnAdd;
			}
			set
			{
				editOnAdd = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();
			bindDropDownLists();

			#region Child Controls for Default Folder

			tbName.EnableViewState = false;
			Controls.Add(tbName);

			tbDescription.EnableViewState = false;
			Controls.Add(tbDescription);

			cbIsDisabled.EnableViewState = false;
			Controls.Add(cbIsDisabled);

			tbOccupancyAvg.ID = this.ID + "_OccupancyAvg";
			tbOccupancyAvg.EnableViewState = false;
			Controls.Add(tbOccupancyAvg);
			revOccupancyAvg.ControlToValidate = tbOccupancyAvg.ID;
			revOccupancyAvg.ValidationExpression = "^(\\+|-)?\\d+$";
			revOccupancyAvg.ErrorMessage = "*";
			revOccupancyAvg.Display = ValidatorDisplay.Dynamic;
			revOccupancyAvg.EnableViewState = false;
			Controls.Add(revOccupancyAvg);

			tbOccupancyAvgDate.EnableViewState = false;
			Controls.Add(tbOccupancyAvgDate);

			#endregion

			#region Child Controls for Access Control Folder

			msAccessControlGroup.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msAccessControlGroup);

			#endregion

			#region Child Controls for Recurrency Folder

			tbRecurrenceType.EnableViewState = false;
			Controls.Add(tbRecurrenceType);

			tbRecurrenceCount.ID = this.ID + "_RecurrenceCount";
			tbRecurrenceCount.EnableViewState = false;
			Controls.Add(tbRecurrenceCount);
			revRecurrenceCount.ControlToValidate = tbRecurrenceCount.ID;
			revRecurrenceCount.ValidationExpression = "^(\\+|-)?\\d+$";
			revRecurrenceCount.ErrorMessage = "*";
			revRecurrenceCount.Display = ValidatorDisplay.Dynamic;
			revRecurrenceCount.EnableViewState = false;
			Controls.Add(revRecurrenceCount);

			tbRecurrenceEnd.EnableViewState = false;
			Controls.Add(tbRecurrenceEnd);

			tbRecurrenceSpan.EnableViewState = false;
			Controls.Add(tbRecurrenceSpan);

			#endregion

			#region Child Controls for Next Class Folder

			tbNextSigninStart.EnableViewState = false;
			Controls.Add(tbNextSigninStart);

			tbNextSigninEnd.EnableViewState = false;
			Controls.Add(tbNextSigninEnd);

			tbNextClassStart.EnableViewState = false;
			Controls.Add(tbNextClassStart);

			tbNextClassEnd.EnableViewState = false;
			Controls.Add(tbNextClassEnd);

			msInstructor.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msInstructor);

			msLocation.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msLocation);

			#endregion

			btOk.Text = "OK";
			btOk.Width = Unit.Pixel(72);
			btOk.EnableViewState = false;
			btOk.Click += new EventHandler(ok_Click);
			Controls.Add(btOk);

			btCancel.Text = "Cancel";
			btCancel.Width = Unit.Pixel(72);
			btCancel.EnableViewState = false;
			btCancel.CausesValidation = false;
			btCancel.Click += new EventHandler(cancel_Click);
			Controls.Add(btCancel);

			btDelete.Text = "Delete";
			btDelete.Width = Unit.Pixel(72);
			btDelete.EnableViewState = false;
			btDelete.Click += new EventHandler(delete_Click);
			Controls.Add(btDelete);

			ChildControlsCreated = true;
		}

		private void bindDropDownLists()
		{
			#region Bind Access Control Child Data

			msAccessControlGroup.Items.Add(new ListItem("Null", "Null"));
			DojoAccessControlGroupManager accessControlGroupManager = new DojoAccessControlGroupManager();
			DojoAccessControlGroupCollection accessControlGroupCollection = accessControlGroupManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAccessControlGroup accessControlGroup in accessControlGroupCollection)
			{
				ListItem i = new ListItem(accessControlGroup.ToString(), accessControlGroup.ID.ToString());
				msAccessControlGroup.Items.Add(i);
			}

			#endregion

			#region Bind Next Class Child Data

			msInstructor.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager instructorManager = new DojoMemberManager();
			DojoMemberCollection instructorCollection = instructorManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember instructor in instructorCollection)
			{
				ListItem i = new ListItem(instructor.ToString(), instructor.ID.ToString());
				msInstructor.Items.Add(i);
			}

			msLocation.Items.Add(new ListItem("Null", "Null"));
			GreyFoxContactManager locationManager = new GreyFoxContactManager("kitTessen_Locations");
			GreyFoxContactCollection locationCollection = locationManager.GetCollection(string.Empty, string.Empty);
			foreach(GreyFoxContact location in locationCollection)
			{
				ListItem i = new ListItem(location.ToString(), location.ID.ToString());
				msLocation.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoClassDefinitionID == 0)
				obj = new DojoClassDefinition();
			else
				obj = new DojoClassDefinition(dojoClassDefinitionID);

			obj.Name = tbName.Text;
			obj.Description = tbDescription.Text;
			obj.IsDisabled = cbIsDisabled.Checked;
			obj.OccupancyAvg = int.Parse(tbOccupancyAvg.Text);
			obj.OccupancyAvgDate = DateTime.Parse(tbOccupancyAvgDate.Text);
			obj.RecurrenceType = byte.Parse(tbRecurrenceType.Text);
			obj.RecurrenceCount = int.Parse(tbRecurrenceCount.Text);
			obj.RecurrenceEnd = DateTime.Parse(tbRecurrenceEnd.Text);
			obj.RecurrenceSpan = TimeSpan.Parse(tbRecurrenceSpan.Text);
			obj.NextSigninStart = DateTime.Parse(tbNextSigninStart.Text);
			obj.NextSigninEnd = DateTime.Parse(tbNextSigninEnd.Text);
			obj.NextClassStart = DateTime.Parse(tbNextClassStart.Text);
			obj.NextClassEnd = DateTime.Parse(tbNextClassEnd.Text);

			if(msAccessControlGroup.SelectedItem != null && msAccessControlGroup.SelectedItem.Value != "Null")
				obj.AccessControlGroup = DojoAccessControlGroup.NewPlaceHolder(
					int.Parse(msAccessControlGroup.SelectedItem.Value));
			else
				obj.AccessControlGroup = null;

			if(msInstructor.SelectedItem != null && msInstructor.SelectedItem.Value != "Null")
				obj.Instructor = DojoMember.NewPlaceHolder(
					int.Parse(msInstructor.SelectedItem.Value));
			else
				obj.Instructor = null;

			if(msLocation.SelectedItem != null && msLocation.SelectedItem.Value != "Null")
				obj.Location = GreyFoxContact.NewPlaceHolder("kitTessen_Locations", 
					int.Parse(msLocation.SelectedItem.Value));
			else
				obj.Location = null;

			if(editOnAdd)
				dojoClassDefinitionID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbDescription.Text = string.Empty;
				cbIsDisabled.Checked = false;
				tbOccupancyAvg.Text = string.Empty;
				tbOccupancyAvgDate.Text = DateTime.Now.ToString();
				tbRecurrenceType.Text = string.Empty;
				tbRecurrenceCount.Text = string.Empty;
				tbRecurrenceEnd.Text = DateTime.Now.ToString();
				tbRecurrenceSpan.Text = string.Empty;
				tbNextSigninStart.Text = DateTime.Now.ToString();
				tbNextSigninEnd.Text = DateTime.Now.ToString();
				tbNextClassStart.Text = DateTime.Now.ToString();
				tbNextClassEnd.Text = DateTime.Now.ToString();
				msAccessControlGroup.SelectedIndex = 0;
				msInstructor.SelectedIndex = 0;
				msLocation.SelectedIndex = 0;
			}

			OnUpdated(EventArgs.Empty);
		}

		#endregion

		protected void cancel_Click(object sender, EventArgs e)
		{
			this.OnCancelled(EventArgs.Empty);
		}

		protected void delete_Click(object sender, EventArgs e)
		{
			this.OnDeleteClicked(EventArgs.Empty);
		}

		public event EventHandler Cancelled;
		protected virtual void OnCancelled(EventArgs e)
		{
			if(Cancelled != null)
				Cancelled(this, e);
		}

		public event EventHandler Updated;
		protected virtual void OnUpdated(EventArgs e)
		{
			if(Updated != null)
				Updated(this, e);
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
			features = TableWindowFeatures.DisableContentSeparation;
			components = TableWindowComponents.Tabs;
			tabStrip = new TabStrip();
			tabStrip.Tabs = new TabList();

			Tab DefaultTab = new Tab("Default");
			DefaultTab.Visible = true;
			DefaultTab.RenderDiv += new TabRenderHandler(renderDefaultFolder);
			DefaultTab.Visible = true;
			tabStrip.Tabs.Add(DefaultTab);

			Tab Access_ControlTab = new Tab("Access Control");
			Access_ControlTab.RenderDiv += new TabRenderHandler(renderAccess_ControlFolder);
			tabStrip.Tabs.Add(Access_ControlTab);

			Tab RecurrencyTab = new Tab("Recurrency");
			RecurrencyTab.RenderDiv += new TabRenderHandler(renderRecurrencyFolder);
			tabStrip.Tabs.Add(RecurrencyTab);

			Tab Next_ClassTab = new Tab("Next Class");
			Next_ClassTab.RenderDiv += new TabRenderHandler(renderNext_ClassFolder);
			tabStrip.Tabs.Add(Next_ClassTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoClassDefinitionID > 0)
				{
					obj = new DojoClassDefinition(dojoClassDefinitionID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoClassDefinitionID <= 0)
				{
					obj = new DojoClassDefinition();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbName.Text = obj.Name;
				tbDescription.Text = obj.Description;
				cbIsDisabled.Checked = obj.IsDisabled;
				tbOccupancyAvg.Text = obj.OccupancyAvg.ToString();
				tbOccupancyAvgDate.Text = obj.OccupancyAvgDate.ToString();
				tbRecurrenceType.Text = obj.RecurrenceType.ToString();
				tbRecurrenceCount.Text = obj.RecurrenceCount.ToString();
				tbRecurrenceEnd.Text = obj.RecurrenceEnd.ToString();
				tbRecurrenceSpan.Text = obj.RecurrenceSpan.ToString();
				tbNextSigninStart.Text = obj.NextSigninStart.ToString();
				tbNextSigninEnd.Text = obj.NextSigninEnd.ToString();
				tbNextClassStart.Text = obj.NextClassStart.ToString();
				tbNextClassEnd.Text = obj.NextClassEnd.ToString();

				//
				// Set Children Selections
				//
				if(obj.AccessControlGroup != null)
					foreach(ListItem item in msAccessControlGroup.Items)
						item.Selected = obj.AccessControlGroup.ID.ToString() == item.Value;
					else
						msAccessControlGroup.SelectedIndex = 0;

				if(obj.Instructor != null)
					foreach(ListItem item in msInstructor.Items)
						item.Selected = obj.Instructor.ID.ToString() == item.Value;
					else
						msInstructor.SelectedIndex = 0;

				if(obj.Location != null)
					foreach(ListItem item in msLocation.Items)
						item.Selected = obj.Location.ID.ToString() == item.Value;
					else
						msLocation.SelectedIndex = 0;

			}
		}

		protected override void RenderContent(HtmlTextWriter output)
		{
			RenderTabPanels(output);
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
			output.Write("&nbsp;");
			btCancel.RenderControl(output);
			if(DeleteClicked != null)
			{
				output.Write(" ");
				btDelete.RenderControl(output);
			}
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
		}

		private void renderDefaultFolder(HtmlTextWriter output)
		{
			//
			// Render Name
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class Name");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbName.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Description
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Definition Description");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbDescription.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsDisabled
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsDisabled");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsDisabled.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OccupancyAvg
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyAvg");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbOccupancyAvg.RenderControl(output);
			revOccupancyAvg.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OccupancyAvgDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyAvgDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbOccupancyAvgDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		private void renderAccess_ControlFolder(HtmlTextWriter output)
		{
			//
			// Render AccessControlGroup
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AccessControlGroup");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msAccessControlGroup.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderRecurrencyFolder(HtmlTextWriter output)
		{
			//
			// Render RecurrenceType
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("The recurrence type for scheduling.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbRecurrenceType.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RecurrenceCount
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("The remaining count for recurrences.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbRecurrenceCount.RenderControl(output);
			revRecurrenceCount.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RecurrenceEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Date to end class definition.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbRecurrenceEnd.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RecurrenceSpan
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Time span to calculate recurring classes.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbRecurrenceSpan.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderNext_ClassFolder(HtmlTextWriter output)
		{
			//
			// Render NextSigninStart
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Next Signin Start");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbNextSigninStart.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render NextSigninEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Next Signin End");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbNextSigninEnd.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render NextClassStart
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Next Class Start");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbNextClassStart.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render NextClassEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Next Class End");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbNextClassEnd.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Instructor
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Instructor");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msInstructor.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Location
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Location");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msLocation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

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

