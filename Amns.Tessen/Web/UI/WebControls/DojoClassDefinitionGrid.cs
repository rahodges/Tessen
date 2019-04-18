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
	ToolboxData("<{0}:DojoClassDefinitionGrid runat=server></{0}:DojoClassDefinitionGrid>")]
	public class DojoClassDefinitionGrid : TableGrid
	{
		private string connectionString;
		
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

		public DojoClassDefinitionGrid() : base()
		{
			this.features |= TableWindowFeatures.ClientSideSelector;

			this.headerLockEnabled = true;
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

		protected override void RenderContentHeader(HtmlTextWriter output)
		{
			RenderRow(this.HeaderRowCssClass, "Class", "Signin Start", "Class Times", "Signin End");
		}

        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();

			DojoClassDefinitionManager m = new DojoClassDefinitionManager();
			DojoClassDefinitionCollection dojoClassDefinitionCollection = m.GetCollection(string.Empty, 
                "DojoClassDefinition.NextClassStart, " +
                "DojoClassDefinition.NextClassEnd",
				new DojoClassDefinitionFlags[] {
												   DojoClassDefinitionFlags.Instructor,
												   DojoClassDefinitionFlags.InstructorPrivateContact});

			bool rowflag = false;
			string rowCssClass;		

			//
			// Render Records
			//
			foreach(DojoClassDefinition entry in dojoClassDefinitionCollection)
			{				
				if(rowflag)
					rowCssClass = this.defaultRowCssClass;
				else
					rowCssClass = this.alternateRowCssClass;

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
				
				output.WriteFullBeginTag("strong");
				output.Write(entry.Name);
				output.WriteEndTag("strong");
				output.Write("<br>");
                if (entry.Instructor != null)
                {
                    output.Write(entry.Instructor.PrivateContact.FullName);
                }
                else
                {
                    output.Write("No instructor specified.");
                }
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Class Start and End Dates
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				
//				output.WriteBeginTag("div");		// ADD RIGHT ARROW
//				output.WriteAttribute("style", "float:right;font-size:large;font-weight:bold;");
//				output.Write(HtmlTextWriter.TagRightChar);
//				output.Write("&rarr;");
//				output.WriteEndTag("div");

				output.Write(entry.NextSigninStart.ToLongDateString());
				output.Write("<br /> ");
				output.Write(entry.NextSigninStart.ToShortTimeString());
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Class Start and End Dates
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				
//				output.WriteBeginTag("div");		// ADD RIGHT ARROW
//				output.WriteAttribute("style", "float:right;font-size:large;font-weight:bold;");
//				output.Write(HtmlTextWriter.TagRightChar);
//				output.Write("&rarr;");
//				output.WriteEndTag("div");

				// Don't Display Date of Class Start
				// if the signing start's date is the same (looks better).
				if(entry.NextClassStart.Date != entry.NextSigninStart.Date)
				{
					output.Write(entry.NextClassStart.ToLongDateString());
					output.Write("<br />");
				}
				output.Write(entry.NextClassStart.ToShortTimeString());
				output.Write(" - ");
				output.Write(entry.NextClassEnd.ToShortTimeString());
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Class Start and End Dates
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				
				// Don't display date of signin end
				// if the signin end's start date is the same (looks better)
				if(entry.NextSigninEnd.Date != entry.NextClassEnd.Date)
				{
					output.Write(entry.NextSigninEnd.ToLongDateString());
					output.Write("<br /> ");
				}
				output.Write(entry.NextSigninEnd.ToShortTimeString());
				output.WriteEndTag("td");
				output.WriteLine();


				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

	}
}