using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Amns.GreyFox.WebControls;
using Amns.GreyFox.People;
using FreeTextBoxControls;

namespace Amns.GreyFox.Tessen.WebControls.Reports
{
	/// <summary>
	/// Default web editor for DojoSeminar.
	/// </summary>
	[DefaultProperty("ConnectionString"), 
		ToolboxData("<{0}:MembershipReport runat=server></{0}:MembershipReport>")]
	public class MembershipReport : TableWindow, INamingContainer
	{
		private string connectionString;

		private Amns.GreyFox.WebControls.TableGraph TableGraph1;				

		[Bindable(true), Category("Data"), DefaultValue(0)]
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

		protected override void CreateChildControls()
		{		
			ChildControlsCreated = true;
			TableGraph1.Title = "Rank Report (Active Members)";
			TableGraph1.UserWidth = "100";
			TableGraph1.XAxisTitle = "Ranks";
		}

		protected override void OnInit(EventArgs e)
		{
			columnCount = 4;
			features = TableWindowFeatures.DisableContentSeparation;
//			components = TableWindowComponents.Toolbar |
//				TableWindowComponents.Footer;
		}

		protected override void RenderToolbar(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			output.WriteLine();

			output.Indent++;
			output.WriteBeginTag("td");
			output.WriteAttribute("class", this.SubHeaderCssClass);
			output.WriteAttribute("height", "28px");
			output.WriteAttribute("colspan", this.columnCount.ToString());
			output.Write(HtmlTextWriter.TagRightChar);
			output.WriteLine();
			output.Indent++;			

			// Write Tabs

			output.Indent--;
			output.WriteEndTag("td");
			output.WriteLine();

			output.Indent--;
			output.WriteEndTag("tr");
			output.WriteLine();
		}

		protected override void RenderFooter(HtmlTextWriter output)
		{
			//
			// Render OK/Cancel Buttons
			//			
		}

		protected override void OnPreRender(EventArgs e)
		{
			EnsureWindowScripts();
		}

		protected override void RenderContent(HtmlTextWriter output)
		{
			// Get Membership
			DojoMemberManager mm = new DojoMemberManager(connectionString);
			DojoMemberCollection members = mm.GetMemberList(DojoMemberManager.MemberListType.Active);

			int[] rankValue
			int[] rankCounts = new int[20]

			DojoRankManager rm = new DojoRankManager(connectionString);
			DojoRankCollection ranks = rm.GetCollection(string.Empty, string.Empty);
			TableGraph1.RenderControl(output);
		}

//		protected override void LoadViewState(object savedState)
//		{
//			if(savedState != null)
//			{
//				object[] myState = (object[]) savedState;
//				if(myState[0] != null)
//					base.LoadViewState(myState[0]);
//				if(myState[1] != null)
//					dojoSeminarID = (int) myState[1];
//				if(myState[2] != null)
//					tabPage = (DojoSeminarEditorTab) myState[2];
//			}
//		}
//
//		protected override object SaveViewState()
//		{
//			object baseState = base.SaveViewState();
//			object[] myState = new object[3];
//			myState[0] = baseState;
//			myState[1] = dojoSeminarID;
//			myState[2] = tabPage;
//			return myState;
//		}
	}
}
