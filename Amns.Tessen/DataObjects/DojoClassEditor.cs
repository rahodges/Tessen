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
	[ToolboxData("<{0}:DojoClassEditor runat=server></{0}:DojoClassEditor>")]
	public class DojoClassEditor : TableWindow, INamingContainer
	{
		private int dojoClassID;
		private DojoClass obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for Default Folder

		private TextBox tbName = new TextBox();
		private MultiSelectBox msInstructor = new MultiSelectBox();
		private MultiSelectBox msParentSeminar = new MultiSelectBox();
		private MultiSelectBox msParentDefinition = new MultiSelectBox();
		private MultiSelectBox msLocation = new MultiSelectBox();
		private MultiSelectBox msAccessControlGroup = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Occupancy Folder

		private TextBox tbOccupancyMax = new TextBox();
		private RegularExpressionValidator revOccupancyMax = new RegularExpressionValidator();
		private TextBox tbOccupancyTarget = new TextBox();
		private RegularExpressionValidator revOccupancyTarget = new RegularExpressionValidator();
		private TextBox tbOccupancyCurrent = new TextBox();
		private RegularExpressionValidator revOccupancyCurrent = new RegularExpressionValidator();
		private TextBox tbOccupancyCheckDate = new TextBox();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Schedule Folder

		private TextBox tbSigninStart = new TextBox();
		private TextBox tbSigninEnd = new TextBox();
		private TextBox tbClassStart = new TextBox();
		private TextBox tbClassEnd = new TextBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
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
				loadFlag = true;
				dojoClassID = value;
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

			msInstructor.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msInstructor);

			msParentSeminar.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msParentSeminar);

			msParentDefinition.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msParentDefinition);

			msLocation.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msLocation);

			msAccessControlGroup.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msAccessControlGroup);

			#endregion

			#region Child Controls for Occupancy Folder

			tbOccupancyMax.ID = this.ID + "_OccupancyMax";
			tbOccupancyMax.EnableViewState = false;
			Controls.Add(tbOccupancyMax);
			revOccupancyMax.ControlToValidate = tbOccupancyMax.ID;
			revOccupancyMax.ValidationExpression = "^(\\+|-)?\\d+$";
			revOccupancyMax.ErrorMessage = "*";
			revOccupancyMax.Display = ValidatorDisplay.Dynamic;
			revOccupancyMax.EnableViewState = false;
			Controls.Add(revOccupancyMax);

			tbOccupancyTarget.ID = this.ID + "_OccupancyTarget";
			tbOccupancyTarget.EnableViewState = false;
			Controls.Add(tbOccupancyTarget);
			revOccupancyTarget.ControlToValidate = tbOccupancyTarget.ID;
			revOccupancyTarget.ValidationExpression = "^(\\+|-)?\\d+$";
			revOccupancyTarget.ErrorMessage = "*";
			revOccupancyTarget.Display = ValidatorDisplay.Dynamic;
			revOccupancyTarget.EnableViewState = false;
			Controls.Add(revOccupancyTarget);

			tbOccupancyCurrent.ID = this.ID + "_OccupancyCurrent";
			tbOccupancyCurrent.EnableViewState = false;
			Controls.Add(tbOccupancyCurrent);
			revOccupancyCurrent.ControlToValidate = tbOccupancyCurrent.ID;
			revOccupancyCurrent.ValidationExpression = "^(\\+|-)?\\d+$";
			revOccupancyCurrent.ErrorMessage = "*";
			revOccupancyCurrent.Display = ValidatorDisplay.Dynamic;
			revOccupancyCurrent.EnableViewState = false;
			Controls.Add(revOccupancyCurrent);

			tbOccupancyCheckDate.EnableViewState = false;
			Controls.Add(tbOccupancyCheckDate);

			#endregion

			#region Child Controls for Schedule Folder

			tbSigninStart.EnableViewState = false;
			Controls.Add(tbSigninStart);

			tbSigninEnd.EnableViewState = false;
			Controls.Add(tbSigninEnd);

			tbClassStart.EnableViewState = false;
			Controls.Add(tbClassStart);

			tbClassEnd.EnableViewState = false;
			Controls.Add(tbClassEnd);

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
			#region Bind Default Child Data

			msInstructor.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager instructorManager = new DojoMemberManager();
			DojoMemberCollection instructorCollection = instructorManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember instructor in instructorCollection)
			{
				ListItem i = new ListItem(instructor.ToString(), instructor.ID.ToString());
				msInstructor.Items.Add(i);
			}

			msParentSeminar.Items.Add(new ListItem("Null", "Null"));
			DojoSeminarManager parentSeminarManager = new DojoSeminarManager();
			DojoSeminarCollection parentSeminarCollection = parentSeminarManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoSeminar parentSeminar in parentSeminarCollection)
			{
				ListItem i = new ListItem(parentSeminar.ToString(), parentSeminar.ID.ToString());
				msParentSeminar.Items.Add(i);
			}

			msParentDefinition.Items.Add(new ListItem("Null", "Null"));
			DojoClassDefinitionManager parentDefinitionManager = new DojoClassDefinitionManager();
			DojoClassDefinitionCollection parentDefinitionCollection = parentDefinitionManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClassDefinition parentDefinition in parentDefinitionCollection)
			{
				ListItem i = new ListItem(parentDefinition.ToString(), parentDefinition.ID.ToString());
				msParentDefinition.Items.Add(i);
			}

			msLocation.Items.Add(new ListItem("Null", "Null"));
			GreyFoxContactManager locationManager = new GreyFoxContactManager("kitTessen_Locations");
			GreyFoxContactCollection locationCollection = locationManager.GetCollection(string.Empty, string.Empty);
			foreach(GreyFoxContact location in locationCollection)
			{
				ListItem i = new ListItem(location.ToString(), location.ID.ToString());
				msLocation.Items.Add(i);
			}

			msAccessControlGroup.Items.Add(new ListItem("Null", "Null"));
			DojoAccessControlGroupManager accessControlGroupManager = new DojoAccessControlGroupManager();
			DojoAccessControlGroupCollection accessControlGroupCollection = accessControlGroupManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAccessControlGroup accessControlGroup in accessControlGroupCollection)
			{
				ListItem i = new ListItem(accessControlGroup.ToString(), accessControlGroup.ID.ToString());
				msAccessControlGroup.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoClassID == 0)
				obj = new DojoClass();
			else
				obj = new DojoClass(dojoClassID);

			obj.Name = tbName.Text;
			obj.OccupancyMax = int.Parse(tbOccupancyMax.Text);
			obj.OccupancyTarget = int.Parse(tbOccupancyTarget.Text);
			obj.OccupancyCurrent = int.Parse(tbOccupancyCurrent.Text);
			obj.OccupancyCheckDate = DateTime.Parse(tbOccupancyCheckDate.Text);
			obj.SigninStart = DateTime.Parse(tbSigninStart.Text);
			obj.SigninEnd = DateTime.Parse(tbSigninEnd.Text);
			obj.ClassStart = DateTime.Parse(tbClassStart.Text);
			obj.ClassEnd = DateTime.Parse(tbClassEnd.Text);

			if(msInstructor.SelectedItem != null && msInstructor.SelectedItem.Value != "Null")
				obj.Instructor = DojoMember.NewPlaceHolder(
					int.Parse(msInstructor.SelectedItem.Value));
			else
				obj.Instructor = null;

			if(msParentSeminar.SelectedItem != null && msParentSeminar.SelectedItem.Value != "Null")
				obj.ParentSeminar = DojoSeminar.NewPlaceHolder(
					int.Parse(msParentSeminar.SelectedItem.Value));
			else
				obj.ParentSeminar = null;

			if(msParentDefinition.SelectedItem != null && msParentDefinition.SelectedItem.Value != "Null")
				obj.ParentDefinition = DojoClassDefinition.NewPlaceHolder(
					int.Parse(msParentDefinition.SelectedItem.Value));
			else
				obj.ParentDefinition = null;

			if(msLocation.SelectedItem != null && msLocation.SelectedItem.Value != "Null")
				obj.Location = GreyFoxContact.NewPlaceHolder("kitTessen_Locations", 
					int.Parse(msLocation.SelectedItem.Value));
			else
				obj.Location = null;

			if(msAccessControlGroup.SelectedItem != null && msAccessControlGroup.SelectedItem.Value != "Null")
				obj.AccessControlGroup = DojoAccessControlGroup.NewPlaceHolder(
					int.Parse(msAccessControlGroup.SelectedItem.Value));
			else
				obj.AccessControlGroup = null;

			if(editOnAdd)
				dojoClassID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbOccupancyMax.Text = string.Empty;
				tbOccupancyTarget.Text = string.Empty;
				tbOccupancyCurrent.Text = string.Empty;
				tbOccupancyCheckDate.Text = DateTime.Now.ToString();
				tbSigninStart.Text = DateTime.Now.ToString();
				tbSigninEnd.Text = DateTime.Now.ToString();
				tbClassStart.Text = DateTime.Now.ToString();
				tbClassEnd.Text = DateTime.Now.ToString();
				msInstructor.SelectedIndex = 0;
				msParentSeminar.SelectedIndex = 0;
				msParentDefinition.SelectedIndex = 0;
				msLocation.SelectedIndex = 0;
				msAccessControlGroup.SelectedIndex = 0;
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

			Tab OccupancyTab = new Tab("Occupancy");
			OccupancyTab.RenderDiv += new TabRenderHandler(renderOccupancyFolder);
			tabStrip.Tabs.Add(OccupancyTab);

			Tab ScheduleTab = new Tab("Schedule");
			ScheduleTab.RenderDiv += new TabRenderHandler(renderScheduleFolder);
			tabStrip.Tabs.Add(ScheduleTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoClassID > 0)
				{
					obj = new DojoClass(dojoClassID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoClassID <= 0)
				{
					obj = new DojoClass();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbName.Text = obj.Name;
				tbOccupancyMax.Text = obj.OccupancyMax.ToString();
				tbOccupancyTarget.Text = obj.OccupancyTarget.ToString();
				tbOccupancyCurrent.Text = obj.OccupancyCurrent.ToString();
				tbOccupancyCheckDate.Text = obj.OccupancyCheckDate.ToString();
				tbSigninStart.Text = obj.SigninStart.ToString();
				tbSigninEnd.Text = obj.SigninEnd.ToString();
				tbClassStart.Text = obj.ClassStart.ToString();
				tbClassEnd.Text = obj.ClassEnd.ToString();

				//
				// Set Children Selections
				//
				if(obj.Instructor != null)
					foreach(ListItem item in msInstructor.Items)
						item.Selected = obj.Instructor.ID.ToString() == item.Value;
					else
						msInstructor.SelectedIndex = 0;

				if(obj.ParentSeminar != null)
					foreach(ListItem item in msParentSeminar.Items)
						item.Selected = obj.ParentSeminar.ID.ToString() == item.Value;
					else
						msParentSeminar.SelectedIndex = 0;

				if(obj.ParentDefinition != null)
					foreach(ListItem item in msParentDefinition.Items)
						item.Selected = obj.ParentDefinition.ID.ToString() == item.Value;
					else
						msParentDefinition.SelectedIndex = 0;

				if(obj.Location != null)
					foreach(ListItem item in msLocation.Items)
						item.Selected = obj.Location.ID.ToString() == item.Value;
					else
						msLocation.SelectedIndex = 0;

				if(obj.AccessControlGroup != null)
					foreach(ListItem item in msAccessControlGroup.Items)
						item.Selected = obj.AccessControlGroup.ID.ToString() == item.Value;
					else
						msAccessControlGroup.SelectedIndex = 0;

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
			// Render ParentSeminar
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentSeminar");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msParentSeminar.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentDefinition
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentDefinition");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msParentDefinition.RenderControl(output);
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

		private void renderOccupancyFolder(HtmlTextWriter output)
		{
			//
			// Render OccupancyMax
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyMax");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbOccupancyMax.RenderControl(output);
			revOccupancyMax.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OccupancyTarget
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyTarget");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbOccupancyTarget.RenderControl(output);
			revOccupancyTarget.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OccupancyCurrent
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyCurrent");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbOccupancyCurrent.RenderControl(output);
			revOccupancyCurrent.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OccupancyCheckDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OccupancyCheckDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbOccupancyCheckDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		private void renderScheduleFolder(HtmlTextWriter output)
		{
			//
			// Render SigninStart
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Signin Start");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbSigninStart.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render SigninEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Signin End");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbSigninEnd.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ClassStart
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class Start");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbClassStart.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ClassEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class End");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbClassEnd.RenderControl(output);
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

