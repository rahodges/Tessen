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
using Amns.Tessen.Web.UI.WebControls.Views;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for MemberListGrid.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:DojoTestGrid runat=server></{0}:DojoTestGrid>")]
	public class DojoTestGrid : TableGrid
	{
		string connectionString;
		Button btGenerateTestList = new Button();
		
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

		public DojoTestGrid() : base()
		{
			this.features |= TableWindowFeatures.ClientSideSelector;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			#region Process Test List Postback

			if(Page.IsPostBack)
			{
				string key;

				for(int i = 0; i < Page.Request.Form.Count; i++)
				{
					key = Page.Request.Form.GetKey(i);

					if(key.StartsWith(this.ClientID + "_remove_"))
					{
						int memberID = 
							int.Parse(key.Substring(this.ClientID.Length + 8));
						
						DojoTest test = new DojoTest(selectedID);
						TestListGenerator gen = new TestListGenerator(connectionString);						
						gen.RemoveMember(test.ActiveTestList, 
							DojoMember.NewPlaceHolder(memberID));
						gen.CompileTestList(test);						
					}
				}
			}

			#endregion
		}

		protected override void CreateChildControls()
		{
            base.CreateChildControls();

            //btGenerateTestList = new Button();
            //btGenerateTestList.Click += new EventHandler(btGenerateTestList_Click);
            //btGenerateTestList.Text = "Generate Test List";
            //Controls.Add(btGenerateTestList);

            //ComponentArt.Web.UI.ToolBar testToolbar = ToolBarUtility.DefaultToolBar("Test");
            //ToolBarUtility.AddControlItem(testToolbar, btGenerateTestList);
            //toolbars.Add(testToolbar);

			ChildControlsCreated = true;
		}

		protected void btGenerateTestList_Click(object sender, EventArgs e)
		{
			if(selectedID != -1)
			{
				TestListGenerator gen = new TestListGenerator(connectionString);
				gen.Generate(new DojoTest(selectedID));
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit (e);

			bool adminMode = Page.User.IsInRole("Tessen/Administrator");
			this.deleteButton.Enabled = adminMode;
			this.editButton.Enabled = adminMode;
			this.newButton.Enabled = adminMode;
		}

		#region Rendering
        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();

			DojoTestManager m = new DojoTestManager();
			DojoTestCollection dojoTestCollection = m.GetCollection(string.Empty, "TestDate DESC", 
				new DojoTestFlags[] { DojoTestFlags.Location });

			// Render Header Row
			this.headerLockEnabled = true;
			RenderRow(this.HeaderRowCssClass, "Test", "Date", "Time", "List Status");
			bool rowflag = false;
			string rowCssClass;		

			//
			// Render Records
			//
			foreach(DojoTest entry in dojoTestCollection)
			{				
				if(rowflag)		rowCssClass = this.defaultRowCssClass;
				else			rowCssClass = this.alternateRowCssClass;

				rowflag = !rowflag;

				output.WriteBeginTag("tr");
				output.WriteAttribute("i", entry.ID.ToString());
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.Indent++;

				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("<strong>" + entry.Name + "</strong>");
				output.Write("<br>");
				output.Write(entry.Location.BusinessName);
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Test Date
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.TestDate.ToShortDateString());
				output.WriteEndTag("td");
				output.WriteLine();

				//
				// Render Test Time
				//
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write(entry.TestDate.ToShortTimeString());
				output.WriteEndTag("td");
				output.WriteLine();

				// 
				// Render Active Test List
				// 
				output.WriteBeginTag("td");
				output.WriteAttribute("class", rowCssClass);
				output.WriteAttribute("valign", "top");
				output.Write(HtmlTextWriter.TagRightChar);
				if(entry.ActiveTestList != null)
				{
					if(entry.ActiveTestList.Status.IsDraft)
					{
						output.Write("Draft");
					}
					else if(entry.ActiveTestList.Status.IsFinal)
					{
						output.Write("Final");
					}
					else if(entry.ActiveTestList.Status.IsComplete)
					{
						output.Write("Complete");
					}
				}
				else
				{
					output.Write("None");
				}
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