using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Rappahanock;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// <summary>
	/// A custom grid for DojoSeminarOption.
	/// </summary>
	/// </summary>
	[ToolboxData("<{0}:DojoSeminarOptionGrid runat=server></{0}:DojoSeminarOptionGrid>")]
	public class DojoSeminarOptionGrid : TableGrid
	{

		#region Public Properties
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
			DojoSeminarOptionManager m = new DojoSeminarOptionManager();
			DojoSeminarOptionCollection dojoSeminarOptionCollection = m.GetCollection(string.Empty, string.Empty, null);
			// Render Header Row
			this.headerLockEnabled = true;
			RenderRow(this.HeaderRowCssClass, "Name", "Description", "Fee");

			bool rowflag = false;
			string rowCssClass;
			//
			// Render Records
			//
			foreach(DojoSeminarOption dojoSeminarOption in dojoSeminarOptionCollection)
			{
				if(rowflag) rowCssClass = defaultRowCssClass;
				else rowCssClass = alternateRowCssClass;
				rowflag = !rowflag;
				output.WriteBeginTag("tr");
				output.WriteAttribute("i", dojoSeminarOption.ID.ToString());
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Indent++;
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("style", "font-weight:bold;");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(dojoSeminarOption.Name);
				output.WriteEndTag("td");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(dojoSeminarOption.Description);
				output.WriteEndTag("td");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(dojoSeminarOption.Fee.ToString("c"));
				output.WriteEndTag("td");
				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

	}
}
