using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoAttendanceEntry.
	/// </summary>
	[ToolboxData("<{0}:DojoAttendanceEntryEditor runat=server></{0}:DojoAttendanceEntryEditor>")]
	public class DojoAttendanceEntryEditor : TableWindow, INamingContainer
	{
		private int dojoAttendanceEntryID;
		private DojoAttendanceEntry obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for Default Folder

		private TextBox tbSigninTime = new TextBox();
		private TextBox tbSignoutTime = new TextBox();
		private MultiSelectBox msClass = new MultiSelectBox();
		private MultiSelectBox msMember = new MultiSelectBox();
		private MultiSelectBox msRank = new MultiSelectBox();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

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
			Controls.Clear();
			bindDropDownLists();

			#region Child Controls for Default Folder

			tbSigninTime.EnableViewState = false;
			Controls.Add(tbSigninTime);

			tbSignoutTime.EnableViewState = false;
			Controls.Add(tbSignoutTime);

			msClass.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msClass);

			msMember.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMember);

			msRank.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msRank);

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
			#region Bind Default Child Data

			msClass.Items.Add(new ListItem("Null", "Null"));
			DojoClassManager _classManager = new DojoClassManager();
			DojoClassCollection _classCollection = _classManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoClass _class in _classCollection)
			{
				ListItem i = new ListItem(_class.ToString(), _class.ID.ToString());
				msClass.Items.Add(i);
			}

			msMember.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager memberManager = new DojoMemberManager();
			DojoMemberCollection memberCollection = memberManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember member in memberCollection)
			{
				ListItem i = new ListItem(member.ToString(), member.ID.ToString());
				msMember.Items.Add(i);
			}

			msRank.Items.Add(new ListItem("Null", "Null"));
			DojoRankManager rankManager = new DojoRankManager();
			DojoRankCollection rankCollection = rankManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoRank rank in rankCollection)
			{
				ListItem i = new ListItem(rank.ToString(), rank.ID.ToString());
				msRank.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoAttendanceEntryID == 0)
				obj = new DojoAttendanceEntry();
			else
				obj = new DojoAttendanceEntry(dojoAttendanceEntryID);

			obj.SigninTime = DateTime.Parse(tbSigninTime.Text);
			obj.SignoutTime = DateTime.Parse(tbSignoutTime.Text);

			if(msClass.SelectedItem != null && msClass.SelectedItem.Value != "Null")
				obj.Class = DojoClass.NewPlaceHolder(
					int.Parse(msClass.SelectedItem.Value));
			else
				obj.Class = null;

			if(msMember.SelectedItem != null && msMember.SelectedItem.Value != "Null")
				obj.Member = DojoMember.NewPlaceHolder(
					int.Parse(msMember.SelectedItem.Value));
			else
				obj.Member = null;

			if(msRank.SelectedItem != null && msRank.SelectedItem.Value != "Null")
				obj.Rank = DojoRank.NewPlaceHolder(
					int.Parse(msRank.SelectedItem.Value));
			else
				obj.Rank = null;

			if(editOnAdd)
				dojoAttendanceEntryID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbSigninTime.Text = DateTime.Now.ToString();
				tbSignoutTime.Text = DateTime.Now.ToString();
				msClass.SelectedIndex = 0;
				msMember.SelectedIndex = 0;
				msRank.SelectedIndex = 0;
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

			Tab DefaultTab = new Tab("Default");
			DefaultTab.Visible = true;
			DefaultTab.RenderDiv += new TabRenderHandler(renderDefaultFolder);
			DefaultTab.Visible = true;
			tabStrip.Tabs.Add(DefaultTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoAttendanceEntryID > 0)
				{
					obj = new DojoAttendanceEntry(dojoAttendanceEntryID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoAttendanceEntryID <= 0)
				{
					obj = new DojoAttendanceEntry();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbSigninTime.Text = obj.SigninTime.ToString();
				tbSignoutTime.Text = obj.SignoutTime.ToString();

				//
				// Set Children Selections
				//
				if(obj.Class != null)
					foreach(ListItem item in msClass.Items)
						item.Selected = obj.Class.ID.ToString() == item.Value;
					else
						msClass.SelectedIndex = 0;

				if(obj.Member != null)
					foreach(ListItem item in msMember.Items)
						item.Selected = obj.Member.ID.ToString() == item.Value;
					else
						msMember.SelectedIndex = 0;

				if(obj.Rank != null)
					foreach(ListItem item in msRank.Items)
						item.Selected = obj.Rank.ID.ToString() == item.Value;
					else
						msRank.SelectedIndex = 0;

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

		private void renderDefaultFolder(HtmlTextWriter output)
		{
			//
			// Render SigninTime
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Signin Time");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbSigninTime.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render SignoutTime
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Signout Time");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbSignoutTime.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msClass.RenderControl(output);
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
			// Render Rank
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Rank");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msRank.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		protected override void LoadViewState(object savedState)
		{
			if(savedState != null)
			{
				object[] myState = (object[]) savedState;
				if(myState[0] != null)
					base.LoadViewState(myState[0]);
				if(myState[1] != null)
					dojoAttendanceEntryID = (int) myState[1];
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

