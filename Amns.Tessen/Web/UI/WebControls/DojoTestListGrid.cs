using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// <summary>
	/// A custom grid for DojoTestList.
	/// </summary>
	/// </summary>
	[DefaultProperty("ConnectionString"),
		ToolboxData("<{0}:DojoTestListGrid runat=server></{0}:DojoTestListGrid>")]
	public class DojoTestListGrid : TableGrid
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

		public DojoTestListGrid() : base()
		{
			this.features |= TableWindowFeatures.ClientSideSelector;
		}

		#region Rendering

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			DojoTestListManager m = new DojoTestListManager();
			DojoTestListCollection dojoTestListCollection = m.GetCollection(string.Empty, string.Empty, null);
			// Render Header Row
			this.headerLockEnabled = true;
			RenderRow(this.HeaderRowCssClass, "Test", "Test Date");

			bool rowflag = false;
			string rowCssClass;
			//
			// Render Records
			//
			foreach(DojoTestList dojoTestList in dojoTestListCollection)
			{
				if(rowflag) rowCssClass = defaultRowCssClass;
				else rowCssClass = alternateRowCssClass;

				rowflag = !rowflag;

				output.WriteBeginTag("tr");
				output.WriteAttribute("i", dojoTestList.ID.ToString());
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Indent++;

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Write(dojoTestList.Test.Name);
				output.Write("<br><em>");
				output.Write(dojoTestList.EditorComments);
				output.Write("</em>");
				output.WriteEndTag("td");
				output.WriteLine();

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Write(dojoTestList.Test.TestDate);
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
