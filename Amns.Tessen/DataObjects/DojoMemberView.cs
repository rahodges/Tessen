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
	[ToolboxData("<DojoMember:DojoMemberView runat=server></{0}:DojoMemberView>")]
	public class DojoMemberView : TableWindow, INamingContainer
	{
		private int dojoMemberID;
		private DojoMember dojoMember;

		#region Private Control Fields for General Folder

		private Literal ltMemberType = new Literal();
		private Literal ltPrivateContact = new Literal();
		private Literal ltEmergencyContact = new Literal();
		private Literal ltPublicContact = new Literal();
		private Literal ltParentMember = new Literal();

		#endregion

		#region Private Control Fields for _system Folder

		private Literal ltCreateDate = new Literal();
		private Literal ltModifyDate = new Literal();

		#endregion

		#region Private Control Fields for Membership Folder

		private Literal ltMemberSince = new Literal();
		private Literal ltIsPrimaryOrgActive = new Literal();
		private Literal ltIsParentOrgActive = new Literal();
		private Literal ltLastMembershipScan = new Literal();
		private Literal ltPrimaryOrgMembership = new Literal();
		private Literal ltParentOrgMembership = new Literal();

		#endregion

		#region Private Control Fields for Attendance Folder

		private Literal ltTimeInRank = new Literal();
		private Literal ltTimeInMembership = new Literal();
		private Literal ltLastSignin = new Literal();
		private Literal ltLastAttendanceScan = new Literal();
		private Literal ltAttendanceMessage = new Literal();
		private Literal ltInstructor1 = new Literal();
		private Literal ltInstructor2 = new Literal();
		private Literal ltInstructor3 = new Literal();

		#endregion

		#region Private Control Fields for Activity Folder

		private Literal ltHasWaiver = new Literal();
		private Literal ltIsPromotable = new Literal();
		private Literal ltIsInstructor = new Literal();
		private Literal ltRankDate = new Literal();
		private Literal ltPromotionFlags = new Literal();
		private Literal ltRank = new Literal();

		#endregion

		#region Private Control Fields for Security Folder

		private Literal ltUser = new Literal();

		#endregion

		#region Private Control Fields for Accounting Folder

		private Literal ltIsPastDue = new Literal();
		private Literal ltLastDuesScan = new Literal();

		#endregion

		#region Private Control Fields for Rappahanock Folder

		private Literal ltCustomer = new Literal();

		#endregion

		private Button btOk = new Button();
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
				dojoMemberID = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();

			#region Child Controls for General Folder

			ltMemberType.EnableViewState = false;
			Controls.Add(ltMemberType);

			ltPrivateContact.EnableViewState = false;
			Controls.Add(ltPrivateContact);

			ltEmergencyContact.EnableViewState = false;
			Controls.Add(ltEmergencyContact);

			ltPublicContact.EnableViewState = false;
			Controls.Add(ltPublicContact);

			ltParentMember.EnableViewState = false;
			Controls.Add(ltParentMember);

			#endregion

			#region Child Controls for _system Folder

			ltCreateDate.EnableViewState = false;
			Controls.Add(ltCreateDate);

			ltModifyDate.EnableViewState = false;
			Controls.Add(ltModifyDate);

			#endregion

			#region Child Controls for Membership Folder

			ltMemberSince.EnableViewState = false;
			Controls.Add(ltMemberSince);

			ltIsPrimaryOrgActive.EnableViewState = false;
			Controls.Add(ltIsPrimaryOrgActive);

			ltPrimaryOrgMembership.EnableViewState = false;
			Controls.Add(ltPrimaryOrgMembership);

			ltIsParentOrgActive.EnableViewState = false;
			Controls.Add(ltIsParentOrgActive);

			ltParentOrgMembership.EnableViewState = false;
			Controls.Add(ltParentOrgMembership);

			ltLastMembershipScan.EnableViewState = false;
			Controls.Add(ltLastMembershipScan);

			#endregion

			#region Child Controls for Attendance Folder

			ltTimeInRank.EnableViewState = false;
			Controls.Add(ltTimeInRank);

			ltTimeInMembership.EnableViewState = false;
			Controls.Add(ltTimeInMembership);

			ltInstructor1.EnableViewState = false;
			Controls.Add(ltInstructor1);

			ltInstructor2.EnableViewState = false;
			Controls.Add(ltInstructor2);

			ltInstructor3.EnableViewState = false;
			Controls.Add(ltInstructor3);

			ltLastSignin.EnableViewState = false;
			Controls.Add(ltLastSignin);

			ltLastAttendanceScan.EnableViewState = false;
			Controls.Add(ltLastAttendanceScan);

			ltAttendanceMessage.EnableViewState = false;
			Controls.Add(ltAttendanceMessage);

			#endregion

			#region Child Controls for Activity Folder

			ltHasWaiver.EnableViewState = false;
			Controls.Add(ltHasWaiver);

			ltIsPromotable.EnableViewState = false;
			Controls.Add(ltIsPromotable);

			ltPromotionFlags.EnableViewState = false;
			Controls.Add(ltPromotionFlags);

			ltIsInstructor.EnableViewState = false;
			Controls.Add(ltIsInstructor);

			ltRank.EnableViewState = false;
			Controls.Add(ltRank);

			ltRankDate.EnableViewState = false;
			Controls.Add(ltRankDate);

			#endregion

			#region Child Controls for Security Folder

			ltUser.EnableViewState = false;
			Controls.Add(ltUser);

			#endregion

			#region Child Controls for Accounting Folder

			ltIsPastDue.EnableViewState = false;
			Controls.Add(ltIsPastDue);

			ltLastDuesScan.EnableViewState = false;
			Controls.Add(ltLastDuesScan);

			#endregion

			#region Child Controls for Rappahanock Folder

			ltCustomer.EnableViewState = false;
			Controls.Add(ltCustomer);

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
			if(dojoMemberID != 0)
			{
				dojoMember = new DojoMember(dojoMemberID);

				#region Bind General Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// MemberType

				if(dojoMember.MemberType != null)
					ltMemberType.Text = dojoMember.MemberType.ToString();
				else
					ltMemberType.Text = string.Empty;

				// PrivateContact

				if(dojoMember.PrivateContact != null)
					ltPrivateContact.Text = dojoMember.PrivateContact.ToString();
				else
					ltPrivateContact.Text = string.Empty;

				// EmergencyContact

				if(dojoMember.EmergencyContact != null)
					ltEmergencyContact.Text = dojoMember.EmergencyContact.ToString();
				else
					ltEmergencyContact.Text = string.Empty;

				// PublicContact

				if(dojoMember.PublicContact != null)
					ltPublicContact.Text = dojoMember.PublicContact.ToString();
				else
					ltPublicContact.Text = string.Empty;

				// ParentMember

				if(dojoMember.ParentMember != null)
					ltParentMember.Text = dojoMember.ParentMember.ToString();
				else
					ltParentMember.Text = string.Empty;


				#endregion

				#region Bind _system Folder

				//
				// Set Field Entries
				//

				ltCreateDate.Text = dojoMember.CreateDate.ToString();
				ltModifyDate.Text = dojoMember.ModifyDate.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Membership Folder

				//
				// Set Field Entries
				//

				ltMemberSince.Text = dojoMember.MemberSince.ToString();
				ltIsPrimaryOrgActive.Text = dojoMember.IsPrimaryOrgActive.ToString();
				ltIsParentOrgActive.Text = dojoMember.IsParentOrgActive.ToString();
				ltLastMembershipScan.Text = dojoMember.LastMembershipScan.ToString();

				//
				// Set Children Selections
				//

				// PrimaryOrgMembership

				if(dojoMember.PrimaryOrgMembership != null)
					ltPrimaryOrgMembership.Text = dojoMember.PrimaryOrgMembership.ToString();
				else
					ltPrimaryOrgMembership.Text = string.Empty;

				// ParentOrgMembership

				if(dojoMember.ParentOrgMembership != null)
					ltParentOrgMembership.Text = dojoMember.ParentOrgMembership.ToString();
				else
					ltParentOrgMembership.Text = string.Empty;


				#endregion

				#region Bind Attendance Folder

				//
				// Set Field Entries
				//

				ltTimeInRank.Text = dojoMember.TimeInRank.ToString();
				ltTimeInMembership.Text = dojoMember.TimeInMembership.ToString();
				ltLastSignin.Text = dojoMember.LastSignin.ToString();
				ltLastAttendanceScan.Text = dojoMember.LastAttendanceScan.ToString();
				ltAttendanceMessage.Text = dojoMember.AttendanceMessage.ToString();

				//
				// Set Children Selections
				//

				// Instructor1

				if(dojoMember.Instructor1 != null)
					ltInstructor1.Text = dojoMember.Instructor1.ToString();
				else
					ltInstructor1.Text = string.Empty;

				// Instructor2

				if(dojoMember.Instructor2 != null)
					ltInstructor2.Text = dojoMember.Instructor2.ToString();
				else
					ltInstructor2.Text = string.Empty;

				// Instructor3

				if(dojoMember.Instructor3 != null)
					ltInstructor3.Text = dojoMember.Instructor3.ToString();
				else
					ltInstructor3.Text = string.Empty;


				#endregion

				#region Bind Activity Folder

				//
				// Set Field Entries
				//

				ltHasWaiver.Text = dojoMember.HasWaiver.ToString();
				ltIsPromotable.Text = dojoMember.IsPromotable.ToString();
				ltIsInstructor.Text = dojoMember.IsInstructor.ToString();
				ltRankDate.Text = dojoMember.RankDate.ToString();

				//
				// Set Children Selections
				//

				// PromotionFlags

				if(dojoMember.PromotionFlags != null)
					ltPromotionFlags.Text = dojoMember.PromotionFlags.ToString();
				else
					ltPromotionFlags.Text = string.Empty;

				// Rank

				if(dojoMember.Rank != null)
					ltRank.Text = dojoMember.Rank.ToString();
				else
					ltRank.Text = string.Empty;


				#endregion

				#region Bind Security Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// User

				if(dojoMember.User != null)
					ltUser.Text = dojoMember.User.ToString();
				else
					ltUser.Text = string.Empty;


				#endregion

				#region Bind Accounting Folder

				//
				// Set Field Entries
				//

				ltIsPastDue.Text = dojoMember.IsPastDue.ToString();
				ltLastDuesScan.Text = dojoMember.LastDuesScan.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Rappahanock Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// Customer

				if(dojoMember.Customer != null)
					ltCustomer.Text = dojoMember.Customer.ToString();
				else
					ltCustomer.Text = string.Empty;


				#endregion

				text = "View  - " + dojoMember.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoMember ID", dojoMemberID.ToString());
			output.WriteEndTag("tr");

			renderGeneralFolder(output);

			render_systemFolder(output);

			renderMembershipFolder(output);

			renderAttendanceFolder(output);

			renderActivityFolder(output);

			renderSecurityFolder(output);

			renderAccountingFolder(output);

			renderRappahanockFolder(output);

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

		#region Render General Folder

		private void renderGeneralFolder(HtmlTextWriter output)
		{
			//
			// Render General Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("General");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberType
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberType");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltMemberType.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PrivateContact
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PrivateContact");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
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
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("EmergencyContact");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
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
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PublicContact");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPublicContact.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentMember
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentMember");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltParentMember.RenderControl(output);
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

			//
			// Render CreateDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CreateDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
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
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ModifyDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltModifyDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Membership Folder

		private void renderMembershipFolder(HtmlTextWriter output)
		{
			//
			// Render Membership Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Membership");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberSince
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Member Since");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltMemberSince.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsPrimaryOrgActive
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Dojo membership activity.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsPrimaryOrgActive.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PrimaryOrgMembership
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PrimaryOrgMembership");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPrimaryOrgMembership.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsParentOrgActive
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Is Parent Active?");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsParentOrgActive.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentOrgMembership
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentOrgMembership");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltParentOrgMembership.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render LastMembershipScan
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("LastMembershipScan");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltLastMembershipScan.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Attendance Folder

		private void renderAttendanceFolder(HtmlTextWriter output)
		{
			//
			// Render Attendance Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Attendance");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render TimeInRank
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("TimeInRank");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
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
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("TimeInMembership");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
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
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Instructor1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltInstructor1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Instructor2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Instructor2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltInstructor2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Instructor3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Instructor3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltInstructor3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render LastSignin
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Last Signin");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
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
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("LastAttendanceScan");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
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
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AttendanceMessage");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAttendanceMessage.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Activity Folder

		private void renderActivityFolder(HtmlTextWriter output)
		{
			//
			// Render Activity Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Activity");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render HasWaiver
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("HasWaiver");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltHasWaiver.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsPromotable
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsPromotable");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsPromotable.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PromotionFlags
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PromotionFlags");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPromotionFlags.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsInstructor
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsInstructor");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsInstructor.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Rank
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Rank");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRank.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RankDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("RankDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRankDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Security Folder

		private void renderSecurityFolder(HtmlTextWriter output)
		{
			//
			// Render Security Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Security");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render User
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("User");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltUser.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Accounting Folder

		private void renderAccountingFolder(HtmlTextWriter output)
		{
			//
			// Render Accounting Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Accounting");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsPastDue
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsPastDue");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsPastDue.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render LastDuesScan
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("LastDuesScan");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltLastDuesScan.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Rappahanock Folder

		private void renderRappahanockFolder(HtmlTextWriter output)
		{
			//
			// Render Rappahanock Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Rappahanock");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Customer
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Customer");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltCustomer.RenderControl(output);
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
