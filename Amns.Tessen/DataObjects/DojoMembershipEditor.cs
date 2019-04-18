using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using Amns.GreyFox.Web.UI.WebControls;
using System.Web.UI.WebControls;
using Amns.Rappahanock;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoMembership.
	/// </summary>
	[ToolboxData("<{0}:DojoMembershipEditor runat=server></{0}:DojoMembershipEditor>")]
	public class DojoMembershipEditor : TableWindow, INamingContainer
	{
		private int dojoMembershipID;
		private DojoMembership obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for Default Folder

		private DateEditor deStartDate = new DateEditor();
		private DateEditor deEndDate = new DateEditor();
		private TextBox tbOrganizationMemberID = new TextBox();
		private MultiSelectBox msMember = new MultiSelectBox();
		private MultiSelectBox msMemberType = new MultiSelectBox();
		private MultiSelectBox msOrganization = new MultiSelectBox();
		private MultiSelectBox msParentMembership = new MultiSelectBox();
		private MultiSelectBox msSourceTemplate = new MultiSelectBox();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Rappahanock Folder

		private MultiSelectBox msInvoiceLine = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoMembershipID
		{
			get
			{
				return dojoMembershipID;
			}
			set
			{
				loadFlag = true;
				dojoMembershipID = value;
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

			msMember.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMember);

			msMemberType.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMemberType);

			deStartDate.ID = this.ID + "_StartDate";
			deStartDate.AutoAdjust = true;
			deStartDate.EnableViewState = false;
			Controls.Add(deStartDate);

			deEndDate.ID = this.ID + "_EndDate";
			deEndDate.AutoAdjust = true;
			deEndDate.EnableViewState = false;
			Controls.Add(deEndDate);

			msOrganization.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msOrganization);

			tbOrganizationMemberID.EnableViewState = false;
			Controls.Add(tbOrganizationMemberID);

			msParentMembership.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msParentMembership);

			msSourceTemplate.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msSourceTemplate);

			#endregion

			#region Child Controls for Rappahanock Folder

			msInvoiceLine.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msInvoiceLine);

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

			msMember.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager memberManager = new DojoMemberManager();
			DojoMemberCollection memberCollection = memberManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember member in memberCollection)
			{
				ListItem i = new ListItem(member.ToString(), member.ID.ToString());
				msMember.Items.Add(i);
			}

			msMemberType.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager memberTypeManager = new DojoMemberTypeManager();
			DojoMemberTypeCollection memberTypeCollection = memberTypeManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType memberType in memberTypeCollection)
			{
				ListItem i = new ListItem(memberType.ToString(), memberType.ID.ToString());
				msMemberType.Items.Add(i);
			}

			msOrganization.Items.Add(new ListItem("Null", "Null"));
			DojoOrganizationManager organizationManager = new DojoOrganizationManager();
			DojoOrganizationCollection organizationCollection = organizationManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoOrganization organization in organizationCollection)
			{
				ListItem i = new ListItem(organization.ToString(), organization.ID.ToString());
				msOrganization.Items.Add(i);
			}

			msParentMembership.Items.Add(new ListItem("Null", "Null"));
			DojoMembershipManager parentMembershipManager = new DojoMembershipManager();
			DojoMembershipCollection parentMembershipCollection = parentMembershipManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMembership parentMembership in parentMembershipCollection)
			{
				ListItem i = new ListItem(parentMembership.ToString(), parentMembership.ID.ToString());
				msParentMembership.Items.Add(i);
			}

			msSourceTemplate.Items.Add(new ListItem("Null", "Null"));
			DojoMembershipTemplateManager sourceTemplateManager = new DojoMembershipTemplateManager();
			DojoMembershipTemplateCollection sourceTemplateCollection = sourceTemplateManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMembershipTemplate sourceTemplate in sourceTemplateCollection)
			{
				ListItem i = new ListItem(sourceTemplate.ToString(), sourceTemplate.ID.ToString());
				msSourceTemplate.Items.Add(i);
			}

			#endregion

			#region Bind Rappahanock Child Data

			msInvoiceLine.Items.Add(new ListItem("Null", "Null"));
			RHInvoiceLineManager invoiceLineManager = new RHInvoiceLineManager();
			RHInvoiceLineCollection invoiceLineCollection = invoiceLineManager.GetCollection(string.Empty, string.Empty, null);
			foreach(RHInvoiceLine invoiceLine in invoiceLineCollection)
			{
				ListItem i = new ListItem(invoiceLine.ToString(), invoiceLine.ID.ToString());
				msInvoiceLine.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoMembershipID == 0)
				obj = new DojoMembership();
			else
				obj = new DojoMembership(dojoMembershipID);

			obj.StartDate = deStartDate.Date;
			obj.EndDate = deEndDate.Date;
			obj.OrganizationMemberID = tbOrganizationMemberID.Text;

			if(msMember.SelectedItem != null && msMember.SelectedItem.Value != "Null")
				obj.Member = DojoMember.NewPlaceHolder(
					int.Parse(msMember.SelectedItem.Value));
			else
				obj.Member = null;

			if(msMemberType.SelectedItem != null && msMemberType.SelectedItem.Value != "Null")
				obj.MemberType = DojoMemberType.NewPlaceHolder(
					int.Parse(msMemberType.SelectedItem.Value));
			else
				obj.MemberType = null;

			if(msOrganization.SelectedItem != null && msOrganization.SelectedItem.Value != "Null")
				obj.Organization = DojoOrganization.NewPlaceHolder(
					int.Parse(msOrganization.SelectedItem.Value));
			else
				obj.Organization = null;

			if(msParentMembership.SelectedItem != null && msParentMembership.SelectedItem.Value != "Null")
				obj.ParentMembership = DojoMembership.NewPlaceHolder(
					int.Parse(msParentMembership.SelectedItem.Value));
			else
				obj.ParentMembership = null;

			if(msSourceTemplate.SelectedItem != null && msSourceTemplate.SelectedItem.Value != "Null")
				obj.SourceTemplate = DojoMembershipTemplate.NewPlaceHolder(
					int.Parse(msSourceTemplate.SelectedItem.Value));
			else
				obj.SourceTemplate = null;

			if(msInvoiceLine.SelectedItem != null && msInvoiceLine.SelectedItem.Value != "Null")
				obj.InvoiceLine = RHInvoiceLine.NewPlaceHolder(
					int.Parse(msInvoiceLine.SelectedItem.Value));
			else
				obj.InvoiceLine = null;

			if(editOnAdd)
				dojoMembershipID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				deStartDate.Date = DateTime.Now;
				deEndDate.Date = DateTime.Now;
				tbOrganizationMemberID.Text = string.Empty;
				msMember.SelectedIndex = 0;
				msMemberType.SelectedIndex = 0;
				msOrganization.SelectedIndex = 0;
				msParentMembership.SelectedIndex = 0;
				msSourceTemplate.SelectedIndex = 0;
				msInvoiceLine.SelectedIndex = 0;
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

			Tab RappahanockTab = new Tab("Rappahanock");
			RappahanockTab.RenderDiv += new TabRenderHandler(renderRappahanockFolder);
			tabStrip.Tabs.Add(RappahanockTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoMembershipID > 0)
				{
					obj = new DojoMembership(dojoMembershipID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoMembershipID <= 0)
				{
					obj = new DojoMembership();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				deStartDate.Date = obj.StartDate;
				deEndDate.Date = obj.EndDate;
				tbOrganizationMemberID.Text = obj.OrganizationMemberID;

				//
				// Set Children Selections
				//
				if(obj.Member != null)
					foreach(ListItem item in msMember.Items)
						item.Selected = obj.Member.ID.ToString() == item.Value;
					else
						msMember.SelectedIndex = 0;

				if(obj.MemberType != null)
					foreach(ListItem item in msMemberType.Items)
						item.Selected = obj.MemberType.ID.ToString() == item.Value;
					else
						msMemberType.SelectedIndex = 0;

				if(obj.Organization != null)
					foreach(ListItem item in msOrganization.Items)
						item.Selected = obj.Organization.ID.ToString() == item.Value;
					else
						msOrganization.SelectedIndex = 0;

				if(obj.ParentMembership != null)
					foreach(ListItem item in msParentMembership.Items)
						item.Selected = obj.ParentMembership.ID.ToString() == item.Value;
					else
						msParentMembership.SelectedIndex = 0;

				if(obj.SourceTemplate != null)
					foreach(ListItem item in msSourceTemplate.Items)
						item.Selected = obj.SourceTemplate.ID.ToString() == item.Value;
					else
						msSourceTemplate.SelectedIndex = 0;

				if(obj.InvoiceLine != null)
					foreach(ListItem item in msInvoiceLine.Items)
						item.Selected = obj.InvoiceLine.ID.ToString() == item.Value;
					else
						msInvoiceLine.SelectedIndex = 0;

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
			// Render Member
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Member");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMember.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberType
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberType");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMemberType.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render StartDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("StartDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			deStartDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render EndDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("EndDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			deEndDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Organization
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Organization");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msOrganization.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OrganizationMemberID
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OrganizationMemberID");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbOrganizationMemberID.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentMembership
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentMembership");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msParentMembership.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render SourceTemplate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("SourceTemplate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msSourceTemplate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		private void renderRappahanockFolder(HtmlTextWriter output)
		{
			//
			// Render InvoiceLine
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("InvoiceLine");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msInvoiceLine.RenderControl(output);
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
					dojoMembershipID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoMembershipID;
			return myState;
		}
	}
}

