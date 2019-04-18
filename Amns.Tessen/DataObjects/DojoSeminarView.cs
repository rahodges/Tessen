using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using FreeTextBoxControls;
using Amns.GreyFox.People;
using Amns.Rappahanock;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoSeminar.
	/// </summary>
	[ToolboxData("<DojoSeminar:DojoSeminarView runat=server></{0}:DojoSeminarView>")]
	public class DojoSeminarView : TableWindow, INamingContainer
	{
		private int dojoSeminarID;
		private DojoSeminar dojoSeminar;

		#region Private Control Fields for General Folder

		private Literal ltName = new Literal();
		private Literal ltStartDate = new Literal();
		private Literal ltEndDate = new Literal();
		private Literal ltDescription = new Literal();
		private Literal ltIsLocal = new Literal();
		private Literal ltLocation = new Literal();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Registration Folder

		private Literal ltClassUnitType = new Literal();
		private Literal ltClassUnitFee = new Literal();
		private Literal ltBaseRegistrationFee = new Literal();
		private Literal ltRegistrationEnabled = new Literal();
		private Literal ltRegistrationStart = new Literal();
		private Literal ltFullEarlyRegistrationFee = new Literal();
		private Literal ltEarlyEndDate = new Literal();
		private Literal ltFullRegistrationFee = new Literal();
		private Literal ltLateStartDate = new Literal();
		private Literal ltFullLateRegistrationFee = new Literal();
		private Literal ltRegistrationEnd = new Literal();
		private Literal ltOptions = new Literal();

		#endregion

		#region Private Control Fields for Details Folder

		private Literal ltDetails = new Literal();
		private Literal ltDetailsOverrideUrl = new Literal();
		private Literal ltPdfUrl = new Literal();

		#endregion

		#region Private Control Fields for Rappahanock Folder

		private Literal ltItem = new Literal();

		#endregion

		private Button btOk = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoSeminarID
		{
			get
			{
				return dojoSeminarID;
			}
			set
			{
				dojoSeminarID = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();

			#region Child Controls for General Folder

			ltName.EnableViewState = false;
			Controls.Add(ltName);

			ltStartDate.EnableViewState = false;
			Controls.Add(ltStartDate);

			ltEndDate.EnableViewState = false;
			Controls.Add(ltEndDate);

			ltDescription.EnableViewState = false;
			Controls.Add(ltDescription);

			ltIsLocal.EnableViewState = false;
			Controls.Add(ltIsLocal);

			ltLocation.EnableViewState = false;
			Controls.Add(ltLocation);

			#endregion

			#region Child Controls for Registration Folder

			ltClassUnitType.EnableViewState = false;
			Controls.Add(ltClassUnitType);

			ltClassUnitFee.EnableViewState = false;
			Controls.Add(ltClassUnitFee);

			ltBaseRegistrationFee.EnableViewState = false;
			Controls.Add(ltBaseRegistrationFee);

			ltRegistrationEnabled.EnableViewState = false;
			Controls.Add(ltRegistrationEnabled);

			ltRegistrationStart.EnableViewState = false;
			Controls.Add(ltRegistrationStart);

			ltFullEarlyRegistrationFee.EnableViewState = false;
			Controls.Add(ltFullEarlyRegistrationFee);

			ltEarlyEndDate.EnableViewState = false;
			Controls.Add(ltEarlyEndDate);

			ltFullRegistrationFee.EnableViewState = false;
			Controls.Add(ltFullRegistrationFee);

			ltLateStartDate.EnableViewState = false;
			Controls.Add(ltLateStartDate);

			ltFullLateRegistrationFee.EnableViewState = false;
			Controls.Add(ltFullLateRegistrationFee);

			ltRegistrationEnd.EnableViewState = false;
			Controls.Add(ltRegistrationEnd);

			ltOptions.EnableViewState = false;
			Controls.Add(ltOptions);

			#endregion

			#region Child Controls for Details Folder

			ltDetails.EnableViewState = false;
			Controls.Add(ltDetails);

			ltDetailsOverrideUrl.EnableViewState = false;
			Controls.Add(ltDetailsOverrideUrl);

			ltPdfUrl.EnableViewState = false;
			Controls.Add(ltPdfUrl);

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
			if(dojoSeminarID != 0)
			{
				dojoSeminar = new DojoSeminar(dojoSeminarID);

				#region Bind General Folder

				//
				// Set Field Entries
				//

				ltName.Text = dojoSeminar.Name.ToString();
				ltStartDate.Text = dojoSeminar.StartDate.ToString();
				ltEndDate.Text = dojoSeminar.EndDate.ToString();
				ltDescription.Text = dojoSeminar.Description.ToString();
				ltIsLocal.Text = dojoSeminar.IsLocal.ToString();

				//
				// Set Children Selections
				//

				// Location

				if(dojoSeminar.Location != null)
					ltLocation.Text = dojoSeminar.Location.ToString();
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

				#region Bind Registration Folder

				//
				// Set Field Entries
				//

				ltClassUnitType.Text = dojoSeminar.ClassUnitType.ToString();
				ltClassUnitFee.Text = dojoSeminar.ClassUnitFee.ToString();
				ltBaseRegistrationFee.Text = dojoSeminar.BaseRegistrationFee.ToString();
				ltRegistrationEnabled.Text = dojoSeminar.RegistrationEnabled.ToString();
				ltRegistrationStart.Text = dojoSeminar.RegistrationStart.ToString();
				ltFullEarlyRegistrationFee.Text = dojoSeminar.FullEarlyRegistrationFee.ToString();
				ltEarlyEndDate.Text = dojoSeminar.EarlyEndDate.ToString();
				ltFullRegistrationFee.Text = dojoSeminar.FullRegistrationFee.ToString();
				ltLateStartDate.Text = dojoSeminar.LateStartDate.ToString();
				ltFullLateRegistrationFee.Text = dojoSeminar.FullLateRegistrationFee.ToString();
				ltRegistrationEnd.Text = dojoSeminar.RegistrationEnd.ToString();

				//
				// Set Children Selections
				//

				// Options

				if(dojoSeminar.Options != null)
					ltOptions.Text = dojoSeminar.Options.ToString();
				else
					ltOptions.Text = string.Empty;


				#endregion

				#region Bind Details Folder

				//
				// Set Field Entries
				//

				ltDetails.Text = dojoSeminar.Details.ToString();
				ltDetailsOverrideUrl.Text = dojoSeminar.DetailsOverrideUrl.ToString();
				ltPdfUrl.Text = dojoSeminar.PdfUrl.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Rappahanock Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// Item

				if(dojoSeminar.Item != null)
					ltItem.Text = dojoSeminar.Item.ToString();
				else
					ltItem.Text = string.Empty;


				#endregion

				text = "View  - " + dojoSeminar.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoSeminar ID", dojoSeminarID.ToString());
			output.WriteEndTag("tr");

			renderGeneralFolder(output);

			render_systemFolder(output);

			renderRegistrationFolder(output);

			renderDetailsFolder(output);

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
			// Render Name
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Seminar name.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltName.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render StartDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("First date of seminar.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltStartDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render EndDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Last day of seminar.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltEndDate.RenderControl(output);
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
			output.Write("Seminar description.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDescription.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsLocal
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsLocal");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsLocal.RenderControl(output);
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

		#region Render Registration Folder

		private void renderRegistrationFolder(HtmlTextWriter output)
		{
			//
			// Render Registration Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Registration");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ClassUnitType
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ClassUnitType");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClassUnitType.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ClassUnitFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Class unit fee.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClassUnitFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render BaseRegistrationFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Base registration fee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltBaseRegistrationFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RegistrationEnabled
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Enable registration.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRegistrationEnabled.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RegistrationStart
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Registration start.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRegistrationStart.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render FullEarlyRegistrationFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("FullEarlyRegistrationFee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltFullEarlyRegistrationFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render EarlyEndDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("EarlyEndDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltEarlyEndDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render FullRegistrationFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Full registration fee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltFullRegistrationFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render LateStartDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("LateStartDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltLateStartDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render FullLateRegistrationFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("FullLateRegistrationFee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltFullLateRegistrationFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RegistrationEnd
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Registration end.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRegistrationEnd.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Options
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Options");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOptions.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Details Folder

		private void renderDetailsFolder(HtmlTextWriter output)
		{
			//
			// Render Details Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Details");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Details
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Details");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDetails.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DetailsOverrideUrl
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Details Override URL");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDetailsOverrideUrl.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PdfUrl
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PDF Link");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPdfUrl.RenderControl(output);
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
					dojoSeminarID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoSeminarID;
			return myState;
		}
	}
}
