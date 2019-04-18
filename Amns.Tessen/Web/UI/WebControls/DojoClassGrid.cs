using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen;
using Amns.GreyFox.Scheduling;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for MemberListGrid.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:DojoClassGrid runat=server></{0}:DojoClassGrid>")]
	public class DojoClassGrid : TableGrid 
	{
		private string connectionString;

		private int parentSeminarID = -1;			// '0' Delists classes associated with seminars.
													// '-1' Lists all classes associated with seminars.
		private int parentDefinitionID = -1;		// '0' Delists classes associated with definitions.
													// '-1' Lists all classes associated with definitions.
		private int copyID = -1;

		private DateTime localTime = DateTime.MinValue;
	
		private CheckBox cbSeminarFilter;
		private DropDownList ddSearchMode;
		
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

		[Bindable(true),
		Category("Behavior"),
		DefaultValue("")]
		public DateTime LocalTime
		{
			get
			{
				if(localTime == DateTime.MinValue)
					return DateTime.Now;
				return localTime;
			}
			set
			{
				localTime = value;
			}
		}

		[Bindable(true),
		Category("Behavior"),
		DefaultValue(-1)]
		public int ParentSeminarID
		{
			get
			{
				return parentSeminarID;
			}
			set
			{
				parentSeminarID = value;
			}
		}

		[Bindable(true),
		Category("Behavior"),
		DefaultValue(-1)]
		public int ParentDefinitionID
		{
			get
			{
				return parentDefinitionID;
			}
			set
			{
				parentDefinitionID = value;
			}
		}

		#endregion

		public DojoClassGrid() : base()
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

		#region Child Control Methods and Event Handling

		protected override void CreateChildControls()
		{
            base.CreateChildControls();

            ddSearchMode = new DropDownList();
            ddSearchMode.ID = "ddSearchMode";
			if(ddSearchMode.Items.Count == 0)
			{
				ddSearchMode.Items.Add("Today");
				ddSearchMode.Items.Add("This Week");
				ddSearchMode.Items.Add("Last Week");			
				ddSearchMode.Items.Add("This Month");			
				ddSearchMode.Items.Add("Last Month");
				ddSearchMode.Items.Add("This Year");
				ddSearchMode.Items.Add("All");
			}			
			ddSearchMode.AutoPostBack = true;
			Controls.Add(ddSearchMode);

            cbSeminarFilter = new CheckBox();
            cbSeminarFilter.ID = "cbSeminarFilter";
			cbSeminarFilter.Text = "Seminars Only";
			cbSeminarFilter.AutoPostBack = true;
			Controls.Add(cbSeminarFilter);

            toolbars[0].Items.Add(ToolBarUtility.Break());            
            ToolBarUtility.AddControlItem(toolbars[0], ddSearchMode);
            ToolBarUtility.AddControlItem(toolbars[0], cbSeminarFilter);
            toolbars[0].Items.Add(ToolBarUtility.Copy());
            toolbars[0].Items.Add(ToolBarUtility.Paste());
			
			ChildControlsCreated = true;
		}

		#endregion

		public override void ProcessCommand(string command, string parameters)
		{
			switch(command)
			{
				case "copy":
					this.copyID = this.selectedID;
					break;
				case "paste":
					if(this.copyID != -1)
					{
						DojoClass srcClass = new DojoClass(this.copyID);
						DojoClass destClass = new DojoClass(this.selectedID);

						// Copy class times only across days
						if(srcClass.ClassStart.Date != destClass.ClassStart.Date)
						{
							destClass.ClassStart = new DateTime(destClass.ClassStart.Year,
								destClass.ClassStart.Month,
								destClass.ClassStart.Day,
								srcClass.ClassStart.Hour,
								srcClass.ClassStart.Minute,
								srcClass.ClassStart.Second,
								srcClass.ClassStart.Millisecond);
							destClass.ClassEnd = new DateTime(destClass.ClassEnd.Year,
								destClass.ClassEnd.Month,
								destClass.ClassEnd.Day,
								srcClass.ClassEnd.Hour,
								srcClass.ClassEnd.Minute,
								srcClass.ClassEnd.Second,
								srcClass.ClassEnd.Millisecond);
						}
						destClass.Name = srcClass.Name;
						destClass.Instructor = srcClass.Instructor;

						// TODO: Check parent definition to see if this copied class falls in the definition specs.
						destClass.ParentDefinition = null;
						destClass.ParentSeminar = srcClass.ParentSeminar;
						destClass.Location = srcClass.Location;
						destClass.Save();
					}
					break;
			}
		}


		#region Rendering
        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();
			StringBuilder whereQuery = new StringBuilder();

			DojoClassManager m = new DojoClassManager();
			
			//
			// Qualify Parent Definition Selection in Query
			//
			if(parentDefinitionID == 0)
				whereQuery.Append("(ParentDefinitionID=null OR ParentDefinitionID=0) ");
			else if(parentDefinitionID > 0)
				whereQuery.Append("ParentDefinitionID=" + parentDefinitionID.ToString() + ") ");
		
			//
			// Qualify Parent Seminar Selection in Query
			//
			if(parentSeminarID == 0)
			{
				if(whereQuery.Length > 0)
					whereQuery.Append("AND (ParentSeminarID=null OR ParentSeminarID=0) ");
				else
					whereQuery.Append("(ParentSeminarID=null OR ParentSeminarID=0) ");
			}
			else if(parentSeminarID > 0)
			{
				whereQuery.Append("(ParentSeminarID=" + parentSeminarID.ToString() + ") ");
			}

			switch(ddSearchMode.SelectedItem.Text)
			{				
				case "Today":			// today
					if(whereQuery.Length > 0)
						whereQuery.Append(" AND ");
					whereQuery.Append("(ClassStart>=#");
					whereQuery.Append(LocalTime.Date);
					whereQuery.Append("# AND ClassStart <#");
					whereQuery.Append(LocalTime.Date.AddDays(1));
					whereQuery.Append("#)");
					break;
				case "This Month":
					if(whereQuery.Length > 0)
						whereQuery.Append(" AND ");
					whereQuery.Append("(ClassStart>=#");
					whereQuery.Append(DateManipulator.FirstOfMonth(LocalTime.Date));
					whereQuery.Append("# AND ClassStart <#");
					whereQuery.Append(DateManipulator.FirstOfMonth(LocalTime.Date).AddMonths(1));
					whereQuery.Append("#)");
					break;
				case "Last Month":
					if(whereQuery.Length > 0)
						whereQuery.Append(" AND ");
					whereQuery.Append("(ClassStart>=#");
					whereQuery.Append(DateManipulator.SubtractMonths(DateManipulator.FirstOfMonth(LocalTime.Date), 1));
					whereQuery.Append("# AND ClassStart <#");
					whereQuery.Append(DateManipulator.FirstOfMonth(LocalTime.Date));
					whereQuery.Append("#)");
					break;
				case "This Week":
					if(whereQuery.Length > 0)
						whereQuery.Append(" AND ");
					whereQuery.Append("(ClassStart>=#");
					whereQuery.Append(DateManipulator.FirstOfWeek(LocalTime.Date));
					whereQuery.Append("# AND ClassStart <#");
					whereQuery.Append(DateManipulator.LastOfWeek(LocalTime.Date).AddDays(1));
					whereQuery.Append("#)");
					break;
				case "Last Week":
					if(whereQuery.Length > 0)
						whereQuery.Append(" AND ");
					whereQuery.Append("(ClassStart>=#");
					whereQuery.Append(DateManipulator.FirstOfWeek(LocalTime.Date).Subtract(TimeSpan.FromDays(7)));
					whereQuery.Append("# AND ClassStart <#");
					whereQuery.Append(DateManipulator.LastOfWeek(LocalTime.Date).Subtract(TimeSpan.FromDays(6)));
					whereQuery.Append("#)");
					break;
				case "This Year":
					if(whereQuery.Length > 0)
						whereQuery.Append(" AND ");
					whereQuery.Append("(ClassStart>=#");
					whereQuery.Append(DateTime.Parse("1/1/" + LocalTime.Date.Year.ToString()));
					whereQuery.Append("# AND ClassStart <#");
					whereQuery.Append(DateTime.Parse("1/1/" + (LocalTime.Date.Year + 1).ToString()));
					whereQuery.Append("#)");
					break;
			}

			if(cbSeminarFilter.Checked)
			{
				if(whereQuery.Length > 0)
					whereQuery.Append(" AND ");
				whereQuery.Append("ParentSeminarID IS NOT NULL");
			}

			DojoClassCollection dojoClassCollection = m.GetCollection(whereQuery.ToString(), "ClassStart ASC",
				new DojoClassFlags[] {
					DojoClassFlags.Instructor,
					DojoClassFlags.InstructorPrivateContact});

			bool rowflag = false;
			string rowCssClass;	
			string currentIndex = string.Empty;
			string previousIndex = string.Empty;

			//
			// Render Records
			//
			foreach(DojoClass entry in dojoClassCollection)
			{
				#region Index Rows Rendering

				currentIndex = entry.ClassStart.ToLongDateString();

				if(currentIndex != previousIndex)
				{
					output.WriteFullBeginTag("tr");
					output.WriteBeginTag("td");
					output.WriteAttribute("valign", "top");
					output.WriteAttribute("colspan", "4");
					output.WriteAttribute("class", indexRowCssClass);
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write(currentIndex);
					output.WriteEndTag("td");
					output.WriteEndTag("tr");

					previousIndex = currentIndex;
				}

				#endregion

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
				output.Write(entry.Instructor.PrivateContact.FullName);
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Class Start and End Dates
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
//				output.Write(entry.ClassStart.ToLongDateString());
//				output.Write("<br />");
				output.Write(entry.ClassStart.ToShortTimeString());
				output.Write(" - ");
				output.Write(entry.ClassEnd.ToShortTimeString());
				output.WriteEndTag("td");
				output.WriteLine();

				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

		#region Render Viewpane

		protected override void RenderViewPane(HtmlTextWriter output)
		{
			if(ConnectionString == string.Empty)
				output.Write("Empty Connection String!");

			DojoClass c = new DojoClass(int.Parse(Page.Request.QueryString[0]));

            RenderTableBeginTag("_viewPanel", this.CellPadding, this.CellSpacing, Unit.Percentage(100), Unit.Empty, this.CssClass);
           
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("th");
			output.WriteAttribute("class", this.HeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(c.Name);
			output.WriteEndTag("th");
			output.WriteEndTag("tr");

			#region Students Attended

			DojoAttendanceEntryManager aem = new DojoAttendanceEntryManager();
			DojoAttendanceEntryCollection entries = aem.GetCollection("ClassID=" + c.ID.ToString(), "LastName, FirstName, MiddleName", 
				DojoAttendanceEntryFlags.Member, DojoAttendanceEntryFlags.MemberPrivateContact);

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Students (" + entries.Count.ToString() + ")");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");			

			foreach(DojoAttendanceEntry entry in entries)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", this.defaultRowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.Member.PrivateContact.ConstructName("LS,FMi."));
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}

			#endregion

			output.WriteEndTag("table");
		}

		#endregion

		#region Viewstate Methods

		protected override void LoadViewState(object savedState) 
		{

			// Customize state management to handle saving state of contained objects.

			if (savedState != null) 
			{
				object[] myState = (object[])savedState;

				if (myState[0] != null) base.LoadViewState(myState[0]);
				if (myState[1] != null) copyID = (int) myState[1];
			}

		}

		protected override object SaveViewState() 
		{
			// Customized state management to handle saving state of contained objects  such as styles.

			object baseState = base.SaveViewState();
			
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = copyID;

			return myState;
		}

		#endregion

	}
}