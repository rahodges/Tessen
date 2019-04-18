using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.GreyFox.People;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoSeminarRegistration.
	/// </summary>
	[DefaultProperty("ConnectionString"), 
	ToolboxData("<{0}:DojoSeminarRegistrationEditor runat=server></{0}:DojoSeminarRegistrationEditor>")]
	public class DojoSeminarRegistrationEditor : TableWindow, INamingContainer
	{
		private int dojoSeminarRegistrationID;
		private int defaultDojoSeminarID = -1;
		private DojoSeminarRegistration editDojoSeminarRegistration;
		private string connectionString;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for General Folder
		
		private Literal ltContact = new Literal();
		private MultiSelectBox msStatus = new MultiSelectBox();
		private DateEditor deRegistrationDate = new DateEditor();
		private TextBox tbClassUnits = new TextBox();
		private MultiSelectBox msParentSeminar = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Payment_Details Folder

		private TextBox tbTotalFee = new TextBox();
		private DateEditor dePaymentDate = new DateEditor();
		private TextBox tbPaymentReference = new TextBox();
		private TextBox tbPaymentAmount = new TextBox();

		#endregion

		#region Private Control Fields for Contact Folder

		private TextBox tbName = new TextBox();
		private TextBox tbAddress1 = new TextBox();
		private TextBox tbAddress2 = new TextBox();
		private TextBox tbCity = new TextBox();
		private TextBox tbStateProvince = new TextBox();
		private TextBox tbPostalCode = new TextBox();
		private TextBox tbCountry = new TextBox();
		private TextBox tbHomePhone = new TextBox();
		private TextBox tbWorkPhone = new TextBox();
		private TextBox tbMobilePhone = new TextBox();
		private TextBox tbPager = new TextBox();
		private TextBox tbEmail1 = new TextBox();
		private TextBox tbEmail2 = new TextBox();
		private TextBox tbUrl = new TextBox();
		private TextBox tbMemoText = new TextBox();

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

		[Bindable(true), Category("Data"), DefaultValue(-1)]
		public int DefaultDojoSeminarID
		{
			get
			{
				return defaultDojoSeminarID;
			}
			set
			{
				defaultDojoSeminarID = value;
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

			ltContact.EnableViewState = false;
			Controls.Add(ltContact);

			msStatus.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msStatus);

			msParentSeminar.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msParentSeminar);

			deRegistrationDate.ID = this.ID + "_RegistrationDate";
			deRegistrationDate.AutoAdjust = true;
			deRegistrationDate.EnableViewState = false;
			Controls.Add(deRegistrationDate);

			tbClassUnits.EnableViewState = false;
			Controls.Add(tbClassUnits);

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

			tbName.Width = Unit.Pixel(200);
			tbName.EnableViewState = false;
			Controls.Add(tbName);

			tbAddress1.Width = Unit.Pixel(175);
			tbAddress1.EnableViewState = false;
			Controls.Add(tbAddress1);

			tbAddress2.Width = Unit.Pixel(175);
			tbAddress2.EnableViewState = false;
			Controls.Add(tbAddress2);

			tbCity.Width = Unit.Pixel(175);
			tbCity.EnableViewState = false;
			Controls.Add(tbCity);

			tbStateProvince.Width = Unit.Pixel(175);
			tbStateProvince.EnableViewState = false;
			Controls.Add(tbStateProvince);

			tbPostalCode.Width = Unit.Pixel(175);
			tbPostalCode.EnableViewState = false;
			Controls.Add(tbPostalCode);

			tbCountry.Width = Unit.Pixel(175);
			tbCountry.EnableViewState = false;
			Controls.Add(tbCountry);

			tbHomePhone.Width = Unit.Pixel(175);
			tbHomePhone.EnableViewState = false;
			Controls.Add(tbHomePhone);

			tbWorkPhone.Width = Unit.Pixel(175);
			tbWorkPhone.EnableViewState = false;
			Controls.Add(tbWorkPhone);

			tbMobilePhone.Width = Unit.Pixel(175);
			tbMobilePhone.EnableViewState = false;
			Controls.Add(tbMobilePhone);

			tbPager.Width = Unit.Pixel(175);
			tbPager.EnableViewState = false;
			Controls.Add(tbPager);

			tbEmail1.Width = Unit.Pixel(175);
			tbEmail1.EnableViewState = false;
			Controls.Add(tbEmail1);

			tbEmail2.Width = Unit.Pixel(175);
			tbEmail2.EnableViewState = false;
			Controls.Add(tbEmail2);

			tbUrl.Width = Unit.Pixel(175);
			tbUrl.EnableViewState = false;
			Controls.Add(tbUrl);

			tbMemoText.TextMode = TextBoxMode.MultiLine;
			tbMemoText.Rows = 7;
			tbMemoText.Width = Unit.Percentage(100);
			tbMemoText.EnableViewState = false;
			Controls.Add(tbMemoText);

			#endregion

			btOk.Text = "OK";
			btOk.Width = Unit.Pixel(72);
			btOk.EnableViewState = false;
			btOk.Click += new EventHandler(ok_Click);
			Controls.Add(btOk);

			btCancel.Text = "Cancel";
			btCancel.Width = Unit.Pixel(72);
			btCancel.EnableViewState = false;
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

			DojoSeminarManager parentSeminarManager = new DojoSeminarManager();
			DojoSeminarCollection parentSeminarCollection = 
				parentSeminarManager.GetCollection(string.Empty, "Name", null);

			foreach(DojoSeminar parentSeminar in parentSeminarCollection)
			{
				ListItem i = new ListItem(parentSeminar.Name, parentSeminar.ID.ToString());
				msParentSeminar.Items.Add(i);
			}

			if(msStatus.Items.Count == 0)
			{
				msStatus.Items.Add(new ListItem("Unverified", "0"));
				msStatus.Items.Add(new ListItem("Verified", "10"));
				msStatus.Items.Add(new ListItem("Paid", "15"));
			}

			#endregion

			#region Bind Contact Child Data

			#endregion
		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoSeminarRegistrationID == 0)
			{
				editDojoSeminarRegistration = new DojoSeminarRegistration();
				editDojoSeminarRegistration.Contact = new GreyFoxContact("kitTessen_SeminarRegistrations_Contacts");
			}
			else
			{
				editDojoSeminarRegistration = new DojoSeminarRegistration(dojoSeminarRegistrationID);
			}

			editDojoSeminarRegistration.Contact.ParseName(tbName.Text);
			editDojoSeminarRegistration.Contact.ParseAddress(tbAddress1.Text, tbAddress2.Text, 
				tbCity.Text, tbStateProvince.Text, tbPostalCode.Text, tbCountry.Text);
			editDojoSeminarRegistration.Contact.ParsePhones(tbHomePhone.Text, tbWorkPhone.Text,
				string.Empty, tbPager.Text, tbMobilePhone.Text);
			editDojoSeminarRegistration.Contact.Email1 = tbEmail1.Text;
			editDojoSeminarRegistration.Contact.Email2 = tbEmail2.Text;
			editDojoSeminarRegistration.Contact.Url = tbUrl.Text;
			editDojoSeminarRegistration.Contact.MemoText = tbMemoText.Text;
			editDojoSeminarRegistration.Contact.Save();

			editDojoSeminarRegistration.Status = byte.Parse(msStatus.SelectedValue);
			editDojoSeminarRegistration.RegistrationDate = deRegistrationDate.Date;
			editDojoSeminarRegistration.ClassUnits = int.Parse(tbClassUnits.Text);
			editDojoSeminarRegistration.TotalFee = decimal.Parse(tbTotalFee.Text);
			editDojoSeminarRegistration.PaymentDate = dePaymentDate.Date;
			editDojoSeminarRegistration.PaymentReference = tbPaymentReference.Text;
			editDojoSeminarRegistration.PaymentAmount = decimal.Parse(tbPaymentAmount.Text);

			if(msParentSeminar.SelectedItem != null)
				editDojoSeminarRegistration.ParentSeminar = DojoSeminar.NewPlaceHolder( 
					int.Parse(msParentSeminar.SelectedItem.Value));
			else
				editDojoSeminarRegistration.ParentSeminar = null;

			if(editOnAdd)
				dojoSeminarRegistrationID = editDojoSeminarRegistration.Save();
			else
				editDojoSeminarRegistration.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbAddress1.Text = string.Empty;
				tbAddress2.Text = string.Empty;
				tbCity.Text = string.Empty;
				tbStateProvince.Text = string.Empty;
				tbPostalCode.Text = string.Empty;
				tbCountry.Text = string.Empty;
				tbHomePhone.Text = string.Empty;
				tbWorkPhone.Text = string.Empty;
				tbMobilePhone.Text = string.Empty;
				tbPager.Text = string.Empty;
				tbEmail1.Text = string.Empty;
				tbEmail2.Text = string.Empty;
				tbUrl.Text = string.Empty;
				tbMemoText.Text = string.Empty;

				deRegistrationDate.Date = DateTime.Now;
				tbClassUnits.Text = "-1";
				tbTotalFee.Text = "0";
				dePaymentDate.Date = DateTime.Now;
				tbPaymentReference.Text = string.Empty;
				tbPaymentAmount.Text = "0";

				msParentSeminar.SelectedIndex = 0;
				msStatus.SelectedIndex = 0;
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
			GeneralTab.RenderDiv += new TabRenderHandler(renderGeneralFolder);
			GeneralTab.Visible = true;
			tabStrip.Tabs.Add(GeneralTab);

			Tab ContactTab = new Tab("Contact");
			ContactTab.RenderDiv += new TabRenderHandler(renderContactFolder);
			tabStrip.Tabs.Add(ContactTab);

			Tab Payment_DetailsTab = new Tab("Payment Details");
			Payment_DetailsTab.RenderDiv += new TabRenderHandler(renderPayment_DetailsFolder);
			tabStrip.Tabs.Add(Payment_DetailsTab);
		}

		protected override void OnPreRender(EventArgs e)
		{
			if(dojoSeminarRegistrationID != 0 & loadFlag)
			{
				editDojoSeminarRegistration = new DojoSeminarRegistration(dojoSeminarRegistrationID);

				//
				// Set Field Entries
				//
				deRegistrationDate.Date = editDojoSeminarRegistration.RegistrationDate;
				tbClassUnits.Text = editDojoSeminarRegistration.ClassUnits.ToString();
				tbTotalFee.Text = editDojoSeminarRegistration.TotalFee.ToString("N");
				dePaymentDate.Date = editDojoSeminarRegistration.PaymentDate;
				tbPaymentReference.Text = editDojoSeminarRegistration.PaymentReference;
				tbPaymentAmount.Text = editDojoSeminarRegistration.PaymentAmount.ToString("N");

				tbName.Text = editDojoSeminarRegistration.Contact.FullName;
				tbAddress1.Text = editDojoSeminarRegistration.Contact.Address1;
				tbAddress2.Text = editDojoSeminarRegistration.Contact.Address2;
				tbCity.Text = editDojoSeminarRegistration.Contact.City;
				tbStateProvince.Text = editDojoSeminarRegistration.Contact.StateProvince;
				tbPostalCode.Text = editDojoSeminarRegistration.Contact.PostalCode;
				tbCountry.Text = editDojoSeminarRegistration.Contact.Country;
				tbHomePhone.Text = editDojoSeminarRegistration.Contact.HomePhone;
				tbWorkPhone.Text = editDojoSeminarRegistration.Contact.WorkPhone;
				tbMobilePhone.Text = editDojoSeminarRegistration.Contact.MobilePhone;
				tbPager.Text = editDojoSeminarRegistration.Contact.Pager;
				tbEmail1.Text = editDojoSeminarRegistration.Contact.Email1;
				tbEmail2.Text = editDojoSeminarRegistration.Contact.Email2;
				tbUrl.Text = editDojoSeminarRegistration.Contact.Url;
				tbMemoText.Text = editDojoSeminarRegistration.Contact.MemoText;

				//
				//
				// Set Children Selections
				//
				if(editDojoSeminarRegistration.ParentSeminar != null)
					foreach(ListItem item in msParentSeminar.Items)
						item.Selected = editDojoSeminarRegistration.ParentSeminar.ID.ToString() == item.Value;

				foreach(ListItem item in msStatus.Items)
					item.Selected = editDojoSeminarRegistration.Status.ToString() == item.Value;

				if(editDojoSeminarRegistration.Contact != null)
					ltContact.Text = editDojoSeminarRegistration.Contact.ToString();
				else
					ltContact.Text = string.Empty;

				Text = "Edit  - " + editDojoSeminarRegistration.ToString();
			}
			else
			{
				if(defaultDojoSeminarID != -1)
				{
					// Render Defaults
					DojoSeminar s = new DojoSeminar(defaultDojoSeminarID);
				
					// Select Default Seminar
					foreach(ListItem i in msParentSeminar.Items)
					{
						i.Selected = defaultDojoSeminarID.ToString() == i.Value;
					}
				
					deRegistrationDate.Date = DateTime.Now;
					dePaymentDate.Date = DateTime.Now;

                    switch (s.ClassUnitType)
                    {
                        case DojoSeminarClassUnitType.Day:
                            TimeSpan days = s.EndDate.Date - s.StartDate.Date;
                            tbClassUnits.Text = days.TotalDays.ToString();
                            break;
                        case DojoSeminarClassUnitType.Class:
                            tbClassUnits.Text = "-1";
                            break;
                        default:
                            goto case DojoSeminarClassUnitType.Class;
                    }

					tbTotalFee.Text = s.FullRegistrationFee.ToString("N");;
                   
					tbPaymentAmount.Text = "0.00";
				}

				Text = "Add ";
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
			ltContact.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Status
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Status");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentSeminar
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Seminar");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msParentSeminar.RenderControl(output);
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
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

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
			output.Write("Total registration calcSeminarFee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbTotalFee.RenderControl(output);
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
		}

		private void renderContactFolder(HtmlTextWriter output)
		{
			RenderPropertyRows(string.Empty, string.Empty,
				new string[] { "Name", "Address", "", "City", "State/Province", "PostalCode",
								 "Country", "Home", "Work", "Mobile", "Pager", "Email 1", "Email 2", "Url", "Memo"},
				new Control[] {tbName, tbAddress1, tbAddress2, tbCity, tbStateProvince, tbPostalCode, 
								  tbCountry, tbHomePhone,tbWorkPhone, tbMobilePhone, tbPager, tbEmail1, tbEmail2, tbUrl, tbMemoText});

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