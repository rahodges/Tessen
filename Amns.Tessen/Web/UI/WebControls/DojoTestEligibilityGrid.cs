using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen;
using Amns.Tessen.Utilities;
using Amns.Tessen.Web.UI.WebControls.Views;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for DojoTestEligibilityGrid.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:DojoTestEligibilityGrid runat=server></{0}:DojoTestEligibilityGrid>")]
	public class DojoTestEligibilityGrid : TableGrid
	{
		string connectionString;

		CheckBox cbAllMembers;
		Button btPromote = new Button();
		DropDownList ddTests = new DropDownList();
		
		#region Public Properties

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

		public DojoTestEligibilityGrid()
		{
			this.ProcessViewPanePostback();

			features |= TableWindowFeatures.ClientSideSelector;
			components |= TableWindowComponents.ViewPane;

			this.headerLockEnabled = true;
			this.columnLockEnabled = true;

			this.ViewPane = new MemberViewPane();

			toolbars.Remove(toolbars[0]); // Remove add/edit/remove		
		}


		#region Child Control Methods and Event Handling

		protected override void CreateChildControls()
		{
			Controls.Clear();

			ddTests.EnableViewState = false;
			ddTests.AutoPostBack = true;
			Controls.Add(ddTests);

			btPromote.Text = "Promote";
			btPromote.Click += new EventHandler(btPromote_Click);
			Controls.Add(btPromote);

			cbAllMembers = new CheckBox();
			cbAllMembers.Text = "All Members";
			cbAllMembers.EnableViewState = false;
			cbAllMembers.AutoPostBack = true;
			Controls.Add(cbAllMembers);
			
			bindDropDownLists();

            ComponentArt.Web.UI.ToolBar testToolbar = ToolBarUtility.DefaultToolBar("Test");
            ToolBarUtility.AddControlItem(testToolbar, ddTests);
            ToolBarUtility.AddControlItem(testToolbar, btPromote);
            toolbars.Add(testToolbar);

			ChildControlsCreated = true;
		}

		#endregion
		
		private void bindDropDownLists()
		{
			DojoTestManager testManager = new DojoTestManager();
			DojoTestCollection tests = testManager.GetCollection("TestDate>=#" + 
				DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)).ToString() + "#",
				"TestDate",
				null);
			ddTests.Items.Add(new ListItem("Now", "-1"));
			foreach(DojoTest test in tests)
			{
				ListItem i = new ListItem(test.name + " (" + test.testDate.ToShortDateString() + ")",
					test.iD.ToString());				
				ddTests.Items.Add(i);
			}
		}

		private void btPromote_Click(object sender, EventArgs e)
		{
			if(ddTests.SelectedValue != "-1")
			{
				DojoMember member = new DojoMember(this.SelectedID);
				DojoTest test = new DojoTest(int.Parse(ddTests.SelectedValue));
				DojoPromotion promotion = new DojoPromotion();
				
				promotion.LastRank = member.Rank;
				promotion.Member = member;	
				promotion.PromotionDate = test.TestDate;
				promotion.PromotionRank = member.Rank.PromotionRank;
				promotion.Test = test;
				promotion.Save();

				member.Rank = member.Rank.PromotionRank;
				member.RankDate = test.TestDate;
				member.Save();
				
//				MembershipScan mScan = new MembershipScan();
//				mScan.FixAttendance();

				Page.ClientScript.RegisterStartupScript(this.GetType(), "WarningMessage", 
					"<script language=\"javascript\">alert('" +
					member.PrivateContact.FullName + " promoted to " + member.Rank.Name + "." +
					"');</script>");;
			}
		}

		#region Rendering

		protected override void OnPreRender(EventArgs e)
		{
			btPromote.Enabled = ddTests.SelectedValue != "-1";
			
			base.OnPreRender (e);
		}


		protected override void RenderContentHeader(HtmlTextWriter output)
		{
			#region Header Row

			output.WriteFullBeginTag("tr");
			output.WriteLine();
			
			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.headerRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Name");
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.headerRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Eligible On");
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.headerRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Hours");
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.headerRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Eligible For");
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.headerRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Last Seen");
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.headerRowCssClass);
			output.WriteAttribute("colspan", "3");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Instructors");
			output.WriteEndTag("td");

			output.WriteEndTag("tr");
			output.WriteLine();

			#endregion
		}
        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			TestListGenerator gen;
			DojoMemberCollection eligibles;

			EnsureChildControls();

			gen = new TestListGenerator(connectionString);

			if(cbAllMembers.Checked)
			{
				DojoMemberManager memberManager = new DojoMemberManager();
                eligibles = memberManager.GetCollection("DojoMember.IsPrimaryOrgActive=true", 
					"DojoMember.RankID, DojoMember.RankDate DESC",
					new DojoMemberFlags[]
				{
					DojoMemberFlags.PrivateContact, 
					DojoMemberFlags.Rank
				});
			}
			else if(ddTests.SelectedItem.Value == "-1")
			{
				eligibles = gen.GetEligibleMembers();
			}
			else
			{
				eligibles = gen.GetEligibleMembers(new DojoTest(int.Parse(ddTests.SelectedItem.Value)));
			}

			bool rowflag = false;
			string rowCssClass;	
	
			//
			// Render Records
			//
			foreach(DojoMember member in eligibles)
			{					
				if(rowflag)			rowCssClass = this.defaultRowCssClass;
				else						rowCssClass = this.alternateRowCssClass;
				rowflag = !rowflag;

				output.WriteBeginTag("tr");
				output.WriteAttribute("i", member.ID.ToString());
				output.Write(HtmlTextWriter.TagRightChar);
				output.Indent++;

				//
				// Render Main Representation of Record
				//				
				output.WriteBeginTag("td");
				output.WriteAttribute("nowrap", "true");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
                output.Write(member.PrivateContact.ConstructName("F Mi. L"));
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Eligibility Date
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(member.TestEligibilityDate.ToShortDateString());
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Hours Balance
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				if(member.TestEligibilityHoursBalance.TotalHours > 0)
					output.Write("+");
				output.Write(member.TestEligibilityHoursBalance.TotalHours.ToString("f"));
				output.WriteEndTag("td");
				output.WriteLine();

				
				//
				// Render Promotion Rank
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(member.Rank.PromotionRank.Name);
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Last Seen
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(member.LastSignin.ToShortDateString());
				output.WriteEndTag("td");
				output.WriteLine();

				renderInstructor(output, member.Instructor1, rowCssClass);
				renderInstructor(output, member.Instructor2, rowCssClass);
				renderInstructor(output, member.Instructor3, rowCssClass);

				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		private void renderInstructor(HtmlTextWriter output, DojoMember instructor, string rowCssClass)
		{
			output.WriteBeginTag("td");
			output.WriteAttribute("class", rowCssClass);
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			if(instructor != null)
			{
				output.Write(instructor.PrivateContact.ConstructName("Fi. L"));				
			}
			else
			{
				output.Write("&nbsp;");
			}
			output.WriteEndTag("td");
			output.WriteLine();
		}

		#endregion
	}
}