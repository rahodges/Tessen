using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls.Views
{
	/// <summary>
	/// Summary description for MemberQuickView.
	/// </summary>
	public class MemberViewPane : TableWindowViewPane
	{
		DojoMember __member;

		public DojoMember Member
		{
			get { return __member; }
			set { __member = value; }
		}

		public override void Render(System.Web.UI.HtmlTextWriter output)
		{
			TableGrid grid;
			
			if(ParentWindow is TableGrid)
			{
				grid = (TableGrid) ParentWindow;
				
				if(ParentWindow is DojoMemberGrid)
				{
				
				}
				else if(ParentWindow is DojoTestEligibilityGrid)
				{
				
				}
				else
				{
					throw(new Exception("Parent window is not supported."));
				}
			}
			else
			{
				throw(new Exception("Parent window is not supported."));
			}

			DojoMember m = new DojoMember(int.Parse(grid.Page.Request.QueryString[0]));
		
			RenderTableBeginTag(output, "_viewPanel", grid.CellPadding, grid.CellSpacing, Unit.Percentage(100), Unit.Empty, grid.CssClass);
           
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("th");
			output.WriteAttribute("class", grid.HeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(m.PrivateContact.FullName);
			output.WriteEndTag("th");
			output.WriteEndTag("tr");

			#region Contact Information

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Contacts");
			if(m.PrivateContact.IsBadAddress)
				output.Write(" - <strong>Bad Address</strong>");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.DefaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(m.PrivateContact.ConstructAddress("<br />"));
			output.Write("<br />");
			if(m.PrivateContact.HomePhone != string.Empty)
				output.Write(m.PrivateContact.HomePhone + " (h)<br />");
			if(m.PrivateContact.WorkPhone != string.Empty)
				output.Write(m.PrivateContact.WorkPhone + " (w)<br />");
			if(m.PrivateContact.MobilePhone != string.Empty)
				output.Write(m.PrivateContact.MobilePhone + " (m)<br />");
			if(m.PrivateContact.Email1 != string.Empty)
			{
				output.Write("<a href=\"mailto:");
				output.Write(m.PrivateContact.Email1);
				output.Write("\">");
				output.Write(m.PrivateContact.Email1);
				output.Write("</a>");
				output.Write("<br />");
			}
			if(m.PrivateContact.ValidationMemo != null && m.PrivateContact.ValidationMemo.Length > 0)
			{
				output.Write("<br />");
				output.Write("<strong>Validation Memo : </strong><br />");
				output.Write(m.PrivateContact.ValidationMemo.Replace("\n", "<br />"));
			}
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			#endregion

			#region Membership Information

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Membership");
			if(m.IsPastDue)
				output.Write(" - <strong>Past Due</strong>");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.DefaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Student Type</strong> : ");
			output.Write(m.MemberType.Name);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.DefaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Membership Date</strong> : ");
			output.Write(m.MemberSince.ToLongDateString());
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.DefaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Active Membership</strong> : ");
			if(m.IsPrimaryOrgActive)
				output.Write("Yes");
			else
				output.Write("No");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.DefaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Rank</strong> : ");
			output.Write(m.Rank.Name);
			output.Write(" (" + m.RankDate.ToShortDateString() + ")");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			#endregion

			#region Attendance Information

			// Pull last 90 days of attendance from the database

			int maxEntries = 150;
			int displayEntries = 5;
			DateTime minSearchDate = DateTime.Now.Subtract(TimeSpan.FromDays(90));

			DojoAttendanceEntryManager aem = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection attendance = 
				aem.GetCollection(maxEntries, "MemberID=" + m.ID.ToString() + 
				" AND ClassStart>=#" + minSearchDate.ToString() + "#", "ClassStart DESC",
				DojoAttendanceEntryFlags.Class);

			DojoMember instructor1 = m.Instructor1;
			DojoMember instructor2 = m.Instructor2;
			DojoMember instructor3 = m.Instructor3;

			if(attendance.Count < displayEntries)
				displayEntries = attendance.Count;

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Attendance");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.DefaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Time In Membership</strong> : ");
			output.Write(m.TimeInMembership.TotalHours.ToString("f") + " Hours");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.DefaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Time In Rank</strong> : ");
			output.Write(m.TimeInRank.TotalHours.ToString("f") + " Hours");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.DefaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Last Signin</strong> : ");
			output.Write(m.LastSignin.ToLongDateString());
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			// Top Instructor
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Ninety Day Instructors");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			if(instructor1 != null)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", grid.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(instructor1.PrivateContact.FullName);
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}
			else
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", grid.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("None");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}
			
			if(instructor2 != null)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", grid.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(instructor2.PrivateContact.FullName);
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}

			if(instructor3 != null)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", grid.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(instructor3.PrivateContact.FullName);
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}
			
			// Display Last 5 Classes
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", grid.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Ninety Day Activity");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			
			if(displayEntries == 0)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", grid.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("None");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}
			else
			{
				for(int x = 0; x < displayEntries; x++)
				{
					DojoAttendanceEntry entry = attendance[x];

					output.WriteFullBeginTag("tr");
					output.WriteBeginTag("td");
					output.WriteAttribute("class", grid.DefaultRowCssClass);
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write(entry.Class.Name + 
						" - " + 
						entry.Class.ClassStart.ToString("dddd, MMMM d - h:mm tt"));
					output.WriteEndTag("td");
					output.WriteEndTag("tr");
				}				
			}

			#endregion

			//			#region Instructor Information
			//
			//			if(m.IsInstructor)
			//			{
			//				output.WriteFullBeginTag("tr");
			//				output.WriteBeginTag("td");
			//				output.WriteAttribute("class", grid.SubHeaderCssClass);
			//				output.Write(HtmlTextWriter.TagRightChar);
			//				output.Write("Instructor Details");
			//				output.WriteEndTag("td");
			//				output.WriteEndTag("tr");
			//			}
			//
			//			#endregion

			#region Security

			if(this.ParentWindow.Page.User.IsInRole("Tessen/Administrator"))
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", grid.SubHeaderCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("Security");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");

                if (m.UserAccount == null)
				{
					output.WriteFullBeginTag("tr");
					output.WriteBeginTag("td");
					output.WriteAttribute("class", grid.DefaultRowCssClass);
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write("The member has no associated user account.");
					output.WriteEndTag("td");
					output.WriteEndTag("tr");
				}
				else
				{
					output.WriteFullBeginTag("tr");
					output.WriteBeginTag("td");
					output.WriteAttribute("class", grid.DefaultRowCssClass);
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write("<strong>Username</strong> : ");
                    output.Write(m.UserAccount.UserName);
					output.Write("<br />");
					output.Write("<strong>Last Access</strong> : ");
                    if (m.UserAccount.LoginDate != DateTime.MinValue)
                        output.Write(m.UserAccount.LoginDate);
					else
						output.Write("None");
					output.Write("<br />");
					output.Write("<strong>Login Count</strong> : ");
                    output.Write(m.UserAccount.LoginCount);
					output.WriteEndTag("td");
					output.WriteEndTag("tr");				
				}
			}

			#endregion

			#region Memos

			if(this.ParentWindow.Page.User.IsInRole("Tessen/Administrator"))
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", grid.SubHeaderCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("Current Attendance Message");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");

				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", grid.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(m.AttendanceMessage != "")
					output.Write(m.AttendanceMessage);
				else
					output.Write("Empty");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");

				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", grid.SubHeaderCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("Memo");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");

				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", grid.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(m.PrivateContact.MemoText != "")
					output.Write(m.PrivateContact.MemoText.Replace("\n", "<br>"));
				else
					output.Write("Empty");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}
            
			#endregion

			output.WriteEndTag("table");
			
		}

	}
}
