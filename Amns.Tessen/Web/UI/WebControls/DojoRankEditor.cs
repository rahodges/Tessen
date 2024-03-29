/* ********************************************************** *
 * AMNS NitroCast v1.0 Class ComponentArt Based Editor          *
 * Autogenerated by NitroCast � 2007 Roy A.E Hodges             *
 * All Rights Reserved                                        *
 * ---------------------------------------------------------- *
 * Source code may not be reproduced or redistributed without *
 * written expressed permission from the author.              *
 * Permission is granted to modify source code by licencee.   *
 * These permissions do not extend to third parties.          *
 * ********************************************************** */

using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using ComponentArt.Web.UI;
using System.Web.UI.WebControls;
using Amns.Rappahanock;

namespace Amns.Tessen.Web.UI.WebControls
{
    /// <summary>
    /// Default web editor for DojoRank.
    /// </summary>
    [ToolboxData("<{0}:DojoRankEditor runat=server></{0}:DojoRankEditor>")]
    public class DojoRankEditor : System.Web.UI.Control, INamingContainer 
    {
        private int dojoRankID;
        private DojoRank obj;
        private bool loadFlag = false;
        private bool resetOnAdd;
        private bool editOnAdd;
        private string cssClass;

        protected ComponentArt.Web.UI.TabStrip tabstrip;
        protected ComponentArt.Web.UI.MultiPage multipage;
        protected Literal headerText;

        #region Private Control Fields for Default Folder

        private TextBox tbName;
        private TextBox tbPromotionTimeInRank;
        private RegularExpressionValidator revPromotionTimeInRank;
        private RequiredFieldValidator reqPromotionTimeInRank;
        private TextBox tbPromotionTimeFromLastTest;
        private RegularExpressionValidator revPromotionTimeFromLastTest;
        private RequiredFieldValidator reqPromotionTimeFromLastTest;
        private TextBox tbPromotionRequirements;
        private TextBox tbPromotionFee;
        private RequiredFieldValidator reqPromotionFee;
        private RangeValidator rngPromotionFee;
        private DropDownList ddPromotionRank;
        private CheckBox cbPromotionResetIP;
        private TextBox tbOrderNum;
        private RequiredFieldValidator reqOrderNum;
        private RangeValidator rngOrderNum;
        private TextBox tbDescription;

        #endregion

        #region Private Control Fields for Rappahanock Folder

        private DropDownList ddItem;

        #endregion

        private Button btOk;
        private Button btCancel;
        private Button btDelete;

        #region Public Control Properties

