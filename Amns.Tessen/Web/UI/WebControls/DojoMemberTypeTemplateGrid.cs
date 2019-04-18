using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.GreyFox.Security;
using Amns.Tessen.Utilities;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// <summary>
	/// A custom grid for DojoMemberTypeTemplate.
	/// </summary>
	/// </summary>
	[ToolboxData("<{0}:DojoMemberTypeTemplateGrid runat=server></{0}:DojoMemberTypeTemplateGrid>")]
	public class DojoMemberTypeTemplateGrid : TreeView
	{
        private int selectedID;

        public int SelectedID { get { return selectedID; } }

        public EventHandler TemplateClicked;
        protected virtual void OnTemplateClicked(EventArgs e)
        {
            if (TemplateClicked != null)
                TemplateClicked(this, e);
        }

        protected override void CreateChildControls()
        {            
            base.CreateChildControls();

            tree.AutoPostBackOnSelect = true;
            tree.NodeSelected += new ComponentArt.Web.UI.TreeView.NodeSelectedEventHandler(tree_NodeSelected);

            ChildControlsCreated = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            DojoMemberTypeTemplateManager m;
            DojoMemberTypeTemplateCollection templates;
            ComponentArt.Web.UI.TreeViewNode node;

            base.OnLoad(e);
            EnsureChildControls();

            if (tree.Nodes.Count == 0)
            {
                node = new ComponentArt.Web.UI.TreeViewNode();
                node.Text = "Member Type Templates";
                node.ImageUrl = "./images/tree/root.gif";
                node.Expanded = true;
                tree.Nodes.Add(node);

                m = new DojoMemberTypeTemplateManager();
                templates = m.GetCollection(string.Empty, "Name, CreateDate", null);

                TreeMaker.Make(templates, TreeMaker.MakeMode.Collapse);

                foreach (DojoMemberTypeTemplate template in templates)
                {
                    if (template.parent == null)
                        addTemplate(node, template);
                }
            }
        }

        private void tree_NodeSelected(object sender, ComponentArt.Web.UI.TreeViewNodeEventArgs e)
        {
            if (tree.SelectedNode.ParentNode != null)
            {
                selectedID = int.Parse(tree.SelectedNode.Value);
                if (TemplateClicked != null)
                    TemplateClicked(this, new EventArgs());
            }
        }

        private void addTemplate(ComponentArt.Web.UI.TreeViewNode parentNode,
            DojoMemberTypeTemplate template)
        {
            // Safe to use internals due to TreeMaker requirements!

            ComponentArt.Web.UI.TreeViewNode node;
            
            node = new ComponentArt.Web.UI.TreeViewNode();
            node.Text = template.name;      
            node.Value = template.iD.ToString();
            node.ToolTip = template.description;
            parentNode.Nodes.Add(node);

            if (template.subTemplates != null)
                foreach (DojoMemberTypeTemplate subTemplate in template.subTemplates)
                    addTemplate(node, subTemplate);
        }
	}
}
