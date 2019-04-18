using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoSeminarRegistrationOption.
	/// </summary>
	[ToolboxData("<{0}:DojoSeminarRegistrationOptionEditor runat=server></{0}:DojoSeminarRegistrationOptionEditor>")]
	public class DojoSeminarRegistrationOptionEditor : TableWindow, INamingContainer
	{
		private int dojoSeminarRegistrationOptionID;
		private DojoSeminarRegistrationOption obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for Default Folder

		private TextBox tbQuantity = new TextBox();
		private RegularExpressionValidator revQuantity = new RegularExpressionValidator();
		private TextBox tbTotalFee = new TextBox();
		private TextBox tbCostPerItem = new TextBox();
		private MultiSelectBox msParentOption = new MultiSelectBox();
		private MultiSelectBox msParentRegistration = new MultiSelectBox();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoSeminarRegistrationOptionID
		{
			get
			{
				return dojoSeminarRegistrationOptionID;
			}
			set
			{
				loadFlag = true;
				dojoSeminarRegistrationOptionID = value;
			}
		}

		[Bindable(true), Category("Behavior"), DefaultValue(false)]
		public bool ResetOnAdd
		{
			get
			{
				return resetOnAdd;
			}
			set
			{
				resetOnAdd = value;
			}
		}

		[Bindable(true), Category("Behavior"), DefaultValue(false)]
		public bool EditOnAdd
		{
			get
			{
				return editOnAdd;
			}
			set
			{
				editOnAdd = value;
			}
		}

		#endregion

		protected override void CreateChildControls()
		{
			Controls.Clear();
			bindDropDownLists();

			#region Child Controls for Default Folder

			tbQuantity.ID = this.ID + "_Quantity";
			tbQuantity.EnableViewState = false;
			Controls.Add(tbQuantity);
			revQuantity.ControlToValidate = tbQuantity.ID;
			revQuantity.ValidationExpression = "^(\\+|-)?\\d+$";
			revQuantity.ErrorMessage = "*";
			revQuantity.Display = ValidatorDisplay.Dynamic;
			revQuantity.EnableViewState = false;
			Controls.Add(revQuantity);

			tbTotalFee.EnableViewState = false;
			Controls.Add(tbTotalFee);

			tbCostPerItem.EnableViewState = false;
			Controls.Add(tbCostPerItem);

			msParentOption.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msParentOption);

			msParentRegistration.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msParentRegistration);

			#endregion

			btOk.Text = "OK";
			btOk.Width = Unit.Pixel(72);
			btOk.EnableViewState = false;
			btOk.Click += new EventHandler(ok_Click);
			Controls.Add(btOk);

			btCancel.Text = "Cancel";
			btCancel.Width = Unit.Pixel(72);
			btCancel.EnableViewState = false;
			btCancel.CausesValidation = false;
			btCancel.Click += new EventHandler(cancel_Click);
			Controls.Add(btCancel);

			btDelete.Text = "Delete";
			btDelete.Width = Unit.Pixel(72);
			btDelete.EnableViewState = false;
			btDelete.Click += new EventHandler(delete_Click);
			Controls.Add(btDelete);

			ChildControlsCreated = true;
		}

		private void bindDropDownLists()
		{
			#region Bind Default Child Data

			msParentOption.Items.Add(new ListItem("Null", "Null"));
			DojoSeminarOptionManager parentOptionManager = new DojoSeminarOptionManager();
			DojoSeminarOptionCollection parentOptionCollection = parentOptionManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoSeminarOption parentOption in parentOptionCollection)
			{
				ListItem i = new ListItem(parentOption.ToString(), parentOption.ID.ToString());
				msParentOption.Items.Add(i);
			}

			msParentRegistration.Items.Add(new ListItem("Null", "Null"));
			DojoSeminarRegistrationManager parentRegistrationManager = new DojoSeminarRegistrationManager();
			DojoSeminarRegistrationCollection parentRegistrationCollection = parentRegistrationManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoSeminarRegistration parentRegistration in parentRegistrationCollection)
			{
				ListItem i = new ListItem(parentRegistration.ToString(), parentRegistration.ID.ToString());
				msParentRegistration.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoSeminarRegistrationOptionID == 0)
				obj = new DojoSeminarRegistrationOption();
			else
				obj = new DojoSeminarRegistrationOption(dojoSeminarRegistrationOptionID);

			obj.Quantity = int.Parse(tbQuantity.Text);
			obj.TotalFee = decimal.Parse(tbTotalFee.Text);
			obj.CostPerItem = decimal.Parse(tbCostPerItem.Text);

			if(msParentOption.SelectedItem != null && msParentOption.SelectedItem.Value != "Null")
				obj.ParentOption = DojoSeminarOption.NewPlaceHolder(
					int.Parse(msParentOption.SelectedItem.Value));
			else
				obj.ParentOption = null;

			if(msParentRegistration.SelectedItem != null && msParentRegistration.SelectedItem.Value != "Null")
				obj.ParentRegistration = DojoSeminarRegistration.NewPlaceHolder(
					int.Parse(msParentRegistration.SelectedItem.Value));
			else
				obj.ParentRegistration = null;

			if(editOnAdd)
				dojoSeminarRegistrationOptionID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbQuantity.Text = string.Empty;
				tbTotalFee.Text = string.Empty;
				tbCostPerItem.Text = string.Empty;
				msParentOption.SelectedIndex = 0;
				msParentRegistration.SelectedIndex = 0;
			}

			OnUpdated(EventArgs.Empty);
		}

		#endregion

		protected void cancel_Click(object sender, EventArgs e)
		{
			this.OnCancelled(EventArgs.Empty);
		}

		protected void delete_Click(object sender, EventArgs e)
		{
			this.OnDeleteClicked(EventArgs.Empty);
		}

		public event EventHandler Cancelled;
		protected virtual void OnCancelled(EventArgs e)
		{
			if(Cancelled != null)
				Cancelled(this, e);
		}

		public event EventHandler Updated;
		protected virtual void OnUpdated(EventArgs e)
		{
			if(Updated != null)
				Updated(this, e);
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
			features = TableWindowFeatures.DisableContentSeparation;
			components = TableWindowComponents.Tabs;
			tabStrip = new TabStrip();
			tabStrip.Tabs = new TabList();

			Tab DefaultTab = new Tab("Default");
			DefaultTab.Visible = true;
			DefaultTab.RenderDiv += new TabRenderHandler(renderDefaultFolder);
			DefaultTab.Visible = true;
			tabStrip.Tabs.Add(DefaultTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoSeminarRegistrationOptionID > 0)
				{
					obj = new DojoSeminarRegistrationOption(dojoSeminarRegistrationOptionID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoSeminarRegistrationOptionID <= 0)
				{
					obj = new DojoSeminarRegistrationOption();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbQuantity.Text = obj.Quantity.ToString();
				tbTotalFee.Text = obj.TotalFee.ToString();
				tbCostPerItem.Text = obj.CostPerItem.ToString();

				//
				// Set Children Selections
				//
				if(obj.ParentOption != null)
					foreach(ListItem item in msParentOption.Items)
						item.Selected = obj.ParentOption.ID.ToString() == item.Value;
					else
						msParentOption.SelectedIndex = 0;

				if(obj.ParentRegistration != null)
					foreach(ListItem item in msParentRegistration.Items)
						item.Selected = obj.ParentRegistration.ID.ToString() == item.Value;
					else
						msParentRegistration.SelectedIndex = 0;

			}
		}

		protected override void RenderContent(HtmlTextWriter output)
		{
			RenderTabPanels(output);
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
			output.Write("&nbsp;");
			btCancel.RenderControl(output);
			if(DeleteClicked != null)
			{
				output.Write(" ");
				btDelete.RenderControl(output);
			}
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
		}

		private void renderDefaultFolder(HtmlTextWriter output)
		{
			//
			// Render Quantity
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Quantity");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbQuantity.RenderControl(output);
			revQuantity.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render TotalFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("TotalFee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbTotalFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render CostPerItem
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("CostPerItem");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbCostPerItem.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentOption
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentOption");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msParentOption.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ParentRegistration
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ParentRegistration");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msParentRegistration.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		protected override void LoadViewState(object savedState)
		{
			if(savedState != null)
			{
				object[] myState = (object[]) savedState;
				if(myState[0] != null)
					base.LoadViewState(myState[0]);
				if(myState[1] != null)
					dojoSeminarRegistrationOptionID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoSeminarRegistrationOptionID;
			return myState;
		}
	}
}

