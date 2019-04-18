using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// <summary>
	/// A custom grid for DojoTestListJournalEntry.
	/// </summary>
	/// </summary>
	[DefaultProperty("ConnectionString"),
		ToolboxData("<{0}:DojoTestListJournalEntryGrid runat=server></{0}:DojoTestListJournalEntryGrid>")]
	public class DojoTestListJournalEntryGrid : TableGrid
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
		}

		#region Rendering

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			DojoTestListJournalEntryManager m = new DojoTestListJournalEntryManager();
			DojoTestListJournalEntryCollection dojoTestListJournalEntryCollection = m.GetCollection(string.Empty, string.Empty, null);
			// Render Header Row
			this.headerLockEnabled = true;
			RenderRow(this.HeaderRowCssClass, "Entry");

			bool rowflag = false;
			string rowCssClass;
			//
			// Render Records
			//
			foreach(DojoTestListJournalEntry dojoTestListJournalEntry in dojoTestListJournalEntryCollection)
			{
				if(rowflag) rowCssClass = defaultRowCssClass;
				else rowCssClass = alternateRowCssClass;
				rowflag = !rowflag;
				output.WriteBeginTag("tr");
				output.WriteAttribute("i", dojoTestListJournalEntry.ID.ToString());
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Indent++;
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(dojoTestListJournalEntry.Member.PrivateContact.ConstructName("L,FMi"));
				output.WriteEndTag("td");
				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

	}
}
