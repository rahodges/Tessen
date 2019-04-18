using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.Tessen.Utilities;
using Amns.Rappahanock;
using Amns.Rappahanock.Utilities;
using Amns.Rappahanock.Web.Utilities;

namespace Amns.Tessen.Web.UI.WebControls
{	
	/// <summary>
	/// Default web editor for DojoSeminarRegistration.
	/// </summary>
	[DefaultProperty("ConnectionString"), 
	ToolboxData("<{0}:SeminarRegistrationControl runat=server></{0}:SeminarRegistrationControl>")]
	public class SeminarRegistrationControl : TableWindow, INamingContainer
	{
		public enum RegistrationPaymentMode { None, Rappahanock } // PayflowLink, PayflowPro};
		public enum Tab {Create, Verify, Payment, Disabled, InvalidSeminar, Locked};
        
		DojoSeminarRegistration registration;
		DojoSeminarRegistrationOptionCollection registrationOptions;
		DojoSeminar seminar;
		Tab selectedTab = Tab.Create;
		
		int seminarID;
		int registrationID;

		TextBox tbName;
		RequiredFieldValidator fvName;
		TextBox tbAddress1;
		TextBox tbAddress2;
		TextBox tbCity;
		TextBox tbStateProvince;
		TextBox tbPostalCode;
		TextBox tbCountry;
		TextBox tbHomePhone;
		TextBox tbWorkPhone;
		TextBox tbEmail1;
		RequiredFieldValidator fvEmail1;
		RegularExpressionValidator evEmail1;

		DropDownList ddClassUnits;
        
		Button btOk ;
		Button btBack;
		Button btCancel;

        RegistrationPaymentMode paymentMode = RegistrationPaymentMode.Rappahanock;		

		string message = string.Empty;
        		
		string rappahanockCartUrl = "~/Cart.aspx";
        
		#region Public Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int SeminarID
		{
			get { return seminarID; }
			set { seminarID = value; }
		}

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int RegistrationID
		{
			get { return registrationID; }
			set { registrationID = value; }
		}

		#endregion

		#region Payment Properties

		[Bindable(true), Category("Payments"), DefaultValue(0)]
		public RegistrationPaymentMode PaymentMode
		{
			get { return paymentMode; }
			set { paymentMode = value; }
		}

        ///// <summary>
        ///// The message displayed when the charge has been posted through the Silent Post feature
        ///// and updated in the database.
        ///// </summary>
        //[Bindable(true), Category("Payflow"), DefaultValue("Payment has been received.")] 
        //public string PayflowSuccessMessage
        //{
        //    get { return payflowSuccessMessage; }
        //    set { payflowSuccessMessage = value; }
        //}		
		
        ///// <summary>
        ///// The message displayed when the charge has not posted correctly through the Silent Post
        ///// feature or if another unexpected error has occured.
        ///// </summary>
        //[Bindable(true), Category("Payflow"), DefaultValue("There was an error processing payment.<br><br>{0} - {1}")] 
        //public string PayflowErrorMessage
        //{
        //    get { return payflowErrorMessage; }
        //    set { payflowErrorMessage = value; }
        //}

        //[Bindable(true), Category("Payflow"), DefaultValue("")]
        //public string PayflowLogin
        //{
        //    get { return payflowLogin; }
        //    set { payflowLogin = value; }
        //}

        //[Bindable(true), Category("Payflow"), DefaultValue("")]
        //public string PayflowUser
        //{
        //    get { return payflowUser; }
        //    set { payflowUser = value; }
        //}

        //[Bindable(true), Category("Payflow"), DefaultValue("")]
        //public string PayflowPassword
        //{
        //    get { return payflowPassword; }
        //    set { payflowPassword = value; }
        //}
		
        //[Bindable(true), Category("Payflow"), DefaultValue("Verisign")]
        //public string PayflowPartner
        //{
        //    get { return payflowPartner; }
        //    set { payflowPartner = value; }
        //}

