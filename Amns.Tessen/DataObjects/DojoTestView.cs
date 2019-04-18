using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.Rappahanock;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoTest.
	/// </summary>
	[ToolboxData("<DojoTest:DojoTestView runat=server></{0}:DojoTestView>")]
	public class DojoTestView : TableWindow, INamingContainer
	{
		private int dojoTestID;
		private DojoTest dojoTest;

		#region Private Control Fields for Default Folder

		private Literal ltName = new Literal();
		private Literal ltDescription = new Literal();
		private Literal ltTestDate = new Literal();
		private Literal ltLocation = new Literal();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for List Generator Folder

		private Literal ltListMemberType1 = new Literal();
		private Literal ltListMemberType2 = new Literal();
		private Literal ltListMemberType3 = new Literal();

		#endregion

		#region Private Control Fields for Administration Folder

		private Literal ltPanelChief = new Literal();
		private Literal ltPanelMember1 = new Literal();
		private Literal ltPanelMember2 = new Literal();
		private Literal ltPanelMember3 = new Literal();
		private Literal ltPanelMember4 = new Literal();
		private Literal ltPanelMember5 = new Literal();

		#endregion

		#region Private Control Fields for System Folder

		private Literal ltStatus = new Literal();
		private Literal ltActiveTestList = new Literal();

		#endregion

		#region Private Control Fields for Rappahanock Folder

		private Literal ltItem = new Literal();

		#endregion

		private Button btOk = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoTestID
		{
			get
			{
				return dojoTestID;
			}
			set
			{
				dojoTestID = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();

			#region Child Controls for Default Folder

			ltName.EnableViewState = false;
			Controls.Add(ltName);

			ltDescription.EnableViewState = false;
			Controls.Add(ltDescription);

			ltTestDate.EnableViewState = false;
			Controls.Add(ltTestDate);

			ltLocation.EnableViewState = false;
			Controls.Add(ltLocation);

			#endregion

			#region Child Controls for List Generator Folder

			ltListMemberType1.EnableViewState = false;
			Controls.Add(ltListMemberType1);

			ltListMemberType2.EnableViewState = false;
			Controls.Add(ltListMemberType2);

			ltListMemberType3.EnableViewState = false;
			Controls.Add(ltListMemberType3);

			#endregion

			#region Child Controls for Administration Folder

			ltPanelChief.EnableViewState = false;
			Controls.Add(ltPanelChief);

			ltPanelMember1.EnableViewState = false;
			Controls.Add(ltPanelMember1);

			ltPanelMember2.EnableViewState = false;
			Controls.Add(ltPanelMember2);

			ltPanelMember3.EnableViewState = false;
			Controls.Add(ltPanelMember3);

			ltPanelMember4.EnableViewState = false;
			Controls.Add(ltPanelMember4);

			ltPanelMember5.EnableViewState = false;
			Controls.Add(ltPanelMember5);

			#endregion

			#region Child Controls for System Folder

			ltStatus.EnableViewState = false;
			Controls.Add(ltStatus);

			ltActiveTestList.EnableViewState = false;
			Controls.Add(ltActiveTestList);

			#endregion

			#region Child Controls for Rappahanock Folder

			ltItem.EnableViewState = false;
			Controls.Add(ltItem);

			#endregion

			btOk.Text = "OK";
			btOk.Width = Unit.Pixel(72);
			btOk.EnableViewState = false;
			btOk.Click += new EventHandler(ok_Click);
			Controls.Add(btOk);

			btDelete.Text = "Delete";
			btDelete.Width = Unit.Pixel(72);
			btDelete.EnableViewState = false;
			btDelete.Click += new EventHandler(delete_Click);
			Controls.Add(btDelete);

			ChildControlsCreated = true;
		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			OnOkClicked(EventArgs.Empty);
		}

		#endregion

		protected void delete_Click(object sender, EventArgs e)
		{
			this.OnDeleteClicked(EventArgs.Empty);
		}

		public event EventHandler OkClicked;
		protected virtual void OnOkClicked(EventArgs e)
		{
			if(OkClicked != null)
				OkClicked(this, e);
		}

		public event EventHandler DeleteClicked;
		protected virtual void OnDeleteClicked(EventArgs e)
		{
			if(DeleteClicked != null)
			DeleteClicked(this, e);
		}

		protected override void OnInit(EventArgs e)
		{
			columnCount = 2;
			features = TableWindowFeatures.DisableContentSeparation | 
				TableWindowFeatures.WindowPrinter;
		}

		protected override void OnPreRender(EventArgs e)
		{
			if(dojoTestID != 0)
			{
				dojoTest = new DojoTest(dojoTestID);

				#region Bind Default Folder

				//
				// Set Field Entries
				//

				ltName.Text = dojoTest.Name.ToString();
				ltDescription.Text = dojoTest.Description.ToString();
				ltTestDate.Text = dojoTest.TestDate.ToString();

				//
				// Set Children Selections
				//

				// Location

				if(dojoTest.Location != null)
					ltLocation.Text = dojoTest.Location.ToString();
				else
					ltLocation.Text = string.Empty;


				#endregion

				#region Bind _system Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//


				#endregion

				#region Bind List Generator Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// ListMemberType1

				if(dojoTest.ListMemberType1 != null)
					ltListMemberType1.Text = dojoTest.ListMemberType1.ToString();
				else
					ltListMemberType1.Text = string.Empty;

				// ListMemberType2

				if(dojoTest.ListMemberType2 != null)
					ltListMemberType2.Text = dojoTest.ListMemberType2.ToString();
				else
					ltListMemberType2.Text = string.Empty;

				// ListMemberType3

				if(dojoTest.ListMemberType3 != null)
					ltListMemberType3.Text = dojoTest.ListMemberType3.ToString();
				else
					ltListMemberType3.Text = string.Empty;


				#endregion

				#region Bind Administration Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// PanelChief

				if(dojoTest.PanelChief != null)
					ltPanelChief.Text = dojoTest.PanelChief.ToString();
				else
					ltPanelChief.Text = string.Empty;

				// PanelMember1

				if(dojoTest.PanelMember1 != null)
					ltPanelMember1.Text = dojoTest.PanelMember1.ToString();
				else
					ltPanelMember1.Text = string.Empty;

				// PanelMember2

				if(dojoTest.PanelMember2 != null)
					ltPanelMember2.Text = dojoTest.PanelMember2.ToString();
				else
					ltPanelMember2.Text = string.Empty;

				// PanelMember3

				if(dojoTest.PanelMember3 != null)
					ltPanelMember3.Text = dojoTest.PanelMember3.ToString();
				else
					ltPanelMember3.Text = string.Empty;

				// PanelMember4

				if(dojoTest.PanelMember4 != null)
					ltPanelMember4.Text = dojoTest.PanelMember4.ToString();
				else
					ltPanelMember4.Text = string.Empty;

				// PanelMember5

				if(dojoTest.PanelMember5 != null)
					ltPanelMember5.Text = dojoTest.PanelMember5.ToString();
				else
					ltPanelMember5.Text = string.Empty;


				#endregion

				#region Bind System Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// Status

				if(dojoTest.Status != null)
					ltStatus.Text = dojoTest.Status.ToString();
				else
					ltStatus.Text = string.Empty;

				// ActiveTestList

				if(dojoTest.ActiveTestList != null)
					ltActiveTestList.Text = dojoTest.ActiveTestList.ToString();
				else
					ltActiveTestList.Text = string.Empty;


				#endregion

				#region Bind Rappahanock Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// Item

				if(dojoTest.Item != null)
					ltItem.Text = dojoTest.Item.ToString();
				else
					ltItem.Text = string.Empty;


				#endregion

				text = "View  - " + dojoTest.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoTest ID", dojoTestID.ToString());
			output.WriteEndTag("tr");

			renderDefaultFolder(output);

			render_systemFolder(output);

			renderList_GeneratorFolder(output);

			renderAdministrationFolder(output);

			renderSystemFolder(output);

			renderRappahanockFolder(output);

			//
			// Render OK/Cancel Buttons
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("colspan", "2");
			output.WriteAttribute("align", "right");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			btOk.RenderControl(output);
			if(DeleteClicked != null)
			{
				output.Write(" ");
				btDelete.RenderControl(output);
			}
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
		}

		#region Render Default Folder

		private void renderDefaultFolder(HtmlTextWriter output)
		{
			//
			// Render Default Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Default");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Name
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Name");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltName.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Description
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Description");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDescription.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render TestDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Test Date");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltTestDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Location
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Location");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltLocation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render _system Folder

		private void render_systemFolder(HtmlTextWriter output)
		{
			//
			// Render _system Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("System Folder");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render List Generator Folder

		private void renderList_GeneratorFolder(HtmlTextWriter output)
		{
			//
			// Render List Generator Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("List Generator");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ListMemberType1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ListMemberType1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltListMemberType1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ListMemberType2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ListMemberType2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltListMemberType2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ListMemberType3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ListMemberType3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltListMemberType3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Administration Folder

		private void renderAdministrationFolder(HtmlTextWriter output)
		{
			//
			// Render Administration Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Administration");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PanelChief
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelChief");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPanelChief.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PanelMember1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelMember1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPanelMember1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PanelMember2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelMember2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPanelMember2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PanelMember3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelMember3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPanelMember3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PanelMember4
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelMember4");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPanelMember4.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PanelMember5
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelMember5");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPanelMember5.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render System Folder

		private void renderSystemFolder(HtmlTextWriter output)
		{
			//
			// Render System Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("System");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Status
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Status");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltStatus.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ActiveTestList
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ActiveTestList");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltActiveTestList.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Rappahanock Folder

		private void renderRappahanockFolder(HtmlTextWriter output)
		{
			//
			// Render Rappahanock Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Rappahanock");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Item
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Item");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltItem.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		protected override void LoadViewState(object savedState)
		{
			if(savedState != null)
			{
				object[] myState = (object[]) savedState;
				if(myState[0] != null)
					base.LoadViewState(myState[0]);
				if(myState[1] != null)
					dojoTestID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoTestID;
			return myState;
		}
	}
}
