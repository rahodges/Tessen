using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using Amns.GreyFox.People;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// The member editor is a dynamic editor which displays information based on the
	/// role setting provided. It will edit all properties of a member, and in addition
	/// insert, delete, update their Amns.GreyFox accounts information.
	/// 
	/// This editor can also trigger promotion scans if configured to do so.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:DojoMemberDeleteDialog runat=server></{0}:DojoMemberDeleteDialog>")]
	public class DojoMemberDeleteDialog : TableWindow, INamingContainer
	{
		private int memberID;
		private DojoMember editMember;
		private string connectionString;
		
		private DropDownList ddMembers = new DropDownList();

		private Button btOk = new Button();
		private Button btNext = new Button();
		private Button btCancel = new Button();

		private bool classError;
		private bool classDefinitionError;
		private bool attendanceError;
		private bool bulkAttendanceError;

		#region Public Properties
		
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
				ddMembers.Visible = value == 0;
				memberID = value;
			}
		}

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

		// Organization activity cannot be set here, it is set through organization interractions,
		// however it is displayed with a javascript selector. A drop down list has the organizations
		// available for the student to be part of, and the student's status in the organization.
		// On promotion scans, organization status codes for students can be required.

		protected override void CreateChildControls()
		{
			Controls.Clear();
			bindDropDownLists();

			ddMembers.EnableViewState = false;			
			Controls.Add(ddMembers);

			btNext.Text = "Next";
			btNext.EnableViewState = false;
			btNext.Click += new EventHandler(btNext_Click);
			Controls.Add(btNext);

			btOk.Text = "OK";
			btOk.EnableViewState = false;
			btOk.Click += new EventHandler(btOk_Click);
			Controls.Add(btOk);

			btCancel.Text = "Cancel";
			btCancel.EnableViewState = false;
			btCancel.Click += new EventHandler(btCancel_Click);
			Controls.Add(btCancel);

			ChildControlsCreated = true;
		}

		private void bindDropDownLists()
		{

			DojoMemberCollection members = 
				new DojoMemberManager().GetCollection(string.Empty, 
				"LastName, FirstName, MiddleName", 
				DojoMemberFlags.PrivateContact);

			foreach(DojoMember member in members)
			{
				ListItem i = new ListItem(member.PrivateContact.ConstructName("L, FM."), member.iD.ToString());
				if(editMember != null)
					i.Selected = member.iD == editMember.ID;
				ddMembers.Items.Add(i);
			}
		}

		protected void btNext_Click(object sender, EventArgs e)
		{
			memberID = int.Parse(ddMembers.SelectedItem.Value);
		}
		
		protected void btOk_Click(object sender, EventArgs e)
		{
			OleDbConnection dbConnection = new OleDbConnection(connectionString);
			OleDbCommand dbCommand = new OleDbCommand();
			dbCommand.Connection = dbConnection;
			dbConnection.Open();

			//
			// Check relations for deletion errors.
			//
			dbCommand.CommandText = "SELECT COUNT(*) FROM kitTessen_Classes WHERE InstructorID=" +
				memberID.ToString() + ";";
			if((int) dbCommand.ExecuteScalar() > 0)
			{
				classError = true;
				dbConnection.Close();
				return;
			}

			dbCommand.CommandText = "SELECT COUNT(*) FROM kitTessen_ClassDefinitions WHERE InstructorID=" +
				memberID.ToString() + ";";
			if((int) dbCommand.ExecuteScalar() > 0)
			{
				classError = true;
				dbConnection.Close();
				return;
			}

			//
			// Delete Attendance
			//
			dbCommand.CommandText = "DELETE * FROM kitTessen_Attendance WHERE MemberID=" +
				memberID + ";";
			dbCommand.ExecuteNonQuery();

			dbCommand.CommandText = "DELETE * FROM kitTessen_BulkAttendance WHERE MemberID=" +
				memberID + ";";
			dbCommand.ExecuteNonQuery();

			//
			// Delete Promotions
			//
			dbCommand.CommandText = "DELETE * FROM kitTessen_Promotions WHERE MemberID=" +
				memberID + ";";
			dbCommand.ExecuteNonQuery();

			//
			// Delete Member
			//
			dbCommand.CommandText = "DELETE * FROM kitTessen_Members WHERE DojoMemberID=" +
				memberID + ";";
			dbCommand.ExecuteNonQuery();

			dbConnection.Close();

			memberID = 0;

			OnDeleted(EventArgs.Empty);
		}

		protected void btCancel_Click(object sender, EventArgs e)
		{
			this.OnCancelled(EventArgs.Empty);
		}

		#region Public Control Events

		public event EventHandler Cancelled;
		protected virtual void OnCancelled(EventArgs e)
		{
			if(Cancelled != null)
				Cancelled(this, e);
		}

		public event EventHandler Deleted;
		protected virtual void OnDeleted(EventArgs e)
		{
			if(Deleted != null)
				Deleted(this, e);
		}

		#endregion

		protected override void OnInit(EventArgs e)
		{
			columnCount = 2;
			features = TableWindowFeatures.DisableContentSeparation;
		}

		protected override void OnPreRender(EventArgs e)
		{
			if(memberID != 0)
			{
				editMember = new DojoMember(memberID);
				Text = "Delete - " + editMember.PrivateContact.FullName;
			}
			else
			{
				Text = "Delete Member";
			}

			//
			// Detect class and class definition relations and issue error.
			//
			DojoClassManager cm = new DojoClassManager();
			DojoClassDefinitionManager cdm = new DojoClassDefinitionManager();
			classError = cm.ClassCountByInstructor(memberID) > 0;
			classDefinitionError = cdm.GetClassDefinitionCountByInstructor(memberID) > 0;
			btOk.Enabled = !classError & !classDefinitionError;

			//
			// Detect attendance and issue notice.
			//
			DojoAttendanceEntryManager am = new
				DojoAttendanceEntryManager();
			attendanceError = am.ClassCountByMember(memberID) > 0;
			DojoBulkAttendanceEntryManager bam = new
				DojoBulkAttendanceEntryManager();
            bulkAttendanceError = bam.ClassCountByMember(memberID) > 0;
		}

		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>Warning:</strong> This action deletes the selected member and all " +
				"related data such as attendance, promotions, and dues payments. This action should only be " +
				"used to remove duplicate records from the database, use the archive utility to archive old " +
				"records. <em>Use with caution, this lineOption cannot be undone.</em>");
			
			if(classError | classDefinitionError)
			{
				output.Write("<payment style=\"color:red;\"><strong>There are ");
				
				if(classError & classDefinitionError)
					output.Write("classes and class definitions ");
				else if(classError)
					output.Write("classes ");
				else
					output.Write("class definitions ");

				output.Write("which require this member to exist. Change the instructor of these items to allow " +
					"deletion.</payment>");
			}
			
			if(attendanceError | bulkAttendanceError)
				output.Write("<payment style=\"color:red;\"><strong>The selected member has attendance. " +
					"Deleting this member will also delete their attendance.</payment>");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");			
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Member to delete: ");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("width", "100%");
			output.Write(HtmlTextWriter.TagRightChar);
			if(memberID != 0)
			{
				output.Write(editMember.PrivateContact.FullName);
				output.Write(" (");
				output.Write(editMember.iD);
				output.Write(")");
			}
			else
			{
				ddMembers.RenderControl(output);				
			}
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			if(this.memberID != 0)
				btOk.RenderControl(output);
			else
                btNext.RenderControl(output);
			output.Write("&nbsp;");
			btCancel.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");			
		}

		#region ViewState Methods

		protected override void LoadViewState(object savedState) 
		{
			if(savedState != null)
			{
				// Load State from the array of objects that was saved at ;
				// SavedViewState.
				object[] myState = (object[])savedState;
				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					memberID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{			
			object baseState = base.SaveViewState();
			object[] myState = new object[5];
			myState[0] = baseState;
			myState[1] = memberID;
			return myState;
		}

		#endregion

	}
}
