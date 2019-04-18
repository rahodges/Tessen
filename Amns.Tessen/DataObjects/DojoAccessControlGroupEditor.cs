using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoAccessControlGroup.
	/// </summary>
	[ToolboxData("<{0}:DojoAccessControlGroupEditor runat=server></{0}:DojoAccessControlGroupEditor>")]
	public class DojoAccessControlGroupEditor : TableWindow, INamingContainer
	{
		private int dojoAccessControlGroupID;
		private DojoAccessControlGroup obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for _system Folder

		private Literal ltCreateDate = new Literal();
		private Literal ltModifyDate = new Literal();

		#endregion

		#region Private Control Fields for General Folder

		private TextBox tbName = new TextBox();
		private TextBox tbDescription = new TextBox();
		private TextBox tbOrderNum = new TextBox();
		private RegularExpressionValidator revOrderNum = new RegularExpressionValidator();

		#endregion

		#region Private Control Fields for Allowed Folder

		private MultiSelectBox msAllowedMemberType1 = new MultiSelectBox();
		private MultiSelectBox msAllowedMemberType2 = new MultiSelectBox();
		private MultiSelectBox msAllowedMemberType3 = new MultiSelectBox();
		private MultiSelectBox msAllowedMemberType4 = new MultiSelectBox();
		private MultiSelectBox msAllowedMemberType5 = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Denied Folder

		private MultiSelectBox msDeniedMemberType1 = new MultiSelectBox();
		private MultiSelectBox msDeniedMemberType2 = new MultiSelectBox();
		private MultiSelectBox msDeniedMemberType3 = new MultiSelectBox();
		private MultiSelectBox msDeniedMemberType4 = new MultiSelectBox();
		private MultiSelectBox msDeniedMemberType5 = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoAccessControlGroupID
		{
			get
			{
				return dojoAccessControlGroupID;
			}
			set
			{
				loadFlag = true;
				dojoAccessControlGroupID = value;
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
			Controls.Clear();
			bindDropDownLists();

			#region Child Controls for _system Folder

			ltCreateDate.EnableViewState = false;
			Controls.Add(ltCreateDate);

			ltModifyDate.EnableViewState = false;
			Controls.Add(ltModifyDate);

			#endregion

			#region Child Controls for General Folder

			tbName.EnableViewState = false;
			Controls.Add(tbName);

			tbDescription.EnableViewState = false;
			Controls.Add(tbDescription);

			tbOrderNum.ID = this.ID + "_OrderNum";
			tbOrderNum.EnableViewState = false;
			Controls.Add(tbOrderNum);
			revOrderNum.ControlToValidate = tbOrderNum.ID;
			revOrderNum.ValidationExpression = "^(\\+|-)?\\d+$";
			revOrderNum.ErrorMessage = "*";
			revOrderNum.Display = ValidatorDisplay.Dynamic;
			revOrderNum.EnableViewState = false;
			Controls.Add(revOrderNum);

			#endregion

			#region Child Controls for Allowed Folder

			msAllowedMemberType1.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msAllowedMemberType1);

			msAllowedMemberType2.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msAllowedMemberType2);

			msAllowedMemberType3.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msAllowedMemberType3);

			msAllowedMemberType4.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msAllowedMemberType4);

			msAllowedMemberType5.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msAllowedMemberType5);

			#endregion

			#region Child Controls for Denied Folder

			msDeniedMemberType1.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msDeniedMemberType1);

			msDeniedMemberType2.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msDeniedMemberType2);

			msDeniedMemberType3.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msDeniedMemberType3);

			msDeniedMemberType4.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msDeniedMemberType4);

			msDeniedMemberType5.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msDeniedMemberType5);

			#endregion

			btOk.Text = "OK";
			btOk.Width = Unit.Pixel(72);
			btOk.EnableViewState = false;
			btOk.Click += new EventHandler(ok_Click);
			Controls.Add(btOk);

			btCancel.Text = "Cancel";
			btCancel.Width = Unit.Pixel(72);
			btCancel.EnableViewState = false;
			btCancel.CausesValidation = false;
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
			#region Bind Allowed Child Data

			msAllowedMemberType1.Items.Add(new ListItem("Null", "Null"));
			DojoAttendanceEntryManager allowedMemberType1Manager = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection allowedMemberType1Collection = allowedMemberType1Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAttendanceEntry allowedMemberType1 in allowedMemberType1Collection)
			{
				ListItem i = new ListItem(allowedMemberType1.ToString(), allowedMemberType1.ID.ToString());
				msAllowedMemberType1.Items.Add(i);
			}

			msAllowedMemberType2.Items.Add(new ListItem("Null", "Null"));
			DojoAttendanceEntryManager allowedMemberType2Manager = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection allowedMemberType2Collection = allowedMemberType2Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAttendanceEntry allowedMemberType2 in allowedMemberType2Collection)
			{
				ListItem i = new ListItem(allowedMemberType2.ToString(), allowedMemberType2.ID.ToString());
				msAllowedMemberType2.Items.Add(i);
			}

			msAllowedMemberType3.Items.Add(new ListItem("Null", "Null"));
			DojoAttendanceEntryManager allowedMemberType3Manager = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection allowedMemberType3Collection = allowedMemberType3Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAttendanceEntry allowedMemberType3 in allowedMemberType3Collection)
			{
				ListItem i = new ListItem(allowedMemberType3.ToString(), allowedMemberType3.ID.ToString());
				msAllowedMemberType3.Items.Add(i);
			}

			msAllowedMemberType4.Items.Add(new ListItem("Null", "Null"));
			DojoAttendanceEntryManager allowedMemberType4Manager = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection allowedMemberType4Collection = allowedMemberType4Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAttendanceEntry allowedMemberType4 in allowedMemberType4Collection)
			{
				ListItem i = new ListItem(allowedMemberType4.ToString(), allowedMemberType4.ID.ToString());
				msAllowedMemberType4.Items.Add(i);
			}

			msAllowedMemberType5.Items.Add(new ListItem("Null", "Null"));
			DojoAttendanceEntryManager allowedMemberType5Manager = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection allowedMemberType5Collection = allowedMemberType5Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAttendanceEntry allowedMemberType5 in allowedMemberType5Collection)
			{
				ListItem i = new ListItem(allowedMemberType5.ToString(), allowedMemberType5.ID.ToString());
				msAllowedMemberType5.Items.Add(i);
			}

			#endregion

			#region Bind Denied Child Data

			msDeniedMemberType1.Items.Add(new ListItem("Null", "Null"));
			DojoAttendanceEntryManager deniedMemberType1Manager = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection deniedMemberType1Collection = deniedMemberType1Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAttendanceEntry deniedMemberType1 in deniedMemberType1Collection)
			{
				ListItem i = new ListItem(deniedMemberType1.ToString(), deniedMemberType1.ID.ToString());
				msDeniedMemberType1.Items.Add(i);
			}

			msDeniedMemberType2.Items.Add(new ListItem("Null", "Null"));
			DojoAttendanceEntryManager deniedMemberType2Manager = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection deniedMemberType2Collection = deniedMemberType2Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAttendanceEntry deniedMemberType2 in deniedMemberType2Collection)
			{
				ListItem i = new ListItem(deniedMemberType2.ToString(), deniedMemberType2.ID.ToString());
				msDeniedMemberType2.Items.Add(i);
			}

			msDeniedMemberType3.Items.Add(new ListItem("Null", "Null"));
			DojoAttendanceEntryManager deniedMemberType3Manager = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection deniedMemberType3Collection = deniedMemberType3Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAttendanceEntry deniedMemberType3 in deniedMemberType3Collection)
			{
				ListItem i = new ListItem(deniedMemberType3.ToString(), deniedMemberType3.ID.ToString());
				msDeniedMemberType3.Items.Add(i);
			}

			msDeniedMemberType4.Items.Add(new ListItem("Null", "Null"));
			DojoAttendanceEntryManager deniedMemberType4Manager = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection deniedMemberType4Collection = deniedMemberType4Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAttendanceEntry deniedMemberType4 in deniedMemberType4Collection)
			{
				ListItem i = new ListItem(deniedMemberType4.ToString(), deniedMemberType4.ID.ToString());
				msDeniedMemberType4.Items.Add(i);
			}

			msDeniedMemberType5.Items.Add(new ListItem("Null", "Null"));
			DojoAttendanceEntryManager deniedMemberType5Manager = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection deniedMemberType5Collection = deniedMemberType5Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoAttendanceEntry deniedMemberType5 in deniedMemberType5Collection)
			{
				ListItem i = new ListItem(deniedMemberType5.ToString(), deniedMemberType5.ID.ToString());
				msDeniedMemberType5.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoAccessControlGroupID == 0)
				obj = new DojoAccessControlGroup();
			else
				obj = new DojoAccessControlGroup(dojoAccessControlGroupID);

			obj.Name = tbName.Text;
			obj.Description = tbDescription.Text;
			obj.OrderNum = int.Parse(tbOrderNum.Text);

			if(msAllowedMemberType1.SelectedItem != null && msAllowedMemberType1.SelectedItem.Value != "Null")
				obj.AllowedMemberType1 = DojoAttendanceEntry.NewPlaceHolder(
					int.Parse(msAllowedMemberType1.SelectedItem.Value));
			else
				obj.AllowedMemberType1 = null;

			if(msAllowedMemberType2.SelectedItem != null && msAllowedMemberType2.SelectedItem.Value != "Null")
				obj.AllowedMemberType2 = DojoAttendanceEntry.NewPlaceHolder(
					int.Parse(msAllowedMemberType2.SelectedItem.Value));
			else
				obj.AllowedMemberType2 = null;

			if(msAllowedMemberType3.SelectedItem != null && msAllowedMemberType3.SelectedItem.Value != "Null")
				obj.AllowedMemberType3 = DojoAttendanceEntry.NewPlaceHolder(
					int.Parse(msAllowedMemberType3.SelectedItem.Value));
			else
				obj.AllowedMemberType3 = null;

			if(msAllowedMemberType4.SelectedItem != null && msAllowedMemberType4.SelectedItem.Value != "Null")
				obj.AllowedMemberType4 = DojoAttendanceEntry.NewPlaceHolder(
					int.Parse(msAllowedMemberType4.SelectedItem.Value));
			else
				obj.AllowedMemberType4 = null;

			if(msAllowedMemberType5.SelectedItem != null && msAllowedMemberType5.SelectedItem.Value != "Null")
				obj.AllowedMemberType5 = DojoAttendanceEntry.NewPlaceHolder(
					int.Parse(msAllowedMemberType5.SelectedItem.Value));
			else
				obj.AllowedMemberType5 = null;

			if(msDeniedMemberType1.SelectedItem != null && msDeniedMemberType1.SelectedItem.Value != "Null")
				obj.DeniedMemberType1 = DojoAttendanceEntry.NewPlaceHolder(
					int.Parse(msDeniedMemberType1.SelectedItem.Value));
			else
				obj.DeniedMemberType1 = null;

			if(msDeniedMemberType2.SelectedItem != null && msDeniedMemberType2.SelectedItem.Value != "Null")
				obj.DeniedMemberType2 = DojoAttendanceEntry.NewPlaceHolder(
					int.Parse(msDeniedMemberType2.SelectedItem.Value));
			else
				obj.DeniedMemberType2 = null;

			if(msDeniedMemberType3.SelectedItem != null && msDeniedMemberType3.SelectedItem.Value != "Null")
				obj.DeniedMemberType3 = DojoAttendanceEntry.NewPlaceHolder(
					int.Parse(msDeniedMemberType3.SelectedItem.Value));
			else
				obj.DeniedMemberType3 = null;

			if(msDeniedMemberType4.SelectedItem != null && msDeniedMemberType4.SelectedItem.Value != "Null")
				obj.DeniedMemberType4 = DojoAttendanceEntry.NewPlaceHolder(
					int.Parse(msDeniedMemberType4.SelectedItem.Value));
			else
				obj.DeniedMemberType4 = null;

			if(msDeniedMemberType5.SelectedItem != null && msDeniedMemberType5.SelectedItem.Value != "Null")
				obj.DeniedMemberType5 = DojoAttendanceEntry.NewPlaceHolder(
					int.Parse(msDeniedMemberType5.SelectedItem.Value));
			else
				obj.DeniedMemberType5 = null;

			if(editOnAdd)
				dojoAccessControlGroupID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbDescription.Text = string.Empty;
				tbOrderNum.Text = string.Empty;
				msAllowedMemberType1.SelectedIndex = 0;
				msAllowedMemberType2.SelectedIndex = 0;
				msAllowedMemberType3.SelectedIndex = 0;
				msAllowedMemberType4.SelectedIndex = 0;
				msAllowedMemberType5.SelectedIndex = 0;
				msDeniedMemberType1.SelectedIndex = 0;
				msDeniedMemberType2.SelectedIndex = 0;
				msDeniedMemberType3.SelectedIndex = 0;
				msDeniedMemberType4.SelectedIndex = 0;
				msDeniedMemberType5.SelectedIndex = 0;
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
			GeneralTab.Visible = true;
			GeneralTab.RenderDiv += new TabRenderHandler(renderGeneralFolder);
			tabStrip.Tabs.Add(GeneralTab);

			Tab AllowedTab = new Tab("Allowed");
			AllowedTab.RenderDiv += new TabRenderHandler(renderAllowedFolder);
			tabStrip.Tabs.Add(AllowedTab);

			Tab DeniedTab = new Tab("Denied");
			DeniedTab.RenderDiv += new TabRenderHandler(renderDeniedFolder);
			tabStrip.Tabs.Add(DeniedTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoAccessControlGroupID > 0)
				{
					obj = new DojoAccessControlGroup(dojoAccessControlGroupID);
					text = "Edit kitTessen_AccessControlGroups - " + obj.ToString();
				}
				else if(dojoAccessControlGroupID <= 0)
				{
					obj = new DojoAccessControlGroup();
					text = "Add kitTessen_AccessControlGroups";
				}

				//
				// Set Field Entries
				//
				ltCreateDate.Text = obj.CreateDate.ToString();
				ltModifyDate.Text = obj.ModifyDate.ToString();
				tbName.Text = obj.Name;
				tbDescription.Text = obj.Description;
				tbOrderNum.Text = obj.OrderNum.ToString();

				//
				// Set Children Selections
				//
				if(obj.AllowedMemberType1 != null)
					foreach(ListItem item in msAllowedMemberType1.Items)
						item.Selected = obj.AllowedMemberType1.ID.ToString() == item.Value;
					else
						msAllowedMemberType1.SelectedIndex = 0;

				if(obj.AllowedMemberType2 != null)
					foreach(ListItem item in msAllowedMemberType2.Items)
						item.Selected = obj.AllowedMemberType2.ID.ToString() == item.Value;
					else
						msAllowedMemberType2.SelectedIndex = 0;

				if(obj.AllowedMemberType3 != null)
					foreach(ListItem item in msAllowedMemberType3.Items)
						item.Selected = obj.AllowedMemberType3.ID.ToString() == item.Value;
					else
						msAllowedMemberType3.SelectedIndex = 0;

				if(obj.AllowedMemberType4 != null)
					foreach(ListItem item in msAllowedMemberType4.Items)
						item.Selected = obj.AllowedMemberType4.ID.ToString() == item.Value;
					else
						msAllowedMemberType4.SelectedIndex = 0;

				if(obj.AllowedMemberType5 != null)
					foreach(ListItem item in msAllowedMemberType5.Items)
						item.Selected = obj.AllowedMemberType5.ID.ToString() == item.Value;
					else
						msAllowedMemberType5.SelectedIndex = 0;

				if(obj.DeniedMemberType1 != null)
					foreach(ListItem item in msDeniedMemberType1.Items)
						item.Selected = obj.DeniedMemberType1.ID.ToString() == item.Value;
					else
						msDeniedMemberType1.SelectedIndex = 0;

				if(obj.DeniedMemberType2 != null)
					foreach(ListItem item in msDeniedMemberType2.Items)
						item.Selected = obj.DeniedMemberType2.ID.ToString() == item.Value;
					else
						msDeniedMemberType2.SelectedIndex = 0;

				if(obj.DeniedMemberType3 != null)
					foreach(ListItem item in msDeniedMemberType3.Items)
						item.Selected = obj.DeniedMemberType3.ID.ToString() == item.Value;
					else
						msDeniedMemberType3.SelectedIndex = 0;

				if(obj.DeniedMemberType4 != null)
					foreach(ListItem item in msDeniedMemberType4.Items)
						item.Selected = obj.DeniedMemberType4.ID.ToString() == item.Value;
					else
						msDeniedMemberType4.SelectedIndex = 0;

				if(obj.DeniedMemberType5 != null)
					foreach(ListItem item in msDeniedMemberType5.Items)
						item.Selected = obj.DeniedMemberType5.ID.ToString() == item.Value;
					else
						msDeniedMemberType5.SelectedIndex = 0;

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

		private void render_systemFolder(HtmlTextWriter output)
		{
			//
			// Render CreateDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CreateDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltCreateDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ModifyDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ModifyDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltModifyDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderGeneralFolder(HtmlTextWriter output)
		{
			//
			// Render Name
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Name");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbName.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Description
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Description");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbDescription.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OrderNum
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OrderNum");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbOrderNum.RenderControl(output);
			revOrderNum.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderAllowedFolder(HtmlTextWriter output)
		{
			//
			// Render AllowedMemberType1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowedMemberType1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msAllowedMemberType1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowedMemberType2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowedMemberType2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msAllowedMemberType2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowedMemberType3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowedMemberType3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msAllowedMemberType3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowedMemberType4
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowedMemberType4");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msAllowedMemberType4.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowedMemberType5
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowedMemberType5");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msAllowedMemberType5.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderDeniedFolder(HtmlTextWriter output)
		{
			//
			// Render DeniedMemberType1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DeniedMemberType1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msDeniedMemberType1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DeniedMemberType2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DeniedMemberType2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msDeniedMemberType2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DeniedMemberType3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DeniedMemberType3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msDeniedMemberType3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DeniedMemberType4
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DeniedMemberType4");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msDeniedMemberType4.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DeniedMemberType5
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DeniedMemberType5");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msDeniedMemberType5.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		protected override void LoadViewState(object savedState)
		{
			if(savedState != null)
			{
				object[] myState = (object[]) savedState;
				if(myState[0] != null)
					base.LoadViewState(myState[0]);
				if(myState[1] != null)
					dojoAccessControlGroupID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoAccessControlGroupID;
			return myState;
		}
	}
}

