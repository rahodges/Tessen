using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Amns.Tessen;
using Amns.Tessen.Utilities;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	// THIS CODE IS HARD BOUND TO DATABASE!

	/// <summary>
	/// Summary description for MyMembershipPanel.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:MyMembershipPanel runat=server></{0}:MyMembershipPanel>")]
	public class MyMembershipPanel : TableWindow
	{
		private string connectionString;
		private int memberID;
		private string procedureName;
		private string organizationActiveImageUrl;
		private string membershipActiveImageUrl;
		private bool attendanceHistoryEnabled = true;
		private string pastDueWarning = "<font color=\"#FF0000\"><strong>Notice: " +
			"Your dues are past due, please make an appointment with the black box.</strong><font>";
		private string mailingAddressWarning = "<font color=\"#FF0000\"><strong>Notice: " +
			"Your mailing address needs to be updated, please fill out an address " +
			"correction form.</strong><font>";

		#region Public Properties

		[Bindable(true), 
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
				connectionString = value;
			}
		}


		
		[Bindable(true), 
		Category("Data"), 
		DefaultValue(0)] 
		public int StudentID
		{
			get
			{
				return memberID;
			}
			set
			{
				memberID = value;
			}
		}
		
		[Bindable(true), 
		Category("Data"), 
		DefaultValue("procDm_SelectMyMembershipPanel")] 
		public string ProcedureName
		{
			get
			{
				return procedureName;
			}
			set
			{
				procedureName = value;
			}
		}
		
		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public string OrganizationActiveImageUrl
		{
			get
			{
				return organizationActiveImageUrl;
			}
			set
			{
				organizationActiveImageUrl = value;
			}
		}

		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("<font color=\"#FF0000\"><strong>Notice: Your dues " +
			 "are past due, please make an appointment with the black box.</strong><font>")]
		public string PastDueWarning
		{
			get
			{
				return pastDueWarning;
			}
			set
			{
				pastDueWarning = value;
			}
		}

		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("<font color=\"#FF0000\"><strong>Notice: " +
			"Your mailing address needs to be updated, please fill out an address " +
			"correction form.</strong><font>")]
		public string MailingAddressWarning
		{
			get
			{
				return mailingAddressWarning;
			}
			set
			{
				mailingAddressWarning = value;
			}
		}
		
		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public string MembershipActiveImageUrl
		{
			get
			{
				return membershipActiveImageUrl;
			}
			set
			{
				membershipActiveImageUrl = value;
			}
		}

		[Bindable(true), Category("Behavior"), DefaultValue(true)]
		public bool AttendanceHistoryEnabled
		{
			get
			{
				return attendanceHistoryEnabled;
			}
			set
			{
				attendanceHistoryEnabled = value;
			}
		}

		#endregion

		protected override void OnInit(EventArgs e)
		{
			columnCount = 3;
			features = TableWindowFeatures.DisableContentSeparation;
		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			OleDbConnection dbConnection = new OleDbConnection(connectionString);
			OleDbCommand dbCommand = new OleDbCommand();
			dbCommand.Connection = dbConnection;
			dbCommand.CommandText = "SELECT SUM(ClassEnd - ClassStart) FROM kitTessen_Attendance " +
				"INNER JOIN kitTessen_Classes ON kitTessen_Attendance.ClassID=kitTessen_Classes.DojoClassID " +
				"WHERE MemberID=" + memberID.ToString() + ";";

            EnsureChildControls();

			// OPEN DATABASE FOR READING ==============================
			DojoMember m = new DojoMember(memberID);
			AttendanceScan scan = new AttendanceScan(m, TimeSpan.FromHours(1));
			scan.RunScan();

			// FIRST, MIDDLE, LAST & RANK ============================== 2:1-2
			output.WriteFullBeginTag("tr");
			output.WriteLine();

			output.Indent++;
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.WriteFullBeginTag("strong");
			output.Write(m.PrivateContact.FullName);
			output.Write(", ");
			output.Write(m.Rank.Name);
			output.WriteEndTag("strong");
			output.WriteEndTag("td");
			output.WriteLine();
			output.Indent--;

			// OUTPUT MEMBERSHIP AND ORGANIZATION ICONS ================ 2:3

			output.Indent++;
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("rowspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			if(m.IsPrimaryOrgActive)
			{
				output.WriteBeginTag("img");
				output.WriteAttribute("src", membershipActiveImageUrl);
				output.Write(HtmlTextWriter.TagRightChar);
			}
			output.WriteEndTag("td");
			output.WriteLine();
			output.Indent--;

			output.WriteEndTag("tr");
			output.WriteLine();

			// OUTPUT MEMBERSHIP TYPE AND STUFF ======================== 4:1-2
			output.WriteFullBeginTag("tr");
			output.WriteLine();
			RenderCell(string.Format("<strong>{0}</strong>", "Adult Member", "left", "2"));
			output.WriteEndTag("tr");
			output.WriteLine();

			// OUTPUT BAD ADDRESS FLAG ================================ 4:-3
			if(m.PrivateContact.IsBadAddress)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("colspan", "3");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(mailingAddressWarning);
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
				output.WriteLine();
			}

			// OUTPUT PAST DUE FLAG =================================== 4:-3
			if(m.IsPastDue)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("colspan", "3");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(pastDueWarning);
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
				output.WriteLine();
			}

			// OUTPUT Day TRAINING TIMES, RANK DATE =================== 4:-3
			output.WriteFullBeginTag("tr");
			output.WriteFullBeginTag("td");
			output.Write("Member Since");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(m.MemberSince.ToShortDateString());
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			output.WriteLine();

			// OUTPUT Day TRAINING TIMES, RANK DATE =================== 4:-3
			output.WriteFullBeginTag("tr");
			output.WriteFullBeginTag("td");
			output.Write("Training Hours");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("{0:f} hrs.", m.TimeInMembership.TotalHours);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			output.WriteLine();

			// OUTPUT Day TRAINING TIMES, RANK DATE =================== 4:-3
			output.WriteFullBeginTag("tr");
			output.WriteFullBeginTag("td");
			output.Write("Hours in Rank *");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("{0:f} hrs.", m.TimeInRank.TotalHours);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			output.WriteLine();

			// OUTPUT Day TRAINING TIMES, RANK DATE =================== 4:-3
			output.WriteFullBeginTag("tr");
			output.WriteFullBeginTag("td");
			output.Write("This Week");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("{0:f} hrs.", scan.TotalHoursThisWeek);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			output.WriteLine();

			output.WriteFullBeginTag("tr");
			output.WriteFullBeginTag("td");
			output.Write("Last Week");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("{0:f} hrs.", scan.TotalHoursLastWeek);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			output.WriteLine();

			if(m.Rank.PromotionTimeInRank.TotalHours < 100000)
			{

				// OUTPUT Day TRAINING TIMES, RANK DATE =================== 4:-3
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("colspan", "3");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("<strong>");
				if(m.TimeInRank >= m.Rank.PromotionTimeInRank &
					m.RankDate.Add(m.Rank.PromotionTimeFromLastTest) <= DateTime.Now)
				{
					output.Write("You meet minimum time requirements for ");
					output.Write(m.Rank.PromotionRank.Name);
					output.Write(".");
				}
				else if(m.TimeInRank < m.Rank.PromotionTimeInRank &
					m.RankDate.Add(m.Rank.PromotionTimeFromLastTest) > DateTime.Now)
				{
					output.Write("You will meet minumum time requirements for ");
					output.Write(m.Rank.PromotionRank.Name);
					output.Write(" on ");
					output.Write(m.RankDate.Add(m.Rank.PromotionTimeFromLastTest).ToShortDateString());
					output.Write(" with ");
					output.Write("{0:f}", m.Rank.PromotionTimeInRank.TotalHours - m.TimeInRank.TotalHours);
					output.Write(" more hours of training.");
				}
				else if(m.TimeInRank >= m.Rank.PromotionTimeInRank)
				{
					output.Write("You will meet minumum time requirements for ");
					output.Write(m.Rank.PromotionRank.Name);
					output.Write(" on ");
					output.Write(m.RankDate.Add(m.Rank.PromotionTimeFromLastTest).ToShortDateString());
					output.Write(".");
				}
				else if(m.RankDate.Add(m.Rank.PromotionTimeFromLastTest) <= DateTime.Now)
				{
					output.Write("You need ");
					output.Write("{0:f}", m.Rank.PromotionTimeInRank.TotalHours - m.TimeInRank.TotalHours);
					output.Write(" training hours in rank to meet minimum time requirements for ");
					output.Write(m.Rank.PromotionRank.Name);
					output.Write(".");
				}

				output.Write(" Permission to test will be at your teachers' discretion.</strong>");

				output.WriteEndTag("td");
				output.WriteEndTag("tr");
				output.WriteLine();
			}

			//
			// Output Attendance Scan Time
			//
			output.WriteFullBeginTag("tr");
			output.WriteLine();
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Last Attendance Scan ");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("align", "right");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(m.lastAttendanceScan.ToShortDateString());
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			output.WriteLine();
		}
	}
}