using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen.Utilities;

namespace Amns.Tessen.Web.UI.WebControls.Views
{
	/// <summary>
	/// Summary description for MemberQuickView.
	/// </summary>
	public class TestViewPane : TableWindowViewPane
	{
		DojoTest _test;

		public DojoTest Test
		{
			get { return _test; }
			set { _test = value; }
		}

		public override void Render(System.Web.UI.HtmlTextWriter output)
		{
			DojoTest test;
			TestCandidateCollection candidates;
			TestListGenerator gen;
			string connectionString;
			TableGrid grid;
			
			if(ParentWindow is TableGrid)
			{
				grid = (TableGrid) ParentWindow;
				
				if(ParentWindow is DojoTestGrid)
				{
					connectionString = ((DojoTestGrid) ParentWindow).ConnectionString;
				}
				else if(ParentWindow is DojoTestListGrid)
				{
					connectionString = ((DojoTestListGrid) ParentWindow).ConnectionString;
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

			test = new DojoTest(int.Parse(grid.Page.Request.QueryString[0]));
		
			RenderTableBeginTag(output, "_viewPanel", grid.CellPadding, grid.CellSpacing, Unit.Percentage(100), Unit.Empty, grid.CssClass);
           
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("th");
			output.WriteAttribute("colspan", "4");
			output.WriteAttribute("class", grid.HeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(test.Name);
			output.WriteEndTag("th");
			output.WriteEndTag("tr");

			#region Candidates Information

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "4");
			output.WriteAttribute("class", grid.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Candidates");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			if(test.ActiveTestList != null)
			{
				gen = new TestListGenerator(connectionString);
				candidates = gen.BuildTestList(test);

				foreach(TestCandidate candidate in candidates)
				{
					if(!candidate.IsRemoved)
					{
						output.WriteFullBeginTag("tr");

						output.WriteBeginTag("td");
						output.WriteAttribute("class", grid.DefaultRowCssClass);
						output.Write(HtmlTextWriter.TagRightChar);
						output.Write(candidate.Member.PrivateContact.ConstructName("L,FMi"));
						output.WriteEndTag("td");

						output.WriteBeginTag("td");
						output.WriteAttribute("class", grid.DefaultRowCssClass);
						output.Write(HtmlTextWriter.TagRightChar);
						output.Write(candidate.Status.Name);
						output.WriteEndTag("td");

						output.WriteBeginTag("td");
						output.WriteAttribute("class", grid.DefaultRowCssClass);
						output.Write(HtmlTextWriter.TagRightChar);
						output.Write(candidate.LastEntry.Comment);
						output.WriteEndTag("td");

						output.WriteBeginTag("td");
						output.WriteAttribute("class", grid.DefaultRowCssClass);
						output.Write(HtmlTextWriter.TagRightChar);
						output.Write("<input type=\"submit\" name=\"" + 
							grid.ClientID + "_remove_" + candidate.Member.ID.ToString() + 
							"\" value=\"Remove\" />");
						output.WriteEndTag("td");
					
						output.WriteEndTag("tr");
					}
				}
			}
			else
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("colspan", "4");
				output.WriteAttribute("class", grid.DefaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("No active test list found.");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}

			#endregion

			output.WriteEndTag("table");
			
		}

	}
}