using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Amns.Tessen.Utilities;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoPromotion.
	/// </summary>
	[DefaultProperty("ConnectionString"), 
		ToolboxData("<{0}:DojoPromotionEditor runat=server></{0}:DojoPromotionEditor>")]
	public class DojoPromotionEditor : TableWindow, INamingContainer
	{
		private bool loadForm;
		private int dojoPromotionID;
		private DojoPromotion editDojoPromotion;

		private TextBox tbPromotionDate = new TextBox();
		private DropDownList ddMember = new DropDownList();
		private DropDownList ddTest = new DropDownList();
		private DropDownList ddPromotionRank = new DropDownList();
		private DropDownList ddLastRank = new DropDownList();

		private Button btNext = new Button();
		private Button btOk = new Button();
		private Button btCancel = new Button();

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoPromotionID
		{
			get
			{
				return dojoPromotionID;
			}
			set
			{
				dojoPromotionID = value;
				loadForm = true;
			}
		}

		protected override void CreateChildControls()
		{
			Controls.Clear();
			bindDropDownLists();

			tbPromotionDate.Width = Unit.Pixel(175);
			tbPromotionDate.EnableViewState = false;
			Controls.Add(tbPromotionDate);

			ddMember.EnableViewState = false;
			Controls.Add(ddMember);

			ddTest.EnableViewState = false;
			Controls.Add(ddTest);

			ddPromotionRank.EnableViewState = false;
			Controls.Add(ddPromotionRank);

			ddLastRank.EnableViewState = false;
			Controls.Add(ddLastRank);

			btNext.Text = "Next";
			btNext.Width = Unit.Pixel(72);
			btNext.EnableViewState = false;
			btNext.Click += new EventHandler(btNext_Click);
			Controls.Add(btNext);

			btOk.Text = "OK";
			btOk.Width = Unit.Pixel(72);
			btOk.EnableViewState = false;
			btOk.Click += new EventHandler(btOk_Click);
			Controls.Add(btOk);

			btCancel.Text = "Cancel";
			btCancel.Width = Unit.Pixel(72);
			btCancel.EnableViewState = false;
			btCancel.Click += new EventHandler(btCancel_Click);
			Controls.Add(btCancel);

			ChildControlsCreated = true;
		}

		private void bindDropDownLists()
		{
			DojoMemberCollection members = 
				new DojoMemberManager().GetCollection("IsPrimaryOrgActive=true", 
				"LastName, FirstName, MiddleName", 
				DojoMemberFlags.PrivateContact);

			foreach(DojoMember member in members)
			{
				ListItem i = new ListItem(member.PrivateContact.ConstructName("L, FM."), member.iD.ToString());
				if(editDojoPromotion != null)
					if(editDojoPromotion.Member != null)
						i.Selected = member.iD == editDojoPromotion.Member.ID;
				ddMember.Items.Add(i);
			}

			DojoTestManager tm = new DojoTestManager();
			DojoTestCollection tests = tm.GetCollection(string.Empty, "TestDate", null);
			
			ddTest.Items.Add(new ListItem("None", "-1"));
			foreach(DojoTest test in tests)
			{
				ListItem i = new ListItem(test.Name + " (" + test.TestDate.ToShortDateString() + ")",
					test.ID.ToString());
				if(editDojoPromotion != null)
				{
					if(editDojoPromotion.Test != null)
					{
						i.Selected = test.ID == editDojoPromotion.Test.ID;
					}
				}
				ddTest.Items.Add(i);
			}

			DojoRankManager rm = new DojoRankManager();
			DojoRankCollection ranks = rm.GetCollection(string.Empty, "Name", null);
			foreach(DojoRank rank in ranks)
			{
				ListItem i = new ListItem(rank.Name, rank.ID.ToString());
				if(editDojoPromotion != null)
					if(editDojoPromotion.PromotionRank != null)
						i.Selected = rank.ID == editDojoPromotion.PromotionRank.ID;
				ddPromotionRank.Items.Add(i);
			}
			
			ddLastRank.Items.Add(new ListItem("Current Rank", "-1"));
			foreach(DojoRank rank in ranks)
			{
				ListItem i = new ListItem(rank.Name, rank.ID.ToString());
				if(editDojoPromotion != null)
					if(editDojoPromotion.LastRank != null)
						i.Selected = rank.ID == editDojoPromotion.LastRank.ID;
				ddLastRank.Items.Add(i);
			}
		}

		public void reset()
		{
			DojoTest recentTest = null;

			DojoTestManager tm = new DojoTestManager();
			DojoTestCollection tests = tm.GetCollection(string.Empty, "TestDate", null);

			// Select the most recent test automatically
			if(!Page.IsPostBack)
			{	
				if(tests.Count > 0)
					recentTest = tests[0];

				foreach(DojoTest test in tests)
				{
					if((DateTime.Now - test.TestDate).Negate() <
						(DateTime.Now - recentTest.TestDate).Negate())
						recentTest = test;
				}
			}

			if(recentTest != null)
			{
				foreach(ListItem i in ddTest.Items)
				{
					i.Selected = i.Value == recentTest.ID.ToString();
				}
			}
		}

		protected void btNext_Click(object sender, EventArgs e)
		{
			add();

			ddMember.SelectedIndex = 0;
		}


		protected void btOk_Click(object sender, EventArgs e)
		{
			add();

            tbPromotionDate.Text = string.Empty;
			ddMember.SelectedIndex = 0;
			ddTest.SelectedIndex = 0;
			ddPromotionRank.SelectedIndex = 0;
			ddLastRank.SelectedIndex = 0;

			this.OnClosed(EventArgs.Empty);
		}

		protected void btCancel_Click(object sender, EventArgs e)
		{
			this.OnCancelled(EventArgs.Empty);
		}

		private void add()
		{
			DojoTest test;
			DojoMember member;
			
			member = new DojoMember(int.Parse(ddMember.SelectedValue));

			if(dojoPromotionID == 0)
				editDojoPromotion = new DojoPromotion();
			else
				editDojoPromotion = new DojoPromotion(dojoPromotionID);
			
			if(ddMember.SelectedItem != null)
				editDojoPromotion.Member = member;
			else
				editDojoPromotion.Member = null;

			// If a test is not selected, set the test to null
			// and the promotion date to the date entered unless
			// the date entered is blank. In the event it is blank
			// use the current date.
			// If the test is selected, use the test and test date
			// to set the promotion test and promotion date.
			if(ddTest.SelectedValue == "-1")
			{
				editDojoPromotion.Test = null;
				try 
				{
					editDojoPromotion.PromotionDate =
						DateTime.Parse(tbPromotionDate.Text);
				}
				catch
				{
					editDojoPromotion.PromotionDate = 
						DateTime.Now;
					tbPromotionDate.Text = 
						DateTime.Now.ToString();
				}			
			}
			else
			{			
				test = new DojoTest(int.Parse(ddTest.SelectedValue));
				editDojoPromotion.Test = test;				
				editDojoPromotion.PromotionDate = test.TestDate;
				tbPromotionDate.Text = "";
			}

			if(ddPromotionRank.SelectedItem != null)
				editDojoPromotion.PromotionRank = DojoRank.NewPlaceHolder(
					int.Parse(ddPromotionRank.SelectedItem.Value));
			else
				editDojoPromotion.PromotionRank = null;

			// If the Last Rank is "Current" then get the member's
			// last rank and set it.
			if(ddLastRank.SelectedValue == "-1")
			{
				editDojoPromotion.LastRank = member.Rank;
			}
			else
			{
				if(ddLastRank.SelectedItem != null)
					editDojoPromotion.LastRank = DojoRank.NewPlaceHolder(
						int.Parse(ddLastRank.SelectedItem.Value));
				else
					editDojoPromotion.LastRank = null;
			}

			// Run an attendace adjustment on this rank.
			MembershipScan scan = new MembershipScan();
            scan.AttendanceAdjustment(editDojoPromotion.Member);

			dojoPromotionID = editDojoPromotion.Save();
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

		public event EventHandler Closed;
		protected virtual void OnClosed(EventArgs e)
		{
			if(Closed != null)
				Closed(this, e);
		}

		protected override void OnInit(EventArgs e)
		{
			columnCount = 2;
			features = TableWindowFeatures.DisableContentSeparation;
		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadForm)
			{
				if(dojoPromotionID != 0)
				{
					editDojoPromotion = new DojoPromotion(dojoPromotionID);

					//
					// Set Field Entries
					//
					tbPromotionDate.Text = editDojoPromotion.PromotionDate.ToString();

					//
					// Set Children Selections
					//
					foreach(ListItem item in ddMember.Items)
						item.Selected = editDojoPromotion.Member.ID.ToString() == item.Value;

					if(editDojoPromotion.Test != null)
					{
						foreach(ListItem item in ddTest.Items)
							item.Selected = editDojoPromotion.Test.ID.ToString() == item.Value;
					}

					foreach(ListItem item in ddPromotionRank.Items)
						item.Selected = editDojoPromotion.PromotionRank.ID.ToString() == item.Value;

					foreach(ListItem item in ddLastRank.Items)
						item.Selected = editDojoPromotion.LastRank.ID.ToString() == item.Value;

					Text = "Edit Promotion - " + editDojoPromotion.Name;

					btNext.Visible = false;
				}
				else
				{
					Text = "Add Promotion";
					reset();
				}
			}

		}

		protected override void RenderContent(HtmlTextWriter output)
		{
			//
			// Render Member
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Member");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ddMember.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Test
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Test");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ddTest.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PromotionDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("&nbsp;");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			tbPromotionDate.RenderControl(output);
			output.Write("<br><em>Optional Promotion Date</em>");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render LastRank
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Promotion");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ddLastRank.RenderControl(output);
			output.Write(" Promoted To ");
			ddPromotionRank.RenderControl(output);
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
			btNext.RenderControl(output);
			output.Write("&nbsp;");
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
					dojoPromotionID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoPromotionID;
			return myState;
		}
	}
}
