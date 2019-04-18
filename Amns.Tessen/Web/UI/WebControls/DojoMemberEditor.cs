using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Amns.GreyFox.People;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.GreyFox.Security;
using Amns.Tessen.Utilities;
using ComponentArt.Web.UI;
using Amns.Rappahanock;
using Amns.Rappahanock.Utilities;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// The member editor is a dynamic editor which displays information based on the
	/// role setting provided. It will edit all properties of a member, and in addition
	/// insert, delete, update their Amns.GreyFox accounts information.
	/// 
	/// This editor can also trigger promotion scans if configured to do so.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:DojoMemberEditor runat=server></{0}:DojoMemberEditor>")]
	public class DojoMemberEditor : System.Web.UI.Control, INamingContainer
	{
        private DojoMember member;

		private int memberID;
        private bool loadFlag;
		private bool resetOnAdd;
		private bool editOnAdd;
        private string cssClass;

        protected ComponentArt.Web.UI.TabStrip tabstrip;
        protected ComponentArt.Web.UI.MultiPage multipage;
        protected Literal headerText;

        protected ComponentArt.Web.UI.PageView generalView;
        private TextBox tbName;
        private TextBox tbAddress1;
        private TextBox tbAddress2;
        private TextBox tbCity;
        private TextBox tbStateProvince;
        private TextBox tbPostalCode;
        private TextBox tbCountry;
        private TextBox tbHomePhone;
        private TextBox tbWorkPhone;
        private TextBox tbMobilePhone;
        private TextBox tbPager;
        private TextBox tbEmail1;
        private TextBox tbEmail2;
        private TextBox tbUrl;
        private CheckBox cbBadAddress;
        private CheckBox cbBadHomePhone;
        private CheckBox cbBadWorkPhone;
        private CheckBox cbBadMobilePhone;
        private CheckBox cbBadEmail;
        private CheckBox cbBadUrl;
        private ComponentArt.Web.UI.Calendar calBirthDateP;
        private System.Web.UI.HtmlControls.HtmlImage calBirthDateB;
        private ComponentArt.Web.UI.Calendar calBirthDateC;

        protected ComponentArt.Web.UI.PageView emergencyView;
		private TextBox tbEName;
		private TextBox tbEAddress1;
		private TextBox tbEAddress2;
		private TextBox tbECity;
		private TextBox tbEStateProvince;
		private TextBox tbEPostalCode;
		private TextBox tbECountry;
		private TextBox tbEHomePhone;
		private TextBox tbEWorkPhone;
		private TextBox tbEMobilePhone;
		private TextBox tbEPager;
		private TextBox tbEEmail1;
		private TextBox tbEEmail2;
		private TextBox tbEUrl;

        protected ComponentArt.Web.UI.PageView membershipView;
        protected ComponentArt.Web.UI.ComboBox comboStudentType;
        private ComponentArt.Web.UI.ComboBox comboMemberTypeTemplate;
        protected ComponentArt.Web.UI.ComboBox comboParent;
        private ComponentArt.Web.UI.Calendar calMemberDateP;
        private System.Web.UI.HtmlControls.HtmlImage calMemberDateB;
        private ComponentArt.Web.UI.Calendar calMemberDateC;
        private CheckBox cbActive;
        private CheckBox cbSignedWaiver;
        private CheckBox cbInstructor;
        private TextBox tbOrganizationID;
        private CheckBox cbOrganizationActive;
        private CheckBox cbIsPastDue;
        private TextBox tbUserAccount;
        private CheckBox cbIsDisabled;
        private Label lbUserAccount;
        private TextBox tbMemberTypeTreeHash;
        private LiteralControl ltMessage;

        protected ComponentArt.Web.UI.PageView rankView;
        private ComponentArt.Web.UI.ComboBox comboRank;
        private ComponentArt.Web.UI.Calendar calRankDateP;
        private System.Web.UI.HtmlControls.HtmlImage calRankDateB;
        private ComponentArt.Web.UI.Calendar calRankDateC;
        private ComponentArt.Web.UI.ComboBox comboPromotionFlag;
        
        protected ComponentArt.Web.UI.PageView memoView;
        private TextBox tbMemoText;

        protected ComponentArt.Web.UI.PageView attendanceView;
		private TextBox tbAttendanceMessage;

        protected ComponentArt.Web.UI.PageView primaryAccountView;
        private TextBox tbAccountName;
        private TextBox tbAccountNumber;
        private TextBox tbAccountExpMonth;
        private RequiredFieldValidator reqAccountExpMonth;
        private RangeValidator rngAccountExpMonth;
        private TextBox tbAccountExpYear;
        private RequiredFieldValidator reqAccountExpYear;
        private RangeValidator rngAccountExpYear;
        private DropDownList ddAccountType;

		private Button btOk;
		private Button btCancel;
		private Button btDelete;

		#region Public Properties
		
		[Bindable(true),
		Category("Behavior"),
		DefaultValue(0)]
		public int MemberID
		{
			get
			{
				return memberID;
			}
			set
			{
                loadFlag = true;
				memberID = value;
			}
		}

		[Bindable(true),
		Category("Behavior"),
		DefaultValue(false)]
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

		[Bindable(true),
		Category("Behavior"),
		DefaultValue(false)]
		public bool AutoEdit
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

        [Bindable(true), Category("Appearance"), DefaultValue(false)]
        public string CssClass
        {
            get
            {
                return cssClass;
            }
            set
            {
                cssClass = value;
            }
        }

		#endregion

		// Organization activity cannot be set here, it is set through organization interractions,
		// however it is displayed with a javascript selector. A drop down list has the organizations
		// available for the student to be part of, and the student's status in the organization.
		// On promotion scans, organization status codes for students can be required.

		protected override void CreateChildControls()
		{
            base.CreateChildControls();

            Panel container = new Panel();
            container.ID = this.ID;
            container.CssClass = this.CssClass;
            Controls.Add(container);

            Panel header = new Panel();
            header.ID = "header";
            header.CssClass = "pHead";
            container.Controls.Add(header);

            headerText = new Literal();
            headerText.ID = "headerText";
            header.Controls.Add(headerText);

            Panel content = new Panel();
            content.ID = "content";
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
            tabstrip.EnableViewState = false;
            content.Controls.Add(tabstrip);

            #endregion

            #region MultiPage

            multipage = new ComponentArt.Web.UI.MultiPage();
            multipage.ID = this.ID + "_MultiPage";
            multipage.CssClass = "MultiPage";
            content.Controls.Add(multipage);

            #endregion

			#region Contact Information

            generalView = new ComponentArt.Web.UI.PageView();
            generalView.ID = "generalView";
            generalView.CssClass = "PageContent";
            multipage.PageViews.Add(generalView);

            TabStripTab generalTab = new TabStripTab();
            generalTab.ID = "generalTab";
            generalTab.Text = Localization.PeopleStrings.GeneralTab;
            generalTab.PageViewId = generalView.ID;
            tabstrip.Tabs.Add(generalTab);

            tbName = new TextBox();
            tbName.ID = "tbName";
            tbName.Width = Unit.Pixel(200);
            tbName.EnableViewState = false;
            cbActive = new CheckBox();
            cbActive.ID = "cbActive";
            cbActive.EnableViewState = false;
            cbActive.Text = Localization.Strings.ActiveMembership;            
            registerControl(generalView,
                Localization.PeopleStrings.Name, tbName, cbActive);

            tbAddress1 = new TextBox();
            tbAddress1.ID = "tbAddress1";
            tbAddress1.Width = Unit.Pixel(175);
            tbAddress1.EnableViewState = false;
            cbBadAddress = new CheckBox();
            cbBadAddress.ID = "cbBadAddress";
            cbBadAddress.Text = Localization.PeopleStrings.Invalid;
            registerControl(generalView,
                Localization.PeopleStrings.Address, tbAddress1, cbBadAddress);

            tbAddress2 = new TextBox();
            tbAddress2.ID = "tbAddress2";
            tbAddress2.Width = Unit.Pixel(175);
            tbAddress2.EnableViewState = false;
            registerControl(generalView, string.Empty, tbAddress2);

            tbCity = new TextBox();
            tbCity.ID = "tbCity";
            tbCity.Width = Unit.Pixel(175);
            tbCity.EnableViewState = false;
            registerControl(generalView,
                Localization.PeopleStrings.City, tbCity);

            tbStateProvince = new TextBox();
            tbStateProvince.ID = "tbStateProvince";
            tbStateProvince.Width = Unit.Pixel(175);
            tbStateProvince.EnableViewState = false;
            registerControl(generalView,
                Localization.PeopleStrings.StateProvince, tbStateProvince);

            tbPostalCode = new TextBox();
            tbPostalCode.ID = "tbPostalCode";
            tbPostalCode.Width = Unit.Pixel(175);
            tbPostalCode.EnableViewState = false;
            registerControl(generalView,
                Localization.PeopleStrings.PostalCode, tbPostalCode);

            tbCountry = new TextBox();
            tbCountry.ID = "tbCountry";
            tbCountry.Width = Unit.Pixel(175);
            tbCountry.EnableViewState = false;
            registerControl(generalView,
                Localization.PeopleStrings.Country, tbCountry);

            tbHomePhone = new TextBox();
            tbHomePhone.ID = "tbHomePhone";
            tbHomePhone.Width = Unit.Pixel(175);
            tbHomePhone.EnableViewState = false;
            cbBadHomePhone = new CheckBox();
            cbBadHomePhone.ID = "cbBadHomePhone";
            cbBadHomePhone.Text = Localization.PeopleStrings.Invalid;
            registerControl(generalView,
                Localization.PeopleStrings.HomePhone, tbHomePhone, cbBadHomePhone);

            tbWorkPhone = new TextBox();
            tbWorkPhone.ID = "tbWorkPhone";
            tbWorkPhone.Width = Unit.Pixel(175);
            tbWorkPhone.EnableViewState = false;
            cbBadWorkPhone = new CheckBox();
            cbBadWorkPhone.ID = "cbBadWorkPhone";
            cbBadWorkPhone.Text = Localization.PeopleStrings.Invalid;
            registerControl(generalView,
                Localization.PeopleStrings.WorkPhone, tbWorkPhone, cbBadWorkPhone);

            tbMobilePhone = new TextBox();
            tbMobilePhone.ID = "tbMobilePhone";
            tbMobilePhone.Width = Unit.Pixel(175);
            tbMobilePhone.EnableViewState = false;
            cbBadMobilePhone = new CheckBox();
            cbBadMobilePhone.ID = "cbBadMobilePhone";
            cbBadMobilePhone.Text = Localization.PeopleStrings.Invalid;
            registerControl(generalView,
                Localization.PeopleStrings.MobilePhone, tbMobilePhone, cbBadMobilePhone);

            tbPager = new TextBox();
            tbPager.ID = "tbPager";
            tbPager.Width = Unit.Pixel(175);
            tbPager.EnableViewState = false;
            registerControl(generalView,
                Localization.PeopleStrings.Pager, tbPager);

            tbEmail1 = new TextBox();
            tbEmail1.ID = "tbEmail1";
            tbEmail1.Width = Unit.Pixel(175);
            tbEmail1.EnableViewState = false;
            cbBadEmail = new CheckBox();
            cbBadEmail.ID = "cbBadEmail";
            cbBadEmail.Text = Localization.PeopleStrings.Invalid;
            registerControl(generalView,
                Localization.PeopleStrings.Email, tbEmail1, cbBadEmail);

            tbEmail2 = new TextBox();
            tbEmail2.ID = "tbEmail2";
            tbEmail2.Width = Unit.Pixel(175);
            tbEmail2.EnableViewState = false;
            registerControl(generalView,
                Localization.PeopleStrings.Email2, tbEmail2);

            tbUrl = new TextBox();
            tbUrl.ID = "tbUrl";
            tbUrl.Width = Unit.Pixel(175);
            tbUrl.EnableViewState = false;
            cbBadUrl = new CheckBox();
            cbBadUrl.ID = "cbBadUrl";
            cbBadUrl.Text = Localization.PeopleStrings.Invalid;
            registerControl(generalView,
                Localization.PeopleStrings.WebsiteUrl, tbUrl, cbBadUrl);

            PlaceHolder phBirthDate = new PlaceHolder();
            phBirthDate.ID = "phBirthDate";
            generalView.Controls.Add(phBirthDate);
            CalendarHelper.RegisterCalendarPair(phBirthDate, "calBirthDate", 
                DateTime.Now.Subtract(TimeSpan.FromDays(365 * 20)),
                out calBirthDateP, out calBirthDateB, out calBirthDateC, true);
            registerControl(generalView, 
                Localization.PeopleStrings.BirthDate, phBirthDate);

            #endregion

            #region Emergency Information

            emergencyView = new ComponentArt.Web.UI.PageView();
            emergencyView.ID = "emergencyView";
            emergencyView.CssClass = "PageContent";
            multipage.PageViews.Add(emergencyView);

            TabStripTab emergencyTab = new TabStripTab();
            emergencyTab.Text = Localization.PeopleStrings.EmergencyTab;
            emergencyTab.PageViewId = emergencyView.ID;
            tabstrip.Tabs.Add(emergencyTab);

            tbEName = new TextBox();
            tbEName.ID = "tbEName";
            tbEName.Width = Unit.Pixel(200);
            tbEName.EnableViewState = false;
            registerControl(emergencyView, 
                Localization.PeopleStrings.Name, tbEName);

            tbEAddress1 = new TextBox();
            tbEAddress1.ID = "tbEAddress1";
            tbEAddress1.Width = Unit.Pixel(175);
            tbEAddress1.EnableViewState = false;
            Controls.Add(tbEAddress1);
            registerControl(emergencyView, 
                Localization.PeopleStrings.Address, tbEAddress1);

            tbEAddress2 = new TextBox();
            tbEAddress2.ID = "tbEAddress2";
            tbEAddress2.Width = Unit.Pixel(175);
            tbEAddress2.EnableViewState = false;
            registerControl(emergencyView, "&nbsp;", tbEAddress2);

            tbECity = new TextBox();
            tbECity.ID = "tbECity";
            tbECity.Width = Unit.Pixel(175);
            tbECity.EnableViewState = false;
            Controls.Add(tbECity);
            registerControl(emergencyView, 
                Localization.PeopleStrings.City, tbECity);

            tbEStateProvince = new TextBox();
            tbEStateProvince.ID = "tbEStateProvince";
            tbEStateProvince.Width = Unit.Pixel(175);
            tbEStateProvince.EnableViewState = false;
            registerControl(emergencyView, 
                Localization.PeopleStrings.StateProvince, tbEStateProvince);

            tbEPostalCode = new TextBox();
            tbEPostalCode.ID = "tbEPostalCode";
            tbEPostalCode.Width = Unit.Pixel(175);
            tbEPostalCode.EnableViewState = false;
            registerControl(emergencyView, 
                Localization.PeopleStrings.PostalCode, tbEPostalCode);

            tbECountry = new TextBox();
            tbECountry.ID = "tbECountry";
            tbECountry.Width = Unit.Pixel(175);
            tbECountry.EnableViewState = false;
            registerControl(emergencyView, 
                Localization.PeopleStrings.Country, tbECountry);

            tbEHomePhone = new TextBox();
            tbEHomePhone.ID = "tbEHomePhone";
            tbEHomePhone.Width = Unit.Pixel(175);
            tbEHomePhone.EnableViewState = false;
            registerControl(emergencyView, 
                Localization.PeopleStrings.HomePhone, tbEHomePhone);

            tbEWorkPhone = new TextBox();
            tbEWorkPhone.ID = "tbEWorkPhone";
            tbEWorkPhone.Width = Unit.Pixel(175);
            tbEWorkPhone.EnableViewState = false;
            registerControl(emergencyView, 
                Localization.PeopleStrings.WorkPhone, tbEWorkPhone);

            tbEMobilePhone = new TextBox();
            tbMobilePhone.ID = "tbEMobilePhone";
            tbEMobilePhone.Width = Unit.Pixel(175);
            tbEMobilePhone.EnableViewState = false;
            registerControl(emergencyView, 
                Localization.PeopleStrings.MobilePhone, tbEMobilePhone);

            tbEPager = new TextBox();
            tbEPager.ID = "tbEPager";
            tbEPager.Width = Unit.Pixel(175);
            tbEPager.EnableViewState = false;
            registerControl(emergencyView, 
                Localization.PeopleStrings.Pager, tbEPager);

            tbEEmail1 = new TextBox();
            tbEEmail1.ID = "tbEEmail1";
            tbEEmail1.Width = Unit.Pixel(175);
            tbEEmail1.EnableViewState = false;
            registerControl(emergencyView, 
                Localization.PeopleStrings.Email, tbEEmail1);

            tbEEmail2 = new TextBox();
            tbEEmail2.ID = "tbEEmail2";
            tbEEmail2.Width = Unit.Pixel(175);
            tbEEmail2.EnableViewState = false;
            registerControl(emergencyView, 
                Localization.PeopleStrings.Email2, tbEEmail2);

            tbEUrl = new TextBox();
            tbEUrl.ID = "tbEUrl";
            tbEUrl.Width = Unit.Pixel(175);
            tbEUrl.EnableViewState = false;
            registerControl(emergencyView, 
                Localization.PeopleStrings.WebsiteUrl, tbEUrl);

            #endregion

            #region Membership

            membershipView = new ComponentArt.Web.UI.PageView();
            membershipView.ID = "membershipView";
            membershipView.CssClass = "PageContent";
            multipage.PageViews.Add(membershipView);

            TabStripTab membershipTab = new TabStripTab();
            membershipTab.ID = "membershipTab";
            membershipTab.Text = Localization.Strings.MembershipTab;
            membershipTab.PageViewId = membershipView.ID;
            tabstrip.Tabs.Add(membershipTab);

            comboStudentType = new ComponentArt.Web.UI.ComboBox();
            comboStudentType.ID = "comboStudentType";
            comboStudentType.CssClass = "comboBox";
            comboStudentType.HoverCssClass = "comboBoxHover";
            comboStudentType.FocusedCssClass = "comboBoxHover";
            comboStudentType.TextBoxCssClass = "comboTextBox";
            comboStudentType.DropDownCssClass = "comboDropDown";
            comboStudentType.ItemCssClass = "comboItem";
            comboStudentType.ItemHoverCssClass = "comboItemHover";
            comboStudentType.SelectedItemCssClass = "comboItemHover";
            comboStudentType.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboStudentType.DropImageUrl = "combobox_images/drop.gif";
            comboStudentType.Width = Unit.Pixel(300);
            registerControl(membershipView, Localization.Strings.MemberType, comboStudentType);

            comboMemberTypeTemplate = new ComponentArt.Web.UI.ComboBox();
            comboMemberTypeTemplate.ID = "comboMemberTypeTemplate";
            comboMemberTypeTemplate.CssClass = "comboBox";
            comboMemberTypeTemplate.HoverCssClass = "comboBoxHover";
            comboMemberTypeTemplate.FocusedCssClass = "comboBoxHover";
            comboMemberTypeTemplate.TextBoxCssClass = "comboTextBox";
            comboMemberTypeTemplate.DropDownCssClass = "comboDropDown";
            comboMemberTypeTemplate.ItemCssClass = "comboItem";
            comboMemberTypeTemplate.ItemHoverCssClass = "comboItemHover";
            comboMemberTypeTemplate.SelectedItemCssClass = "comboItemHover";
            comboMemberTypeTemplate.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboMemberTypeTemplate.DropImageUrl = "combobox_images/drop.gif";
            comboMemberTypeTemplate.Width = Unit.Pixel(300);
            registerControl(membershipView,
                Localization.Strings.ParentTemplate, comboMemberTypeTemplate);

            comboParent = new ComponentArt.Web.UI.ComboBox();
            comboParent.ID = "comboParent";
            comboParent.CssClass = "comboBox";
            comboParent.HoverCssClass = "comboBoxHover";
            comboParent.FocusedCssClass = "comboBoxHover";
            comboParent.TextBoxCssClass = "comboTextBox";
            comboParent.DropDownCssClass = "comboDropDown";
            comboParent.ItemCssClass = "comboItem";
            comboParent.ItemHoverCssClass = "comboItemHover";
            comboParent.SelectedItemCssClass = "comboItemHover";
            comboParent.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboParent.DropImageUrl = "combobox_images/drop.gif";
            comboParent.Width = Unit.Pixel(300);
            registerControl(membershipView, Localization.PeopleStrings.Parent, comboParent);

            PlaceHolder phMemberDate = new PlaceHolder();
            phMemberDate.ID = "phMemberDate";
            membershipView.Controls.Add(phMemberDate);
			CalendarHelper.RegisterCalendarPair(phMemberDate, "calMemberDate", DateTime.Now,
				out calMemberDateP, out calMemberDateB, out calMemberDateC, true);
            registerControl(membershipView, Localization.Strings.MemberSince, phMemberDate);

            cbSignedWaiver = new CheckBox();
            cbSignedWaiver.ID = "cbSignedWaiver";
			cbSignedWaiver.EnableViewState = false;
            registerControl(membershipView, "Signed Waiver", cbSignedWaiver);

            cbInstructor = new CheckBox();
            cbInstructor.ID = "cbInstructor";
			cbInstructor.EnableViewState = false;
            registerControl(membershipView, "Instructor", cbInstructor);

            tbOrganizationID = new TextBox();
            tbOrganizationID.ID = "tbOrganizationID";
			tbOrganizationID.EnableViewState = false;
            registerControl(membershipView, "Primary Org. ID", tbOrganizationID);

            cbOrganizationActive = new CheckBox();
            cbOrganizationActive.ID = "cbOrganizationActive";
			cbOrganizationActive.EnableViewState = false;
            registerControl(membershipView, "Primary Org. Active", cbOrganizationActive);

            cbIsPastDue = new CheckBox();
            cbIsPastDue.ID = "cbIsPastDue";
			cbIsPastDue.EnableViewState = false;
            registerControl(membershipView, "Past Due", cbIsPastDue);
            
            tbUserAccount = new TextBox();
            tbUserAccount.ID = "tbUserAccount";
            tbUserAccount.EnableViewState = false;
            cbIsDisabled = new CheckBox();
            cbIsDisabled.Width = Unit.Pixel(175);
            cbIsDisabled.EnableViewState = false;
            cbIsDisabled.Text = Localization.Strings.Disabled;
            registerControl(membershipView, "User Account", tbUserAccount, cbIsDisabled);
            lbUserAccount = new Label();
            lbUserAccount.Text = Localization.Strings.UserAccountNotice;
            lbUserAccount.EnableViewState = false;
            registerControl(membershipView,
                "&nbsp;", lbUserAccount);

            tbMemberTypeTreeHash = new TextBox();
            tbMemberTypeTreeHash.ID = "MemberTypeTreeHash";
            tbMemberTypeTreeHash.EnableViewState = false;
            tbMemberTypeTreeHash.Enabled = false;
            registerControl(membershipView,
                Localization.Strings.TreeHash, tbMemberTypeTreeHash);

            ltMessage = new LiteralControl();
            registerControl(membershipView, "&nbsp;", ltMessage);

            #endregion

            #region Rank and Promotions

            rankView = new ComponentArt.Web.UI.PageView();
            rankView.ID = "rankView";
            rankView.CssClass = "PageContent";
            multipage.PageViews.Add(rankView);

            TabStripTab rankTab = new TabStripTab();
            rankTab.ID = "rankTab";
            rankTab.Text = "Rank & Promotions";
            rankTab.PageViewId = rankView.ID;
            tabstrip.Tabs.Add(rankTab);

            comboRank = new ComponentArt.Web.UI.ComboBox();
            comboRank.ID = "comoRank";
            comboRank.CssClass = "comboBox";
            comboRank.HoverCssClass = "comboBoxHover";
            comboRank.FocusedCssClass = "comboBoxHover";
            comboRank.TextBoxCssClass = "comboTextBox";
            comboRank.DropDownCssClass = "comboDropDown";
            comboRank.ItemCssClass = "comboItem";
            comboRank.ItemHoverCssClass = "comboItemHover";
            comboRank.SelectedItemCssClass = "comboItemHover";
            comboRank.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboRank.DropImageUrl = "combobox_images/drop.gif";
            comboRank.Width = Unit.Pixel(300);
            registerControl(rankView, "Rank", comboRank);

            PlaceHolder phRankDate = new PlaceHolder();
            phRankDate.ID = "phRankDate";
            membershipView.Controls.Add(phRankDate);
            CalendarHelper.RegisterCalendarPair(phRankDate, "calRankDate", DateTime.Now,
                out calRankDateP, out calRankDateB, out calRankDateC, true);
            registerControl(rankView, "&nbsp;", phRankDate);

            comboPromotionFlag = new ComponentArt.Web.UI.ComboBox();
            comboPromotionFlag.ID = "comboPromotionFlag";
            comboPromotionFlag.CssClass = "comboBox";
            comboPromotionFlag.HoverCssClass = "comboBoxHover";
            comboPromotionFlag.FocusedCssClass = "comboBoxHover";
            comboPromotionFlag.TextBoxCssClass = "comboTextBox";
            comboPromotionFlag.DropDownCssClass = "comboDropDown";
            comboPromotionFlag.ItemCssClass = "comboItem";
            comboPromotionFlag.ItemHoverCssClass = "comboItemHover";
            comboPromotionFlag.SelectedItemCssClass = "comboItemHover";
            comboPromotionFlag.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboPromotionFlag.DropImageUrl = "combobox_images/drop.gif";
            comboPromotionFlag.Width = Unit.Pixel(300);
            registerControl(rankView, "Promotion Flag", comboPromotionFlag);

            #endregion

            #region Memo

            memoView = new ComponentArt.Web.UI.PageView();
            memoView.ID = "memoView";
            memoView.CssClass = "PageContent";
            multipage.PageViews.Add(memoView);

            TabStripTab memoTab = new TabStripTab();
            memoTab.ID = "memoTab";
            memoTab.Text = Localization.PeopleStrings.Memo;
            memoTab.PageViewId = memoView.ID;
            tabstrip.Tabs.Add(memoTab);

            tbMemoText = new TextBox();
            tbMemoText.ID = "tbMemoText";
            tbMemoText.TextMode = TextBoxMode.MultiLine;
			tbMemoText.Rows = 7;
            tbMemoText.Width = Unit.Pixel(350);
			tbMemoText.EnableViewState = false;
            registerControl(memoView, Localization.PeopleStrings.Memo, tbMemoText);

            #endregion

            #region Attendance

            attendanceView = new ComponentArt.Web.UI.PageView();
            attendanceView.ID = "attendanceView";
            attendanceView.CssClass = "PageContent";
            multipage.PageViews.Add(attendanceView);

            TabStripTab attendanceTab = new TabStripTab();
            attendanceTab.ID = "attendanceTab";
            attendanceTab.Text = "Attendance";
            attendanceTab.PageViewId = attendanceView.ID;
            tabstrip.Tabs.Add(attendanceTab);

            tbAttendanceMessage = new TextBox();
            tbAttendanceMessage.ID = "tbAttendanceMessage";
            tbAttendanceMessage.TextMode = TextBoxMode.MultiLine;
			tbAttendanceMessage.Rows = 7;
            tbAttendanceMessage.Width = Unit.Pixel(350);
			tbAttendanceMessage.EnableViewState = false;
			Controls.Add(tbAttendanceMessage);
            registerControl(attendanceView, "Attendance Message", tbAttendanceMessage);

            #endregion

            #region Primary Account View

            primaryAccountView = new ComponentArt.Web.UI.PageView();
            primaryAccountView.ID = "PrimaryAccountView";
            primaryAccountView.CssClass = "PageContent";
            multipage.PageViews.Add(primaryAccountView);

            TabStripTab primaryAccountTab = new TabStripTab();
            primaryAccountTab.Text = Localization.Strings.PrimaryAccountTab;
            primaryAccountTab.PageViewId = primaryAccountView.ID;
            tabstrip.Tabs.Add(primaryAccountTab);

            tbAccountName = new TextBox();
            tbAccountName.ID = "tbAccountName";
            tbAccountName.EnableViewState = false;
            registerControl(primaryAccountView, "Account Name", tbAccountName);

            tbAccountNumber = new TextBox();
            tbAccountNumber.ID = "tbAccountNumber";
            tbAccountNumber.EnableViewState = false;
            registerControl(primaryAccountView, "AccountNumber", tbAccountNumber);

            tbAccountExpMonth = new TextBox();
            tbAccountExpMonth.ID = "tbAccountExpMonth";
            tbAccountExpMonth.EnableViewState = false;
            reqAccountExpMonth = new RequiredFieldValidator();
            reqAccountExpMonth.ID = "reqAccountExpMonth";
            reqAccountExpMonth.ControlToValidate = tbAccountExpMonth.ID;
            reqAccountExpMonth.ErrorMessage = "*";
            reqAccountExpMonth.Display = ValidatorDisplay.Dynamic;
            rngAccountExpMonth = new RangeValidator();
            rngAccountExpMonth.ID = "rngAccountExpMonth";
            rngAccountExpMonth.ControlToValidate = tbAccountExpMonth.ID;
            rngAccountExpMonth.ErrorMessage = "*";
            rngAccountExpMonth.Display = ValidatorDisplay.Dynamic;
            rngAccountExpMonth.Type = ValidationDataType.Integer;
            rngAccountExpMonth.MinimumValue = "1";
            rngAccountExpMonth.MaximumValue = "12";
            registerControl(primaryAccountView, "AccountExpMonth", tbAccountExpMonth, reqAccountExpMonth, rngAccountExpMonth);

            tbAccountExpYear = new TextBox();
            tbAccountExpYear.ID = "tbAccountExpYear";
            tbAccountExpYear.EnableViewState = false;
            reqAccountExpYear = new RequiredFieldValidator();
            reqAccountExpYear.ID = "reqAccountExpYear";
            reqAccountExpYear.ControlToValidate = tbAccountExpYear.ID;
            reqAccountExpYear.ErrorMessage = "*";
            reqAccountExpYear.Display = ValidatorDisplay.Dynamic;
            rngAccountExpYear = new RangeValidator();
            rngAccountExpYear.ID = "rngAccountExpYear";
            rngAccountExpYear.ControlToValidate = tbAccountExpYear.ID;
            rngAccountExpYear.ErrorMessage = "*";
            rngAccountExpYear.Display = ValidatorDisplay.Dynamic;
            rngAccountExpYear.Type = ValidationDataType.Integer;
            rngAccountExpYear.MinimumValue = "2000";
            rngAccountExpYear.MaximumValue = "2999";
            registerControl(primaryAccountView, "AccountExpYear", tbAccountExpYear, reqAccountExpYear, rngAccountExpYear);

            ddAccountType = new DropDownList();
            ddAccountType.ID = "ddAccountType";
            ddAccountType.EnableViewState = false;
            registerControl(primaryAccountView, "AccountType", ddAccountType);

            #endregion

            #region Buttons

            Panel buttons = new Panel();
            buttons.ID = "buttons";
            buttons.CssClass = "pButtons";
            content.Controls.Add(buttons);

            btOk = new Button();
            btOk.ID = "btOk";
            btOk.Text = Localization.Strings.OK;
            btOk.Width = Unit.Pixel(72);
            btOk.EnableViewState = false;
            btOk.Click += new EventHandler(ok_Click);
            buttons.Controls.Add(btOk);

            btCancel = new Button();
            btCancel.ID = "btCancel";
            btCancel.Text = Localization.Strings.Cancel;
            btCancel.Width = Unit.Pixel(72);
            btCancel.EnableViewState = false;
            btCancel.Click += new EventHandler(cancel_Click);
            buttons.Controls.Add(btCancel);

            btDelete = new Button();
            btDelete.ID = "btDelete";
            btDelete.Text = Localization.Strings.Delete;
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
            params Control[] controls)
        {
            ControlCollection pageControls = pageView.Controls;

            pageControls.Add(new LiteralControl("<div class=\"inputrow\">"));
            pageControls.Add(new LiteralControl("<span class=\"inputlabel\">"));
            pageControls.Add(new LiteralControl(caption));
            pageControls.Add(new LiteralControl("</span><span class=\"inputfield\">"));
            foreach (System.Web.UI.Control control in controls)            
            {
                pageControls.Add(control);
            }
            pageControls.Add(new LiteralControl("</span></div>"));
        }

        private void bind()
        {
            DojoMemberTypeManager memberTypeManager = new DojoMemberTypeManager();
            DojoMemberTypeCollection memberTypes =
                memberTypeManager.GetCollection(string.Empty, "OrderNum");
            foreach (DojoMemberType memberType in memberTypes)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = memberType.Name;
                item.Value = memberType.ID.ToString();
                comboStudentType.Items.Add(item);
            }

            DojoMemberTypeTemplateManager memberTypeTemplateManager =
                new DojoMemberTypeTemplateManager();
            DojoMemberTypeTemplateCollection memberTypeTemplates =
                memberTypeTemplateManager.GetCollection(string.Empty, string.Empty, null);
            foreach (DojoMemberTypeTemplate typeTemplate in memberTypeTemplates)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = typeTemplate.Name;
                item.Value = typeTemplate.ID.ToString();
                comboMemberTypeTemplate.Items.Add(item);
            }

            DojoMemberManager memberManager = new DojoMemberManager();
            DojoMemberCollection members =
                memberManager.GetCollection(string.Empty,
                "PrivateContact.LastName, PrivateContact.FirstName, PrivateContact.MiddleName",
                DojoMemberFlags.PrivateContact);
            foreach (DojoMember member in members)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = member.PrivateContact.ConstructName("L, F Mi.");
                item.Value = member.ID.ToString();
                comboParent.Items.Add(item);
            }

            DojoRankManager rankManager = new DojoRankManager();
            DojoRankCollection ranks =
                rankManager.GetCollection(string.Empty, "OrderNum", null);
            foreach (DojoRank rank in ranks)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = rank.Name;
                item.Value = rank.ID.ToString();
                comboRank.Items.Add(item);
            }

            DojoPromotionFlagManager promotionFlagManager = new DojoPromotionFlagManager();
            DojoPromotionFlagCollection promotionFlags = promotionFlagManager.GetCollection(string.Empty, string.Empty);
            foreach (DojoPromotionFlag promotionFlag in promotionFlags)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = promotionFlag.Name;
                item.Value = promotionFlag.ID.ToString();
                comboPromotionFlag.Items.Add(item);
            }

            foreach (string name in Enum.GetNames(typeof(RHAccountType)))
                ddAccountType.Items.Add(new ListItem(name, name));
        }
		
		protected void ok_Click(object sender, EventArgs e)
		{
            EnsureChildControls();

            Page.Validate();
            if (!Page.IsValid)
                return;

            if (Amns.Tessen.Utilities.HashBuilder.IsCompiling)
                return;

			if(memberID == 0)
			{
				member = new DojoMember();
				member.PrivateContact = new GreyFoxContact("kitTessen_Members_PrivateContacts");
			}
			else
			{
				member = new DojoMember(memberID);
			}

            member.PrivateContact.ParseName(tbName.Text);
            member.PrivateContact.Address1 = tbAddress1.Text;
            member.PrivateContact.IsBadAddress = cbBadAddress.Checked;
            member.PrivateContact.Address2 = tbAddress2.Text;
            member.PrivateContact.City = tbCity.Text;
            member.PrivateContact.StateProvince = tbStateProvince.Text;
            member.PrivateContact.PostalCode = tbPostalCode.Text;
            member.PrivateContact.Country = tbCountry.Text;
            member.PrivateContact.HomePhone = tbHomePhone.Text;
            member.PrivateContact.IsBadHomePhone = cbBadHomePhone.Checked;
            member.PrivateContact.WorkPhone = tbWorkPhone.Text;
            member.PrivateContact.IsBadWorkPhone = cbBadWorkPhone.Checked;
            member.PrivateContact.MobilePhone = tbMobilePhone.Text;
            member.PrivateContact.Pager = tbPager.Text;
            member.PrivateContact.IsBadMobilePhone = cbBadMobilePhone.Checked;
            member.PrivateContact.Email1 = tbEmail1.Text;
            member.PrivateContact.IsBadEmail = cbBadEmail.Checked;
            member.PrivateContact.Email2 = tbEmail2.Text;
            member.PrivateContact.Url = tbUrl.Text;
            member.PrivateContact.IsBadUrl = cbBadUrl.Checked;
			member.PrivateContact.BirthDate = calBirthDateP.SelectedDate;
			member.PrivateContact.MemoText = tbMemoText.Text;
			member.PrivateContact.Save();

			if(member.EmergencyContact == null)
				member.EmergencyContact = new GreyFoxContact("kitTessen_Members_EmergencyContacts");

			member.EmergencyContact.ParseName(tbEName.Text);
			member.EmergencyContact.ParseAddress(tbEAddress1.Text, tbEAddress2.Text, 
				tbECity.Text, tbEStateProvince.Text, tbEPostalCode.Text, tbECountry.Text);
			member.EmergencyContact.ParsePhones(tbEHomePhone.Text, tbEWorkPhone.Text,
				string.Empty, tbEPager.Text, tbEMobilePhone.Text);
			member.EmergencyContact.Email1 = tbEEmail1.Text;
			member.EmergencyContact.Email2 = tbEEmail2.Text;
			member.EmergencyContact.Url = tbEUrl.Text;
			member.EmergencyContact.Save();

            if (comboStudentType.SelectedItem != null)
                member.MemberType = DojoMemberType.NewPlaceHolder(int.Parse(comboStudentType.SelectedValue));
            else
                member.MemberType = null;

            if (comboMemberTypeTemplate.SelectedItem != null)
                member.MemberTypeTemplate = DojoMemberTypeTemplate.NewPlaceHolder(
                    int.Parse(comboMemberTypeTemplate.SelectedItem.Value));
            else
                member.MemberTypeTemplate = null;

            if (comboParent.SelectedItem != null)
                member.Parent = DojoMember.NewPlaceHolder(int.Parse(comboParent.SelectedValue));
            else
                member.Parent = null;

			member.MemberSince = calMemberDateP.SelectedDate;
			member.IsPrimaryOrgActive = cbActive.Checked;
			member.IsPastDue = cbIsPastDue.Checked;
			member.HasWaiver = cbSignedWaiver.Checked;
            if (comboRank.SelectedItem != null)
            {
                member.Rank = DojoRank.NewPlaceHolder(int.Parse(comboRank.SelectedValue));
            }
            else
            {
                member.Rank = null;
            }
			member.RankDate = calRankDateP.SelectedDate;
			member.IsInstructor = cbInstructor.Checked;
			member.AttendanceMessage = tbAttendanceMessage.Text;

            if (tbUserAccount.Text.Length > 0)
            {
                if (!MemberBuilder.UpdateUser(member, tbUserAccount.Text, true))
                {
                    lbUserAccount.Text = Localization.Strings.UserAccountInUse;
                    lbUserAccount.ForeColor = System.Drawing.Color.Red;
                    lbUserAccount.Visible = true;
                    tabstrip.SelectedTab = tabstrip.Tabs[2];
                    multipage.SelectedIndex = 2;
                    return;
                }
            }                       

            if (member.UserAccount != null)
            {
                member.UserAccount.IsDisabled = cbIsDisabled.Checked;
                member.UserAccount.Save();
                
                if (member.Customer == null)
                    member.Customer = TessenFactory.Customer(member);

                if (member.Customer.PrimaryAccount == null)
                    member.Customer.PrimaryAccount = new RHCustomerAccount();

                member.Customer.PrimaryAccount.AccountName = tbAccountName.Text;
                if (!tbAccountNumber.Text.StartsWith("xxxx"))
                    member.Customer.PrimaryAccount.EncryptAccountNumber(tbAccountNumber.Text);
                member.Customer.PrimaryAccount.AccountExpMonth = short.Parse(tbAccountExpMonth.Text);
                member.Customer.PrimaryAccount.AccountExpYear = short.Parse(tbAccountExpYear.Text);
                if (ddAccountType.SelectedValue != string.Empty)
                    member.Customer.PrimaryAccount.AccountType = (RHAccountType)
                        Enum.Parse(typeof(RHAccountType), ddAccountType.SelectedValue);
                member.Customer.PrimaryAccount.Customer = member.Customer; // Redundant...
                member.Customer.PrimaryAccount.Save();

                member.Customer.Save(); // Must save after the primary account!
            }  
            
			
			if(editOnAdd)
			{
				memberID = member.Save();
			}
			else
			{
				member.Save();
			}
			
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
				calBirthDateP.SelectedDate = DateTime.Parse("1/1/1980");
				tbMemoText.Text = string.Empty;

				tbEName.Text = string.Empty;
				tbEAddress1.Text = string.Empty;
				tbEAddress2.Text = string.Empty;
				tbECity.Text = string.Empty;
				tbEStateProvince.Text = string.Empty;
				tbEPostalCode.Text = string.Empty;
				tbECountry.Text = string.Empty;
				tbEHomePhone.Text = string.Empty;
				tbEWorkPhone.Text = string.Empty;
				tbEMobilePhone.Text = string.Empty;
				tbEPager.Text = string.Empty;
				tbEEmail1.Text = string.Empty;
				tbEEmail2.Text = string.Empty;
				tbEUrl.Text = string.Empty;

				comboStudentType.SelectedIndex = 0;
				calMemberDateP.SelectedDate = DateTime.Now;
				comboRank.SelectedIndex = 0;
				calRankDateP.SelectedDate = DateTime.Now;
				cbActive.Checked = false;
				cbIsPastDue.Checked = false;
				cbOrganizationActive.Checked = false;
				cbSignedWaiver.Checked = false;
				
				cbInstructor.Checked = false;
				tbAttendanceMessage.Text = "";
			}

            HashBuilder.Compile(typeof(DojoMember));
            
			OnUpdated(EventArgs.Empty);
		}

		protected void cancel_Click(object sender, EventArgs e)
		{
			this.OnCancelled(EventArgs.Empty);
		}

		protected void delete_Click(object sender, EventArgs e)
		{
			this.OnDeleteClicked(EventArgs.Empty);
		}

		#region Public Control Events

		public event EventHandler Cancelled;
		protected virtual void OnCancelled(EventArgs e)
		{
			if(Cancelled != null)
				Cancelled(this, e);
		}

		public event EventHandler Created;
		protected virtual void OnCreated(EventArgs e)
		{
			if(Created != null)
				Created(this, e);
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

		#endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EnsureChildControls(); // Necissary to catch events!
        }

		protected override void OnPreRender(EventArgs e)
		{
            if (HashBuilder.IsCompiling)
            {
                ltMessage.Text = string.Format(
                    Localization.Strings.HashCompileWarning,
                    HashBuilder.ItemNumber,
                    HashBuilder.ItemCount);
                btOk.Enabled = false;
            }
            else
            {
                ltMessage.Text = string.Format(
                    Localization.Strings.HashCompileReady,
                    HashBuilder.ItemNumber,
                    HashBuilder.ItemCount);
            }

            if (loadFlag)
            {
                tabstrip.SelectedTab = tabstrip.Tabs[0];

                if (memberID > 0)
                {
                    member = new DojoMember(memberID);
                    
                    headerText.Text = "Edit - " + member.PrivateContact.FullName;

                    tbName.Text = member.PrivateContact.FullName;
                    tbAddress1.Text = member.PrivateContact.Address1;
                    cbBadAddress.Checked = member.PrivateContact.IsBadAddress;
                    tbAddress2.Text = member.PrivateContact.Address2;
                    tbCity.Text = member.PrivateContact.City;
                    tbStateProvince.Text = member.PrivateContact.StateProvince;
                    tbPostalCode.Text = member.PrivateContact.PostalCode;
                    tbCountry.Text = member.PrivateContact.Country;
                    tbHomePhone.Text = member.PrivateContact.HomePhone;
                    cbBadHomePhone.Checked = member.PrivateContact.IsBadHomePhone;
                    tbWorkPhone.Text = member.PrivateContact.WorkPhone;
                    cbBadWorkPhone.Checked = member.PrivateContact.IsBadWorkPhone;
                    tbMobilePhone.Text = member.PrivateContact.MobilePhone;
                    cbBadMobilePhone.Checked = member.PrivateContact.IsBadMobilePhone;
                    tbPager.Text = member.PrivateContact.Pager;
                    tbEmail1.Text = member.PrivateContact.Email1;
                    cbBadEmail.Checked = member.PrivateContact.IsBadEmail;
                    tbEmail2.Text = member.PrivateContact.Email2;
                    tbUrl.Text = member.PrivateContact.Url;
                    cbBadUrl.Checked = member.PrivateContact.IsBadUrl;
					calBirthDateP.SelectedDate = member.PrivateContact.BirthDate;
					tbMemoText.Text = member.PrivateContact.MemoText;

					if(member.EmergencyContact != null)
					{
						tbEName.Text = member.EmergencyContact.FullName;
						tbEAddress1.Text = member.EmergencyContact.Address1;
						tbEAddress2.Text = member.EmergencyContact.Address2;
						tbECity.Text = member.EmergencyContact.City;
						tbEStateProvince.Text = member.EmergencyContact.StateProvince;
						tbEPostalCode.Text = member.EmergencyContact.PostalCode;
						tbECountry.Text = member.EmergencyContact.Country;
						tbEHomePhone.Text = member.EmergencyContact.HomePhone;
						tbEWorkPhone.Text = member.EmergencyContact.WorkPhone;
						tbEMobilePhone.Text = member.EmergencyContact.MobilePhone;
						tbEPager.Text = member.EmergencyContact.Pager;
						tbEEmail1.Text = member.EmergencyContact.Email1;
						tbEEmail2.Text = member.EmergencyContact.Email2;
						tbEUrl.Text = member.EmergencyContact.Url;
					}

                    if (member.Rank != null)
                    {
                        comboRank.Text = member.Rank.Name;
                        foreach (ComboBoxItem item in comboRank.Items)
                        {
                            if (item.Value == member.Rank.ID.ToString())
                            {
                                comboRank.SelectedItem = item;
                                break;
                            }
                        }
                    }
                    else
                    {
                        comboRank.Text = string.Empty;
                        comboRank.SelectedItem = null;
                    }

                    if (member.UserAccount != null)
                    {
                        tbUserAccount.Text = member.UserAccount.UserName;
                        cbIsDisabled.Checked = member.UserAccount.IsDisabled;
                    }

                    if (member.MemberType != null)
                    {
                        comboStudentType.Text = member.MemberType.Name;
                        foreach (ComboBoxItem item in comboStudentType.Items)
                        {
                            if (item.Value == member.MemberType.ID.ToString())
                            {
                                comboStudentType.SelectedItem = item;
                                break;
                            }
                        }
                    }
                    else
                    {
                        comboStudentType.Text = string.Empty;
                        comboRank.SelectedItem = null;
                    }

                    if (member.MemberTypeTemplate != null)
                    {
                        comboMemberTypeTemplate.Text = member.MemberTypeTemplate.Name;
                        foreach (ComboBoxItem item in comboMemberTypeTemplate.Items)
                        {
                            if (item.Value == member.MemberTypeTemplate.ID.ToString())
                            {
                                comboMemberTypeTemplate.SelectedItem = item;
                                break;
                            }
                        }
                    }
                    else
                    {
                        comboMemberTypeTemplate.Text = string.Empty;
                        comboMemberTypeTemplate.SelectedItem = null;
                    }

                    if (member.Parent != null)
                    {
                        comboParent.Text = member.Parent.PrivateContact.ConstructName("L, F Mi.");
                        foreach (ComboBoxItem item in comboParent.Items)
                        {
                            if (item.Value == member.Parent.ID.ToString())
                            {
                                comboParent.SelectedItem = item;
                                break;
                            }
                        }
                    }
                    else
                    {
                        comboParent.Text = string.Empty;
                        comboParent.SelectedItem = null;
                    }

					calRankDateP.SelectedDate = member.RankDate;
					calMemberDateP.SelectedDate = member.MemberSince;
                    tbMemberTypeTreeHash.Text = member.MemberTypeTreeHash;
                    tbAttendanceMessage.Text = member.AttendanceMessage;
                    cbActive.Checked = member.IsPrimaryOrgActive;
                    cbIsPastDue.Checked = member.IsPastDue;
                    cbSignedWaiver.Checked = member.HasWaiver;
                    cbInstructor.Checked = member.IsInstructor;

                    if (member.Customer != null && member.Customer.PrimaryAccount != null)
                    {
                        tbAccountName.Text = member.Customer.PrimaryAccount.AccountName;
                        tbAccountNumber.Text = member.Customer.PrimaryAccount.DecryptAccountNumber();
                        tbAccountExpMonth.Text = member.Customer.PrimaryAccount.AccountExpMonth.ToString();
                        tbAccountExpYear.Text = member.Customer.PrimaryAccount.AccountExpYear.ToString();
                        foreach (ListItem item in ddAccountType.Items)
                            item.Selected = member.Customer.PrimaryAccount.AccountType.ToString() == item.Value;
                    }
                    else
                    {
                        tbAccountExpMonth.Text = "1";
                        tbAccountExpYear.Text = "2000";
                    }

                }
                else
	            {
					calBirthDateP.SelectedDate = new DateTime(1980, 1, 1);
					calMemberDateP.SelectedDate = DateTime.Now;
					calRankDateP.SelectedDate = DateTime.Now;

                    member = new DojoMember();
                    headerText.Text = "Add";
                }          
			}
		}

		#region ViewState Methods

		protected override void LoadViewState(object savedState) 
		{
			if(savedState != null)
			{
				// Load State from the array of objects that was saved at ;
				// SavedViewState.
				object[] myState = (object[])savedState;
				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					memberID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{			
			object baseState = base.SaveViewState();
			object[] myState = new object[5];
			myState[0] = baseState;
			myState[1] = memberID;
			return myState;
		}

		#endregion

	}
}
