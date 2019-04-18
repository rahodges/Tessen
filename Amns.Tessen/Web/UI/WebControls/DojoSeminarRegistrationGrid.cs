using System;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.Tessen.Utilities;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// A custom grid for DojoSeminarRegistration.
	/// </summary>
	[DefaultProperty("ConnectionString"),
	ToolboxData("<{0}:DojoSeminarRegistrationGrid runat=server></{0}:DojoSeminarRegistrationGrid>")]
	public class DojoSeminarRegistrationGrid : TableGrid
	{
		bool registrationsLoaded = false;
		string connectionString;
		int defaultSeminarID = -1;
		
		CheckBox cbIsPaid;
		DropDownList ddParentSeminar;
		DropDownList ddView;
		Button btExport;

		DojoSeminarRegistrationCollection registrations;

		#region Public Properties

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

		[Bindable(false), Category("Data"), DefaultValue(-1)]
		public int DefaultSeminarID
		{
			get { return defaultSeminarID; }
			set { defaultSeminarID = value; }
		}

		public int SelectedSeminarID
		{
			get { return int.Parse(ddParentSeminar.SelectedItem.Value); }
		}

		#endregion

		public DojoSeminarRegistrationGrid() : base()
		{
			this.ProcessViewPanePostback();
			this.features |= TableWindowFeatures.ClientSideSelector;
			this.components |= TableWindowComponents.ViewPane |
				TableWindowComponents.Footer;
			this.ContentWidth = Unit.Pixel(375);
			this.headerLockEnabled = true;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit (e);

			bool adminMode = Page.User.IsInRole("Tessen/Administrator");
			this.deleteButton.Enabled = adminMode;
			this.editButton.Enabled = adminMode;
			this.newButton.Enabled = adminMode;
		}

		protected override void CreateChildControls()
		{
			if(connectionString == string.Empty)
				throw(new Exception("Empty connection string."));

			ddParentSeminar = new DropDownList();
			ddParentSeminar.AutoPostBack = true;
			Controls.Add(ddParentSeminar);

			ddView = new DropDownList();
			ddView.Items.Add(new ListItem("Default"));
			ddView.Items.Add(new ListItem("Address"));
			ddView.AutoPostBack = true;
			Controls.Add(ddView);
            
			cbIsPaid = new CheckBox();
			if(!Page.IsPostBack) cbIsPaid.Checked = true;		// Be sure to check this box by default.
			cbIsPaid.AutoPostBack = true;
			Controls.Add(cbIsPaid);

			btExport = new Button();
			btExport.Text = "Export";
			btExport.Click += new System.EventHandler(this.btExport_Click);
			Controls.Add(btExport);

            ComponentArt.Web.UI.ToolBar searchToolBar = ToolBarUtility.DefaultToolBar("Search");
            ToolBarUtility.AddControlItem(searchToolBar, cbIsPaid);
            ToolBarUtility.AddControlItem(searchToolBar, ddParentSeminar);
            ToolBarUtility.AddControlItem(searchToolBar, ddView);
            ToolBarUtility.AddControlItem(searchToolBar, btExport);
            toolbars.Add(searchToolBar);

			ChildControlsCreated = true;

			bindDropDownLists();
		}

		private void bindDropDownLists()
		{
			ddParentSeminar.Items.Clear();
			DojoSeminarManager m = new DojoSeminarManager();
			DojoSeminarCollection seminars = m.GetCollection(string.Empty, "Name", DojoSeminarFlags.Location);
			foreach(DojoSeminar seminar in seminars)
			{
				ListItem i = new ListItem(seminar.Name, seminar.ID.ToString());				
				ddParentSeminar.Items.Add(i);
			}

			// Bind the seminar ID to the drop down box and clear the value
			if(defaultSeminarID != -1)
			{
				foreach(ListItem i in ddParentSeminar.Items)
					i.Selected = i.Value == defaultSeminarID.ToString();
				defaultSeminarID = -1;
			}
		}

		private void btExport_Click(object sender, System.EventArgs e)
		{			
			string output;
			SeminarExporter exporter;
						
			loadRegistrations();
			exporter = new SeminarExporter();
			output = exporter.ConstructThirdPartyMailingList(registrations);

			Page.Response.Clear();
			Page.Response.ContentType = "text/plain";
			Page.Response.AddHeader("Content-Disposition", 
				"attachment;filename=" + ddParentSeminar.SelectedItem.Text + "_" + DateTime.Now.ToString("MM-dd-yy") + ".csv");
			Page.Response.Write(output);
			Page.Response.End();
		}

		private void loadRegistrations()
		{
			if(registrationsLoaded) return;

			DojoSeminarRegistrationManager m = new DojoSeminarRegistrationManager();

			if(cbIsPaid.Checked)
				registrations = 
					m.GetCollection("ParentSeminarID=" + 
                    ddParentSeminar.SelectedItem.Value + " AND " +
					"PaymentAmount > 0",
					"Contact.LastName, " +
                    "Contact.FirstName", 
					DojoSeminarRegistrationFlags.ParentSeminar, 
                    DojoSeminarRegistrationFlags.Contact);
			else if(ddParentSeminar.SelectedItem.Value != "-1")
				registrations =
                    m.GetCollection("ParentSeminarID=" + 
                    ddParentSeminar.SelectedItem.Value,
                    "Contact.LastName, " +
                    "Contact.FirstName", 
					DojoSeminarRegistrationFlags.ParentSeminar, 
                    DojoSeminarRegistrationFlags.Contact);
			else
				registrations =
					m.GetCollection(string.Empty,
                    "Contact.LastName, " +
                    "Contact.FirstName", 
					DojoSeminarRegistrationFlags.ParentSeminar, 
                    DojoSeminarRegistrationFlags.Contact);

			registrationsLoaded = true;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			EnsureChildControls();	

			loadRegistrations();
		}


		#region Rendering

		protected override void RenderContentHeader(HtmlTextWriter output)
		{
			switch(ddView.SelectedItem.Value)
			{
				case "Default":
					this.RenderRow(this.HeaderRowCssClass,
						"Name", "Date", "Days", "Fee", "Paid");
					break;
				case "Address":
					this.RenderRow(this.HeaderRowCssClass,
						"Name", "Address1", "Address2", "City", "State", 
						"Postal Code", "Country", "Home Phone", "Email");
					break;
			}
		}


		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			switch(ddView.SelectedItem.Value)
			{
				case "Default":
					renderDefaultGrid(output);
					break;
				case "Address":
					renderAddressGrid(output);
					break;
			}
		}

		#region Default Grid

		protected void renderDefaultGrid(HtmlTextWriter output)
		{
			bool rowflag = false;
			string rowCssClass;

			//
			// Render Records
			//
			foreach(DojoSeminarRegistration dojoSeminarRegistration in registrations)
			{
				if(rowflag)			
					rowCssClass = this.defaultRowCssClass;
				else						
					rowCssClass = this.alternateRowCssClass;
				rowflag = !rowflag;

				output.WriteBeginTag("tr");
				output.WriteAttribute("i", dojoSeminarRegistration.ID.ToString());
				output.Write(HtmlTextWriter.TagRightChar);
				output.WriteLine();
				output.Indent++;
		
				//
				// Render Main Representation of Record
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(dojoSeminarRegistration.Contact.ConstructName("L, F Mi."));
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render ID of Record
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(dojoSeminarRegistration.RegistrationDate);
				output.WriteEndTag("td");
				output.WriteLine();
				
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(dojoSeminarRegistration.ClassUnits);
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render ID of Record
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(dojoSeminarRegistration.TotalFee.ToString("c"));
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render ID of Record
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(dojoSeminarRegistration.PaymentAmount.ToString("c"));
				output.WriteEndTag("td");
				output.WriteLine();

//				output.WriteBeginTag("td");
//				output.WriteAttribute("class", rowCssClass);
//				output.Write(HtmlTextWriter.TagRightChar);
//				if(dojoSeminarRegistration.PaymentReference == string.Empty)
//					output.Write("&nbsp;");
//				else
//					output.Write(dojoSeminarRegistration.PaymentReference);
//				output.WriteEndTag("td");
//				output.WriteLine();

				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

		#region Address Grid

		protected void renderAddressGrid(HtmlTextWriter output)
		{
			bool rowflag = false;
			string rowCssClass;

			//
			// Render Records
			//
			foreach(DojoSeminarRegistration dojoSeminarRegistration in registrations)
			{
				if(rowflag)			
					rowCssClass = this.defaultRowCssClass;
				else						
					rowCssClass = this.alternateRowCssClass;
				rowflag = !rowflag;

				output.WriteBeginTag("tr");
				output.WriteAttribute("i", dojoSeminarRegistration.ID.ToString());
				output.Write(HtmlTextWriter.TagRightChar);
				output.WriteLine();
				output.Indent++;

				//
				// Render Main Representation of Record
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(dojoSeminarRegistration.Contact.ConstructName("L, F Mi."));
				output.WriteEndTag("td");
				output.WriteLine();

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(dojoSeminarRegistration.Contact.Address1.Length > 0)
					output.Write(dojoSeminarRegistration.Contact.Address1);
				else
					output.Write("&nbsp;");
				output.WriteEndTag("td");
				output.WriteLine();

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(dojoSeminarRegistration.Contact.Address2.Length > 0)
					output.Write(dojoSeminarRegistration.Contact.Address2);
				else
					output.Write("&nbsp;");
				output.WriteEndTag("td");
				output.WriteLine();

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(dojoSeminarRegistration.Contact.City.Length > 0)
					output.Write(dojoSeminarRegistration.Contact.City);
				else 
					output.Write("&nbsp;");
				output.WriteEndTag("td");
				output.WriteLine();

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(dojoSeminarRegistration.Contact.StateProvince.Length > 0)
					output.Write(dojoSeminarRegistration.Contact.StateProvince);
				else
					output.Write("&nbsp;");
				output.WriteEndTag("td");
				output.WriteLine();

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(dojoSeminarRegistration.Contact.PostalCode.Length > 0)
					output.Write(dojoSeminarRegistration.Contact.PostalCode);
				else
					output.Write("&nbsp;");
				output.WriteEndTag("td");
				output.WriteLine();

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(dojoSeminarRegistration.Contact.Country.Length > 0)
					output.Write(dojoSeminarRegistration.Contact.Country);
				else
					output.Write("&nbsp;");
				output.WriteEndTag("td");
				output.WriteLine();

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(dojoSeminarRegistration.Contact.HomePhone != "")
					output.Write(dojoSeminarRegistration.Contact.HomePhone);
				else
					output.Write("&nbsp;");
				output.WriteEndTag("td");
				output.WriteLine();

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(dojoSeminarRegistration.Contact.Email1 != "")
					output.Write(dojoSeminarRegistration.Contact.Email1);
				else
					output.Write("&nbsp;");
				output.WriteEndTag("td");
				output.WriteLine();
				
				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

		#region Footer

		protected override void RenderFooter(HtmlTextWriter output)
		{
			decimal totalFees = decimal.Zero;
			decimal totalFeesCollected = decimal.Zero;
			int totalRegistrations = 0;
			int totalPaidRegistrations = 0;

			// Calculate Total Fee
			foreach(DojoSeminarRegistration registration in registrations)
			{
				totalFees += registration.TotalFee;
				totalFeesCollected += registration.PaymentAmount;
				totalRegistrations++;
				if(registration.PaymentAmount >= registration.TotalFee)
					totalPaidRegistrations++;
			}


			output.WriteBeginTag("tr");
			output.WriteLine(HtmlTextWriter.TagRightChar);
			output.Indent++;

			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>{0}</strong> paid registrations of <strong>{1}</strong> registrations | <strong>{2:c}</strong> collected of <strong>{3:c}</strong>",
				totalPaidRegistrations, totalRegistrations, totalFeesCollected, totalFees);
			output.WriteEndTag("td");
			output.WriteLine();

			output.Indent--;
			output.WriteEndTag("tr");
			output.WriteLine();
		}

		#endregion

		#endregion

		#region ViewPane

		protected override void RenderViewPane(HtmlTextWriter output)
		{
			RenderTableBeginTag("_viewPanel", this.CellPadding, this.CellSpacing, Unit.Percentage(100), Unit.Empty, this.CssClass);
           
			DojoSeminarRegistration m = new DojoSeminarRegistration(int.Parse(Page.Request.QueryString[0]));

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("th");
			output.WriteAttribute("class", this.HeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(m.Contact.FullName);
			output.WriteEndTag("th");
			output.WriteEndTag("tr");

			#region Contact Information

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Contact Information");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.defaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(m.Contact.ConstructAddress("<br />"));
			output.Write("<br />");
			if(m.Contact.HomePhone != string.Empty)
				output.Write(m.Contact.HomePhone + " (h)<br />");
			if(m.Contact.WorkPhone != string.Empty)
				output.Write(m.Contact.WorkPhone + " (w)<br />");
			if(m.Contact.Email1 != string.Empty)
			{
				output.Write("<a href=\"mailto:");
				output.Write(m.Contact.Email1);
				output.Write("\">");
				output.Write(m.Contact.Email1);
				output.Write("</a>");
				output.Write("<br />");
			}
			if(m.Contact.ValidationMemo != null && m.Contact.ValidationMemo.Length > 0)
			{
				output.Write("<br />");
				output.Write("<strong>Validation Memo : </strong><br />");
				output.Write(m.Contact.ValidationMemo.Replace("\n", "<br />"));
			}
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			#endregion

			#region Registration

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Payment");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			if(m.PaymentAmount == decimal.Zero)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", this.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("<strong>Payment Amount</strong> : Unpaid");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}
			else
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", this.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("<strong>Payment Amount</strong> : ");
				output.Write(m.PaymentAmount.ToString("c"));
				output.WriteEndTag("td");
				output.WriteEndTag("tr");

				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", this.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("<strong>Payment Date</strong> : ");
				output.Write(m.PaymentDate.ToLongDateString());
				output.WriteEndTag("td");
				output.WriteEndTag("tr");

				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", this.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("<strong>Payment Reference</strong> : ");
				output.Write(m.PaymentReference);
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}

			#endregion

			#region Options

			DojoSeminarRegistrationOptionCollection options =
				new DojoSeminarRegistrationOptionManager().GetCollection("ParentRegistrationID=" +
				m.ID.ToString(), string.Empty, null);

			if(options.Count > 0)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", this.SubHeaderCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("Options");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");

				foreach(DojoSeminarRegistrationOption option in options)
				{
					output.WriteFullBeginTag("tr");
					output.WriteBeginTag("td");
					output.WriteAttribute("class", this.DefaultRowCssClass);
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write(option.ParentOption.Name);
					output.Write(" - ");
					output.Write(option.Quantity.ToString());
					output.WriteEndTag("td");
					output.WriteEndTag("tr");
				}

			}

			#endregion

			output.WriteEndTag("table");
		}

		#endregion

		protected override void LoadViewState(object savedState)
		{
			if (savedState != null) 
			{
				object[] myState = (object[])savedState;

				if (myState[0] != null) base.LoadViewState(myState[0]);
//				if (myState[1] != null) defaultSeminarID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			
			object[] myState = new object[1];
			myState[0] = baseState;
//			myState[1] = defaultSeminarID;

			return myState;
		}


	}
}
