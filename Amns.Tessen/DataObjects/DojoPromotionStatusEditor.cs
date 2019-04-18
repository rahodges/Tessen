using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoPromotionStatus.
	/// </summary>
	[ToolboxData("<{0}:DojoPromotionStatusEditor runat=server></{0}:DojoPromotionStatusEditor>")]
	public class DojoPromotionStatusEditor : TableWindow, INamingContainer
	{
		private int dojoPromotionStatusID;
		private DojoPromotionStatus obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for General Folder

		private TextBox tbName = new TextBox();
		private TextBox tbOrderNum = new TextBox();
		private RegularExpressionValidator revOrderNum = new RegularExpressionValidator();

		#endregion

		#region Private Control Fields for Flags Folder

		private CheckBox cbIsEligible = new CheckBox();
		private CheckBox cbIsPassed = new CheckBox();
		private CheckBox cbIsFailed = new CheckBox();
		private CheckBox cbIsFiled = new CheckBox();
		private CheckBox cbIsApproved = new CheckBox();
		private CheckBox cbIsAwarded = new CheckBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoPromotionStatusID
		{
			get
			{
				return dojoPromotionStatusID;
			}
			set
			{
				loadFlag = true;
				dojoPromotionStatusID = value;
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

			#region Child Controls for General Folder

			tbName.EnableViewState = false;
			Controls.Add(tbName);

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

			#region Child Controls for Flags Folder

			cbIsEligible.EnableViewState = false;
			Controls.Add(cbIsEligible);

			cbIsPassed.EnableViewState = false;
			Controls.Add(cbIsPassed);

			cbIsFailed.EnableViewState = false;
			Controls.Add(cbIsFailed);

			cbIsFiled.EnableViewState = false;
			Controls.Add(cbIsFiled);

			cbIsApproved.EnableViewState = false;
			Controls.Add(cbIsApproved);

			cbIsAwarded.EnableViewState = false;
			Controls.Add(cbIsAwarded);

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
		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoPromotionStatusID == 0)
				obj = new DojoPromotionStatus();
			else
				obj = new DojoPromotionStatus(dojoPromotionStatusID);

			obj.Name = tbName.Text;
			obj.OrderNum = int.Parse(tbOrderNum.Text);
			obj.IsEligible = cbIsEligible.Checked;
			obj.IsPassed = cbIsPassed.Checked;
			obj.IsFailed = cbIsFailed.Checked;
			obj.IsFiled = cbIsFiled.Checked;
			obj.IsApproved = cbIsApproved.Checked;
			obj.IsAwarded = cbIsAwarded.Checked;

			if(editOnAdd)
				dojoPromotionStatusID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbOrderNum.Text = string.Empty;
				cbIsEligible.Checked = false;
				cbIsPassed.Checked = false;
				cbIsFailed.Checked = false;
				cbIsFiled.Checked = false;
				cbIsApproved.Checked = false;
				cbIsAwarded.Checked = false;
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

			Tab GeneralTab = new Tab("General");
			GeneralTab.Visible = true;
			GeneralTab.RenderDiv += new TabRenderHandler(renderGeneralFolder);
			tabStrip.Tabs.Add(GeneralTab);

			Tab FlagsTab = new Tab("Flags");
			FlagsTab.RenderDiv += new TabRenderHandler(renderFlagsFolder);
			tabStrip.Tabs.Add(FlagsTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoPromotionStatusID > 0)
				{
					obj = new DojoPromotionStatus(dojoPromotionStatusID);
					text = "Edit Promotion Status - " + obj.ToString();
				}
				else if(dojoPromotionStatusID <= 0)
				{
					obj = new DojoPromotionStatus();
					text = "Add Promotion Status";
				}

				//
				// Set Field Entries
				//
				tbName.Text = obj.Name;
				tbOrderNum.Text = obj.OrderNum.ToString();
				cbIsEligible.Checked = obj.IsEligible;
				cbIsPassed.Checked = obj.IsPassed;
				cbIsFailed.Checked = obj.IsFailed;
				cbIsFiled.Checked = obj.IsFiled;
				cbIsApproved.Checked = obj.IsApproved;
				cbIsAwarded.Checked = obj.IsAwarded;

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

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		private void renderGeneralFolder(HtmlTextWriter output)
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

		private void renderFlagsFolder(HtmlTextWriter output)
		{
			//
			// Render IsEligible
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsEligible");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsEligible.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsPassed
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsPassed");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsPassed.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsFailed
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsFailed");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsFailed.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsFiled
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsFiled");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsFiled.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsApproved
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsApproved");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsApproved.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render IsAwarded
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("IsAwarded");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			cbIsAwarded.RenderControl(output);
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
					dojoPromotionStatusID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoPromotionStatusID;
			return myState;
		}
	}
}

