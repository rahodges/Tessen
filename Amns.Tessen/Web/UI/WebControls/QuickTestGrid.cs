using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for MemberListGrid.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:QuickTestGrid runat=server></{0}:QuickTestGrid>")]
	public class QuickTestGrid : TableWindow, IPostBackEventHandler 
	{
		private string connectionString;
		private DateTime minDateTime;
		private DateTime maxDateTime;
		
		private int selectedID = -1;

		private bool selectEnabled = true;
		private bool deleteEnabled;

		private string defaultRowCssClass;
		private string alternateRowCssClass;
		
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
		DefaultValue(-1)]
		public int SelectedID
		{
			get
			{
				return selectedID;
			}
			set
			{
				selectedID = value;
			}
		}

		[Bindable(true),
		Category("Behavior"),
		DefaultValue(-1)]
		public DateTime MaxDateTime
		{
			get
			{
				return maxDateTime;
			}
			set
			{
				maxDateTime = value;
			}
		}

		[Bindable(true),
		Category("Behavior"),
		DefaultValue(-1)]
		public DateTime MinDateTime
		{
			get
			{
				return minDateTime;
			}
			set
			{
				minDateTime = value;
			}
		}
		
		[Bindable(true), Category("Behavior"), DefaultValue(true)]
		public bool SelectEnabled
		{
			get
			{
				return selectEnabled;
			}
			set
			{
				selectEnabled = value;
			}
		}
		
		[Bindable(true), Category("Behavior"), DefaultValue(false)]
		public bool DeleteEnabled
		{
			get
			{
				return deleteEnabled;
			}
			set
			{
				deleteEnabled = value;
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

		#endregion

		#region Child Control Methods and Event Handling

		protected override void CreateChildControls()
		{
			ChildControlsCreated = true;
		}

		#endregion

		#region Events

		public event EventHandler SelectionChanged;
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if(SelectionChanged != null)
				SelectionChanged(this, e);
		}

		#endregion

		protected override void OnInit(EventArgs e)
		{
			columnCount = 1;
			features = TableWindowFeatures.DisableContentSeparation;
			// | TableWindowFeatures.Scroller;
		}

		#region Rendering

		protected override void OnPreRender(EventArgs e)
		{
			EnsureChildControls();
		}

		protected override void RenderToolbar(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			output.WriteLine();

			output.Indent++;
			output.WriteBeginTag("td");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("class", this.SubHeaderCssClass);
			output.WriteAttribute("height", "28px");
			output.WriteAttribute("colspan", this.columnCount.ToString());
			output.Write(HtmlTextWriter.TagRightChar);
			output.WriteLine();

			output.Indent++;			

			// RENDER OBJECTS

			output.Indent--;
			output.WriteEndTag("td");
			output.WriteLine();

			output.Indent--;
			output.WriteEndTag("tr");
			output.WriteLine();
		}
        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void RenderContent(HtmlTextWriter output)
		{
			EnsureChildControls();

			DojoTestManager m = new DojoTestManager();
			DojoTestCollection dojoTestCollection = m.GetCollection("DojoTest.TestDate>=#" +
				minDateTime.ToShortDateString() + "# AND DojoTest.TestDate<=#" +
				maxDateTime.ToShortDateString() + "#", " DojoTest.TestDate ",
				new DojoTestFlags[] {
					DojoTestFlags.Location});

			bool rowflag = false;
			string rowCssClass;		

			//
			// Render Records
			//
			foreach(DojoTest entry in dojoTestCollection)
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
				// Render Main Representation of Record
				//				
				output.WriteBeginTag("td");
				output.WriteAttribute("valign", "top");
				output.WriteAttribute("class", rowCssClass);
				output.Write(HtmlTextWriter.TagRightChar);
				
				if(selectEnabled)
				{
					output.WriteBeginTag("a");
					output.WriteAttribute("href", "javascript:" + Page.ClientScript.GetPostBackEventReference(this, "edit_" + entry.ID));
					output.Write(HtmlTextWriter.TagRightChar);
                    output.Write(entry.Name);
					output.WriteEndTag("a");
					
					
					
				}
				else
				{
					output.Write(entry.Name);									
				}
				output.Write("<br>");
				output.Write(entry.Location.BusinessName);
				output.Write("<br>");
				output.Write(entry.TestDate.ToShortDateString());
				output.Write(" @ ");
				output.Write(entry.TestDate.ToShortTimeString());
				output.WriteEndTag("td");
				output.WriteLine();

				if(deleteEnabled)
				{
					output.WriteBeginTag("a");
					output.WriteAttribute("href", "javascript:" + Page.ClientScript.GetPostBackEventReference(this, "delete_" + entry.ID));
					output.Write(HtmlTextWriter.TagRightChar);
					output.Write("delete");
					output.WriteEndTag("a");
					output.WriteLine();
				}

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

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					selectedID = (int) myState[1];
			}
		}

		protected override object SaveViewState() 
		{
			// Customized state management to handle saving state of contained objects  such as styles.

			object baseState = base.SaveViewState();
			
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = selectedID;

			return myState;
		}

		#endregion
	}
}