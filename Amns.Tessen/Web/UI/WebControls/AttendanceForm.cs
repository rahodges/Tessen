using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen;
using Amns.Tessen.Utilities;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for MemberListGrid.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:AttendanceForm runat=server></{0}:AttendanceForm>")]
	public class AttendanceForm : TableWindow, IPostBackEventHandler
	{
		private string connectionString;
		private DateTime localTime;
		private TimeSpan pageTime = TimeSpan.FromDays(7);
		private TimeSpan backTime = TimeSpan.Zero;
		private bool backPagingEnabled = false;
		private int memberID;

		private string defaultRowCssClass;
		private string alternateRowCssClass;

		private int selectedID;
		private string commandName;
		private bool substitutionEnabled = false;
		private DropDownList ddInstructors = new DropDownList();
		private Button btSwitch = new Button();
		private Button btSub = new Button();
		private Button btSubCancel = new Button();

		private LinkButton btBack = new LinkButton();
		private LinkButton btForward = new LinkButton();

		private LinkButton lbOk = new LinkButton();
		private Button btOk = new Button();
		private Button btCancel = new Button();

		private bool inverseListingEnabled;

		private string propertiesIconUrl = string.Empty;

		// Data
		private string[] classIdArray;
		private DojoClassCollection classes;
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
				return localTime;
			}
			set
			{
				localTime = value;
			}
		}

		[Bindable(true),
		Category("Behavior"),
		DefaultValue("")]
		public TimeSpan BackTime
		{
			get
			{
				return backTime;
			}
			set
			{
				backTime = value;
			}
		}

		[Bindable(true),
		Category("Behavior"),
		DefaultValue(false)]
		public bool BackPagingEnabled
		{
			get
			{
				return backPagingEnabled;
			}
			set
			{
				backPagingEnabled = value;
			}
		}
		
		[Bindable(true),
		Category("Behavior"),
		DefaultValue(0)]
		public int MemberID
		{
			get
			{
				return memberID;
			}
			set
			{
				memberID = value;
			}
		}
		
		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public string DefaultRowCssClass
		{
			get
			{
				return defaultRowCssClass;
			}
			set
			{
				defaultRowCssClass = value;
			}
		}

		
		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public string AlternateRowCssClass
		{
			get
			{
				return alternateRowCssClass;
			}
			set
			{
				alternateRowCssClass = value;
			}
		}

		private string indexRowCssClass = "";
		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public string IndexRowCssClass
		{
			get
			{
				return indexRowCssClass;
			}
			set
			{
				indexRowCssClass = value;
			}
		}

		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public string PropertiesIconUrl
		{
			get
			{
				return propertiesIconUrl;
			}
			set
			{
				propertiesIconUrl = value;
			}
		}

		private bool scrollerEnabled;
		public bool ScrollerEnabled
		{
			get
			{
				return scrollerEnabled;
			}
			set
			{
				scrollerEnabled = value;
			}
		}

		public bool InverseListingEnabled
		{
			get
			{
				return inverseListingEnabled;
			}
			set
			{
				inverseListingEnabled = value;
			}
		}

		[Bindable(true),
		Category("Behavior"),
		DefaultValue(false)]
		public bool SubstitutionEnabled
		{
			get
			{
				return substitutionEnabled;
			}
			set
			{
				substitutionEnabled = value;
			}
		}

		#endregion

		#region Child Control Methods and Event Handling

		protected override void CreateChildControls()
		{
			Controls.Clear();

			bindDropDownLists();

			Controls.Add(ddInstructors);

			btSwitch.Text = "Change Schedule";
			btSwitch.Width = Unit.Pixel(125);
			btSwitch.Click += new EventHandler(this.btSwitch_Click);
			Controls.Add(btSwitch);

			btSub.Text = "Substitute";
			btSub.Width = Unit.Pixel(72);
			btSub.Click += new EventHandler(this.btSub_Click);
			Controls.Add(btSub);

			btSubCancel.Text = "Cancel";
			btSubCancel.Width = Unit.Pixel(72);
			btSubCancel.Click += new EventHandler(this.btSubCancel_Click);
			Controls.Add(btSubCancel);

			btBack.Text = "Back";
			btBack.Click += new EventHandler(this.btBack_Click);
			Controls.Add(btBack);

			btForward.Text = "Forward";
			btForward.Click += new EventHandler(this.btForward_Click);
			Controls.Add(btForward);

			lbOk.Text = "Save";
			lbOk.Click += new EventHandler(this.btOk_Click);
			lbOk.CssClass = indexRowCssClass;
			Controls.Add(lbOk);

			btOk.Text = "OK";
			btOk.Width = Unit.Pixel(72);
			btOk.Click += new EventHandler(this.btOk_Click);
			Controls.Add(btOk);

			btCancel.Text = "Cancel";
			btCancel.Width = Unit.Pixel(72);
			btCancel.Click += new EventHandler(this.btCancel_Click);
			Controls.Add(btCancel);

			ChildControlsCreated = true;
		}

		private void btBack_Click(object sender, EventArgs e)
		{
			// Save Attendance
			saveAttendance();

			localTime = localTime.Subtract(pageTime);
		}

		private void btForward_Click(object sender, EventArgs e)
		{
			// Save Attendance
			saveAttendance();

			localTime = localTime.Add(pageTime);

			if(localTime > DateTime.Now)
			{
				localTime = DateTime.Now;
			}
		}

		private void btSwitch_Click(object sender, EventArgs e)
		{
			DojoClass selectedClass = new DojoClass(selectedID);
			DojoClassDefinition parentDefinition = selectedClass.ParentDefinition;

			// BUGFIX... DO NOT REMOVE!
			// FOR SOME REASON THE INSTRUCTOR IS NOT CHANGED IF THIS HAS NOT BEEN LOADED. 
			// WTF!
			parentDefinition.EnsurePreLoad();	

			selectedClass.Instructor = DojoMember.NewPlaceHolder(int.Parse(ddInstructors.SelectedItem.Value));
			parentDefinition.Instructor = selectedClass.Instructor;

			selectedClass.Save();
			parentDefinition.Save();

			selectedID = 0;
		}

		private void btSub_Click(object sender, EventArgs e)
		{
			DojoClass selectedClass = new DojoClass(selectedID);
			selectedClass.Instructor = DojoMember.NewPlaceHolder(int.Parse(ddInstructors.SelectedItem.Value));
			selectedClass.Save();
			selectedID = 0;
		}

		private void btSubCancel_Click(object sender, EventArgs e)
		{
			selectedID = 0;
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			this.OnCancelled(e);
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			saveAttendance();
			OnUpdate(EventArgs.Empty);
		}

		private void saveAttendance()
		{
			DojoMember member;
			string whereQuery;
			DojoAttendanceEntryManager aManager;
			DojoAttendanceEntryCollection attendance;
			int[] selectedClasses;
			string[] ids;
			bool removeEntry;
			bool formChanged;
			AttendanceScanner aScanner;

			// Load Member and set LastSignIn to present time
			member = new DojoMember(memberID);
			member.LastSignin = DateTime.Now;
			
			// Get classes on the attendance form.
			classIdArray = Context.Request.Form["___" + ClientID + "Classes"].Split(',');

			// Build query to get member's attendance for the classes on
			// the attendance form and get the memberTypeTemplates.
			whereQuery = "MemberID=" + member.iD;
			if(classIdArray.Length > 0)
			{
				whereQuery += " AND (";
				for(int x = 0; x < classIdArray.Length; x++)
					if(x == 0)
						whereQuery += "ClassID=" + classIdArray[x] + " ";
					else
						whereQuery += "OR ClassID=" + classIdArray[x] + " ";
				whereQuery += ") ";
			}
			aManager = new DojoAttendanceEntryManager();
			attendance = aManager.GetCollection(whereQuery, string.Empty, null);

			// Load selected classes from form.
			if(Page.Request.Form[this.ClientID + "classoptions"] != null)
			{	
				ids = Page.Request.Form[this.ClientID + "classoptions"].Split(',');
				selectedClasses = new int[ids.Length];
				for(int x = 0; x < ids.Length; x++)
					selectedClasses[x] = int.Parse(ids[x]);
			}
			else
			{
				// What the hell is this?
				selectedClasses = new int[0];
			}
			
			// Assume that the form has not been changed.
			formChanged = false;

			// Save newly checked classes.
			for(int x = 0; x < selectedClasses.Length; x++)
			{			
				// Ignore classes already checked!
				foreach(DojoAttendanceEntry aEntry in attendance)
					if(selectedClasses[x] == aEntry.Class.iD)
						goto NEXT_ENTRY;

				DojoAttendanceEntry entry = new DojoAttendanceEntry();
				entry.Class = DojoClass.NewPlaceHolder(selectedClasses[x]);
				entry.Member = member;
				entry.Rank = member.rank;
				entry.SigninTime = localTime;
				attendance.Add(entry);

				entry.Save();

				formChanged = true;
			
			NEXT_ENTRY:
				continue;
			}

			// Delete unchecked classes.
			foreach(DojoAttendanceEntry aEntry in attendance)
			{
				removeEntry = true;
				for(int x = 0; x < selectedClasses.Length; x++)
					if(aEntry.Class.iD == selectedClasses[x])
						removeEntry = false;

				if(removeEntry)
				{
					aEntry.Delete();
					formChanged = true;
				}
			}

			// Run attendance scan if form has changed
			if(formChanged)
			{
				aScanner = new AttendanceScanner();
				aScanner.RunMemberAttendanceScan(member, TimeSpan.FromHours(1));
				member.Save();
			}
		}

		#endregion

		#region Events

		public event EventHandler Update;
		protected virtual void OnUpdate(EventArgs e)
		{
			if(Update != null)
				Update(this, e);
		}

		public event EventHandler Cancelled;
		protected virtual void OnCancelled(EventArgs e)
		{
			if(Cancelled != null)
				Cancelled(this, e);
		}

		#endregion


		// Method of IPostBackEventHandler that raises change events.
		public override void ProcessPostBackEvent(string eventArgument)
		{	
			commandName = eventArgument.Substring(0, eventArgument.IndexOf("_"));
			selectedID = int.Parse(eventArgument.Substring(eventArgument.IndexOf("_") + 1));
			
			// Save Attendance
			saveAttendance();
		}

		private void bindDropDownLists()
		{
			DojoMemberManager m = new DojoMemberManager();
			DojoMemberCollection instructors = m.GetCollection("IsInstructor=true", 
				"LastName, FirstName, MiddleName", DojoMemberFlags.PrivateContact);
            			
			foreach(DojoMember instructor in instructors)
				ddInstructors.Items.Add(new ListItem(instructor.PrivateContact.ConstructName("LS,FMi."),
					instructor.ID.ToString()));
		}

		private void setInstructor(int instructorID)
		{
			ListItemCollection items = ddInstructors.Items;
			
			for(int x = 0; x < items.Count; x++)
				items[x].Selected = items[x].Value == instructorID.ToString();
		}

		protected override void OnInit(EventArgs e)
		{
			columnCount = 3;

			if(scrollerEnabled)
				features = TableWindowFeatures.Scroller;
			else
				features = TableWindowFeatures.DisableContentSeparation;

			components = TableWindowComponents.Toolbar | TableWindowComponents.Footer;			
		}

		#region Rendering

		protected override void OnPreRender(EventArgs e)
		{
			DojoClassManager cManager;
			DojoAttendanceEntryManager aManager;
			string classQuery;
			string attendanceQuery;

			EnsureChildControls();

			if(this.backTime == TimeSpan.Zero)
			{
				classQuery = 
					"SigninEnd>=#" + 
					localTime.Subtract(backTime).ToString() + "# AND " +
					"SigninStart<=#" + 
					localTime.ToString() + "#";
			}
			else
			{
				classQuery = 
					"ClassEnd>=#" + 
					localTime.Subtract(backTime).Date.ToString() + "# AND " +
					"ClassStart<=#" + 
					localTime.AddDays(1).Date.ToString() + "#";
			}

			attendanceQuery = 
				"MemberID=" + memberID.ToString() + " AND " +
				classQuery;

			cManager = new DojoClassManager();
			aManager = new DojoAttendanceEntryManager();;
			
			// Be sure to adjust the backtime if you want earlier signin times.
			classes = cManager.GetCollection(classQuery, "ClassStart", 
					DojoClassFlags.Instructor,
					DojoClassFlags.InstructorRank,
					DojoClassFlags.InstructorPrivateContact);

			classIdArray = new string[classes.Count];

			for(int x = 0; x < classes.Count; x++)
			{
				classIdArray[x] = classes[x].iD.ToString();
			}

			Page.ClientScript.RegisterHiddenField("___" + ClientID + "Classes", 
				string.Join(",", classIdArray));			

			attendance = 
				aManager.GetCollection(attendanceQuery, 
				string.Empty, 
				DojoAttendanceEntryFlags.Class);
		}

		protected override void RenderToolbar(HtmlTextWriter output)
		{			
			output.WriteFullBeginTag("tr");
			output.WriteLine();
			output.Indent++;
			
			output.WriteBeginTag("td");			
			output.WriteAttribute("class", this.SubHeaderCssClass);
			output.WriteAttribute("height", "28px");
			output.WriteAttribute("colspan", "3");
			output.Write(HtmlTextWriter.TagRightChar);
			output.WriteLine();
			output.Indent++;
			
			// Render Local Time in a Right Floating DIV
			output.WriteBeginTag("div");
			output.WriteAttribute("style", "float:right;");
			output.Write(HtmlTextWriter.TagRightChar);			
			output.Write(localTime.ToLongDateString());
			output.WriteEndTag("div");
			
			// Render Back and Forward Controls
			btBack.RenderControl(output);
			output.Write(" | ");
			btForward.RenderControl(output);
			
			output.Indent--;
			output.WriteEndTag("td");
			output.WriteLine();

			output.Indent--;
			output.WriteEndTag("tr");
			output.WriteLine();
		}

		protected override void RenderFooter(HtmlTextWriter output)
		{
			// DO NOT RENDER BUTTONS IF INSTRUCTORS ARE BEING CHANGED
			if(selectedID == 0)
			{			
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("align", "right");
				output.WriteAttribute("class", this.SubHeaderCssClass);
				output.WriteAttribute("height", "28px");
				output.WriteAttribute("colspan", this.columnCount.ToString());
				output.Write(HtmlTextWriter.TagRightChar);
			
			
				if(classes.Count > 0)
				{
					btOk.RenderControl(output);
					output.Write(" ");
				}
				btCancel.RenderControl(output);
			

				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}
		}
        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();

			if(inverseListingEnabled)
				renderInverseListing(output);
			else
				renderDefaultListing(output);
		}

		/// <summary>
		/// Renders the classes by days, index headers are listed descending.
		/// </summary>
		/// <param name="output"></param>
		protected void renderInverseListing(HtmlTextWriter output)
		{            
			bool rowflag = false;
			string rowCssClass;		

			if(classes.Count == 0)
			{
				output.WriteFullBeginTag("tr");
				output.WriteLine();
				output.Indent++;
				output.WriteBeginTag("td");
				output.WriteAttribute("height", "28px");
				output.WriteAttribute("colspan", this.columnCount.ToString());
				output.Write(HtmlTextWriter.TagRightChar);
				output.WriteLine();
				output.Indent++;
				output.Write("No classes available for signin.");
				output.Indent--;
				output.WriteEndTag("td");
				output.WriteLine();
				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
				return;
			}

			//
			// Render Records
			//
			bool haltRender = false;
			int endClassIndex = classes.Count - 1;
			int startClassIndex = 0;
			DateTime indexDate = DateTime.MaxValue;

			// find the first class to start at.
			for(int x = endClassIndex; x >= 0; x--)
				if(classes[x].ClassStart.Date != indexDate.Date)
				{
					indexDate = classes[x].ClassStart.Date;
					endClassIndex = x;
					break;
				}

			while(!haltRender)
			{
				for(int x = 0; x <= endClassIndex; x++)
					if(classes[x].ClassStart.Date == indexDate)
					{
						startClassIndex = x;
						break;
					}

				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", indexRowCssClass);
				output.WriteAttribute("colspan", "2");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(indexDate.ToString("dddd, MMMM dd"));
				output.WriteEndTag("td");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", indexRowCssClass);
				output.WriteAttribute("align", "right");
				output.Write(HtmlTextWriter.TagRightChar);

				// DO NOT RENDER SAVE LINKS WHILE INSTRUCTORS ARE BEING CHANGED
				if(selectedID == 0)
					lbOk.RenderControl(output);
				
				output.WriteEndTag("td");
				output.WriteEndTag("tr");

				for(int x = startClassIndex; x <= endClassIndex; x++)
				{				
					if(rowflag)
						rowCssClass = this.defaultRowCssClass;
					else
						rowCssClass = this.alternateRowCssClass;

					rowflag = !rowflag;
					
					output.WriteFullBeginTag("tr");
					output.WriteLine();
					output.Indent++;
	
					//
					// Render Checkbox for Class
					//
					output.WriteBeginTag("td");
					output.WriteAttribute("class", rowCssClass);
					output.WriteAttribute("valign", "top");
					output.Write(HtmlTextWriter.TagRightChar);
					output.WriteBeginTag("input");
					output.WriteAttribute("type", "checkbox");
					output.WriteAttribute("name", this.ClientID + "classoptions");
					output.WriteAttribute("value", classes[x].ID.ToString());
					// DISABLE CHECKS IF EDITING INSTRUCTOR
					if(selectedID != 0)
						output.WriteAttribute("disabled", "true");

					// TODO: Remove used attendance entries from collection during loop to speed up
					//       Searches
					for(int y = 0; y < attendance.Count; y++)
						if(classes[x].ID == attendance[y].Class.ID)
							output.WriteAttribute("checked", "true");

					output.Write(HtmlTextWriter.TagRightChar);
					output.WriteEndTag("td");
					output.WriteLine();

					//
					// Render Class Name
					//				
					output.WriteBeginTag("td");
					output.WriteAttribute("valign", "top");
					output.WriteAttribute("width", "100%");
					output.WriteAttribute("class", rowCssClass);
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write(classes[x].ClassStart.ToShortTimeString());
					output.Write("&nbsp;");
					output.Write(classes[x].Name);
					output.Write("<br>");
					if(selectedID == classes[x].ID)
					{
						setInstructor(classes[x].Instructor.ID);
						ddInstructors.RenderControl(output);
						output.Write(" ");
						btSub.RenderControl(output);
						output.Write(" ");
						if(classes[x].ParentDefinition != null)
						{
							btSwitch.RenderControl(output);
							output.Write(" ");
						}						
						btSubCancel.RenderControl(output);
					}
					else
					{
						output.Write(classes[x].Instructor.PrivateContact.FullName);
						output.Write(", ");
						output.Write(classes[x].Instructor.Rank.Name);
					}
					output.WriteEndTag("td");
					output.WriteLine();

					//
					// Render Class Times
					//
					output.WriteBeginTag("td");
					output.WriteAttribute("class", rowCssClass);
					output.WriteAttribute("valign", "top");
					output.WriteAttribute("align", "right");
					output.WriteAttribute("nowrap", "true");
					output.Write(HtmlTextWriter.TagRightChar);		

//					if(substitutionEnabled & localTime.Date == classes[x].ClassStart.Date & selectedID == 0)
					if(substitutionEnabled & selectedID == 0)
					{
						output.WriteBeginTag("a");
						output.WriteAttribute("href", "javascript:" + Page.ClientScript.GetPostBackEventReference(this, "sub_" + classes[x].ID.ToString()));
						output.Write(HtmlTextWriter.TagRightChar);
						if(propertiesIconUrl != string.Empty)
							output.Write("<img src=\"" + Page.ResolveUrl(propertiesIconUrl) + "\" alt=\"Properties\" border=\"0\">");
						else
							output.Write("change");
						output.WriteEndTag("a");
					}

					output.WriteEndTag("td");
					output.WriteLine();
				
					output.Indent--;
					output.WriteEndTag("tr");
					output.WriteLine();					
				}

				endClassIndex = startClassIndex - 1;
				if(endClassIndex < 0)
					haltRender = true;
				else
					indexDate = classes[endClassIndex].ClassStart.Date;
			}
		}
        
		/// <summary>
		/// Renders the classes by days, index headers are listed ascending.
		/// </summary>
		/// <param name="output"></param>
		protected void renderDefaultListing(HtmlTextWriter output)
		{           
			bool rowflag = false;
			string rowCssClass;		

			// 
			// An indexer to seperate class days with a horizontal rule or some other divider.
			//
			DateTime indexDate = DateTime.MinValue;
//			if(classes.Count > 0)
//				indexDate = classes[0].ClassStart.Date;

			if(classes.Count == 0)
			{
				output.WriteFullBeginTag("tr");
				output.WriteLine();
				output.Indent++;
				output.WriteBeginTag("td");
				output.WriteAttribute("height", "28px");
				output.WriteAttribute("colspan", this.columnCount.ToString());
				output.Write(HtmlTextWriter.TagRightChar);
				output.WriteLine();
				output.Indent++;
				output.Write("No classes available for signin.");
				output.Indent--;
				output.WriteEndTag("td");
				output.WriteLine();
				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
				return;
			}

			//
			// Render Records
			//
			foreach(DojoClass classEntry in classes)
			{				
				if(rowflag)
					rowCssClass = this.defaultRowCssClass;
				else
					rowCssClass = this.alternateRowCssClass;

				rowflag = !rowflag;

				if(indexDate != classEntry.ClassStart.Date | indexDate == DateTime.MinValue)
				{					
					indexDate = classEntry.ClassStart.Date;

					output.WriteFullBeginTag("tr");
					output.WriteBeginTag("td");
					output.WriteAttribute("class", indexRowCssClass);
					output.WriteAttribute("colspan", "3");
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write(indexDate.ToLongDateString());
					output.WriteEndTag("td");
					output.WriteEndTag("tr");
				}

				output.WriteFullBeginTag("tr");
				output.WriteLine();
				output.Indent++;
	
				//
				// Render Checkbox for Class
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.WriteBeginTag("input");
				output.WriteAttribute("type", "checkbox");
				output.WriteAttribute("name", this.ClientID + "classoptions");
				output.WriteAttribute("value", classEntry.ID.ToString());
				
				// TODO: Remove used attendance entries from collection during loop to speed up
				//       Searches
				for(int x = 0; x < attendance.Count; x++)
					if(classEntry.ID == attendance[x].Class.ID)
						output.WriteAttribute("checked", "true");

				output.Write(HtmlTextWriter.TagRightChar);
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Class Name
				//				
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(classEntry.Name);
				output.Write("<br>");
				output.Write(classEntry.Instructor.PrivateContact.FullName);
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Class Times
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("align", "right");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(classEntry.ClassStart.ToShortTimeString());
				output.WriteEndTag("td");
				output.WriteLine();
				
				output.Indent--;
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

		#region Viewstate Methods

		protected override void LoadViewState(object savedState) 
		{
			EnsureChildControls();
			// Customize state management to handle saving state of contained objects.

			if (savedState != null) 
			{
				object[] myState = (object[])savedState;

				if(myState[0] != null)
					base.LoadViewState(myState[0]);
				if(myState[1] != null)
					memberID = (int) myState[1];
				if(myState[2] != null)
					selectedID = (int) myState[2];
				if(myState[3] != null)
					substitutionEnabled = (bool) myState[3];
				if(myState[4] != null)
					Text = (string) myState[4];
				if(myState[5] != null)
					LocalTime = (DateTime) myState[5];
			}
		}

		protected override object SaveViewState() 
		{
			// Customized state management to handle saving state of contained objects  such as styles.

			object baseState = base.SaveViewState();
			
			object[] myState = new object[6];
			myState[0] = baseState;
			myState[1] = memberID;
			myState[2] = selectedID;
			myState[3] = substitutionEnabled;
			myState[4] = Text;
			myState[5] = localTime;

			return myState;
		}

		#endregion
	}
}