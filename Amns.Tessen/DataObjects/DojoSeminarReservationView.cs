using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoSeminarReservation.
	/// </summary>
	[ToolboxData("<DojoSeminarReservation:DojoSeminarReservationView runat=server></{0}:DojoSeminarReservationView>")]
	public class DojoSeminarReservationView : TableWindow, INamingContainer
	{
		private int dojoSeminarReservationID;
		private DojoSeminarReservation dojoSeminarReservation;

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for General Folder

		private Literal ltRegistration = new Literal();
		private Literal ltParentReservation = new Literal();

		#endregion

		#region Private Control Fields for Dates Folder

		private Literal ltIsBlockReservation = new Literal();
		private Literal ltCheckIn = new Literal();
		private Literal ltCheckOut = new Literal();

		#endregion

		#region Private Control Fields for Classes Folder

		private Literal ltIsClassReservation = new Literal();
		private Literal ltClass1 = new Literal();
		private Literal ltClass2 = new Literal();
		private Literal ltClass3 = new Literal();
		private Literal ltClass4 = new Literal();
		private Literal ltClass5 = new Literal();
		private Literal ltClass6 = new Literal();
		private Literal ltClass7 = new Literal();
		private Literal ltClass8 = new Literal();
		private Literal ltClass9 = new Literal();
		private Literal ltClass10 = new Literal();

		#endregion

		#region Private Control Fields for Definitions Folder

		private Literal ltIsDefinitionReservation = new Literal();
		private Literal ltDefinition1 = new Literal();
		private Literal ltDefinition2 = new Literal();
		private Literal ltDefinition3 = new Literal();

		#endregion

		private Button btOk = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoSeminarReservationID
		{
			get
			{
				return dojoSeminarReservationID;
			}
			set
			{
				dojoSeminarReservationID = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();

			#region Child Controls for General Folder

			ltRegistration.EnableViewState = false;
			Controls.Add(ltRegistration);

			ltParentReservation.EnableViewState = false;
			Controls.Add(ltParentReservation);

			#endregion

			#region Child Controls for Dates Folder

			ltIsBlockReservation.EnableViewState = false;
			Controls.Add(ltIsBlockReservation);

			ltCheckIn.EnableViewState = false;
			Controls.Add(ltCheckIn);

			ltCheckOut.EnableViewState = false;
			Controls.Add(ltCheckOut);

			#endregion

			#region Child Controls for Classes Folder

			ltIsClassReservation.EnableViewState = false;
			Controls.Add(ltIsClassReservation);

			ltClass1.EnableViewState = false;
			Controls.Add(ltClass1);

			ltClass2.EnableViewState = false;
			Controls.Add(ltClass2);

			ltClass3.EnableViewState = false;
			Controls.Add(ltClass3);

			ltClass4.EnableViewState = false;
			Controls.Add(ltClass4);

			ltClass5.EnableViewState = false;
			Controls.Add(ltClass5);

			ltClass6.EnableViewState = false;
			Controls.Add(ltClass6);

			ltClass7.EnableViewState = false;
			Controls.Add(ltClass7);

			ltClass8.EnableViewState = false;
			Controls.Add(ltClass8);

			ltClass9.EnableViewState = false;
			Controls.Add(ltClass9);

			ltClass10.EnableViewState = false;
			Controls.Add(ltClass10);

			#endregion

			#region Child Controls for Definitions Folder

			ltIsDefinitionReservation.EnableViewState = false;
			Controls.Add(ltIsDefinitionReservation);

			ltDefinition1.EnableViewState = false;
			Controls.Add(ltDefinition1);

			ltDefinition2.EnableViewState = false;
			Controls.Add(ltDefinition2);

			ltDefinition3.EnableViewState = false;
			Controls.Add(ltDefinition3);

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
			if(dojoSeminarReservationID != 0)
			{
				dojoSeminarReservation = new DojoSeminarReservation(dojoSeminarReservationID);

				#region Bind _system Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//


				#endregion

				#region Bind General Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// Registration

				if(dojoSeminarReservation.Registration != null)
					ltRegistration.Text = dojoSeminarReservation.Registration.ToString();
				else
					ltRegistration.Text = string.Empty;

				// ParentReservation

				if(dojoSeminarReservation.ParentReservation != null)
					ltParentReservation.Text = dojoSeminarReservation.ParentReservation.ToString();
				else
					ltParentReservation.Text = string.Empty;


				#endregion

				#region Bind Dates Folder

				//
				// Set Field Entries
				//

				ltIsBlockReservation.Text = dojoSeminarReservation.IsBlockReservation.ToString();
				ltCheckIn.Text = dojoSeminarReservation.CheckIn.ToString();
				ltCheckOut.Text = dojoSeminarReservation.CheckOut.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Classes Folder

				//
				// Set Field Entries
				//

				ltIsClassReservation.Text = dojoSeminarReservation.IsClassReservation.ToString();

				//
				// Set Children Selections
				//

				// Class1

				if(dojoSeminarReservation.Class1 != null)
					ltClass1.Text = dojoSeminarReservation.Class1.ToString();
				else
					ltClass1.Text = string.Empty;

				// Class2

				if(dojoSeminarReservation.Class2 != null)
					ltClass2.Text = dojoSeminarReservation.Class2.ToString();
				else
					ltClass2.Text = string.Empty;

				// Class3

				if(dojoSeminarReservation.Class3 != null)
					ltClass3.Text = dojoSeminarReservation.Class3.ToString();
				else
					ltClass3.Text = string.Empty;

				// Class4

				if(dojoSeminarReservation.Class4 != null)
					ltClass4.Text = dojoSeminarReservation.Class4.ToString();
				else
					ltClass4.Text = string.Empty;

				// Class5

				if(dojoSeminarReservation.Class5 != null)
					ltClass5.Text = dojoSeminarReservation.Class5.ToString();
				else
					ltClass5.Text = string.Empty;

				// Class6

				if(dojoSeminarReservation.Class6 != null)
					ltClass6.Text = dojoSeminarReservation.Class6.ToString();
				else
					ltClass6.Text = string.Empty;

				// Class7

				if(dojoSeminarReservation.Class7 != null)
					ltClass7.Text = dojoSeminarReservation.Class7.ToString();
				else
					ltClass7.Text = string.Empty;

				// Class8

				if(dojoSeminarReservation.Class8 != null)
					ltClass8.Text = dojoSeminarReservation.Class8.ToString();
				else
					ltClass8.Text = string.Empty;

				// Class9

				if(dojoSeminarReservation.Class9 != null)
					ltClass9.Text = dojoSeminarReservation.Class9.ToString();
				else
					ltClass9.Text = string.Empty;

				// Class10

				if(dojoSeminarReservation.Class10 != null)
					ltClass10.Text = dojoSeminarReservation.Class10.ToString();
				else
					ltClass10.Text = string.Empty;


				#endregion

				#region Bind Definitions Folder

				//
				// Set Field Entries
				//

				ltIsDefinitionReservation.Text = dojoSeminarReservation.IsDefinitionReservation.ToString();

				//
				// Set Children Selections
				//

				// Definition1

				if(dojoSeminarReservation.Definition1 != null)
					ltDefinition1.Text = dojoSeminarReservation.Definition1.ToString();
				else
					ltDefinition1.Text = string.Empty;

				// Definition2

				if(dojoSeminarReservation.Definition2 != null)
					ltDefinition2.Text = dojoSeminarReservation.Definition2.ToString();
				else
					ltDefinition2.Text = string.Empty;

				// Definition3

				if(dojoSeminarReservation.Definition3 != null)
					ltDefinition3.Text = dojoSeminarReservation.Definition3.ToString();
				else
					ltDefinition3.Text = string.Empty;


				#endregion

				text = "View  - " + dojoSeminarReservation.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoSeminarReservation ID", dojoSeminarReservationID.ToString());
			output.WriteEndTag("tr");

			render_systemFolder(output);

			renderGeneralFolder(output);

			renderDatesFolder(output);

			renderClassesFolder(output);

			renderDefinitionsFolder(output);

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

		#region Render General Folder

		private void renderGeneralFolder(HtmlTextWriter output)
		{
			//
			// Render General Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("General");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Registration
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Registration");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRegistration.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentReservation
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentReservation");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltParentReservation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Dates Folder

		private void renderDatesFolder(HtmlTextWriter output)
		{
			//
			// Render Dates Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Dates");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsBlockReservation
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsBlockReservation");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsBlockReservation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render CheckIn
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CheckIn");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltCheckIn.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render CheckOut
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CheckOut");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltCheckOut.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Classes Folder

		private void renderClassesFolder(HtmlTextWriter output)
		{
			//
			// Render Classes Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Classes");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsClassReservation
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsClassReservation");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsClassReservation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClass1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClass2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClass3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class4
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class4");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClass4.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class5
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class5");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClass5.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class6
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class6");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClass6.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class7
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class7");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClass7.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class8
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class8");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClass8.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class9
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class9");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClass9.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Class10
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class10");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClass10.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Definitions Folder

		private void renderDefinitionsFolder(HtmlTextWriter output)
		{
			//
			// Render Definitions Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Definitions");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsDefinitionReservation
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsDefinitionReservation");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsDefinitionReservation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Definition1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Definition1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDefinition1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Definition2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Definition2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDefinition2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Definition3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Definition3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDefinition3.RenderControl(output);
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
					dojoSeminarReservationID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoSeminarReservationID;
			return myState;
		}
	}
}
