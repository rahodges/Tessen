using System;
using System.Data;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for MemberListMailForm.
	/// </summary>
	[DefaultProperty("SmtpHost"), 
	ToolboxData("<{0}:MemberListMailForm runat=server></{0}:MemberListMailForm>")]
	public class MemberListMailForm : TableWindow
	{

		#region Properties

		private string connectionString;
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

		private bool debugFlag;
		[Bindable(true), Category("Behavior"), DefaultValue(false)]
		public bool Debug
		{
			get
			{
				return debugFlag;
			}
			set
			{
				debugFlag = value;
			}
		}

		private string smtpHost = "127.0.0.1";
		[Bindable(true), Category("Smtp Server"), DefaultValue("127.0.0.1")]
		public string SmtpHost
		{
			get
			{
				return smtpHost;
			}
			set
			{
				smtpHost = value;
			}
		}

		private string smtpPassword;
		[Bindable(true), Category("Smtp Server"), DefaultValue("")]
		public string SmtpPassword
		{
			get
			{
				return smtpPassword;
			}
			set
			{
				smtpPassword = value;
			}
		}

		private string smtpUsername;
		[Bindable(true), Category("Smtp Server"), DefaultValue("")]
		public string SmtpUsername
		{
			get
			{
				return smtpUsername;
			}
			set
			{
				smtpUsername = value;
			}
		}

		private int smtpPort = 25;
		[Bindable(true), Category("Smtp Server"), DefaultValue(25)]
		public int SmtpPort
		{
			get
			{
				return smtpPort;
			}
			set
			{
				smtpPort = value;
			}
		}

		private string mailFrom; // Should be in the form me@localhost.com <Information>
		[Bindable(true), Category("Email"), DefaultValue("")]
		public string MailFrom
		{
			get
			{
				return mailFrom;
			}
			set
			{
				mailFrom = value;
			}
		}

		private string mailSubject; // Should be in the form www@test.com <Ellie Kileen>; test@test.net <Marvin Bowser>
		[Bindable(true), Category("Email"), DefaultValue("")]
		public string MailSubject
		{
			get
			{
				return mailSubject;
			}
			set
			{
				mailSubject = value;
			}
		}

		private string mailBody; // Should be in the form www@test.com <Ellie Kileen>; test@test.net <Marvin Bowser>
		[Bindable(true), Category("Email"), DefaultValue("")]
		public string MailBody
		{
			get
			{
				return mailBody;
			}
			set
			{
				mailBody = value;
			}
		}

		private string listType;
		[Bindable(true), Category("Behavior"), DefaultValue("All")]
		public string ListType
		{
			get
			{
				return listType;
			}
			set
			{
				listType = value;
			}
		}

		#endregion

		#region CreateChildControls

		protected override void CreateChildControls()
		{
			Button okButton = new Button();
			okButton.ID = "okButton";
			okButton.Text = "OK";
			okButton.Width = Unit.Pixel(72);
			okButton.Click += new EventHandler(this.okButton_Clicked);
			Controls.Add(okButton);
			
			Button cancelButton = new Button();
			cancelButton.ID = "cancelButton";
			cancelButton.Text = "Cancel";
			cancelButton.Width = Unit.Pixel(72);
			cancelButton.Click += new EventHandler(this.cancelButton_Clicked);
			Controls.Add(cancelButton);

			TextBox fromTextBox = new TextBox();
			fromTextBox.ID = this.ID+"from";
			fromTextBox.Width = Unit.Percentage(100);
			Controls.Add(fromTextBox);

			TextBox subjectTextBox = new TextBox();
			subjectTextBox.ID = this.ID+"subject";
			subjectTextBox.Width = Unit.Percentage(100);
			Controls.Add(subjectTextBox);

			TextBox bodyTextBox = new TextBox();
			bodyTextBox.ID = this.ID+"body";
			bodyTextBox.TextMode = TextBoxMode.MultiLine;
			bodyTextBox.Rows = 25;
			bodyTextBox.Width = Unit.Percentage(100);
			Controls.Add(bodyTextBox);

			DropDownList listDropDown = new DropDownList();
			listDropDown.ID = this.ID+"listDropDown";
			listDropDown.Items.Add("Active");
			listDropDown.Items.Add("All");
			listDropDown.Items.Add("Instructors");
			listDropDown.Items.Add("Yudansha");
			listDropDown.Items.Add("Mudansha");
			listDropDown.Items.Add("Promotable");
			listDropDown.Width = Unit.Percentage(100);
			listDropDown.AutoPostBack = true;
			listDropDown.SelectedIndexChanged += new EventHandler(this.listDropDown_SelectedIndexChanged);
			Controls.Add(listDropDown);
            
			ChildControlsCreated = true;
		}

		#endregion

		#region Events

		private void cancelButton_Clicked(object sender, EventArgs e)
		{
			OnCancelButtonClicked(System.EventArgs.Empty);
		}

		private void okButton_Clicked(object sender, EventArgs e)
		{
			SendMail();

			if(sentFlag)
			{
				Button doneButton = new Button();
				doneButton.Text = "OK";
				doneButton.Width = Unit.Pixel(72);
				doneButton.ID = "DoneButton";
				doneButton.Click += new EventHandler(this.doneButton_Clicked);
				Controls.Add(doneButton);
			}
		}

		private void doneButton_Clicked(object sender, EventArgs e)
		{
            OnDoneClick(EventArgs.Empty);
		}
		public event EventHandler DoneClick;
		protected virtual void OnDoneClick(EventArgs e)
		{
			if(DoneClick != null)
				DoneClick(this, e);
		}

		private void listDropDown_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList listDropDown = (DropDownList) sender;
			listType = listDropDown.SelectedItem.Value;
			
			OnListIndexChanged(EventArgs.Empty);
		}

		public event EventHandler ListIndexChanged;
		protected virtual void OnListIndexChanged(EventArgs e) 
		{
			if(ListIndexChanged != null)
				ListIndexChanged(this, e);
		}

		#endregion

		private bool sentFlag;
		private bool errorFlag;
		private string errorMessage;

		#region SendMail Method

		protected void SendMail()
		{
			EnsureChildControls();
			// LOAD DATA

			if(mailFrom == "" || mailFrom == string.Empty)
			{
				this.errorFlag = true;
				this.errorMessage = "From address required.<br>";
			}

			if(mailBody == "" || mailBody == string.Empty)
			{
				this.errorFlag = true;
				this.errorMessage += "Cannot send an empty email.<br>";
			}

			if(errorFlag)
				return;
			
			DojoMemberManager mManager = new DojoMemberManager();
			DojoMemberCollection members;
			switch(listType)
			{
				case "Active":
					members = mManager.GetCollection("IsPrimaryOrgActive=true", "LastName", DojoMemberFlags.PrivateContact);
					break;
				case "Instructors":
					members = mManager.GetCollection("IsInstructor=true AND IsPrimaryOrgActive=true", "LastName", DojoMemberFlags.PrivateContact);					
					break;
				case "Yudansha":
					members = mManager.GetCollection("RankID >= 8 AND IsPrimaryOrgActive=true", "LastName", DojoMemberFlags.PrivateContact);
					break;
				case "Mudansha":
					members = mManager.GetCollection("RankID <= 7 AND IsPrimaryOrgActive=true", "LastName", DojoMemberFlags.PrivateContact);
					break;
				case "All":
					members = mManager.GetCollection(string.Empty, "LastName", DojoMemberFlags.PrivateContact);
					break;
				default:
					throw(new Exception("ListType is invalid."));
			}

            SmtpClient smtpClient = new SmtpClient(smtpHost);
			
			MailMessage message = new MailMessage(mailFrom, 
                "Aikido Shobukan Dojo <members@aikido-shobukan.org>");
			message.Subject = mailSubject;
			message.Body = mailBody;
						
			int sendBccCounter = 0;
			int maxBccCount = 500;
			string address = string.Empty;

			try
			{
				foreach(DojoMember member in members)
				{
					if(member.PrivateContact.Email1 != null && 
						member.PrivateContact.Email1 != string.Empty &&
						member.PrivateContact.Email1.IndexOf("@") != -1)
					{
                        message.Bcc.Add(new MailAddress(member.PrivateContact.Email1));

						sendBccCounter++;

						// Send Emails if the BCC field is maxed
						if(maxBccCount == sendBccCounter)
						{
                            smtpClient.Send(message);
                            message.Bcc.Clear();
							sendBccCounter = 0;
						}						
					}
				}

				// Send Remaining Emails
                if (sendBccCounter > 0)
                {
                    smtpClient.Send(message);
                }

				sentFlag = true;
			}
			catch (Exception e)
			{
				errorFlag = true;
				errorMessage = e.ToString();
			}

		}

		#endregion

		protected override void OnLoad(EventArgs e)
		{
			EnsureChildControls();
			if(Page.IsPostBack)
			{
				mailFrom = ((TextBox) FindControl(this.ID+"from")).Text;
				mailBody = ((TextBox) FindControl(this.ID+"body")).Text;
				mailSubject = ((TextBox) FindControl(this.ID+"subject")).Text;
			}
			else
			{
				((TextBox) FindControl(this.ID+"from")).Text = mailFrom;
				((TextBox) FindControl(this.ID+"body")).Text = mailBody;
				((TextBox) FindControl(this.ID+"subject")).Text = mailSubject;
			}
		}

		public event EventHandler CancelButtonClicked;
		protected virtual void OnCancelButtonClicked(EventArgs e)
		{
			if(CancelButtonClicked != null)
				CancelButtonClicked(this, e);
		}

		#region Rendering

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
            
			columnCount = 2;
		}

		protected override void OnPreRender(EventArgs e)
		{
			EnsureChildControls();

			DropDownList listDropDown = (DropDownList) FindControl(this.ID+"listDropDown");
			foreach(ListItem l in listDropDown.Items)
			{
				if(listType == l.Value)
					l.Selected = true;
				else
					l.Selected = false;
			}
		}

		protected override void RenderContent(HtmlTextWriter output)
		{
			if(sentFlag)
			{
				output.WriteFullBeginTag("tr");
				output.WriteLine();
				RenderCell("Mail has been sent.");
				RenderCell(FindControl("DoneButton"), "align=\"right\"");
				output.WriteEndTag("tr");
				output.WriteLine();
			}
			else if(errorFlag)
			{
				output.WriteFullBeginTag("tr");
				output.WriteLine();
                RenderCell(errorMessage);
				output.WriteEndTag("tr");
				output.WriteLine();
			}
			else
			{
			
				// RENDER CONTENTS OF TABLE ================================
				// "To" Table Row
				output.WriteFullBeginTag("tr");
				output.WriteLine();
				RenderCell("To: ");
				RenderCell(FindControl(this.ID+"listDropDown"), "align=\"right\" width=\"100%\"");				
				output.WriteEndTag("tr");
				output.WriteLine();

				// "From" Table Row
				output.WriteFullBeginTag("tr");
				output.WriteLine();
				RenderCell("From: ");
				RenderCell(FindControl(this.ID+"from"));
				output.WriteEndTag("tr");
				output.WriteLine();

				// "From" Table Row
				output.WriteFullBeginTag("tr");
				output.WriteLine();
				RenderCell("Subject: ");
				RenderCell(FindControl(this.ID+"subject"));
				output.WriteEndTag("tr");
				output.WriteLine();

				// "Body Header" Table Row
				output.WriteFullBeginTag("tr");
				output.WriteLine();
				RenderCell("Body: ", "left", "2");
				output.WriteEndTag("tr");
				output.WriteLine();

				// "Body" Table Row
				output.WriteFullBeginTag("tr");
				output.WriteLine();
				RenderCell(FindControl(this.ID+"body"), "colspan=\"2\"");
				output.WriteEndTag("tr");
				output.WriteLine();

				output.WriteFullBeginTag("tr");
				output.WriteLine();
				output.Indent++;
				output.WriteBeginTag("td");
				output.WriteAttribute("align", "right");
				output.WriteAttribute("colspan", "2");
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Indent++;
				FindControl("okButton").RenderControl(output);
				output.Write("&nbsp;");
				FindControl("cancelButton").RenderControl(output);
				output.WriteEndTag("td");
				output.WriteLine();
				output.Indent = output.Indent - 2;
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

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					listType = (string) myState[1];
			}
		}

		protected override object SaveViewState() 
		{
			// Customized state management to handle saving state of contained objects  such as styles.

			object baseState = base.SaveViewState();
			
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = listType;

			return myState;
		}

		#endregion
	}
}