        //[Bindable(true), Category("Payflow"), DefaultValue("payflow.verisign.com")]
        //public string PayflowHost
        //{
        //    get { return payflowHost; }
        //    set { payflowHost = value; }
        //}
		
        //[Bindable(true), Category("Payflow"), DefaultValue('S')]
        //public char PayflowTransactionTypeCode
        //{
        //    get { return payflowTransactionTypeCode; }
        //    set { payflowTransactionTypeCode = value; }
        //}

        //[Bindable(true), Category("Payflow"), DefaultValue("https://payflowlink.paypal.com")]
        //public string PayflowLinkUrl
        //{
        //    get { return payflowLinkUrl; }
        //    set { payflowLinkUrl = value; }
        //}

		#endregion

		#region Rappahanock Properties

		[Bindable(true), Category("Rappahanock"), DefaultValue("~/Cart.aspx")]
		public string RappahanockCartUrl
		{
			get { return rappahanockCartUrl; }
			set { rappahanockCartUrl = value; }
		}

		#endregion

		#region CreateChildControls

		protected override void CreateChildControls()
		{
			#region Registrant Controls

            tbName = new TextBox();
            tbName.ID = "Name";
            tbName.MaxLength = 255;
            tbName.Width = Unit.Pixel(200);
            tbName.EnableViewState = false;
            Controls.Add(tbName);

            fvName = new RequiredFieldValidator();
            fvName.Text = "*";
            fvName.ErrorMessage = "*";
            fvName.EnableViewState = false;
            fvName.ControlToValidate = tbName.ID;
            fvName.EnableClientScript = true;
            fvName.Enabled = false;
            Controls.Add(fvName);

            tbAddress1 = new TextBox();
            tbAddress1.MaxLength = 75;
            tbAddress1.Width = Unit.Pixel(175);
            tbAddress1.EnableViewState = false;
            Controls.Add(tbAddress1);

            tbAddress2 = new TextBox();
            tbAddress2.MaxLength = 75;
            tbAddress2.Width = Unit.Pixel(175);
            tbAddress2.EnableViewState = false;
            Controls.Add(tbAddress2);

            tbCity = new TextBox();
            tbCity.MaxLength = 75;
            tbCity.Width = Unit.Pixel(175);
            tbCity.EnableViewState = false;
            Controls.Add(tbCity);

            tbStateProvince = new TextBox();
            tbStateProvince.MaxLength = 75;
            tbStateProvince.Width = Unit.Pixel(175);
            tbStateProvince.EnableViewState = false;
            Controls.Add(tbStateProvince);

            tbPostalCode = new TextBox();
            tbPostalCode.MaxLength = 15;
            tbPostalCode.Width = Unit.Pixel(175);
            tbPostalCode.EnableViewState = false;
            Controls.Add(tbPostalCode);

            tbCountry = new TextBox();
            tbCountry.MaxLength = 25;
            tbCountry.Width = Unit.Pixel(175);
            tbCountry.EnableViewState = false;
            Controls.Add(tbCountry);

            tbHomePhone = new TextBox();
            tbHomePhone.MaxLength = 25;
            tbHomePhone.Width = Unit.Pixel(175);
            tbHomePhone.EnableViewState = false;
            Controls.Add(tbHomePhone);

            tbWorkPhone = new TextBox();
            tbWorkPhone.MaxLength = 25;
            tbWorkPhone.Width = Unit.Pixel(175);
            tbWorkPhone.EnableViewState = false;
            Controls.Add(tbWorkPhone);

            tbEmail1 = new TextBox();
            tbEmail1.ID = "Email1";
            tbEmail1.MaxLength = 155;
            tbEmail1.Width = Unit.Pixel(175);
            tbEmail1.EnableViewState = false;
            Controls.Add(tbEmail1);

            fvEmail1 = new RequiredFieldValidator();
            fvEmail1.Text = "*";
            fvEmail1.ErrorMessage = "*";
            fvEmail1.ControlToValidate = tbEmail1.ID;
            fvEmail1.EnableClientScript = true;
            fvEmail1.EnableViewState = false;
            fvEmail1.Enabled = false;
            Controls.Add(fvEmail1);

            evEmail1 = new RegularExpressionValidator();
            evEmail1.ControlToValidate = tbEmail1.ID;
            evEmail1.ValidationExpression = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            evEmail1.ErrorMessage = "Invalid";
            evEmail1.EnableViewState = false;
            evEmail1.Enabled = false;
            Controls.Add(evEmail1);

            #endregion

			#region Registration Controls

			ddClassUnits = new DropDownList();
			ddClassUnits.ID = "ClassUnits";
			Controls.Add(ddClassUnits);

			#endregion

			#region Buttons

			btOk = new Button();
			btOk.Text = "OK";
			btOk.Width = Unit.Pixel(72);
			btOk.EnableViewState = false;
			btOk.CausesValidation = true;
			btOk.Click += new EventHandler(ok_Click);
			Controls.Add(btOk);

			btCancel = new Button();
			btCancel.Text = "Cancel";
			btCancel.Width = Unit.Pixel(72);
			btCancel.EnableViewState = false;
			btCancel.Click += new EventHandler(cancel_Click);
			btCancel.CausesValidation = false;
			Controls.Add(btCancel);

			btBack = new Button();
			btBack.Text = "Back";
			btBack.Width = Unit.Pixel(72);
			btBack.EnableViewState = false;
			btBack.Click += new EventHandler(back_Click);
			btBack.CausesValidation = false;
			Controls.Add(btBack);

			#endregion

			ChildControlsCreated = true;
		}

