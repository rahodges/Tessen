using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.People;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoOrganization.
	/// </summary>
	[ToolboxData("<DojoOrganization:DojoOrganizationView runat=server></{0}:DojoOrganizationView>")]
	public class DojoOrganizationView : TableWindow, INamingContainer
	{
		private int dojoOrganizationID;
		private DojoOrganization dojoOrganization;

		#region Private Control Fields for Default Folder

		private Literal ltName = new Literal();
		private Literal ltDescription = new Literal();
		private Literal ltLocation = new Literal();
		private Literal ltClassLocations = new Literal();
		private Literal ltAdministrativeContact = new Literal();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Web Services Folder

		private Literal ltWebServiceUrl = new Literal();
		private Literal ltRefreshTime = new Literal();

		#endregion

		#region Private Control Fields for System Folder

		private Literal ltIsPrimary = new Literal();

		#endregion

		#region Private Control Fields for Membership Folder

		private Literal ltPromotionFlagEnabled = new Literal();
		private Literal ltDefaultMemberType = new Literal();

		#endregion

		private Button btOk = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoOrganizationID
		{
			get
			{
				return dojoOrganizationID;
			}
			set
			{
				dojoOrganizationID = value;
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

			ltLocation.EnableViewState = false;
			Controls.Add(ltLocation);

			ltClassLocations.EnableViewState = false;
			Controls.Add(ltClassLocations);

			ltAdministrativeContact.EnableViewState = false;
			Controls.Add(ltAdministrativeContact);

			#endregion

			#region Child Controls for Web Services Folder

			ltWebServiceUrl.EnableViewState = false;
			Controls.Add(ltWebServiceUrl);

			ltRefreshTime.EnableViewState = false;
			Controls.Add(ltRefreshTime);

			#endregion

			#region Child Controls for System Folder

			ltIsPrimary.EnableViewState = false;
			Controls.Add(ltIsPrimary);

			#endregion

			#region Child Controls for Membership Folder

			ltDefaultMemberType.EnableViewState = false;
			Controls.Add(ltDefaultMemberType);

			ltPromotionFlagEnabled.EnableViewState = false;
			Controls.Add(ltPromotionFlagEnabled);

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
			if(dojoOrganizationID != 0)
			{
				dojoOrganization = new DojoOrganization(dojoOrganizationID);

				#region Bind Default Folder

				//
				// Set Field Entries
				//

				ltName.Text = dojoOrganization.Name.ToString();
				ltDescription.Text = dojoOrganization.Description.ToString();

				//
				// Set Children Selections
				//

				// Location

				if(dojoOrganization.Location != null)
					ltLocation.Text = dojoOrganization.Location.ToString();
				else
					ltLocation.Text = string.Empty;

				// ClassLocations

				if(dojoOrganization.ClassLocations != null)
					ltClassLocations.Text = dojoOrganization.ClassLocations.ToString();
				else
					ltClassLocations.Text = string.Empty;

				// AdministrativeContact

				if(dojoOrganization.AdministrativeContact != null)
					ltAdministrativeContact.Text = dojoOrganization.AdministrativeContact.ToString();
				else
					ltAdministrativeContact.Text = string.Empty;


				#endregion

				#region Bind _system Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//


				#endregion

				#region Bind Web Services Folder

				//
				// Set Field Entries
				//

				ltWebServiceUrl.Text = dojoOrganization.WebServiceUrl.ToString();
				ltRefreshTime.Text = dojoOrganization.RefreshTime.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind System Folder

				//
				// Set Field Entries
				//

				ltIsPrimary.Text = dojoOrganization.IsPrimary.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Membership Folder

				//
				// Set Field Entries
				//

				ltPromotionFlagEnabled.Text = dojoOrganization.PromotionFlagEnabled.ToString();

				//
				// Set Children Selections
				//

				// DefaultMemberType

				if(dojoOrganization.DefaultMemberType != null)
					ltDefaultMemberType.Text = dojoOrganization.DefaultMemberType.ToString();
				else
					ltDefaultMemberType.Text = string.Empty;


				#endregion

				text = "View  - " + dojoOrganization.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoOrganization ID", dojoOrganizationID.ToString());
			output.WriteEndTag("tr");

			renderDefaultFolder(output);

			render_systemFolder(output);

			renderWeb_ServicesFolder(output);

			renderSystemFolder(output);

			renderMembershipFolder(output);

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
			output.Write("Name of Organization");
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

			//
			// Render ClassLocations
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ClassLocations");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltClassLocations.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AdministrativeContact
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AdministrativeContact");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAdministrativeContact.RenderControl(output);
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

		#region Render Web Services Folder

		private void renderWeb_ServicesFolder(HtmlTextWriter output)
		{
			//
			// Render Web Services Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Web Services");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render WebServiceUrl
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Webservice URL for external Tessen connections.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltWebServiceUrl.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render RefreshTime
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("RefreshTime");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltRefreshTime.RenderControl(output);
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
			// Render IsPrimary
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Primary Organization?");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltIsPrimary.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Membership Folder

		private void renderMembershipFolder(HtmlTextWriter output)
		{
			//
			// Render Membership Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Membership");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DefaultMemberType
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DefaultMemberType");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDefaultMemberType.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PromotionFlagEnabled
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Activates promotion limiting.");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltPromotionFlagEnabled.RenderControl(output);
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
					dojoOrganizationID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoOrganizationID;
			return myState;
		}
	}
}
