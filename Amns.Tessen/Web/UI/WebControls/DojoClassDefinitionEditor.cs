using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using ComponentArt.Web.UI;
using System.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen;

namespace Amns.Tessen.Web.UI.WebControls
{
    /// <summary>
    /// Default web editor for DojoClassDefinition.
    /// </summary>
    [ToolboxData("<{0}:DojoClassDefinitionEditor runat=server></{0}:DojoClassDefinitionEditor>")]
    public class DojoClassDefinitionEditor : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        private int dojoClassDefinitionID;
        private DojoClassDefinition obj;
        private bool loadFlag = false;
        private bool resetOnAdd;
        private bool editOnAdd;

        protected ComponentArt.Web.UI.TabStrip tabstrip;
        protected ComponentArt.Web.UI.MultiPage multipage;
        protected Literal headerText;

        #region Private Control Fields for Default Folder

        protected ComponentArt.Web.UI.PageView DefaultView;
        private TextBox tbName;
        private TextBox tbDescription;
        private CheckBox cbIsDisabled;
        private TextBox tbOccupancyAvg;
        private RegularExpressionValidator revOccupancyAvg;
        private TextBox tbOccupancyAvgDate;

        #endregion

        #region Private Control Fields for _system Folder

        protected ComponentArt.Web.UI.PageView _systemView;

        #endregion

        #region Private Control Fields for Access_Control Folder

        protected ComponentArt.Web.UI.PageView Access_ControlView;
        private ComponentArt.Web.UI.ComboBox comboAccessControlGroup;

        #endregion

        #region Private Control Fields for Recurrency Folder

        protected ComponentArt.Web.UI.PageView RecurrencyView;
        private TextBox tbRecurrenceType;
        private TextBox tbRecurrenceCount;
        private RegularExpressionValidator revRecurrenceCount;
        private TextBox tbRecurrenceEnd;
        private TextBox tbRecurrenceSpan;

        #endregion

        #region Private Control Fields for Next_Class Folder

        protected ComponentArt.Web.UI.PageView Next_ClassView;
        private TextBox tbNextSigninStart;
        private TextBox tbNextSigninEnd;
        private TextBox tbNextClassStart;
        private TextBox tbNextClassEnd;
        private ComponentArt.Web.UI.ComboBox comboInstructor;
        private ComponentArt.Web.UI.ComboBox comboLocation;

        #endregion

        private Button btOk;
        private Button btCancel;
        private Button btDelete;

        #region Public Control Properties

