using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Security;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoMemberTypeTemplate.
	/// </summary>
	[ToolboxData("<DojoMemberTypeTemplate:DojoMemberTypeTemplateView runat=server></{0}:DojoMemberTypeTemplateView>")]
	public class DojoMemberTypeTemplateView : TableWindow, INamingContainer
	{
		private int dojoMemberTypeTemplateID;
		private DojoMemberTypeTemplate dojoMemberTypeTemplate;

		#region Private Control Fields for _system Folder

		private Literal ltCreateDate = new Literal();
		private Literal ltModifyDate = new Literal();

		#endregion

		#region Private Control Fields for General Folder

		private Literal ltName = new Literal();
		private Literal ltDescription = new Literal();
		private Literal ltOrderNum = new Literal();
		private Literal ltMemberTypeTreeHash = new Literal();
		private Literal ltParent = new Literal();
		private Literal ltMemberType = new Literal();

		#endregion

		#region Private Control Fields for Initialization Folder

		private Literal ltInitialEmailFrom = new Literal();
		private Literal ltInitialEmailBody = new Literal();
		private Literal ltInitialRank = new Literal();
		private Literal ltInitialRole = new Literal();

		#endregion

		#region Private Control Fields for Access Features Folder

		private Literal ltAllowGuestPurchase = new Literal();
		private Literal ltAllowPurchase = new Literal();
		private Literal ltAllowRenewal = new Literal();
		private Literal ltAllowAutoRenewal = new Literal();

		#endregion

		#region Private Control Fields for Requirements Folder

		private Literal ltAgeYearsMax = new Literal();
		private Literal ltAgeYearsMin = new Literal();
		private Literal ltMemberForMin = new Literal();
		private Literal ltMemberForMax = new Literal();
		private Literal ltRankMin = new Literal();
		private Literal ltRankMax = new Literal();

		#endregion

		#region Private Control Fields for Membership Templates Folder

		private Literal ltMembershipTemplate1 = new Literal();
		private Literal ltMembershipTemplate2 = new Literal();
		private Literal ltMembershipTemplate3 = new Literal();
		private Literal ltMembershipTemplate4 = new Literal();
		private Literal ltMembershipTemplate5 = new Literal();

		#endregion

		private Button btOk = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoMemberTypeTemplateID
		{
			get
			{
				return dojoMemberTypeTemplateID;
			}
			set
			{
				dojoMemberTypeTemplateID = value;
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

			ltParent.EnableViewState = false;
			Controls.Add(ltParent);

			ltMemberType.EnableViewState = false;
			Controls.Add(ltMemberType);

			ltMemberTypeTreeHash.EnableViewState = false;
			Controls.Add(ltMemberTypeTreeHash);

			#endregion

			#region Child Controls for Initialization Folder

			ltInitialRank.EnableViewState = false;
			Controls.Add(ltInitialRank);

			ltInitialRole.EnableViewState = false;
			Controls.Add(ltInitialRole);

			ltInitialEmailFrom.EnableViewState = false;
			Controls.Add(ltInitialEmailFrom);

			ltInitialEmailBody.EnableViewState = false;
			Controls.Add(ltInitialEmailBody);

			#endregion

			#region Child Controls for Access Features Folder

			ltAllowGuestPurchase.EnableViewState = false;
			Controls.Add(ltAllowGuestPurchase);

			ltAllowPurchase.EnableViewState = false;
			Controls.Add(ltAllowPurchase);

			ltAllowRenewal.EnableViewState = false;
			Controls.Add(ltAllowRenewal);

			ltAllowAutoRenewal.EnableViewState = false;
			Controls.Add(ltAllowAutoRenewal);

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

			#endregion

			#region Child Controls for Membership Templates Folder

			ltMembershipTemplate1.EnableViewState = false;
			Controls.Add(ltMembershipTemplate1);

			ltMembershipTemplate2.EnableViewState = false;
			Controls.Add(ltMembershipTemplate2);

			ltMembershipTemplate3.EnableViewState = false;
			Controls.Add(ltMembershipTemplate3);

			ltMembershipTemplate4.EnableViewState = false;
			Controls.Add(ltMembershipTemplate4);

			ltMembershipTemplate5.EnableViewState = false;
			Controls.Add(ltMembershipTemplate5);

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
			if(dojoMemberTypeTemplateID != 0)
			{
				dojoMemberTypeTemplate = new DojoMemberTypeTemplate(dojoMemberTypeTemplateID);

				#region Bind _system Folder

				//
				// Set Field Entries
				//

				ltCreateDate.Text = dojoMemberTypeTemplate.CreateDate.ToString();
				ltModifyDate.Text = dojoMemberTypeTemplate.ModifyDate.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind General Folder

				//
				// Set Field Entries
				//

				ltName.Text = dojoMemberTypeTemplate.Name.ToString();
				ltDescription.Text = dojoMemberTypeTemplate.Description.ToString();
				ltOrderNum.Text = dojoMemberTypeTemplate.OrderNum.ToString();
				ltMemberTypeTreeHash.Text = dojoMemberTypeTemplate.MemberTypeTreeHash.ToString();

				//
				// Set Children Selections
				//

				// Parent

				if(dojoMemberTypeTemplate.Parent != null)
					ltParent.Text = dojoMemberTypeTemplate.Parent.ToString();
				else
					ltParent.Text = string.Empty;

				// MemberType

				if(dojoMemberTypeTemplate.MemberType != null)
					ltMemberType.Text = dojoMemberTypeTemplate.MemberType.ToString();
				else
					ltMemberType.Text = string.Empty;


				#endregion

				#region Bind Initialization Folder

				//
				// Set Field Entries
				//

				ltInitialEmailFrom.Text = dojoMemberTypeTemplate.InitialEmailFrom.ToString();
				ltInitialEmailBody.Text = dojoMemberTypeTemplate.InitialEmailBody.ToString();

				//
				// Set Children Selections
				//

				// InitialRank

				if(dojoMemberTypeTemplate.InitialRank != null)
					ltInitialRank.Text = dojoMemberTypeTemplate.InitialRank.ToString();
				else
					ltInitialRank.Text = string.Empty;

				// InitialRole

				if(dojoMemberTypeTemplate.InitialRole != null)
					ltInitialRole.Text = dojoMemberTypeTemplate.InitialRole.ToString();
				else
					ltInitialRole.Text = string.Empty;


				#endregion

				#region Bind Access Features Folder

				//
				// Set Field Entries
				//

				ltAllowGuestPurchase.Text = dojoMemberTypeTemplate.AllowGuestPurchase.ToString();
				ltAllowPurchase.Text = dojoMemberTypeTemplate.AllowPurchase.ToString();
				ltAllowRenewal.Text = dojoMemberTypeTemplate.AllowRenewal.ToString();
				ltAllowAutoRenewal.Text = dojoMemberTypeTemplate.AllowAutoRenewal.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Requirements Folder

				//
				// Set Field Entries
				//

				ltAgeYearsMax.Text = dojoMemberTypeTemplate.AgeYearsMax.ToString();
				ltAgeYearsMin.Text = dojoMemberTypeTemplate.AgeYearsMin.ToString();
				ltMemberForMin.Text = dojoMemberTypeTemplate.MemberForMin.ToString();
				ltMemberForMax.Text = dojoMemberTypeTemplate.MemberForMax.ToString();

				//
				// Set Children Selections
				//

				// RankMin

				if(dojoMemberTypeTemplate.RankMin != null)
					ltRankMin.Text = dojoMemberTypeTemplate.RankMin.ToString();
				else
					ltRankMin.Text = string.Empty;

				// RankMax

				if(dojoMemberTypeTemplate.RankMax != null)
					ltRankMax.Text = dojoMemberTypeTemplate.RankMax.ToString();
				else
					ltRankMax.Text = string.Empty;


				#endregion

				#region Bind Membership Templates Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// MembershipTemplate1

				if(dojoMemberTypeTemplate.MembershipTemplate1 != null)
					ltMembershipTemplate1.Text = dojoMemberTypeTemplate.MembershipTemplate1.ToString();
				else
					ltMembershipTemplate1.Text = string.Empty;

				// MembershipTemplate2

				if(dojoMemberTypeTemplate.MembershipTemplate2 != null)
					ltMembershipTemplate2.Text = dojoMemberTypeTemplate.MembershipTemplate2.ToString();
				else
					ltMembershipTemplate2.Text = string.Empty;

				// MembershipTemplate3

				if(dojoMemberTypeTemplate.MembershipTemplate3 != null)
					ltMembershipTemplate3.Text = dojoMemberTypeTemplate.MembershipTemplate3.ToString();
				else
					ltMembershipTemplate3.Text = string.Empty;

				// MembershipTemplate4

				if(dojoMemberTypeTemplate.MembershipTemplate4 != null)
					ltMembershipTemplate4.Text = dojoMemberTypeTemplate.MembershipTemplate4.ToString();
				else
					ltMembershipTemplate4.Text = string.Empty;

				// MembershipTemplate5

				if(dojoMemberTypeTemplate.MembershipTemplate5 != null)
					ltMembershipTemplate5.Text = dojoMemberTypeTemplate.MembershipTemplate5.ToString();
				else
					ltMembershipTemplate5.Text = string.Empty;


				#endregion

				text = "View  - " + dojoMemberTypeTemplate.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoMemberTypeTemplate ID", dojoMemberTypeTemplateID.ToString());
			output.WriteEndTag("tr");

			render_systemFolder(output);

			renderGeneralFolder(output);

			renderInitializationFolder(output);

			renderAccess_FeaturesFolder(output);

			renderRequirementsFolder(output);

			renderMembership_TemplatesFolder(output);

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
			// Render Parent
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Parent Template Group");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltParent.RenderControl(output);
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
			output.Write("Member Type");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltMemberType.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MemberTypeTreeHash
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MemberTypeTreeHash");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltMemberTypeTreeHash.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Initialization Folder

		private void renderInitializationFolder(HtmlTextWriter output)
		{
			//
			// Render Initialization Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Initialization");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render InitialRank
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("InitialRank");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltInitialRank.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render InitialRole
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("InitialRole");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltInitialRole.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render InitialEmailFrom
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("InitialEmailFrom");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltInitialEmailFrom.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render InitialEmailBody
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("InitialEmailBody");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltInitialEmailBody.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Access Features Folder

		private void renderAccess_FeaturesFolder(HtmlTextWriter output)
		{
			//
			// Render Access Features Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Access Features");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowGuestPurchase
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowGuestPurchase");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAllowGuestPurchase.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowPurchase
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowPurchase");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAllowPurchase.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowRenewal
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowRenewal");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAllowRenewal.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowAutoRenewal
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowAutoRenewal");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAllowAutoRenewal.RenderControl(output);
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

		}

		#endregion

		#region Render Membership Templates Folder

		private void renderMembership_TemplatesFolder(HtmlTextWriter output)
		{
			//
			// Render Membership Templates Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Membership Templates");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MembershipTemplate1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MembershipTemplate1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltMembershipTemplate1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MembershipTemplate2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MembershipTemplate2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltMembershipTemplate2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MembershipTemplate3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MembershipTemplate3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltMembershipTemplate3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MembershipTemplate4
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MembershipTemplate4");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltMembershipTemplate4.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MembershipTemplate5
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MembershipTemplate5");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltMembershipTemplate5.RenderControl(output);
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
					dojoMemberTypeTemplateID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoMemberTypeTemplateID;
			return myState;
		}
	}
}
