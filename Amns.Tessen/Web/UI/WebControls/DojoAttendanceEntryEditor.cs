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
    /// Default web editor for DojoAttendanceEntry.
    /// </summary>
    [ToolboxData("<{0}:DojoAttendanceEntryEditor runat=server></{0}:DojoAttendanceEntryEditor>")]
    public class DojoAttendanceEntryEditor : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        private int dojoAttendanceEntryID;
        private DojoAttendanceEntry obj;
        private bool loadFlag = false;
        private bool resetOnAdd;
        private bool editOnAdd;

        protected ComponentArt.Web.UI.TabStrip tabstrip;
        protected ComponentArt.Web.UI.MultiPage multipage;
        protected Literal headerText;

        #region Private Control Fields for Default Folder

        protected ComponentArt.Web.UI.PageView DefaultView;
        private TextBox tbSigninTime;
        private TextBox tbSignoutTime;
        private ComponentArt.Web.UI.ComboBox comboClass;
        private ComponentArt.Web.UI.ComboBox comboMember;
        private ComponentArt.Web.UI.ComboBox comboRank;

        #endregion

        #region Private Control Fields for _system Folder

        protected ComponentArt.Web.UI.PageView _systemView;

        #endregion

        private Button btOk;
        private Button btCancel;
        private Button btDelete;

        #region Public Control Properties

        [Bindable(true), Category("Data"), DefaultValue(0)]
        public int DojoAttendanceEntryID
        {
            get
            {
                return dojoAttendanceEntryID;
            }
            set
            {
                loadFlag = true;
                dojoAttendanceEntryID = value;
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
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Signin Time</span>"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbSigninTime = new TextBox();
            tbSigninTime.EnableViewState = false;
            DefaultView.Controls.Add(tbSigninTime);
            DefaultView.Controls.Add(new LiteralControl("</span></div>"));

            DefaultView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Signout Time</span>"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbSignoutTime = new TextBox();
            tbSignoutTime.EnableViewState = false;
            DefaultView.Controls.Add(tbSignoutTime);
            DefaultView.Controls.Add(new LiteralControl("</span></div>"));

            DefaultView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Class</span>"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            comboClass = new ComponentArt.Web.UI.ComboBox();
            comboClass.CssClass = "comboBox";
            comboClass.HoverCssClass = "comboBoxHover";
            comboClass.FocusedCssClass = "comboBoxHover";
            comboClass.TextBoxCssClass = "comboTextBox";
            comboClass.DropDownCssClass = "comboDropDown";
            comboClass.ItemCssClass = "comboItem";
            comboClass.ItemHoverCssClass = "comboItemHover";
            comboClass.SelectedItemCssClass = "comboItemHover";
            comboClass.DropHoverImageUrl = "images/drop_hover.gif";
            comboClass.DropImageUrl = "images/drop.gif";
            comboClass.Width = Unit.Pixel(300);
            DefaultView.Controls.Add(comboClass);
            DefaultView.Controls.Add(new LiteralControl("</span></div>"));

            DefaultView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Member</span>"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            comboMember = new ComponentArt.Web.UI.ComboBox();
            comboMember.CssClass = "comboBox";
            comboMember.HoverCssClass = "comboBoxHover";
            comboMember.FocusedCssClass = "comboBoxHover";
            comboMember.TextBoxCssClass = "comboTextBox";
            comboMember.DropDownCssClass = "comboDropDown";
            comboMember.ItemCssClass = "comboItem";
            comboMember.ItemHoverCssClass = "comboItemHover";
            comboMember.SelectedItemCssClass = "comboItemHover";
            comboMember.DropHoverImageUrl = "images/drop_hover.gif";
            comboMember.DropImageUrl = "images/drop.gif";
            comboMember.Width = Unit.Pixel(300);
            DefaultView.Controls.Add(comboMember);
            DefaultView.Controls.Add(new LiteralControl("</span></div>"));

            DefaultView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Rank</span>"));
            DefaultView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            comboRank = new ComponentArt.Web.UI.ComboBox();
            comboRank.CssClass = "comboBox";
            comboRank.HoverCssClass = "comboBoxHover";
            comboRank.FocusedCssClass = "comboBoxHover";
            comboRank.TextBoxCssClass = "comboTextBox";
            comboRank.DropDownCssClass = "comboDropDown";
            comboRank.ItemCssClass = "comboItem";
            comboRank.ItemHoverCssClass = "comboItemHover";
            comboRank.SelectedItemCssClass = "comboItemHover";
            comboRank.DropHoverImageUrl = "images/drop_hover.gif";
            comboRank.DropImageUrl = "images/drop.gif";
            comboRank.Width = Unit.Pixel(300);
            DefaultView.Controls.Add(comboRank);
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

            DojoClassManager _classManager = new DojoClassManager();
            DojoClassCollection _classCollection = _classManager.GetCollection(string.Empty, string.Empty, null);
            ComponentArt.Web.UI.ComboBoxItem ClassNullItem = new ComponentArt.Web.UI.ComboBoxItem();
            ClassNullItem.Text = "Null";
            ClassNullItem.Value = "Null";
            comboClass.Items.Add(ClassNullItem);
            foreach (DojoClass _class in _classCollection)
            {
                ComponentArt.Web.UI.ComboBoxItem i = new ComponentArt.Web.UI.ComboBoxItem();
                i.Text = _class.ToString();
                i.Value = _class.ID.ToString();
                comboClass.Items.Add(i);
            }

            DojoMemberManager memberManager = new DojoMemberManager();
            DojoMemberCollection memberCollection = memberManager.GetCollection(string.Empty, string.Empty, null);
            ComponentArt.Web.UI.ComboBoxItem MemberNullItem = new ComponentArt.Web.UI.ComboBoxItem();
            MemberNullItem.Text = "Null";
            MemberNullItem.Value = "Null";
            comboMember.Items.Add(MemberNullItem);
            foreach (DojoMember member in memberCollection)
            {
                ComponentArt.Web.UI.ComboBoxItem i = new ComponentArt.Web.UI.ComboBoxItem();
                i.Text = member.ToString();
                i.Value = member.ID.ToString();
                comboMember.Items.Add(i);
            }

            DojoRankManager rankManager = new DojoRankManager();
            DojoRankCollection rankCollection = rankManager.GetCollection(string.Empty, string.Empty, null);
            ComponentArt.Web.UI.ComboBoxItem RankNullItem = new ComponentArt.Web.UI.ComboBoxItem();
            RankNullItem.Text = "Null";
            RankNullItem.Value = "Null";
            comboRank.Items.Add(RankNullItem);
            foreach (DojoRank rank in rankCollection)
            {
                ComponentArt.Web.UI.ComboBoxItem i = new ComponentArt.Web.UI.ComboBoxItem();
                i.Text = rank.ToString();
                i.Value = rank.ID.ToString();
                comboRank.Items.Add(i);
            }

            #endregion

        }

        #region Events

        protected void ok_Click(object sender, EventArgs e)
        {
            if (dojoAttendanceEntryID == 0)
                obj = new DojoAttendanceEntry();
            else
                obj = new DojoAttendanceEntry(dojoAttendanceEntryID);

            obj.SigninTime = DateTime.Parse(tbSigninTime.Text);
            obj.SignoutTime = DateTime.Parse(tbSignoutTime.Text);
            if (comboClass.SelectedItem != null && comboClass.SelectedItem.Value != "Null")
                obj.Class = DojoClass.NewPlaceHolder(
                    int.Parse(comboClass.SelectedItem.Value));
            else
                obj.Class = null;

            if (comboMember.SelectedItem != null && comboMember.SelectedItem.Value != "Null")
                obj.Member = DojoMember.NewPlaceHolder(
                    int.Parse(comboMember.SelectedItem.Value));
            else
                obj.Member = null;

            if (comboRank.SelectedItem != null && comboRank.SelectedItem.Value != "Null")
                obj.Rank = DojoRank.NewPlaceHolder(
                    int.Parse(comboRank.SelectedItem.Value));
            else
                obj.Rank = null;

            if (editOnAdd)
                dojoAttendanceEntryID = obj.Save();
            else
                obj.Save();

            if (resetOnAdd)
            {
                tbSigninTime.Text = DateTime.Now.ToString();
                tbSignoutTime.Text = DateTime.Now.ToString();
                comboClass.SelectedIndex = 0;
                comboMember.SelectedIndex = 0;
                comboRank.SelectedIndex = 0;
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
                if (dojoAttendanceEntryID > 0)
                {
                    obj = new DojoAttendanceEntry(dojoAttendanceEntryID);
                    headerText.Text = "Edit  - " + obj.ToString();
                }
                else if (dojoAttendanceEntryID <= 0)
                {
                    obj = new DojoAttendanceEntry();
                    headerText.Text = "Add ";
                }

                // Bind Default Data
                tbSigninTime.Text = obj.SigninTime.ToString();
                tbSignoutTime.Text = obj.SignoutTime.ToString();
                if (obj.Class != null)
                    foreach (ListItem item in comboClass.Items)
                        item.Selected = obj.Class.ID.ToString() == item.Value;
                else
                    comboClass.SelectedIndex = 0;
                if (obj.Member != null)
                    foreach (ListItem item in comboMember.Items)
                        item.Selected = obj.Member.ID.ToString() == item.Value;
                else
                    comboMember.SelectedIndex = 0;
                if (obj.Rank != null)
                    foreach (ListItem item in comboRank.Items)
                        item.Selected = obj.Rank.ID.ToString() == item.Value;
                else
                    comboRank.SelectedIndex = 0;
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
                    dojoAttendanceEntryID = (int)myState[1];
            }
        }

        protected override object SaveViewState()
        {
            object baseState = base.SaveViewState();
            object[] myState = new object[2];
            myState[0] = baseState;
            myState[1] = dojoAttendanceEntryID;
            return myState;
        }
    }
}

