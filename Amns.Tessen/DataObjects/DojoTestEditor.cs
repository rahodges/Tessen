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
	[ToolboxData("<{0}:DojoTestEditor runat=server></{0}:DojoTestEditor>")]
	public class DojoTestEditor : TableWindow, INamingContainer
	{
		private int dojoTestID;
		private DojoTest obj;
		private bool loadFlag = false;
		private bool resetOnAdd;
		private bool editOnAdd;

		#region Private Control Fields for Default Folder

		private TextBox tbName = new TextBox();
		private TextBox tbDescription = new TextBox();
		private TextBox tbTestDate = new TextBox();
		private MultiSelectBox msLocation = new MultiSelectBox();

		#endregion

		#region Private Control Fields for _system Folder


		#endregion

		#region Private Control Fields for List_Generator Folder

		private MultiSelectBox msListMemberType1 = new MultiSelectBox();
		private MultiSelectBox msListMemberType2 = new MultiSelectBox();
		private MultiSelectBox msListMemberType3 = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Administration Folder

		private MultiSelectBox msPanelChief = new MultiSelectBox();
		private MultiSelectBox msPanelMember1 = new MultiSelectBox();
		private MultiSelectBox msPanelMember2 = new MultiSelectBox();
		private MultiSelectBox msPanelMember3 = new MultiSelectBox();
		private MultiSelectBox msPanelMember4 = new MultiSelectBox();
		private MultiSelectBox msPanelMember5 = new MultiSelectBox();

		#endregion

		#region Private Control Fields for System Folder

		private MultiSelectBox msStatus = new MultiSelectBox();
		private MultiSelectBox msActiveTestList = new MultiSelectBox();

		#endregion

		#region Private Control Fields for Rappahanock Folder

		private MultiSelectBox msItem = new MultiSelectBox();

		#endregion

		private Button btOk = new Button();
		private Button btCancel = new Button();
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
				loadFlag = true;
				dojoTestID = value;
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

			tbTestDate.EnableViewState = false;
			Controls.Add(tbTestDate);

			msLocation.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msLocation);

			#endregion

			#region Child Controls for List Generator Folder

			msListMemberType1.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msListMemberType1);

			msListMemberType2.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msListMemberType2);

			msListMemberType3.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msListMemberType3);

			#endregion

			#region Child Controls for Administration Folder

			msPanelChief.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msPanelChief);

			msPanelMember1.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msPanelMember1);

			msPanelMember2.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msPanelMember2);

			msPanelMember3.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msPanelMember3);

			msPanelMember4.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msPanelMember4);

			msPanelMember5.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msPanelMember5);

			#endregion

			#region Child Controls for System Folder

			msStatus.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msStatus);

			msActiveTestList.Mode = MultiSelectBoxMode.DropDownList;
			Controls.Add(msActiveTestList);

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

			msLocation.Items.Add(new ListItem("Null", "Null"));
			GreyFoxContactManager locationManager = new GreyFoxContactManager("kitTessen_Locations");
			GreyFoxContactCollection locationCollection = locationManager.GetCollection(string.Empty, string.Empty);
			foreach(GreyFoxContact location in locationCollection)
			{
				ListItem i = new ListItem(location.ToString(), location.ID.ToString());
				msLocation.Items.Add(i);
			}

			#endregion

			#region Bind List Generator Child Data

			msListMemberType1.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager listMemberType1Manager = new DojoMemberTypeManager();
			DojoMemberTypeCollection listMemberType1Collection = listMemberType1Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType listMemberType1 in listMemberType1Collection)
			{
				ListItem i = new ListItem(listMemberType1.ToString(), listMemberType1.ID.ToString());
				msListMemberType1.Items.Add(i);
			}

			msListMemberType2.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager listMemberType2Manager = new DojoMemberTypeManager();
			DojoMemberTypeCollection listMemberType2Collection = listMemberType2Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType listMemberType2 in listMemberType2Collection)
			{
				ListItem i = new ListItem(listMemberType2.ToString(), listMemberType2.ID.ToString());
				msListMemberType2.Items.Add(i);
			}

			msListMemberType3.Items.Add(new ListItem("Null", "Null"));
			DojoMemberTypeManager listMemberType3Manager = new DojoMemberTypeManager();
			DojoMemberTypeCollection listMemberType3Collection = listMemberType3Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMemberType listMemberType3 in listMemberType3Collection)
			{
				ListItem i = new ListItem(listMemberType3.ToString(), listMemberType3.ID.ToString());
				msListMemberType3.Items.Add(i);
			}

			#endregion

			#region Bind Administration Child Data

			msPanelChief.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager panelChiefManager = new DojoMemberManager();
			DojoMemberCollection panelChiefCollection = panelChiefManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember panelChief in panelChiefCollection)
			{
				ListItem i = new ListItem(panelChief.ToString(), panelChief.ID.ToString());
				msPanelChief.Items.Add(i);
			}

			msPanelMember1.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager panelMember1Manager = new DojoMemberManager();
			DojoMemberCollection panelMember1Collection = panelMember1Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember panelMember1 in panelMember1Collection)
			{
				ListItem i = new ListItem(panelMember1.ToString(), panelMember1.ID.ToString());
				msPanelMember1.Items.Add(i);
			}

			msPanelMember2.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager panelMember2Manager = new DojoMemberManager();
			DojoMemberCollection panelMember2Collection = panelMember2Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember panelMember2 in panelMember2Collection)
			{
				ListItem i = new ListItem(panelMember2.ToString(), panelMember2.ID.ToString());
				msPanelMember2.Items.Add(i);
			}

			msPanelMember3.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager panelMember3Manager = new DojoMemberManager();
			DojoMemberCollection panelMember3Collection = panelMember3Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember panelMember3 in panelMember3Collection)
			{
				ListItem i = new ListItem(panelMember3.ToString(), panelMember3.ID.ToString());
				msPanelMember3.Items.Add(i);
			}

			msPanelMember4.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager panelMember4Manager = new DojoMemberManager();
			DojoMemberCollection panelMember4Collection = panelMember4Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember panelMember4 in panelMember4Collection)
			{
				ListItem i = new ListItem(panelMember4.ToString(), panelMember4.ID.ToString());
				msPanelMember4.Items.Add(i);
			}

			msPanelMember5.Items.Add(new ListItem("Null", "Null"));
			DojoMemberManager panelMember5Manager = new DojoMemberManager();
			DojoMemberCollection panelMember5Collection = panelMember5Manager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoMember panelMember5 in panelMember5Collection)
			{
				ListItem i = new ListItem(panelMember5.ToString(), panelMember5.ID.ToString());
				msPanelMember5.Items.Add(i);
			}

			#endregion

			#region Bind System Child Data

			msStatus.Items.Add(new ListItem("Null", "Null"));
			DojoTestListStatusManager statusManager = new DojoTestListStatusManager();
			DojoTestListStatusCollection statusCollection = statusManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestListStatus status in statusCollection)
			{
				ListItem i = new ListItem(status.ToString(), status.ID.ToString());
				msStatus.Items.Add(i);
			}

			msActiveTestList.Items.Add(new ListItem("Null", "Null"));
			DojoTestListManager activeTestListManager = new DojoTestListManager();
			DojoTestListCollection activeTestListCollection = activeTestListManager.GetCollection(string.Empty, string.Empty, null);
			foreach(DojoTestList activeTestList in activeTestListCollection)
			{
				ListItem i = new ListItem(activeTestList.ToString(), activeTestList.ID.ToString());
				msActiveTestList.Items.Add(i);
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
			if(dojoTestID == 0)
				obj = new DojoTest();
			else
				obj = new DojoTest(dojoTestID);

			obj.Name = tbName.Text;
			obj.Description = tbDescription.Text;
			obj.TestDate = DateTime.Parse(tbTestDate.Text);

			if(msLocation.SelectedItem != null && msLocation.SelectedItem.Value != "Null")
				obj.Location = GreyFoxContact.NewPlaceHolder("kitTessen_Locations", 
					int.Parse(msLocation.SelectedItem.Value));
			else
				obj.Location = null;

			if(msListMemberType1.SelectedItem != null && msListMemberType1.SelectedItem.Value != "Null")
				obj.ListMemberType1 = DojoMemberType.NewPlaceHolder(
					int.Parse(msListMemberType1.SelectedItem.Value));
			else
				obj.ListMemberType1 = null;

			if(msListMemberType2.SelectedItem != null && msListMemberType2.SelectedItem.Value != "Null")
				obj.ListMemberType2 = DojoMemberType.NewPlaceHolder(
					int.Parse(msListMemberType2.SelectedItem.Value));
			else
				obj.ListMemberType2 = null;

			if(msListMemberType3.SelectedItem != null && msListMemberType3.SelectedItem.Value != "Null")
				obj.ListMemberType3 = DojoMemberType.NewPlaceHolder(
					int.Parse(msListMemberType3.SelectedItem.Value));
			else
				obj.ListMemberType3 = null;

			if(msPanelChief.SelectedItem != null && msPanelChief.SelectedItem.Value != "Null")
				obj.PanelChief = DojoMember.NewPlaceHolder(
					int.Parse(msPanelChief.SelectedItem.Value));
			else
				obj.PanelChief = null;

			if(msPanelMember1.SelectedItem != null && msPanelMember1.SelectedItem.Value != "Null")
				obj.PanelMember1 = DojoMember.NewPlaceHolder(
					int.Parse(msPanelMember1.SelectedItem.Value));
			else
				obj.PanelMember1 = null;

			if(msPanelMember2.SelectedItem != null && msPanelMember2.SelectedItem.Value != "Null")
				obj.PanelMember2 = DojoMember.NewPlaceHolder(
					int.Parse(msPanelMember2.SelectedItem.Value));
			else
				obj.PanelMember2 = null;

			if(msPanelMember3.SelectedItem != null && msPanelMember3.SelectedItem.Value != "Null")
				obj.PanelMember3 = DojoMember.NewPlaceHolder(
					int.Parse(msPanelMember3.SelectedItem.Value));
			else
				obj.PanelMember3 = null;

			if(msPanelMember4.SelectedItem != null && msPanelMember4.SelectedItem.Value != "Null")
				obj.PanelMember4 = DojoMember.NewPlaceHolder(
					int.Parse(msPanelMember4.SelectedItem.Value));
			else
				obj.PanelMember4 = null;

			if(msPanelMember5.SelectedItem != null && msPanelMember5.SelectedItem.Value != "Null")
				obj.PanelMember5 = DojoMember.NewPlaceHolder(
					int.Parse(msPanelMember5.SelectedItem.Value));
			else
				obj.PanelMember5 = null;

			if(msStatus.SelectedItem != null && msStatus.SelectedItem.Value != "Null")
				obj.Status = DojoTestListStatus.NewPlaceHolder(
					int.Parse(msStatus.SelectedItem.Value));
			else
				obj.Status = null;

			if(msActiveTestList.SelectedItem != null && msActiveTestList.SelectedItem.Value != "Null")
				obj.ActiveTestList = DojoTestList.NewPlaceHolder(
					int.Parse(msActiveTestList.SelectedItem.Value));
			else
				obj.ActiveTestList = null;

			if(msItem.SelectedItem != null && msItem.SelectedItem.Value != "Null")
				obj.Item = RHItem.NewPlaceHolder(
					int.Parse(msItem.SelectedItem.Value));
			else
				obj.Item = null;

			if(editOnAdd)
				dojoTestID = obj.Save();
			else
				obj.Save();

			if(resetOnAdd)
			{
				tbName.Text = string.Empty;
				tbDescription.Text = string.Empty;
				tbTestDate.Text = DateTime.Now.ToString();
				msLocation.SelectedIndex = 0;
				msListMemberType1.SelectedIndex = 0;
				msListMemberType2.SelectedIndex = 0;
				msListMemberType3.SelectedIndex = 0;
				msPanelChief.SelectedIndex = 0;
				msPanelMember1.SelectedIndex = 0;
				msPanelMember2.SelectedIndex = 0;
				msPanelMember3.SelectedIndex = 0;
				msPanelMember4.SelectedIndex = 0;
				msPanelMember5.SelectedIndex = 0;
				msStatus.SelectedIndex = 0;
				msActiveTestList.SelectedIndex = 0;
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

			Tab List_GeneratorTab = new Tab("List Generator");
			List_GeneratorTab.RenderDiv += new TabRenderHandler(renderList_GeneratorFolder);
			tabStrip.Tabs.Add(List_GeneratorTab);

			Tab AdministrationTab = new Tab("Administration");
			AdministrationTab.RenderDiv += new TabRenderHandler(renderAdministrationFolder);
			tabStrip.Tabs.Add(AdministrationTab);

			Tab SystemTab = new Tab("System");
			SystemTab.RenderDiv += new TabRenderHandler(renderSystemFolder);
			tabStrip.Tabs.Add(SystemTab);

			Tab RappahanockTab = new Tab("Rappahanock");
			RappahanockTab.RenderDiv += new TabRenderHandler(renderRappahanockFolder);
			tabStrip.Tabs.Add(RappahanockTab);

		}

		protected override void OnPreRender(EventArgs e)
		{
			if(loadFlag)
			{
				if(dojoTestID > 0)
				{
					obj = new DojoTest(dojoTestID);
					text = "Edit  - " + obj.ToString();
				}
				else if(dojoTestID <= 0)
				{
					obj = new DojoTest();
					text = "Add ";
				}

				//
				// Set Field Entries
				//
				tbName.Text = obj.Name;
				tbDescription.Text = obj.Description;
				tbTestDate.Text = obj.TestDate.ToString();

				//
				// Set Children Selections
				//
				if(obj.Location != null)
					foreach(ListItem item in msLocation.Items)
						item.Selected = obj.Location.ID.ToString() == item.Value;
					else
						msLocation.SelectedIndex = 0;

				if(obj.ListMemberType1 != null)
					foreach(ListItem item in msListMemberType1.Items)
						item.Selected = obj.ListMemberType1.ID.ToString() == item.Value;
					else
						msListMemberType1.SelectedIndex = 0;

				if(obj.ListMemberType2 != null)
					foreach(ListItem item in msListMemberType2.Items)
						item.Selected = obj.ListMemberType2.ID.ToString() == item.Value;
					else
						msListMemberType2.SelectedIndex = 0;

				if(obj.ListMemberType3 != null)
					foreach(ListItem item in msListMemberType3.Items)
						item.Selected = obj.ListMemberType3.ID.ToString() == item.Value;
					else
						msListMemberType3.SelectedIndex = 0;

				if(obj.PanelChief != null)
					foreach(ListItem item in msPanelChief.Items)
						item.Selected = obj.PanelChief.ID.ToString() == item.Value;
					else
						msPanelChief.SelectedIndex = 0;

				if(obj.PanelMember1 != null)
					foreach(ListItem item in msPanelMember1.Items)
						item.Selected = obj.PanelMember1.ID.ToString() == item.Value;
					else
						msPanelMember1.SelectedIndex = 0;

				if(obj.PanelMember2 != null)
					foreach(ListItem item in msPanelMember2.Items)
						item.Selected = obj.PanelMember2.ID.ToString() == item.Value;
					else
						msPanelMember2.SelectedIndex = 0;

				if(obj.PanelMember3 != null)
					foreach(ListItem item in msPanelMember3.Items)
						item.Selected = obj.PanelMember3.ID.ToString() == item.Value;
					else
						msPanelMember3.SelectedIndex = 0;

				if(obj.PanelMember4 != null)
					foreach(ListItem item in msPanelMember4.Items)
						item.Selected = obj.PanelMember4.ID.ToString() == item.Value;
					else
						msPanelMember4.SelectedIndex = 0;

				if(obj.PanelMember5 != null)
					foreach(ListItem item in msPanelMember5.Items)
						item.Selected = obj.PanelMember5.ID.ToString() == item.Value;
					else
						msPanelMember5.SelectedIndex = 0;

				if(obj.Status != null)
					foreach(ListItem item in msStatus.Items)
						item.Selected = obj.Status.ID.ToString() == item.Value;
					else
						msStatus.SelectedIndex = 0;

				if(obj.ActiveTestList != null)
					foreach(ListItem item in msActiveTestList.Items)
						item.Selected = obj.ActiveTestList.ID.ToString() == item.Value;
					else
						msActiveTestList.SelectedIndex = 0;

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
			// Render TestDate
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Test Date");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			tbTestDate.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render Location
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Location");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msLocation.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void render_systemFolder(HtmlTextWriter output)
		{
		}

		private void renderList_GeneratorFolder(HtmlTextWriter output)
		{
			//
			// Render ListMemberType1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ListMemberType1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msListMemberType1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ListMemberType2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ListMemberType2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msListMemberType2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render ListMemberType3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ListMemberType3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msListMemberType3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderAdministrationFolder(HtmlTextWriter output)
		{
			//
			// Render PanelChief
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelChief");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msPanelChief.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PanelMember1
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelMember1");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msPanelMember1.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PanelMember2
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelMember2");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msPanelMember2.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PanelMember3
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelMember3");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msPanelMember3.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PanelMember4
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelMember4");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msPanelMember4.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			//
			// Render PanelMember5
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("PanelMember5");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msPanelMember5.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

		}

		private void renderSystemFolder(HtmlTextWriter output)
		{
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

			//
			// Render ActiveTestList
			//
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ActiveTestList");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);
			msActiveTestList.RenderControl(output);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

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

