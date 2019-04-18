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
		ToolboxData("<{0}:DojoRankGrid runat=server></{0}:DojoRankGrid>")]
	public class DojoRankGrid : TableGrid 
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
			base.OnInit (e);

			this.features |= TableWindowFeatures.ClientSideSelector;

			this.columnLockEnabled = true;
			this.headerSortEnabled = true;
			this.headerLockEnabled = true;

			bool adminMode = Page.User.IsInRole("Tessen/Administrator");
			this.deleteButton.Enabled = adminMode;
			this.editButton.Enabled = adminMode;
			this.newButton.Enabled = adminMode;
		}

		protected override void RenderContentHeader(HtmlTextWriter output)
		{
			RenderRow(this.headerRowCssClass, 
                "Order #",
				"Rank", 
				"Promotion Rank",
				"Min. Time From Test",
				"Min. Time In Rank");
		}

		#region Rendering

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();

			DojoRankManager m = new DojoRankManager();			
			DojoRankCollection ranks = m.GetCollection(string.Empty, string.Empty, null);

			bool rowflag = false;
			string rowCssClass;		

			//
			// Render Records
			//
			foreach(DojoRank rank in ranks)
			{
				if(rowflag)
					rowCssClass = this.defaultRowCssClass;
				else
					rowCssClass = this.alternateRowCssClass;

				rowflag = !rowflag;

				output.WriteBeginTag("tr");
				output.WriteAttribute("i", rank.iD.ToString());
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Indent++;
	
				//
				// Render OrderNum of Record
				//
                output.WriteBeginTag("td");
                output.WriteAttribute("class", rowCssClass);
                output.Write(HtmlTextWriter.TagRightChar);
                output.Write(rank.orderNum);
                output.WriteEndTag("td");
                output.WriteLine();

				//
				// Render Main Representation of Record
				//				
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);				
				output.Write(rank.name);
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Promotion Rank
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				if(rank.PromotionRank != null)
					output.Write(rank.PromotionRank.Name);
				else
					output.Write("None");
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Promotion Time From Last Test
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(rank.PromotionTimeFromLastTest.TotalDays);
				output.Write(" days");
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Promotion Time In Rank
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(rank.PromotionTimeInRank.TotalHours);
				output.Write(" hrs.");
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