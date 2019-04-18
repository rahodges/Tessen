using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.GreyFox.Security;
using Amns.Rappahanock;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoMember.
	/// </summary>
	[ToolboxData("<{0}:DojoMemberEditor runat=server></{0}:DojoMemberEditor>")]
	public class DojoMemberEditor : TableWindow, INamingContainer
	{
		private int dojoMemberID;
		private DojoMember obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for General Folder

		private MultiSelectBox msMemberType = new MultiSelectBox();
		private Literal ltPrivateContact = new Literal();
		private Literal ltEmergencyContact = new Literal();
		private MultiSelectBox msPublicContact = new MultiSelectBox();
		private MultiSelectBox msParentMember = new MultiSelectBox();

		#endregion

		#region Private Control Fields for _system Folder

		private Literal ltCreateDate = new Literal();
		private Literal ltModifyDate = new Literal();

		#endregion

		#region Private Control Fields for Membership Folder

		private DateEditor deMemberSince = new DateEditor();
		private CheckBox cbIsPrimaryOrgActive = new CheckBox();
		private CheckBox cbIsParentOrgActive = new CheckBox();
		private TextBox tbLastMembershipScan = new TextBox();
		private MultiSelectBox msPrimaryOrgMembership = new MultiSelectBox();
		private MultiSelectBox msParentOrgMembership = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Attendance Folder

		private Literal ltTimeInRank = new Literal();
		private Literal ltTimeInMembership = new Literal();
		private Literal ltLastSignin = new Literal();
		private Literal ltLastAttendanceScan = new Literal();
		private TextBox tbAttendanceMessage = new TextBox();
		private MultiSelectBox msInstructor1 = new MultiSelectBox();
		private MultiSelectBox msInstructor2 = new MultiSelectBox();
		private MultiSelectBox msInstructor3 = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Activity Folder

		private CheckBox cbHasWaiver = new CheckBox();
		private CheckBox cbIsPromotable = new CheckBox();
		private CheckBox cbIsInstructor = new CheckBox();
		private DateEditor deRankDate = new DateEditor();
		private MultiSelectBox msPromotionFlags = new MultiSelectBox();
		private MultiSelectBox msRank = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Security Folder

		private Literal ltUser = new Literal();

		#endregion

		#region Private Control Fields for Accounting Folder

		private CheckBox cbIsPastDue = new CheckBox();

		#endregion

		#region Private Control Fields for Rappahanock Folder

		private MultiSelectBox msCustomer = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoMemberID
		{
			get
			{
				return dojoMemberID;
			}
			set
			{
				loadFlag = true;
				dojoMemberID = value;
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

			msMemberType.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMemberType);

			ltPrivateContact.EnableViewState = false;
			Controls.Add(ltPrivateContact);

			ltEmergencyContact.EnableViewState = false;
			Controls.Add(ltEmergencyContact);

			msPublicContact.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msPublicContact);

			msParentMember.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msParentMember);

			#endregion

			#region Child Controls for _system Folder

			ltCreateDate.EnableViewState = false;
			Controls.Add(ltCreateDate);

			ltModifyDate.EnableViewState = false;
			Controls.Add(ltModifyDate);

			#endregion

			#region Child Controls for Membership Folder

			deMemberSince.ID = this.ID + "_MemberSince";
			deMemberSince.AutoAdjust = true;
			deMemberSince.EnableViewState = false;
			Controls.Add(deMemberSince);

			cbIsPrimaryOrgActive.EnableViewState = false;
			Controls.Add(cbIsPrimaryOrgActive);

			msPrimaryOrgMembership.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msPrimaryOrgMembership);

			cbIsParentOrgActive.EnableViewState = false;
			Controls.Add(cbIsParentOrgActive);

			msParentOrgMembership.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msParentOrgMembership);

			tbLastMembershipScan.EnableViewState = false;
			Controls.Add(tbLastMembershipScan);

			#endregion

			#region Child Controls for Attendance Folder

			ltTimeInRank.EnableViewState = false;
			Controls.Add(ltTimeInRank);

			ltTimeInMembership.EnableViewState = false;
			Controls.Add(ltTimeInMembership);

			msInstructor1.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msInstructor1);

			msInstructor2.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msInstructor2);

			msInstructor3.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msInstructor3);

			ltLastSignin.EnableViewState = false;
			Controls.Add(ltLastSignin);

			ltLastAttendanceScan.EnableViewState = false;
			Controls.Add(ltLastAttendanceScan);

			tbAttendanceMessage.EnableViewState = false;
			Controls.Add(tbAttendanceMessage);

			#endregion

			#region Child Controls for Activity Folder

			cbHasWaiver.EnableViewState = false;
			Controls.Add(cbHasWaiver);

			cbIsPromotable.EnableViewState = false;
			Controls.Add(cbIsPromotable);

			msPromotionFlags.Mode = MultiSelectBoxMode.DualSelect;
			Controls.Add(msPromotionFlags);

			cbIsInstructor.EnableViewState = false;
			Controls.Add(cbIsInstructor);

			msRank.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msRank);

			deRankDate.ID = this.ID + "_RankDate";
			deRankDate.AutoAdjust = true;
			deRankDate.EnableViewState = false;
			Controls.Add(deRankDate);

			#endregion

			#region Child Controls for Security Folder

			ltUser.EnableViewState = false;
			Controls.Add(ltUser);

			#endregion

			#region Child Controls for Accounting Folder

			cbIsPastDue.EnableViewState = false;
			Controls.Add(cbIsPastDue);


			#endregion

			#region Child Controls for Rappahanock Folder

			msCustomer.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msCustomer);

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

			msMemberType.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager memberTypeManager = new DojoMemberTypeManager();
			DojoMemberTypeCollection memberTypeCollection = memberTypeManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType memberType in memberTypeCollection)
			{
				ListItem i = new ListItem(memberType.ToString(), memberType.ID.ToString());
				msMemberType.Items.Add(i);
			}

			msPublicContact.Items.Add(new ListItem("Null", "Null"));
			GreyFoxContactManager publicContactManager = new GreyFoxContactManager("kitTessen_Members_PublicContacts");
			GreyFoxContactCollection publicContactCollection = publicContactManager.GetCollection(string.Empty, string.Empty);
			foreach(GreyFoxContact publicContact in publicContactCollection)
			{
				ListItem i = new ListItem(publicContact.ToString(), publicContact.ID.ToString());
				msPublicContact.Items.Add(i);
			}

			msParentMember.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager parentMemberManager = new DojoMemberManager();
			DojoMemberCollection parentMemberCollection = parentMemberManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember parentMember in parentMemberCollection)
			{
				ListItem i = new ListItem(parentMember.ToString(), parentMember.ID.ToString());
				msParentMember.Items.Add(i);
			}

			#endregion

			#region Bind Membership Child Data

			msPrimaryOrgMembership.Items.Add(new ListItem("Null", "Null"));
			DojoMembershipManager primaryOrgMembershipManager = new DojoMembershipManager();
			DojoMembershipCollection primaryOrgMembershipCollection = primaryOrgMembershipManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMembership primaryOrgMembership in primaryOrgMembershipCollection)
			{
				ListItem i = new ListItem(primaryOrgMembership.ToString(), primaryOrgMembership.ID.ToString());
				msPrimaryOrgMembership.Items.Add(i);
			}

			msParentOrgMembership.Items.Add(new ListItem("Null", "Null"));
			DojoMembershipManager parentOrgMembershipManager = new DojoMembershipManager();
			DojoMembershipCollection parentOrgMembershipCollection = parentOrgMembershipManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMembership parentOrgMembership in parentOrgMembershipCollection)
			{
				ListItem i = new ListItem(parentOrgMembership.ToString(), parentOrgMembership.ID.ToString());
				msParentOrgMembership.Items.Add(i);
			}

			#endregion

			#region Bind Attendance Child Data

			msInstructor1.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager instructor1Manager = new DojoMemberManager();
			DojoMemberCollection instructor1Collection = instructor1Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember instructor1 in instructor1Collection)
			{
				ListItem i = new ListItem(instructor1.ToString(), instructor1.ID.ToString());
				msInstructor1.Items.Add(i);
			}

			msInstructor2.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager instructor2Manager = new DojoMemberManager();
			DojoMemberCollection instructor2Collection = instructor2Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember instructor2 in instructor2Collection)
			{
				ListItem i = new ListItem(instructor2.ToString(), instructor2.ID.ToString());
				msInstructor2.Items.Add(i);
			}

			msInstructor3.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager instructor3Manager = new DojoMemberManager();
			DojoMemberCollection instructor3Collection = instructor3Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember instructor3 in instructor3Collection)
			{
				ListItem i = new ListItem(instructor3.ToString(), instructor3.ID.ToString());
				msInstructor3.Items.Add(i);
			}

			#endregion

			#region Bind Activity Child Data

			msPromotionFlags.Items.Add(new ListItem("Null", "Null"));
			DojoPromotionFlagManager promotionFlagsManager = new DojoPromotionFlagManager();
			DojoPromotionFlagCollection promotionFlagsCollection = promotionFlagsManager.GetCollection(string.Empty, string.Empty);
			foreach(DojoPromotionFlag promotionFlags in promotionFlagsCollection)
			{
				ListItem i = new ListItem(promotionFlags.ToString(), promotionFlags.ID.ToString());
				msPromotionFlags.Items.Add(i);
			}

			msRank.Items.Add(new ListItem("Null", "Null"));
			DojoRankManager rankManager = new DojoRankManager();
			DojoRankCollection rankCollection = rankManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoRank rank in rankCollection)
			{
				ListItem i = new ListItem(rank.ToString(), rank.ID.ToString());
				msRank.Items.Add(i);
			}

			#endregion

			#region Bind Security Child Data

			#endregion

			#region Bind Rappahanock Child Data

			msCustomer.Items.Add(new ListItem("Null", "Null"));
			RHCustomerManager customerManager = new RHCustomerManager();
			RHCustomerCollection customerCollection = customerManager.GetCollection(string.Empty, string.Empty, null);
			foreach(RHCustomer customer in customerCollection)
			{
				ListItem i = new ListItem(customer.ToString(), customer.ID.ToString());
				msCustomer.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoMemberID == 0)
				obj = new DojoMember();
			else
				obj = new DojoMember(dojoMemberID);

			obj.MemberSince = deMemberSince.Date;
			obj.IsPrimaryOrgActive = cbIsPrimaryOrgActive.Checked;
			obj.IsParentOrgActive = cbIsParentOrgActive.Checked;
			obj.LastMembershipScan = DateTime.Parse(tbLastMembershipScan.Text);
			obj.AttendanceMessage = tbAttendanceMessage.Text;
			obj.HasWaiver = cbHasWaiver.Checked;
			obj.IsPromotable = cbIsPromotable.Checked;
			obj.IsInstructor = cbIsInstructor.Checked;
			obj.RankDate = deRankDate.Date;
			obj.IsPastDue = cbIsPastDue.Checked;

			if(msMemberType.SelectedItem != null && msMemberType.SelectedItem.Value != "Null")
				obj.MemberType = DojoMemberType.NewPlaceHolder(
					int.Parse(msMemberType.SelectedItem.Value));
			else
				obj.MemberType = null;

			if(msPublicContact.SelectedItem != null && msPublicContact.SelectedItem.Value != "Null")
				obj.PublicContact = GreyFoxContact.NewPlaceHolder("kitTessen_Members_PublicContacts", 
					int.Parse(msPublicContact.SelectedItem.Value));
			else
				obj.PublicContact = null;

			if(msParentMember.SelectedItem != null && msParentMember.SelectedItem.Value != "Null")
				obj.ParentMember = DojoMember.NewPlaceHolder(
					int.Parse(msParentMember.SelectedItem.Value));
			else
				obj.ParentMember = null;

			if(msPrimaryOrgMembership.SelectedItem != null && msPrimaryOrgMembership.SelectedItem.Value != "Null")
				obj.PrimaryOrgMembership = DojoMembership.NewPlaceHolder(
					int.Parse(msPrimaryOrgMembership.SelectedItem.Value));
			else
				obj.PrimaryOrgMembership = null;

			if(msParentOrgMembership.SelectedItem != null && msParentOrgMembership.SelectedItem.Value != "Null")
				obj.ParentOrgMembership = DojoMembership.NewPlaceHolder(
					int.Parse(msParentOrgMembership.SelectedItem.Value));
			else
				obj.ParentOrgMembership = null;

			if(msInstructor1.SelectedItem != null && msInstructor1.SelectedItem.Value != "Null")
				obj.Instructor1 = DojoMember.NewPlaceHolder(
					int.Parse(msInstructor1.SelectedItem.Value));
			else
				obj.Instructor1 = null;

			if(msInstructor2.SelectedItem != null && msInstructor2.SelectedItem.Value != "Null")
				obj.Instructor2 = DojoMember.NewPlaceHolder(
					int.Parse(msInstructor2.SelectedItem.Value));
			else
				obj.Instructor2 = null;

			if(msInstructor3.SelectedItem != null && msInstructor3.SelectedItem.Value != "Null")
				obj.Instructor3 = DojoMember.NewPlaceHolder(
					int.Parse(msInstructor3.SelectedItem.Value));
			else
				obj.Instructor3 = null;

			if(msPromotionFlags.IsChanged)
			{
				obj.PromotionFlags = new DojoPromotionFlagCollection();
				foreach(ListItem i in msPromotionFlags.Items)
					if(i.Selected)
						obj.PromotionFlags.Add(DojoPromotionFlag.NewPlaceHolder(int.Parse(i.Value)));
			}

			if(msRank.SelectedItem != null && msRank.SelectedItem.Value != "Null")
				obj.Rank = DojoRank.NewPlaceHolder(
					int.Parse(msRank.SelectedItem.Value));
			else
				obj.Rank = null;

			if(msCustomer.SelectedItem != null && msCustomer.SelectedItem.Value != "Null")
				obj.Customer = RHCustomer.NewPlaceHolder(
					int.Parse(msCustomer.SelectedItem.Value));
			else
				obj.Customer = null;

			if(editOnAdd)
				dojoMemberID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				deMemberSince.Date = DateTime.Now;
				cbIsPrimaryOrgActive.Checked = false;
				cbIsParentOrgActive.Checked = false;
				tbLastMembershipScan.Text = DateTime.Now.ToString();
				tbAttendanceMessage.Text = string.Empty;
				cbHasWaiver.Checked = false;
				cbIsPromotable.Checked = false;
				cbIsInstructor.Checked = false;
				deRankDate.Date = DateTime.Now;
				cbIsPastDue.Checked = false;
				msMemberType.SelectedIndex = 0;
				msPublicContact.SelectedIndex = 0;
				msParentMember.SelectedIndex = 0;
				msPrimaryOrgMembership.SelectedIndex = 0;
				msParentOrgMembership.SelectedIndex = 0;
				msInstructor1.SelectedIndex = 0;
				msInstructor2.SelectedIndex = 0;
				msInstructor3.SelectedIndex = 0;
				msRank.SelectedIndex = 0;
				msCustomer.SelectedIndex = 0;
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

			Tab MembershipTab = new Tab("Membership");
			MembershipTab.RenderDiv += new TabRenderHandler(renderMembershipFolder);
			tabStrip.Tabs.Add(MembershipTab);

			Tab AttendanceTab = new Tab("Attendance");
			AttendanceTab.RenderDiv += new TabRenderHandler(renderAttendanceFolder);
			tabStrip.Tabs.Add(AttendanceTab);

			Tab ActivityTab = new Tab("Activity");
			ActivityTab.RenderDiv += new TabRenderHandler(renderActivityFolder);
			tabStrip.Tabs.Add(ActivityTab);

			Tab SecurityTab = new Tab("Security");
			SecurityTab.RenderDiv += new TabRenderHandler(renderSecurityFolder);
			tabStrip.Tabs.Add(SecurityTab);

			Tab AccountingTab = new Tab("Accounting");
			AccountingTab.RenderDiv += new TabRenderHandler(renderAccountingFolder);
			tabStrip.Tabs.Add(AccountingTab);

			Tab RappahanockTab = new Tab("Rappahanock");
			RappahanockTab.RenderDiv += new TabRenderHandler(renderRappahanockFolder);
			tabStrip.Tabs.Add(RappahanockTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoMemberID > 0)
				{
					obj = new DojoMember(dojoMemberID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoMemberID <= 0)
				{
					obj = new DojoMember();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				ltCreateDate.Text = obj.CreateDate.ToString();
				ltModifyDate.Text = obj.ModifyDate.ToString();
				deMemberSince.Date = obj.MemberSince;
				cbIsPrimaryOrgActive.Checked = obj.IsPrimaryOrgActive;
				cbIsParentOrgActive.Checked = obj.IsParentOrgActive;
				tbLastMembershipScan.Text = obj.LastMembershipScan.ToString();
				ltTimeInRank.Text = obj.TimeInRank.ToString();
				ltTimeInMembership.Text = obj.TimeInMembership.ToString();
				ltLastSignin.Text = obj.LastSignin.ToString();
				ltLastAttendanceScan.Text = obj.LastAttendanceScan.ToString();
				tbAttendanceMessage.Text = obj.AttendanceMessage;
				cbHasWaiver.Checked = obj.HasWaiver;
				cbIsPromotable.Checked = obj.IsPromotable;
				cbIsInstructor.Checked = obj.IsInstructor;
				deRankDate.Date = obj.RankDate;
				cbIsPastDue.Checked = obj.IsPastDue;

				//
				// Set Children Selections
				//
				if(obj.MemberType != null)
					foreach(ListItem item in msMemberType.Items)
						item.Selected = obj.MemberType.ID.ToString() == item.Value;
					else
						msMemberType.SelectedIndex = 0;

				if(obj.PrivateContact != null)
					ltPrivateContact.Text = obj.PrivateContact.ToString();
				else
					ltPrivateContact.Text = string.Empty;
				if(obj.EmergencyContact != null)
					ltEmergencyContact.Text = obj.EmergencyContact.ToString();
				else
					ltEmergencyContact.Text = string.Empty;
				if(obj.PublicContact != null)
					foreach(ListItem item in msPublicContact.Items)
						item.Selected = obj.PublicContact.ID.ToString() == item.Value;
					else
						msPublicContact.SelectedIndex = 0;

				if(obj.ParentMember != null)
					foreach(ListItem item in msParentMember.Items)
						item.Selected = obj.ParentMember.ID.ToString() == item.Value;
					else
						msParentMember.SelectedIndex = 0;

				if(obj.PrimaryOrgMembership != null)
					foreach(ListItem item in msPrimaryOrgMembership.Items)
						item.Selected = obj.PrimaryOrgMembership.ID.ToString() == item.Value;
					else
						msPrimaryOrgMembership.SelectedIndex = 0;

				if(obj.ParentOrgMembership != null)
					foreach(ListItem item in msParentOrgMembership.Items)
						item.Selected = obj.ParentOrgMembership.ID.ToString() == item.Value;
					else
						msParentOrgMembership.SelectedIndex = 0;

				if(obj.Instructor1 != null)
					foreach(ListItem item in msInstructor1.Items)
						item.Selected = obj.Instructor1.ID.ToString() == item.Value;
					else
						msInstructor1.SelectedIndex = 0;

				if(obj.Instructor2 != null)
					foreach(ListItem item in msInstructor2.Items)
						item.Selected = obj.Instructor2.ID.ToString() == item.Value;
					else
						msInstructor2.SelectedIndex = 0;

				if(obj.Instructor3 != null)
					foreach(ListItem item in msInstructor3.Items)
						item.Selected = obj.Instructor3.ID.ToString() == item.Value;
					else
						msInstructor3.SelectedIndex = 0;

				foreach(ListItem i in msPromotionFlags.Items)
					foreach(DojoPromotionFlag dojoPromotionFlag in obj.PromotionFlags)
						if(i.Value == dojoPromotionFlag.ID.ToString())
						{
							i.Selected = true;
							break;
						}
				if(obj.Rank != null)
					foreach(ListItem item in msRank.Items)
						item.Selected = obj.Rank.ID.ToString() == item.Value;
					else
						msRank.SelectedIndex = 0;

				if(obj.User != null)
					ltUser.Text = obj.User.ToString();
				else
					ltUser.Text = string.Empty;
				if(obj.Customer != null)
					foreach(ListItem item in msCustomer.Items)
						item.Selected = obj.Customer.ID.ToString() == item.Value;
					else
						msCustomer.SelectedIndex = 0;

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
			// Render PrivateContact
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PrivateContact");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPrivateContact.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render EmergencyContact
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("EmergencyContact");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltEmergencyContact.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PublicContact
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PublicContact");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msPublicContact.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentMember
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentMember");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msParentMember.RenderControl(output);
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

		private void renderMembershipFolder(HtmlTextWriter output)
		{
			//
			// Render MemberSince
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Member Since");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			deMemberSince.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsPrimaryOrgActive
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Dojo membership activity.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsPrimaryOrgActive.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PrimaryOrgMembership
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PrimaryOrgMembership");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msPrimaryOrgMembership.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsParentOrgActive
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Is Parent Active?");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsParentOrgActive.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentOrgMembership
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentOrgMembership");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msParentOrgMembership.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render LastMembershipScan
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("LastMembershipScan");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbLastMembershipScan.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderAttendanceFolder(HtmlTextWriter output)
		{
			//
			// Render TimeInRank
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("TimeInRank");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltTimeInRank.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render TimeInMembership
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("TimeInMembership");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltTimeInMembership.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Instructor1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Instructor1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msInstructor1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Instructor2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Instructor2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msInstructor2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Instructor3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Instructor3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msInstructor3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render LastSignin
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Last Signin");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltLastSignin.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render LastAttendanceScan
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("LastAttendanceScan");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltLastAttendanceScan.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AttendanceMessage
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AttendanceMessage");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbAttendanceMessage.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderActivityFolder(HtmlTextWriter output)
		{
			//
			// Render HasWaiver
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("HasWaiver");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbHasWaiver.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsPromotable
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsPromotable");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsPromotable.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PromotionFlags
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PromotionFlags");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msPromotionFlags.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsInstructor
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsInstructor");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsInstructor.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Rank
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Rank");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msRank.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RankDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("RankDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			deRankDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderSecurityFolder(HtmlTextWriter output)
		{
			//
			// Render User
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("User");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltUser.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderAccountingFolder(HtmlTextWriter output)
		{
			//
			// Render IsPastDue
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsPastDue");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsPastDue.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderRappahanockFolder(HtmlTextWriter output)
		{
			//
			// Render Customer
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Customer");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msCustomer.RenderControl(output);
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
					dojoMemberID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoMemberID;
			return myState;
		}
	}
}

