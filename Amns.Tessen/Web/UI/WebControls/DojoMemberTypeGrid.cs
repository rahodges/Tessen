using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.GreyFox.People;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// <summary>
	/// A custom grid for DojoMemberType.
	/// </summary>
	/// </summary>
	[DefaultProperty("ConnectionString"),
		ToolboxData("<{0}:DojoMemberTypeGrid runat=server></{0}:DojoMemberTypeGrid>")]
	public class DojoMemberTypeGrid : TableGrid
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

		protected override void OnInit(EventArgs e)
		{
			features = TableWindowFeatures.ClipboardCopier | 
				TableWindowFeatures.Scroller |
				TableWindowFeatures.WindowPrinter |
				TableWindowFeatures.ClientSideSelector;
						
			this.headerLockEnabled = true;
		}

		#region Rendering

		protected override void RenderContentHeader(HtmlTextWriter output)
		{
			RenderRow(this.HeaderRowCssClass, "Name", "Description");
		}


		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			DojoMemberTypeManager m = new DojoMemberTypeManager();
			DojoMemberTypeCollection dojoMemberTypeCollection = 
				m.GetCollection(string.Empty, string.Empty);

			bool rowflag = false;
			string rowCssClass;

			//
			// Render Records
			//
			foreach(DojoMemberType dojoMemberType in dojoMemberTypeCollection)
			{
				if(rowflag) rowCssClass = defaultRowCssClass;
				else rowCssClass = alternateRowCssClass;
				rowflag = !rowflag;
				output.WriteBeginTag("tr");
				output.WriteAttribute("i", dojoMemberType.ID.ToString());				
				output.WriteLine(HtmlTextWriter.TagRightChar);				
				output.Indent++;

				// Write Name
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.WriteLine(dojoMemberType.Name);
				output.WriteEndTag("td");
				output.Indent--;

				// Write Description
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);				
				output.WriteLine(dojoMemberType.Description);
				output.WriteEndTag("td");
				output.Indent--;


				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion
	}
}