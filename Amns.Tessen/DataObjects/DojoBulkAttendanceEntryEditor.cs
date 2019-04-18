using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoBulkAttendanceEntry.
	/// </summary>
	[ToolboxData("<{0}:DojoBulkAttendanceEntryEditor runat=server></{0}:DojoBulkAttendanceEntryEditor>")]
	public class DojoBulkAttendanceEntryEditor : TableWindow, INamingContainer
	{
		private int dojoBulkAttendanceEntryID;
		private DojoBulkAttendanceEntry obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for Default Folder

		private TextBox tbStartDate = new TextBox();
		private TextBox tbEndDate = new TextBox();
		private TextBox tbDuration = new TextBox();
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
		public int DojoBulkAttendanceEntryID
		{
			get
			{
				return dojoBulkAttendanceEntryID;
			}
			set
			{
				loadFlag = true;
				dojoBulkAttendanceEntryID = value;
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

			tbStartDate.EnableViewState = false;
			Controls.Add(tbStartDate);

			tbEndDate.EnableViewState = false;
			Controls.Add(tbEndDate);

			tbDuration.EnableViewState = false;
			Controls.Add(tbDuration);

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
			if(dojoBulkAttendanceEntryID == 0)
				obj = new DojoBulkAttendanceEntry();
			else
				obj = new DojoBulkAttendanceEntry(dojoBulkAttendanceEntryID);

			obj.StartDate = DateTime.Parse(tbStartDate.Text);
			obj.EndDate = DateTime.Parse(tbEndDate.Text);
			obj.Duration = TimeSpan.Parse(tbDuration.Text);

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
				dojoBulkAttendanceEntryID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbStartDate.Text = DateTime.Now.ToString();
				tbEndDate.Text = DateTime.Now.ToString();
				tbDuration.Text = string.Empty;
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
				if(dojoBulkAttendanceEntryID > 0)
				{
					obj = new DojoBulkAttendanceEntry(dojoBulkAttendanceEntryID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoBulkAttendanceEntryID <= 0)
				{
					obj = new DojoBulkAttendanceEntry();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbStartDate.Text = obj.StartDate.ToString();
				tbEndDate.Text = obj.EndDate.ToString();
				tbDuration.Text = obj.Duration.ToString();

				//
				// Set Children Selections
				//
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
			// Render StartDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Attendance Start Date");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbStartDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render EndDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Attendance End Date");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbEndDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Duration
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Duration");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbDuration.RenderControl(output);
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
					dojoBulkAttendanceEntryID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoBulkAttendanceEntryID;
			return myState;
		}
	}
}

