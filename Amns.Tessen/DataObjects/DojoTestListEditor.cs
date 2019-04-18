using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoTestList.
	/// </summary>
	[ToolboxData("<{0}:DojoTestListEditor runat=server></{0}:DojoTestListEditor>")]
	public class DojoTestListEditor : TableWindow, INamingContainer
	{
		private int dojoTestListID;
		private DojoTestList obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for _system Folder

		private Literal ltCreateDate = new Literal();
		private Literal ltModifyDate = new Literal();

		#endregion

		#region Private Control Fields for General Folder

		private TextBox tbEditorComments = new TextBox();
		private CheckBox cbField1 = new CheckBox();
		private MultiSelectBox msTest = new MultiSelectBox();
		private MultiSelectBox msStatus = new MultiSelectBox();
		private MultiSelectBox msEditor = new MultiSelectBox();

		#endregion

		#region Private Control Fields for System Folder

		private Literal ltCandidatesCompileDate = new Literal();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoTestListID
		{
			get
			{
				return dojoTestListID;
			}
			set
			{
				loadFlag = true;
				dojoTestListID = value;
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

			msTest.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msTest);

			msStatus.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msStatus);

			msEditor.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msEditor);

			tbEditorComments.EnableViewState = false;
			Controls.Add(tbEditorComments);

			cbField1.EnableViewState = false;
			Controls.Add(cbField1);

			#endregion

			#region Child Controls for System Folder


			ltCandidatesCompileDate.EnableViewState = false;
			Controls.Add(ltCandidatesCompileDate);

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
			#region Bind General Child Data

			msTest.Items.Add(new ListItem("Null", "Null"));
			DojoTestManager testManager = new DojoTestManager();
			DojoTestCollection testCollection = testManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTest test in testCollection)
			{
				ListItem i = new ListItem(test.ToString(), test.ID.ToString());
				msTest.Items.Add(i);
			}

			msStatus.Items.Add(new ListItem("Null", "Null"));
			DojoTestListStatusManager statusManager = new DojoTestListStatusManager();
			DojoTestListStatusCollection statusCollection = statusManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListStatus status in statusCollection)
			{
				ListItem i = new ListItem(status.ToString(), status.ID.ToString());
				msStatus.Items.Add(i);
			}

			msEditor.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager editorManager = new DojoMemberManager();
			DojoMemberCollection editorCollection = editorManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember editor in editorCollection)
			{
				ListItem i = new ListItem(editor.ToString(), editor.ID.ToString());
				msEditor.Items.Add(i);
			}

			#endregion

			#region Bind System Child Data

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoTestListID == 0)
				obj = new DojoTestList();
			else
				obj = new DojoTestList(dojoTestListID);

			obj.EditorComments = tbEditorComments.Text;
			obj.Field1 = cbField1.Checked;

			if(msTest.SelectedItem != null && msTest.SelectedItem.Value != "Null")
				obj.Test = DojoTest.NewPlaceHolder(
					int.Parse(msTest.SelectedItem.Value));
			else
				obj.Test = null;

			if(msStatus.SelectedItem != null && msStatus.SelectedItem.Value != "Null")
				obj.Status = DojoTestListStatus.NewPlaceHolder(
					int.Parse(msStatus.SelectedItem.Value));
			else
				obj.Status = null;

			if(msEditor.SelectedItem != null && msEditor.SelectedItem.Value != "Null")
				obj.Editor = DojoMember.NewPlaceHolder(
					int.Parse(msEditor.SelectedItem.Value));
			else
				obj.Editor = null;

			if(editOnAdd)
				dojoTestListID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbEditorComments.Text = string.Empty;
				cbField1.Checked = false;
				msTest.SelectedIndex = 0;
				msStatus.SelectedIndex = 0;
				msEditor.SelectedIndex = 0;
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

			Tab SystemTab = new Tab("System");
			SystemTab.RenderDiv += new TabRenderHandler(renderSystemFolder);
			tabStrip.Tabs.Add(SystemTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoTestListID > 0)
				{
					obj = new DojoTestList(dojoTestListID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoTestListID <= 0)
				{
					obj = new DojoTestList();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				ltCreateDate.Text = obj.CreateDate.ToString();
				ltModifyDate.Text = obj.ModifyDate.ToString();
				tbEditorComments.Text = obj.EditorComments;
				cbField1.Checked = obj.Field1;
				ltCandidatesCompileDate.Text = obj.CandidatesCompileDate.ToString();

				//
				// Set Children Selections
				//
				if(obj.Test != null)
					foreach(ListItem item in msTest.Items)
						item.Selected = obj.Test.ID.ToString() == item.Value;
					else
						msTest.SelectedIndex = 0;

				if(obj.Status != null)
					foreach(ListItem item in msStatus.Items)
						item.Selected = obj.Status.ID.ToString() == item.Value;
					else
						msStatus.SelectedIndex = 0;

				if(obj.Editor != null)
					foreach(ListItem item in msEditor.Items)
						item.Selected = obj.Editor.ID.ToString() == item.Value;
					else
						msEditor.SelectedIndex = 0;

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
			// Render Test
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Test");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msTest.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Status
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Status");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Editor
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Editor");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msEditor.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render EditorComments
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("EditorComments");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbEditorComments.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Field1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Field1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbField1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderSystemFolder(HtmlTextWriter output)
		{
			//
			// Render CandidatesCompileDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CandidatesCompileDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			ltCandidatesCompileDate.RenderControl(output);
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
					dojoTestListID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoTestListID;
			return myState;
		}
	}
}

