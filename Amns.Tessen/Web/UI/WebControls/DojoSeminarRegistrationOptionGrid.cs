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
		ToolboxData("<{0}:DojoSeminarRegistrationOptionGrid runat=server></{0}:DojoSeminarRegistrationOptionGrid>")]
	public class DojoSeminarRegistrationOptionGrid : TableGrid
	{
		private string connectionString;
		private int parentSeminarRegistrationID = -1;	// '0' Delists classes associated with seminars.
														// '-1' Lists all classes associated with seminars.
		
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
		Category("Behavior"),
		DefaultValue(-1)]
		public int ParentSeminarRegistrationID
		{
			get
			{
				return parentSeminarRegistrationID;
			}
			set
			{
				parentSeminarRegistrationID = value;
			}
		}

		#endregion

		#region Rendering
        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();

			string whereQuery = string.Empty;
			if(parentSeminarRegistrationID > 0)
				whereQuery = "ParentSeminarRegistrationID=" + parentSeminarRegistrationID;

			DojoTestManager m = new DojoTestManager();
			DojoTestCollection dojoTestCollection = m.GetCollection(string.Empty, string.Empty, null);

			bool rowflag = false;
			string rowCssClass;

			//
			// Render Records
			//
			foreach(DojoTest entry in dojoTestCollection)
			{				
				if(rowflag)
					rowCssClass = this.defaultRowCssClass;
				else
					rowCssClass = this.alternateRowCssClass;

				rowflag = !rowflag;

				output.WriteFullBeginTag("tr");
				output.WriteLine();
				output.Indent++;
	
				//
				// Render ID of Record
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.ID);
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Main Representation of Record
				//				
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				
				if(selectEnabled)
				{
					output.WriteBeginTag("a");
					output.WriteAttribute("href", "javascript:" + GetSelectReference(entry.ID));
					output.Write(HtmlTextWriter.TagRightChar);
                    output.Write(entry.Name);
					output.WriteEndTag("a");
					output.Write("<br>");
					output.Write(entry.Location.BusinessName);
				}
				else
				{
					output.Write(entry.Name);
					output.Write("<br>");
					output.Write(entry.Location.BusinessName);
				}
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Test Date
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.TestDate.ToShortDateString());
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Test Time
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.TestDate.ToShortTimeString());
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