        [Bindable(true), Category("Data"), DefaultValue(0)]
        public int DojoRankID
        {
            get
            {
                return dojoRankID;
            }
            set
            {
                loadFlag = true;
                dojoRankID = value;
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

        [Bindable(true), Category("Appearance"), DefaultValue("")]
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

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

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
            tabstrip.EnableViewState = false;
            content.Controls.Add(tabstrip);

            #endregion

            #region MultiPage

            multipage = new ComponentArt.Web.UI.MultiPage();
            multipage.ID = this.ID + "_MultiPage";
            multipage.CssClass = "MultiPage";
            multipage.EnableViewState = false;
            content.Controls.Add(multipage);

            #endregion

            #region Child Controls for Default Folder

            ComponentArt.Web.UI.PageView DefaultView = new ComponentArt.Web.UI.PageView();
            DefaultView.CssClass = "PageContent";
            multipage.PageViews.Add(DefaultView);

            TabStripTab DefaultTab = new TabStripTab();
            DefaultTab.Text = "Default";
            DefaultTab.PageViewId = DefaultView.ID;
            tabstrip.Tabs.Add(DefaultTab);

            tbName = new TextBox();
            tbName.ID = "tbName";
            tbName.EnableViewState = false;
            registerControl(DefaultView, "Name", tbName);

            tbPromotionTimeInRank = new TextBox();
            tbPromotionTimeInRank.ID = "tbPromotionTimeInRank";
            tbPromotionTimeInRank.EnableViewState = false;
            revPromotionTimeInRank = new RegularExpressionValidator();
            revPromotionTimeInRank.ID = "RevPromotionTimeInRank";
            revPromotionTimeInRank.ControlToValidate = tbPromotionTimeInRank.ID;
            revPromotionTimeInRank.ValidationExpression = @"^\s*-?(\d{0,7}|10[0-5]\d{0,5}|106[0-6]\d{0,4}|1067[0-4]\d{0,3}|10675[0-1]\d{0,2}|((\d{0,7}|10[0-5]\d{0,5}|106[0-6]\d{0,4}|1067[0-4]\d{0,3}|10675[0-1]\d{0,2})\.)?([0-1]?[0-9]|2[0-3]):[0-5]?[0-9](:[0-5]?[0-9](\.\d{1,7})?)?)\s*$";
            revPromotionTimeInRank.ErrorMessage = "*";
            revPromotionTimeInRank.Display = ValidatorDisplay.Dynamic;
            revPromotionTimeInRank.SetFocusOnError = true;
            reqPromotionTimeInRank = new RequiredFieldValidator();
            reqPromotionTimeInRank.ID = "reqPromotionTimeInRank";
            reqPromotionTimeInRank.ControlToValidate = tbPromotionTimeInRank.ID;
            reqPromotionTimeInRank.ErrorMessage = "*";
            reqPromotionTimeInRank.Display = ValidatorDisplay.Dynamic;
            registerControl(DefaultView, "Promotion Time In Rank", tbPromotionTimeInRank, revPromotionTimeInRank, reqPromotionTimeInRank);

            tbPromotionTimeFromLastTest = new TextBox();
            tbPromotionTimeFromLastTest.ID = "tbPromotionTimeFromLastTest";
            tbPromotionTimeFromLastTest.EnableViewState = false;
            revPromotionTimeFromLastTest = new RegularExpressionValidator();
            revPromotionTimeFromLastTest.ID = "RevPromotionTimeFromLastTest";
            revPromotionTimeFromLastTest.ControlToValidate = tbPromotionTimeFromLastTest.ID;
            revPromotionTimeFromLastTest.ValidationExpression = @"^\s*-?(\d{0,7}|10[0-5]\d{0,5}|106[0-6]\d{0,4}|1067[0-4]\d{0,3}|10675[0-1]\d{0,2}|((\d{0,7}|10[0-5]\d{0,5}|106[0-6]\d{0,4}|1067[0-4]\d{0,3}|10675[0-1]\d{0,2})\.)?([0-1]?[0-9]|2[0-3]):[0-5]?[0-9](:[0-5]?[0-9](\.\d{1,7})?)?)\s*$";
            revPromotionTimeFromLastTest.ErrorMessage = "*";
            revPromotionTimeFromLastTest.Display = ValidatorDisplay.Dynamic;
            revPromotionTimeFromLastTest.SetFocusOnError = true;
            reqPromotionTimeFromLastTest = new RequiredFieldValidator();
            reqPromotionTimeFromLastTest.ID = "reqPromotionTimeFromLastTest";
            reqPromotionTimeFromLastTest.ControlToValidate = tbPromotionTimeFromLastTest.ID;
            reqPromotionTimeFromLastTest.ErrorMessage = "*";
            reqPromotionTimeFromLastTest.Display = ValidatorDisplay.Dynamic;
            registerControl(DefaultView, "Promotion Time From Last Test", tbPromotionTimeFromLastTest, revPromotionTimeFromLastTest, reqPromotionTimeFromLastTest);

            tbPromotionRequirements = new TextBox();
            tbPromotionRequirements.ID = "tbPromotionRequirements";
            tbPromotionRequirements.EnableViewState = false;
            registerControl(DefaultView, "Promotion Requirements", tbPromotionRequirements);

            tbPromotionFee = new TextBox();
            tbPromotionFee.ID = "tbPromotionFee";
            tbPromotionFee.EnableViewState = false;
            reqPromotionFee = new RequiredFieldValidator();
            reqPromotionFee.ID = "reqPromotionFee";
            reqPromotionFee.ControlToValidate = tbPromotionFee.ID;
            reqPromotionFee.ErrorMessage = "*";
            reqPromotionFee.Display = ValidatorDisplay.Dynamic;
            rngPromotionFee = new RangeValidator();
            rngPromotionFee.ID = "rngPromotionFee";
            rngPromotionFee.ControlToValidate = tbPromotionFee.ID;
            rngPromotionFee.ErrorMessage = "*";
            rngPromotionFee.Display = ValidatorDisplay.Dynamic;
            rngPromotionFee.Type = ValidationDataType.Double;
            rngPromotionFee.MinimumValue = "0";
            rngPromotionFee.MaximumValue = "79228162514264337593543950335";
            registerControl(DefaultView, "Promotion Fee", tbPromotionFee, reqPromotionFee, rngPromotionFee);

            ddPromotionRank = new DropDownList();
            ddPromotionRank.ID = "ddPromotionRank";
            ddPromotionRank.EnableViewState = false;
            registerControl(DefaultView, "PromotionRank", ddPromotionRank);

            cbPromotionResetIP = new CheckBox();
            cbPromotionResetIP.ID = "cbPromotionResetIP";
            cbPromotionResetIP.EnableViewState = false;
            registerControl(DefaultView, "PromotionResetIP", cbPromotionResetIP);

            tbOrderNum = new TextBox();
            tbOrderNum.ID = "tbOrderNum";
            tbOrderNum.EnableViewState = false;
            reqOrderNum = new RequiredFieldValidator();
            reqOrderNum.ID = "reqOrderNum";
            reqOrderNum.ControlToValidate = tbOrderNum.ID;
            reqOrderNum.ErrorMessage = "*";
            reqOrderNum.Display = ValidatorDisplay.Dynamic;
            rngOrderNum = new RangeValidator();
            rngOrderNum.ID = "rngOrderNum";
            rngOrderNum.ControlToValidate = tbOrderNum.ID;
            rngOrderNum.ErrorMessage = "*";
            rngOrderNum.Display = ValidatorDisplay.Dynamic;
            rngOrderNum.Type = ValidationDataType.Integer;
            rngOrderNum.MinimumValue = "-2147483648";
            rngOrderNum.MaximumValue = "2147483647";
            registerControl(DefaultView, "OrderNum", tbOrderNum, reqOrderNum, rngOrderNum);

            tbDescription = new TextBox();
            tbDescription.ID = "tbDescription";
            tbDescription.EnableViewState = false;
            registerControl(DefaultView, "Description", tbDescription);

            #endregion

            #region Child Controls for Rappahanock Folder

            ComponentArt.Web.UI.PageView RappahanockView = new ComponentArt.Web.UI.PageView();
            RappahanockView.CssClass = "PageContent";
            multipage.PageViews.Add(RappahanockView);

            TabStripTab RappahanockTab = new TabStripTab();
            RappahanockTab.Text = "Rappahanock";
            RappahanockTab.PageViewId = RappahanockView.ID;
            tabstrip.Tabs.Add(RappahanockTab);

            ddItem = new DropDownList();
            ddItem.ID = "ddItem";
            ddItem.EnableViewState = false;
            registerControl(RappahanockView, "Item", ddItem);

            #endregion

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
            btCancel.CausesValidation = false;
            btCancel.Click += new EventHandler(cancel_Click);
            buttons.Controls.Add(btCancel);

            btDelete = new Button();
            btDelete.Text = "Delete";
            btDelete.Width = Unit.Pixel(72);
            btDelete.EnableViewState = false;
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
            #region Bind Default Child Data

            DojoRankManager dojoRankManager = new DojoRankManager();
            DojoRankCollection dojoRankCollection = dojoRankManager.GetCollection(string.Empty, string.Empty);
            ddPromotionRank.Items.Add(new ListItem("             ", "null"));
            foreach (DojoRank itemObject in dojoRankCollection)
                ddPromotionRank.Items.Add(new ListItem(itemObject.ToString(), itemObject.ID.ToString()));


            #endregion

            #region Bind Rappahanock Child Data

            RHItemManager rHItemManager = new RHItemManager();
            RHItemCollection rHItemCollection = rHItemManager.GetCollection(string.Empty, string.Empty);
            ddItem.Items.Add(new ListItem("             ", "null"));
            foreach (RHItem itemObject in rHItemCollection)
                ddItem.Items.Add(new ListItem(itemObject.ToString(), itemObject.ID.ToString()));


            #endregion

        }

        #region Events

        protected void ok_Click(object sender, EventArgs e)
        {
            if (dojoRankID == 0)
                obj = new DojoRank();
            else
                obj = new DojoRank(dojoRankID);

            obj.Name = tbName.Text;
            obj.PromotionTimeInRank = TimeSpan.Parse(tbPromotionTimeInRank.Text);
            obj.PromotionTimeFromLastTest = TimeSpan.Parse(tbPromotionTimeFromLastTest.Text);
            obj.PromotionRequirements = tbPromotionRequirements.Text;
            obj.PromotionFee = decimal.Parse(tbPromotionFee.Text);
            if (ddPromotionRank.SelectedItem != null && ddPromotionRank.SelectedValue != "null")
            {
                obj.PromotionRank = DojoRank.NewPlaceHolder(int.Parse(ddPromotionRank.SelectedValue));
            }
            else
            {
                obj.PromotionRank = null;
            }
            obj.PromotionResetIP = cbPromotionResetIP.Checked;
            obj.OrderNum = int.Parse(tbOrderNum.Text);
            obj.Description = tbDescription.Text;
            if (ddItem.SelectedItem != null && ddItem.SelectedValue != "null")
            {
                obj.Item = RHItem.NewPlaceHolder(int.Parse(ddItem.SelectedValue));
            }
            else
            {
                obj.Item = null;
            }
            if (editOnAdd)
                dojoRankID = obj.Save();
            else
                obj.Save();

            if (resetOnAdd)
            {
                DojoRank newObj = new DojoRank();
                tbName.Text = newObj.Name;
                tbPromotionTimeInRank.Text = newObj.PromotionTimeInRank.ToString();
                tbPromotionTimeFromLastTest.Text = newObj.PromotionTimeFromLastTest.ToString();
                tbPromotionRequirements.Text = newObj.PromotionRequirements;
                tbPromotionFee.Text = newObj.PromotionFee.ToString();
                if (newObj.PromotionRank != null)
                    foreach (ListItem item in ddPromotionRank.Items)
                        item.Selected = newObj.PromotionRank.ID.ToString() == item.Value;
                else if (ddPromotionRank.Items.Count > 0)
                    ddPromotionRank.SelectedIndex = 0;

                cbPromotionResetIP.Checked = newObj.PromotionResetIP;
                tbOrderNum.Text = newObj.OrderNum.ToString();
                tbDescription.Text = newObj.Description;
                if (newObj.Item != null)
                    foreach (ListItem item in ddItem.Items)
                        item.Selected = newObj.Item.ID.ToString() == item.Value;
                else if (ddItem.Items.Count > 0)
                    ddItem.SelectedIndex = 0;

            }

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
            if (Cancelled != null)
                Cancelled(this, e);
        }

        public event EventHandler Updated;
        protected virtual void OnUpdated(EventArgs e)
        {
            if (Updated != null)
                Updated(this, e);
        }

        public event EventHandler DeleteClicked;
        protected virtual void OnDeleteClicked(EventArgs e)
        {
            if (DeleteClicked != null)
                DeleteClicked(this, e);
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EnsureChildControls();  // Fires Events???
        }               

        protected override void OnPreRender(EventArgs e)
        {
            if (loadFlag)
            {
                if (dojoRankID > 0)
                {
                    obj = new DojoRank(dojoRankID);
                    headerText.Text = "Edit  - " + obj.ToString();
                }
                else if (dojoRankID <= 0)
                {
                    obj = new DojoRank();
                    headerText.Text = "Add ";
                }

                // Bind Default Data
                tbName.Text = obj.Name;
                tbPromotionTimeInRank.Text = obj.PromotionTimeInRank.ToString();
                tbPromotionTimeFromLastTest.Text = obj.PromotionTimeFromLastTest.ToString();
                tbPromotionRequirements.Text = obj.PromotionRequirements;
                tbPromotionFee.Text = obj.PromotionFee.ToString();
                if (obj.PromotionRank != null)
                    foreach (ListItem item in ddPromotionRank.Items)
                        item.Selected = obj.PromotionRank.ID.ToString() == item.Value;
                else if (ddPromotionRank.Items.Count > 0)
                    ddPromotionRank.SelectedIndex = 0;

                cbPromotionResetIP.Checked = obj.PromotionResetIP;
                tbOrderNum.Text = obj.OrderNum.ToString();
                tbDescription.Text = obj.Description;

                // Bind Rappahanock Data
                if (obj.Item != null)
                    foreach (ListItem item in ddItem.Items)
                        item.Selected = obj.Item.ID.ToString() == item.Value;
                else if (ddItem.Items.Count > 0)
                    ddItem.SelectedIndex = 0;

                tabstrip.SelectedTab = tabstrip.Tabs[0];
                multipage.SelectedIndex = 0;
            }
        }

        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                object[] myState = (object[])savedState;
                if (myState[0] != null)
                    base.LoadViewState(myState[0]);
                if (myState[1] != null)
                    dojoRankID = (int)myState[1];
            }
        }

        protected override object SaveViewState()
        {
            object baseState = base.SaveViewState();
            object[] myState = new object[2];
            myState[0] = baseState;
            myState[1] = dojoRankID;
            return myState;
        }
    }
}
