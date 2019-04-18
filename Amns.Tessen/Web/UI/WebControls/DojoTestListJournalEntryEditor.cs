using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoTestListJournalEntry.
	/// </summary>
	[DefaultProperty("ConnectionString"), 
		ToolboxData("<{0}:DojoTestListJournalEntryEditor runat=server></{0}:DojoTestListJournalEntryEditor>")]
	public class DojoTestListJournalEntryEditor : TableWindow, INamingContainer
	{
		private int dojoTestListJournalEntryID;
		private DojoTestListJournalEntry obj;
		private string connectionString;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for General Folder

		private MultiSelectBox msTestList = new MultiSelectBox();
		private MultiSelectBox msMember = new MultiSelectBox();
		private MultiSelectBox msEntryType = new MultiSelectBox();

		#endregion

		#region Private Control Fields for _system Folder

		private Literal ltCreateDate = new Literal();

		#endregion

		#region Private Control Fields for Details Folder

		private TextBox tbComment = new TextBox();
		private MultiSelectBox msEditor = new MultiSelectBox();
		private MultiSelectBox msPromotion = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoTestListJournalEntryID
		{
			get
			{
				return dojoTestListJournalEntryID;
			}
			set
			{
				loadFlag = true;
				dojoTestListJournalEntryID = value;
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

			msTestList.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msTestList);

			msMember.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMember);

			msEntryType.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msEntryType);

			#endregion

			#region Child Controls for _system Folder

			ltCreateDate.EnableViewState = false;
			Controls.Add(ltCreateDate);

			#endregion

			#region Child Controls for Details Folder

			msEditor.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msEditor);

			tbComment.EnableViewState = false;
			Controls.Add(tbComment);

			msPromotion.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msPromotion);

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

			msTestList.Items.Add(new ListItem("Null", "Null"));
			DojoTestListManager testListManager = new DojoTestListManager();
			DojoTestListCollection testListCollection = testListManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestList testList in testListCollection)
			{
				ListItem i = new ListItem(testList.ToString(), testList.ID.ToString());
				msTestList.Items.Add(i);
			}

			msMember.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager memberManager = new DojoMemberManager();
			DojoMemberCollection memberCollection = memberManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember member in memberCollection)
			{
				ListItem i = new ListItem(member.ToString(), member.ID.ToString());
				msMember.Items.Add(i);
			}

			msEntryType.Items.Add(new ListItem("Null", "Null"));
			DojoTestListJournalEntryTypeManager entryTypeManager = new DojoTestListJournalEntryTypeManager();
			DojoTestListJournalEntryTypeCollection entryTypeCollection = entryTypeManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListJournalEntryType entryType in entryTypeCollection)
			{
				ListItem i = new ListItem(entryType.ToString(), entryType.ID.ToString());
				msEntryType.Items.Add(i);
			}

			#endregion

			#region Bind Details Child Data

			msEditor.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager editorManager = new DojoMemberManager();
			DojoMemberCollection editorCollection = editorManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember editor in editorCollection)
			{
				ListItem i = new ListItem(editor.ToString(), editor.ID.ToString());
				msEditor.Items.Add(i);
			}

			msPromotion.Items.Add(new ListItem("Null", "Null"));
			DojoPromotionManager promotionManager = new DojoPromotionManager();
			DojoPromotionCollection promotionCollection = promotionManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoPromotion promotion in promotionCollection)
			{
				ListItem i = new ListItem(promotion.ToString(), promotion.ID.ToString());
				msPromotion.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoTestListJournalEntryID == 0)
				obj = new DojoTestListJournalEntry();
			else
				obj = new DojoTestListJournalEntry(dojoTestListJournalEntryID);

			obj.Comment = tbComment.Text;

			if(msTestList.SelectedItem != null && msTestList.SelectedItem.Value != "Null")
				obj.TestList = DojoTestList.NewPlaceHolder( 
					int.Parse(msTestList.SelectedItem.Value));
			else
				obj.TestList = null;

			if(msMember.SelectedItem != null && msMember.SelectedItem.Value != "Null")
				obj.Member = DojoMember.NewPlaceHolder(
					int.Parse(msMember.SelectedItem.Value));
			else
				obj.Member = null;

			if(msEntryType.SelectedItem != null && msEntryType.SelectedItem.Value != "Null")
				obj.EntryType = DojoTestListJournalEntryType.NewPlaceHolder( 
					int.Parse(msEntryType.SelectedItem.Value));
			else
				obj.EntryType = null;

			if(msEditor.SelectedItem != null && msEditor.SelectedItem.Value != "Null")
				obj.Editor = DojoMember.NewPlaceHolder(
					int.Parse(msEditor.SelectedItem.Value));
			else
				obj.Editor = null;

			if(msPromotion.SelectedItem != null && msPromotion.SelectedItem.Value != "Null")
				obj.Promotion = DojoPromotion.NewPlaceHolder( 
					int.Parse(msPromotion.SelectedItem.Value));
			else
				obj.Promotion = null;

			if(editOnAdd)
				dojoTestListJournalEntryID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbComment.Text = string.Empty;
				msTestList.SelectedIndex = 0;
				msMember.SelectedIndex = 0;
				msEntryType.SelectedIndex = 0;
				msEditor.SelectedIndex = 0;
				msPromotion.SelectedIndex = 0;
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
			GeneralTab.Visible = true;
			tabStrip.Tabs.Add(GeneralTab);

			Tab DetailsTab = new Tab("Details");
			DetailsTab.RenderDiv += new TabRenderHandler(renderDetailsFolder);
			tabStrip.Tabs.Add(DetailsTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoTestListJournalEntryID > 0)
				{
					obj = new DojoTestListJournalEntry(dojoTestListJournalEntryID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoTestListJournalEntryID <= 0)
				{
					obj = new DojoTestListJournalEntry();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				ltCreateDate.Text = obj.CreateDate.ToString();
				tbComment.Text = obj.Comment;

				//
				// Set Children Selections
				//
				if(obj.TestList != null)
					foreach(ListItem item in msTestList.Items)
						item.Selected = obj.TestList.ID.ToString() == item.Value;
					else
						msTestList.SelectedIndex = 0;

				if(obj.Member != null)
					foreach(ListItem item in msMember.Items)
						item.Selected = obj.Member.ID.ToString() == item.Value;
					else
						msMember.SelectedIndex = 0;

				if(obj.EntryType != null)
					foreach(ListItem item in msEntryType.Items)
						item.Selected = obj.EntryType.ID.ToString() == item.Value;
					else
						msEntryType.SelectedIndex = 0;

				if(obj.Editor != null)
					foreach(ListItem item in msEditor.Items)
						item.Selected = obj.Editor.ID.ToString() == item.Value;
					else
						msEditor.SelectedIndex = 0;

				if(obj.Promotion != null)
					foreach(ListItem item in msPromotion.Items)
						item.Selected = obj.Promotion.ID.ToString() == item.Value;
					else
						msPromotion.SelectedIndex = 0;

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

		private void renderGeneralFolder(HtmlTextWriter output)
		{
			//
			// Render TestList
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("TestList");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msTestList.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Member
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Member");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMember.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render EntryType
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("EntryType");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msEntryType.RenderControl(output);
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

		}

		private void renderDetailsFolder(HtmlTextWriter output)
		{
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
			// Render Comment
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Comment");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbComment.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Promotion
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Promotion");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msPromotion.RenderControl(output);
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
					dojoTestListJournalEntryID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoTestListJournalEntryID;
			return myState;
		}
	}
}

