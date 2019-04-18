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
		ToolboxData("<{0}:DojoPromotionGrid runat=server></{0}:DojoPromotionGrid>")]
	public class DojoPromotionGrid : TableGrid
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

		protected override void OnInit(EventArgs e)
		{
			base.OnInit (e);

			this.features |= TableWindowFeatures.ClientSideSelector;

			this.headerSortEnabled = true;
			this.headerLockEnabled = true;

			bool adminMode = Page.User.IsInRole("Tessen/Administrator");
			this.deleteButton.Enabled = adminMode;
			this.editButton.Enabled = adminMode;
			this.newButton.Enabled = adminMode;
		}

		#region Rendering

		protected override void RenderContentHeader(HtmlTextWriter output)
		{
			RenderRow(this.headerRowCssClass, 
				"Member", 
				"Test", "Test Date", "Test Location", 
				"Promotion Date", "Promotion Grade");
		}

        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();

			DojoPromotionManager m = new DojoPromotionManager();
			DojoPromotionCollection dojoPromotionCollection = m.GetCollection(string.Empty, "PromotionDate DESC", 
				new DojoPromotionFlags[] {
					DojoPromotionFlags.Member,
					DojoPromotionFlags.MemberPrivateContact,
					DojoPromotionFlags.PromotionRank,
					DojoPromotionFlags.Test,
					DojoPromotionFlags.TestLocation});

			bool rowflag = false;
			string rowCssClass;	

			//
			// Render Records
			//
			foreach(DojoPromotion entry in dojoPromotionCollection)
			{
				if(rowflag)			rowCssClass = this.defaultRowCssClass;
				else						rowCssClass = this.alternateRowCssClass;
				rowflag = !rowflag;

				output.WriteBeginTag("tr");
				output.WriteAttribute("i", entry.iD.ToString());
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Indent++;
	
				//
				// Render ID of Record
				//
//				output.WriteBeginTag("td");
//				output.WriteAttribute("class", rowCssClass);
//				output.Write(HtmlTextWriter.TagRightChar);
//				output.Write(entry.ID);
//				output.WriteEndTag("td");
//				output.WriteLine();

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
				// Render Test
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				if(entry.Test != null)
                    output.Write(entry.Test.Name);
				else 
					output.Write("None");
				output.WriteEndTag("td");
				output.WriteLine();

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				if(entry.Test != null)
                    output.Write(entry.Test.TestDate.ToShortDateString());
				else
					output.Write("None");
				output.WriteEndTag("td");
				output.WriteLine();

				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				if(entry.Test != null)
                    output.Write(entry.Test.Location.BusinessName);
				else
					output.Write("None");
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Promotion Date
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.PromotionDate.ToShortDateString());
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Promotion Rank
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.PromotionRank.Name);
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Active Promotion
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				if(entry.Member.Rank == entry.PromotionRank)
					output.Write("Active");
				else
					output.Write("&nbsp;");
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