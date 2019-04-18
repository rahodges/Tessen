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
	[ToolboxData("<{0}:DojoSeminarRegistrationEditor runat=server></{0}:DojoSeminarRegistrationEditor>")]
	public class DojoSeminarRegistrationEditor : TableWindow, INamingContainer
	{
		private int dojoSeminarRegistrationID;
		private DojoSeminarRegistration obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for General Folder

		private TextBox tbStatus = new TextBox();
		private DateEditor deRegistrationDate = new DateEditor();
		private TextBox tbClassUnits = new TextBox();
		private RegularExpressionValidator revClassUnits = new RegularExpressionValidator();
		private Literal ltParentSeminar = new Literal();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Payment_Details Folder

		private TextBox tbTotalFee = new TextBox();
		private DateEditor dePaymentDate = new DateEditor();
		private TextBox tbPaymentReference = new TextBox();
		private TextBox tbPaymentAmount = new TextBox();

		#endregion

		#region Private Control Fields for Contact Folder

		private MultiSelectBox msContact = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Rappahanock Folder

		private MultiSelectBox msCustomer = new MultiSelectBox();
		private MultiSelectBox msInvoiceLine = new MultiSelectBox();
		private MultiSelectBox msSalesOrderLine = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
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
				loadFlag = true;
				dojoSeminarRegistrationID = value;
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

			tbStatus.EnableViewState = false;
			Controls.Add(tbStatus);

			ltParentSeminar.EnableViewState = false;
			Controls.Add(ltParentSeminar);

			deRegistrationDate.ID = this.ID + "_RegistrationDate";
			deRegistrationDate.AutoAdjust = true;
			deRegistrationDate.EnableViewState = false;
			Controls.Add(deRegistrationDate);

			tbClassUnits.ID = this.ID + "_ClassUnits";
			tbClassUnits.EnableViewState = false;
			Controls.Add(tbClassUnits);
			revClassUnits.ControlToValidate = tbClassUnits.ID;
			revClassUnits.ValidationExpression = "^(\\+|-)?\\d+$";
			revClassUnits.ErrorMessage = "*";
			revClassUnits.Display = ValidatorDisplay.Dynamic;
			revClassUnits.EnableViewState = false;
			Controls.Add(revClassUnits);

			#endregion

			#region Child Controls for Payment Details Folder

			tbTotalFee.EnableViewState = false;
			Controls.Add(tbTotalFee);

			dePaymentDate.ID = this.ID + "_PaymentDate";
			dePaymentDate.AutoAdjust = true;
			dePaymentDate.EnableViewState = false;
			Controls.Add(dePaymentDate);

			tbPaymentReference.EnableViewState = false;
			Controls.Add(tbPaymentReference);

			tbPaymentAmount.EnableViewState = false;
			Controls.Add(tbPaymentAmount);

			#endregion

			#region Child Controls for Contact Folder

			msContact.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msContact);

			#endregion

			#region Child Controls for Rappahanock Folder

			msCustomer.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msCustomer);

			msInvoiceLine.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msInvoiceLine);

			msSalesOrderLine.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msSalesOrderLine);

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

			#endregion

			#region Bind Contact Child Data

			msContact.Items.Add(new ListItem("Null", "Null"));
			GreyFoxContactManager contactManager = new GreyFoxContactManager("kitTessen_SeminarRegistrations_Contacts");
			GreyFoxContactCollection contactCollection = contactManager.GetCollection(string.Empty, string.Empty);
			foreach(GreyFoxContact contact in contactCollection)
			{
				ListItem i = new ListItem(contact.ToString(), contact.ID.ToString());
				msContact.Items.Add(i);
			}

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

			msInvoiceLine.Items.Add(new ListItem("Null", "Null"));
			RHInvoiceLineManager invoiceLineManager = new RHInvoiceLineManager();
			RHInvoiceLineCollection invoiceLineCollection = invoiceLineManager.GetCollection(string.Empty, string.Empty, null);
			foreach(RHInvoiceLine invoiceLine in invoiceLineCollection)
			{
				ListItem i = new ListItem(invoiceLine.ToString(), invoiceLine.ID.ToString());
				msInvoiceLine.Items.Add(i);
			}

			msSalesOrderLine.Items.Add(new ListItem("Null", "Null"));
			RHSalesOrderLineManager salesOrderLineManager = new RHSalesOrderLineManager();
			RHSalesOrderLineCollection salesOrderLineCollection = salesOrderLineManager.GetCollection(string.Empty, string.Empty, null);
			foreach(RHSalesOrderLine salesOrderLine in salesOrderLineCollection)
			{
				ListItem i = new ListItem(salesOrderLine.ToString(), salesOrderLine.ID.ToString());
				msSalesOrderLine.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoSeminarRegistrationID == 0)
				obj = new DojoSeminarRegistration();
			else
				obj = new DojoSeminarRegistration(dojoSeminarRegistrationID);

			obj.Status = byte.Parse(tbStatus.Text);
			obj.RegistrationDate = deRegistrationDate.Date;
			obj.ClassUnits = int.Parse(tbClassUnits.Text);
			obj.TotalFee = decimal.Parse(tbTotalFee.Text);
			obj.PaymentDate = dePaymentDate.Date;
			obj.PaymentReference = tbPaymentReference.Text;
			obj.PaymentAmount = decimal.Parse(tbPaymentAmount.Text);

			if(msContact.SelectedItem != null && msContact.SelectedItem.Value != "Null")
				obj.Contact = GreyFoxContact.NewPlaceHolder("kitTessen_SeminarRegistrations_Contacts", 
					int.Parse(msContact.SelectedItem.Value));
			else
				obj.Contact = null;

			if(msCustomer.SelectedItem != null && msCustomer.SelectedItem.Value != "Null")
				obj.Customer = RHCustomer.NewPlaceHolder(
					int.Parse(msCustomer.SelectedItem.Value));
			else
				obj.Customer = null;

			if(msInvoiceLine.SelectedItem != null && msInvoiceLine.SelectedItem.Value != "Null")
				obj.InvoiceLine = RHInvoiceLine.NewPlaceHolder(
					int.Parse(msInvoiceLine.SelectedItem.Value));
			else
				obj.InvoiceLine = null;

			if(msSalesOrderLine.SelectedItem != null && msSalesOrderLine.SelectedItem.Value != "Null")
				obj.SalesOrderLine = RHSalesOrderLine.NewPlaceHolder(
					int.Parse(msSalesOrderLine.SelectedItem.Value));
			else
				obj.SalesOrderLine = null;

			if(editOnAdd)
				dojoSeminarRegistrationID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbStatus.Text = string.Empty;
				deRegistrationDate.Date = DateTime.Now;
				tbClassUnits.Text = string.Empty;
				tbTotalFee.Text = string.Empty;
				dePaymentDate.Date = DateTime.Now;
				tbPaymentReference.Text = string.Empty;
				tbPaymentAmount.Text = string.Empty;
				msContact.SelectedIndex = 0;
				msCustomer.SelectedIndex = 0;
				msInvoiceLine.SelectedIndex = 0;
				msSalesOrderLine.SelectedIndex = 0;
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

			Tab Payment_DetailsTab = new Tab("Payment Details");
			Payment_DetailsTab.RenderDiv += new TabRenderHandler(renderPayment_DetailsFolder);
			tabStrip.Tabs.Add(Payment_DetailsTab);

			Tab ContactTab = new Tab("Contact");
			ContactTab.RenderDiv += new TabRenderHandler(renderContactFolder);
			tabStrip.Tabs.Add(ContactTab);

			Tab RappahanockTab = new Tab("Rappahanock");
			RappahanockTab.RenderDiv += new TabRenderHandler(renderRappahanockFolder);
			tabStrip.Tabs.Add(RappahanockTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoSeminarRegistrationID > 0)
				{
					obj = new DojoSeminarRegistration(dojoSeminarRegistrationID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoSeminarRegistrationID <= 0)
				{
					obj = new DojoSeminarRegistration();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbStatus.Text = obj.Status.ToString();
				deRegistrationDate.Date = obj.RegistrationDate;
				tbClassUnits.Text = obj.ClassUnits.ToString();
				tbTotalFee.Text = obj.TotalFee.ToString();
				dePaymentDate.Date = obj.PaymentDate;
				tbPaymentReference.Text = obj.PaymentReference;
				tbPaymentAmount.Text = obj.PaymentAmount.ToString();

				//
				// Set Children Selections
				//
				if(obj.ParentSeminar != null)
					ltParentSeminar.Text = obj.ParentSeminar.ToString();
				else
					ltParentSeminar.Text = string.Empty;
				if(obj.Contact != null)
					foreach(ListItem item in msContact.Items)
						item.Selected = obj.Contact.ID.ToString() == item.Value;
					else
						msContact.SelectedIndex = 0;

				if(obj.Customer != null)
					foreach(ListItem item in msCustomer.Items)
						item.Selected = obj.Customer.ID.ToString() == item.Value;
					else
						msCustomer.SelectedIndex = 0;

				if(obj.InvoiceLine != null)
					foreach(ListItem item in msInvoiceLine.Items)
						item.Selected = obj.InvoiceLine.ID.ToString() == item.Value;
					else
						msInvoiceLine.SelectedIndex = 0;

				if(obj.SalesOrderLine != null)
					foreach(ListItem item in msSalesOrderLine.Items)
						item.Selected = obj.SalesOrderLine.ID.ToString() == item.Value;
					else
						msSalesOrderLine.SelectedIndex = 0;

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
			// Render Status
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Registration status");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentSeminar
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentSeminar");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
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
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("RegistrationDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			deRegistrationDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ClassUnits
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class units");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbClassUnits.RenderControl(output);
			revClassUnits.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		private void renderPayment_DetailsFolder(HtmlTextWriter output)
		{
			//
			// Render TotalFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Total registration fee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbTotalFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PaymentDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Date of payment");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			dePaymentDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PaymentReference
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PaymentReference");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbPaymentReference.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PaymentAmount
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Amount paid");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbPaymentAmount.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderContactFolder(HtmlTextWriter output)
		{
			//
			// Render Contact
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Contact");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msContact.RenderControl(output);
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
			output.Write("Registrant");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msCustomer.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render InvoiceLine
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Invoice Line");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msInvoiceLine.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render SalesOrderLine
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Sales Order Line");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msSalesOrderLine.RenderControl(output);
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

