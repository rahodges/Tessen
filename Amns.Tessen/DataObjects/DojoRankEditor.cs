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
	/// Default web editor for DojoRank.
	/// </summary>
	[ToolboxData("<{0}:DojoRankEditor runat=server></{0}:DojoRankEditor>")]
	public class DojoRankEditor : TableWindow, INamingContainer
	{
		private int dojoRankID;
		private DojoRank obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for Default Folder

		private TextBox tbName = new TextBox();
		private TextBox tbPromotionTimeInRank = new TextBox();
		private TextBox tbPromotionTimeFromLastTest = new TextBox();
		private TextBox tbPromotionRequirements = new TextBox();
		private TextBox tbPromotionFee = new TextBox();
		private CheckBox cbPromotionResetIP = new CheckBox();
		private TextBox tbOrderNum = new TextBox();
		private RegularExpressionValidator revOrderNum = new RegularExpressionValidator();
		private MultiSelectBox msPromotionRank = new MultiSelectBox();

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
		public int DojoRankID
		{
			get
			{
				return dojoRankID;
			}
			set
			{
				loadFlag = true;
				dojoRankID = value;
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

			tbPromotionTimeInRank.EnableViewState = false;
			Controls.Add(tbPromotionTimeInRank);

			tbPromotionTimeFromLastTest.EnableViewState = false;
			Controls.Add(tbPromotionTimeFromLastTest);

			tbPromotionRequirements.EnableViewState = false;
			Controls.Add(tbPromotionRequirements);

			tbPromotionFee.EnableViewState = false;
			Controls.Add(tbPromotionFee);

			msPromotionRank.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msPromotionRank);

			cbPromotionResetIP.EnableViewState = false;
			Controls.Add(cbPromotionResetIP);

			tbOrderNum.ID = this.ID + "_OrderNum";
			tbOrderNum.EnableViewState = false;
			Controls.Add(tbOrderNum);
			revOrderNum.ControlToValidate = tbOrderNum.ID;
			revOrderNum.ValidationExpression = "^(\\+|-)?\\d+$";
			revOrderNum.ErrorMessage = "*";
			revOrderNum.Display = ValidatorDisplay.Dynamic;
			revOrderNum.EnableViewState = false;
			Controls.Add(revOrderNum);

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
			#region Bind Default Child Data

			msPromotionRank.Items.Add(new ListItem("Null", "Null"));
			DojoRankManager promotionRankManager = new DojoRankManager();
			DojoRankCollection promotionRankCollection = promotionRankManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoRank promotionRank in promotionRankCollection)
			{
				ListItem i = new ListItem(promotionRank.ToString(), promotionRank.ID.ToString());
				msPromotionRank.Items.Add(i);
			}

			#endregion

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
			if(dojoRankID == 0)
				obj = new DojoRank();
			else
				obj = new DojoRank(dojoRankID);

			obj.Name = tbName.Text;
			obj.PromotionTimeInRank = TimeSpan.Parse(tbPromotionTimeInRank.Text);
			obj.PromotionTimeFromLastTest = TimeSpan.Parse(tbPromotionTimeFromLastTest.Text);
			obj.PromotionRequirements = tbPromotionRequirements.Text;
			obj.PromotionFee = decimal.Parse(tbPromotionFee.Text);
			obj.PromotionResetIP = cbPromotionResetIP.Checked;
			obj.OrderNum = int.Parse(tbOrderNum.Text);

			if(msPromotionRank.SelectedItem != null && msPromotionRank.SelectedItem.Value != "Null")
				obj.PromotionRank = DojoRank.NewPlaceHolder(
					int.Parse(msPromotionRank.SelectedItem.Value));
			else
				obj.PromotionRank = null;

			if(msItem.SelectedItem != null && msItem.SelectedItem.Value != "Null")
				obj.Item = RHItem.NewPlaceHolder(
					int.Parse(msItem.SelectedItem.Value));
			else
				obj.Item = null;

			if(editOnAdd)
				dojoRankID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbPromotionTimeInRank.Text = string.Empty;
				tbPromotionTimeFromLastTest.Text = string.Empty;
				tbPromotionRequirements.Text = string.Empty;
				tbPromotionFee.Text = string.Empty;
				cbPromotionResetIP.Checked = false;
				tbOrderNum.Text = string.Empty;
				msPromotionRank.SelectedIndex = 0;
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
				if(dojoRankID > 0)
				{
					obj = new DojoRank(dojoRankID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoRankID <= 0)
				{
					obj = new DojoRank();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbName.Text = obj.Name;
				tbPromotionTimeInRank.Text = obj.PromotionTimeInRank.ToString();
				tbPromotionTimeFromLastTest.Text = obj.PromotionTimeFromLastTest.ToString();
				tbPromotionRequirements.Text = obj.PromotionRequirements;
				tbPromotionFee.Text = obj.PromotionFee.ToString();
				cbPromotionResetIP.Checked = obj.PromotionResetIP;
				tbOrderNum.Text = obj.OrderNum.ToString();

				//
				// Set Children Selections
				//
				if(obj.PromotionRank != null)
					foreach(ListItem item in msPromotionRank.Items)
						item.Selected = obj.PromotionRank.ID.ToString() == item.Value;
					else
						msPromotionRank.SelectedIndex = 0;

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
			// Render PromotionTimeInRank
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Promotion Time In Rank");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbPromotionTimeInRank.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PromotionTimeFromLastTest
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Promotion Time From Last Test");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbPromotionTimeFromLastTest.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PromotionRequirements
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Promotion Requirements");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbPromotionRequirements.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PromotionFee
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Promotion Fee");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbPromotionFee.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PromotionRank
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PromotionRank");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msPromotionRank.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PromotionResetIP
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PromotionResetIP");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbPromotionResetIP.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render OrderNum
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("OrderNum");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbOrderNum.RenderControl(output);
			revOrderNum.RenderControl(output);
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
					dojoRankID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoRankID;
			return myState;
		}
	}
}

