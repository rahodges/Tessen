using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web;
using Amns.GreyFox.Scheduling;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen;
using Amns.GreyFox.Web;

namespace Amns.Tessen.Web.UI.WebControls
{
	public enum DojoSeminarGridStyle: byte {Default = 0, Stacked = 1}

	/// <summary>
	/// Summary description for MemberListGrid.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:DojoSeminarGrid runat=server></{0}:DojoSeminarGrid>")]
	public class DojoSeminarGrid : TableGrid
	{		
		private bool registrationEnabled = false;
		private string selectUrlFormat = string.Empty;	// {0} inserts ID
		private string registerUrlFormat = string.Empty;
		private bool overrideSelectUrl = false;
		private bool localOnly = false;

		private DateTime maxDate = DateTime.MaxValue;
		private DateTime minDate = DateTime.MinValue;
		private string pdfLinkActiveIconUrl;
		private string pdfLinkDisabledIconUrl;
		private DojoSeminarGridStyle displayStyle;
		
		#region Public Properties

		[Bindable(true), Category("Behavior")]
		public DateTime MaxDate
		{
			get
			{
				return maxDate;
			}
			set
			{
				maxDate = value;
			}
		}

		[Bindable(true), Category("Behavior")]
		public DateTime MinDate
		{
			get
			{
				return minDate;
			}
			set
			{
				minDate = value;
			}
		}

		[Bindable(true), Category("Icons")]
		public string PdfLinkActiveIconUrl
		{
			get
			{
				return pdfLinkActiveIconUrl;
			}
			set
			{
				pdfLinkActiveIconUrl = value;
			}
		}

		[Bindable(true), Category("Icons")]
		public string PdfLinkDisabledIconUrl
		{
			get
			{
				return pdfLinkDisabledIconUrl;
			}
			set
			{
				pdfLinkDisabledIconUrl = value;
			}
		}

		[Bindable(true), Category("Behavior")]
		public DojoSeminarGridStyle DisplayStyle
		{
			get
			{
				return displayStyle;
			}
			set
			{
				displayStyle = value;
			}
		}

		[Bindable(true), Category("Behavior")]
		public string SelectUrlFormat
		{
			get
			{
				return selectUrlFormat;
			}
			set
			{
				selectUrlFormat = value;
			}
		}

		[Bindable(true), Category("Behavior")]
		public string RegisterUrlFormat
		{
			get
			{
				return registerUrlFormat;
			}
			set
			{
				registerUrlFormat = value;
			}
		}

		[Bindable(true), Category("Behavior"), DefaultValue(false)]
		public bool OvverideSelectUrl
		{
			get
			{
				return overrideSelectUrl;
			}
			set
			{
				overrideSelectUrl = value;
			}
		}

		[Bindable(true), Category("Behavior"), DefaultValue(false)]
		public bool LocalOnly
		{
			get
			{
				return localOnly;
			}
			set
			{
				localOnly = value;
			}
		}

		#endregion

		public DojoSeminarGrid() : base()
		{
			this.features |= TableWindowFeatures.ClientSideSelector;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit (e);

			bool adminMode = Page.User.IsInRole("Tessen/Administrator");
			this.deleteButton.Enabled = adminMode;
			this.editButton.Enabled = adminMode;
			this.newButton.Enabled = adminMode;
		}

		protected override void OnLoad(EventArgs e)
		{
			switch(displayStyle)
			{
				case DojoSeminarGridStyle.Default:
                        Amns.GreyFox.Web.UI.WebControls.ControlExtender.RegisterTooltipScript(this.Page, 1, "grey", 2, "white");
					break;
				case DojoSeminarGridStyle.Stacked:
					components = 0;
					features = TableWindowFeatures.DisableContentSeparation;
					this.columnCount = 2;
					break;
			}

			base.OnLoad(e);
		}

		#region Rendering

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();

			string whereQuery = string.Empty;
			if(minDate != DateTime.MinValue)
				whereQuery = "EndDate>=#" + minDate.ToString() + "#";
			if(maxDate != DateTime.MaxValue)
			{
				if(whereQuery.Length != 0)
					whereQuery += " AND ";
				whereQuery += "EndDate<=#" + maxDate.ToString() + "#";
			}

			if(localOnly)
			{
				if(whereQuery.Length != 0)
					whereQuery += " AND ";
				whereQuery += "IsLocal=true";
			}
            
