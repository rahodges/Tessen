using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;

namespace Amns.Tessen.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:SeminarPane runat=server></{0}:SeminarPane>")]
    public class SeminarPane : Control
    {
        protected CallBack callBack;
        private string cssClass;

        private Label name;
        private Label description;
        private Label location;
        private Label fee;
        private Table table;

        private Table loadingTable;

        public string CssClass { get { return cssClass; } set { cssClass = value; } }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            callBack = new CallBack();
            callBack.Callback += 
                new CallBack.CallbackEventHandler(callBack_Callback);
            callBack.ID = this.ID;
            Controls.Add(callBack);           
 
            table = new Table();
            table.CssClass = cssClass;
            name = new Label();
            description = new Label();
            location = new Label();
            fee = new Label();
            addCells(table, name, description, location, fee);
            callBack.Controls.Add(table);

            ClientTemplate loadingTemplate = new ClientTemplate();
            loadingTable = new Table();
            callBack.LoadingPanelClientTemplate = loadingTemplate;
            loadingTemplate.Controls.Add(loadingTable);
            addCells(loadingTable, new LiteralControl("Loading..."),
                new LiteralControl("<img src=\"images/spinner.gif\" " +
                    "width=\"16\" height=\"16\" border=\"0\">"));
                        
            ChildControlsCreated = true;
        }

        private void addCells(Table table, 
            Control headerControl, params Control[] controls)
        {            
            TableRow headerRow = new TableRow();
            TableHeaderCell headerCell = new TableHeaderCell();
            headerCell.Controls.Add(headerControl);
            headerRow.Cells.Add(headerCell);
            table.Rows.Add(headerRow);

            foreach (Control c in controls)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.Controls.Add(c);
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }
        }

        private void addCell(Table table, string cssStyle, string text)           
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Text = text;
            row.Cells.Add(cell);
            table.Rows.Add(row);
        }

        private void Populate(string id)
        {
            DojoSeminar s;

            s = new DojoSeminar(int.Parse(id));

            DojoSeminarRegistrationManager dsrm = new DojoSeminarRegistrationManager();
            DojoSeminarRegistrationCollection registrations = 
                dsrm.GetCollection("ParentSeminarID=" + id, "LastName, FirstName, MiddleName",
                DojoSeminarRegistrationFlags.Contact);                       

            name.Text = s.Name;
            description.Text = s.Description;
            location.Text = s.Location.BusinessName;

            Panel regPanel = new Panel();
            regPanel.ID = "regPanel";
            regPanel.Style.Add("overflow", "scroll");
            regPanel.Style.Add("height", "200px");
            addCells(table, new LiteralControl("Registrations"), regPanel);
            Table regTable = new Table();
            regPanel.Controls.Add(regTable);

            foreach (DojoSeminarRegistration r in registrations)
            {
                addCell(regTable, string.Empty, r.Contact.FullName);
            }
        }

        public void callBack_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            Populate(e.Parameter);

            table.RenderControl(e.Output);
        }        
    }
}
