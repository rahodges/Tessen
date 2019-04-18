using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Text;
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
		ToolboxData("<{0}:DojoAttendanceEntryGrid runat=server></{0}:DojoAttendanceEntryGrid>")]
	public class DojoAttendanceEntryGrid : TableGrid 
	{
		private string connectionString;

        private DropDownList ddSearchMode;
        private DropDownList ddMembers;
        private DropDownList ddClassDefinitions;
        private DropDownList ddInstructors;
        private DropDownList ddView;

		private DateTime localTime = DateTime.MinValue;

		private DojoAttendanceEntryCollection attendance;
		
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

		#endregion

		public DojoAttendanceEntryGrid() : base()
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
			ddSearchMode.AutoPostBack = true;
			Controls.Add(ddSearchMode);

            ddMembers = new DropDownList();
            ddMembers.ID = "ddMembers";
			ddMembers.AutoPostBack = true;
			Controls.Add(ddMembers);

            ddInstructors = new DropDownList();
            ddInstructors.ID = "ddInstructors";
			ddInstructors.AutoPostBack = true;
			Controls.Add(ddInstructors);

            ddClassDefinitions = new DropDownList();
            ddClassDefinitions.ID = "ddClassDefinitions";
            ddClassDefinitions.AutoPostBack = true;
            Controls.Add(ddClassDefinitions);

            ddView = new DropDownList();
            ddView.ID = "ddView";
			ddView.Items.Add(new ListItem("Default", "default"));
			ddView.Items.Add(new ListItem("Weekly", "weekly"));
			ddView.Items.Add(new ListItem("Weekly Summary", "weeklysummary"));
			ddView.AutoPostBack = true;
			ddView.EnableViewState = false;
			Controls.Add(ddView);

            toolbars[0].Items.Add(ToolBarUtility.Break());
            ToolBarUtility.AddControlItem(toolbars[0], ddSearchMode);
            ToolBarUtility.AddControlItem(toolbars[0], ddMembers);
            ToolBarUtility.AddControlItem(toolbars[0], ddClassDefinitions);
            ToolBarUtility.AddControlItem(toolbars[0], ddInstructors);

            toolbars[0].Items.Add(ToolBarUtility.Break());
            ToolBarUtility.AddControlItem(toolbars[0], ddView);

            ChildControlsCreated = true;

            bindDropDownLists();
		}

		private void bindDropDownLists()
		{
			DojoMemberCollection members = 
				new DojoMemberManager().GetCollection(string.Empty, 
				"LastName, FirstName, MiddleName", 
				DojoMemberFlags.PrivateContact);

			if(ddSearchMode.Items.Count == 0)
			{
				ddSearchMode.Items.Add("Today");
				ddSearchMode.Items.Add("This Week");
				ddSearchMode.Items.Add("Last Week");			
				ddSearchMode.Items.Add("This Month");			
				ddSearchMode.Items.Add("Last Month");
				ddSearchMode.Items.Add("All");
			}

			if(ddMembers.Items.Count == 0)
			{
				ddMembers.Items.Add(new ListItem("All Members", "-1"));
				foreach(DojoMember member in members)
				{
					ListItem i = new ListItem(member.PrivateContact.ConstructName("L, FM."), 
						member.iD.ToString());
					ddMembers.Items.Add(i);
				}
			}

			if(ddClassDefinitions.Items.Count == 0)
			{
				ddClassDefinitions.Items.Add(new ListItem("All Definitions", "-1"));
				ddClassDefinitions.Items.Add(new ListItem("None", "0"));

				DojoClassDefinitionManager cdm = new DojoClassDefinitionManager();
				DojoClassDefinitionCollection definitions = cdm.GetCollection(string.Empty, 
					string.Empty, null);

				foreach(DojoClassDefinition def in definitions)
					ddClassDefinitions.Items.Add(new ListItem(def.ToString(), def.ID.ToString()));
			}

			// TODO: Bind instructors to attendance... fix DBMODEL to create single
			// instance placeholders (multiple similar id's > one placeholder)

			if(ddInstructors.Items.Count == 0)
			{				
				ddInstructors.Items.Add(new ListItem("All Instructors", "-1"));
			
				foreach(DojoMember member in members)
				{
					if(member.IsInstructor)
					{
						ListItem i = new ListItem(member.PrivateContact.ConstructName("L, FM."), 
							member.iD.ToString());
						ddInstructors.Items.Add(i);
					}
				}
			}
		}

		#endregion

		#region Rendering
        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();

			DojoAttendanceEntryFlags[] options = new DojoAttendanceEntryFlags[]
			{
				DojoAttendanceEntryFlags.Class,
				DojoAttendanceEntryFlags.Member,
				DojoAttendanceEntryFlags.Rank,
				DojoAttendanceEntryFlags.MemberPrivateContact
			};

			DojoAttendanceEntryManager m = new DojoAttendanceEntryManager();
			
			StringBuilder whereQuery = new StringBuilder();

			if(ddMembers.SelectedItem.Value != "-1")
			{
				whereQuery.Append("MemberID=");
				whereQuery.Append(ddMembers.SelectedItem.Value);				
			}

			if(ddClassDefinitions.SelectedItem.Value != "-1")
			{
				if(whereQuery.Length > 0)
					whereQuery.Append(" AND ");

				if(ddClassDefinitions.SelectedItem.Value == "0")
					whereQuery.Append("ParentDefinitionID=null");
				else
				{
					whereQuery.Append("ParentDefinitionID=");
					whereQuery.Append(ddClassDefinitions.SelectedItem.Value);
				}
			}

			if(ddInstructors.SelectedItem.Value != "-1")
			{
				if(whereQuery.Length > 0)
					whereQuery.Append(" AND ");

				whereQuery.Append("kitTessen_Classes.InstructorID=");
				whereQuery.Append(ddInstructors.SelectedItem.Value);
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
					whereQuery.Append(DateManipulator.SubtractMonths(LocalTime.Date, 1));
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
			}

			attendance = 
				m.GetCollection(whereQuery.ToString(), "ClassStart DESC", options);

			switch(ddView.SelectedItem.Value)
			{
				case "default":
					RenderView(output, false);
					break;
				case "weekly":
					RenderView(output, true);
					break;
				case "weeklysummary":
					RenderWeeklySummary(output);
					break;
			}
		}

		private void RenderView(HtmlTextWriter output, bool showWeeklyIndex)
		{
			// Variables for holding the days of a week
			DateTime currentIndexDate = DateTime.MinValue;
			DateTime lastIndexDate = DateTime.MinValue;

			bool rowflag = false;
			string rowCssClass;	

			//
			// Render Records
			//
			foreach(DojoAttendanceEntry entry in attendance)
			{
				#region Render Indexing

				// Render Week Headers
				if(showWeeklyIndex)
				{
					// Set the first and last day of week for the current entry
					currentIndexDate = DateManipulator.FirstOfWeek(entry.Class.ClassStart);

					if(lastIndexDate != currentIndexDate)
					{
						output.WriteFullBeginTag("tr");
						output.WriteBeginTag("td");
						output.WriteAttribute("class", indexRowCssClass);
						output.WriteAttribute("colspan", "8");
						output.Write(HtmlTextWriter.TagRightChar);
						output.Write(currentIndexDate.ToLongDateString());
						output.Write(" - ");
						output.Write(DateManipulator.LastOfWeek(currentIndexDate).ToLongDateString());					
						output.WriteEndTag("td");
						output.WriteEndTag("tr");

						lastIndexDate = currentIndexDate;
					}
				}
				else
				{
					currentIndexDate = entry.Class.ClassStart.Date;

					if(currentIndexDate != lastIndexDate)
					{
						output.WriteFullBeginTag("tr");
						output.WriteBeginTag("td");
						output.WriteAttribute("valign", "top");
						output.WriteAttribute("colspan", "4");
						output.WriteAttribute("class", indexRowCssClass);
						output.Write(HtmlTextWriter.TagRightChar);
						output.Write(currentIndexDate.ToLongDateString());
						output.WriteEndTag("td");
						output.WriteEndTag("tr");

						lastIndexDate = currentIndexDate;
					}
				}

				#endregion

				// Flip Rowstate
				if(rowflag)		rowCssClass = this.defaultRowCssClass;
				else			rowCssClass = this.alternateRowCssClass;
				rowflag = !rowflag;

				renderEntry(output, rowCssClass, entry);
			}
		}

		private void renderEntry(HtmlTextWriter output, string rowCssClass, 
			DojoAttendanceEntry entry)
		{
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

			if(ddMembers.SelectedItem.Value != "-1")
				output.Write(entry.Class.ClassStart.ToString("f"));
			else
				output.Write(entry.Member.PrivateContact.FullName);

			output.WriteEndTag("td");
			output.WriteLine();

			//
			// Render Class Date
			//
			if(ddMembers.SelectedItem.Value == "-1")
			{
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.Class.ClassStart.ToString("f"));
				output.WriteEndTag("td");
				output.WriteLine();
			}

			// Render Class
			output.WriteBeginTag("td");
			output.WriteAttribute("class", rowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(entry.Class.Name);
			output.WriteEndTag("td");
			output.WriteLine();

			// Render Rank
			output.WriteBeginTag("td");
			output.WriteAttribute("class", rowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(entry.Rank.Name);
			output.WriteEndTag("td");
			output.WriteLine();

			output.Indent--;
			output.WriteEndTag("tr");
			output.WriteLine();
		}

		private void RenderWeeklySummary(HtmlTextWriter output)
		{
			if(attendance.Count == 0)
				return;

			int entryIndex = 0;

			// Variable to hold the current working entry
			DojoAttendanceEntry entry = attendance[entryIndex];

			// Variable to hold first and last class dates to start and stop the loop
            DojoAttendanceEntry lastEntry = attendance[attendance.Count-1];

			// Variables for holding the first date of each week
			DateTime currentFirstDayOfWeek = DateManipulator.FirstOfWeek(entry.Class.ClassStart);
			DateTime previousFirstDayOfWeek = currentFirstDayOfWeek - TimeSpan.FromDays(7);

			// Set hit counters to -1 to inform loop on first run
			int beginnerHits = 0;
			int mudanshaHits = 0;
			int yudanshaHits = 0;
			int totalHits = 0;	
		
			while(entry != lastEntry)
			{
				while(entry.Class.ClassStart > currentFirstDayOfWeek)
				{
					// Increment Counters
					totalHits++;

					if(entry.Rank.ID == 1)
						beginnerHits++;
					if(entry.Rank.IsMudansha)
						mudanshaHits++;
					if(entry.Rank.IsYudansha)
						yudanshaHits++;

					entryIndex++;

					if(entryIndex < attendance.Count)
						entry = attendance[entryIndex];
					else
						break;
				}

				renderWeeklySummary(output, this.DefaultRowCssClass, 
					currentFirstDayOfWeek,
					DateManipulator.LastOfWeek(currentFirstDayOfWeek),
					beginnerHits, mudanshaHits, yudanshaHits, totalHits);

				// Reset Counters
				beginnerHits = 0;
				mudanshaHits = 0;
				yudanshaHits = 0;
				totalHits = 0;

				currentFirstDayOfWeek = previousFirstDayOfWeek;
				previousFirstDayOfWeek = currentFirstDayOfWeek - TimeSpan.FromDays(7);
			}
		}	

		private void renderWeeklySummary(HtmlTextWriter output, string rowCssClass,
			DateTime firstDay, DateTime lastDay,
			int beginnerHits, int mudanshaHits, int yudanshaHits, int totalHits)
		{
			output.WriteFullBeginTag("tr");
			output.WriteLine();
			output.Indent++;

			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", rowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);		
			output.Write(firstDay.ToShortDateString());
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", rowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(lastDay.ToShortDateString());
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", rowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);		
//			output.Write("Beginners: ");
			output.Write(beginnerHits);
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", rowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);		
//			output.Write("Mudansha: ");
			output.Write(mudanshaHits);
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", rowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);		
//			output.Write("Yudansha: ");
			output.Write(yudanshaHits);
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", rowCssClass);
			output.Write(HtmlTextWriter.TagRightChar);		
//			output.Write("Total Members: ");
			output.Write(totalHits);
			output.WriteEndTag("td");

			output.Indent--;
			output.WriteEndTag("tr");
			output.WriteLine();
		}

		#endregion
	}
}