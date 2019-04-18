using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoAccessControlGroup.
	/// </summary>
	[ToolboxData("<DojoAccessControlGroup:DojoAccessControlGroupView runat=server></{0}:DojoAccessControlGroupView>")]
	public class DojoAccessControlGroupView : TableWindow, INamingContainer
	{
		private int dojoAccessControlGroupID;
		private DojoAccessControlGroup dojoAccessControlGroup;

		#region Private Control Fields for _system Folder

		private Literal ltCreateDate = new Literal();
		private Literal ltModifyDate = new Literal();

		#endregion

		#region Private Control Fields for General Folder

		private Literal ltName = new Literal();
		private Literal ltDescription = new Literal();
		private Literal ltOrderNum = new Literal();

		#endregion

		#region Private Control Fields for Allowed Folder

		private Literal ltAllowedMemberType1 = new Literal();
		private Literal ltAllowedMemberType2 = new Literal();
		private Literal ltAllowedMemberType3 = new Literal();
		private Literal ltAllowedMemberType4 = new Literal();
		private Literal ltAllowedMemberType5 = new Literal();

		#endregion

		#region Private Control Fields for Denied Folder

		private Literal ltDeniedMemberType1 = new Literal();
		private Literal ltDeniedMemberType2 = new Literal();
		private Literal ltDeniedMemberType3 = new Literal();
		private Literal ltDeniedMemberType4 = new Literal();
		private Literal ltDeniedMemberType5 = new Literal();

		#endregion

		private Button btOk = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoAccessControlGroupID
		{
			get
			{
				return dojoAccessControlGroupID;
			}
			set
			{
				dojoAccessControlGroupID = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();

			#region Child Controls for _system Folder

			ltCreateDate.EnableViewState = false;
			Controls.Add(ltCreateDate);

			ltModifyDate.EnableViewState = false;
			Controls.Add(ltModifyDate);

			#endregion

			#region Child Controls for General Folder

			ltName.EnableViewState = false;
			Controls.Add(ltName);

			ltDescription.EnableViewState = false;
			Controls.Add(ltDescription);

			ltOrderNum.EnableViewState = false;
			Controls.Add(ltOrderNum);

			#endregion

			#region Child Controls for Allowed Folder

			ltAllowedMemberType1.EnableViewState = false;
			Controls.Add(ltAllowedMemberType1);

			ltAllowedMemberType2.EnableViewState = false;
			Controls.Add(ltAllowedMemberType2);

			ltAllowedMemberType3.EnableViewState = false;
			Controls.Add(ltAllowedMemberType3);

			ltAllowedMemberType4.EnableViewState = false;
			Controls.Add(ltAllowedMemberType4);

			ltAllowedMemberType5.EnableViewState = false;
			Controls.Add(ltAllowedMemberType5);

			#endregion

			#region Child Controls for Denied Folder

			ltDeniedMemberType1.EnableViewState = false;
			Controls.Add(ltDeniedMemberType1);

			ltDeniedMemberType2.EnableViewState = false;
			Controls.Add(ltDeniedMemberType2);

			ltDeniedMemberType3.EnableViewState = false;
			Controls.Add(ltDeniedMemberType3);

			ltDeniedMemberType4.EnableViewState = false;
			Controls.Add(ltDeniedMemberType4);

			ltDeniedMemberType5.EnableViewState = false;
			Controls.Add(ltDeniedMemberType5);

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
			if(dojoAccessControlGroupID != 0)
			{
				dojoAccessControlGroup = new DojoAccessControlGroup(dojoAccessControlGroupID);

				#region Bind _system Folder

				//
				// Set Field Entries
				//

				ltCreateDate.Text = dojoAccessControlGroup.CreateDate.ToString();
				ltModifyDate.Text = dojoAccessControlGroup.ModifyDate.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind General Folder

				//
				// Set Field Entries
				//

				ltName.Text = dojoAccessControlGroup.Name.ToString();
				ltDescription.Text = dojoAccessControlGroup.Description.ToString();
				ltOrderNum.Text = dojoAccessControlGroup.OrderNum.ToString();

				//
				// Set Children Selections
				//


				#endregion

				#region Bind Allowed Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// AllowedMemberType1

				if(dojoAccessControlGroup.AllowedMemberType1 != null)
					ltAllowedMemberType1.Text = dojoAccessControlGroup.AllowedMemberType1.ToString();
				else
					ltAllowedMemberType1.Text = string.Empty;

				// AllowedMemberType2

				if(dojoAccessControlGroup.AllowedMemberType2 != null)
					ltAllowedMemberType2.Text = dojoAccessControlGroup.AllowedMemberType2.ToString();
				else
					ltAllowedMemberType2.Text = string.Empty;

				// AllowedMemberType3

				if(dojoAccessControlGroup.AllowedMemberType3 != null)
					ltAllowedMemberType3.Text = dojoAccessControlGroup.AllowedMemberType3.ToString();
				else
					ltAllowedMemberType3.Text = string.Empty;

				// AllowedMemberType4

				if(dojoAccessControlGroup.AllowedMemberType4 != null)
					ltAllowedMemberType4.Text = dojoAccessControlGroup.AllowedMemberType4.ToString();
				else
					ltAllowedMemberType4.Text = string.Empty;

				// AllowedMemberType5

				if(dojoAccessControlGroup.AllowedMemberType5 != null)
					ltAllowedMemberType5.Text = dojoAccessControlGroup.AllowedMemberType5.ToString();
				else
					ltAllowedMemberType5.Text = string.Empty;


				#endregion

				#region Bind Denied Folder

				//
				// Set Field Entries
				//


				//
				// Set Children Selections
				//

				// DeniedMemberType1

				if(dojoAccessControlGroup.DeniedMemberType1 != null)
					ltDeniedMemberType1.Text = dojoAccessControlGroup.DeniedMemberType1.ToString();
				else
					ltDeniedMemberType1.Text = string.Empty;

				// DeniedMemberType2

				if(dojoAccessControlGroup.DeniedMemberType2 != null)
					ltDeniedMemberType2.Text = dojoAccessControlGroup.DeniedMemberType2.ToString();
				else
					ltDeniedMemberType2.Text = string.Empty;

				// DeniedMemberType3

				if(dojoAccessControlGroup.DeniedMemberType3 != null)
					ltDeniedMemberType3.Text = dojoAccessControlGroup.DeniedMemberType3.ToString();
				else
					ltDeniedMemberType3.Text = string.Empty;

				// DeniedMemberType4

				if(dojoAccessControlGroup.DeniedMemberType4 != null)
					ltDeniedMemberType4.Text = dojoAccessControlGroup.DeniedMemberType4.ToString();
				else
					ltDeniedMemberType4.Text = string.Empty;

				// DeniedMemberType5

				if(dojoAccessControlGroup.DeniedMemberType5 != null)
					ltDeniedMemberType5.Text = dojoAccessControlGroup.DeniedMemberType5.ToString();
				else
					ltDeniedMemberType5.Text = string.Empty;


				#endregion

				text = "View kitTessen_AccessControlGroups - " + dojoAccessControlGroup.ToString();
			}
		}
		protected override void RenderContent(HtmlTextWriter output)
		{
			output.WriteFullBeginTag("tr");
			RenderRow("row1", "DojoAccessControlGroup ID", dojoAccessControlGroupID.ToString());
			output.WriteEndTag("tr");

			render_systemFolder(output);

			renderGeneralFolder(output);

			renderAllowedFolder(output);

			renderDeniedFolder(output);

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

			//
			// Render CreateDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CreateDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltCreateDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ModifyDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ModifyDate");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltModifyDate.RenderControl(output);
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
			// Render OrderNum
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OrderNum");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltOrderNum.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Allowed Folder

		private void renderAllowedFolder(HtmlTextWriter output)
		{
			//
			// Render Allowed Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Allowed");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowedMemberType1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowedMemberType1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAllowedMemberType1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowedMemberType2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowedMemberType2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAllowedMemberType2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowedMemberType3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowedMemberType3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAllowedMemberType3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowedMemberType4
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowedMemberType4");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAllowedMemberType4.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render AllowedMemberType5
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("AllowedMemberType5");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltAllowedMemberType5.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		#endregion

		#region Render Denied Folder

		private void renderDeniedFolder(HtmlTextWriter output)
		{
			//
			// Render Denied Folder
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Denied");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DeniedMemberType1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DeniedMemberType1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDeniedMemberType1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DeniedMemberType2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DeniedMemberType2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDeniedMemberType2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DeniedMemberType3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DeniedMemberType3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDeniedMemberType3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DeniedMemberType4
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DeniedMemberType4");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDeniedMemberType4.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render DeniedMemberType5
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("DeniedMemberType5");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.Write(HtmlTextWriter.TagRightChar);
			ltDeniedMemberType5.RenderControl(output);
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
					dojoAccessControlGroupID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoAccessControlGroupID;
			return myState;
		}
	}
}