        [Bindable(true), Category("Data"), DefaultValue(0)]
        public int DojoClassDefinitionID
        {
            get
            {
                return dojoClassDefinitionID;
            }
            set
            {
                loadFlag = true;
                dojoClassDefinitionID = value;
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

            tbName = new TextBox();
            tbName.EnableViewState = false;
            cbIsDisabled = new CheckBox();
            cbIsDisabled.EnableViewState = false;
            cbIsDisabled.Text = Localization.Strings.Disabled;
            registerControl(DefaultView, 
                Localization.PeopleStrings.Name, tbName, cbIsDisabled);

            tbDescription = new TextBox();
            tbDescription.EnableViewState = false;
            registerControl(DefaultView,
                Localization.Strings.Description, tbDescription);

            tbOccupancyAvg = new TextBox();
            tbOccupancyAvg.ID = this.ID + "_OccupancyAvg";
            tbOccupancyAvg.EnableViewState = false;
            DefaultView.Controls.Add(tbOccupancyAvg);
            revOccupancyAvg = new RegularExpressionValidator();
            revOccupancyAvg.ControlToValidate = tbOccupancyAvg.ID;
            revOccupancyAvg.ValidationExpression = "^(\\+|-)?\\d+$";
            revOccupancyAvg.ErrorMessage = "*";
            revOccupancyAvg.Display = ValidatorDisplay.Dynamic;
            revOccupancyAvg.EnableViewState = false;
            registerControl(DefaultView,
                "OccupancyAvg", tbOccupancyAvg, revOccupancyAvg);

            tbOccupancyAvgDate = new TextBox();
            tbOccupancyAvgDate.EnableViewState = false;
            DefaultView.Controls.Add(tbOccupancyAvgDate);
            registerControl(DefaultView,
                "OccupancyAvgDate", tbOccupancyAvgDate);

            #endregion

            #region Child Controls for Access Control Folder

            Access_ControlView = new ComponentArt.Web.UI.PageView();
            Access_ControlView.CssClass = "PageContent";
            multipage.PageViews.Add(Access_ControlView);

            TabStripTab Access_ControlTab = new TabStripTab();
            Access_ControlTab.Text = "Access Control";
            Access_ControlTab.PageViewId = Access_ControlView.ID;
            tabstrip.Tabs.Add(Access_ControlTab);

            Access_ControlView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            Access_ControlView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">AccessControlGroup</span>"));
            Access_ControlView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            comboAccessControlGroup = new ComponentArt.Web.UI.ComboBox();
            comboAccessControlGroup.CssClass = "comboBox";
            comboAccessControlGroup.HoverCssClass = "comboBoxHover";
            comboAccessControlGroup.FocusedCssClass = "comboBoxHover";
            comboAccessControlGroup.TextBoxCssClass = "comboTextBox";
            comboAccessControlGroup.DropDownCssClass = "comboDropDown";
            comboAccessControlGroup.ItemCssClass = "comboItem";
            comboAccessControlGroup.ItemHoverCssClass = "comboItemHover";
            comboAccessControlGroup.SelectedItemCssClass = "comboItemHover";
            comboAccessControlGroup.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboAccessControlGroup.DropImageUrl = "combobox_images/drop.gif";
            comboAccessControlGroup.Width = Unit.Pixel(300);
            Access_ControlView.Controls.Add(comboAccessControlGroup);
            Access_ControlView.Controls.Add(new LiteralControl("</span></div>"));

            #endregion

            #region Child Controls for Recurrency Folder

            RecurrencyView = new ComponentArt.Web.UI.PageView();
            RecurrencyView.CssClass = "PageContent";
            multipage.PageViews.Add(RecurrencyView);

            TabStripTab RecurrencyTab = new TabStripTab();
            RecurrencyTab.Text = "Recurrency";
            RecurrencyTab.PageViewId = RecurrencyView.ID;
            tabstrip.Tabs.Add(RecurrencyTab);

