using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.GreyFox.Web.UI.WebControls;
using ComponentArt.Web.UI;
using Amns.Rappahanock;
using Amns.Rappahanock.Utilities;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoSeminar.
	/// </summary>
	[DefaultProperty("ConnectionString"), 
	ToolboxData("<{0}:DojoSeminarEditor runat=server></{0}:DojoSeminarEditor>")]
	public class DojoSeminarEditor : System.Web.UI.WebControls.WebControl, INamingContainer
	{
		private int dojoSeminarID;
		private DojoSeminar editDojoSeminar;
		private string connectionString;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

        protected ComponentArt.Web.UI.TabStrip tabstrip;
        protected ComponentArt.Web.UI.MultiPage multipage;
        protected Literal headerText;

		#region General

        protected ComponentArt.Web.UI.PageView generalView;
        private TextBox tbName;
        private TextBox tbDescription;
		private ComponentArt.Web.UI.Calendar calStartP;
		private System.Web.UI.HtmlControls.HtmlImage calStartB;
		private ComponentArt.Web.UI.Calendar calStartC;
		private ComponentArt.Web.UI.Calendar calEndP;
		private System.Web.UI.HtmlControls.HtmlImage calEndB;
		private ComponentArt.Web.UI.Calendar calEndC;
        private CheckBox cbIsLocal;
        private ComponentArt.Web.UI.ComboBox comboLocation;

        #endregion

        #region Registration

        protected ComponentArt.Web.UI.PageView registrationView;
        private TextBox tbBaseRegistrationFee;        
        private DropDownList ddClassUnitType;
		private TextBox tbClassUnitFee;
		private CheckBox cbRegistrationEnabled;
		private ComponentArt.Web.UI.Calendar calRegStartP;
		private System.Web.UI.HtmlControls.HtmlImage calRegStartB;
		private ComponentArt.Web.UI.Calendar calRegStartC;
		private TextBox tbFullEarlyRegistrationFee;
		private ComponentArt.Web.UI.Calendar calEarlyEndP;
		private System.Web.UI.HtmlControls.HtmlImage calEarlyEndB;
		private ComponentArt.Web.UI.Calendar calEarlyEndC;
		private TextBox tbFullRegistrationFee;
		private ComponentArt.Web.UI.Calendar calLateStartP;
		private System.Web.UI.HtmlControls.HtmlImage calLateStartB;
		private ComponentArt.Web.UI.Calendar calLateStartC;
		private TextBox tbFullLateRegistrationFee;
		private ComponentArt.Web.UI.Calendar calRegEndP;
		private System.Web.UI.HtmlControls.HtmlImage calRegEndB;
		private ComponentArt.Web.UI.Calendar calRegEndC;
        private ComponentArt.Web.UI.ComboBox comboRappahanockItem;

        #endregion

        #region Options

        protected ComponentArt.Web.UI.PageView optionsView;
        private MultiSelectBox msOptions;

        #endregion

        #region Details

        protected ComponentArt.Web.UI.PageView detailsView;
        private TextBox tbDetails;
		private TextBox tbDetailsOverrideUrl;
		private TextBox tbPdfUrl;

		#endregion

		private Button btOk;
		private Button btCancel;
		private Button btDelete;

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoSeminarID
		{
			get
			{
				return dojoSeminarID;
			}
			set
			{
				loadFlag = true;
				dojoSeminarID = value;
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
            Panel container = new Panel();
            container.CssClass = this.CssClass;
            Controls.Add(container);

            Panel header = new Panel();
            header.CssClass = "pHead";
            container.Controls.Add(header);

            headerText = new Literal();
            header.Controls.Add(headerText);

            Panel content = new Panel();
            content.CssClass = "pContent";
            container.Controls.Add(content);

            #region TabStrip

            tabstrip = new ComponentArt.Web.UI.TabStrip();

            // Create the DefaultTabLook instance and add it to the ItemLooks collection
            ComponentArt.Web.UI.ItemLook defaultTabLook = new ComponentArt.Web.UI.ItemLook();
            defaultTabLook.LookId = "DefaultTabLook";
            defaultTabLook.CssClass = "DefaultTab";
            defaultTabLook.HoverCssClass = "DefaultTabHover";
            defaultTabLook.LabelPaddingLeft = Unit.Pixel(10);
            defaultTabLook.LabelPaddingRight = Unit.Pixel(10);
            defaultTabLook.LabelPaddingTop = Unit.Pixel(5);
            defaultTabLook.LabelPaddingBottom = Unit.Pixel(4);
            defaultTabLook.LeftIconUrl = "tab_left_icon.gif";
            defaultTabLook.RightIconUrl = "tab_right_icon.gif";
            defaultTabLook.HoverLeftIconUrl = "hover_tab_left_icon.gif";
            defaultTabLook.HoverRightIconUrl = "hover_tab_right_icon.gif";
            defaultTabLook.LeftIconWidth = Unit.Pixel(3);
            defaultTabLook.LeftIconHeight = Unit.Pixel(21);
            defaultTabLook.RightIconWidth = Unit.Pixel(3);
            defaultTabLook.RightIconHeight = Unit.Pixel(21);
            tabstrip.ItemLooks.Add(defaultTabLook);

            // Create the SelectedTabLook instance and add it to the ItemLooks collection
            ComponentArt.Web.UI.ItemLook selectedTabLook = new ComponentArt.Web.UI.ItemLook();
            selectedTabLook.LookId = "SelectedTabLook";
            selectedTabLook.CssClass = "SelectedTab";
            selectedTabLook.LabelPaddingLeft = Unit.Pixel(10);
            selectedTabLook.LabelPaddingRight = Unit.Pixel(10);
            selectedTabLook.LabelPaddingTop = Unit.Pixel(5);
            selectedTabLook.LabelPaddingBottom = Unit.Pixel(4);
            selectedTabLook.LeftIconUrl = "selected_tab_left_icon.gif";
            selectedTabLook.RightIconUrl = "selected_tab_right_icon.gif";
            selectedTabLook.LeftIconWidth = Unit.Pixel(3);
            selectedTabLook.LeftIconHeight = Unit.Pixel(21);
            selectedTabLook.RightIconWidth = Unit.Pixel(3);
            selectedTabLook.RightIconHeight = Unit.Pixel(21);
            tabstrip.ItemLooks.Add(selectedTabLook);

            ComponentArt.Web.UI.ItemLook scrollItemLook = new ItemLook();
            scrollItemLook.LookId = "ScrollItem";
            scrollItemLook.CssClass = "ScrollItem";
            scrollItemLook.HoverCssClass = "ScrollItemHover";
            scrollItemLook.LabelPaddingLeft = Unit.Pixel(5);
            scrollItemLook.LabelPaddingRight = Unit.Pixel(5);
            scrollItemLook.LabelPaddingTop = Unit.Pixel(0);
            scrollItemLook.LabelPaddingBottom = Unit.Pixel(0);
            tabstrip.ItemLooks.Add(scrollItemLook);

            tabstrip.ID = this.ID + "_TabStrip";
            tabstrip.CssClass = "TopGroup";
            tabstrip.DefaultItemLookId = "DefaultTabLook";
            tabstrip.DefaultSelectedItemLookId = "SelectedTabLook";
            tabstrip.DefaultGroupTabSpacing = 1;
            tabstrip.ImagesBaseUrl = "tabstrip_images/";
            tabstrip.MultiPageId = this.ID + "_MultiPage";
            tabstrip.ScrollingEnabled = true;
            tabstrip.ScrollLeftLookId = "ScrollItem";
            tabstrip.ScrollRightLookId = "ScrollItem";
            content.Controls.Add(tabstrip);

            #endregion

            #region MultiPage

            multipage = new ComponentArt.Web.UI.MultiPage();
            multipage.ID = this.ID + "_MultiPage";
            multipage.CssClass = "MultiPage";
            content.Controls.Add(multipage);

            #endregion

			#region General

            generalView = new ComponentArt.Web.UI.PageView();
            generalView.CssClass = "PageContent";
            multipage.PageViews.Add(generalView);

            TabStripTab generalTab = new TabStripTab();
            generalTab.Text = "General";
            generalTab.PageViewId = generalView.ID;
            tabstrip.Tabs.Add(generalTab);

            tbName = new TextBox();
			tbName.EnableViewState = false;
			tbName.Width = Unit.Pixel(350);
            registerControl(generalView, "Name", tbName);

            tbDescription = new TextBox();
            tbDescription.EnableViewState = false;
            tbDescription.Rows = 3;
            tbDescription.TextMode = TextBoxMode.MultiLine;
            tbDescription.Width = Unit.Pixel(350);
            registerControl(generalView, "Description", tbDescription);

            PlaceHolder phStartDate = new PlaceHolder();
            generalView.Controls.Add(phStartDate);
            CalendarHelper.RegisterCalendarPair(phStartDate, "calStartDate",
                DateTime.Now.Subtract(TimeSpan.FromDays(365 * 20)),
                out calStartP, out calStartB, out calStartC, true);
            registerControl(generalView, "First Day", phStartDate);

            PlaceHolder phEndDate = new PlaceHolder();
            generalView.Controls.Add(phEndDate);
            CalendarHelper.RegisterCalendarPair(phEndDate, "calEndDate",
                DateTime.Now.Subtract(TimeSpan.FromDays(365 * 20)),
                out calEndP, out calEndB, out calEndC, true);
            registerControl(generalView, "Last Day", phEndDate);

            cbIsLocal = new CheckBox();
            cbIsLocal.EnableViewState = false;
            cbIsLocal.Text = "Yes";
            registerControl(generalView, "Local Seminar", cbIsLocal);
            
            comboLocation = new ComponentArt.Web.UI.ComboBox();
            comboLocation.CssClass = "comboBox";
            comboLocation.HoverCssClass = "comboBoxHover";
            comboLocation.FocusedCssClass = "comboBoxHover";
            comboLocation.TextBoxCssClass = "comboTextBox";
            comboLocation.DropDownCssClass = "comboDropDown";
            comboLocation.ItemCssClass = "comboItem";
            comboLocation.ItemHoverCssClass = "comboItemHover";
            comboLocation.SelectedItemCssClass = "comboItemHover";
            comboLocation.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboLocation.DropImageUrl = "combobox_images/drop.gif";
            comboLocation.Width = Unit.Pixel(300);
            registerControl(generalView, "Location", comboLocation);

            #endregion

            #region Registration

            registrationView = new ComponentArt.Web.UI.PageView();
            registrationView.CssClass = "PageContent";
            multipage.PageViews.Add(registrationView);

            TabStripTab registrationTab = new TabStripTab();
            registrationTab.Text = "Registration";
            registrationTab.PageViewId = registrationView.ID;
            tabstrip.Tabs.Add(registrationTab);

            tbBaseRegistrationFee = new TextBox();
            tbBaseRegistrationFee.EnableViewState = false;
			tbBaseRegistrationFee.Width = Unit.Pixel(50);
            registerControl(registrationView, "Base Fee",
                tbBaseRegistrationFee);

            PlaceHolder phClassUnitFee = new PlaceHolder();
            registrationView.Controls.Add(phClassUnitFee);
            tbClassUnitFee = new TextBox();
            tbClassUnitFee.EnableViewState = false;
			tbClassUnitFee.Width = Unit.Pixel(50);
			phClassUnitFee.Controls.Add(tbClassUnitFee);
            phClassUnitFee.Controls.Add(new LiteralControl(" per "));
            ddClassUnitType = new DropDownList();
            phClassUnitFee.Controls.Add(ddClassUnitType);
            registerControl(registrationView, "Class Unit Fee", phClassUnitFee);

            cbRegistrationEnabled = new CheckBox();
			cbRegistrationEnabled.EnableViewState = false;
			cbRegistrationEnabled.Text = "Yes";
            registerControl(registrationView, "Enable Registration",
                cbRegistrationEnabled);

            PlaceHolder phRegStart = new PlaceHolder();
            registrationView.Controls.Add(phRegStart);
            CalendarHelper.RegisterCalendarPair(phRegStart, 
                "calRegStart", DateTime.Now,
				out calRegStartP, out calRegStartB, out calRegStartC, true);
            registerControl(registrationView, "Registration Starts", phRegStart);

            tbFullEarlyRegistrationFee = new TextBox();
			tbFullEarlyRegistrationFee.EnableViewState = false;
			tbFullEarlyRegistrationFee.Width = Unit.Pixel(50);
            registerControl(registrationView, "Full Early Reg. Fee",
                tbFullEarlyRegistrationFee);

            PlaceHolder phEarlyEnd = new PlaceHolder();
            registrationView.Controls.Add(phEarlyEnd);
            CalendarHelper.RegisterCalendarPair(phEarlyEnd, 
                "calEarlyEnd", DateTime.Now,
                out calEarlyEndP, out calEarlyEndB, out calEarlyEndC, true);
            registerControl(registrationView, "Early Registration Ends", phEarlyEnd);

            tbFullRegistrationFee = new TextBox();
			tbFullRegistrationFee.EnableViewState = false;
			tbFullRegistrationFee.Width = Unit.Pixel(50);
            registerControl(registrationView, "Full Reg. Fee",
                tbFullRegistrationFee);

            PlaceHolder phLateStart = new PlaceHolder();
            registrationView.Controls.Add(phLateStart);
            CalendarHelper.RegisterCalendarPair(phLateStart,
                "calLateStart", DateTime.Now,
                out calLateStartP, out calLateStartB, out calLateStartC, true);
            registerControl(registrationView, "Late Reg. Starts", phLateStart);

            tbFullLateRegistrationFee = new TextBox();
			tbFullLateRegistrationFee.EnableViewState = false;
			tbFullLateRegistrationFee.Width = Unit.Pixel(50);
            registerControl(registrationView, "Full Late Reg. Fee",
                tbFullLateRegistrationFee);

            PlaceHolder phRegEnd = new PlaceHolder();
            registrationView.Controls.Add(phRegEnd);
            CalendarHelper.RegisterCalendarPair(phRegEnd,
                "calRegEnd", DateTime.Now,
                out calRegEndP, out calRegEndB, out calRegEndC, true);
            registerControl(registrationView, "Registration Ends", phRegEnd);

            comboRappahanockItem = new ComponentArt.Web.UI.ComboBox();
            comboRappahanockItem.CssClass = "comboBox";
            comboRappahanockItem.HoverCssClass = "comboBoxHover";
            comboRappahanockItem.FocusedCssClass = "comboBoxHover";
            comboRappahanockItem.TextBoxCssClass = "comboTextBox";
            comboRappahanockItem.DropDownCssClass = "comboDropDown";
            comboRappahanockItem.ItemCssClass = "comboItem";
            comboRappahanockItem.ItemHoverCssClass = "comboItemHover";
            comboRappahanockItem.SelectedItemCssClass = "comboItemHover";
            comboRappahanockItem.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboRappahanockItem.DropImageUrl = "combobox_images/drop.gif";
            comboRappahanockItem.Width = Unit.Pixel(300);
            registerControl(registrationView, "Rappahanock Item", comboRappahanockItem);

            #endregion

            #region Options

            optionsView = new ComponentArt.Web.UI.PageView();
            optionsView.CssClass = "PageContent";
            multipage.PageViews.Add(optionsView);

            TabStripTab optionsTab = new TabStripTab();
            optionsTab.Text = "Options";
            optionsTab.PageViewId = optionsView.ID;
            tabstrip.Tabs.Add(optionsTab);

            msOptions = new MultiSelectBox();
            msOptions.Mode = MultiSelectBoxMode.CheckBoxList;
            registerControl(optionsView, "Options", msOptions);

            #endregion

            #region Details

            detailsView = new ComponentArt.Web.UI.PageView();
            detailsView.CssClass = "PageContent";
            multipage.PageViews.Add(detailsView);

            TabStripTab detailsTab = new TabStripTab();
            detailsTab.Text = "Details";
            detailsTab.PageViewId = detailsView.ID;
            tabstrip.Tabs.Add(detailsTab);

            tbDetails = new TextBox();
            tbDetails.TextMode = TextBoxMode.MultiLine;
            tbDetails.Rows = 20;
            tbDetails.Width = Unit.Percentage(100);
            tbDetails.EnableViewState = false;
            registerControl(detailsView, "Details", tbDetails);

            tbDetailsOverrideUrl = new TextBox();
			tbDetailsOverrideUrl.EnableViewState = false;
			tbDetailsOverrideUrl.Width = Unit.Pixel(350);
            registerControl(detailsView, "Details Override URL", tbDetailsOverrideUrl);

            tbPdfUrl = new TextBox();
			tbPdfUrl.EnableViewState = false;
			tbPdfUrl.Width = Unit.Pixel(350);
            registerControl(detailsView, "PDF Link URL", tbPdfUrl);
            
			#endregion

            #region Buttons

            Panel buttons = new Panel();
            buttons.CssClass = "pButtons";
            content.Controls.Add(buttons);

            btOk = new Button();
            btOk.Text = "OK";
            btOk.Width = Unit.Pixel(72);
            btOk.EnableViewState = false;
            btOk.Click += new EventHandler(ok_Click);
            buttons.Controls.Add(btOk);

            btCancel = new Button();
            btCancel.Text = "Cancel";
            btCancel.Width = Unit.Pixel(72);
            btCancel.EnableViewState = false;
            btCancel.Click += new EventHandler(cancel_Click);
            buttons.Controls.Add(btCancel);

            btDelete = new Button();
            btDelete.Text = "Delete";
            btDelete.Width = Unit.Pixel(72);
            btDelete.EnableViewState = false;
            btDelete.Click += new EventHandler(delete_Click);
            buttons.Controls.Add(btDelete);

            #endregion

            bind();

			ChildControlsCreated = true;
		}

        private void registerControl(ComponentArt.Web.UI.PageView pageView,
            string caption,
            Control control)
        {
            ControlCollection controls = pageView.Controls;

            controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            controls.Add(new LiteralControl("<span class=\"inputlabel\">"));
            controls.Add(new LiteralControl(caption));
            controls.Add(new LiteralControl("</span><span class=\"inputfield\">"));
            controls.Add(control);
            controls.Add(new LiteralControl("</span></div>"));
        }

		private void bind()
		{
			#region Bind Default Child Data

            string[] classUnitTypeNames;
            byte[] classUnitTypeValues;

            msOptions.Items.Clear();
			DojoSeminarOptionManager optionsManager = 
                new DojoSeminarOptionManager();
			DojoSeminarOptionCollection optionsCollection = 
                optionsManager.GetCollection(string.Empty, string.Empty);
			foreach(DojoSeminarOption options in optionsCollection)
			{
				ListItem i = new ListItem(options.Name +                     
                    " (" + options.Fee.ToString("c") + ")", options.ID.ToString());
				msOptions.Items.Add(i);
			}

			GreyFoxContactManager locationManager = 
                new GreyFoxContactManager("kitTessen_Locations");
			GreyFoxContactCollection locationCollection = 
                locationManager.GetCollection(string.Empty, string.Empty);
			foreach(GreyFoxContact location in locationCollection)
			{
                ComboBoxItem item = new ComboBoxItem();
                item.Text = location.BusinessName;
                item.Value = location.ID.ToString();
                comboLocation.Items.Add(item);
			}

            RHItemManager rhim = new RHItemManager();
            RHItemCollection rhItems = 
                rhim.GetCollection(string.Empty, string.Empty, null);
            foreach (RHItem rhItem in rhItems)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = rhItem.Name;
                item.Value = rhItem.ID.ToString();
                comboRappahanockItem.Items.Add(item);
            }

            //
            // Bind Class Unit Types
            //            
            ddClassUnitType.Items.Clear();
            classUnitTypeNames = Enum.GetNames(typeof(DojoSeminarClassUnitType));
            classUnitTypeValues = (byte[])Enum.GetValues(typeof(DojoSeminarClassUnitType));
            for (int i = 0; i <= classUnitTypeNames.GetUpperBound(0); i++)
            {
                ddClassUnitType.Items.Add(new ListItem(
                    classUnitTypeNames[i],
                    classUnitTypeValues[i].ToString()));
            }

			#endregion            
        }

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoSeminarID == 0)
				editDojoSeminar = new DojoSeminar();
			else
				editDojoSeminar = new DojoSeminar(dojoSeminarID);

			editDojoSeminar.Name = tbName.Text;
			editDojoSeminar.StartDate = calStartP.SelectedDate;
			editDojoSeminar.EndDate = calEndP.SelectedDate;
			editDojoSeminar.Description = tbDescription.Text;
			editDojoSeminar.PdfUrl = tbPdfUrl.Text;
			editDojoSeminar.ClassUnitFee = decimal.Parse(tbClassUnitFee.Text);
			editDojoSeminar.BaseRegistrationFee = decimal.Parse(tbBaseRegistrationFee.Text);
			editDojoSeminar.RegistrationEnabled = cbRegistrationEnabled.Checked;

			editDojoSeminar.RegistrationStart = calRegStartP.SelectedDate;
			editDojoSeminar.FullEarlyRegistrationFee = decimal.Parse(tbFullEarlyRegistrationFee.Text);
			editDojoSeminar.EarlyEndDate = calEarlyEndP.SelectedDate;			
			editDojoSeminar.FullRegistrationFee = decimal.Parse(tbFullRegistrationFee.Text);
			editDojoSeminar.LateStartDate = calLateStartP.SelectedDate;
			editDojoSeminar.FullLateRegistrationFee = decimal.Parse(tbFullLateRegistrationFee.Text);
			editDojoSeminar.RegistrationEnd = calRegEndP.SelectedDate;

			editDojoSeminar.DetailsOverrideUrl = tbDetailsOverrideUrl.Text;
			
			editDojoSeminar.ClassUnitType = (DojoSeminarClassUnitType)
                Enum.Parse(typeof(DojoSeminarClassUnitType), ddClassUnitType.SelectedItem.Value);
			editDojoSeminar.Details = tbDetails.Text;
			editDojoSeminar.IsLocal = cbIsLocal.Checked;

			if(msOptions.IsChanged)
			{
				editDojoSeminar.Options = new DojoSeminarOptionCollection();
				foreach(ListItem i in msOptions.Items)
					if(i.Selected)
						editDojoSeminar.Options.Add(DojoSeminarOption.NewPlaceHolder(int.Parse(i.Value)));
			}

            /// Selects the specified location, otherwise
            /// creates a new location.
            if (comboLocation.SelectedItem != null)
            {
                editDojoSeminar.Location =
                    GreyFoxContact.NewPlaceHolder(DojoSeminarManager.LocationTable,
                    int.Parse(comboLocation.SelectedValue));
            }
            else
            {
                if (comboLocation.Text != string.Empty)
                {
                    GreyFoxContact location =
                        new GreyFoxContact(DojoSeminarManager.LocationTable);
                    location.BusinessName = comboLocation.Text;
                    location.Save();
                    editDojoSeminar.Location = location;
                }
                else
                {
                    editDojoSeminar.Location = null;
                }
            }

            // Set the Rappahanock Item, otherwise create a new
            // item in Rappahanock that is tied to the seminar.
            // This is for SalesOrder and invoicing.
            if (comboRappahanockItem.SelectedItem != null)
            {
                editDojoSeminar.Item =
                    RHItem.NewPlaceHolder(
                    int.Parse(comboRappahanockItem.SelectedValue));
            }
            else
            {
                if (comboRappahanockItem.Text != string.Empty)
                {
                    RHItem newItem = RHFactory.ServiceItem(
                        comboRappahanockItem.Text,
                        tbDescription.Text,
                        decimal.Parse(tbFullRegistrationFee.Text),
                        null);
                    newItem.Save();
                    editDojoSeminar.Item = newItem;
                }
                else
                {
                    editDojoSeminar.Item = null;
                }
            }

			if(editOnAdd)
				dojoSeminarID = editDojoSeminar.Save();
			else
				editDojoSeminar.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				calStartP.SelectedDate = DateTime.Now;				
				calEndP.SelectedDate = DateTime.Now;
				tbDescription.Text = string.Empty;
				tbPdfUrl.Text = string.Empty;
				tbClassUnitFee.Text = string.Empty;
				tbBaseRegistrationFee.Text = string.Empty;
				cbRegistrationEnabled.Checked = false;
				
				calRegStartP.SelectedDate = DateTime.Now;
				tbFullEarlyRegistrationFee.Text = "0";
				calEarlyEndP.SelectedDate = DateTime.Now;
				tbFullRegistrationFee.Text = "0";
				calLateStartP.SelectedDate = DateTime.Now;
				tbFullLateRegistrationFee.Text = "0";
				calRegEndP.SelectedDate = DateTime.Now;
				
				tbDetailsOverrideUrl.Text = string.Empty;
				
				ddClassUnitType.SelectedIndex = 0;
				tbDetails.Text = string.Empty;
				cbIsLocal.Checked = false;
                comboLocation.Text = string.Empty;
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

		protected override void OnPreRender(EventArgs e)
		{
            if (loadFlag)
            {
                tabstrip.SelectedTab = tabstrip.Tabs[0];

                if (dojoSeminarID > 0)
                {
                    editDojoSeminar = new DojoSeminar(dojoSeminarID);

                    //
                    // Set Field Entries
                    //
                    tbName.Text = editDojoSeminar.Name;
                    calStartP.SelectedDate = editDojoSeminar.StartDate;
                    calEndP.SelectedDate = editDojoSeminar.EndDate;
                    tbDescription.Text = editDojoSeminar.Description;
                    cbIsLocal.Checked = editDojoSeminar.IsLocal;
                    tbClassUnitFee.Text = editDojoSeminar.ClassUnitFee.ToString();
                    tbBaseRegistrationFee.Text = editDojoSeminar.BaseRegistrationFee.ToString();

                    cbRegistrationEnabled.Checked = editDojoSeminar.RegistrationEnabled;

                    calRegStartP.SelectedDate = editDojoSeminar.RegistrationStart;
                    tbFullEarlyRegistrationFee.Text = editDojoSeminar.FullEarlyRegistrationFee.ToString();
                    calEarlyEndP.SelectedDate = editDojoSeminar.EarlyEndDate;
                    tbFullRegistrationFee.Text = editDojoSeminar.FullRegistrationFee.ToString();
                    calLateStartP.SelectedDate = editDojoSeminar.LateStartDate;
                    tbFullLateRegistrationFee.Text = editDojoSeminar.FullLateRegistrationFee.ToString();
                    calRegEndP.SelectedDate = editDojoSeminar.RegistrationEnd;

                    tbDetails.Text = editDojoSeminar.Details;
                    tbDetailsOverrideUrl.Text = editDojoSeminar.DetailsOverrideUrl;
                    tbPdfUrl.Text = editDojoSeminar.PdfUrl;

                    //
                    // Set Children Selections
                    //
                    if (editDojoSeminar.Location != null)
                    {
                        comboLocation.Text = editDojoSeminar.Location.BusinessName;
                        foreach (ComboBoxItem item in comboLocation.Items)
                        {
                            if (item.Value == editDojoSeminar.Location.ID.ToString())
                            {
                                comboLocation.SelectedItem = item;
                                break;
                            }
                        }
                    }
                    else
                    {
                        comboLocation.Text = string.Empty;
                    }

                    foreach (ListItem i in msOptions.Items)
                        foreach (DojoSeminarOption dojoSeminarOption in editDojoSeminar.Options)
                            if (i.Value == dojoSeminarOption.ID.ToString())
                            {
                                i.Selected = true;
                                break;
                            }

                    foreach (ListItem i in ddClassUnitType.Items)
                        i.Selected = i.Value == ((byte)editDojoSeminar.ClassUnitType).ToString();

                    if (editDojoSeminar.Item != null)
                    {
                        comboRappahanockItem.Text = editDojoSeminar.Item.Name;
                        foreach (ComboBoxItem item in comboRappahanockItem.Items)
                        {
                            if (item.Value == editDojoSeminar.Item.ID.ToString())
                            {
                                comboRappahanockItem.SelectedItem = item;
                                break;
                            }
                        }
                    }
                    else
                    {
                        comboRappahanockItem.Text = string.Empty;
                    }

                    headerText.Text = "Edit  - " + editDojoSeminar.Name;
                }
                else
                    headerText.Text = "Add ";
            }
		}

		protected override void LoadViewState(object savedState)
		{
			if(savedState != null)
			{
				object[] myState = (object[]) savedState;
				if(myState[0] != null)
					base.LoadViewState(myState[0]);
				if(myState[1] != null)
					dojoSeminarID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoSeminarID;
			return myState;
		}
	}
}
