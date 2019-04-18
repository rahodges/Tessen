using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.Rappahanock;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoMembershipTemplate.
	/// </summary>
	[ToolboxData("<{0}:DojoMembershipTemplateEditor runat=server></{0}:DojoMembershipTemplateEditor>")]
	public class DojoMembershipTemplateEditor : TableWindow, INamingContainer
	{
		private int dojoMembershipTemplateID;
		private DojoMembershipTemplate obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for _system Folder

		private Literal ltCreateDate = new Literal();
		private Literal ltModifyDate = new Literal();

		#endregion

		#region Private Control Fields for General Folder

		private TextBox tbName = new TextBox();
		private TextBox tbDescription = new TextBox();
		private TextBox tbDuration = new TextBox();
		private TextBox tbFee = new TextBox();
		private MultiSelectBox msParentTemplate = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Member_Types Folder

		private MultiSelectBox msMemberType1 = new MultiSelectBox();
		private MultiSelectBox msMemberType2 = new MultiSelectBox();
		private MultiSelectBox msMemberType3 = new MultiSelectBox();
		private MultiSelectBox msMemberType4 = new MultiSelectBox();
		private MultiSelectBox msMemberType5 = new MultiSelectBox();
		private MultiSelectBox msMemberType6 = new MultiSelectBox();
		private MultiSelectBox msMemberType7 = new MultiSelectBox();
		private MultiSelectBox msMemberType8 = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Rappahanock Folder

		private MultiSelectBox msItem = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Renewals Folder

		private CheckBox cbAutoRenewalEnabled = new CheckBox();
		private CheckBox cbAutoPayEnabled = new CheckBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoMembershipTemplateID
		{
			get
			{
				return dojoMembershipTemplateID;
			}
			set
			{
				loadFlag = true;
				dojoMembershipTemplateID = value;
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

			#region Child Controls for _system Folder

			ltCreateDate.EnableViewState = false;
			Controls.Add(ltCreateDate);

			ltModifyDate.EnableViewState = false;
			Controls.Add(ltModifyDate);

			#endregion

			#region Child Controls for General Folder

			tbName.EnableViewState = false;
			Controls.Add(tbName);

			tbDescription.EnableViewState = false;
			Controls.Add(tbDescription);

			tbDuration.EnableViewState = false;
			Controls.Add(tbDuration);

			tbFee.EnableViewState = false;
			Controls.Add(tbFee);

			msParentTemplate.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msParentTemplate);

			#endregion

			#region Child Controls for Member Types Folder

			msMemberType1.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMemberType1);

			msMemberType2.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMemberType2);

			msMemberType3.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMemberType3);

			msMemberType4.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMemberType4);

			msMemberType5.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMemberType5);

			msMemberType6.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMemberType6);

			msMemberType7.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMemberType7);

			msMemberType8.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMemberType8);

			#endregion

			#region Child Controls for Rappahanock Folder

			msItem.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msItem);

			#endregion

			#region Child Controls for Renewals Folder

			cbAutoRenewalEnabled.EnableViewState = false;
			Controls.Add(cbAutoRenewalEnabled);

			cbAutoPayEnabled.EnableViewState = false;
			Controls.Add(cbAutoPayEnabled);

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

			msParentTemplate.Items.Add(new ListItem("Null", "Null"));
			DojoMembershipTemplateManager parentTemplateManager = new DojoMembershipTemplateManager();
			DojoMembershipTemplateCollection parentTemplateCollection = parentTemplateManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMembershipTemplate parentTemplate in parentTemplateCollection)
			{
				ListItem i = new ListItem(parentTemplate.ToString(), parentTemplate.ID.ToString());
				msParentTemplate.Items.Add(i);
			}

			#endregion

			#region Bind Member Types Child Data

			msMemberType1.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager memberType1Manager = new DojoMemberTypeManager();
			DojoMemberTypeCollection memberType1Collection = memberType1Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType memberType1 in memberType1Collection)
			{
				ListItem i = new ListItem(memberType1.ToString(), memberType1.ID.ToString());
				msMemberType1.Items.Add(i);
			}

			msMemberType2.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager memberType2Manager = new DojoMemberTypeManager();
			DojoMemberTypeCollection memberType2Collection = memberType2Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType memberType2 in memberType2Collection)
			{
				ListItem i = new ListItem(memberType2.ToString(), memberType2.ID.ToString());
				msMemberType2.Items.Add(i);
			}

			msMemberType3.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager memberType3Manager = new DojoMemberTypeManager();
			DojoMemberTypeCollection memberType3Collection = memberType3Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType memberType3 in memberType3Collection)
			{
				ListItem i = new ListItem(memberType3.ToString(), memberType3.ID.ToString());
				msMemberType3.Items.Add(i);
			}

			msMemberType4.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager memberType4Manager = new DojoMemberTypeManager();
			DojoMemberTypeCollection memberType4Collection = memberType4Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType memberType4 in memberType4Collection)
			{
				ListItem i = new ListItem(memberType4.ToString(), memberType4.ID.ToString());
				msMemberType4.Items.Add(i);
			}

			msMemberType5.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager memberType5Manager = new DojoMemberTypeManager();
			DojoMemberTypeCollection memberType5Collection = memberType5Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType memberType5 in memberType5Collection)
			{
				ListItem i = new ListItem(memberType5.ToString(), memberType5.ID.ToString());
				msMemberType5.Items.Add(i);
			}

			msMemberType6.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager memberType6Manager = new DojoMemberTypeManager();
			DojoMemberTypeCollection memberType6Collection = memberType6Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType memberType6 in memberType6Collection)
			{
				ListItem i = new ListItem(memberType6.ToString(), memberType6.ID.ToString());
				msMemberType6.Items.Add(i);
			}

			msMemberType7.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager memberType7Manager = new DojoMemberTypeManager();
			DojoMemberTypeCollection memberType7Collection = memberType7Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType memberType7 in memberType7Collection)
			{
				ListItem i = new ListItem(memberType7.ToString(), memberType7.ID.ToString());
				msMemberType7.Items.Add(i);
			}

			msMemberType8.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager memberType8Manager = new DojoMemberTypeManager();
			DojoMemberTypeCollection memberType8Collection = memberType8Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType memberType8 in memberType8Collection)
			{
				ListItem i = new ListItem(memberType8.ToString(), memberType8.ID.ToString());
				msMemberType8.Items.Add(i);
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
			if(dojoMembershipTemplateID == 0)
				obj = new DojoMembershipTemplate();
			else
				obj = new DojoMembershipTemplate(dojoMembershipTemplateID);

			obj.Name = tbName.Text;
			obj.Description = tbDescription.Text;
			obj.Duration = TimeSpan.Parse(tbDuration.Text);
			obj.Fee = decimal.Parse(tbFee.Text);
			obj.AutoRenewalEnabled = cbAutoRenewalEnabled.Checked;
			obj.AutoPayEnabled = cbAutoPayEnabled.Checked;

			if(msParentTemplate.SelectedItem != null && msParentTemplate.SelectedItem.Value != "Null")
				obj.ParentTemplate = DojoMembershipTemplate.NewPlaceHolder(
					int.Parse(msParentTemplate.SelectedItem.Value));
			else
				obj.ParentTemplate = null;

			if(msMemberType1.SelectedItem != null && msMemberType1.SelectedItem.Value != "Null")
				obj.MemberType1 = DojoMemberType.NewPlaceHolder(
					int.Parse(msMemberType1.SelectedItem.Value));
			else
				obj.MemberType1 = null;

			if(msMemberType2.SelectedItem != null && msMemberType2.SelectedItem.Value != "Null")
				obj.MemberType2 = DojoMemberType.NewPlaceHolder(
					int.Parse(msMemberType2.SelectedItem.Value));
			else
				obj.MemberType2 = null;

			if(msMemberType3.SelectedItem != null && msMemberType3.SelectedItem.Value != "Null")
				obj.MemberType3 = DojoMemberType.NewPlaceHolder(
					int.Parse(msMemberType3.SelectedItem.Value));
			else
				obj.MemberType3 = null;

			if(msMemberType4.SelectedItem != null && msMemberType4.SelectedItem.Value != "Null")
				obj.MemberType4 = DojoMemberType.NewPlaceHolder(
					int.Parse(msMemberType4.SelectedItem.Value));
			else
				obj.MemberType4 = null;

			if(msMemberType5.SelectedItem != null && msMemberType5.SelectedItem.Value != "Null")
				obj.MemberType5 = DojoMemberType.NewPlaceHolder(
					int.Parse(msMemberType5.SelectedItem.Value));
			else
				obj.MemberType5 = null;

			if(msMemberType6.SelectedItem != null && msMemberType6.SelectedItem.Value != "Null")
				obj.MemberType6 = DojoMemberType.NewPlaceHolder(
					int.Parse(msMemberType6.SelectedItem.Value));
			else
				obj.MemberType6 = null;

			if(msMemberType7.SelectedItem != null && msMemberType7.SelectedItem.Value != "Null")
				obj.MemberType7 = DojoMemberType.NewPlaceHolder(
					int.Parse(msMemberType7.SelectedItem.Value));
			else
				obj.MemberType7 = null;

			if(msMemberType8.SelectedItem != null && msMemberType8.SelectedItem.Value != "Null")
				obj.MemberType8 = DojoMemberType.NewPlaceHolder(
					int.Parse(msMemberType8.SelectedItem.Value));
			else
				obj.MemberType8 = null;

			if(msItem.SelectedItem != null && msItem.SelectedItem.Value != "Null")
				obj.Item = RHItem.NewPlaceHolder(
					int.Parse(msItem.SelectedItem.Value));
			else
				obj.Item = null;

			if(editOnAdd)
				dojoMembershipTemplateID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbDescription.Text = string.Empty;
				tbDuration.Text = string.Empty;
				tbFee.Text = string.Empty;
				cbAutoRenewalEnabled.Checked = false;
				cbAutoPayEnabled.Checked = false;
				msParentTemplate.SelectedIndex = 0;
				msMemberType1.SelectedIndex = 0;
				msMemberType2.SelectedIndex = 0;
				msMemberType3.SelectedIndex = 0;
				msMemberType4.SelectedIndex = 0;
				msMemberType5.SelectedIndex = 0;
				msMemberType6.SelectedIndex = 0;
				msMemberType7.SelectedIndex = 0;
				msMemberType8.SelectedIndex = 0;
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
			tabStrip.Tabs.Add(GeneralTab);

			Tab Member_TypesTab = new Tab("Member Types");
			Member_TypesTab.RenderDiv += new TabRenderHandler(renderMember_TypesFolder);
			tabStrip.Tabs.Add(Member_TypesTab);

			Tab RappahanockTab = new Tab("Rappahanock");
			RappahanockTab.RenderDiv += new TabRenderHandler(renderRappahanockFolder);
			tabStrip.Tabs.Add(RappahanockTab);

			Tab RenewalsTab = new Tab("Renewals");
			RenewalsTab.RenderDiv += new TabRenderHandler(renderRenewalsFolder);
			tabStrip.Tabs.Add(RenewalsTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoMembershipTemplateID > 0)
				{
					obj = new DojoMembershipTemplate(dojoMembershipTemplateID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoMembershipTemplateID <= 0)
				{
					obj = new DojoMembershipTemplate();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				ltCreateDate.Text = obj.CreateDate.ToString();
				ltModifyDate.Text = obj.ModifyDate.ToString();
				tbName.Text = obj.Name;
				tbDescription.Text = obj.Description;
				tbDuration.Text = obj.Duration.ToString();
				tbFee.Text = obj.Fee.ToString();
				cbAutoRenewalEnabled.Checked = obj.AutoRenewalEnabled;
				cbAutoPayEnabled.Checked = obj.AutoPayEnabled;

				//
				// Set Children Selections
				//
				if(obj.ParentTemplate != null)
					foreach(ListItem item in msParentTemplate.Items)
						item.Selected = obj.ParentTemplate.ID.ToString() == item.Value;
					else
						msParentTemplate.SelectedIndex = 0;

				if(obj.MemberType1 != null)
					foreach(ListItem item in msMemberType1.Items)
						item.Selected = obj.MemberType1.ID.ToString() == item.Value;
					else
						msMemberType1.SelectedIndex = 0;

				if(obj.MemberType2 != null)
					foreach(ListItem item in msMemberType2.Items)
						item.Selected = obj.MemberType2.ID.ToString() == item.Value;
					else
						msMemberType2.SelectedIndex = 0;

				if(obj.MemberType3 != null)
					foreach(ListItem item in msMemberType3.Items)
						item.Selected = obj.MemberType3.ID.ToString() == item.Value;
					else
						msMemberType3.SelectedIndex = 0;

				if(obj.MemberType4 != null)
					foreach(ListItem item in msMemberType4.Items)
						item.Selected = obj.MemberType4.ID.ToString() == item.Value;
					else
						msMemberType4.SelectedIndex = 0;

				if(obj.MemberType5 != null)
					foreach(ListItem item in msMemberType5.Items)
						item.Selected = obj.MemberType5.ID.ToString() == item.Value;
					else
						msMemberType5.SelectedIndex = 0;

				if(obj.MemberType6 != null)
					foreach(ListItem item in msMemberType6.Items)
						item.Selected = obj.MemberType6.ID.ToString() == item.Value;
					else
						msMemberType6.SelectedIndex = 0;

				if(obj.MemberType7 != null)
					foreach(ListItem item in msMemberType7.Items)
						item.Selected = obj.MemberType7.ID.ToString() == item.Value;
					else
						msMemberType7.SelectedIndex = 0;

				if(obj.MemberType8 != null)
					foreach(ListItem item in msMemberType8.Items)
						item.Selected = obj.MemberType8.ID.ToString() == item.Value;
					else
						msMemberType8.SelectedIndex = 0;

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

		private void render_systemFolder(HtmlTextWriter output)
		{
			//
			// Render CreateDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CreateDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltCreateDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ModifyDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ModifyDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltModifyDate.RenderControl(output);
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
			// Render Duration
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Duration");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbDuration.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Fee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Fee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentTemplate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Parent Template");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msParentTemplate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderMember_TypesFolder(HtmlTextWriter output)
		{
			//
			// Render MemberType1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberType1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMemberType1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberType2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberType2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMemberType2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberType3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberType3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMemberType3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberType4
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberType4");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMemberType4.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberType5
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberType5");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMemberType5.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberType6
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberType6");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMemberType6.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberType7
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberType7");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMemberType7.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberType8
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberType8");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMemberType8.RenderControl(output);
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

		private void renderRenewalsFolder(HtmlTextWriter output)
		{
			//
			// Render AutoRenewalEnabled
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AutoRenewalEnabled");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbAutoRenewalEnabled.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AutoPayEnabled
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AutoPayEnabled");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbAutoPayEnabled.RenderControl(output);
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
					dojoMembershipTemplateID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoMembershipTemplateID;
			return myState;
		}
	}
}

