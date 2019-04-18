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
	/// Default web editor for DojoOrganization.
	/// </summary>
	[ToolboxData("<{0}:DojoOrganizationEditor runat=server></{0}:DojoOrganizationEditor>")]
	public class DojoOrganizationEditor : TableWindow, INamingContainer
	{
		private int dojoOrganizationID;
		private DojoOrganization obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for Default Folder

		private TextBox tbName = new TextBox();
		private TextBox tbDescription = new TextBox();
		private MultiSelectBox msLocation = new MultiSelectBox();
		private MultiSelectBox msClassLocations = new MultiSelectBox();
		private MultiSelectBox msAdministrativeContact = new MultiSelectBox();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Web_Services Folder

		private TextBox tbWebServiceUrl = new TextBox();
		private TextBox tbRefreshTime = new TextBox();

		#endregion

		#region Private Control Fields for System Folder

		private CheckBox cbIsPrimary = new CheckBox();

		#endregion

		#region Private Control Fields for Membership Folder

		private CheckBox cbPromotionFlagEnabled = new CheckBox();
		private MultiSelectBox msDefaultMemberType = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoOrganizationID
		{
			get
			{
				return dojoOrganizationID;
			}
			set
			{
				loadFlag = true;
				dojoOrganizationID = value;
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

			msLocation.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msLocation);

			msClassLocations.Mode = MultiSelectBoxMode.DualSelect;
			Controls.Add(msClassLocations);

			msAdministrativeContact.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msAdministrativeContact);

			#endregion

			#region Child Controls for Web Services Folder

			tbWebServiceUrl.EnableViewState = false;
			Controls.Add(tbWebServiceUrl);

			tbRefreshTime.EnableViewState = false;
			Controls.Add(tbRefreshTime);

			#endregion

			#region Child Controls for System Folder

			cbIsPrimary.EnableViewState = false;
			Controls.Add(cbIsPrimary);

			#endregion

			#region Child Controls for Membership Folder

			msDefaultMemberType.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msDefaultMemberType);

			cbPromotionFlagEnabled.EnableViewState = false;
			Controls.Add(cbPromotionFlagEnabled);

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

			msLocation.Items.Add(new ListItem("Null", "Null"));
			GreyFoxContactManager locationManager = new GreyFoxContactManager("kitTessen_Locations");
			GreyFoxContactCollection locationCollection = locationManager.GetCollection(string.Empty, string.Empty);
			foreach(GreyFoxContact location in locationCollection)
			{
				ListItem i = new ListItem(location.ToString(), location.ID.ToString());
				msLocation.Items.Add(i);
			}

			msClassLocations.Items.Add(new ListItem("Null", "Null"));
			GreyFoxContactManager classLocationsManager = new GreyFoxContactManager("kitTessen_Locations");
			GreyFoxContactCollection classLocationsCollection = classLocationsManager.GetCollection(string.Empty, string.Empty);
			foreach(GreyFoxContact classLocations in classLocationsCollection)
			{
				ListItem i = new ListItem(classLocations.ToString(), classLocations.ID.ToString());
				msClassLocations.Items.Add(i);
			}

			msAdministrativeContact.Items.Add(new ListItem("Null", "Null"));
			GreyFoxContactManager administrativeContactManager = new GreyFoxContactManager("sysGlobal_Contacts");
			GreyFoxContactCollection administrativeContactCollection = administrativeContactManager.GetCollection(string.Empty, string.Empty);
			foreach(GreyFoxContact administrativeContact in administrativeContactCollection)
			{
				ListItem i = new ListItem(administrativeContact.ToString(), administrativeContact.ID.ToString());
				msAdministrativeContact.Items.Add(i);
			}

			#endregion

			#region Bind Membership Child Data

			msDefaultMemberType.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager defaultMemberTypeManager = new DojoMemberTypeManager();
			DojoMemberTypeCollection defaultMemberTypeCollection = defaultMemberTypeManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType defaultMemberType in defaultMemberTypeCollection)
			{
				ListItem i = new ListItem(defaultMemberType.ToString(), defaultMemberType.ID.ToString());
				msDefaultMemberType.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoOrganizationID == 0)
				obj = new DojoOrganization();
			else
				obj = new DojoOrganization(dojoOrganizationID);

			obj.Name = tbName.Text;
			obj.Description = tbDescription.Text;
			obj.WebServiceUrl = tbWebServiceUrl.Text;
			obj.RefreshTime = TimeSpan.Parse(tbRefreshTime.Text);
			obj.IsPrimary = cbIsPrimary.Checked;
			obj.PromotionFlagEnabled = cbPromotionFlagEnabled.Checked;

			if(msLocation.SelectedItem != null && msLocation.SelectedItem.Value != "Null")
				obj.Location = GreyFoxContact.NewPlaceHolder("kitTessen_Locations", 
					int.Parse(msLocation.SelectedItem.Value));
			else
				obj.Location = null;

			if(msClassLocations.IsChanged)
			{
				obj.ClassLocations = new GreyFoxContactCollection();
				foreach(ListItem i in msClassLocations.Items)
					if(i.Selected)
						obj.ClassLocations.Add(GreyFoxContact.NewPlaceHolder("kitTessen_Locations", int.Parse(i.Value)));
			}

			if(msAdministrativeContact.SelectedItem != null && msAdministrativeContact.SelectedItem.Value != "Null")
				obj.AdministrativeContact = GreyFoxContact.NewPlaceHolder("sysGlobal_Contacts", 
					int.Parse(msAdministrativeContact.SelectedItem.Value));
			else
				obj.AdministrativeContact = null;

			if(msDefaultMemberType.SelectedItem != null && msDefaultMemberType.SelectedItem.Value != "Null")
				obj.DefaultMemberType = DojoMemberType.NewPlaceHolder(
					int.Parse(msDefaultMemberType.SelectedItem.Value));
			else
				obj.DefaultMemberType = null;

			if(editOnAdd)
				dojoOrganizationID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbDescription.Text = string.Empty;
				tbWebServiceUrl.Text = string.Empty;
				tbRefreshTime.Text = string.Empty;
				cbIsPrimary.Checked = false;
				cbPromotionFlagEnabled.Checked = false;
				msLocation.SelectedIndex = 0;
				msAdministrativeContact.SelectedIndex = 0;
				msDefaultMemberType.SelectedIndex = 0;
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

			Tab Web_ServicesTab = new Tab("Web Services");
			Web_ServicesTab.RenderDiv += new TabRenderHandler(renderWeb_ServicesFolder);
			tabStrip.Tabs.Add(Web_ServicesTab);

			Tab SystemTab = new Tab("System");
			SystemTab.RenderDiv += new TabRenderHandler(renderSystemFolder);
			tabStrip.Tabs.Add(SystemTab);

			Tab MembershipTab = new Tab("Membership");
			MembershipTab.RenderDiv += new TabRenderHandler(renderMembershipFolder);
			tabStrip.Tabs.Add(MembershipTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoOrganizationID > 0)
				{
					obj = new DojoOrganization(dojoOrganizationID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoOrganizationID <= 0)
				{
					obj = new DojoOrganization();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbName.Text = obj.Name;
				tbDescription.Text = obj.Description;
				tbWebServiceUrl.Text = obj.WebServiceUrl;
				tbRefreshTime.Text = obj.RefreshTime.ToString();
				cbIsPrimary.Checked = obj.IsPrimary;
				cbPromotionFlagEnabled.Checked = obj.PromotionFlagEnabled;

				//
				// Set Children Selections
				//
				if(obj.Location != null)
					foreach(ListItem item in msLocation.Items)
						item.Selected = obj.Location.ID.ToString() == item.Value;
					else
						msLocation.SelectedIndex = 0;

				foreach(ListItem i in msClassLocations.Items)
					foreach(GreyFoxContact greyFoxContact in obj.ClassLocations)
						if(i.Value == greyFoxContact.ID.ToString())
						{
							i.Selected = true;
							break;
						}
				if(obj.AdministrativeContact != null)
					foreach(ListItem item in msAdministrativeContact.Items)
						item.Selected = obj.AdministrativeContact.ID.ToString() == item.Value;
					else
						msAdministrativeContact.SelectedIndex = 0;

				if(obj.DefaultMemberType != null)
					foreach(ListItem item in msDefaultMemberType.Items)
						item.Selected = obj.DefaultMemberType.ID.ToString() == item.Value;
					else
						msDefaultMemberType.SelectedIndex = 0;

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
			output.Write("Name of Organization");
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
			output.Write("Description");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbDescription.RenderControl(output);
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
			// Render ClassLocations
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ClassLocations");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClassLocations.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AdministrativeContact
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AdministrativeContact");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msAdministrativeContact.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		private void renderWeb_ServicesFolder(HtmlTextWriter output)
		{
			//
			// Render WebServiceUrl
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Webservice URL for external Tessen connections.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbWebServiceUrl.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RefreshTime
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("RefreshTime");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbRefreshTime.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderSystemFolder(HtmlTextWriter output)
		{
			//
			// Render IsPrimary
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Primary Organization?");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsPrimary.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderMembershipFolder(HtmlTextWriter output)
		{
			//
			// Render DefaultMemberType
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DefaultMemberType");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msDefaultMemberType.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PromotionFlagEnabled
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Activates promotion limiting.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbPromotionFlagEnabled.RenderControl(output);
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
					dojoOrganizationID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoOrganizationID;
			return myState;
		}
	}
}

