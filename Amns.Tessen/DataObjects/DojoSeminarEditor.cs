using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using FreeTextBoxControls;
using Amns.GreyFox.People;
using Amns.Rappahanock;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoSeminar.
	/// </summary>
	[ToolboxData("<{0}:DojoSeminarEditor runat=server></{0}:DojoSeminarEditor>")]
	public class DojoSeminarEditor : TableWindow, INamingContainer
	{
		private int dojoSeminarID;
		private DojoSeminar obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for General Folder

		private TextBox tbName = new TextBox();
		private TextBox tbStartDate = new TextBox();
		private TextBox tbEndDate = new TextBox();
		private TextBox tbDescription = new TextBox();
		private CheckBox cbIsLocal = new CheckBox();
		private MultiSelectBox msLocation = new MultiSelectBox();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Registration Folder

		private TextBox tbClassUnitType = new TextBox();
		private TextBox tbClassUnitFee = new TextBox();
		private TextBox tbBaseRegistrationFee = new TextBox();
		private CheckBox cbRegistrationEnabled = new CheckBox();
		private TextBox tbRegistrationStart = new TextBox();
		private TextBox tbFullEarlyRegistrationFee = new TextBox();
		private TextBox tbEarlyEndDate = new TextBox();
		private TextBox tbFullRegistrationFee = new TextBox();
		private TextBox tbLateStartDate = new TextBox();
		private TextBox tbFullLateRegistrationFee = new TextBox();
		private TextBox tbRegistrationEnd = new TextBox();
		private MultiSelectBox msOptions = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Details Folder

		private FreeTextBox ftbDetails = new FreeTextBox();
		private TextBox tbDetailsOverrideUrl = new TextBox();
		private TextBox tbPdfUrl = new TextBox();

		#endregion

		#region Private Control Fields for Rappahanock Folder

		private MultiSelectBox msItem = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoSeminarID
		{
			get
			{
				return dojoSeminarID;
			}
			set
			{
				loadFlag = true;
				dojoSeminarID = value;
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

			tbName.EnableViewState = false;
			Controls.Add(tbName);

			tbStartDate.EnableViewState = false;
			Controls.Add(tbStartDate);

			tbEndDate.EnableViewState = false;
			Controls.Add(tbEndDate);

			tbDescription.EnableViewState = false;
			Controls.Add(tbDescription);

			cbIsLocal.EnableViewState = false;
			Controls.Add(cbIsLocal);

			msLocation.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msLocation);

			#endregion

			#region Child Controls for Registration Folder

			tbClassUnitType.EnableViewState = false;
			Controls.Add(tbClassUnitType);

			tbClassUnitFee.EnableViewState = false;
			Controls.Add(tbClassUnitFee);

			tbBaseRegistrationFee.EnableViewState = false;
			Controls.Add(tbBaseRegistrationFee);

			cbRegistrationEnabled.EnableViewState = false;
			Controls.Add(cbRegistrationEnabled);

			tbRegistrationStart.EnableViewState = false;
			Controls.Add(tbRegistrationStart);

			tbFullEarlyRegistrationFee.EnableViewState = false;
			Controls.Add(tbFullEarlyRegistrationFee);

			tbEarlyEndDate.EnableViewState = false;
			Controls.Add(tbEarlyEndDate);

			tbFullRegistrationFee.EnableViewState = false;
			Controls.Add(tbFullRegistrationFee);

			tbLateStartDate.EnableViewState = false;
			Controls.Add(tbLateStartDate);

			tbFullLateRegistrationFee.EnableViewState = false;
			Controls.Add(tbFullLateRegistrationFee);

			tbRegistrationEnd.EnableViewState = false;
			Controls.Add(tbRegistrationEnd);

			msOptions.Mode = MultiSelectBoxMode.DualSelect;
			Controls.Add(msOptions);

			#endregion

			#region Child Controls for Details Folder

			ftbDetails.ID = this.ID + "_Details";
			ftbDetails.Width = Unit.Percentage(100);
			ftbDetails.EnableViewState = false;
			Controls.Add(ftbDetails);

			tbDetailsOverrideUrl.EnableViewState = false;
			Controls.Add(tbDetailsOverrideUrl);

			tbPdfUrl.EnableViewState = false;
			Controls.Add(tbPdfUrl);

			#endregion

			#region Child Controls for Rappahanock Folder

			msItem.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msItem);

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

			msLocation.Items.Add(new ListItem("Null", "Null"));
			GreyFoxContactManager locationManager = new GreyFoxContactManager("kitTessen_Locations");
			GreyFoxContactCollection locationCollection = locationManager.GetCollection(string.Empty, string.Empty);
			foreach(GreyFoxContact location in locationCollection)
			{
				ListItem i = new ListItem(location.ToString(), location.ID.ToString());
				msLocation.Items.Add(i);
			}

			#endregion

			#region Bind Registration Child Data

			msOptions.Items.Add(new ListItem("Null", "Null"));
			DojoSeminarOptionManager optionsManager = new DojoSeminarOptionManager();
			DojoSeminarOptionCollection optionsCollection = optionsManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoSeminarOption options in optionsCollection)
			{
				ListItem i = new ListItem(options.ToString(), options.ID.ToString());
				msOptions.Items.Add(i);
			}

			#endregion

			#region Bind Rappahanock Child Data

			msItem.Items.Add(new ListItem("Null", "Null"));
			RHItemManager itemManager = new RHItemManager();
			RHItemCollection itemCollection = itemManager.GetCollection(string.Empty, string.Empty, null);
			foreach(RHItem item in itemCollection)
			{
				ListItem i = new ListItem(item.ToString(), item.ID.ToString());
				msItem.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoSeminarID == 0)
				obj = new DojoSeminar();
			else
				obj = new DojoSeminar(dojoSeminarID);

			obj.Name = tbName.Text;
			obj.StartDate = DateTime.Parse(tbStartDate.Text);
			obj.EndDate = DateTime.Parse(tbEndDate.Text);
			obj.Description = tbDescription.Text;
			obj.IsLocal = cbIsLocal.Checked;
			obj.ClassUnitType = byte.Parse(tbClassUnitType.Text);
			obj.ClassUnitFee = decimal.Parse(tbClassUnitFee.Text);
			obj.BaseRegistrationFee = decimal.Parse(tbBaseRegistrationFee.Text);
			obj.RegistrationEnabled = cbRegistrationEnabled.Checked;
			obj.RegistrationStart = DateTime.Parse(tbRegistrationStart.Text);
			obj.FullEarlyRegistrationFee = decimal.Parse(tbFullEarlyRegistrationFee.Text);
			obj.EarlyEndDate = DateTime.Parse(tbEarlyEndDate.Text);
			obj.FullRegistrationFee = decimal.Parse(tbFullRegistrationFee.Text);
			obj.LateStartDate = DateTime.Parse(tbLateStartDate.Text);
			obj.FullLateRegistrationFee = decimal.Parse(tbFullLateRegistrationFee.Text);
			obj.RegistrationEnd = DateTime.Parse(tbRegistrationEnd.Text);
			obj.Details = ftbDetails.Text;
			obj.DetailsOverrideUrl = tbDetailsOverrideUrl.Text;
			obj.PdfUrl = tbPdfUrl.Text;

			if(msLocation.SelectedItem != null && msLocation.SelectedItem.Value != "Null")
				obj.Location = GreyFoxContact.NewPlaceHolder("kitTessen_Locations", 
					int.Parse(msLocation.SelectedItem.Value));
			else
				obj.Location = null;

			if(msOptions.IsChanged)
			{
				obj.Options = new DojoSeminarOptionCollection();
				foreach(ListItem i in msOptions.Items)
					if(i.Selected)
						obj.Options.Add(DojoSeminarOption.NewPlaceHolder(int.Parse(i.Value)));
			}

			if(msItem.SelectedItem != null && msItem.SelectedItem.Value != "Null")
				obj.Item = RHItem.NewPlaceHolder(
					int.Parse(msItem.SelectedItem.Value));
			else
				obj.Item = null;

			if(editOnAdd)
				dojoSeminarID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbStartDate.Text = DateTime.Now.ToString();
				tbEndDate.Text = DateTime.Now.ToString();
				tbDescription.Text = string.Empty;
				cbIsLocal.Checked = false;
				tbClassUnitType.Text = string.Empty;
				tbClassUnitFee.Text = string.Empty;
				tbBaseRegistrationFee.Text = string.Empty;
				cbRegistrationEnabled.Checked = false;
				tbRegistrationStart.Text = DateTime.Now.ToString();
				tbFullEarlyRegistrationFee.Text = string.Empty;
				tbEarlyEndDate.Text = DateTime.Now.ToString();
				tbFullRegistrationFee.Text = string.Empty;
				tbLateStartDate.Text = DateTime.Now.ToString();
				tbFullLateRegistrationFee.Text = string.Empty;
				tbRegistrationEnd.Text = DateTime.Now.ToString();
				ftbDetails.Text = string.Empty;
				tbDetailsOverrideUrl.Text = string.Empty;
				tbPdfUrl.Text = string.Empty;
				msLocation.SelectedIndex = 0;
				msItem.SelectedIndex = 0;
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
			GeneralTab.Visible = true;
			tabStrip.Tabs.Add(GeneralTab);

			Tab RegistrationTab = new Tab("Registration");
			RegistrationTab.RenderDiv += new TabRenderHandler(renderRegistrationFolder);
			tabStrip.Tabs.Add(RegistrationTab);

			Tab DetailsTab = new Tab("Details");
			DetailsTab.RenderDiv += new TabRenderHandler(renderDetailsFolder);
			tabStrip.Tabs.Add(DetailsTab);

			Tab RappahanockTab = new Tab("Rappahanock");
			RappahanockTab.RenderDiv += new TabRenderHandler(renderRappahanockFolder);
			tabStrip.Tabs.Add(RappahanockTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoSeminarID > 0)
				{
					obj = new DojoSeminar(dojoSeminarID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoSeminarID <= 0)
				{
					obj = new DojoSeminar();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbName.Text = obj.Name;
				tbStartDate.Text = obj.StartDate.ToString();
				tbEndDate.Text = obj.EndDate.ToString();
				tbDescription.Text = obj.Description;
				cbIsLocal.Checked = obj.IsLocal;
				tbClassUnitType.Text = obj.ClassUnitType.ToString();
				tbClassUnitFee.Text = obj.ClassUnitFee.ToString();
				tbBaseRegistrationFee.Text = obj.BaseRegistrationFee.ToString();
				cbRegistrationEnabled.Checked = obj.RegistrationEnabled;
				tbRegistrationStart.Text = obj.RegistrationStart.ToString();
				tbFullEarlyRegistrationFee.Text = obj.FullEarlyRegistrationFee.ToString();
				tbEarlyEndDate.Text = obj.EarlyEndDate.ToString();
				tbFullRegistrationFee.Text = obj.FullRegistrationFee.ToString();
				tbLateStartDate.Text = obj.LateStartDate.ToString();
				tbFullLateRegistrationFee.Text = obj.FullLateRegistrationFee.ToString();
				tbRegistrationEnd.Text = obj.RegistrationEnd.ToString();
				ftbDetails.Text = obj.Details;
				tbDetailsOverrideUrl.Text = obj.DetailsOverrideUrl;
				tbPdfUrl.Text = obj.PdfUrl;

				//
				// Set Children Selections
				//
				if(obj.Location != null)
					foreach(ListItem item in msLocation.Items)
						item.Selected = obj.Location.ID.ToString() == item.Value;
					else
						msLocation.SelectedIndex = 0;

				foreach(ListItem i in msOptions.Items)
					foreach(DojoSeminarOption dojoSeminarOption in obj.Options)
						if(i.Value == dojoSeminarOption.ID.ToString())
						{
							i.Selected = true;
							break;
						}
				if(obj.Item != null)
					foreach(ListItem item in msItem.Items)
						item.Selected = obj.Item.ID.ToString() == item.Value;
					else
						msItem.SelectedIndex = 0;

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

		private void renderGeneralFolder(HtmlTextWriter output)
		{
			//
			// Render Name
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Seminar name.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbName.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render StartDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("First date of seminar.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbStartDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render EndDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Last day of seminar.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbEndDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Description
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Seminar description.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbDescription.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsLocal
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsLocal");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsLocal.RenderControl(output);
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

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		private void renderRegistrationFolder(HtmlTextWriter output)
		{
			//
			// Render ClassUnitType
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ClassUnitType");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbClassUnitType.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ClassUnitFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class unit fee.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbClassUnitFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render BaseRegistrationFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Base registration fee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbBaseRegistrationFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RegistrationEnabled
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Enable registration.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbRegistrationEnabled.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RegistrationStart
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Registration start.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbRegistrationStart.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render FullEarlyRegistrationFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("FullEarlyRegistrationFee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbFullEarlyRegistrationFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render EarlyEndDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("EarlyEndDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbEarlyEndDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render FullRegistrationFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Full registration fee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbFullRegistrationFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render LateStartDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("LateStartDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbLateStartDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render FullLateRegistrationFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("FullLateRegistrationFee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbFullLateRegistrationFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RegistrationEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Registration end.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbRegistrationEnd.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Options
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Options");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msOptions.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderDetailsFolder(HtmlTextWriter output)
		{
			//
			// Render Details
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Details");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ftbDetails.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DetailsOverrideUrl
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Details Override URL");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbDetailsOverrideUrl.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PdfUrl
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PDF Link");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbPdfUrl.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderRappahanockFolder(HtmlTextWriter output)
		{
			//
			// Render Item
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Item");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msItem.RenderControl(output);
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
					dojoSeminarID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoSeminarID;
			return myState;
		}
	}
}

