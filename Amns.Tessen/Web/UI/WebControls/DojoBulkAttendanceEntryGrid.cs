using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for MemberListGrid.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:DojoBulkAttendanceEntryGrid runat=server></{0}:DojoBulkAttendanceEntryGrid>")]
	public class DojoBulkAttendanceEntryGrid : TableGrid 
	{
		private string connectionString;
		
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
		#endregion

		public DojoBulkAttendanceEntryGrid() : base()
		{
			this.features |= TableWindowFeatures.ClientSideSelector;
		}
		
		protected override void OnInit(EventArgs e)
		{
			base.OnInit (e);
			bool adminMode = Page.User.IsInRole("Tessen/Administrator");
			this.deleteButton.Enabled = adminMode;
			this.editButton.Enabled = adminMode;
			this.newButton.Enabled = adminMode;
		}

		#region Rendering
        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();

			DojoBulkAttendanceEntryManager m = new DojoBulkAttendanceEntryManager();
			DojoBulkAttendanceEntryCollection dojoBulkAttendanceEntryCollection = m.GetCollection(string.Empty,
				"LastName, FirstName, MiddleName", 
				new DojoBulkAttendanceEntryFlags[] {
					DojoBulkAttendanceEntryFlags.Member,
					DojoBulkAttendanceEntryFlags.Rank,
					DojoBulkAttendanceEntryFlags.MemberPrivateContact});

			bool rowflag = false;
			string rowCssClass;		

			//
			// Render Records
			//
			foreach(DojoBulkAttendanceEntry entry in dojoBulkAttendanceEntryCollection)
			{	
				if(rowflag)					rowCssClass = this.defaultRowCssClass;
				else						rowCssClass = this.alternateRowCssClass;
				rowflag = !rowflag;

				output.WriteBeginTag("tr");
				output.WriteAttribute("i", entry.ID.ToString());
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Indent++;

				//
				// Render Main Representation of Record
				//				
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.Member.PrivateContact.FullName);
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Start and End Dates
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.StartDate.ToShortDateString());
				output.Write(" - ");
				output.Write(entry.EndDate.ToShortDateString());
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Time
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.Duration.TotalHours);
				output.Write(" hrs. ");
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Rank
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.Rank.Name);
				output.WriteEndTag("td");
				output.WriteLine();

				if(deleteEnabled)
				{
					output.WriteBeginTag("a");
					output.WriteAttribute("href", "javascript:" + Page.ClientScript.GetPostBackEventReference(this, "delete_" + entry.ID));
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write("delete");
					output.WriteEndTag("a");
					output.WriteLine();
				}

				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion
	}
}