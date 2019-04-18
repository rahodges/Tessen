using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using ComponentArt.Web.UI;
using System.Web.UI.WebControls;
using Amns.GreyFox.Security;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen.Utilities;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoMemberTypeTemplate.
	/// </summary>
	[ToolboxData("<{0}:DojoMemberTypeTemplateEditor runat=server></{0}:DojoMemberTypeTemplateEditor>")]
	public class DojoMemberTypeTemplateEditor : System.Web.UI.Control, INamingContainer
	{
		private int dojoMemberTypeTemplateID;
		private DojoMemberTypeTemplate obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;
        private string cssClass;

		protected ComponentArt.Web.UI.TabStrip tabstrip;
		protected ComponentArt.Web.UI.MultiPage multipage;
		protected LiteralControl headerText;

        public string CssClass { get { return cssClass; } set { cssClass = value; } }

		#region Private Control Fields for _system Folder

        //protected ComponentArt.Web.UI.PageView _systemView;
        //private Literal ltCreateDate;
        //private Literal ltModifyDate;

		#endregion

		#region Private Control Fields for General Folder

		protected ComponentArt.Web.UI.PageView GeneralView;
		private TextBox tbName;
        private RequiredFieldValidator reqName;
		private TextBox tbDescription;
		private TextBox tbOrderNum;
        private RegularExpressionValidator revOrderNum;
		private RequiredFieldValidator reqOrderNum;
		private ComponentArt.Web.UI.ComboBox comboParent;
		private ComponentArt.Web.UI.ComboBox comboMemberType;
		private TextBox tbMemberTypeTreeHash;
        private LiteralControl ltMessage;

		#endregion

		#region Private Control Fields for Initialization Folder

		protected ComponentArt.Web.UI.PageView InitializationView;
		private ComponentArt.Web.UI.ComboBox comboInitialRank;
		private ComponentArt.Web.UI.ComboBox comboInitialRole;
		private TextBox tbInitialEmailFrom;
		private TextBox tbInitialEmailBody;

		#endregion

		#region Private Control Fields for Access_Features Folder

		protected ComponentArt.Web.UI.PageView SecurityView;
		private CheckBox cbAllowGuestPurchase;
		private CheckBox cbAllowPurchase;
		private CheckBox cbAllowRenewal;
		private CheckBox cbAllowAutoRenewal;

		#endregion

		#region Private Control Fields for Requirements Folder

		protected ComponentArt.Web.UI.PageView RequirementsView;
		private TextBox tbAgeYearsMax;
        private RequiredFieldValidator reqAgeYearsMax;
		private RegularExpressionValidator revAgeYearsMax;
		private TextBox tbAgeYearsMin;
        private RequiredFieldValidator reqAgeYearsMin;
		private RegularExpressionValidator revAgeYearsMin;
		private TextBox tbMemberForMin;
        private RequiredFieldValidator reqMemberForMin;
		private RegularExpressionValidator revMemberForMin;
		private TextBox tbMemberForMax;
        private RequiredFieldValidator reqMemberForMax;
		private RegularExpressionValidator revMemberForMax;
		private ComponentArt.Web.UI.ComboBox comboRankMin;
		private ComponentArt.Web.UI.ComboBox comboRankMax;

		#endregion

		#region Private Control Fields for Membership_Templates Folder

		protected ComponentArt.Web.UI.PageView MembershipsView;
		private ComponentArt.Web.UI.ComboBox comboMembershipTemplate1;
		private ComponentArt.Web.UI.ComboBox comboMembershipTemplate2;
		private ComponentArt.Web.UI.ComboBox comboMembershipTemplate3;
		private ComponentArt.Web.UI.ComboBox comboMembershipTemplate4;
		private ComponentArt.Web.UI.ComboBox comboMembershipTemplate5;

		#endregion

		private Button btOk;
		private Button btCancel;
		private Button btDelete;

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
				loadFlag = true;
				dojoMemberTypeTemplateID = value;
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
            Panel content = new Panel();
			content.CssClass = cssClass;
			this.Controls.Add(content);

            Panel header = new Panel();
            header.CssClass = "pHead";
            content.Controls.Add(header);

            headerText = new LiteralControl();
            header.Controls.Add(headerText);

            #region MultiPage

            multipage = new ComponentArt.Web.UI.MultiPage();
            multipage.ID = this.ID + "_MultiPage";
            multipage.CssClass = "MultiPage";

            if (multipage.PageViews.Count == 0)
            {
                GeneralView = new ComponentArt.Web.UI.PageView();
                GeneralView.ID = "GeneralView";
                GeneralView.CssClass = "PageContent";
                multipage.PageViews.Add(GeneralView);

                InitializationView = new ComponentArt.Web.UI.PageView();
                InitializationView.ID = "InitializationView";
                InitializationView.CssClass = "PageContent";
                multipage.PageViews.Add(InitializationView);

                SecurityView = new ComponentArt.Web.UI.PageView();
                SecurityView.ID = "SecurityView";
                SecurityView.CssClass = "PageContent";
                multipage.PageViews.Add(SecurityView);

                RequirementsView = new ComponentArt.Web.UI.PageView();
                RequirementsView.ID = "RequirementsView";
                RequirementsView.CssClass = "PageContent";
                multipage.PageViews.Add(RequirementsView);

                MembershipsView = new ComponentArt.Web.UI.PageView();
                MembershipsView.ID = "MembershipsView";
                MembershipsView.CssClass = "PageContent";
                multipage.PageViews.Add(MembershipsView);
            }

            #endregion

			#region Tab Strip

			tabstrip = new ComponentArt.Web.UI.TabStrip();

			// Create the DefaultTabLook instance and add it to the ItemLooks collection
			ComponentArt.Web.UI.ItemLook defaultTabLook = new ComponentArt.Web.UI.ItemLook();
			defaultTabLook.LookId = "DefaultTabLook";
			defaultTabLook.CssClass = "DefaultTab";
			defaultTabLook.HoverCssClass = "DefaultTabHover";
			defaultTabLook.LabelPaddingLeft = Unit.Parse("10");
			defaultTabLook.LabelPaddingRight = Unit.Parse("10");
			defaultTabLook.LabelPaddingTop = Unit.Parse("5");
			defaultTabLook.LabelPaddingBottom = Unit.Parse("4");
			defaultTabLook.LeftIconUrl = "tab_left_icon.gif";
			defaultTabLook.RightIconUrl = "tab_right_icon.gif";
			defaultTabLook.HoverLeftIconUrl = "hover_tab_left_icon.gif";
			defaultTabLook.HoverRightIconUrl = "hover_tab_right_icon.gif";
			defaultTabLook.LeftIconWidth = Unit.Parse("3");
			defaultTabLook.LeftIconHeight = Unit.Parse("21");
			defaultTabLook.RightIconWidth = Unit.Parse("3");
			defaultTabLook.RightIconHeight = Unit.Parse("21");
			tabstrip.ItemLooks.Add(defaultTabLook);

			// Create the SelectedTabLook instance and add it to the ItemLooks collection
			ComponentArt.Web.UI.ItemLook selectedTabLook = new ComponentArt.Web.UI.ItemLook();
			selectedTabLook.LookId = "SelectedTabLook";
			selectedTabLook.CssClass = "SelectedTab";
			selectedTabLook.LabelPaddingLeft = Unit.Parse("10");
			selectedTabLook.LabelPaddingRight = Unit.Parse("10");
			selectedTabLook.LabelPaddingTop = Unit.Parse("5");
			selectedTabLook.LabelPaddingBottom = Unit.Parse("4");
			selectedTabLook.LeftIconUrl = "selected_tab_left_icon.gif";
			selectedTabLook.RightIconUrl = "selected_tab_right_icon.gif";
			selectedTabLook.LeftIconWidth = Unit.Parse("3");
			selectedTabLook.LeftIconHeight = Unit.Parse("21");
			selectedTabLook.RightIconWidth = Unit.Parse("3");
			selectedTabLook.RightIconHeight = Unit.Parse("21");
			tabstrip.ItemLooks.Add(selectedTabLook);

			tabstrip.ID = this.ID + "_TabStrip";
			tabstrip.CssClass = "TopGroup";
			tabstrip.DefaultItemLookId = "DefaultTabLook";
			tabstrip.DefaultSelectedItemLookId = "SelectedTabLook";
			tabstrip.DefaultGroupTabSpacing = 1;
			tabstrip.ImagesBaseUrl = "tabstrip_images/";
			tabstrip.MultiPageId = this.ID + "_MultiPage";
			
            if (tabstrip.Tabs.Count == 0)
            {
                TabStripTab GeneralTab = new TabStripTab();
                GeneralTab.Text = Localization.Strings.General;
                GeneralTab.PageViewId = GeneralView.ID;
                tabstrip.Tabs.Add(GeneralTab);

                TabStripTab Membership_TemplatesTab = new TabStripTab();
                Membership_TemplatesTab.Text =
                    Localization.Strings.Memberships;
                Membership_TemplatesTab.PageViewId = MembershipsView.ID;
                tabstrip.Tabs.Add(Membership_TemplatesTab);

                TabStripTab InitializationTab = new TabStripTab();
                InitializationTab.Text = Localization.Strings.NewMembers;
                InitializationTab.PageViewId = InitializationView.ID;
                tabstrip.Tabs.Add(InitializationTab);

                TabStripTab RequirementsTab = new TabStripTab();
                RequirementsTab.Text = Localization.Strings.Requirements;
                RequirementsTab.PageViewId = RequirementsView.ID;
                tabstrip.Tabs.Add(RequirementsTab);

                TabStripTab SecurityTab = new TabStripTab();
                SecurityTab.Text =
                    Localization.Strings.Security;
                SecurityTab.PageViewId = SecurityView.ID;
                tabstrip.Tabs.Add(SecurityTab);
            }

			#endregion

			#region Child Controls for _system Folder

            //_systemView = new ComponentArt.Web.UI.PageView();
            //_systemView.CssClass = "PageContent";
            //multipage.PageViews.Add(_systemView);

            //TabStripTab _systemTab = new TabStripTab();
            //_systemTab.Text = "_system";
            //_systemTab.PageViewId = _systemView.ID;
            //tabstrip.Tabs.Add(_systemTab);

            //ltCreateDate = new Literal();
            //ltCreateDate.EnableViewState = false;
            //registerControl(_systemView, "CreateDate", ltCreateDate);

            //ltModifyDate = new Literal();
            //ltModifyDate.EnableViewState = false;
            //registerControl(_systemView, "ModifyDate", ltModifyDate);

			#endregion

            content.Controls.Add(tabstrip);
            content.Controls.Add(multipage);

			#region Child Controls for General Folder

			tbName = new TextBox();
            tbName.ID = "Name";
            tbName.Width = Unit.Pixel(250);
            reqName = new RequiredFieldValidator();
            reqName.ID = "ReqName";
            reqName.ControlToValidate = tbName.ID;
            reqName.ErrorMessage = Localization.Strings.Required;
            reqName.Display = ValidatorDisplay.Dynamic;
			registerControl(GeneralView, 
                Localization.Strings.Name, tbName, reqName);

			tbDescription = new TextBox();
            tbDescription.ID = "Description";
            tbDescription.Width = Unit.Pixel(250);
            tbDescription.TextMode = TextBoxMode.MultiLine;
            tbDescription.Rows = 5;
			tbDescription.EnableViewState = false;
			registerControl(GeneralView, 
                Localization.Strings.Description, tbDescription);

			tbOrderNum = new TextBox();
			tbOrderNum.ID = this.ID + "OrderNum";
			tbOrderNum.EnableViewState = false;
			revOrderNum = new RegularExpressionValidator();
            revOrderNum.ID = "RevOrderNum";
			revOrderNum.ControlToValidate = tbOrderNum.ID;
			revOrderNum.ValidationExpression = "^(\\+|-)?\\d+$";
			revOrderNum.ErrorMessage = Localization.Strings.IllegalValue;
			revOrderNum.Display = ValidatorDisplay.Dynamic;
            revOrderNum.SetFocusOnError = true;
            reqOrderNum = new RequiredFieldValidator();
            reqOrderNum.ID = "ReqOrderNum";
            reqOrderNum.ErrorMessage = Localization.Strings.Required;
            reqOrderNum.ControlToValidate = tbOrderNum.ID;
            reqOrderNum.Display = ValidatorDisplay.Dynamic;
			registerControl(GeneralView, 
                Localization.Strings.OrderNum, tbOrderNum, revOrderNum, reqOrderNum);
                        
			comboParent = new ComponentArt.Web.UI.ComboBox();
            comboParent.ID = "ComboParent";
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
			registerControl(GeneralView, 
                Localization.Strings.ParentTemplate, comboParent);

			comboMemberType = new ComponentArt.Web.UI.ComboBox();
            comboMemberType.ID = "ComboMemberType";
			comboMemberType.CssClass = "comboBox";
			comboMemberType.HoverCssClass = "comboBoxHover";
			comboMemberType.FocusedCssClass = "comboBoxHover";
			comboMemberType.TextBoxCssClass = "comboTextBox";
			comboMemberType.DropDownCssClass = "comboDropDown";
			comboMemberType.ItemCssClass = "comboItem";
			comboMemberType.ItemHoverCssClass = "comboItemHover";
			comboMemberType.SelectedItemCssClass = "comboItemHover";
            comboMemberType.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboMemberType.DropImageUrl = "combobox_images/drop.gif";
			comboMemberType.Width = Unit.Pixel(300);
			registerControl(GeneralView, 
                Localization.Strings.PrimaryMemberType, comboMemberType);
            registerControl(GeneralView,
                "&nbsp;<br>&nbsp;<br>&nbsp;", new LiteralControl(
                Localization.Strings.PrimaryMemberTypeNote));

			tbMemberTypeTreeHash = new TextBox();
            tbMemberTypeTreeHash.ID = "MemberTypeTreeHash";
			tbMemberTypeTreeHash.EnableViewState = false;
            tbMemberTypeTreeHash.Enabled = false;
			registerControl(GeneralView, 
                Localization.Strings.TreeHash, tbMemberTypeTreeHash);

            ltMessage = new LiteralControl();
            registerControl(GeneralView, "&nbsp;", ltMessage);

			#endregion

			#region Child Controls for Initialization Folder

			comboInitialRank = new ComponentArt.Web.UI.ComboBox();
            comboInitialRank.ID = "ComboInitialRank";
			comboInitialRank.CssClass = "comboBox";
			comboInitialRank.HoverCssClass = "comboBoxHover";
			comboInitialRank.FocusedCssClass = "comboBoxHover";
			comboInitialRank.TextBoxCssClass = "comboTextBox";
			comboInitialRank.DropDownCssClass = "comboDropDown";
			comboInitialRank.ItemCssClass = "comboItem";
			comboInitialRank.ItemHoverCssClass = "comboItemHover";
			comboInitialRank.SelectedItemCssClass = "comboItemHover";
            comboInitialRank.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboInitialRank.DropImageUrl = "combobox_images/drop.gif";
			comboInitialRank.Width = Unit.Pixel(300);
			registerControl(InitializationView, 
                Localization.Strings.InitialRank, 
                comboInitialRank);
            registerControl(InitializationView,
                "&nbsp;", new LiteralControl(
                Localization.Strings.InitialRankNote));

			comboInitialRole = new ComponentArt.Web.UI.ComboBox();
            comboInitialRole.ID = "ComboInitialRole";
			comboInitialRole.CssClass = "comboBox";
			comboInitialRole.HoverCssClass = "comboBoxHover";
			comboInitialRole.FocusedCssClass = "comboBoxHover";
			comboInitialRole.TextBoxCssClass = "comboTextBox";
			comboInitialRole.DropDownCssClass = "comboDropDown";
			comboInitialRole.ItemCssClass = "comboItem";
			comboInitialRole.ItemHoverCssClass = "comboItemHover";
			comboInitialRole.SelectedItemCssClass = "comboItemHover";
            comboInitialRole.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboInitialRole.DropImageUrl = "combobox_images/drop.gif";
			comboInitialRole.Width = Unit.Pixel(300);
			registerControl(InitializationView, 
                Localization.Strings.InitialRole, 
                comboInitialRole);
            registerControl(InitializationView,
                "&nbsp;", new LiteralControl(
                Localization.Strings.InitialRoleNote));

			tbInitialEmailFrom = new TextBox();
            tbInitialEmailFrom.ID = "InitialEmailFrom";
			tbInitialEmailFrom.EnableViewState = false;
            tbInitialEmailFrom.Width = Unit.Pixel(350);
			registerControl(InitializationView, 
                Localization.Strings.InitialEmailFrom,
                tbInitialEmailFrom);

			tbInitialEmailBody = new TextBox();
            tbInitialEmailBody.ID = "InitialEmailBody";
			tbInitialEmailBody.EnableViewState = false;
            tbInitialEmailBody.Width = Unit.Pixel(350);
            tbInitialEmailBody.TextMode = TextBoxMode.MultiLine;
            tbInitialEmailBody.Rows = 15;
			registerControl(InitializationView, 
                Localization.Strings.InitialEmailBody, 
                tbInitialEmailBody);

			#endregion

			#region Child Controls for Access Features Folder

			cbAllowGuestPurchase = new CheckBox();
            cbAllowGuestPurchase.ID = "AllowGuestPurchase";
			cbAllowGuestPurchase.EnableViewState = false;
			registerControl(SecurityView, 
                Localization.Strings.AllowGuestPurchase,
                cbAllowGuestPurchase);

			cbAllowPurchase = new CheckBox();
            cbAllowPurchase.ID = "AllowPurchase";
			cbAllowPurchase.EnableViewState = false;
			registerControl(SecurityView, 
                Localization.Strings.AllowMemberPurchase, 
                cbAllowPurchase);

			cbAllowRenewal = new CheckBox();
            cbAllowRenewal.ID = "AllowRenewal";
			cbAllowRenewal.EnableViewState = false;
			registerControl(SecurityView, 
                Localization.Strings.AllowMemberRenewal, 
                cbAllowRenewal);

			cbAllowAutoRenewal = new CheckBox();
            cbAllowAutoRenewal.ID = "AllowAutoRenewal";
			cbAllowAutoRenewal.EnableViewState = false;
			registerControl(SecurityView, 
                Localization.Strings.AllowAutoRenewal, 
                cbAllowAutoRenewal);

			#endregion

			#region Child Controls for Requirements Folder

			tbAgeYearsMax = new TextBox();
			tbAgeYearsMax.ID = "AgeYearsMax";
            tbAgeYearsMax.Width = Unit.Pixel(15);
            tbAgeYearsMax.MaxLength = 2;
			tbAgeYearsMax.EnableViewState = false;
			revAgeYearsMax = new RegularExpressionValidator();
            revAgeYearsMax.ID = "RevAgeYearsMax";
			revAgeYearsMax.ControlToValidate = tbAgeYearsMax.ID;
			revAgeYearsMax.ValidationExpression = "^(\\+|-)?\\d+$";
			revAgeYearsMax.ErrorMessage = Localization.Strings.IllegalValue;
            revAgeYearsMax.SetFocusOnError = true;
            revAgeYearsMax.EnableClientScript = true;
            reqAgeYearsMax = new RequiredFieldValidator();
            reqAgeYearsMax.ID = "ReqAgeYearsMax";
            reqAgeYearsMax.ControlToValidate = tbAgeYearsMax.ID;
            reqAgeYearsMax.ErrorMessage = Localization.Strings.Required;
            reqAgeYearsMax.Display = ValidatorDisplay.Dynamic;
			registerControl(RequirementsView, 
                Localization.Strings.MaximumAge, tbAgeYearsMax, 
                new LiteralControl(Localization.Strings.YearsOld),
                revAgeYearsMax, reqAgeYearsMax);

			tbAgeYearsMin = new TextBox();
			tbAgeYearsMin.ID = "AgeYearsMin";
            tbAgeYearsMin.Width = Unit.Pixel(15);
            tbAgeYearsMin.MaxLength = 2;
			tbAgeYearsMin.EnableViewState = false;
			revAgeYearsMin = new RegularExpressionValidator();
            revAgeYearsMin.ID = "RevAgeYearsMin";
			revAgeYearsMin.ControlToValidate = tbAgeYearsMin.ID;
			revAgeYearsMin.ValidationExpression = "^(\\+|-)?\\d+$";
			revAgeYearsMin.ErrorMessage = Localization.Strings.IllegalValue;
			revAgeYearsMin.Display = ValidatorDisplay.Dynamic;
            revAgeYearsMin.SetFocusOnError = true;
            reqAgeYearsMin = new RequiredFieldValidator();
            reqAgeYearsMin.ID = "ReqAgeYearsMin";
            reqAgeYearsMin.ControlToValidate = tbAgeYearsMin.ID;
            reqAgeYearsMin.ErrorMessage = Localization.Strings.Required;
            reqAgeYearsMin.Display = ValidatorDisplay.Dynamic;
			RequirementsView.Controls.Add(revAgeYearsMin);
            registerControl(RequirementsView,
                Localization.Strings.MinimumAge, tbAgeYearsMin,
                new LiteralControl(Localization.Strings.YearsOld),
                revAgeYearsMin, reqAgeYearsMin);

			tbMemberForMin = new TextBox();
			tbMemberForMin.ID = "MemberForMin";
            tbMemberForMin.Width = Unit.Pixel(15);
            tbMemberForMin.MaxLength = 2;
			tbMemberForMin.EnableViewState = false;
			revMemberForMin = new RegularExpressionValidator();
            revMemberForMin.ID = "RevMemberForMin";
			revMemberForMin.ControlToValidate = tbMemberForMin.ID;
			revMemberForMin.ValidationExpression = "^(\\+|-)?\\d+$";
            revMemberForMin.ErrorMessage = Localization.Strings.IllegalValue;
			revMemberForMin.Display = ValidatorDisplay.Dynamic;
            revMemberForMin.SetFocusOnError = true;
            reqMemberForMin = new RequiredFieldValidator();
            reqMemberForMin.ID = "ReqMemberForMin";
            reqMemberForMin.ControlToValidate = tbMemberForMin.ID;
            reqMemberForMin.ErrorMessage = Localization.Strings.Required;
            reqMemberForMin.Display = ValidatorDisplay.Dynamic;
			RequirementsView.Controls.Add(revMemberForMin);
			registerControl(RequirementsView, 
                Localization.Strings.MinimumMemberFor, tbMemberForMin, 
                new LiteralControl(Localization.Strings.Years),
                revMemberForMin, reqMemberForMin);

			tbMemberForMax = new TextBox();
			tbMemberForMax.ID = "MemberForMax";
            tbMemberForMax.Width = Unit.Pixel(15);
            tbMemberForMax.MaxLength = 2;
			tbMemberForMax.EnableViewState = false;
			revMemberForMax = new RegularExpressionValidator();
            revMemberForMax.ID = "RevMemberForMax";
			revMemberForMax.ControlToValidate = tbMemberForMax.ID;
			revMemberForMax.ValidationExpression = "^(\\+|-)?\\d+$";
			revMemberForMax.ErrorMessage = "*";
			revMemberForMax.Display = ValidatorDisplay.Dynamic;
            revMemberForMax.SetFocusOnError = true;
            reqMemberForMax = new RequiredFieldValidator();
            reqMemberForMax.ID = "ReqMemberForMax";
            reqMemberForMax.ControlToValidate = tbMemberForMax.ID;
            reqMemberForMax.ErrorMessage = Localization.Strings.IllegalValue;
            reqMemberForMax.Display = ValidatorDisplay.Dynamic;
			RequirementsView.Controls.Add(revMemberForMax);
			registerControl(RequirementsView, 
                Localization.Strings.MaximumMemberFor,
                tbMemberForMax, 
                new LiteralControl(Localization.Strings.Years),
                revMemberForMax, reqMemberForMax);

            Page.Validators.Add(revOrderNum);

			comboRankMin = new ComponentArt.Web.UI.ComboBox();
            comboRankMin.ID = "ComboRankMin";
			comboRankMin.CssClass = "comboBox";
			comboRankMin.HoverCssClass = "comboBoxHover";
			comboRankMin.FocusedCssClass = "comboBoxHover";
			comboRankMin.TextBoxCssClass = "comboTextBox";
			comboRankMin.DropDownCssClass = "comboDropDown";
			comboRankMin.ItemCssClass = "comboItem";
			comboRankMin.ItemHoverCssClass = "comboItemHover";
			comboRankMin.SelectedItemCssClass = "comboItemHover";
            comboRankMin.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboRankMin.DropImageUrl = "combobox_images/drop.gif";
			comboRankMin.Width = Unit.Pixel(300);
			registerControl(RequirementsView, 
                Localization.Strings.MinimumRank, comboRankMin);

			comboRankMax = new ComponentArt.Web.UI.ComboBox();
            comboRankMax.ID = "ComboRankMax";
			comboRankMax.CssClass = "comboBox";
			comboRankMax.HoverCssClass = "comboBoxHover";
			comboRankMax.FocusedCssClass = "comboBoxHover";
			comboRankMax.TextBoxCssClass = "comboTextBox";
			comboRankMax.DropDownCssClass = "comboDropDown";
			comboRankMax.ItemCssClass = "comboItem";
			comboRankMax.ItemHoverCssClass = "comboItemHover";
			comboRankMax.SelectedItemCssClass = "comboItemHover";
            comboRankMax.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboRankMax.DropImageUrl = "combobox_images/drop.gif";
			comboRankMax.Width = Unit.Pixel(300);
			registerControl(RequirementsView, 
                Localization.Strings.MaximumRank, comboRankMax);

			#endregion

			#region Child Controls for Membership Templates Folder

			comboMembershipTemplate1 = new ComponentArt.Web.UI.ComboBox();
            comboMembershipTemplate1.ID = "ComboMembershipTemplate1";
			comboMembershipTemplate1.CssClass = "comboBox";
			comboMembershipTemplate1.HoverCssClass = "comboBoxHover";
			comboMembershipTemplate1.FocusedCssClass = "comboBoxHover";
			comboMembershipTemplate1.TextBoxCssClass = "comboTextBox";
			comboMembershipTemplate1.DropDownCssClass = "comboDropDown";
			comboMembershipTemplate1.ItemCssClass = "comboItem";
			comboMembershipTemplate1.ItemHoverCssClass = "comboItemHover";
			comboMembershipTemplate1.SelectedItemCssClass = "comboItemHover";
            comboMembershipTemplate1.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboMembershipTemplate1.DropImageUrl = "combobox_images/drop.gif";
			comboMembershipTemplate1.Width = Unit.Pixel(300);
            registerControl(MembershipsView,
                Localization.Strings.MembershipTemplate1,
                comboMembershipTemplate1);

			comboMembershipTemplate2 = new ComponentArt.Web.UI.ComboBox();
            comboMembershipTemplate2.ID = "ComboMembershipTemplate2";
			comboMembershipTemplate2.CssClass = "comboBox";
			comboMembershipTemplate2.HoverCssClass = "comboBoxHover";
			comboMembershipTemplate2.FocusedCssClass = "comboBoxHover";
			comboMembershipTemplate2.TextBoxCssClass = "comboTextBox";
			comboMembershipTemplate2.DropDownCssClass = "comboDropDown";
			comboMembershipTemplate2.ItemCssClass = "comboItem";
			comboMembershipTemplate2.ItemHoverCssClass = "comboItemHover";
			comboMembershipTemplate2.SelectedItemCssClass = "comboItemHover";
            comboMembershipTemplate2.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboMembershipTemplate2.DropImageUrl = "combobox_images/drop.gif";
			comboMembershipTemplate2.Width = Unit.Pixel(300);
            registerControl(MembershipsView,
                Localization.Strings.MembershipTemplate2,
                comboMembershipTemplate2);

			comboMembershipTemplate3 = new ComponentArt.Web.UI.ComboBox();
            comboMembershipTemplate3.ID = "ComboMembershipTemplate3";
			comboMembershipTemplate3.CssClass = "comboBox";
			comboMembershipTemplate3.HoverCssClass = "comboBoxHover";
			comboMembershipTemplate3.FocusedCssClass = "comboBoxHover";
			comboMembershipTemplate3.TextBoxCssClass = "comboTextBox";
			comboMembershipTemplate3.DropDownCssClass = "comboDropDown";
			comboMembershipTemplate3.ItemCssClass = "comboItem";
			comboMembershipTemplate3.ItemHoverCssClass = "comboItemHover";
			comboMembershipTemplate3.SelectedItemCssClass = "comboItemHover";
            comboMembershipTemplate3.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboMembershipTemplate3.DropImageUrl = "combobox_images/drop.gif";
			comboMembershipTemplate3.Width = Unit.Pixel(300);
            registerControl(MembershipsView,
                Localization.Strings.MembershipTemplate3, 
                comboMembershipTemplate3);

			comboMembershipTemplate4 = new ComponentArt.Web.UI.ComboBox();
            comboMembershipTemplate4.ID = "ComboMembershipTemplate4";
			comboMembershipTemplate4.CssClass = "comboBox";
			comboMembershipTemplate4.HoverCssClass = "comboBoxHover";
			comboMembershipTemplate4.FocusedCssClass = "comboBoxHover";
			comboMembershipTemplate4.TextBoxCssClass = "comboTextBox";
			comboMembershipTemplate4.DropDownCssClass = "comboDropDown";
			comboMembershipTemplate4.ItemCssClass = "comboItem";
			comboMembershipTemplate4.ItemHoverCssClass = "comboItemHover";
			comboMembershipTemplate4.SelectedItemCssClass = "comboItemHover";
            comboMembershipTemplate4.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboMembershipTemplate4.DropImageUrl = "combobox_images/drop.gif";
			comboMembershipTemplate4.Width = Unit.Pixel(300);
            registerControl(MembershipsView,
                Localization.Strings.MembershipTemplate4,
                comboMembershipTemplate4);

			comboMembershipTemplate5 = new ComponentArt.Web.UI.ComboBox();
            comboMembershipTemplate5.ID = "ComboMembershipTemplate5";
			comboMembershipTemplate5.CssClass = "comboBox";
			comboMembershipTemplate5.HoverCssClass = "comboBoxHover";
			comboMembershipTemplate5.FocusedCssClass = "comboBoxHover";
			comboMembershipTemplate5.TextBoxCssClass = "comboTextBox";
			comboMembershipTemplate5.DropDownCssClass = "comboDropDown";
			comboMembershipTemplate5.ItemCssClass = "comboItem";
			comboMembershipTemplate5.ItemHoverCssClass = "comboItemHover";
			comboMembershipTemplate5.SelectedItemCssClass = "comboItemHover";
            comboMembershipTemplate5.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboMembershipTemplate5.DropImageUrl = "combobox_images/drop.gif";
			comboMembershipTemplate5.Width = Unit.Pixel(300);
			registerControl(MembershipsView, 
                Localization.Strings.MembershipTemplate5,
                comboMembershipTemplate5);

			#endregion
                        
			Panel buttons = new Panel();
			buttons.CssClass = "pButtons";
			content.Controls.Add(buttons);

			btOk = new Button();
            btOk.ID = "btOK";
			btOk.Text = Localization.Strings.OK;
			btOk.Width = Unit.Pixel(72);
			btOk.Click += new EventHandler(ok_Click);
            btOk.CausesValidation = true;
			buttons.Controls.Add(btOk);

			btCancel = new Button();
            btCancel.ID = "btCancel";
			btCancel.Text = Localization.Strings.Cancel;
			btCancel.Width = Unit.Pixel(72);
			btCancel.CausesValidation = false;
			btCancel.Click += new EventHandler(cancel_Click);
			buttons.Controls.Add(btCancel);

			btDelete = new Button();
            btDelete.ID = "btDelete";
			btDelete.Text = Localization.Strings.Delete;
			btDelete.Width = Unit.Pixel(72);
			btDelete.Click += new EventHandler(delete_Click);
			buttons.Controls.Add(btDelete);

			bind();
			ChildControlsCreated = true;
		}

		private void registerControl(ComponentArt.Web.UI.PageView pageView,
		string caption,
		params Control[] controls)
		{
			ControlCollection pageViewControls = pageView.Controls;
			pageViewControls.Add(new LiteralControl("<div class=\"inputrow\">"));
			pageViewControls.Add(new LiteralControl("<span class=\"inputlabel\">"));
			pageViewControls.Add(new LiteralControl(caption));
			pageViewControls.Add(new LiteralControl("</span><span class=\"inputfield\">"));
			foreach(Control control in controls)
				pageViewControls.Add(control);
			pageViewControls.Add(new LiteralControl("</span></div>"));
		}

		private void bind()
		{
			#region Bind General Child Data

            if (comboParent.Items.Count == 0)
            {
                DojoMemberTypeTemplateManager parentManager =
                    new DojoMemberTypeTemplateManager();
                DojoMemberTypeTemplateCollection parentCollection =
                    parentManager.GetCollection(string.Empty, string.Empty, null);
                foreach (DojoMemberTypeTemplate parent in parentCollection)
                {
                    addComboItem(comboParent, parent.Name, parent.ID.ToString());
                }
            }

            if (comboMemberType.Items.Count == 0)
            {
                DojoMemberTypeManager memberTypeManager = new DojoMemberTypeManager();
                DojoMemberTypeCollection memberTypeCollection =
                    memberTypeManager.GetCollection(string.Empty, string.Empty);
                foreach (DojoMemberType memberType in memberTypeCollection)
                {
                    addComboItem(comboMemberType, memberType.Name, memberType.ID.ToString());
                }
            }

			#endregion

			#region Bind Initialization Child Data

            // Initial Rank is bound in requirements

            if (comboInitialRole.Items.Count == 0)
            {
                GreyFoxRoleManager initialRoleManager = new GreyFoxRoleManager();
                GreyFoxRoleCollection initialRoleCollection =
                    initialRoleManager.GetCollection(string.Empty, string.Empty);
                foreach (GreyFoxRole initialRole in initialRoleCollection)
                {
                    addComboItem(comboInitialRole, initialRole.Name, initialRole.ID.ToString());
                }
            }

			#endregion

			#region Bind Requirements Child Data

            if (comboRankMin.Items.Count == 0)
            {
                DojoRankManager rankManager = new DojoRankManager();
                DojoRankCollection rankCollection =
                    rankManager.GetCollection(string.Empty, string.Empty, null);
                foreach (DojoRank rank in rankCollection)
                {
                    addComboItem(comboRankMin, rank.Name, rank.ID.ToString());
                    addComboItem(comboRankMax, rank.Name, rank.ID.ToString());
                    addComboItem(comboInitialRank, rank.Name, rank.ID.ToString());
                }
            }

			#endregion

			#region Bind Membership Templates Child Data

            if (comboMembershipTemplate1.Items.Count == 0)
            {
                DojoMembershipTemplateManager membershipTemplateManager =
                    new DojoMembershipTemplateManager();
                DojoMembershipTemplateCollection membershipTemplateCollection =
                    membershipTemplateManager.GetCollection(string.Empty, string.Empty, null);
                foreach (DojoMembershipTemplate membershipTemplate in membershipTemplateCollection)
                {
                    addComboItem(comboMembershipTemplate1, membershipTemplate.Name,
                        membershipTemplate.ID.ToString());
                    addComboItem(comboMembershipTemplate2, membershipTemplate.Name,
                        membershipTemplate.ID.ToString());
                    addComboItem(comboMembershipTemplate3, membershipTemplate.Name,
                        membershipTemplate.ID.ToString());
                    addComboItem(comboMembershipTemplate4, membershipTemplate.Name,
                        membershipTemplate.ID.ToString());
                    addComboItem(comboMembershipTemplate5, membershipTemplate.Name,
                        membershipTemplate.ID.ToString());
                }
            }

			#endregion
		}

        private void addComboItem(ComponentArt.Web.UI.ComboBox comboBox, string name, string value)
        {
            ComponentArt.Web.UI.ComboBoxItem i = new ComponentArt.Web.UI.ComboBoxItem();
            i.Text = name;
            i.Value = value;
            comboBox.Items.Add(i);
        }

		#region Events

		protected void ok_Click(object sender, EventArgs e)
		{
            Page.Validate();
            if (!Page.IsValid)
                return;

            if (Amns.Tessen.Utilities.HashBuilder.IsCompiling)
                return;

			if(dojoMemberTypeTemplateID == 0)
				obj = new DojoMemberTypeTemplate();
			else
				obj = new DojoMemberTypeTemplate(dojoMemberTypeTemplateID);

			obj.Name = tbName.Text;
			obj.Description = tbDescription.Text;
			obj.OrderNum = int.Parse(tbOrderNum.Text);
			
            if(comboParent.SelectedItem != null)
				obj.Parent = DojoMemberTypeTemplate.NewPlaceHolder(
					int.Parse(comboParent.SelectedItem.Value));
			else
				obj.Parent = null;

			if(comboMemberType.SelectedItem != null)
				obj.MemberType = DojoMemberType.NewPlaceHolder(
					int.Parse(comboMemberType.SelectedItem.Value));
			else
				obj.MemberType = null;

			obj.MemberTypeTreeHash = tbMemberTypeTreeHash.Text;
			if(comboInitialRank.SelectedItem != null)
				obj.InitialRank = DojoRank.NewPlaceHolder(
					int.Parse(comboInitialRank.SelectedItem.Value));
			else
				obj.InitialRank = null;

			if(comboInitialRole.SelectedItem != null)
				obj.InitialRole = GreyFoxRole.NewPlaceHolder(
					int.Parse(comboInitialRole.SelectedItem.Value));
			else
				obj.InitialRole = null;

			obj.InitialEmailFrom = tbInitialEmailFrom.Text;
			obj.InitialEmailBody = tbInitialEmailBody.Text;
			obj.AllowGuestPurchase = cbAllowGuestPurchase.Checked;
			obj.AllowPurchase = cbAllowPurchase.Checked;
			obj.AllowRenewal = cbAllowRenewal.Checked;
			obj.AllowAutoRenewal = cbAllowAutoRenewal.Checked;
			obj.AgeYearsMax = int.Parse(tbAgeYearsMax.Text);
			obj.AgeYearsMin = int.Parse(tbAgeYearsMin.Text);
			obj.MemberForMin = int.Parse(tbMemberForMin.Text);
			obj.MemberForMax = int.Parse(tbMemberForMax.Text);
			if(comboRankMin.SelectedItem != null)
				obj.RankMin = DojoRank.NewPlaceHolder(
					int.Parse(comboRankMin.SelectedItem.Value));
			else
				obj.RankMin = null;

			if(comboRankMax.SelectedItem != null)
				obj.RankMax = DojoRank.NewPlaceHolder(
					int.Parse(comboRankMax.SelectedItem.Value));
			else
				obj.RankMax = null;

			if(comboMembershipTemplate1.SelectedItem != null)
				obj.MembershipTemplate1 = DojoMembershipTemplate.NewPlaceHolder(
					int.Parse(comboMembershipTemplate1.SelectedItem.Value));
			else
				obj.MembershipTemplate1 = null;

			if(comboMembershipTemplate2.SelectedItem != null)
				obj.MembershipTemplate2 = DojoMembershipTemplate.NewPlaceHolder(
					int.Parse(comboMembershipTemplate2.SelectedItem.Value));
			else
				obj.MembershipTemplate2 = null;

			if(comboMembershipTemplate3.SelectedItem != null)
				obj.MembershipTemplate3 = DojoMembershipTemplate.NewPlaceHolder(
					int.Parse(comboMembershipTemplate3.SelectedItem.Value));
			else
				obj.MembershipTemplate3 = null;

			if(comboMembershipTemplate4.SelectedItem != null)
				obj.MembershipTemplate4 = DojoMembershipTemplate.NewPlaceHolder(
					int.Parse(comboMembershipTemplate4.SelectedItem.Value));
			else
				obj.MembershipTemplate4 = null;

			if(comboMembershipTemplate5.SelectedItem != null)
				obj.MembershipTemplate5 = DojoMembershipTemplate.NewPlaceHolder(
					int.Parse(comboMembershipTemplate5.SelectedItem.Value));
			else
				obj.MembershipTemplate5 = null;

			if(editOnAdd)
				dojoMemberTypeTemplateID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbDescription.Text = string.Empty;
				tbOrderNum.Text = string.Empty;
				tbMemberTypeTreeHash.Text = string.Empty;
				tbInitialEmailFrom.Text = string.Empty;
				tbInitialEmailBody.Text = string.Empty;
				cbAllowGuestPurchase.Checked = false;
				cbAllowPurchase.Checked = false;
				cbAllowRenewal.Checked = false;
				cbAllowAutoRenewal.Checked = false;
				tbAgeYearsMax.Text = string.Empty;
				tbAgeYearsMin.Text = string.Empty;
				tbMemberForMin.Text = string.Empty;
				tbMemberForMax.Text = string.Empty;
				comboParent.SelectedIndex = 0;
                comboMemberType.SelectedIndex = 0;
                comboInitialRank.SelectedIndex = 0;
                comboInitialRole.SelectedIndex = 0;
                comboRankMin.SelectedIndex = 0;
                comboRankMax.SelectedIndex = 0;
                comboMembershipTemplate1.SelectedIndex = 0;
                comboMembershipTemplate2.SelectedIndex = 0;
                comboMembershipTemplate3.SelectedIndex = 0;
                comboMembershipTemplate4.SelectedIndex = 0;
                comboMembershipTemplate5.SelectedIndex = 0;
			}

            HashBuilder.Compile(typeof(DojoMemberTypeTemplateCollection));

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

		#endregion

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
                if (dojoMemberTypeTemplateID > 0)
                {
                    obj = new DojoMemberTypeTemplate(dojoMemberTypeTemplateID);
                    headerText.Text =
                        Localization.Strings.EditPrefix + obj.Name;
                }
                else if (dojoMemberTypeTemplateID <= 0)
                {
                    obj = new DojoMemberTypeTemplate();
                    headerText.Text = Localization.Strings.Add;
                }

                //// Bind _system Data
                //ltCreateDate.Text = obj.CreateDate.ToString();
                //ltModifyDate.Text = obj.ModifyDate.ToString();

                // Bind General Data
                tbName.Text = obj.Name;
                tbDescription.Text = obj.Description;
                tbOrderNum.Text = obj.OrderNum.ToString();
                tbMemberTypeTreeHash.Text = obj.MemberTypeTreeHash;

                if (obj.Parent != null)
                    selectComboItem(comboParent,
                         obj.Parent.Name,
                         obj.Parent.ID.ToString());
                else
                {
                    comboParent.Text = string.Empty;
                    comboParent.SelectedItem = null;
                }

                if (obj.MemberType != null)
                    selectComboItem(comboMemberType,
                         obj.MemberType.Name,
                         obj.MemberType.ID.ToString());
                else
                {
                    comboMemberType.Text = string.Empty;
                    comboMemberType.SelectedItem = null;
                }

                // Bind Initialization Data
                if (obj.InitialRank != null)
                    selectComboItem(comboInitialRank,
                         obj.InitialRank.Name,
                         obj.InitialRank.ID.ToString());
                else
                {
                    comboInitialRank.Text = string.Empty;
                    comboInitialRank.SelectedItem = null;
                }

                if (obj.InitialRole != null)
                    selectComboItem(comboInitialRole,
                         obj.InitialRole.Name,
                         obj.InitialRole.ID.ToString());
                else
                {
                    comboInitialRole.Text = string.Empty;
                    comboInitialRole.SelectedItem = null;
                }

                tbInitialEmailFrom.Text = obj.InitialEmailFrom;
                tbInitialEmailBody.Text = obj.InitialEmailBody;

                // Bind Access Features Data
                cbAllowGuestPurchase.Checked = obj.AllowGuestPurchase;
                cbAllowPurchase.Checked = obj.AllowPurchase;
                cbAllowRenewal.Checked = obj.AllowRenewal;
                cbAllowAutoRenewal.Checked = obj.AllowAutoRenewal;

                // Bind Requirements Data                
                tbAgeYearsMax.Text = obj.AgeYearsMax.ToString();
                tbAgeYearsMin.Text = obj.AgeYearsMin.ToString();
                tbMemberForMin.Text = obj.MemberForMin.ToString();
                tbMemberForMax.Text = obj.MemberForMax.ToString();

                if (obj.RankMin != null)
                    selectComboItem(comboRankMin,
                        obj.RankMin.Name,
                        obj.RankMin.ID.ToString());
                else
                {
                    comboRankMin.Text = string.Empty;
                    comboRankMin.SelectedItem = null;
                }

                if (obj.RankMax != null)
                    selectComboItem(comboRankMax,
                        obj.RankMax.Name,
                        obj.RankMax.ID.ToString());
                else
                {
                    comboRankMax.Text = string.Empty;
                    comboRankMax.SelectedItem = null;
                }

                if (obj.MembershipTemplate1 != null)
                    selectComboItem(comboMembershipTemplate1,
                        obj.MembershipTemplate1.Name,
                        obj.MembershipTemplate1.ID.ToString());
                else
                {
                    comboMembershipTemplate1.Text = string.Empty;
                    comboMembershipTemplate1.SelectedItem = null;
                }

                if (obj.MembershipTemplate2 != null)
                    selectComboItem(comboMembershipTemplate2,
                        obj.MembershipTemplate2.Name,
                        obj.MembershipTemplate2.ID.ToString());
                else
                {
                    comboMembershipTemplate2.Text = string.Empty;
                    comboMembershipTemplate2.SelectedItem = null;
                }

                if (obj.MembershipTemplate3 != null)
                    selectComboItem(comboMembershipTemplate3,
                        obj.MembershipTemplate3.Name,
                        obj.MembershipTemplate3.ID.ToString());
                else
                {
                    comboMembershipTemplate3.Text = string.Empty;
                    comboMembershipTemplate3.SelectedItem = null;
                }

                if (obj.MembershipTemplate4 != null)
                    selectComboItem(comboMembershipTemplate4,
                        obj.MembershipTemplate4.Name,
                        obj.MembershipTemplate4.ID.ToString());
                else
                {
                    comboMembershipTemplate4.Text = string.Empty;
                    comboMembershipTemplate4.SelectedItem = null;
                }

                if (obj.MembershipTemplate5 != null)
                    selectComboItem(comboMembershipTemplate5,
                        obj.MembershipTemplate5.Name,
                        obj.MembershipTemplate5.ID.ToString());
                else
                {
                    comboMembershipTemplate5.Text = string.Empty;
                    comboMembershipTemplate5.SelectedItem = null;
                }

                tabstrip.SelectedTab = tabstrip.Tabs[0];
                multipage.SelectedIndex = 0;
            }
        }               

        private void selectComboItem(ComponentArt.Web.UI.ComboBox combo,
            string text,
            string value)
        {
            combo.Text = text;
            foreach (ComboBoxItem item in combo.Items)
            {
                if (item.Value == value)
                {
                    combo.SelectedItem = item;
                    break;
                }
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

