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
	[ToolboxData("<DojoTestListJournalEntryType:DojoTestListJournalEntryTypeView runat=server></{0}:DojoTestListJournalEntryTypeView>")]
	public class DojoTestListJournalEntryTypeView : TableWindow, INamingContainer
	{
		private int dojoTestListJournalEntryTypeID;
		private DojoTestListJournalEntryType dojoTestListJournalEntryType;

		#region Private Control Fields for General Folder

		private Literal ltName = new Literal();
		private Literal ltDescription = new Literal();
		private Literal ltOrderNum = new Literal();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Flags Folder

		private Literal ltEligible = new Literal();
		private Literal ltFailed = new Literal();
		private Literal ltPassed = new Literal();
		private Literal ltCertificateRequest = new Literal();
		private Literal ltCertificatePending = new Literal();
		private Literal ltCertificateReceived = new Literal();
		private Literal ltIneligible = new Literal();

		#endregion

		#region Private Control Fields for Status Changes Folder

		private Literal ltOnRemovedStatus = new Literal();
		private Literal ltOnFailedStatus = new Literal();
		private Literal ltOnPassedStatus = new Literal();
		private Literal ltOnPromotedStatus = new Literal();
		private Literal ltOnCertificateRequestedStatus = new Literal();
		private Literal ltOnCertificatePendingStatus = new Literal();
		private Literal ltOnCertificateReceivedStatus = new Literal();

		#endregion

		private Button btOk = new Button();
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
				dojoTestListJournalEntryTypeID = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();

			#region Child Controls for General Folder

			ltName.EnableViewState = false;
			Controls.Add(ltName);

			ltDescription.EnableViewState = false;
			Controls.Add(ltDescription);

			ltOrderNum.EnableViewState = false;
			Controls.Add(ltOrderNum);

			#endregion

			#region Child Controls for Flags Folder

			ltEligible.EnableViewState = false;
			Controls.Add(ltEligible);

			ltFailed.EnableViewState = false;
			Controls.Add(ltFailed);

			ltPassed.EnableViewState = false;
			Controls.Add(ltPassed);

			ltCertificateRequest.EnableViewState = false;
			Controls.Add(ltCertificateRequest);

			ltCertificatePending.EnableViewState = false;
			Controls.Add(ltCertificatePending);

			ltCertificateReceived.EnableViewState = false;
			Controls.Add(ltCertificateReceived);

			ltIneligible.EnableViewState = false;
			Controls.Add(ltIneligible);

			#endregion

			#region Child Controls for Status Changes Folder

			ltOnRemovedStatus.EnableViewState = false;
			Controls.Add(ltOnRemovedStatus);

			ltOnFailedStatus.EnableViewState = false;
			Controls.Add(ltOnFailedStatus);

			ltOnPassedStatus.EnableViewState = false;
			Controls.Add(ltOnPassedStatus);

			ltOnPromotedStatus.EnableViewState = false;
			Controls.Add(ltOnPromotedStatus);

			ltOnCertificateRequestedStatus.EnableViewState = false;
			Controls.Add(ltOnCertificateRequestedStatus);

			ltOnCertificatePendingStatus.EnableViewState = false;
			Controls.Add(ltOnCertificatePendingStatus);

			ltOnCertificateReceivedStatus.EnableViewState = false;
			Controls.Add(ltOnCertificateReceivedStatus);

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
			if(dojoTestListJournalEntryTypeID != 0)
			{
				dojoTestListJournalEntryType = new DojoTestListJournalEntryType(dojoTestListJournalEntryTypeID);

				#region Bind General Folder

				//
				// Set Field Entries
				//

				ltName.Text = dojoTestListJournalEntryType.Name.ToString();
				ltDescription.Text = dojoTestListJournalEntryType.Description.ToString();
				ltOrderNum.Text = dojoTestListJournalEntryType.OrderNum.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind _system Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//


				#endregion

				#region Bind Flags Folder

				//
				// Set Field Entries
				//

				ltEligible.Text = dojoTestListJournalEntryType.Eligible.ToString();
				ltFailed.Text = dojoTestListJournalEntryType.Failed.ToString();
				ltPassed.Text = dojoTestListJournalEntryType.Passed.ToString();
				ltCertificateRequest.Text = dojoTestListJournalEntryType.CertificateRequest.ToString();
				ltCertificatePending.Text = dojoTestListJournalEntryType.CertificatePending.ToString();
				ltCertificateReceived.Text = dojoTestListJournalEntryType.CertificateReceived.ToString();
				ltIneligible.Text = dojoTestListJournalEntryType.Ineligible.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Status Changes Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// OnRemovedStatus

				if(dojoTestListJournalEntryType.OnRemovedStatus != null)
					ltOnRemovedStatus.Text = dojoTestListJournalEntryType.OnRemovedStatus.ToString();
				else
					ltOnRemovedStatus.Text = string.Empty;

				// OnFailedStatus

				if(dojoTestListJournalEntryType.OnFailedStatus != null)
					ltOnFailedStatus.Text = dojoTestListJournalEntryType.OnFailedStatus.ToString();
				else
					ltOnFailedStatus.Text = string.Empty;

				// OnPassedStatus

				if(dojoTestListJournalEntryType.OnPassedStatus != null)
					ltOnPassedStatus.Text = dojoTestListJournalEntryType.OnPassedStatus.ToString();
				else
					ltOnPassedStatus.Text = string.Empty;

				// OnPromotedStatus

				if(dojoTestListJournalEntryType.OnPromotedStatus != null)
					ltOnPromotedStatus.Text = dojoTestListJournalEntryType.OnPromotedStatus.ToString();
				else
					ltOnPromotedStatus.Text = string.Empty;

				// OnCertificateRequestedStatus

				if(dojoTestListJournalEntryType.OnCertificateRequestedStatus != null)
					ltOnCertificateRequestedStatus.Text = dojoTestListJournalEntryType.OnCertificateRequestedStatus.ToString();
				else
					ltOnCertificateRequestedStatus.Text = string.Empty;

				// OnCertificatePendingStatus

				if(dojoTestListJournalEntryType.OnCertificatePendingStatus != null)
					ltOnCertificatePendingStatus.Text = dojoTestListJournalEntryType.OnCertificatePendingStatus.ToString();
				else
					ltOnCertificatePendingStatus.Text = string.Empty;

				// OnCertificateReceivedStatus

				if(dojoTestListJournalEntryType.OnCertificateReceivedStatus != null)
					ltOnCertificateReceivedStatus.Text = dojoTestListJournalEntryType.OnCertificateReceivedStatus.ToString();
				else
					ltOnCertificateReceivedStatus.Text = string.Empty;


				#endregion

				text = "View  - " + dojoTestListJournalEntryType.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoTestListJournalEntryType ID", dojoTestListJournalEntryTypeID.ToString());
			output.WriteEndTag("tr");

			renderGeneralFolder(output);

			render_systemFolder(output);

			renderFlagsFolder(output);

			renderStatus_ChangesFolder(output);

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

		}

		#endregion

		#region Render Flags Folder

		private void renderFlagsFolder(HtmlTextWriter output)
		{
			//
			// Render Flags Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Flags");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Eligible
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Eligible");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltEligible.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Failed
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Failed");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltFailed.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Passed
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Passed");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPassed.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render CertificateRequest
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CertificateRequest");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltCertificateRequest.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render CertificatePending
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CertificatePending");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltCertificatePending.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render CertificateReceived
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CertificateReceived");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltCertificateReceived.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Ineligible
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Ineligible");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIneligible.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Status Changes Folder

		private void renderStatus_ChangesFolder(HtmlTextWriter output)
		{
			//
			// Render Status Changes Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Status Changes");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnRemovedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnRemovedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOnRemovedStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnFailedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnFailedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOnFailedStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnPassedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnPassedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOnPassedStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnPromotedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnPromotedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOnPromotedStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnCertificateRequestedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnCertificateRequestedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOnCertificateRequestedStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnCertificatePendingStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnCertificatePendingStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOnCertificatePendingStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnCertificateReceivedStatus
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnCertificateReceivedStatus");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOnCertificateReceivedStatus.RenderControl(output);
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
