using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoTestListStatus.
	/// </summary>
	[DefaultProperty("ConnectionString"), 
		ToolboxData("<{0}:DojoTestListStatusEditor runat=server></{0}:DojoTestListStatusEditor>")]
	public class DojoTestListStatusEditor : TableWindow, INamingContainer
	{
		private int dojoTestListStatusID;
		private DojoTestListStatus obj;
		private string connectionString;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for General Folder

		private TextBox tbName = new TextBox();
		private TextBox tbDescription = new TextBox();
		private TextBox tbOrderNum = new TextBox();
		private RegularExpressionValidator revOrderNum = new RegularExpressionValidator();

		#endregion

		#region Private Control Fields for Flags Folder

		private CheckBox cbIsDraft = new CheckBox();
		private CheckBox cbIsFinal = new CheckBox();
		private CheckBox cbIsComplete = new CheckBox();
		private CheckBox cbTeacherEditingEnabled = new CheckBox();

		#endregion

		#region Private Control Fields for Status_Changes Folder

		private MultiSelectBox msOnFinalized = new MultiSelectBox();
		private MultiSelectBox msOnCompleted = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoTestListStatusID
		{
			get
			{
				return dojoTestListStatusID;
			}
			set
			{
				loadFlag = true;
				dojoTestListStatusID = value;
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
			Controls.Clear();
			bindDropDownLists();

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

			#region Child Controls for Flags Folder

			cbIsDraft.EnableViewState = false;
			Controls.Add(cbIsDraft);

			cbIsFinal.EnableViewState = false;
			Controls.Add(cbIsFinal);

			cbIsComplete.EnableViewState = false;
			Controls.Add(cbIsComplete);

			cbTeacherEditingEnabled.EnableViewState = false;
			Controls.Add(cbTeacherEditingEnabled);

			#endregion

			#region Child Controls for Status Changes Folder

			msOnFinalized.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msOnFinalized);

			msOnCompleted.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msOnCompleted);

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
			#region Bind Status Changes Child Data

			msOnFinalized.Items.Add(new ListItem("Null", "Null"));
			DojoTestListStatusManager onFinalizedManager = new DojoTestListStatusManager();
			DojoTestListStatusCollection onFinalizedCollection = onFinalizedManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListStatus onFinalized in onFinalizedCollection)
			{
				ListItem i = new ListItem(onFinalized.ToString(), onFinalized.ID.ToString());
				msOnFinalized.Items.Add(i);
			}

			msOnCompleted.Items.Add(new ListItem("Null", "Null"));
			DojoTestListStatusManager onCompletedManager = new DojoTestListStatusManager();
			DojoTestListStatusCollection onCompletedCollection = onCompletedManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListStatus onCompleted in onCompletedCollection)
			{
				ListItem i = new ListItem(onCompleted.ToString(), onCompleted.ID.ToString());
				msOnCompleted.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoTestListStatusID == 0)
				obj = new DojoTestListStatus();
			else
				obj = new DojoTestListStatus(dojoTestListStatusID);

			obj.Name = tbName.Text;
			obj.Description = tbDescription.Text;
			obj.OrderNum = int.Parse(tbOrderNum.Text);
			obj.IsDraft = cbIsDraft.Checked;
			obj.IsFinal = cbIsFinal.Checked;
			obj.IsComplete = cbIsComplete.Checked;
			obj.TeacherEditingEnabled = cbTeacherEditingEnabled.Checked;

			if(msOnFinalized.SelectedItem != null && msOnFinalized.SelectedItem.Value != "Null")
				obj.OnFinalized = DojoTestListStatus.NewPlaceHolder( 
					int.Parse(msOnFinalized.SelectedItem.Value));
			else
				obj.OnFinalized = null;

			if(msOnCompleted.SelectedItem != null && msOnCompleted.SelectedItem.Value != "Null")
				obj.OnCompleted = DojoTestListStatus.NewPlaceHolder(
					int.Parse(msOnCompleted.SelectedItem.Value));
			else
				obj.OnCompleted = null;

			if(editOnAdd)
				dojoTestListStatusID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbDescription.Text = string.Empty;
				tbOrderNum.Text = string.Empty;
				cbIsDraft.Checked = false;
				cbIsFinal.Checked = false;
				cbIsComplete.Checked = false;
				cbTeacherEditingEnabled.Checked = false;
				msOnFinalized.SelectedIndex = 0;
				msOnCompleted.SelectedIndex = 0;
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

			Tab FlagsTab = new Tab("Flags");
			FlagsTab.RenderDiv += new TabRenderHandler(renderFlagsFolder);
			tabStrip.Tabs.Add(FlagsTab);

			Tab Status_ChangesTab = new Tab("Status Changes");
			Status_ChangesTab.RenderDiv += new TabRenderHandler(renderStatus_ChangesFolder);
			tabStrip.Tabs.Add(Status_ChangesTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoTestListStatusID > 0)
				{
					obj = new DojoTestListStatus(dojoTestListStatusID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoTestListStatusID <= 0)
				{
					obj = new DojoTestListStatus();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbName.Text = obj.Name;
				tbDescription.Text = obj.Description;
				tbOrderNum.Text = obj.OrderNum.ToString();
				cbIsDraft.Checked = obj.IsDraft;
				cbIsFinal.Checked = obj.IsFinal;
				cbIsComplete.Checked = obj.IsComplete;
				cbTeacherEditingEnabled.Checked = obj.TeacherEditingEnabled;

				//
				// Set Children Selections
				//
				if(obj.OnFinalized != null)
					foreach(ListItem item in msOnFinalized.Items)
						item.Selected = obj.OnFinalized.ID.ToString() == item.Value;
					else
						msOnFinalized.SelectedIndex = 0;

				if(obj.OnCompleted != null)
					foreach(ListItem item in msOnCompleted.Items)
						item.Selected = obj.OnCompleted.ID.ToString() == item.Value;
					else
						msOnCompleted.SelectedIndex = 0;

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

		private void renderFlagsFolder(HtmlTextWriter output)
		{
			//
			// Render IsDraft
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsDraft");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsDraft.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsFinal
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsFinal");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsFinal.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsComplete
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsComplete");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsComplete.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render TeacherEditingEnabled
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("TeacherEditingEnabled");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbTeacherEditingEnabled.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderStatus_ChangesFolder(HtmlTextWriter output)
		{
			//
			// Render OnFinalized
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnFinalized");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msOnFinalized.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OnCompleted
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OnCompleted");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msOnCompleted.RenderControl(output);
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
					dojoTestListStatusID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoTestListStatusID;
			return myState;
		}
	}
}

