using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoTestListJournalEntryType.
	/// </summary>
	[DefaultProperty("ConnectionString"), 
		ToolboxData("<{0}:DojoTestListJournalEntryTypeEditor runat=server></{0}:DojoTestListJournalEntryTypeEditor>")]
	public class DojoTestListJournalEntryTypeEditor : TableWindow, INamingContainer
	{
		private int dojoTestListJournalEntryTypeID;
		private DojoTestListJournalEntryType obj;
		private string connectionString;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for General Folder

		private TextBox tbName = new TextBox();
		private TextBox tbDescription = new TextBox();
		private TextBox tbOrderNum = new TextBox();
		private RegularExpressionValidator revOrderNum = new RegularExpressionValidator();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Flags Folder

		private CheckBox cbEligible = new CheckBox();
		private CheckBox cbFailed = new CheckBox();
		private CheckBox cbPassed = new CheckBox();
		private CheckBox cbCertificateRequest = new CheckBox();
		private CheckBox cbCertificatePending = new CheckBox();
		private CheckBox cbCertificateReceived = new CheckBox();
		private CheckBox cbIneligible = new CheckBox();

		#endregion

		#region Private Control Fields for Status_Changes Folder

		private MultiSelectBox msOnRemovedStatus = new MultiSelectBox();
		private MultiSelectBox msOnFailedStatus = new MultiSelectBox();
		private MultiSelectBox msOnPassedStatus = new MultiSelectBox();
		private MultiSelectBox msOnPromotedStatus = new MultiSelectBox();
		private MultiSelectBox msOnCertificateRequestedStatus = new MultiSelectBox();
		private MultiSelectBox msOnCertificatePendingStatus = new MultiSelectBox();
		private MultiSelectBox msOnCertificateReceivedStatus = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoTestListJournalEntryTypeID
		{
			get
			{
				return dojoTestListJournalEntryTypeID;
			}
			set
			{
				loadFlag = true;
				dojoTestListJournalEntryTypeID = value;
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
			bindDropDownLists();

			#region Child Controls for General Folder

			tbName.EnableViewState = false;
			Controls.Add(tbName);

			tbDescription.EnableViewState = false;
			Controls.Add(tbDescription);

			tbOrderNum.ID = this.ID + "_OrderNum";
			tbOrderNum.EnableViewState = false;
			Controls.Add(tbOrderNum);
			revOrderNum.ControlToValidate = tbOrderNum.ID;
			revOrderNum.ValidationExpression = "^(\\+|-)?\\d+$";
			revOrderNum.ErrorMessage = "*";
			revOrderNum.Display = ValidatorDisplay.Dynamic;
			revOrderNum.EnableViewState = false;
			Controls.Add(revOrderNum);

			#endregion

			#region Child Controls for Flags Folder

			cbEligible.EnableViewState = false;
			Controls.Add(cbEligible);

			cbFailed.EnableViewState = false;
			Controls.Add(cbFailed);

			cbPassed.EnableViewState = false;
			Controls.Add(cbPassed);

			cbCertificateRequest.EnableViewState = false;
			Controls.Add(cbCertificateRequest);

			cbCertificatePending.EnableViewState = false;
			Controls.Add(cbCertificatePending);

			cbCertificateReceived.EnableViewState = false;
			Controls.Add(cbCertificateReceived);

			cbIneligible.EnableViewState = false;
			Controls.Add(cbIneligible);

			#endregion

			#region Child Controls for Status Changes Folder

			msOnRemovedStatus.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msOnRemovedStatus);

			msOnFailedStatus.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msOnFailedStatus);

			msOnPassedStatus.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msOnPassedStatus);

			msOnPromotedStatus.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msOnPromotedStatus);

			msOnCertificateRequestedStatus.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msOnCertificateRequestedStatus);

			msOnCertificatePendingStatus.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msOnCertificatePendingStatus);

			msOnCertificateReceivedStatus.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msOnCertificateReceivedStatus);

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
			#region Bind Status Changes Child Data

			msOnRemovedStatus.Items.Add(new ListItem("Null", "Null"));
			DojoTestListJournalEntryTypeManager onRemovedStatusManager = new DojoTestListJournalEntryTypeManager();
			DojoTestListJournalEntryTypeCollection onRemovedStatusCollection = onRemovedStatusManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListJournalEntryType onRemovedStatus in onRemovedStatusCollection)
			{
				ListItem i = new ListItem(onRemovedStatus.ToString(), onRemovedStatus.ID.ToString());
				msOnRemovedStatus.Items.Add(i);
			}

			msOnFailedStatus.Items.Add(new ListItem("Null", "Null"));
			DojoTestListJournalEntryTypeManager onFailedStatusManager = new DojoTestListJournalEntryTypeManager();
			DojoTestListJournalEntryTypeCollection onFailedStatusCollection = onFailedStatusManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListJournalEntryType onFailedStatus in onFailedStatusCollection)
			{
				ListItem i = new ListItem(onFailedStatus.ToString(), onFailedStatus.ID.ToString());
				msOnFailedStatus.Items.Add(i);
			}

			msOnPassedStatus.Items.Add(new ListItem("Null", "Null"));
			DojoTestListJournalEntryTypeManager onPassedStatusManager = new DojoTestListJournalEntryTypeManager();
			DojoTestListJournalEntryTypeCollection onPassedStatusCollection = onPassedStatusManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListJournalEntryType onPassedStatus in onPassedStatusCollection)
			{
				ListItem i = new ListItem(onPassedStatus.ToString(), onPassedStatus.ID.ToString());
				msOnPassedStatus.Items.Add(i);
			}

			msOnPromotedStatus.Items.Add(new ListItem("Null", "Null"));
			DojoTestListJournalEntryTypeManager onPromotedStatusManager = new DojoTestListJournalEntryTypeManager();
			DojoTestListJournalEntryTypeCollection onPromotedStatusCollection = onPromotedStatusManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListJournalEntryType onPromotedStatus in onPromotedStatusCollection)
			{
				ListItem i = new ListItem(onPromotedStatus.ToString(), onPromotedStatus.ID.ToString());
				msOnPromotedStatus.Items.Add(i);
			}

			msOnCertificateRequestedStatus.Items.Add(new ListItem("Null", "Null"));
			DojoTestListJournalEntryTypeManager onCertificateRequestedStatusManager = new DojoTestListJournalEntryTypeManager();
			DojoTestListJournalEntryTypeCollection onCertificateRequestedStatusCollection = onCertificateRequestedStatusManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListJournalEntryType onCertificateRequestedStatus in onCertificateRequestedStatusCollection)
			{
				ListItem i = new ListItem(onCertificateRequestedStatus.ToString(), onCertificateRequestedStatus.ID.ToString());
				msOnCertificateRequestedStatus.Items.Add(i);
			}

			msOnCertificatePendingStatus.Items.Add(new ListItem("Null", "Null"));
			DojoTestListJournalEntryTypeManager onCertificatePendingStatusManager = new DojoTestListJournalEntryTypeManager();
			DojoTestListJournalEntryTypeCollection onCertificatePendingStatusCollection = onCertificatePendingStatusManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListJournalEntryType onCertificatePendingStatus in onCertificatePendingStatusCollection)
			{
				ListItem i = new ListItem(onCertificatePendingStatus.ToString(), onCertificatePendingStatus.ID.ToString());
				msOnCertificatePendingStatus.Items.Add(i);
			}

			msOnCertificateReceivedStatus.Items.Add(new ListItem("Null", "Null"));
			DojoTestListJournalEntryTypeManager onCertificateReceivedStatusManager = new DojoTestListJournalEntryTypeManager();
			DojoTestListJournalEntryTypeCollection onCertificateReceivedStatusCollection = onCertificateReceivedStatusManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListJournalEntryType onCertificateReceivedStatus in onCertificateReceivedStatusCollection)
			{
				ListItem i = new ListItem(onCertificateReceivedStatus.ToString(), onCertificateReceivedStatus.ID.ToString());
				msOnCertificateReceivedStatus.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoTestListJournalEntryTypeID == 0)
				obj = new DojoTestListJournalEntryType();
			else
				obj = new DojoTestListJournalEntryType(dojoTestListJournalEntryTypeID);

			obj.Name = tbName.Text;
			obj.Description = tbDescription.Text;
			obj.OrderNum = int.Parse(tbOrderNum.Text);
			obj.Eligible = cbEligible.Checked;
			obj.Failed = cbFailed.Checked;
			obj.Passed = cbPassed.Checked;
			obj.CertificateRequest = cbCertificateRequest.Checked;
			obj.CertificatePending = cbCertificatePending.Checked;
			obj.CertificateReceived = cbCertificateReceived.Checked;
			obj.Ineligible = cbIneligible.Checked;

			if(msOnRemovedStatus.SelectedItem != null && msOnRemovedStatus.SelectedItem.Value != "Null")
				obj.OnRemovedStatus = DojoTestListJournalEntryType.NewPlaceHolder(
					int.Parse(msOnRemovedStatus.SelectedItem.Value));
			else
				obj.OnRemovedStatus = null;

			if(msOnFailedStatus.SelectedItem != null && msOnFailedStatus.SelectedItem.Value != "Null")
				obj.OnFailedStatus = DojoTestListJournalEntryType.NewPlaceHolder(
					int.Parse(msOnFailedStatus.SelectedItem.Value));
			else
				obj.OnFailedStatus = null;

			if(msOnPassedStatus.SelectedItem != null && msOnPassedStatus.SelectedItem.Value != "Null")
				obj.OnPassedStatus = DojoTestListJournalEntryType.NewPlaceHolder(
					int.Parse(msOnPassedStatus.SelectedItem.Value));
			else
				obj.OnPassedStatus = null;

			if(msOnPromotedStatus.SelectedItem != null && msOnPromotedStatus.SelectedItem.Value != "Null")
				obj.OnPromotedStatus = DojoTestListJournalEntryType.NewPlaceHolder(
					int.Parse(msOnPromotedStatus.SelectedItem.Value));
			else
				obj.OnPromotedStatus = null;

			if(msOnCertificateRequestedStatus.SelectedItem != null && msOnCertificateRequestedStatus.SelectedItem.Value != "Null")
				obj.OnCertificateRequestedStatus = DojoTestListJournalEntryType.NewPlaceHolder(
					int.Parse(msOnCertificateRequestedStatus.SelectedItem.Value));
			else
				obj.OnCertificateRequestedStatus = null;

			if(msOnCertificatePendingStatus.SelectedItem != null && msOnCertificatePendingStatus.SelectedItem.Value != "Null")
				obj.OnCertificatePendingStatus = DojoTestListJournalEntryType.NewPlaceHolder(
					int.Parse(msOnCertificatePendingStatus.SelectedItem.Value));
			else
				obj.OnCertificatePendingStatus = null;

			if(msOnCertificateReceivedStatus.SelectedItem != null && msOnCertificateReceivedStatus.SelectedItem.Value != "Null")
				obj.OnCertificateReceivedStatus = DojoTestListJournalEntryType.NewPlaceHolder(
					int.Parse(msOnCertificateReceivedStatus.SelectedItem.Value));
			else
				obj.OnCertificateReceivedStatus = null;

			if(editOnAdd)
				dojoTestListJournalEntryTypeID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbDescription.Text = string.Empty;
				tbOrderNum.Text = string.Empty;
				cbEligible.Checked = false;
				cbFailed.Checked = false;
				cbPassed.Checked = false;
				cbCertificateRequest.Checked = false;
				cbCertificatePending.Checked = false;
				cbCertificateReceived.Checked = false;
				cbIneligible.Checked = false;
				msOnRemovedStatus.SelectedIndex = 0;
				msOnFailedStatus.SelectedIndex = 0;
				msOnPassedStatus.SelectedIndex = 0;
				msOnPromotedStatus.SelectedIndex = 0;
				msOnCertificateRequestedStatus.SelectedIndex = 0;
				msOnCertificatePendingStatus.SelectedIndex = 0;
				msOnCertificateReceivedStatus.SelectedIndex = 0;
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

			Tab FlagsTab = new Tab("Flags");
			FlagsTab.RenderDiv += new TabRenderHandler(renderFlagsFolder);
			tabStrip.Tabs.Add(FlagsTab);

			Tab Status_ChangesTab = new Tab("Status Changes");
			Status_ChangesTab.RenderDiv += new TabRenderHandler(renderStatus_ChangesFolder);
			tabStrip.Tabs.Add(Status_ChangesTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoTestListJournalEntryTypeID > 0)
				{
					obj = new DojoTestListJournalEntryType(dojoTestListJournalEntryTypeID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoTestListJournalEntryTypeID <= 0)
				{
					obj = new DojoTestListJournalEntryType();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbName.Text = obj.Name;
				tbDescription.Text = obj.Description;
				tbOrderNum.Text = obj.OrderNum.ToString();
				cbEligible.Checked = obj.Eligible;
				cbFailed.Checked = obj.Failed;
				cbPassed.Checked = obj.Passed;
				cbCertificateRequest.Checked = obj.CertificateRequest;
				cbCertificatePending.Checked = obj.CertificatePending;
				cbCertificateReceived.Checked = obj.CertificateReceived;
				cbIneligible.Checked = obj.Ineligible;

				//
				// Set Children Selections
				//
				if(obj.OnRemovedStatus != null)
					foreach(ListItem item in msOnRemovedStatus.Items)
						item.Selected = obj.OnRemovedStatus.ID.ToString() == item.Value;
					else
						msOnRemovedStatus.SelectedIndex = 0;

				if(obj.OnFailedStatus != null)
					foreach(ListItem item in msOnFailedStatus.Items)
						item.Selected = obj.OnFailedStatus.ID.ToString() == item.Value;
					else
						msOnFailedStatus.SelectedIndex = 0;

				if(obj.OnPassedStatus != null)
					foreach(ListItem item in msOnPassedStatus.Items)
						item.Selected = obj.OnPassedStatus.ID.ToString() == item.Value;
					else
						msOnPassedStatus.SelectedIndex = 0;

				if(obj.OnPromotedStatus != null)
					foreach(ListItem item in msOnPromotedStatus.Items)
						item.Selected = obj.OnPromotedStatus.ID.ToString() == item.Value;
					else
						msOnPromotedStatus.SelectedIndex = 0;

				if(obj.OnCertificateRequestedStatus != null)
					foreach(ListItem item in msOnCertificateRequestedStatus.Items)
						item.Selected = obj.OnCertificateRequestedStatus.ID.ToString() == item.Value;
					else
						msOnCertificateRequestedStatus.SelectedIndex = 0;

				if(obj.OnCertificatePendingStatus != null)
					foreach(ListItem item in msOnCertificatePendingStatus.Items)
						item.Selected = obj.OnCertificatePendingStatus.ID.ToString() == item.Value;
					else
						msOnCertificatePendingStatus.SelectedIndex = 0;

				if(obj.OnCertificateReceivedStatus != null)
					foreach(ListItem item in msOnCertificateReceivedStatus.Items)
						item.Selected = obj.OnCertificateReceivedStatus.ID.ToString() == item.Value;
					else
						msOnCertificateReceivedStatus.SelectedIndex = 0;

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
			output.Write("Name");
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
			// Render OrderNum
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OrderNum");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbOrderNum.RenderControl(output);
			revOrderNum.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		private void renderFlagsFolder(HtmlTextWriter output)
		{
			//
			// Render Eligible
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Eligible");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbEligible.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Failed
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Failed");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbFailed.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Passed
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Passed");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbPassed.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render CertificateRequest
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CertificateRequest");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbCertificateRequest.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render CertificatePending
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CertificatePending");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbCertificatePending.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render CertificateReceived
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CertificateReceived");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbCertificateReceived.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Ineligible
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Ineligible");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIneligible.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderStatus_ChangesFolder(HtmlTextWriter output)
		{
			//
			// Render OnRemovedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnRemovedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msOnRemovedStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnFailedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnFailedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msOnFailedStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnPassedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnPassedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msOnPassedStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnPromotedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnPromotedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msOnPromotedStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnCertificateRequestedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnCertificateRequestedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msOnCertificateRequestedStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnCertificatePendingStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnCertificatePendingStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msOnCertificatePendingStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnCertificateReceivedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnCertificateReceivedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msOnCertificateReceivedStatus.RenderControl(output);
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
					dojoTestListJournalEntryTypeID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoTestListJournalEntryTypeID;
			return myState;
		}
	}
}

