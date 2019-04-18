using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoSeminarReservation.
	/// </summary>
	[ToolboxData("<{0}:DojoSeminarReservationEditor runat=server></{0}:DojoSeminarReservationEditor>")]
	public class DojoSeminarReservationEditor : TableWindow, INamingContainer
	{
		private int dojoSeminarReservationID;
		private DojoSeminarReservation obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for General Folder

		private MultiSelectBox msRegistration = new MultiSelectBox();
		private MultiSelectBox msParentReservation = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Dates Folder

		private CheckBox cbIsBlockReservation = new CheckBox();
		private TextBox tbCheckIn = new TextBox();
		private CheckBox cbCheckOut = new CheckBox();

		#endregion

		#region Private Control Fields for Classes Folder

		private CheckBox cbIsClassReservation = new CheckBox();
		private MultiSelectBox msClass1 = new MultiSelectBox();
		private MultiSelectBox msClass2 = new MultiSelectBox();
		private MultiSelectBox msClass3 = new MultiSelectBox();
		private MultiSelectBox msClass4 = new MultiSelectBox();
		private MultiSelectBox msClass5 = new MultiSelectBox();
		private MultiSelectBox msClass6 = new MultiSelectBox();
		private MultiSelectBox msClass7 = new MultiSelectBox();
		private MultiSelectBox msClass8 = new MultiSelectBox();
		private MultiSelectBox msClass9 = new MultiSelectBox();
		private MultiSelectBox msClass10 = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Definitions Folder

		private CheckBox cbIsDefinitionReservation = new CheckBox();
		private MultiSelectBox msDefinition1 = new MultiSelectBox();
		private MultiSelectBox msDefinition2 = new MultiSelectBox();
		private MultiSelectBox msDefinition3 = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoSeminarReservationID
		{
			get
			{
				return dojoSeminarReservationID;
			}
			set
			{
				loadFlag = true;
				dojoSeminarReservationID = value;
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

			#region Child Controls for General Folder

			msRegistration.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msRegistration);

			msParentReservation.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msParentReservation);

			#endregion

			#region Child Controls for Dates Folder

			cbIsBlockReservation.EnableViewState = false;
			Controls.Add(cbIsBlockReservation);

			tbCheckIn.EnableViewState = false;
			Controls.Add(tbCheckIn);

			cbCheckOut.EnableViewState = false;
			Controls.Add(cbCheckOut);

			#endregion

			#region Child Controls for Classes Folder

			cbIsClassReservation.EnableViewState = false;
			Controls.Add(cbIsClassReservation);

			msClass1.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msClass1);

			msClass2.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msClass2);

			msClass3.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msClass3);

			msClass4.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msClass4);

			msClass5.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msClass5);

			msClass6.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msClass6);

			msClass7.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msClass7);

			msClass8.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msClass8);

			msClass9.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msClass9);

			msClass10.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msClass10);

			#endregion

			#region Child Controls for Definitions Folder

			cbIsDefinitionReservation.EnableViewState = false;
			Controls.Add(cbIsDefinitionReservation);

			msDefinition1.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msDefinition1);

			msDefinition2.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msDefinition2);

			msDefinition3.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msDefinition3);

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
			#region Bind General Child Data

			msRegistration.Items.Add(new ListItem("Null", "Null"));
			DojoSeminarRegistrationManager registrationManager = new DojoSeminarRegistrationManager();
			DojoSeminarRegistrationCollection registrationCollection = registrationManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoSeminarRegistration registration in registrationCollection)
			{
				ListItem i = new ListItem(registration.ToString(), registration.ID.ToString());
				msRegistration.Items.Add(i);
			}

			msParentReservation.Items.Add(new ListItem("Null", "Null"));
			DojoSeminarReservationManager parentReservationManager = new DojoSeminarReservationManager();
			DojoSeminarReservationCollection parentReservationCollection = parentReservationManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoSeminarReservation parentReservation in parentReservationCollection)
			{
				ListItem i = new ListItem(parentReservation.ToString(), parentReservation.ID.ToString());
				msParentReservation.Items.Add(i);
			}

			#endregion

			#region Bind Classes Child Data

			msClass1.Items.Add(new ListItem("Null", "Null"));
			DojoClassManager class1Manager = new DojoClassManager();
			DojoClassCollection class1Collection = class1Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClass class1 in class1Collection)
			{
				ListItem i = new ListItem(class1.ToString(), class1.ID.ToString());
				msClass1.Items.Add(i);
			}

			msClass2.Items.Add(new ListItem("Null", "Null"));
			DojoClassManager class2Manager = new DojoClassManager();
			DojoClassCollection class2Collection = class2Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClass class2 in class2Collection)
			{
				ListItem i = new ListItem(class2.ToString(), class2.ID.ToString());
				msClass2.Items.Add(i);
			}

			msClass3.Items.Add(new ListItem("Null", "Null"));
			DojoClassManager class3Manager = new DojoClassManager();
			DojoClassCollection class3Collection = class3Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClass class3 in class3Collection)
			{
				ListItem i = new ListItem(class3.ToString(), class3.ID.ToString());
				msClass3.Items.Add(i);
			}

			msClass4.Items.Add(new ListItem("Null", "Null"));
			DojoClassManager class4Manager = new DojoClassManager();
			DojoClassCollection class4Collection = class4Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClass class4 in class4Collection)
			{
				ListItem i = new ListItem(class4.ToString(), class4.ID.ToString());
				msClass4.Items.Add(i);
			}

			msClass5.Items.Add(new ListItem("Null", "Null"));
			DojoClassManager class5Manager = new DojoClassManager();
			DojoClassCollection class5Collection = class5Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClass class5 in class5Collection)
			{
				ListItem i = new ListItem(class5.ToString(), class5.ID.ToString());
				msClass5.Items.Add(i);
			}

			msClass6.Items.Add(new ListItem("Null", "Null"));
			DojoClassManager class6Manager = new DojoClassManager();
			DojoClassCollection class6Collection = class6Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClass class6 in class6Collection)
			{
				ListItem i = new ListItem(class6.ToString(), class6.ID.ToString());
				msClass6.Items.Add(i);
			}

			msClass7.Items.Add(new ListItem("Null", "Null"));
			DojoClassManager class7Manager = new DojoClassManager();
			DojoClassCollection class7Collection = class7Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClass class7 in class7Collection)
			{
				ListItem i = new ListItem(class7.ToString(), class7.ID.ToString());
				msClass7.Items.Add(i);
			}

			msClass8.Items.Add(new ListItem("Null", "Null"));
			DojoClassManager class8Manager = new DojoClassManager();
			DojoClassCollection class8Collection = class8Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClass class8 in class8Collection)
			{
				ListItem i = new ListItem(class8.ToString(), class8.ID.ToString());
				msClass8.Items.Add(i);
			}

			msClass9.Items.Add(new ListItem("Null", "Null"));
			DojoClassManager class9Manager = new DojoClassManager();
			DojoClassCollection class9Collection = class9Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClass class9 in class9Collection)
			{
				ListItem i = new ListItem(class9.ToString(), class9.ID.ToString());
				msClass9.Items.Add(i);
			}

			msClass10.Items.Add(new ListItem("Null", "Null"));
			DojoClassManager class10Manager = new DojoClassManager();
			DojoClassCollection class10Collection = class10Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClass class10 in class10Collection)
			{
				ListItem i = new ListItem(class10.ToString(), class10.ID.ToString());
				msClass10.Items.Add(i);
			}

			#endregion

			#region Bind Definitions Child Data

			msDefinition1.Items.Add(new ListItem("Null", "Null"));
			DojoClassDefinitionManager definition1Manager = new DojoClassDefinitionManager();
			DojoClassDefinitionCollection definition1Collection = definition1Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClassDefinition definition1 in definition1Collection)
			{
				ListItem i = new ListItem(definition1.ToString(), definition1.ID.ToString());
				msDefinition1.Items.Add(i);
			}

			msDefinition2.Items.Add(new ListItem("Null", "Null"));
			DojoClassDefinitionManager definition2Manager = new DojoClassDefinitionManager();
			DojoClassDefinitionCollection definition2Collection = definition2Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClassDefinition definition2 in definition2Collection)
			{
				ListItem i = new ListItem(definition2.ToString(), definition2.ID.ToString());
				msDefinition2.Items.Add(i);
			}

			msDefinition3.Items.Add(new ListItem("Null", "Null"));
			DojoClassDefinitionManager definition3Manager = new DojoClassDefinitionManager();
			DojoClassDefinitionCollection definition3Collection = definition3Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClassDefinition definition3 in definition3Collection)
			{
				ListItem i = new ListItem(definition3.ToString(), definition3.ID.ToString());
				msDefinition3.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoSeminarReservationID == 0)
				obj = new DojoSeminarReservation();
			else
				obj = new DojoSeminarReservation(dojoSeminarReservationID);

			obj.IsBlockReservation = cbIsBlockReservation.Checked;
			obj.CheckIn = DateTime.Parse(tbCheckIn.Text);
			obj.CheckOut = cbCheckOut.Checked;
			obj.IsClassReservation = cbIsClassReservation.Checked;
			obj.IsDefinitionReservation = cbIsDefinitionReservation.Checked;

			if(msRegistration.SelectedItem != null && msRegistration.SelectedItem.Value != "Null")
				obj.Registration = DojoSeminarRegistration.NewPlaceHolder(
					int.Parse(msRegistration.SelectedItem.Value));
			else
				obj.Registration = null;

			if(msParentReservation.SelectedItem != null && msParentReservation.SelectedItem.Value != "Null")
				obj.ParentReservation = DojoSeminarReservation.NewPlaceHolder(
					int.Parse(msParentReservation.SelectedItem.Value));
			else
				obj.ParentReservation = null;

			if(msClass1.SelectedItem != null && msClass1.SelectedItem.Value != "Null")
				obj.Class1 = DojoClass.NewPlaceHolder(
					int.Parse(msClass1.SelectedItem.Value));
			else
				obj.Class1 = null;

			if(msClass2.SelectedItem != null && msClass2.SelectedItem.Value != "Null")
				obj.Class2 = DojoClass.NewPlaceHolder(
					int.Parse(msClass2.SelectedItem.Value));
			else
				obj.Class2 = null;

			if(msClass3.SelectedItem != null && msClass3.SelectedItem.Value != "Null")
				obj.Class3 = DojoClass.NewPlaceHolder(
					int.Parse(msClass3.SelectedItem.Value));
			else
				obj.Class3 = null;

			if(msClass4.SelectedItem != null && msClass4.SelectedItem.Value != "Null")
				obj.Class4 = DojoClass.NewPlaceHolder(
					int.Parse(msClass4.SelectedItem.Value));
			else
				obj.Class4 = null;

			if(msClass5.SelectedItem != null && msClass5.SelectedItem.Value != "Null")
				obj.Class5 = DojoClass.NewPlaceHolder(
					int.Parse(msClass5.SelectedItem.Value));
			else
				obj.Class5 = null;

			if(msClass6.SelectedItem != null && msClass6.SelectedItem.Value != "Null")
				obj.Class6 = DojoClass.NewPlaceHolder(
					int.Parse(msClass6.SelectedItem.Value));
			else
				obj.Class6 = null;

			if(msClass7.SelectedItem != null && msClass7.SelectedItem.Value != "Null")
				obj.Class7 = DojoClass.NewPlaceHolder(
					int.Parse(msClass7.SelectedItem.Value));
			else
				obj.Class7 = null;

			if(msClass8.SelectedItem != null && msClass8.SelectedItem.Value != "Null")
				obj.Class8 = DojoClass.NewPlaceHolder(
					int.Parse(msClass8.SelectedItem.Value));
			else
				obj.Class8 = null;

			if(msClass9.SelectedItem != null && msClass9.SelectedItem.Value != "Null")
				obj.Class9 = DojoClass.NewPlaceHolder(
					int.Parse(msClass9.SelectedItem.Value));
			else
				obj.Class9 = null;

			if(msClass10.SelectedItem != null && msClass10.SelectedItem.Value != "Null")
				obj.Class10 = DojoClass.NewPlaceHolder(
					int.Parse(msClass10.SelectedItem.Value));
			else
				obj.Class10 = null;

			if(msDefinition1.SelectedItem != null && msDefinition1.SelectedItem.Value != "Null")
				obj.Definition1 = DojoClassDefinition.NewPlaceHolder(
					int.Parse(msDefinition1.SelectedItem.Value));
			else
				obj.Definition1 = null;

			if(msDefinition2.SelectedItem != null && msDefinition2.SelectedItem.Value != "Null")
				obj.Definition2 = DojoClassDefinition.NewPlaceHolder(
					int.Parse(msDefinition2.SelectedItem.Value));
			else
				obj.Definition2 = null;

			if(msDefinition3.SelectedItem != null && msDefinition3.SelectedItem.Value != "Null")
				obj.Definition3 = DojoClassDefinition.NewPlaceHolder(
					int.Parse(msDefinition3.SelectedItem.Value));
			else
				obj.Definition3 = null;

			if(editOnAdd)
				dojoSeminarReservationID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				cbIsBlockReservation.Checked = false;
				tbCheckIn.Text = DateTime.Now.ToString();
				cbCheckOut.Checked = false;
				cbIsClassReservation.Checked = false;
				cbIsDefinitionReservation.Checked = false;
				msRegistration.SelectedIndex = 0;
				msParentReservation.SelectedIndex = 0;
				msClass1.SelectedIndex = 0;
				msClass2.SelectedIndex = 0;
				msClass3.SelectedIndex = 0;
				msClass4.SelectedIndex = 0;
				msClass5.SelectedIndex = 0;
				msClass6.SelectedIndex = 0;
				msClass7.SelectedIndex = 0;
				msClass8.SelectedIndex = 0;
				msClass9.SelectedIndex = 0;
				msClass10.SelectedIndex = 0;
				msDefinition1.SelectedIndex = 0;
				msDefinition2.SelectedIndex = 0;
				msDefinition3.SelectedIndex = 0;
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

			Tab GeneralTab = new Tab("General");
			GeneralTab.Visible = true;
			GeneralTab.RenderDiv += new TabRenderHandler(renderGeneralFolder);
			tabStrip.Tabs.Add(GeneralTab);

			Tab DatesTab = new Tab("Dates");
			DatesTab.RenderDiv += new TabRenderHandler(renderDatesFolder);
			tabStrip.Tabs.Add(DatesTab);

			Tab ClassesTab = new Tab("Classes");
			ClassesTab.RenderDiv += new TabRenderHandler(renderClassesFolder);
			tabStrip.Tabs.Add(ClassesTab);

			Tab DefinitionsTab = new Tab("Definitions");
			DefinitionsTab.RenderDiv += new TabRenderHandler(renderDefinitionsFolder);
			tabStrip.Tabs.Add(DefinitionsTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoSeminarReservationID > 0)
				{
					obj = new DojoSeminarReservation(dojoSeminarReservationID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoSeminarReservationID <= 0)
				{
					obj = new DojoSeminarReservation();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				cbIsBlockReservation.Checked = obj.IsBlockReservation;
				tbCheckIn.Text = obj.CheckIn.ToString();
				cbCheckOut.Checked = obj.CheckOut;
				cbIsClassReservation.Checked = obj.IsClassReservation;
				cbIsDefinitionReservation.Checked = obj.IsDefinitionReservation;

				//
				// Set Children Selections
				//
				if(obj.Registration != null)
					foreach(ListItem item in msRegistration.Items)
						item.Selected = obj.Registration.ID.ToString() == item.Value;
					else
						msRegistration.SelectedIndex = 0;

				if(obj.ParentReservation != null)
					foreach(ListItem item in msParentReservation.Items)
						item.Selected = obj.ParentReservation.ID.ToString() == item.Value;
					else
						msParentReservation.SelectedIndex = 0;

				if(obj.Class1 != null)
					foreach(ListItem item in msClass1.Items)
						item.Selected = obj.Class1.ID.ToString() == item.Value;
					else
						msClass1.SelectedIndex = 0;

				if(obj.Class2 != null)
					foreach(ListItem item in msClass2.Items)
						item.Selected = obj.Class2.ID.ToString() == item.Value;
					else
						msClass2.SelectedIndex = 0;

				if(obj.Class3 != null)
					foreach(ListItem item in msClass3.Items)
						item.Selected = obj.Class3.ID.ToString() == item.Value;
					else
						msClass3.SelectedIndex = 0;

				if(obj.Class4 != null)
					foreach(ListItem item in msClass4.Items)
						item.Selected = obj.Class4.ID.ToString() == item.Value;
					else
						msClass4.SelectedIndex = 0;

				if(obj.Class5 != null)
					foreach(ListItem item in msClass5.Items)
						item.Selected = obj.Class5.ID.ToString() == item.Value;
					else
						msClass5.SelectedIndex = 0;

				if(obj.Class6 != null)
					foreach(ListItem item in msClass6.Items)
						item.Selected = obj.Class6.ID.ToString() == item.Value;
					else
						msClass6.SelectedIndex = 0;

				if(obj.Class7 != null)
					foreach(ListItem item in msClass7.Items)
						item.Selected = obj.Class7.ID.ToString() == item.Value;
					else
						msClass7.SelectedIndex = 0;

				if(obj.Class8 != null)
					foreach(ListItem item in msClass8.Items)
						item.Selected = obj.Class8.ID.ToString() == item.Value;
					else
						msClass8.SelectedIndex = 0;

				if(obj.Class9 != null)
					foreach(ListItem item in msClass9.Items)
						item.Selected = obj.Class9.ID.ToString() == item.Value;
					else
						msClass9.SelectedIndex = 0;

				if(obj.Class10 != null)
					foreach(ListItem item in msClass10.Items)
						item.Selected = obj.Class10.ID.ToString() == item.Value;
					else
						msClass10.SelectedIndex = 0;

				if(obj.Definition1 != null)
					foreach(ListItem item in msDefinition1.Items)
						item.Selected = obj.Definition1.ID.ToString() == item.Value;
					else
						msDefinition1.SelectedIndex = 0;

				if(obj.Definition2 != null)
					foreach(ListItem item in msDefinition2.Items)
						item.Selected = obj.Definition2.ID.ToString() == item.Value;
					else
						msDefinition2.SelectedIndex = 0;

				if(obj.Definition3 != null)
					foreach(ListItem item in msDefinition3.Items)
						item.Selected = obj.Definition3.ID.ToString() == item.Value;
					else
						msDefinition3.SelectedIndex = 0;

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

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		private void renderGeneralFolder(HtmlTextWriter output)
		{
			//
			// Render Registration
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Registration");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msRegistration.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentReservation
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentReservation");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msParentReservation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderDatesFolder(HtmlTextWriter output)
		{
			//
			// Render IsBlockReservation
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsBlockReservation");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsBlockReservation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render CheckIn
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CheckIn");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbCheckIn.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render CheckOut
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CheckOut");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbCheckOut.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderClassesFolder(HtmlTextWriter output)
		{
			//
			// Render IsClassReservation
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsClassReservation");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsClassReservation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClass1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClass2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClass3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class4
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class4");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClass4.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class5
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class5");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClass5.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class6
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class6");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClass6.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class7
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class7");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClass7.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class8
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class8");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClass8.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class9
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class9");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClass9.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class10
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class10");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClass10.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderDefinitionsFolder(HtmlTextWriter output)
		{
			//
			// Render IsDefinitionReservation
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsDefinitionReservation");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsDefinitionReservation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Definition1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Definition1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msDefinition1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Definition2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Definition2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msDefinition2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Definition3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Definition3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msDefinition3.RenderControl(output);
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
	}
}

