using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoBulkAttendanceEntry.
	/// </summary>
	[DefaultProperty("ConnectionString"), 
		ToolboxData("<{0}:DojoBulkAttendanceEntryEditor runat=server></{0}:DojoBulkAttendanceEntryEditor>")]
	public class DojoBulkAttendanceEntryEditor : TableWindow, INamingContainer
	{
		private int dojoBulkAttendanceEntryID;
		private DojoBulkAttendanceEntry editDojoBulkAttendanceEntry;
		private string connectionString;
		private bool resetOnAdd;
		private bool editOnAdd;

		private TextBox tbStartDate = new TextBox();
		private TextBox tbEndDate = new TextBox();
		private TextBox tbDuration = new TextBox();
		private DropDownList ddMember = new DropDownList();
		private DropDownList ddRank = new DropDownList();

		private Button btOk = new Button();
		private Button btCancel = new Button();

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoBulkAttendanceEntryID
		{
			get
			{
				return dojoBulkAttendanceEntryID;
			}
			set
			{
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

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public string ConnectionString
		{
			get
			{
				return connectionString;
			}
			set
			{
				connectionString = value;
			}
		}

		protected override void CreateChildControls()
		{
			Controls.Clear();
			bindDropDownLists();

			tbStartDate.Width = Unit.Pixel(175);
			tbStartDate.EnableViewState = false;
			Controls.Add(tbStartDate);

			tbEndDate.Width = Unit.Pixel(175);
			tbEndDate.EnableViewState = false;
			Controls.Add(tbEndDate);

			tbDuration.Width = Unit.Pixel(175);
			tbDuration.EnableViewState = false;
			Controls.Add(tbDuration);

			ddMember.EnableViewState = false;
			Controls.Add(ddMember);

			ddRank.EnableViewState = false;
			Controls.Add(ddRank);

			btOk.Text = "OK";
			btOk.Width = Unit.Pixel(72);
			btOk.EnableViewState = false;
			btOk.Click += new EventHandler(ok_Click);
			Controls.Add(btOk);

			btCancel.Text = "Cancel";
			btCancel.Width = Unit.Pixel(72);
			btCancel.EnableViewState = false;
			btCancel.Click += new EventHandler(cancel_Click);
			Controls.Add(btCancel);

			ChildControlsCreated = true;
		}

		private void bindDropDownLists()
		{
			DojoMemberCollection members = 
				new DojoMemberManager().GetCollection(string.Empty, 
				"LastName, FirstName, MiddleName", 
				DojoMemberFlags.PrivateContact);

			foreach(DojoMember member in members)
			{
				ListItem i = new ListItem(member.PrivateContact.ConstructName("L, FM."), member.iD.ToString());
				if(editDojoBulkAttendanceEntry != null)
					if(editDojoBulkAttendanceEntry.Member != null)
						i.Selected = member.iD == editDojoBulkAttendanceEntry.Member.ID;
				ddMember.Items.Add(i);
			}

            DojoRankManager rankManager = new DojoRankManager();
            DojoRankCollection ranks = rankManager.GetCollection(string.Empty, string.Empty, null);
            foreach (DojoRank rank in ranks)
                ddRank.Items.Add(new ListItem(rank.Name, rank.ID.ToString()));
		}

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoBulkAttendanceEntryID == 0)
				editDojoBulkAttendanceEntry = new DojoBulkAttendanceEntry();
			else
				editDojoBulkAttendanceEntry = new DojoBulkAttendanceEntry(dojoBulkAttendanceEntryID);

			editDojoBulkAttendanceEntry.StartDate = DateTime.Parse(tbStartDate.Text);
			editDojoBulkAttendanceEntry.EndDate = DateTime.Parse(tbEndDate.Text);
			editDojoBulkAttendanceEntry.Duration = TimeSpan.FromHours(double.Parse(tbDuration.Text));

			if(ddMember.SelectedItem != null)
				editDojoBulkAttendanceEntry.Member = DojoMember.NewPlaceHolder(
					int.Parse(ddMember.SelectedItem.Value));
			else
				editDojoBulkAttendanceEntry.Member = null;

			if(ddRank.SelectedItem != null)
				editDojoBulkAttendanceEntry.Rank = DojoRank.NewPlaceHolder( 
					int.Parse(ddRank.SelectedItem.Value));
			else
				editDojoBulkAttendanceEntry.Rank = null;

			if(editOnAdd)
				dojoBulkAttendanceEntryID = editDojoBulkAttendanceEntry.Save();
			else
				editDojoBulkAttendanceEntry.Save();

			if(resetOnAdd)
			{
				tbStartDate.Text = string.Empty;
				tbEndDate.Text = string.Empty;
				tbDuration.Text = string.Empty;
				ddMember.SelectedIndex = 0;
				ddRank.SelectedIndex = 0;
			}

			OnUpdated(EventArgs.Empty);
		}

		protected void cancel_Click(object sender, EventArgs e)
		{
			this.OnCancelled(EventArgs.Empty);
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

		protected override void OnInit(EventArgs e)
		{
			columnCount = 2;
			features = TableWindowFeatures.DisableContentSeparation;;
		}

		protected override void OnPreRender(EventArgs e)
		{
			if(dojoBulkAttendanceEntryID != 0)
			{
				editDojoBulkAttendanceEntry = new DojoBulkAttendanceEntry(dojoBulkAttendanceEntryID);

				//
				// Set Field Entries
				//
				tbStartDate.Text = editDojoBulkAttendanceEntry.StartDate.ToString();
				tbEndDate.Text = editDojoBulkAttendanceEntry.EndDate.ToString();
				tbDuration.Text = editDojoBulkAttendanceEntry.Duration.TotalHours.ToString();

				//
				// Set Children Selections
				//
				foreach(ListItem item in ddMember.Items)
					item.Selected = editDojoBulkAttendanceEntry.Member.ID.ToString() == item.Value;

				foreach(ListItem item in ddRank.Items)
					item.Selected = editDojoBulkAttendanceEntry.Rank.ID.ToString() == item.Value;

				Text = "Edit Bulk Attendance - " + editDojoBulkAttendanceEntry.StartDate.ToShortDateString() + "-" +
					editDojoBulkAttendanceEntry.EndDate.ToShortDateString();
			}
			else
				Text = "Add Bulk Attendance";
		}

		protected override void RenderContent(HtmlTextWriter output)
		{
			this.InitializeRenderHelpers(output); 

			output.WriteFullBeginTag("tr");
			RenderCell("DojoBulkAttendanceEntry ID", "class=\"row1\"");
			RenderCell(dojoBulkAttendanceEntryID.ToString(), "class=\"row1\"");
			output.WriteEndTag("tr");

			//
			// Render StartDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Attendance Start Date");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			tbStartDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render EndDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Attendance End Date");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			tbEndDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Duration
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Duration (hrs.)");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			tbDuration.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Member
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ddMember.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Rank
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ddRank.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

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
