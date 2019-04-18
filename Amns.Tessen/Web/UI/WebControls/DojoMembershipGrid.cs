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
	/// A custom grid for DojoMembership.
	/// </summary>
	/// </summary>
	[ToolboxData("<{0}:DojoMembershipGrid runat=server></{0}:DojoMembershipGrid>")]
	public class DojoMembershipGrid : TableGrid
	{

		#region Public Properties
		#endregion

		protected override void OnInit(EventArgs e)
		{
            base.OnInit(e);

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
			DojoMembershipManager m = new DojoMembershipManager();
			DojoMembershipCollection dojoMembershipCollection = m.GetCollection(string.Empty,
                "Member_PrivateContact.LastName, Member_PrivateContact.FirstName, Member_PrivateContact.LastName",
                DojoMembershipFlags.Member,
                DojoMembershipFlags.MemberPrivateContact,
                DojoMembershipFlags.MembershipTemplate);
			// Render Header Row
			this.headerLockEnabled = true;
			RenderRow(this.HeaderRowCssClass, "Member", "Name");

			bool rowflag = false;
			string rowCssClass;
			//
			// Render Records
			//
			foreach(DojoMembership dojoMembership in dojoMembershipCollection)
			{
				if(rowflag) rowCssClass = defaultRowCssClass;
				else rowCssClass = alternateRowCssClass;
				rowflag = !rowflag;
				output.WriteBeginTag("tr");
				output.WriteAttribute("i", dojoMembership.ID.ToString());
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Indent++;
                
                output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
                output.WriteLine(dojoMembership.Member.PrivateContact.FullName);
                output.WriteEndTag("td");

                output.WriteBeginTag("td");
                output.WriteAttribute("class", rowCssClass);
                output.Write(HtmlTextWriter.TagRightChar);
                output.WriteLine(dojoMembership.MembershipTemplate.Name);
                output.WriteEndTag("td");

				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

	}
}