		#endregion

		#region Init

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			columnCount = 2;
			features = TableWindowFeatures.DisableContentSeparation;
		}

		#endregion

		#region Load
        
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			EnsureChildControls();

			if(seminarID == 0)
			{
                if (registrationID != 0)
                {
                    try
                    {
                        registration = new DojoSeminarRegistration(registrationID);
                        seminarID = registration.ParentSeminar.ID;
                        if (registration.SessionID != Page.Session.SessionID &
                            registration.SessionID != Page.User.Identity.Name)
                        {
                            selectedTab = Tab.Locked;
                        }
                    }
                    catch
                    {
                        selectedTab = Tab.Locked;
                    }
                }
                else
                {
                    selectedTab = Tab.InvalidSeminar;
                }
			}

			try
			{
				seminar = new DojoSeminar(seminarID);
			}
			catch
			{
				selectedTab = Tab.InvalidSeminar;
				return;
			}

			if(!seminar.RegistrationEnabled |
				DateTime.Now < seminar.RegistrationStart |
				DateTime.Now > seminar.RegistrationEnd)
			{
				selectedTab = Tab.Disabled;
				return;
			}	

			bind();		
		}

		#endregion

		#region Bind Controls

		private void bind()
		{
			Decimal fullFee;
			string fullName;

			// Detect Early Bird
			if(DateTime.Now < seminar.EarlyEndDate)
			{
				fullFee = seminar.FullEarlyRegistrationFee;
				fullName = Localization.Strings.FullEarlyRegistration;
			}
			else if(DateTime.Now < seminar.LateStartDate)
			{
				fullFee = seminar.FullRegistrationFee;
				fullName = Localization.Strings.FullRegistration;
			}
			else
			{
				fullFee = seminar.FullLateRegistrationFee;
				fullName = Localization.Strings.FullLateRegistration;
			}

			if(ddClassUnits.Items.Count == 0)
			{
				switch(seminar.ClassUnitType)
				{
					case DojoSeminarClassUnitType.Day:			// Day
						TimeSpan span = seminar.endDate.Subtract(seminar.startDate);
						ddClassUnits.Items.Add(new ListItem(
							string.Format("{0} - {1:c}", fullName, fullFee),
							span.Days.ToString()));
                        for (int x = 1; x <= span.Days - 1; x++)
                        {
                            if (x * seminar.classUnitFee >= fullFee)
                                break;
                            ddClassUnits.Items.Add(new ListItem(string.Format("{0} " +
                                (x == 1 ? Localization.Strings.DayUnit_Singular :
                                Localization.Strings.DayUnit_Plural) +
                                " - {1:c}",
                                x, x * seminar.classUnitFee), x.ToString()));
                        }
						break;
					case DojoSeminarClassUnitType.Class:			// Class
						int classCount = seminar.ClassCount;
						ddClassUnits.Items.Add(new ListItem(
							string.Format("{0} - {1:c}", fullName, fullFee),
							classCount.ToString()));
						for(int x = 1; x <= classCount - 1; x++)
						{
							// Do not add any fees greater than the full registration
							if(x * seminar.ClassUnitFee >= fullFee)
								break;
							ddClassUnits.Items.Add(new ListItem(string.Format("{0} " +
                                (x == 1 ? Localization.Strings.ClassUnit_Singular :
                                Localization.Strings.ClassUnit_Plural) + 
                                " - {1:c}",
								x, x * seminar.classUnitFee), x.ToString()));
						}
						break;
					case DojoSeminarClassUnitType.None:			// None
						ddClassUnits.Items.Add(new ListItem(
							string.Format("{1} - {0:c}", fullName, fullFee), "-1"));
						break;
				}
			}
		}

		#endregion

		#region Event Handlers

		protected void back_Click(object sender, EventArgs e)
		{
			EnsureChildControls();

			switch(selectedTab)
			{
				case Tab.Verify:
					selectedTab = Tab.Create;
					break;
				case Tab.Payment:
					selectedTab = Tab.Verify;
					break;
			}			
		}

		protected void ok_Click(object sender, EventArgs e)
		{
			EnsureChildControls();

			switch(selectedTab)
			{
				case Tab.Create:
					create();
					break;
                case Tab.Verify:
                    {
                        SeminarRegistrationBuilder builder;
                        builder = new SeminarRegistrationBuilder(registrationID);
                        builder.Registration.Status = 10;
                        builder.Registration.Save();
                        selectedTab = Tab.Payment;
                        break;
                    }
                case Tab.Payment:
                    {
                        SeminarRegistrationBuilder builder;
                        builder = new SeminarRegistrationBuilder(registrationID);
                        Page.Response.Redirect(RappahanockCartUrl, true);
                        break;
                    }
                case Tab.Locked:
                    {
                        Page.Response.Redirect("~/");
                        break;
                    }
			}
		}

        protected void cancel_Click(object sender, EventArgs e)
        {
            // Delete registration
            if (registrationID != 0)
            {
                SeminarRegistrationBuilder b =
                    new SeminarRegistrationBuilder(registrationID);
                b.RemoveFromCart();
                b.Delete();
            }

            this.OnCancelled(EventArgs.Empty);
        }

        public event EventHandler Cancelled;
        protected virtual void OnCancelled(EventArgs e)
        {
            if (Cancelled != null)
                Cancelled(this, e);
        }

        public event EventHandler Updated;
        protected virtual void OnUpdated(EventArgs e)
        {
            if (Updated != null)
                Updated(this, e);
        }

		#endregion

		#region Validator Control

		private void setValidators(Tab tab)
		{
			switch(tab)
			{
				case Tab.Create:
					fvName.Enabled = true;
					fvEmail1.Enabled = true;
					evEmail1.Enabled = true;
					break;
			}
		}

		#endregion

		#region Create Registration

		private void create()
		{
            setValidators(Tab.Create);

			Page.Validate();
			if(!Page.IsValid) 
                return;

			// Create a new registration if it does not already exist. Be sure to create
			// the registration in proper order; contact, registration, options. If
			// Rappahanock is enabled, create the sales order for the item.

            SeminarRegistrationBuilder builder = null;

			if(registrationID == 0)
			{
                builder = new SeminarRegistrationBuilder();
                registration = builder.Registration;
                registrationOptions = builder.Options;
				registration.Contact = 
                    new GreyFoxContact(DojoSeminarRegistrationManager.ContactTable);

                // Set a SessionID for security using either the username
                // or ASP.net SessionID
                if (Page.User.Identity.Name != string.Empty)
                    registration.SessionID = Page.User.Identity.Name;
                else
                    registration.SessionID = Page.Session.SessionID;
			}
			else
			{
                // Be sure to remove the registration from the cart if
                // Rappahanock processing is activated.
                try
                {
                    builder = new SeminarRegistrationBuilder(registrationID);
                }
                catch
                {
                    selectedTab = Tab.Locked;
                    return;
                }

                registration = builder.Registration;
                registrationOptions = builder.Options;

                // Check for SessionID for Security
                if (registration.SessionID != Page.Session.SessionID &
                    registration.SessionID != Page.User.Identity.Name)
                {
                    selectedTab = Tab.Locked;
                    return;
                }

                // If the registration has a payment amount over zero,
                // be sure to lock it to remove a vulnerability.
                if (registration.PaymentAmount > 0)
                {
                    selectedTab = Tab.Locked;
                    return;
                }

                if (paymentMode == RegistrationPaymentMode.Rappahanock)
                    builder.RemoveFromCart();
			}

			registration.RegistrationDate = DateTime.Now;
			registration.Status = 0;
			registration.Contact.ParseName(tbName.Text);
			registration.Contact.ParseAddress(tbAddress1.Text, tbAddress2.Text, tbCity.Text,
				tbStateProvince.Text, tbPostalCode.Text, tbCountry.Text);
			registration.Contact.ParsePhones(tbHomePhone.Text, tbWorkPhone.Text, string.Empty,
				string.Empty, string.Empty);
			registration.Contact.Email1 = tbEmail1.Text;
			registration.Contact.Save();
			registration.ParentSeminar = seminar;	
			registration.classUnits = int.Parse(ddClassUnits.SelectedItem.Value);
			registrationID = registration.Save();

			// Set options for the registration by first clearing the options
			// already associated with the registration then recreating them
			// from current selections.            
			DojoSeminarRegistrationOptionManager.ClearOptions(registration);
            registrationOptions.Clear();

			// Loop through the form's post back data looking for the lineOption
			// and quantity keys. Setup the associated options with the
			// current registration only if selected or the quantity is not
			// zero.
			string[] allKeys = Context.Request.Form.AllKeys;
			int optionId;
			int quantity = 0;
			for(int x = 0; x < allKeys.Length; x++)
			{
				if(!allKeys[x].StartsWith(ClientID + "___option") &
					!allKeys[x].StartsWith(ClientID + "___qty"))
					continue;
								
				if(allKeys[x].StartsWith(ClientID + "___option"))
				{
					// Parse OptionID Selected
					optionId = int.Parse(allKeys[x].Substring(
						ClientID.Length + 9, allKeys[x].Length - (ClientID.Length + 9)));
					quantity = 1;						
				}
				else
				{
					// Parse OptionID and Quantity Selected and be sure
					// to ignore quantities less than 1, otherwise some
					// clever registrants will get some discounts!
					optionId = int.Parse(allKeys[x].Substring(
						ClientID.Length + 6, allKeys[x].Length - (ClientID.Length + 6)));
					quantity = 
							int.Parse(Context.Request.Form[ClientID + "___qty" + optionId.ToString()]);

					if(quantity < 1)
						continue;
				}

				// Find the lineOption in the seminar's list of available options then
				// add the lineOption and save it.
				foreach(DojoSeminarOption seminarOption in seminar.Options)
				{
					if(optionId == seminarOption.iD)
					{						
						DojoSeminarRegistrationOption registrationOption = 
							new DojoSeminarRegistrationOption();
						registrationOption.ParentRegistration = registration;
						registrationOption.ParentOption = seminarOption;
						registrationOption.CostPerItem = seminarOption.Fee;
						registrationOption.Quantity = quantity;
						registrationOption.TotalFee = seminarOption.Fee * registrationOption.Quantity;						
						registrationOption.Save();
                        registrationOptions.Add(registrationOption);
						break;
					}
				}
			}

			// Save the registration again to update quantities and fees.
            registration.CalculateFee(registrationOptions);
			registration.Save();

            if (paymentMode == RegistrationPaymentMode.Rappahanock)
                builder.AddToCart();

			selectedTab = Tab.Verify;
		}

		#endregion
 
		#region PreRender

		protected override void OnPreRender(EventArgs e)
		{
			switch(selectedTab)
			{
				case Tab.Disabled:
					btOk.CausesValidation = false;
					break;
				case Tab.Create:
					if(registrationID != 0 && registration == null)
					{
						registration = new DojoSeminarRegistration(registrationID);
					}
					if(registration != null)
					{
						tbName.Text = registration.Contact.FullName;
						tbAddress1.Text = registration.Contact.Address1;
						tbAddress2.Text = registration.Contact.Address2;
						tbCity.Text = registration.Contact.City;
						tbStateProvince.Text = registration.Contact.StateProvince;
						tbPostalCode.Text = registration.Contact.PostalCode;
						tbCountry.Text = registration.Contact.Country;
						tbHomePhone.Text = registration.Contact.HomePhone;
						tbWorkPhone.Text = registration.Contact.WorkPhone;
						tbEmail1.Text = registration.Contact.Email1;
						if(registrationOptions == null)
						{
							DojoSeminarRegistrationOptionManager m = new DojoSeminarRegistrationOptionManager();
							registrationOptions = m.GetCollection("ParentRegistrationID=" + registrationID,
								string.Empty, null);
						}
						foreach(ListItem i in ddClassUnits.Items)
							i.Selected = i.Value == registration.classUnits.ToString();					
					}
					break;
				case Tab.Verify:
					btOk.CausesValidation = false;
					goto case Tab.Create;
				case Tab.Payment:
					btOk.CausesValidation = false;
					break;
                case Tab.Locked:
                    btOk.CausesValidation = false;
                    break;
			}

			setValidators(selectedTab);
		}

		#endregion

		#region Render

		protected override void RenderContent(HtmlTextWriter output)
		{
			switch(selectedTab)
			{
				case Tab.Create:
					renderCreateTab(output);
					break;
				case Tab.Verify:
					renderVerifyTab(output);
					break;
				case Tab.Payment:
					renderPaymentSelectionTab(output);
					break;
				case Tab.Disabled:
					this.renderDisabledTab(output);
					break;
				case Tab.InvalidSeminar:
					this.renderInvalidSeminarTab(output);
					break;
                case Tab.Locked:
                    this.renderLockedTab(output);
                    break;
			}
		}

		#region Create Registration Form

		private void renderCreateTab(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderCell("Seminar", "class=\"row1\"");
			RenderCell(seminar.Name, "class=\"row1\"");
			output.WriteEndTag("tr");

			//
			// Render Seminar Dates
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Dates");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("width", "100%");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(seminar.ConstructDateLongString());
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Instructions:</strong> Please enter the person you wish to register below.");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Contact
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Contact Information");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Name
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Name");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			tbName.RenderControl(output);
			output.Write(" ");
			fvName.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			RenderPropertyRows("row1", "row2",
				new string[] {"Address", "", "City", "State/Province", "PostalCode",
								 "Country", "Home", "Work"},
				new Control[] {tbAddress1, tbAddress2, tbCity, tbStateProvince, tbPostalCode,
								  tbCountry, tbHomePhone, tbWorkPhone});

			//
			// Render Email
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Email");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			tbEmail1.RenderControl(output);
			output.Write(" ");
            fvEmail1.RenderControl(output);
			evEmail1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ClassUnits
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
            switch (seminar.ClassUnitType)
            {
                case DojoSeminarClassUnitType.Day:
                    output.Write(Localization.Strings.DayUnit_Plural);
                    break;
                case DojoSeminarClassUnitType.Class:
                    output.Write(Localization.Strings.ClassUnit_Plural);
                    break;
                case DojoSeminarClassUnitType.None:
                    output.Write("None");
                    break;
            }				
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ddClassUnits.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Options
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "4");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Options");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "4");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);

			output.WriteBeginTag("table");
			output.WriteAttribute("width", "100%");
			output.Write(HtmlTextWriter.TagRightChar);
            
			bool valueSet = false;					// keeps track of value setting in input controls!
			DojoSeminarOption option;
			for(int i = 0; i < seminar.Options.Count; i++)
			{
				option = seminar.Options[i];
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", "row1");
				output.Write(HtmlTextWriter.TagRightChar);
				if(option.MaxQuantity == 1)
				{
					output.WriteBeginTag("input");
					output.WriteAttribute("type", "checkbox");
					output.WriteAttribute("name", ClientID + "___option" + option.iD.ToString());
					if(registrationOptions != null)
						foreach(DojoSeminarRegistrationOption registrationOption in registrationOptions)
							if(registrationOption.parentOption.iD == option.iD)
								output.WriteAttribute("checked", "true");
					output.Write(HtmlTextWriter.TagRightChar);
				}
				else if(option.MaxQuantity > 1)
				{
					output.WriteBeginTag("input");
					output.WriteAttribute("type", "text");
					output.WriteAttribute("name", ClientID + "___qty" + option.iD);
					output.WriteAttribute("style", "width:35px;");
					if(registrationOptions != null)
						foreach(DojoSeminarRegistrationOption registrationOption in registrationOptions)
							if(option.iD == registrationOption.parentOption.iD)
							{
								valueSet = true;
								output.WriteAttribute("value", registrationOption.Quantity.ToString());
							}
					if(!valueSet)
						output.WriteAttribute("value", "0");
					valueSet = false;
					output.Write(HtmlTextWriter.TagRightChar);
				}
				output.WriteEndTag("td");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", "row1");
				output.WriteAttribute("width", "100%");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(option.Name);
				if(option.Fee > 0)
				{
					output.Write(" (");
					output.Write(option.Fee.ToString("c"));
					if(option.maxQuantity > 1)
						output.Write(" ea.)");
					else
						output.Write(")");
				}
				output.WriteEndTag("td");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", "row1");
				output.Write(HtmlTextWriter.TagRightChar);				
				output.WriteEndTag("td");
				output.WriteEndTag("tr");

				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", "row1");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("&nbsp;");
				output.WriteEndTag("td");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", "row1");
				output.WriteAttribute("width", "100%");
				output.WriteAttribute("colspan", "2");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(option.description);
				output.WriteEndTag("td");				
				output.WriteEndTag("tr");
			}
			
			output.WriteEndTag("table");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

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
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
		}

		#endregion

		#region Verify Registration Form

		private void renderVerifyTab(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Please verify your  ");
			output.Write(seminar.Name);
			output.Write(" registration.");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Seminar Dates
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Dates");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("width", "100%");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(seminar.ConstructDateLongString());
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Contact Name
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Registrant");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("width", "100%");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(registration.Contact.FullName);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class Units
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
            switch (seminar.ClassUnitType)
            {
                case DojoSeminarClassUnitType.Day:
                    output.Write("Classes");
                    break;
                case DojoSeminarClassUnitType.Class:
                    output.Write("Days");
                    break;
                case DojoSeminarClassUnitType.None:
                    output.Write("None");
                    break;
            }	
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(ddClassUnits.SelectedItem.Text);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
          
			foreach(DojoSeminarRegistrationOption option in registrationOptions)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", "row1");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(option.ParentOption.Name);				
				output.WriteEndTag("td");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", "row1");
				output.Write(HtmlTextWriter.TagRightChar);
				if(option.ParentOption.MaxQuantity == 1)
					if(option.Quantity == 1)
						output.Write("yes");
					else
						output.Write("no");
				else
					output.Write(option.Quantity);
				output.Write(" for ");
				output.Write(option.TotalFee.ToString("c"));
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}

			//
			// Render Total Registration Fee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Registration Fee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(registration.TotalFee.ToString("c"));
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Back/OK/Cancel Buttons
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			btBack.RenderControl(output);
			output.Write("&nbsp;");
			btOk.RenderControl(output);
			output.Write("&nbsp;");
			btCancel.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
		}

		#endregion

		#region Payment Selection Form

		private void renderPaymentSelectionTab(HtmlTextWriter output)
		{
			if(PaymentMode == RegistrationPaymentMode.Rappahanock)
			{
				#region ...

				output.WriteFullBeginTag("tr");
				RenderCell("The registration has been added to your shopping " +
					"cart. Your registration will not be complete until payment " +
					"has been received.<br><br>", "left", "2");
				output.WriteEndTag("tr");
				output.WriteLine();

				output.WriteFullBeginTag("tr");
				output.WriteLine();
				output.WriteBeginTag("td");
				output.WriteAttribute("colspan", "2");
				output.WriteAttribute("align", "right");
				output.WriteAttribute("class", "row1");
				output.Write(HtmlTextWriter.TagRightChar);
				btBack.RenderControl(output);
				output.Write("&nbsp;");
				btOk.RenderControl(output);
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
				output.WriteLine();

				#endregion
			}
			else
			{
				#region ...

				output.WriteFullBeginTag("tr");
				RenderCell("You are pre-registered for this seminar. " +
					"Registration will not be considered final until payment is " +
					"received by the payment deadline. ");
				output.WriteEndTag("tr");
				output.WriteLine();

				output.WriteFullBeginTag("tr");
				output.WriteLine();
				output.WriteBeginTag("td");
				output.WriteAttribute("colspan", "2");
				output.WriteAttribute("align", "right");
				output.WriteAttribute("class", "row1");
				output.Write(HtmlTextWriter.TagRightChar);
				btBack.RenderControl(output);
				output.Write("&nbsp;");
				btOk.RenderControl(output);
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
				output.WriteLine();

				#endregion
			}
		}

		#endregion

		private void renderInvalidSeminarTab(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(Localization.Strings.SeminarDoesNotExist);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			btCancel.RenderControl(output);						
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
		}

		private void renderDisabledTab(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(string.Format(Localization.Strings.SeminarRegistrationDisabled, seminar.Name));
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			btCancel.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
		}

        private void renderLockedTab(HtmlTextWriter output)
        {
            output.WriteFullBeginTag("tr");
            output.WriteBeginTag("td");
            output.WriteAttribute("colspan", "2");
            output.Write(HtmlTextWriter.TagRightChar);
            output.Write(Localization.Strings.SeminarRegistrationLocked);
            output.WriteEndTag("td");
            output.WriteEndTag("tr");

            output.WriteFullBeginTag("tr");
            output.WriteBeginTag("td");
            output.WriteAttribute("colspan", "2");
            output.WriteAttribute("align", "right");
            output.WriteAttribute("class", "row1");
            output.Write(HtmlTextWriter.TagRightChar);
            btOk.RenderControl(output);
            output.WriteEndTag("td");
            output.WriteEndTag("tr");
        }

		#endregion

		#region ViewState Methods

		protected override void LoadViewState(object savedState)
		{
			if(savedState != null)
			{
				object[] myState = (object[]) savedState;
				if(myState[0] != null)
					base.LoadViewState(myState[0]);
				if(myState[1] != null)
					registrationID = (int) myState[1];
				if(myState[2] != null)
					seminarID = (int) myState[2];
				if(myState[3] != null)
					selectedTab = (Tab) myState[3];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[4];
			myState[0] = baseState;
			myState[1] = registrationID;
			myState[2] = seminarID;
			myState[3] = selectedTab;
			return myState;
		}

		#endregion

	}
}