using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen;
using Amns.Tessen.Utilities;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for TestEligibilityGrid.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:QuickTestEligibilityGrid runat=server></{0}:QuickTestEligibilityGrid>")]
	public class QuickTestEligibilityGrid : TableGrid
	{
		string connectionString;
	
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

		protected override void OnInit(EventArgs e)
		{
			columnCount = 8;
			components = TableWindowComponents.ContentHeader;
			features = TableWindowFeatures.DisableContentSeparation;
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
			TestListGenerator gen = new TestListGenerator(connectionString);
            DojoMemberCollection eligibles = gen.GetEligibleMembers();

			bool rowflag = false;
			string rowCssClass;

			//
			// Render Records
			//
			foreach(DojoMember member in eligibles)
			{
				if(rowflag)			
					rowCssClass = this.defaultRowCssClass;
				else
					rowCssClass = this.alternateRowCssClass;
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
	}
}
