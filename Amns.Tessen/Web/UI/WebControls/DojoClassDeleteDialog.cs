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
	public class DojoClassDeleteDialog : TableWindow, INamingContainer
	{
		private int classID;
		private DojoClass editClass;
		
		private Button btOk = new Button();
		private Button btCancel = new Button();

		private bool attendanceWarning;

		#region Public Properties
		
		[Bindable(true),
		Category("Behavior"),
		DefaultValue(0)]
		public int ClassID
		{
			get
			{
				return classID;
			}
			set
			{
				classID = value;
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
		protected void btOk_Click(object sender, EventArgs e)
		{
			string connectionString = Amns.GreyFox.Data.ManagerCore.GetInstance().ConnectionString;			
			
			OleDbConnection dbConnection = new OleDbConnection(connectionString);
			OleDbCommand dbCommand = new OleDbCommand();
			dbCommand.Connection = dbConnection;
			dbConnection.Open();

			//
			// Delete Attendance
			//
			dbCommand.CommandText = "DELETE * FROM kitTessen_Attendance WHERE ClassID=" +
				classID + ";";
			dbCommand.ExecuteNonQuery();

			dbCommand.CommandText = "DELETE * FROM kitTessen_Classes WHERE DojoClassID=" +
				classID + ";";
			dbCommand.ExecuteNonQuery();

			dbConnection.Close();

			classID = 0;

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
			if(classID != 0)
			{
				editClass = new DojoClass(classID);
				Text = "Delete - " + editClass.ToString();
			}
			else
			{
				Text = "Delete Class";
			}

			//
			// Detect class and class definition relations and issue error.
			//
			string connectionString = Amns.GreyFox.Data.ManagerCore.GetInstance().ConnectionString;	
			OleDbConnection dbConnection = new OleDbConnection(connectionString);
			OleDbCommand dbCommand = new OleDbCommand();
			dbCommand.Connection = dbConnection;
			dbConnection.Open();

			//
			// Check relations for deletion errors.
			//
			dbCommand.CommandText = "SELECT COUNT(*) FROM kitTessen_Attendance WHERE ClassID=" +
				classID.ToString() + ";";
			if((int) dbCommand.ExecuteScalar() > 0)
				attendanceWarning = true;
			dbConnection.Close();

//			btOk.Enabled = !attendanceWarning;
		
		}

		protected override void RenderContent(HtmlTextWriter output)
		{
			if(classID == 0)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				output.WriteAttribute("class", "row1");
				output.WriteAttribute("colspan", "2");
				output.Write(HtmlTextWriter.TagRightChar);

				output.Write("The class selected does not exist.");

				output.WriteEndTag("td");
				output.WriteEndTag("tr");
				return;
			}

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);

			output.Write("<strong>Warning:</strong> This action deletes the selected class and all " +
				"attendance related to the class. This action should only be " +
				"used to remove duplicate or erroneous records from the database, use the archive utility to archive old " +
				"classes. <em>Use with caution, this lineOption cannot be undone.</em>");
			
			if(attendanceWarning)
			{
				output.Write("<payment style=\"color:red;\"><strong>There is existing attendance related to this class; " +
					" do you wish to delete the class and related attendance?</strong></payment>");
			}

			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");			
			output.WriteAttribute("nowrap", "true");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class to delete: ");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("width", "100%");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(editClass.Name);
			output.WriteFullBeginTag("br");
			output.Write(editClass.ClassStart.ToLongDateString());
			output.Write(" ");
			output.Write(editClass.ClassStart.ToLongTimeString());
			output.Write(" - ");
			output.Write(editClass.ClassEnd.ToShortTimeString());
			output.Write(" (");
			output.Write(editClass.iD);
			output.Write(")");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			btOk.RenderControl(output);
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
					classID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{			
			object baseState = base.SaveViewState();
			object[] myState = new object[5];
			myState[0] = baseState;
			myState[1] = classID;
			return myState;
		}

		#endregion

	}
}
