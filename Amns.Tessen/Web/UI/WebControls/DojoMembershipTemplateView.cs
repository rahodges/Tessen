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
	[ToolboxData("<DojoMembershipTemplate:DojoMembershipTemplateView runat=server></{0}:DojoMembershipTemplateView>")]
	public class DojoMembershipTemplateView : TableWindow, INamingContainer
	{
		private int dojoMembershipTemplateID;
		private DojoMembershipTemplate dojoMembershipTemplate;

		#region Private Control Fields for _system Folder

		private Literal ltCreateDate = new Literal();
		private Literal ltModifyDate = new Literal();

		#endregion

		#region Private Control Fields for General Folder

		private Literal ltName = new Literal();
		private Literal ltDescription = new Literal();
		private Literal ltOrderNum = new Literal();
		private Literal ltIsPrimary = new Literal();
		private Literal ltOrganization = new Literal();

		#endregion

		#region Private Control Fields for Fee Schedule Folder

		private Literal ltFee = new Literal();
		private Literal ltStartupFee = new Literal();
		private Literal ltProRateMonthA = new Literal();
		private Literal ltProRateDayA = new Literal();
		private Literal ltProRateFeeA = new Literal();
		private Literal ltProRateMonthB = new Literal();
		private Literal ltProRateDayB = new Literal();
		private Literal ltProRateFeeB = new Literal();
		private Literal ltProRateMonthC = new Literal();
		private Literal ltProRateDayC = new Literal();
		private Literal ltProRateFeeC = new Literal();

		#endregion

		#region Private Control Fields for Requirements Folder

		private Literal ltAgeYearsMax = new Literal();
		private Literal ltAgeYearsMin = new Literal();
		private Literal ltMemberForMin = new Literal();
		private Literal ltMemberForMax = new Literal();
		private Literal ltPurchaseRequired = new Literal();
		private Literal ltTestRequired = new Literal();
		private Literal ltAttendanceRequired = new Literal();
		private Literal ltRankMin = new Literal();
		private Literal ltRankMax = new Literal();

		#endregion

		#region Private Control Fields for Membership Duration Folder


		#endregion

		#region Private Control Fields for Rappahanock Folder

		private Literal ltItem = new Literal();
		private Literal ltItemClass = new Literal();

		#endregion

		private Button btOk = new Button();
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
				dojoMembershipTemplateID = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();

			#region Child Controls for _system Folder

			ltCreateDate.EnableViewState = false;
			Controls.Add(ltCreateDate);

			ltModifyDate.EnableViewState = false;
			Controls.Add(ltModifyDate);

			#endregion

			#region Child Controls for General Folder

			ltName.EnableViewState = false;
			Controls.Add(ltName);

			ltDescription.EnableViewState = false;
			Controls.Add(ltDescription);

			ltOrderNum.EnableViewState = false;
			Controls.Add(ltOrderNum);

			ltOrganization.EnableViewState = false;
			Controls.Add(ltOrganization);

			ltIsPrimary.EnableViewState = false;
			Controls.Add(ltIsPrimary);

			#endregion

			#region Child Controls for Fee Schedule Folder

			ltFee.EnableViewState = false;
			Controls.Add(ltFee);

			ltStartupFee.EnableViewState = false;
			Controls.Add(ltStartupFee);

			ltProRateMonthA.EnableViewState = false;
			Controls.Add(ltProRateMonthA);

			ltProRateDayA.EnableViewState = false;
			Controls.Add(ltProRateDayA);

			ltProRateFeeA.EnableViewState = false;
			Controls.Add(ltProRateFeeA);

			ltProRateMonthB.EnableViewState = false;
			Controls.Add(ltProRateMonthB);

			ltProRateDayB.EnableViewState = false;
			Controls.Add(ltProRateDayB);

			ltProRateFeeB.EnableViewState = false;
			Controls.Add(ltProRateFeeB);

			ltProRateMonthC.EnableViewState = false;
			Controls.Add(ltProRateMonthC);

			ltProRateDayC.EnableViewState = false;
			Controls.Add(ltProRateDayC);

			ltProRateFeeC.EnableViewState = false;
			Controls.Add(ltProRateFeeC);

			#endregion

			#region Child Controls for Requirements Folder

			ltAgeYearsMax.EnableViewState = false;
			Controls.Add(ltAgeYearsMax);

			ltAgeYearsMin.EnableViewState = false;
			Controls.Add(ltAgeYearsMin);

			ltMemberForMin.EnableViewState = false;
			Controls.Add(ltMemberForMin);

			ltMemberForMax.EnableViewState = false;
			Controls.Add(ltMemberForMax);

			ltRankMin.EnableViewState = false;
			Controls.Add(ltRankMin);

			ltRankMax.EnableViewState = false;
			Controls.Add(ltRankMax);

			ltPurchaseRequired.EnableViewState = false;
			Controls.Add(ltPurchaseRequired);

			ltTestRequired.EnableViewState = false;
			Controls.Add(ltTestRequired);

			ltAttendanceRequired.EnableViewState = false;
			Controls.Add(ltAttendanceRequired);

			#endregion

			#region Child Controls for Membership Duration Folder

			#endregion

			#region Child Controls for Rappahanock Folder

			ltItem.EnableViewState = false;
			Controls.Add(ltItem);

			ltItemClass.EnableViewState = false;
			Controls.Add(ltItemClass);

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
			if(dojoMembershipTemplateID != 0)
			{
				dojoMembershipTemplate = new DojoMembershipTemplate(dojoMembershipTemplateID);

				#region Bind _system Folder

				//
				// Set Field Entries
				//

				ltCreateDate.Text = dojoMembershipTemplate.CreateDate.ToString();
				ltModifyDate.Text = dojoMembershipTemplate.ModifyDate.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind General Folder

				//
				// Set Field Entries
				//

				ltName.Text = dojoMembershipTemplate.Name.ToString();
				ltDescription.Text = dojoMembershipTemplate.Description.ToString();
				ltOrderNum.Text = dojoMembershipTemplate.OrderNum.ToString();
				ltIsPrimary.Text = dojoMembershipTemplate.IsPrimary.ToString();

				//
				// Set Children Selections
				//

				// Organization

				if(dojoMembershipTemplate.Organization != null)
					ltOrganization.Text = dojoMembershipTemplate.Organization.ToString();
				else
					ltOrganization.Text = string.Empty;


				#endregion

				#region Bind Fee Schedule Folder

				//
				// Set Field Entries
				//

				ltFee.Text = dojoMembershipTemplate.Fee.ToString();
				ltStartupFee.Text = dojoMembershipTemplate.StartupFee.ToString();
				ltProRateMonthA.Text = dojoMembershipTemplate.ProRateMonthA.ToString();
				ltProRateDayA.Text = dojoMembershipTemplate.ProRateDayA.ToString();
				ltProRateFeeA.Text = dojoMembershipTemplate.ProRateFeeA.ToString();
				ltProRateMonthB.Text = dojoMembershipTemplate.ProRateMonthB.ToString();
				ltProRateDayB.Text = dojoMembershipTemplate.ProRateDayB.ToString();
				ltProRateFeeB.Text = dojoMembershipTemplate.ProRateFeeB.ToString();
				ltProRateMonthC.Text = dojoMembershipTemplate.ProRateMonthC.ToString();
				ltProRateDayC.Text = dojoMembershipTemplate.ProRateDayC.ToString();
				ltProRateFeeC.Text = dojoMembershipTemplate.ProRateFeeC.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Requirements Folder

				//
				// Set Field Entries
				//

				ltAgeYearsMax.Text = dojoMembershipTemplate.AgeYearsMax.ToString();
				ltAgeYearsMin.Text = dojoMembershipTemplate.AgeYearsMin.ToString();
				ltMemberForMin.Text = dojoMembershipTemplate.MemberForMin.ToString();
				ltMemberForMax.Text = dojoMembershipTemplate.MemberForMax.ToString();
				ltPurchaseRequired.Text = dojoMembershipTemplate.PurchaseRequired.ToString();
				ltTestRequired.Text = dojoMembershipTemplate.TestRequired.ToString();
				ltAttendanceRequired.Text = dojoMembershipTemplate.AttendanceRequired.ToString();

				//
				// Set Children Selections
				//

				// RankMin

				if(dojoMembershipTemplate.RankMin != null)
					ltRankMin.Text = dojoMembershipTemplate.RankMin.ToString();
				else
					ltRankMin.Text = string.Empty;

				// RankMax

				if(dojoMembershipTemplate.RankMax != null)
					ltRankMax.Text = dojoMembershipTemplate.RankMax.ToString();
				else
					ltRankMax.Text = string.Empty;


				#endregion

				#region Bind Membership Duration Folder

				//
				// Set Field Entries
				//


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

				// Item

				if(dojoMembershipTemplate.Item != null)
					ltItem.Text = dojoMembershipTemplate.Item.ToString();
				else
					ltItem.Text = string.Empty;

				// ItemClass

				if(dojoMembershipTemplate.ItemClass != null)
					ltItemClass.Text = dojoMembershipTemplate.ItemClass.ToString();
				else
					ltItemClass.Text = string.Empty;


				#endregion

				text = "View Membership Template - " + dojoMembershipTemplate.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoMembershipTemplate ID", dojoMembershipTemplateID.ToString());
			output.WriteEndTag("tr");

			render_systemFolder(output);

			renderGeneralFolder(output);

			renderFee_ScheduleFolder(output);

			renderRequirementsFolder(output);

			renderMembership_DurationFolder(output);

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
			// Render Name
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Name");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltName.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Description
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Description");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDescription.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OrderNum
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OrderNum");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOrderNum.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Organization
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Organization");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOrganization.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsPrimary
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsPrimary");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsPrimary.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Fee Schedule Folder

		private void renderFee_ScheduleFolder(HtmlTextWriter output)
		{
			//
			// Render Fee Schedule Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Fee Schedule");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Fee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Fee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render StartupFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("StartupFee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltStartupFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ProRateMonthA
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ProRateMonthA");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltProRateMonthA.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ProRateDayA
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ProRateDayA");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltProRateDayA.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ProRateFeeA
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ProRateFeeA");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltProRateFeeA.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ProRateMonthB
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ProRateMonthB");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltProRateMonthB.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ProRateDayB
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ProRateDayB");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltProRateDayB.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ProRateFeeB
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ProRateFeeB");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltProRateFeeB.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ProRateMonthC
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ProRateMonthC");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltProRateMonthC.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ProRateDayC
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ProRateDayC");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltProRateDayC.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ProRateFeeC
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ProRateFeeC");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltProRateFeeC.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Requirements Folder

		private void renderRequirementsFolder(HtmlTextWriter output)
		{
			//
			// Render Requirements Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Requirements");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AgeYearsMax
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AgeYearsMax");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAgeYearsMax.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AgeYearsMin
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Age years.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAgeYearsMin.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberForMin
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberForMin");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltMemberForMin.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberForMax
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberForMax");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltMemberForMax.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RankMin
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("RankMin");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRankMin.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RankMax
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("RankMax");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRankMax.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PurchaseRequired
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PurchaseRequired");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPurchaseRequired.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render TestRequired
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("TestRequired");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltTestRequired.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AttendanceRequired
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AttendanceRequired");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAttendanceRequired.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Membership Duration Folder

		private void renderMembership_DurationFolder(HtmlTextWriter output)
		{
			//
			// Render Membership Duration Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Membership Duration");
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
			// Render Item
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Item");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltItem.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ItemClass
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ItemClass");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltItemClass.RenderControl(output);
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
