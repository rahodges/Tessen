using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.Rappahanock;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoSeminarOption.
	/// </summary>
	[ToolboxData("<{0}:DojoSeminarOptionEditor runat=server></{0}:DojoSeminarOptionEditor>")]
	public class DojoSeminarOptionEditor : TableWindow, INamingContainer
	{
		private int dojoSeminarOptionID;
		private DojoSeminarOption obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for Default Folder

		private TextBox tbName = new TextBox();
		private TextBox tbDescription = new TextBox();
		private TextBox tbFee = new TextBox();
		private TextBox tbMaxQuantity = new TextBox();
		private RegularExpressionValidator revMaxQuantity = new RegularExpressionValidator();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for Rappahanock Folder

		private MultiSelectBox msItem = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoSeminarOptionID
		{
			get
			{
				return dojoSeminarOptionID;
			}
			set
			{
				loadFlag = true;
				dojoSeminarOptionID = value;
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

			tbName.EnableViewState = false;
			Controls.Add(tbName);

			tbDescription.EnableViewState = false;
			Controls.Add(tbDescription);

			tbFee.EnableViewState = false;
			Controls.Add(tbFee);

			tbMaxQuantity.ID = this.ID + "_MaxQuantity";
			tbMaxQuantity.EnableViewState = false;
			Controls.Add(tbMaxQuantity);
			revMaxQuantity.ControlToValidate = tbMaxQuantity.ID;
			revMaxQuantity.ValidationExpression = "^(\\+|-)?\\d+$";
			revMaxQuantity.ErrorMessage = "*";
			revMaxQuantity.Display = ValidatorDisplay.Dynamic;
			revMaxQuantity.EnableViewState = false;
			Controls.Add(revMaxQuantity);

			#endregion

			#region Child Controls for Rappahanock Folder

			msItem.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msItem);

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
			#region Bind Rappahanock Child Data

			msItem.Items.Add(new ListItem("Null", "Null"));
			RHItemManager itemManager = new RHItemManager();
			RHItemCollection itemCollection = itemManager.GetCollection(string.Empty, string.Empty, null);
			foreach(RHItem item in itemCollection)
			{
				ListItem i = new ListItem(item.ToString(), item.ID.ToString());
				msItem.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoSeminarOptionID == 0)
				obj = new DojoSeminarOption();
			else
				obj = new DojoSeminarOption(dojoSeminarOptionID);

			obj.Name = tbName.Text;
			obj.Description = tbDescription.Text;
			obj.Fee = decimal.Parse(tbFee.Text);
			obj.MaxQuantity = int.Parse(tbMaxQuantity.Text);

			if(msItem.SelectedItem != null && msItem.SelectedItem.Value != "Null")
				obj.Item = RHItem.NewPlaceHolder(
					int.Parse(msItem.SelectedItem.Value));
			else
				obj.Item = null;

			if(editOnAdd)
				dojoSeminarOptionID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbDescription.Text = string.Empty;
				tbFee.Text = string.Empty;
				tbMaxQuantity.Text = string.Empty;
				msItem.SelectedIndex = 0;
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

			Tab RappahanockTab = new Tab("Rappahanock");
			RappahanockTab.RenderDiv += new TabRenderHandler(renderRappahanockFolder);
			tabStrip.Tabs.Add(RappahanockTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoSeminarOptionID > 0)
				{
					obj = new DojoSeminarOption(dojoSeminarOptionID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoSeminarOptionID <= 0)
				{
					obj = new DojoSeminarOption();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbName.Text = obj.Name;
				tbDescription.Text = obj.Description;
				tbFee.Text = obj.Fee.ToString();
				tbMaxQuantity.Text = obj.MaxQuantity.ToString();

				//
				// Set Children Selections
				//
				if(obj.Item != null)
					foreach(ListItem item in msItem.Items)
						item.Selected = obj.Item.ID.ToString() == item.Value;
					else
						msItem.SelectedIndex = 0;

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
			// Render Name
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Name");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbName.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Description
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Description");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbDescription.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Fee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Fee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render MaxQuantity
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("MaxQuantity");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbMaxQuantity.RenderControl(output);
			revMaxQuantity.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		private void renderRappahanockFolder(HtmlTextWriter output)
		{
			//
			// Render Item
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Item");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msItem.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		protected override void LoadViewState(object savedState)
		{
			if(savedState != null)
			{
				object[] myState = (object[]) savedState;
				if(myState[0] != null)
					base.LoadViewState(myState[0]);
				if(myState[1] != null)
					dojoSeminarOptionID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoSeminarOptionID;
			return myState;
		}
	}
}

