using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.Rappahanock;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoSeminarRegistration.
	/// </summary>
	[ToolboxData("<DojoSeminarRegistration:DojoSeminarRegistrationView runat=server></{0}:DojoSeminarRegistrationView>")]
	public class DojoSeminarRegistrationView : TableWindow, INamingContainer
	{
		private int dojoSeminarRegistrationID;
		private DojoSeminarRegistration dojoSeminarRegistration;

		#region Private Control Fields for General Folder

		private Literal ltStatus = new Literal();
		private Literal ltRegistrationDate = new Literal();
		private Literal ltClassUnits = new Literal();
		private Literal ltParentSeminar = new Literal();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Payment Details Folder

		private Literal ltTotalFee = new Literal();
		private Literal ltPaymentDate = new Literal();
		private Literal ltPaymentReference = new Literal();
		private Literal ltPaymentAmount = new Literal();

		#endregion

		#region Private Control Fields for Contact Folder

		private Literal ltContact = new Literal();

		#endregion

		#region Private Control Fields for Rappahanock Folder

		private Literal ltCustomer = new Literal();
		private Literal ltInvoiceLine = new Literal();
		private Literal ltSalesOrderLine = new Literal();

		#endregion

		private Button btOk = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoSeminarRegistrationID
		{
			get
			{
				return dojoSeminarRegistrationID;
			}
			set
			{
				dojoSeminarRegistrationID = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();

			#region Child Controls for General Folder

			ltStatus.EnableViewState = false;
			Controls.Add(ltStatus);

			ltParentSeminar.EnableViewState = false;
			Controls.Add(ltParentSeminar);

			ltRegistrationDate.EnableViewState = false;
			Controls.Add(ltRegistrationDate);

			ltClassUnits.EnableViewState = false;
			Controls.Add(ltClassUnits);

			#endregion

			#region Child Controls for Payment Details Folder

			ltTotalFee.EnableViewState = false;
			Controls.Add(ltTotalFee);

			ltPaymentDate.EnableViewState = false;
			Controls.Add(ltPaymentDate);

			ltPaymentReference.EnableViewState = false;
			Controls.Add(ltPaymentReference);

			ltPaymentAmount.EnableViewState = false;
			Controls.Add(ltPaymentAmount);

			#endregion

			#region Child Controls for Contact Folder

			ltContact.EnableViewState = false;
			Controls.Add(ltContact);

			#endregion

			#region Child Controls for Rappahanock Folder

			ltCustomer.EnableViewState = false;
			Controls.Add(ltCustomer);

			ltInvoiceLine.EnableViewState = false;
			Controls.Add(ltInvoiceLine);

			ltSalesOrderLine.EnableViewState = false;
			Controls.Add(ltSalesOrderLine);

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
			if(dojoSeminarRegistrationID != 0)
			{
				dojoSeminarRegistration = new DojoSeminarRegistration(dojoSeminarRegistrationID);

				#region Bind General Folder

				//
				// Set Field Entries
				//

				ltStatus.Text = dojoSeminarRegistration.Status.ToString();
				ltRegistrationDate.Text = dojoSeminarRegistration.RegistrationDate.ToString();
				ltClassUnits.Text = dojoSeminarRegistration.ClassUnits.ToString();

				//
				// Set Children Selections
				//

				// ParentSeminar

				if(dojoSeminarRegistration.ParentSeminar != null)
					ltParentSeminar.Text = dojoSeminarRegistration.ParentSeminar.ToString();
				else
					ltParentSeminar.Text = string.Empty;


				#endregion

				#region Bind _system Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//


				#endregion

				#region Bind Payment Details Folder

				//
				// Set Field Entries
				//

				ltTotalFee.Text = dojoSeminarRegistration.TotalFee.ToString();
				ltPaymentDate.Text = dojoSeminarRegistration.PaymentDate.ToString();
				ltPaymentReference.Text = dojoSeminarRegistration.PaymentReference.ToString();
				ltPaymentAmount.Text = dojoSeminarRegistration.PaymentAmount.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Contact Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// Contact

				if(dojoSeminarRegistration.Contact != null)
					ltContact.Text = dojoSeminarRegistration.Contact.ToString();
				else
					ltContact.Text = string.Empty;


				#endregion

				#region Bind Rappahanock Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// Customer

				if(dojoSeminarRegistration.Customer != null)
					ltCustomer.Text = dojoSeminarRegistration.Customer.ToString();
				else
					ltCustomer.Text = string.Empty;

				// InvoiceLine

				if(dojoSeminarRegistration.InvoiceLine != null)
					ltInvoiceLine.Text = dojoSeminarRegistration.InvoiceLine.ToString();
				else
					ltInvoiceLine.Text = string.Empty;

				// SalesOrderLine

				if(dojoSeminarRegistration.SalesOrderLine != null)
					ltSalesOrderLine.Text = dojoSeminarRegistration.SalesOrderLine.ToString();
				else
					ltSalesOrderLine.Text = string.Empty;


				#endregion

				text = "View  - " + dojoSeminarRegistration.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoSeminarRegistration ID", dojoSeminarRegistrationID.ToString());
			output.WriteEndTag("tr");

			renderGeneralFolder(output);

			render_systemFolder(output);

			renderPayment_DetailsFolder(output);

			renderContactFolder(output);

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
			// Render Status
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Registration status");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentSeminar
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentSeminar");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltParentSeminar.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RegistrationDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("RegistrationDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRegistrationDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ClassUnits
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class units");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClassUnits.RenderControl(output);
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

		#region Render Payment Details Folder

		private void renderPayment_DetailsFolder(HtmlTextWriter output)
		{
			//
			// Render Payment Details Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Payment Details");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render TotalFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Total registration fee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltTotalFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PaymentDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Date of payment");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPaymentDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PaymentReference
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PaymentReference");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPaymentReference.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PaymentAmount
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Amount paid");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPaymentAmount.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Contact Folder

		private void renderContactFolder(HtmlTextWriter output)
		{
			//
			// Render Contact Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Contact");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Contact
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Contact");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltContact.RenderControl(output);
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
			output.Write("Registrant");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltCustomer.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render InvoiceLine
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Invoice Line");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltInvoiceLine.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render SalesOrderLine
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Sales Order Line");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltSalesOrderLine.RenderControl(output);
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
					dojoSeminarRegistrationID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoSeminarRegistrationID;
			return myState;
		}
	}
}