			DojoSeminarManager m = new DojoSeminarManager();
			DojoSeminarCollection seminars;

			switch(displayStyle)
			{
				case DojoSeminarGridStyle.Stacked:
					seminars = m.GetCollection(whereQuery, "StartDate", DojoSeminarFlags.Location);
					renderStackedGrid(output, seminars);
					break;
				case DojoSeminarGridStyle.Default:
					seminars = m.GetCollection(whereQuery, "StartDate DESC", DojoSeminarFlags.Location);
					renderDefaultGrid(output, seminars);
					break;
				default:
					goto case DojoSeminarGridStyle.Default;
			}
		}

		protected void renderDefaultGrid(HtmlTextWriter output, DojoSeminarCollection seminars)
		{
			bool rowflag = false;
			string rowCssClass;

			DateTime currentMonth = DateTime.MinValue;
			DateTime lastMonth = DateTime.MinValue;

			//
			// Render Records
			//
			foreach(DojoSeminar seminar in seminars)
			{				
//				if(seminar.StartDate <= DateTime.MinValue)
//					seminar.StartDate = DateTime.Parse("1/1/1980");
//
//				lastMonth = currentMonth;
//				currentMonth = DateManipulator.FirstOfMonth(seminar.StartDate).Date;

//				if(lastMonth != currentMonth)
//				{
//					output.WriteFullBeginTag("tr");
//					output.WriteBeginTag("td");
//					output.WriteAttribute("class", indexRowCssClass);
////					output.WriteAttribute("colspan", "1");
//					output.Write(HtmlTextWriter.TagRightChar);
//					output.Write(currentMonth.ToString("MMMM yyyy"));
//					output.WriteEndTag("td");
//					output.WriteEndTag("tr");
//				}

				if(rowflag)					rowCssClass = this.defaultRowCssClass;
				else						rowCssClass = this.alternateRowCssClass;
				rowflag = !rowflag;

				output.WriteBeginTag("tr");
				output.WriteAttribute("i", seminar.ID.ToString());
				
				// Tooltip Properties
//				output.WriteAttribute("onmouseover", 
//					Amns.GreyFox.Web.UI.WebControls.ControlExtender.GetTooltipStartReference("Seminar ID: " + seminar.ID.ToString(),"#ffffe0", 0));
//				output.WriteAttribute("onmouseout", Amns.GreyFox.Web.UI.WebControls.ControlExtender.GetTooltipEndReference());

				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Indent++;

				//
				// Render Main Representation of Record
				//				
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("<strong>");
				output.Write(seminar.Name);
				output.Write("</strong>");
				output.Write("<br>");
				output.Write(seminar.Location.BusinessName);
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Seminar Dates
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(seminar.ConstructDateLongString());
				output.WriteEndTag("td");
				output.WriteLine();

				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		protected void renderStackedGrid(HtmlTextWriter output, DojoSeminarCollection seminars)
		{
			bool rowflag = false;
			string rowCssClass;

			//
			// Render Records
			//
			foreach(DojoSeminar seminar in seminars)
			{				
				if(seminar.iD == selectedID)	rowCssClass = this.selectedRowCssClass;
				else if(rowflag)				rowCssClass = this.defaultRowCssClass;
				else							rowCssClass = this.alternateRowCssClass;
				rowflag = !rowflag;

				output.WriteFullBeginTag("tr");
				output.WriteLine();
				output.Indent++;

				//
				// Render Main Representation of Record
				//				
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				if(rowCssClass != string.Empty)
				{
					output.WriteAttribute("class", rowCssClass);
				}
				output.Write(HtmlTextWriter.TagRightChar);
				
				if(selectEnabled)
				{
					output.WriteBeginTag("a");
					if(overrideSelectUrl & seminar.DetailsOverrideUrl != "")
						output.WriteAttribute("href", Page.ResolveUrl(seminar.DetailsOverrideUrl));
					else if(selectUrlFormat != string.Empty)
						output.WriteAttribute("href", Page.ResolveUrl(string.Format(selectUrlFormat, seminar.ID)));
					else
                        output.WriteAttribute("href", "javascript:" + GetSelectReference(seminar.ID));
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write(seminar.Name);
					output.WriteEndTag("a");					
				}
				else
				{
					output.Write(seminar.Name);					
				}

				output.Write("<br>");

				// Intelligently Output Dates
				if(seminar.StartDate.Date == seminar.EndDate.Date)
				{
					output.Write(seminar.StartDate.ToLongDateString());
				}
				else
				{
					output.Write(seminar.StartDate.ToLongDateString());
					output.Write(" - ");
					output.Write(seminar.EndDate.ToLongDateString());
				}

				output.Write("<br>");
				output.Write(seminar.Description);

				if(registrationEnabled && seminar.registrationEnabled)
				{
					output.Write("<br>");
					output.WriteBeginTag("a");
					if(registerUrlFormat != string.Empty)
						output.WriteAttribute("href", Page.ResolveUrl(string.Format(registerUrlFormat, seminar.ID)));
					else
						output.WriteAttribute("href", "javascript:" + Page.ClientScript.GetPostBackEventReference(this, "reg_" + seminar.ID));
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write("register online");
					output.WriteEndTag("a");
				}

				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render PDF Link and Registration Link
				//
				output.WriteBeginTag("td");
				if(rowCssClass != string.Empty)
				{
					output.WriteAttribute("class", rowCssClass);
				}
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("align", "right");
				output.Write(HtmlTextWriter.TagRightChar);
				if(seminar.pdfUrl == string.Empty)
				{
					output.WriteBeginTag("img");
					output.WriteAttribute("src", Page.ResolveUrl(pdfLinkDisabledIconUrl));
					output.WriteAttribute("border", "0");
					output.Write(HtmlTextWriter.SelfClosingTagEnd);
				}
				else
				{
					output.WriteBeginTag("a");
					output.WriteAttribute("href", Page.ResolveUrl(seminar.pdfUrl));
					output.Write(HtmlTextWriter.TagRightChar);
					output.WriteBeginTag("img");
					output.WriteAttribute("src", Page.ResolveUrl(pdfLinkActiveIconUrl));
					output.WriteAttribute("border", "0");
					output.WriteAttribute("alt", "Download PDF");
					output.Write(HtmlTextWriter.SelfClosingTagEnd);
					output.WriteEndTag("a");
				}
				
				output.WriteEndTag("td");
				output.WriteLine();

				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

		protected override void RenderViewPane(HtmlTextWriter output)
		{
			this.InitializeRenderHelpers(output);

			DojoSeminar seminar = new DojoSeminar(int.Parse(HttpContext.Current.Request.QueryString[0]));
	
			RenderTableBeginTag("_viewPanel", this.CellPadding, this.CellSpacing, Unit.Percentage(100), Unit.Empty, this.CssClass);
           
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("th");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("class", this.HeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(seminar.Name);
			output.Write(" (" + seminar.ID.ToString() + ")");
			output.WriteEndTag("th");
			output.WriteEndTag("tr");

			#region General Information

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("class", this.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("General");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("class", this.DefaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Location</strong> : ");
			output.Write(seminar.Location.BusinessName);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("class", this.DefaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Partial Registration Fees</strong> : ");
            switch (seminar.ClassUnitType)
            {
                case DojoSeminarClassUnitType.Day:
                    output.Write(seminar.ClassUnitFee.ToString("c"));
                    output.Write(" Per Day");
                    break;
                case DojoSeminarClassUnitType.Class:
                    output.Write(seminar.ClassUnitFee.ToString("c"));
                    output.Write(" Per Class");
                    break;
                case DojoSeminarClassUnitType.None:
                    output.Write(" None ");
                    break;
            }
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("class", this.DefaultRowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Full Registration Fee</strong> : ");
			output.Write(seminar.FullRegistrationFee.ToString("c"));
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			#endregion

			#region Students Attended

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("class", this.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Students");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			DojoSeminarRegistrationManager dsrm = new DojoSeminarRegistrationManager();
			DojoSeminarRegistrationCollection registrations = dsrm.GetCollection("ParentSeminarID=" + seminar.ID.ToString(), "LastName, FirstName, MiddleName", 
				DojoSeminarRegistrationFlags.Contact);

			foreach(DojoSeminarRegistration registration in registrations)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", this.defaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(registration.Contact.ConstructName("LS,FMi."));
				output.WriteEndTag("td");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", this.defaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(registration.IsPaid)
					output.Write("Paid");
				else
					output.Write("Preregistration");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}

			#endregion

			output.WriteEndTag("table");
		}
	}
}