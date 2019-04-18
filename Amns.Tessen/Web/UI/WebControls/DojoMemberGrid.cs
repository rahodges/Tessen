using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen;
using Amns.Tessen.Utilities;
using Amns.Tessen.Web.UI.WebControls.Views;
using ComponentArt.Web.UI;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for MemberListGrid.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:DojoMemberGrid runat=server></{0}:DojoMemberGrid>")]
	public class DojoMemberGrid : TableGrid
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

		private string listType;
		private string indexKey = "Last Name";

		private DropDownList ddFormatType;
		private DropDownList ddListType;
		private DropDownList ddSortType;
		private DropDownList ddCommands ;

		private bool membersLoaded = false;
		private DojoMemberCollection members;
		private bool activeColumnEnabled = true;

		#region Behavior Properties

		
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

		
		[Bindable(true), Category("Behavior"), DefaultValue("")]
		public string IndexKey
		{
			get
			{
				return indexKey;
			}
			set
			{
				indexKey = value;
			}
		}

		#endregion

		#region Appearance Properties

		private string organizationActiveImageUrl = string.Empty;
		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public string OrganizationActiveImageUrl
		{
			get
			{
				return organizationActiveImageUrl;
			}
			set
			{
				organizationActiveImageUrl = value;
			}
		}

		private string membershipActiveImageUrl = string.Empty;
		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public string MembershipActiveImageUrl
		{
			get
			{
				return membershipActiveImageUrl;
			}
			set
			{
				membershipActiveImageUrl = value;
			}
		}

		private string addressWarningImageUrl = string.Empty;
		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public string AddressWarningImageUrl
		{
			get
			{
				return addressWarningImageUrl;
			}
			set
			{
				addressWarningImageUrl = value;
			}
		}

		private string duesWarningImageUrl = string.Empty;
		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public string DuesWarningImageUrl
		{
			get
			{
				return duesWarningImageUrl;
			}
			set
			{
				duesWarningImageUrl = value;
			}
		}

		#endregion

		#region Constructor

		public DojoMemberGrid() : base()
		{
			this.features |= TableWindowFeatures.ClientSideSelector;            			
			this.ContentWidth = Unit.Pixel(300);
		}

		#endregion

		#region Child Control Methods and Event Handling

		protected override void CreateToolBarControls()
		{
            base.CreateToolBarControls();

			ddFormatType = new DropDownList();
            ddFormatType.ID = "ddFormatType";
			ddFormatType.Items.Add("Default");
			ddFormatType.Items.Add("Addresses");
			ddFormatType.Items.Add("Email List");
			ddFormatType.Items.Add("Member IDs");
			ddFormatType.AutoPostBack = true;
            ddFormatType.EnableViewState = false;
			Controls.Add(ddFormatType);

			ddListType = new DropDownList();
			ddListType.ID = "ddListType";
			ddListType.Items.Add("Active");
			ddListType.Items.Add("Inactive");
			ddListType.Items.Add("Instructors");
			ddListType.Items.Add("Yudansha");
			ddListType.Items.Add("Mudansha");
			ddListType.Items.Add("Past Due");
			ddListType.Items.Add("Invalid Contacts");
			ddListType.Items.Add("All");
			ddListType.AutoPostBack = true;
            ddListType.EnableViewState = false;
			Controls.Add(ddListType);
			
			ddSortType = new DropDownList();
            ddSortType.ID = "ddSortType";
			ddSortType.Items.Add(new ListItem("Last Name"));
            ddSortType.Items.Add(new ListItem("First Name"));
			ddSortType.Items.Add(new ListItem("Rank"));
			ddSortType.Items.Add(new ListItem("Member Type"));
			ddSortType.Items.Add(new ListItem("Last Signin"));
			ddSortType.Items.Add(new ListItem("ID"));
			ddSortType.AutoPostBack = true;
            ddSortType.EnableViewState = false;
			Controls.Add(ddSortType);

			ddCommands = new DropDownList();
            ddCommands.ID = "ddCommands";
			ddCommands.Items.Add(new ListItem("--- Select ---"));
			ddCommands.Items.Add(new ListItem("Activate", "activate"));
			ddCommands.Items.Add(new ListItem("Deactivate", "deactivate"));
            ddCommands.Items.Add(new ListItem("Grant Membership", "grantmembership"));
			ddCommands.Items.Add(new ListItem("--- Address ---"));
			ddCommands.Items.Add(new ListItem("Validate", "validateaddress"));
			ddCommands.Items.Add(new ListItem("Invalidate", "invalidateaddress"));
			ddCommands.Items.Add(new ListItem("--- Dues ---"));
			ddCommands.Items.Add(new ListItem("Clear Dues", "cleardues"));
			ddCommands.Items.Add(new ListItem("Past Due", "pastdue"));			
			ddCommands.SelectedIndexChanged += new System.EventHandler(this.ddCommands_SelectIndexChanged);
			ddCommands.AutoPostBack = true;
            ddCommands.EnableViewState = false;
			Controls.Add(ddCommands);

            // TO ADD ITEMS TO THE TOOL BAR AFTER???

            ToolBarUtility.AddControlItem(toolbars[0], ddListType);
            ToolBarUtility.AddControlItem(toolbars[0], ddFormatType);
            ToolBarUtility.AddControlItem(toolbars[0], ddSortType);
            ToolBarUtility.AddControlItem(toolbars[0], ddCommands);
            toolbars[0].Items.Add(ToolBarUtility.Break());
            toolbars[0].Items.Add(ToolBarUtility.CommandItem("export", "Export", "export.gif"));

			bool adminMode = Page.User.IsInRole("Tessen/Administrator");
			this.deleteButton.Enabled = adminMode;
			this.editButton.Enabled = adminMode;
			this.newButton.Enabled = adminMode;
			this.ddCommands.Enabled = adminMode;
		}

        protected override void itemCommand(object sender, ComponentArt.Web.UI.ToolBarItemEventArgs e)
        {
            base.itemCommand(sender, e);

            if (e.Item.Value == "export")
            {
                string output;
                MemberExporter exporter;

                loadMembers();
                exporter = new MemberExporter();
                output = exporter.ConstructThirdPartyMailingList(members);

                Page.Response.Clear();
                Page.Response.ContentType = "text/plain";
                Page.Response.AddHeader("Content-Disposition",
                    "attachment;filename=" + ddListType.SelectedItem.Text + "_" + DateTime.Now.ToString("MM-dd-yy") + ".csv");
                Page.Response.Write(output);
                Page.Response.End();
            }
        }

		#endregion

		#region Events

		public event EventHandler SelectedMemberChanged;
		protected virtual void OnSelectedMemberChanged(EventArgs e)
		{
			if(SelectedMemberChanged != null)
				SelectedMemberChanged(this, e);
		}

		public event EventHandler MassEmailButtonClicked;
		protected virtual void OnMassEmailButtonClicked(EventArgs e)
		{
			MassEmailButtonClicked(this, e);
		}

		public event EventHandler ListIndexChanged;
		protected virtual void OnListIndexChanged(EventArgs e) 
		{
			if(ListIndexChanged != null)
				ListIndexChanged(this, e);
		}

		public event EventHandler IndexDropDownIndexChanged;
		protected virtual void OnIndexDropDownIndexChanged(EventArgs e)
		{
			if(IndexDropDownIndexChanged != null)
				IndexDropDownIndexChanged(this, e);
		}

		public void ddCommands_SelectIndexChanged(object sender, EventArgs e)
		{
			switch(ddCommands.SelectedItem.Value)
			{
				case "activate":
				{
					DojoMember member = new DojoMember(selectedID);
					member.IsPrimaryOrgActive = true;
					member.AddMemoMessage(DateTime.Now.ToString() + " : Activated");
					member.PrivateContact.Save();
					member.Save();
					break;
				}
				case "cleardues":
				{
					DojoMember member = new DojoMember(selectedID);
					member.IsPastDue = false;
					member.AddMemoMessage(DateTime.Now.ToString() + " : Cleared Dues\r\n" +
						member.AttendanceMessage);
					member.AttendanceMessage = string.Empty;
					member.PrivateContact.Save();
					member.Save();
					break;
				}
				case "pastdue":
				{
					DojoMember member = new DojoMember(selectedID);
					member.IsPastDue = true;
					member.AddMemoMessage(DateTime.Now.ToString() + " : Past Due");
					member.PrivateContact.Save();
					member.Save();
					break;
				}
				case "deactivate":
				{
					DojoMember member = new DojoMember(selectedID);
					member.IsPrimaryOrgActive = false;
					member.AddMemoMessage(DateTime.Now.ToString() + " : Deactivated");
					member.PrivateContact.Save();
					member.Save();
					break;
				}					
				case "validateaddress":
				{
					DojoMember member = new DojoMember(selectedID);
					member.PrivateContact.IsBadAddress = false;
					member.AddMemoMessage(DateTime.Now.ToString() + " : Address Validated");
					member.PrivateContact.Save();
					break;
				}
				case "invalidateaddress":
				{
					DojoMember member = new DojoMember(selectedID);
					member.PrivateContact.IsBadAddress = true;
					member.AddMemoMessage(DateTime.Now.ToString() + " : Address Invalidated");
					member.PrivateContact.Save();
					break;
				}
                case "grantmembership":
                {
                    DojoMember member = new DojoMember(selectedID);
                    MembershipBuilder builder = new MembershipBuilder();
                    builder.Load();        // TODO: SPEED ME UP USING THE ROOT MEMBER SEARCH!
                    builder.ProcessTrees();
                    builder.ProcessHashes();
                    List<MembershipPackage> packages = builder.GetMembershipPackages(member);
                    if (packages.Count > 0)                    
                    {
                        MembershipPackage package = packages[0];
                        package.ApplyGrant();
                        package.Save();
                        registerNotification("Membership package for " +
                            member.PrivateContact.FullName +
                            " has been granted. Any sub-members have also been granted " +
                            "memberships.");
                    }
                    packages = null;
                    builder = null;
                    break;
                }
			}

			ddCommands.SelectedIndex = 0;
		}


        private void registerNotification(string message)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(DojoMemberGrid),
                "WarningMessage",                 
                "<script language=\"javascript\">alert('" + message + "');</script>");
        }

		#endregion

		private void loadMembers()
		{
			// Do not reload members
			if(membersLoaded) return;

			DojoMemberManager mManager = new DojoMemberManager();
						
			DojoMemberFlags[] options = new DojoMemberFlags[]
			{
				DojoMemberFlags.PrivateContact,
				DojoMemberFlags.Rank,
				DojoMemberFlags.MemberType
			};

			string sortString = string.Empty;

			switch(ddSortType.SelectedItem.Value)
			{
				case "Rank":
					sortString = 
                        "Rank.DojoRankID, " +
                        "PrivateContact.LastName, " +
                        "PrivateContact.FirstName";
					break;
                case "First Name":
                    sortString = 
                        "PrivateContact.FirstName, " + 
                        "PrivateContact.LastName, " +
                        "PrivateContact.MiddleName";
                    break;
				case "Last Name":
					sortString = 
                        "PrivateContact.LastName, " +
                        "PrivateContact.FirstName, " +
                        "PrivateContact.MiddleName";
					break;
				case "Member Type":
					sortString = 
                        "MemberType.Name, " +
                        "PrivateContact.LastName, " +
                        "PrivateContact.FirstName";
					break;
				case "Last Signin":
					sortString = 
                        "LastSignin, " +
                        "PrivateContact.LastName, " +
                        "PrivateContact.FirstName";
					break;
				case "ID":
					sortString = 
                        "DojoMemberID";
					break;
			}

			switch(ddListType.SelectedItem.Value)
			{
				case "Active":
					members = mManager.GetCollection("IsPrimaryOrgActive=true", sortString, options);
					activeColumnEnabled = false;
					break;
				case "Inactive":
					members = mManager.GetCollection("IsPrimaryOrgActive=false", sortString, options);
					activeColumnEnabled = false;
					break;
				case "Instructors":
					members = mManager.GetCollection("IsInstructor=true", sortString, options);
					break;
				case "Mudansha":
					members = mManager.GetCollection("RankID<=7", sortString, options);
					break;
				case "Yudansha":
					members = mManager.GetCollection("RankID>=8", sortString, options);					
					break;
				case "Past Due":
					members = mManager.GetCollection("IsPastDue=true", sortString, options);
					break;
				case "Invalid Contacts":
					members = mManager.GetCollection("ValidationFlags<>0", sortString, options);
					break;
				default:
					members = mManager.GetCollection(string.Empty, sortString, options);
					break;
			}			
			
			membersLoaded = true;
		}

        #region Rendering

		#region PreRender

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			loadMembers();
		}

		#endregion

		#region Main Content Render Method
       
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			switch(ddFormatType.SelectedItem.Value)
			{
				case "Addresses":
					renderAddressesGrid(output);
					break;
				case "Default":
					renderDefaultGrid(output);
					break;
				case "Email List":
					renderEmailListGrid(output);
					break;
				case "Member IDs":
					this.renderIdGrid(output);
					break;
				default:
					throw(new Exception("Invalid member grid format selected."));
			}
		}

		#endregion

		#region Render Email List

		protected void renderEmailListGrid(HtmlTextWriter output)
		{
			// Render end of nameRow
			output.WriteFullBeginTag("tr");
			output.WriteFullBeginTag("td");
			output.WriteLine();

			foreach(DojoMember m in members)
			{
				if(m.PrivateContact.Email1 == null ||
					m.PrivateContact.Email1 == string.Empty)
					continue;

				output.Write(m.PrivateContact.FullName);
				output.Write(" &lt;");
				output.Write(m.PrivateContact.Email1);
				output.Write("&gt;");
				output.Write("; ");
			}

			// Render end of nameRow
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			output.WriteLine();

		}

		#endregion

		#region Render Address Grid

		protected void renderAddressesGrid(HtmlTextWriter output)
		{
			bool rowflag = false;
			string rowCssClass;
			string currentIndex;
			string previousIndex = "";
			string sortKey = ddSortType.SelectedItem.Text;

			foreach(DojoMember m in members)
			{				
				// ============ RENDER INDEX ROWS ==========

				switch(sortKey)
				{
					case "Rank":
						currentIndex = m.Rank.Name;
						break;
					case "Member Type":
						currentIndex = m.MemberType.Name;
						break;
					case "Last Signin":
						currentIndex = m.LastSignin.ToString("y");
						break;
					case "ID":
						currentIndex = "IDs";
						break;
					default:
						if(m.PrivateContact.LastName.Length < 1)
							currentIndex = "Bad Record";
						else
							currentIndex = m.PrivateContact.LastName.Substring(0,1).ToUpper();

						break;
				}

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

				// ============ RENDER THE RECORD ================

				if(rowflag)
					rowCssClass = this.defaultRowCssClass;
				else
					rowCssClass = this.alternateRowCssClass;

				rowflag = !rowflag;

				output.WriteBeginTag("tr");
				output.WriteAttribute("i", m.ID.ToString());
				output.Write(HtmlTextWriter.TagRightChar);

				// Render name and address
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				
				output.Write(m.PrivateContact.ConstructName("LS,FMi."));
				
				output.WriteFullBeginTag("br");
				output.Write(m.PrivateContact.ConstructAddress("<br>"));
				
				output.WriteEndTag("td");

				//
				// Render email and phone
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				if(m.PrivateContact.Email1 != string.Empty)
				{
					output.WriteBeginTag("a");
					output.WriteAttribute("href", string.Format("mailto:{0}", m.PrivateContact.Email1));
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write(m.PrivateContact.Email1);
					//					output.Write("Email");
					output.WriteEndTag("a");
					output.WriteFullBeginTag("br");
				}
				if(m.PrivateContact.HomePhone != string.Empty)
				{
					output.Write(m.PrivateContact.HomePhone);
					output.WriteFullBeginTag("br");
				}
				if(m.PrivateContact.WorkPhone != string.Empty)
					output.Write(m.PrivateContact.WorkPhone);
				output.WriteEndTag("td");

				// Render icons, rank and err... silly buttons.
				output.WriteBeginTag("td");
				output.WriteAttribute("align", "right");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);

				if(m.IsPrimaryOrgActive)
				{
					output.WriteBeginTag("img");
					output.WriteAttribute("src", membershipActiveImageUrl);
					output.Write(HtmlTextWriter.TagRightChar);
				}

				output.WriteFullBeginTag("br");

				if(m.RankDate != DateTime.MinValue)
				{					
					output.Write(m.Rank.Name);
					output.WriteFullBeginTag("br");
					output.Write(m.rankDate.ToShortDateString());
				}
				else
				{
					output.Write(m.Rank.Name);
					output.WriteFullBeginTag("br");
				}				

				// Render end of nameRow
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

		#region Render Default Grid

		protected void renderDefaultGrid(HtmlTextWriter output)
		{
			bool rowflag = false;
			string rowCssClass;
			string currentIndex;
			string previousIndex = "";
			string sortKey = ddSortType.SelectedItem.Text;

			foreach(DojoMember m in members)
			{				
				// ============ RENDER INDEX ROWS ==========

				switch(sortKey)
				{
					case "Rank":
						currentIndex = m.Rank.Name;
						break;
					case "Member Type":
						currentIndex = m.MemberType.Name;
						break;
					case "Last Signin":
						currentIndex = m.LastSignin.ToString("y");
						break;
					case "ID":
						currentIndex = "IDs";
						break;
                    case "First Name":
                        if(m.PrivateContact.FirstName.Length < 1)
                            currentIndex = "Bad Record";
                        else
                            currentIndex = m.PrivateContact.FirstName.Substring(0,1).ToUpper();
                        break;
					default:
						if(m.PrivateContact.LastName.Length < 1)
							currentIndex = "Bad Record";
						else
							currentIndex = m.PrivateContact.LastName.Substring(0,1).ToUpper();
						break;
				}

				if(currentIndex != previousIndex)
				{
					output.WriteFullBeginTag("tr");
					output.WriteBeginTag("td");
					output.WriteAttribute("valign", "top");
					output.WriteAttribute("colspan", "5");
					output.WriteAttribute("class", indexRowCssClass);
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write(currentIndex);

					output.WriteEndTag("td");
					output.WriteEndTag("tr");

					previousIndex = currentIndex;
				}

				// ============ RENDER THE RECORD ================

				if(rowflag)
					rowCssClass = this.defaultRowCssClass;
				else
					rowCssClass = this.alternateRowCssClass;

				rowflag = !rowflag;

				output.WriteBeginTag("tr");
				output.WriteAttribute("i", m.ID.ToString());
				output.Write(HtmlTextWriter.TagRightChar);

				//
				// Render name
				// 				
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("nowrap", "true");
				output.Write(HtmlTextWriter.TagRightChar);
                if (sortKey == "First Name")
                    output.Write(m.PrivateContact.ConstructName("FMi.LS"));
                else
				    output.Write(m.PrivateContact.ConstructName("LS,FMi."));				
				output.WriteEndTag("td");

				//
				// Render Rank Name
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("width", "100%");
				output.WriteAttribute("align", "right");
				output.Write(HtmlTextWriter.TagRightChar);                
				output.Write(m.Rank != null ? m.Rank.Name : "&nbsp;");
				output.WriteEndTag("td");

				//
				// Render Activity
				//				
				if(activeColumnEnabled)
				{
					output.WriteBeginTag("td");
					output.WriteAttribute("class", rowCssClass);
					output.WriteAttribute("width", "100%");
					output.WriteAttribute("align", "right");
					output.Write(HtmlTextWriter.TagRightChar);
					if(m.IsPrimaryOrgActive)
						output.Write("Active");
					else
						output.Write("&nbsp");
					output.WriteEndTag("td");
				}

				//
				// Render Dues Warning
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("width", "100%");
				output.WriteAttribute("align", "right");
				output.Write(HtmlTextWriter.TagRightChar);
				if(m.IsPastDue)
				{
					if(duesWarningImageUrl != string.Empty)
					{
						output.WriteBeginTag("img");
						output.WriteAttribute("src", duesWarningImageUrl);
						output.WriteAttribute("alt", "(!)");
						output.Write(HtmlTextWriter.TagRightChar);
					}
					else
					{
						output.Write(" (<font color=\"#FF0000\"><strong>$</strong></font>)");
					}
				}
				else
				{
					output.Write("&nbsp;");
				}
				output.WriteEndTag("td");

				//
				// Render Address Warning
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("width", "100%");
				output.WriteAttribute("align", "right");
				output.Write(HtmlTextWriter.TagRightChar);
				if(m.PrivateContact.IsBadAddress)
				{
					if(addressWarningImageUrl != string.Empty)
					{
						output.WriteBeginTag("img");
						output.WriteAttribute("src", addressWarningImageUrl);
						output.WriteAttribute("alt", "(!)");
						output.Write(HtmlTextWriter.TagRightChar);
					}
					else
					{
						output.Write(" (<font color=\"#C68E17\"><strong>!</strong></font>)");
					}
				}
				else
				{
					output.Write("&nbsp;");
				}
				output.WriteEndTag("td");

				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

		#region Render ID Grid

		protected void renderIdGrid(HtmlTextWriter output)
		{
			bool rowflag = false;
			string rowCssClass;
			string currentIndex;
			string previousIndex = "";
			string sortKey = ddSortType.SelectedItem.Text;

			this.headerLockEnabled = true;

			// Render ID
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", HeaderRowCssClass);
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ID");
			output.WriteEndTag("td");
			output.WriteLine();

			// Render name				
			output.WriteBeginTag("td");
			output.WriteAttribute("class", HeaderRowCssClass);
			output.WriteAttribute("width", "100%");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Name");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			foreach(DojoMember m in members)
			{				
				// ============ RENDER INDEX ROWS ==========

				switch(sortKey)
				{
					case "Rank":
						currentIndex = m.Rank.Name;
						break;
					case "Member Type":
						currentIndex = m.MemberType.Name;
						break;
					case "Last Signin":
						currentIndex = m.LastSignin.ToString("y");
						break;
					case "ID":
						currentIndex = "IDs";
						break;
					default:
						if(m.PrivateContact.LastName.Length < 1)
							currentIndex = "Bad Record";
						else
							currentIndex = m.PrivateContact.LastName.Substring(0,1).ToUpper();
						break;
				}

				if(currentIndex != previousIndex)
				{
					output.WriteFullBeginTag("tr");
					output.WriteBeginTag("td");
					output.WriteAttribute("valign", "top");
					output.WriteAttribute("colspan", "4");
					output.WriteAttribute("class", indexRowCssClass);
					output.Write(HtmlTextWriter.TagRightChar);

					//					if(sortKey == "Last Name" & acceleratorsEnabled)
					//					{
					//						output.WriteBeginTag("span");
					//						output.WriteAttribute("accesskey", currentIndex);
					//						output.WriteAttribute("tabindex", "20");
					//						output.WriteAttribute("style", "accelerator:true;");
					//						output.Write(HtmlTextWriter.TagRightChar);
					//						output.Write(currentIndex);
					//						output.WriteEndTag("span");
					//					}
					//					else
					output.Write(currentIndex);

					output.WriteEndTag("td");
					output.WriteEndTag("tr");

					previousIndex = currentIndex;
				}

				// ============ RENDER THE RECORD ================

				rowCssClass = !rowflag ? this.defaultRowCssClass : rowCssClass = this.alternateRowCssClass;

				// Render ID
				output.WriteBeginTag("tr");
				output.WriteAttribute("i", m.ID.ToString());
				output.Write(HtmlTextWriter.TagRightChar);
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("nowrap", "true");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(m.ID.ToString());
				output.WriteEndTag("td");
				output.WriteLine();

				// Render name				
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("width", "100%");
				output.Write(HtmlTextWriter.TagRightChar);
				
				output.Write(m.PrivateContact.ConstructName("LS,FMi."));

				output.WriteEndTag("td");
				
				// Render end of nameRow 
				output.WriteEndTag("tr");
				output.WriteLine();
			}
		}

		#endregion

		#endregion

		#region Render Footer

		protected override void RenderFooter(HtmlTextWriter output)
		{
			int active = 0;

			// Calculate Total Fee
			foreach(DojoMember member in members)
			{
				if(member.IsPrimaryOrgActive)
					active++;
			}

			output.WriteBeginTag("tr");
			output.WriteLine(HtmlTextWriter.TagRightChar);
			output.Indent++;

			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.SubHeaderCssClass);
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("<strong>{0}</strong> members  | <strong>{1}</strong> active",
				members.Count, active);
			output.WriteEndTag("td");
			output.WriteLine();

			output.Indent--;
			output.WriteEndTag("tr");
			output.WriteLine();
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
				if (myState[2] != null)
					indexKey = (string) myState[2];
			}
		}

		protected override object SaveViewState() 
		{
			// Customized state management to handle saving state of contained objects  such as styles.

			object baseState = base.SaveViewState();
			
			object[] myState = new object[3];

			myState[0] = baseState;
			myState[1] = listType;
			myState[2] = indexKey;

			return myState;
		}

		#endregion
	}
}