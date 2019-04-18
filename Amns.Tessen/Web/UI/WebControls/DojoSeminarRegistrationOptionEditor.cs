using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using ComponentArt.Web.UI;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
    /// <summary>
    /// Default web editor for DojoSeminarRegistrationOption.
    /// </summary>
    [ToolboxData("<{0}:DojoSeminarRegistrationOptionEditor runat=server></{0}:DojoSeminarRegistrationOptionEditor>")]
    public class DojoSeminarRegistrationOptionEditor : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        private int dojoSeminarRegistrationOptionID;
        private DojoSeminarRegistrationOption obj;
        private bool loadFlag = false;
        private bool resetOnAdd;
        private bool editOnAdd;

        protected ComponentArt.Web.UI.TabStrip tabstrip;
        protected ComponentArt.Web.UI.MultiPage multipage;
        protected Literal headerText;

        #region Private Control Fields for Default Folder

        protected ComponentArt.Web.UI.PageView DefaultView;
        private TextBox tbQuantity;
        private RegularExpressionValidator revQuantity;
        private TextBox tbTotalFee;
        private TextBox tbCostPerItem;
        private ComponentArt.Web.UI.ComboBox comboParentOption;
        private ComponentArt.Web.UI.ComboBox comboParentRegistration;

        #endregion

        #region Private Control Fields for _system Folder

        protected ComponentArt.Web.UI.PageView _systemView;

        #endregion

        private Button btOk;
        private Button btCancel;
        private Button btDelete;

        #region Public Control Properties

        [Bindable(true), Category("Data"), DefaultValue(0)]
        public int DojoSeminarRegistrationOptionID
        {
            get
            {
                return dojoSeminarRegistrationOptionID;
            }
            set
            {
                loadFlag = true;
                dojoSeminarRegistrationOptionID = value;
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
            content.Controls.Add(tabstrip);

            #endregion

            #region MultiPage

            multipage = new ComponentArt.Web.UI.MultiPage();
            multipage.ID = this.ID + "_MultiPage";
            multipage.CssClass = "MultiPage";
            content.Controls.Add(multipage);

            #endregion

            #region Child Controls for Default Folder

            DefaultView = new ComponentArt.Web.UI.PageView();
            DefaultView.CssClass = "PageContent";
            multipage.PageViews.Add(DefaultView);

            TabStripTab DefaultTab = new TabStripTab();
            DefaultTab.Text = "Default";
            DefaultTab.PageViewId = DefaultView.ID;
            tabstrip.Tabs.Add(DefaultTab);

            DefaultView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Quantity</span>"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbQuantity = new TextBox();
            tbQuantity.ID = this.ID + "_Quantity";
            tbQuantity.EnableViewState = false;
            DefaultView.Controls.Add(tbQuantity);
            revQuantity = new RegularExpressionValidator();
            revQuantity.ControlToValidate = tbQuantity.ID;
            revQuantity.ValidationExpression = "^(\\+|-)?\\d+$";
            revQuantity.ErrorMessage = "*";
            revQuantity.Display = ValidatorDisplay.Dynamic;
            revQuantity.EnableViewState = false;
            DefaultView.Controls.Add(revQuantity);
            DefaultView.Controls.Add(new LiteralControl("</span></div>"));

            DefaultView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">TotalFee</span>"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbTotalFee = new TextBox();
            tbTotalFee.EnableViewState = false;
            DefaultView.Controls.Add(tbTotalFee);
            DefaultView.Controls.Add(new LiteralControl("</span></div>"));

            DefaultView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">CostPerItem</span>"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbCostPerItem = new TextBox();
            tbCostPerItem.EnableViewState = false;
            DefaultView.Controls.Add(tbCostPerItem);
            DefaultView.Controls.Add(new LiteralControl("</span></div>"));

            DefaultView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">ParentOption</span>"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            comboParentOption = new ComponentArt.Web.UI.ComboBox();
            comboParentOption.CssClass = "comboBox";
            comboParentOption.HoverCssClass = "comboBoxHover";
            comboParentOption.FocusedCssClass = "comboBoxHover";
            comboParentOption.TextBoxCssClass = "comboTextBox";
            comboParentOption.DropDownCssClass = "comboDropDown";
            comboParentOption.ItemCssClass = "comboItem";
            comboParentOption.ItemHoverCssClass = "comboItemHover";
            comboParentOption.SelectedItemCssClass = "comboItemHover";
            comboParentOption.DropHoverImageUrl = "images/drop_hover.gif";
            comboParentOption.DropImageUrl = "images/drop.gif";
            comboParentOption.Width = Unit.Pixel(300);
            DefaultView.Controls.Add(comboParentOption);
            DefaultView.Controls.Add(new LiteralControl("</span></div>"));

            DefaultView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">ParentRegistration</span>"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            comboParentRegistration = new ComponentArt.Web.UI.ComboBox();
            comboParentRegistration.CssClass = "comboBox";
            comboParentRegistration.HoverCssClass = "comboBoxHover";
            comboParentRegistration.FocusedCssClass = "comboBoxHover";
            comboParentRegistration.TextBoxCssClass = "comboTextBox";
            comboParentRegistration.DropDownCssClass = "comboDropDown";
            comboParentRegistration.ItemCssClass = "comboItem";
            comboParentRegistration.ItemHoverCssClass = "comboItemHover";
            comboParentRegistration.SelectedItemCssClass = "comboItemHover";
            comboParentRegistration.DropHoverImageUrl = "images/drop_hover.gif";
            comboParentRegistration.DropImageUrl = "images/drop.gif";
            comboParentRegistration.Width = Unit.Pixel(300);
            DefaultView.Controls.Add(comboParentRegistration);
            DefaultView.Controls.Add(new LiteralControl("</span></div>"));

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

        private void bind()
        {
            #region Bind Default Child Data

            DojoSeminarOptionManager parentOptionManager = new DojoSeminarOptionManager();
            DojoSeminarOptionCollection parentOptionCollection = parentOptionManager.GetCollection(string.Empty, string.Empty, null);
            ComponentArt.Web.UI.ComboBoxItem ParentOptionNullItem = new ComponentArt.Web.UI.ComboBoxItem();
            ParentOptionNullItem.Text = "Null";
            ParentOptionNullItem.Value = "Null";
            comboParentOption.Items.Add(ParentOptionNullItem);
            foreach (DojoSeminarOption parentOption in parentOptionCollection)
            {
                ComponentArt.Web.UI.ComboBoxItem i = new ComponentArt.Web.UI.ComboBoxItem();
                i.Text = parentOption.ToString();
                i.Value = parentOption.ID.ToString();
                comboParentOption.Items.Add(i);
            }

            DojoSeminarRegistrationManager parentRegistrationManager = new DojoSeminarRegistrationManager();
            DojoSeminarRegistrationCollection parentRegistrationCollection = parentRegistrationManager.GetCollection(string.Empty, string.Empty, null);
            ComponentArt.Web.UI.ComboBoxItem ParentRegistrationNullItem = new ComponentArt.Web.UI.ComboBoxItem();
            ParentRegistrationNullItem.Text = "Null";
            ParentRegistrationNullItem.Value = "Null";
            comboParentRegistration.Items.Add(ParentRegistrationNullItem);
            foreach (DojoSeminarRegistration parentRegistration in parentRegistrationCollection)
            {
                ComponentArt.Web.UI.ComboBoxItem i = new ComponentArt.Web.UI.ComboBoxItem();
                i.Text = parentRegistration.ToString();
                i.Value = parentRegistration.ID.ToString();
                comboParentRegistration.Items.Add(i);
            }

            #endregion

        }

        #region Events

        protected void ok_Click(object sender, EventArgs e)
        {
            if (dojoSeminarRegistrationOptionID == 0)
                obj = new DojoSeminarRegistrationOption();
            else
                obj = new DojoSeminarRegistrationOption(dojoSeminarRegistrationOptionID);

            obj.Quantity = int.Parse(tbQuantity.Text);
            obj.TotalFee = decimal.Parse(tbTotalFee.Text);
            obj.CostPerItem = decimal.Parse(tbCostPerItem.Text);
            if (comboParentOption.SelectedItem != null && comboParentOption.SelectedItem.Value != "Null")
                obj.ParentOption = DojoSeminarOption.NewPlaceHolder(
                    int.Parse(comboParentOption.SelectedItem.Value));
            else
                obj.ParentOption = null;

            if (comboParentRegistration.SelectedItem != null && comboParentRegistration.SelectedItem.Value != "Null")
                obj.ParentRegistration = DojoSeminarRegistration.NewPlaceHolder(
                    int.Parse(comboParentRegistration.SelectedItem.Value));
            else
                obj.ParentRegistration = null;

            if (editOnAdd)
                dojoSeminarRegistrationOptionID = obj.Save();
            else
                obj.Save();

            if (resetOnAdd)
            {
                tbQuantity.Text = string.Empty;
                tbTotalFee.Text = string.Empty;
                tbCostPerItem.Text = string.Empty;
                comboParentOption.SelectedIndex = 0;
                comboParentRegistration.SelectedIndex = 0;
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

        protected override void OnPreRender(EventArgs e)
        {
            if (loadFlag)
            {
                if (dojoSeminarRegistrationOptionID > 0)
                {
                    obj = new DojoSeminarRegistrationOption(dojoSeminarRegistrationOptionID);
                    headerText.Text = "Edit  - " + obj.ToString();
                }
                else if (dojoSeminarRegistrationOptionID <= 0)
                {
                    obj = new DojoSeminarRegistrationOption();
                    headerText.Text = "Add ";
                }

                // Bind Default Data
                tbQuantity.Text = obj.Quantity.ToString();
                tbTotalFee.Text = obj.TotalFee.ToString();
                tbCostPerItem.Text = obj.CostPerItem.ToString();
                if (obj.ParentOption != null)
                    foreach (ListItem item in comboParentOption.Items)
                        item.Selected = obj.ParentOption.ID.ToString() == item.Value;
                else
                    comboParentOption.SelectedIndex = 0;
                if (obj.ParentRegistration != null)
                    foreach (ListItem item in comboParentRegistration.Items)
                        item.Selected = obj.ParentRegistration.ID.ToString() == item.Value;
                else
                    comboParentRegistration.SelectedIndex = 0;
                tabstrip.SelectedTab = tabstrip.Tabs[0];
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
                    dojoSeminarRegistrationOptionID = (int)myState[1];
            }
        }

        protected override object SaveViewState()
        {
            object baseState = base.SaveViewState();
            object[] myState = new object[2];
            myState[0] = baseState;
            myState[1] = dojoSeminarRegistrationOptionID;
            return myState;
        }
    }
}

