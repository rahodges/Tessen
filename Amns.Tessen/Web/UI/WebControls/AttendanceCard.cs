using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for AttendanceCard.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:AttendanceCard runat=server></{0}:AttendanceCard>")]
	public class AttendanceCard : Amns.GreyFox.Web.UI.WebControls.TableWindow
	{
		string	__connectionString			= string.Empty;
		string	__classTimes				= "07:00,60|09:00,30,30|10:00,30,30|14:00,120,120|18:30,30,30|19:45,30,30";	// commas separate times, use a semicolon to put minutes before and minutes after
		int		__memberID					= -1;
		bool	__fillBlanks				= true;								// fills classes that are blank

		Unit	__dateCellWidth				= Unit.Pixel(13);
		Unit	__dateCellHeight			= Unit.Pixel(13);
		string	__dateCellCssClass			= string.Empty;
		string	__instructorCellCssClass	= string.Empty;
		string	__seminarCellCssClass		= string.Empty;
		string	__blankCellCssClass			= string.Empty;

		#region Public Properties

		#region Behavior

		[Bindable(true), 
		Category("Behavior"), 
		DefaultValue("07:00,09:00,10:00,18:30,19:45")] 
		public string ClassTimes
		{
			get { return __classTimes; }
			set { __classTimes = value; }
		}

		#endregion

		#region Appearance

		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("11px")] 
		public Unit DateCellWidth
		{
			get { return __dateCellWidth; }
			set { __dateCellWidth = value; }
		}

		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("11px")] 
		public Unit DateCellHeight
		{
			get { return __dateCellHeight; }
			set { __dateCellHeight = value; }
		}

		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("")] 
		public string DateCellCssClass 
		{
			get { return __dateCellCssClass; }
			set { __dateCellCssClass = value; }
		}

		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("")] 
		public string SeminarCellCssClass 
		{
			get { return __seminarCellCssClass; }
			set { __seminarCellCssClass = value; }
		}

		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("")] 
		public string InstructorCellCssClass 
		{
			get { return __instructorCellCssClass; }
			set { __instructorCellCssClass = value; }
		}

		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("")] 
		public string BlankCellCssClass 
		{
			get { return __blankCellCssClass; }
			set { __blankCellCssClass = value; }
		}

		#endregion

		#region Data

		[Bindable(true), 
		Category("Data"), 
		DefaultValue("")] 
		public int MemberID 
		{
			get { return __memberID; }
			set { __memberID = value; }
		}

		[Bindable(false),
		Category("Data"),
		DefaultValue("")]
		public string ConnectionString
		{
			get
			{
				return __connectionString;
			}
			set
			{
				// Parse Connection String
				if(value.StartsWith("<jet40virtual>") & Context != null)
					__connectionString = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" +
						Context.Server.MapPath(value.Substring(14, value.Length - 14));
				else if(value.StartsWith("<jet40config>") & Context != null)
					__connectionString = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" +
						Context.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get(value.Substring(13, value.Length - 13)));
				else
					__connectionString = value;
			}
		}

		#endregion

		#endregion

		protected override void OnInit(EventArgs e)
		{
			columnCount = 32;
			features = Amns.GreyFox.Web.UI.WebControls.TableWindowFeatures.DisableContentSeparation;
		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			IFormatProvider enFormat = new System.Globalization.CultureInfo("en-US", true);

			// Attendance Index 
			int attendanceIndex = 0;				// The current attendance entry
			int attendanceIndexMonth = 0;			// The current attendance entry at beginning of month
			int classIndex = 0;						// The current class
			int classIndexMonth = 0;				// The current class at beginning of month
			int year = DateTime.Now.Year;			// This year in an integer

			// Classes
			DojoAttendanceEntryCollection attendance = null;
			DojoClassCollection classes = null;

			// Start card at beginning of year
			DateTime startDate = DateTime.Parse("1/1/" + year.ToString(), enFormat);

			// Start times for classes every day
			DateTime testDateLow		= startDate;
			DateTime testDateHigh		= startDate;
			string[] startStrings		= __classTimes.Split('|');
			DateTime[] startTimes		= new DateTime[startStrings.Length];
			TimeSpan[] startSpansBack	= new TimeSpan[startStrings.Length];
			TimeSpan[] startSpansFor	= new TimeSpan[startStrings.Length];

			// Parse class string
			for(int i = 0; i < startTimes.Length; i++)
			{
				string[] temp = startStrings[i].Split(',');				
				startTimes[i] = DateTime.Parse(temp[0]);
				
				// initialize start spans
				if(temp.Length > 2)
				{
					startSpansFor[i] = TimeSpan.FromMinutes(double.Parse(temp[1]));
					startSpansBack[i] = TimeSpan.FromMinutes(double.Parse(temp[2]));
				}
				else if(temp.Length > 1)
				{
					startSpansFor[i] = TimeSpan.FromMinutes(double.Parse(temp[1]));
					startSpansBack[i] = TimeSpan.Zero;
				}
				else
				{
					startSpansFor[i] = TimeSpan.Zero;
					startSpansBack[i] = TimeSpan.Zero;
				}
			}

			// Load member
			DojoMember m = new DojoMember(__memberID);

			// Load member's attendance
			DojoAttendanceEntryManager aem = new DojoAttendanceEntryManager();
			attendance = aem.GetCollection("MemberID=" + __memberID + 
				" AND ClassStart>#1/1/" + year.ToString() + "#" +
				" AND ClassStart<#1/1/" + (year+1).ToString() + "#", "ClassStart", DojoAttendanceEntryFlags.Class);

			if(__fillBlanks)
			{
				// Load classes
				DojoClassManager cm = new DojoClassManager();
				classes = cm.GetCollection("ClassStart>#1/1/" + year.ToString() + "#" +
					" AND ClassStart<#1/1/" + (year+1).ToString() + "#", "ClassStart", null);
			}

			// If there is no attendance, display no attendance error
			if(attendance.Count == 0)
			{
				output.WriteFullBeginTag("tr");
				output.WriteFullBeginTag("td");
				output.Write("No Attendance");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
				output.WriteLine();
				return;
			}

			#region Classes From January to December

			for(int month = 1; month <= 12; month++)
			{
				output.WriteFullBeginTag("tr");
			
				// Class Column
				output.WriteBeginTag("td");
				if(this.SubHeaderCssClass != string.Empty)
					output.WriteAttribute("class", this.SubHeaderCssClass);
				if(this.__dateCellWidth != Unit.Empty)
					output.WriteAttribute("width", this.__dateCellWidth.ToString());
				if(this.__dateCellHeight != Unit.Empty)
					output.WriteAttribute("height", this.__dateCellHeight.ToString());
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(DateTime.Parse(month.ToString() + "/1/2005", enFormat).ToString("MMMM"));
				output.WriteEndTag("td");

				// Days Columns
				for(int day = 1; day <= 31; day ++)
				{
					if(day > DateTime.DaysInMonth(year, month))
					{
						output.WriteBeginTag("td");
						if(this.SubHeaderCssClass != string.Empty)
							output.WriteAttribute("class", this.SubHeaderCssClass);
						if(this.__dateCellWidth != Unit.Empty)
							output.WriteAttribute("width", this.__dateCellWidth.ToString());
						if(this.__dateCellHeight != Unit.Empty)
							output.WriteAttribute("height", this.__dateCellHeight.ToString());
						output.Write(HtmlTextWriter.TagRightChar);
						output.Write("&nbsp;");
						output.WriteEndTag("td");
						continue;
					}

					output.WriteBeginTag("td");
					if(this.SubHeaderCssClass != string.Empty)
						output.WriteAttribute("class", this.SubHeaderCssClass);
					if(this.__dateCellWidth != Unit.Empty)
						output.WriteAttribute("width", this.__dateCellWidth.ToString());
					if(this.__dateCellHeight != Unit.Empty)
						output.WriteAttribute("height", this.__dateCellHeight.ToString());
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write(day.ToString("##00"));
					output.WriteEndTag("td");
				}

				output.WriteEndTag("tr");
				output.WriteLine();

				attendanceIndexMonth = attendanceIndex;
				classIndexMonth = classIndex;

				// Class Column and Class Rows
				for(int iClass = 0; iClass < startTimes.Length; iClass ++)
				{
					attendanceIndex = attendanceIndexMonth;
					classIndex = classIndexMonth;

					output.WriteFullBeginTag("tr");

					// Class Title
					output.WriteFullBeginTag("td");
					output.Write(startTimes[iClass].ToString("hh:mm tt"));
					output.WriteEndTag("td");

					for(int day = 1; day <= 31; day++)
					{
						if(day > DateTime.DaysInMonth(year, month))
						{
							output.WriteBeginTag("td");
							if(this.__blankCellCssClass != string.Empty)
								output.WriteAttribute("class", this.__blankCellCssClass);
							if(this.__dateCellWidth != Unit.Empty)
								output.WriteAttribute("width", this.__dateCellWidth.ToString());
							if(this.__dateCellHeight != Unit.Empty)
								output.WriteAttribute("height", this.__dateCellHeight.ToString());
							output.Write(HtmlTextWriter.TagRightChar);
							output.Write("&nbsp;");
							output.WriteEndTag("td");
							continue;
						}

						testDateLow = DateTime.Parse(month.ToString() + "/" + day.ToString() + "/" + year.ToString() +
							" " + startTimes[iClass].Hour.ToString("00") + ":" + startTimes[iClass].Minute.ToString("00"), enFormat);
						
						testDateHigh = testDateLow.Add(startSpansFor[iClass]);			// set high test date
						testDateLow = testDateLow.Subtract(startSpansBack[iClass]);		// set low test date						

						// Make sure attendance examined is equal to or more than the test date
						while(attendance[attendanceIndex].Class.ClassStart < testDateLow &&
							attendanceIndex + 1 < attendance.Count)
							attendanceIndex++;

						// Make sure classes examined is equal to or more than the test date
						while(classes[classIndex].ClassStart < testDateLow &&
							classIndex + 1 < classes.Count)
							classIndex++;

						output.WriteBeginTag("td");
						if(this.__fillBlanks &&	!dateCheck(classes[classIndex].ClassStart, testDateLow, testDateHigh) && this.__blankCellCssClass != string.Empty)
							output.WriteAttribute("class", __blankCellCssClass);
						else if(this.__fillBlanks && classes[classIndex].Instructor.ID == this.__memberID && this.__instructorCellCssClass != string.Empty)
							output.WriteAttribute("class", __instructorCellCssClass);
						else if(this.__fillBlanks && classes[classIndex].ParentSeminar != null && this.__seminarCellCssClass != string.Empty)
							output.WriteAttribute("class", __seminarCellCssClass);
						else if(this.__dateCellCssClass != string.Empty)
							output.WriteAttribute("class", this.__dateCellCssClass);
						if(this.__dateCellWidth != Unit.Empty)
							output.WriteAttribute("width", this.__dateCellWidth.ToString());
						if(this.__dateCellHeight != Unit.Empty)
							output.WriteAttribute("height", this.__dateCellHeight.ToString());
						output.Write(HtmlTextWriter.TagRightChar);

						// Now that we have the record closest to the month and date
						// check to see if it is on the month date and time of the start time,
						// if it is, display a tick mark, if not an empty
						if(dateCheck(attendance[attendanceIndex].Class.ClassStart, testDateLow, testDateHigh))
						{
							if(attendance[attendanceIndex].Class.Instructor.ID == this.__memberID)
								output.Write("I");
							else
								output.Write("X");
						}
						else
						{
							output.Write("&nbsp;");
						}

						output.WriteEndTag("td");
					}

					output.WriteEndTag("tr");
					output.WriteLine();
				}

				#endregion

			}

			#region Legend

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Legend");
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			if(this.__blankCellCssClass != string.Empty)
				output.WriteAttribute("class", __blankCellCssClass);
			output.WriteAttribute("colspan", "6");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("No Class");
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "6");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Unattended");
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "6");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("X - Attended");
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			if(this.__instructorCellCssClass != string.Empty)
				output.WriteAttribute("class", this.__instructorCellCssClass);
			output.WriteAttribute("colspan", "6");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("I - Instructed");
			output.WriteEndTag("td");

			output.WriteBeginTag("td");
			if(this.__seminarCellCssClass != string.Empty)
				output.WriteAttribute("class", this.__seminarCellCssClass);
			output.WriteAttribute("colspan", "7");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Seminar");
			output.WriteEndTag("td");

			output.WriteEndTag("tr");

			#endregion

		}

		private bool dateCheck(DateTime testDate, DateTime minDate, DateTime highDate)
		{
			return testDate >= minDate & testDate <= highDate;
		}
	}
}