            RecurrencyView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            RecurrencyView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">The recurrence type for scheduling.</span>"));
            RecurrencyView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbRecurrenceType = new TextBox();
            tbRecurrenceType.EnableViewState = false;
            RecurrencyView.Controls.Add(tbRecurrenceType);
            RecurrencyView.Controls.Add(new LiteralControl("</span></div>"));

            RecurrencyView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            RecurrencyView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">The remaining count for recurrences.</span>"));
            RecurrencyView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbRecurrenceCount = new TextBox();
            tbRecurrenceCount.ID = this.ID + "_RecurrenceCount";
            tbRecurrenceCount.EnableViewState = false;
            RecurrencyView.Controls.Add(tbRecurrenceCount);
            revRecurrenceCount = new RegularExpressionValidator();
            revRecurrenceCount.ControlToValidate = tbRecurrenceCount.ID;
            revRecurrenceCount.ValidationExpression = "^(\\+|-)?\\d+$";
            revRecurrenceCount.ErrorMessage = "*";
            revRecurrenceCount.Display = ValidatorDisplay.Dynamic;
            revRecurrenceCount.EnableViewState = false;
            RecurrencyView.Controls.Add(revRecurrenceCount);
            RecurrencyView.Controls.Add(new LiteralControl("</span></div>"));

            RecurrencyView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            RecurrencyView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Date to end class definition.</span>"));
            RecurrencyView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbRecurrenceEnd = new TextBox();
            tbRecurrenceEnd.EnableViewState = false;
            RecurrencyView.Controls.Add(tbRecurrenceEnd);
            RecurrencyView.Controls.Add(new LiteralControl("</span></div>"));

            RecurrencyView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            RecurrencyView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Time span to calculate recurring classes.</span>"));
            RecurrencyView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbRecurrenceSpan = new TextBox();
            tbRecurrenceSpan.EnableViewState = false;
            RecurrencyView.Controls.Add(tbRecurrenceSpan);
            RecurrencyView.Controls.Add(new LiteralControl("</span></div>"));

            #endregion

            #region Child Controls for Next Class Folder

            Next_ClassView = new ComponentArt.Web.UI.PageView();
            Next_ClassView.CssClass = "PageContent";
            multipage.PageViews.Add(Next_ClassView);

            TabStripTab Next_ClassTab = new TabStripTab();
            Next_ClassTab.Text = "Next Class";
            Next_ClassTab.PageViewId = Next_ClassView.ID;
            tabstrip.Tabs.Add(Next_ClassTab);

            Next_ClassView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Next Signin Start</span>"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbNextSigninStart = new TextBox();
            tbNextSigninStart.EnableViewState = false;
            Next_ClassView.Controls.Add(tbNextSigninStart);
            Next_ClassView.Controls.Add(new LiteralControl("</span></div>"));

            Next_ClassView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Next Signin End</span>"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbNextSigninEnd = new TextBox();
            tbNextSigninEnd.EnableViewState = false;
            Next_ClassView.Controls.Add(tbNextSigninEnd);
            Next_ClassView.Controls.Add(new LiteralControl("</span></div>"));

            Next_ClassView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Next Class Start</span>"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbNextClassStart = new TextBox();
            tbNextClassStart.EnableViewState = false;
            Next_ClassView.Controls.Add(tbNextClassStart);
            Next_ClassView.Controls.Add(new LiteralControl("</span></div>"));

            Next_ClassView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Next Class End</span>"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            tbNextClassEnd = new TextBox();
            tbNextClassEnd.EnableViewState = false;
            Next_ClassView.Controls.Add(tbNextClassEnd);
            Next_ClassView.Controls.Add(new LiteralControl("</span></div>"));

            Next_ClassView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Instructor</span>"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
            comboInstructor = new ComponentArt.Web.UI.ComboBox();
            comboInstructor.CssClass = "comboBox";
            comboInstructor.HoverCssClass = "comboBoxHover";
            comboInstructor.FocusedCssClass = "comboBoxHover";
            comboInstructor.TextBoxCssClass = "comboTextBox";
            comboInstructor.DropDownCssClass = "comboDropDown";
            comboInstructor.ItemCssClass = "comboItem";
            comboInstructor.ItemHoverCssClass = "comboItemHover";
            comboInstructor.SelectedItemCssClass = "comboItemHover";
            comboInstructor.DropHoverImageUrl = "combobox_images/drop_hover.gif";
            comboInstructor.DropImageUrl = "combobox_images/drop.gif";
            comboInstructor.Width = Unit.Pixel(300);
            Next_ClassView.Controls.Add(comboInstructor);
            Next_ClassView.Controls.Add(new LiteralControl("</span></div>"));

            Next_ClassView.Controls.Add(new LiteralControl("<div class=\"inputrow\">"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputlabel\">Location</span>"));
            Next_ClassView.Controls.Add(new LiteralControl("<span class=\"inputfield\">"));
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
            Next_ClassView.Controls.Add(comboLocation);
            Next_ClassView.Controls.Add(new LiteralControl("</span></div>"));

            #endregion

            Panel buttons = new Panel();
            buttons.CssClass = "pButtons";
            content.Controls.Add(buttons);

            btOk = new Button();
            btOk.Text = Localization.Strings.OK;
            btOk.Width = Unit.Pixel(72);
            btOk.EnableViewState = false;
            btOk.Click += new EventHandler(ok_Click);
            buttons.Controls.Add(btOk);

            btCancel = new Button();
            btCancel.Text = Localization.Strings.Cancel;
            btCancel.Width = Unit.Pixel(72);
            btCancel.EnableViewState = false;
            btCancel.CausesValidation = false;
            btCancel.Click += new EventHandler(cancel_Click);
            buttons.Controls.Add(btCancel);

            btDelete = new Button();
            btDelete.Text = Localization.Strings.Delete;
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
            #region Bind Access Control Child Data

            DojoAccessControlGroupManager accessControlGroupManager = new DojoAccessControlGroupManager();
            DojoAccessControlGroupCollection accessControlGroupCollection = accessControlGroupManager.GetCollection(string.Empty, string.Empty, null);
            foreach (DojoAccessControlGroup accessControlGroup in accessControlGroupCollection)
            {
                ComponentArt.Web.UI.ComboBoxItem i = new ComponentArt.Web.UI.ComboBoxItem();
                i.Text = accessControlGroup.ToString();
                i.Value = accessControlGroup.ID.ToString();
                comboAccessControlGroup.Items.Add(i);
            }

            #endregion

            #region Bind Next Class Child Data

            DojoMemberManager instructorManager = new DojoMemberManager();
            DojoMemberCollection instructorCollection = 
                instructorManager.GetCollection("IsInstructor=true", 
                "LastName, FirstName, MiddleName", 
                DojoMemberFlags.PrivateContact);
            foreach (DojoMember instructor in instructorCollection)
            {
                ComponentArt.Web.UI.ComboBoxItem i = new ComponentArt.Web.UI.ComboBoxItem();
                i.Text = instructor.PrivateContact.FullName;
                i.Value = instructor.ID.ToString();
                comboInstructor.Items.Add(i);
            }

            GreyFoxContactManager locationManager = new GreyFoxContactManager("kitTessen_Locations");
            GreyFoxContactCollection locationCollection = locationManager.GetCollection(string.Empty, string.Empty);
            foreach (GreyFoxContact location in locationCollection)
            {
                ComponentArt.Web.UI.ComboBoxItem i = new ComponentArt.Web.UI.ComboBoxItem();
                i.Text = location.BusinessName;
                i.Value = location.ID.ToString();
                comboLocation.Items.Add(i);
            }

            #endregion

        }

        #region Events

        protected void ok_Click(object sender, EventArgs e)
        {
            if (dojoClassDefinitionID == 0)
                obj = new DojoClassDefinition();
            else
                obj = new DojoClassDefinition(dojoClassDefinitionID);

            obj.Name = tbName.Text;
            obj.Description = tbDescription.Text;
            obj.IsDisabled = cbIsDisabled.Checked;
            obj.OccupancyAvg = int.Parse(tbOccupancyAvg.Text);
            obj.OccupancyAvgDate = DateTime.Parse(tbOccupancyAvgDate.Text);
            if (comboAccessControlGroup.SelectedItem != null && comboAccessControlGroup.SelectedItem.Value != "Null")
                obj.AccessControlGroup = DojoAccessControlGroup.NewPlaceHolder(
                    int.Parse(comboAccessControlGroup.SelectedItem.Value));
            else
                obj.AccessControlGroup = null;

            obj.RecurrenceType = 
                (DojoRecurrenceType)Enum.Parse(typeof(DojoRecurrenceType), 
                tbRecurrenceType.Text);
            obj.RecurrenceCount = int.Parse(tbRecurrenceCount.Text);
            obj.RecurrenceEnd = DateTime.Parse(tbRecurrenceEnd.Text);
            obj.RecurrenceSpan = TimeSpan.Parse(tbRecurrenceSpan.Text);
            obj.NextSigninStart = DateTime.Parse(tbNextSigninStart.Text);
            obj.NextSigninEnd = DateTime.Parse(tbNextSigninEnd.Text);
            obj.NextClassStart = DateTime.Parse(tbNextClassStart.Text);
            obj.NextClassEnd = DateTime.Parse(tbNextClassEnd.Text);
            if (comboInstructor.SelectedItem != null && comboInstructor.SelectedItem.Value != "Null")
                obj.Instructor = DojoMember.NewPlaceHolder(
                    int.Parse(comboInstructor.SelectedItem.Value));
            else
                obj.Instructor = null;

            if (comboLocation.SelectedItem != null && comboLocation.SelectedItem.Value != "Null")
                obj.Location = GreyFoxContact.NewPlaceHolder("kitTessen_Locations",
                    int.Parse(comboLocation.SelectedItem.Value));
            else
                obj.Location = null;

            if (editOnAdd)
                dojoClassDefinitionID = obj.Save();
            else
                obj.Save();

            if (resetOnAdd)
            {
                tbName.Text = string.Empty;
                tbDescription.Text = string.Empty;
                cbIsDisabled.Checked = false;
                tbOccupancyAvg.Text = string.Empty;
                tbOccupancyAvgDate.Text = DateTime.Now.ToString();
                tbRecurrenceType.Text = string.Empty;
                tbRecurrenceCount.Text = string.Empty;
                tbRecurrenceEnd.Text = DateTime.Now.ToString();
                tbRecurrenceSpan.Text = string.Empty;
                tbNextSigninStart.Text = DateTime.Now.ToString();
                tbNextSigninEnd.Text = DateTime.Now.ToString();
                tbNextClassStart.Text = DateTime.Now.ToString();
                tbNextClassEnd.Text = DateTime.Now.ToString();
                comboAccessControlGroup.SelectedIndex = 0;
                comboInstructor.SelectedIndex = 0;
                comboLocation.SelectedIndex = 0;
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
                tabstrip.SelectedTab = tabstrip.Tabs[0];

                if (dojoClassDefinitionID > 0)
                {
                    obj = new DojoClassDefinition(dojoClassDefinitionID);
                    headerText.Text = "Edit  - " + obj.ToString();
                }
                else if (dojoClassDefinitionID <= 0)
                {
                    obj = new DojoClassDefinition();
                    headerText.Text = "Add ";
                }

                // Bind Default Data
                tbName.Text = obj.Name;
                tbDescription.Text = obj.Description;
                cbIsDisabled.Checked = obj.IsDisabled;
                tbOccupancyAvg.Text = obj.OccupancyAvg.ToString();
                tbOccupancyAvgDate.Text = obj.OccupancyAvgDate.ToString();

                // Bind Access Control Data
                if (obj.AccessControlGroup != null)
                    foreach (ComboBoxItem item in comboAccessControlGroup.Items)
                        item.Selected = obj.AccessControlGroup.ID.ToString() == item.Value;
                else
                    comboAccessControlGroup.SelectedIndex = 0;

                // Bind Recurrency Data
                tbRecurrenceType.Text = obj.RecurrenceType.ToString();
                tbRecurrenceCount.Text = obj.RecurrenceCount.ToString();
                tbRecurrenceEnd.Text = obj.RecurrenceEnd.ToString();
                tbRecurrenceSpan.Text = obj.RecurrenceSpan.ToString();

                // Bind Next Class Data
                tbNextSigninStart.Text = obj.NextSigninStart.ToString();
                tbNextSigninEnd.Text = obj.NextSigninEnd.ToString();
                tbNextClassStart.Text = obj.NextClassStart.ToString();
                tbNextClassEnd.Text = obj.NextClassEnd.ToString();
                                             
                if (obj.Instructor != null)
                {
                    comboInstructor.Text = obj.Instructor.PrivateContact.FullName;
                    foreach (ComboBoxItem item in comboInstructor.Items)
                    {
                        if (item.Value == obj.Instructor.ID.ToString())
                        {
                            comboInstructor.SelectedItem = item;
                            break;
                        }
                    }
                }
                                
                if (obj.Location != null)
                {
                    comboLocation.Text = obj.Location.BusinessName;
                    foreach (ComboBoxItem item in comboLocation.Items)
                    {
                        if(item.Value == obj.Location.ID.ToString())
                        {
                            comboLocation.SelectedItem = item;
                            break;
                        }
                    }
                }
                    
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
                    dojoClassDefinitionID = (int)myState[1];
            }
        }

        protected override object SaveViewState()
        {
            object baseState = base.SaveViewState();
            object[] myState = new object[2];
            myState[0] = baseState;
            myState[1] = dojoClassDefinitionID;
            return myState;
        }
    }
}
