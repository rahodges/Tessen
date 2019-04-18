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
	/// Summary description for DojoMemberQuickGrid.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:DojoMemberQuickGrid runat=server></{0}:DojoMemberQuickGrid>")]
	public class DojoMemberQuickGrid : TableGrid 
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

		#region Rendering

		protected override void OnPreRender(EventArgs e)
		{
			EnsureChildControls();
		}

		protected override void RenderToolbar(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			output.WriteLine();

			output.Indent++;
			output.WriteBeginTag("td");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("class", this.SubHeaderCssClass);
			output.WriteAttribute("height", "28px");
			output.WriteAttribute("colspan", this.columnCount.ToString());
			output.Write(HtmlTextWriter.TagRightChar);
			output.WriteLine();

			output.Indent++;			

			// RENDER OBJECTS

			output.Indent--;
			output.WriteEndTag("td");
			output.WriteLine();

			output.Indent--;
			output.WriteEndTag("tr");
			output.WriteLine();
		}
        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();

			DojoMemberManager mManager = new DojoMemberManager();
			DojoMemberCollection members;
			
			DojoMemberFlags[] options = new DojoMemberFlags[]
			{
				DojoMemberFlags.PrivateContact,
			};

			members = mManager.GetCollection(string.Empty, "kitTessen_Members_PrivateContacts.LastName", options);

			bool rowflag = false;
			string rowCssClass;		

			//
			// Render Records
			//
			foreach(DojoMember member in members)
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
				// Render Main Representation of Record
				//				
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				
				if(selectEnabled)
				{
					output.WriteBeginTag("a");
					output.WriteAttribute("href", "javascript:" + Page.ClientScript.GetPostBackEventReference(this, "edit_" + member.ID));
					output.Write(HtmlTextWriter.TagRightChar);
                    output.Write(member.PrivateContact.ConstructName("LS,FMi."));
					output.WriteEndTag("a");
				}
				else
				{
					output.Write(member.PrivateContact.ConstructName("LS,FMi."));
				}
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