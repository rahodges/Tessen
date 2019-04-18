using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amns.Tessen.Utilities;

namespace Amns.Tessen.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:MembershipRenewalPanel runat=server></{0}:MembershipRenewalPanel>")]
    public class MembershipRenewalPanel : Control
    {
        Table membershipsTable;

        string cssClass;
        public string CssClass { get { return cssClass; } set { cssClass = value; } }

        protected override void CreateChildControls()
        {
            Panel container = new Panel();
            container.CssClass = cssClass;
            Controls.Add(container);

            Panel divPanel = new Panel();
            container.Controls.Add(divPanel);

            membershipsTable = new Table();
            divPanel.Controls.Add(divPanel);

            TableRow headerRow = new TableRow();
            TableCell headerCell = new TableCell();
            headerCell.ColumnSpan = 3;
            headerCell.Text = Localization.Strings.MembershipTemplates_Title;
            headerRow.Cells.Add(headerCell);
            membershipsTable.Rows.Add(headerRow);

            ChildControlsCreated = true;
        }

        public void Bind(DojoMember member)
        {
            //MembershipScan scan = new MembershipScan();
            //DojoMembershipTemplateCollection availableTemplates =
            //    scan.GetMemberTypeTemplates(member);

            //foreach (DojoMembershipTemplate template in
            //    availableTemplates)
            //{
            //    addMembershipTemplate(membershipsTable, template);
            //}            

            TableRow headerRow = new TableRow();
            TableCell headerCell = new TableCell();
            headerCell.ColumnSpan = 3;
            headerCell.Text = Localization.Strings.MembershipTemplates_None;
            headerRow.Cells.Add(headerCell);
            membershipsTable.Rows.Add(headerRow);
        }

        private void addMembershipTemplate(Table table, DojoMembershipTemplate template)
        {
            TableRow nameRow = new TableRow();
            TableCell radioCell = new TableCell();
            radioCell.Text = "<input type=\"radio\" name=\"MembershipTemplates\" value=\"" +
                template.ID.ToString() + "\" />";
            TableCell nameCell = new TableCell();
            nameCell.Text = template.Name;
            nameRow.Cells.Add(nameCell);
            TableCell feeCell = new TableCell();
            feeCell.Text = template.Fee.ToString("c");
            nameRow.Cells.Add(feeCell);
            table.Rows.Add(nameRow);

            TableRow descriptionRow = new TableRow();
            TableCell descriptionCell = new TableCell();
            descriptionCell.Text = template.Description;
            descriptionCell.ColumnSpan = 3;
            descriptionRow.Cells.Add(descriptionCell);
            table.Rows.Add(descriptionRow);
        }
    }
}
