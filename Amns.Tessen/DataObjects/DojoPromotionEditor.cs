using System;
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Default web editor for DojoPromotion.
	/// </summary>
	[ToolboxData("<{0}:DojoPromotionEditor runat=server></{0}:DojoPromotionEditor>")]
	public class DojoPromotionEditor : TableWindow, INamingContainer
	{
		private int dojoPromotionID;
		private DojoPromotion obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for Default Folder

		private TextBox tbPromotionDate = new TextBox();
		private MultiSelectBox msMember = new MultiSelectBox();
		private MultiSelectBox msTest = new MultiSelectBox();
		private MultiSelectBox msPromotionRank = new MultiSelectBox();
		private MultiSelectBox msLastRank = new MultiSelectBox();
		private MultiSelectBox msStatus = new MultiSelectBox();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
		private Button btDelete = new Button();

		#region Public Control Properties

		[Bindable(true), Category("Data"), DefaultValue(0)]
		public int DojoPromotionID
		{
			get
			{
				return dojoPromotionID;
			}
			set
			{
				loadFlag = true;
				dojoPromotionID = value;
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

			msMember.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msMember);

			msTest.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msTest);

			tbPromotionDate.EnableViewState = false;
			Controls.Add(tbPromotionDate);

			msPromotionRank.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msPromotionRank);

			msLastRank.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msLastRank);

			msStatus.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msStatus);

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

			msMember.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager memberManager = new DojoMemberManager();
			DojoMemberCollection memberCollection = memberManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember member in memberCollection)
			{
				ListItem i = new ListItem(member.ToString(), member.ID.ToString());
				msMember.Items.Add(i);
			}

			msTest.Items.Add(new ListItem("Null", "Null"));
			DojoTestManager testManager = new DojoTestManager();
			DojoTestCollection testCollection = testManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTest test in testCollection)
			{
				ListItem i = new ListItem(test.ToString(), test.ID.ToString());
				msTest.Items.Add(i);
			}

			msPromotionRank.Items.Add(new ListItem("Null", "Null"));
			DojoRankManager promotionRankManager = new DojoRankManager();
			DojoRankCollection promotionRankCollection = promotionRankManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoRank promotionRank in promotionRankCollection)
			{
				ListItem i = new ListItem(promotionRank.ToString(), promotionRank.ID.ToString());
				msPromotionRank.Items.Add(i);
			}

			msLastRank.Items.Add(new ListItem("Null", "Null"));
			DojoRankManager lastRankManager = new DojoRankManager();
			DojoRankCollection lastRankCollection = lastRankManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoRank lastRank in lastRankCollection)
			{
				ListItem i = new ListItem(lastRank.ToString(), lastRank.ID.ToString());
				msLastRank.Items.Add(i);
			}

			msStatus.Items.Add(new ListItem("Null", "Null"));
			DojoPromotionStatusManager statusManager = new DojoPromotionStatusManager();
			DojoPromotionStatusCollection statusCollection = statusManager.GetCollection(string.Empty, string.Empty);
			foreach(DojoPromotionStatus status in statusCollection)
			{
				ListItem i = new ListItem(status.ToString(), status.ID.ToString());
				msStatus.Items.Add(i);
			}

			#endregion

		}

		#region ok_Click Save and Update Method

		protected void ok_Click(object sender, EventArgs e)
		{
			if(dojoPromotionID == 0)
				obj = new DojoPromotion();
			else
				obj = new DojoPromotion(dojoPromotionID);

			obj.PromotionDate = DateTime.Parse(tbPromotionDate.Text);

			if(msMember.SelectedItem != null && msMember.SelectedItem.Value != "Null")
				obj.Member = DojoMember.NewPlaceHolder(
					int.Parse(msMember.SelectedItem.Value));
			else
				obj.Member = null;

			if(msTest.SelectedItem != null && msTest.SelectedItem.Value != "Null")
				obj.Test = DojoTest.NewPlaceHolder(
					int.Parse(msTest.SelectedItem.Value));
			else
				obj.Test = null;

			if(msPromotionRank.SelectedItem != null && msPromotionRank.SelectedItem.Value != "Null")
				obj.PromotionRank = DojoRank.NewPlaceHolder(
					int.Parse(msPromotionRank.SelectedItem.Value));
			else
				obj.PromotionRank = null;

			if(msLastRank.SelectedItem != null && msLastRank.SelectedItem.Value != "Null")
				obj.LastRank = DojoRank.NewPlaceHolder(
					int.Parse(msLastRank.SelectedItem.Value));
			else
				obj.LastRank = null;

			if(msStatus.SelectedItem != null && msStatus.SelectedItem.Value != "Null")
				obj.Status = DojoPromotionStatus.NewPlaceHolder(
					int.Parse(msStatus.SelectedItem.Value));
			else
				obj.Status = null;

			if(editOnAdd)
				dojoPromotionID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbPromotionDate.Text = DateTime.Now.ToString();
				msMember.SelectedIndex = 0;
				msTest.SelectedIndex = 0;
				msPromotionRank.SelectedIndex = 0;
				msLastRank.SelectedIndex = 0;
				msStatus.SelectedIndex = 0;
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
				if(dojoPromotionID > 0)
				{
					obj = new DojoPromotion(dojoPromotionID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoPromotionID <= 0)
				{
					obj = new DojoPromotion();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbPromotionDate.Text = obj.PromotionDate.ToString();

				//
				// Set Children Selections
				//
				if(obj.Member != null)
					foreach(ListItem item in msMember.Items)
						item.Selected = obj.Member.ID.ToString() == item.Value;
					else
						msMember.SelectedIndex = 0;

				if(obj.Test != null)
					foreach(ListItem item in msTest.Items)
						item.Selected = obj.Test.ID.ToString() == item.Value;
					else
						msTest.SelectedIndex = 0;

				if(obj.PromotionRank != null)
					foreach(ListItem item in msPromotionRank.Items)
						item.Selected = obj.PromotionRank.ID.ToString() == item.Value;
					else
						msPromotionRank.SelectedIndex = 0;

				if(obj.LastRank != null)
					foreach(ListItem item in msLastRank.Items)
						item.Selected = obj.LastRank.ID.ToString() == item.Value;
					else
						msLastRank.SelectedIndex = 0;

				if(obj.Status != null)
					foreach(ListItem item in msStatus.Items)
						item.Selected = obj.Status.ID.ToString() == item.Value;
					else
						msStatus.SelectedIndex = 0;

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
			// Render Member
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Member");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msMember.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Test
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Test");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msTest.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PromotionDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Promotion Date");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbPromotionDate.RenderControl(output);
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
			// Render LastRank
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("LastRank");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msLastRank.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Status
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Status");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msStatus.RenderControl(output);
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
					dojoPromotionID = (int) myState[1];
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = dojoPromotionID;
			return myState;
		}
	}
}

