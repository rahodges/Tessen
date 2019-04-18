using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using Amns.Tessen.Utilities;

namespace Amns.Tessen.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:MemberPane runat=server></{0}:MemberPane>")]
    public class MemberPane : Control
    {
        protected CallBack callBack;
        private string cssClass;

        private Label name;
        private Label address;
        private Table table;

        private Table membershipsTable;

        private Table availableMembershipsTable;

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
            callBack.Controls.Add(table);

            name = new Label();
            address = new Label();
            addCells(table, name, address);

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

        private void Populate(string id)
        {
            DojoMember m;
            string validation;
            MembershipBuilder b;
            
            m = new DojoMember(int.Parse(id));

            // We're going to twist the membership builder to get what we want for faster
            // load times. So what we'll do is load the member into the Load Function
            // then we'll ask the membership builder for the member back. Pretty
            // nifty eh? Why? Because the membership builder loads the member's root
            // and children in one pass. :) :) :)
            b = new MembershipBuilder();
            b.Load(m);                     // MUAHAHAHA! FASTER! USES ROOT MEMBER!
            b.ProcessTrees();              // Required for memberships availability
            b.ProcessHashes();             // Required for memberships availability
            b.pullData(m);

            name.Text = m.PrivateContact.FullName;
            address.Text = m.PrivateContact.ConstructAddress("<br />");
            validation = m.PrivateContact.ValidationFlagsToString();
            if (validation.Length != 0)
                address.Text += "<br /><em>" + validation;

            if (m.Parent != null)
            {
                addRow(table, Localization.Strings.ParentMember + " : " + m.Parent.PrivateContact.FullName);
                if (m.Root != null & m.ID != m.Root.ID & m.Parent.ID != m.Root.ID)
                    addRow(table, Localization.Strings.RootMember + " : " + m.Root.PrivateContact.FullName);
            }
            else
            {
                if (m.Root != null & m.Root.ID != m.ID)
                    addRow(table, Localization.Strings.RootMember + " : " + m.Root.PrivateContact.FullName);
            }

            addRow(table, Localization.Strings.MemberType + " : " +
                (m.MemberType != null ? m.MemberType.Name : Localization.Strings.IllegalValue));

            addRow(table, Localization.Strings.Rank + " : " +
                string.Format(Localization.Strings.RankFormat,
                m.Rank != null ? m.Rank.Name : Localization.Strings.NoRankSpecified,
                m.TimeInRank.Hours));

            addRow(table, Localization.Strings.MemberSince + " : " +
                string.Format(Localization.Strings.MemberSinceFormat,
                m.MemberSince, m.TimeInMembership.Hours));
            
            // ATTENDANCE =========================================================

            int maxEntries = 150;
            int displayEntries = 5;
            DateTime minSearchDate = DateTime.Now.Subtract(TimeSpan.FromDays(90));

            DojoAttendanceEntryManager aem = new DojoAttendanceEntryManager();
            DojoAttendanceEntryCollection attendance =
                aem.GetCollection(maxEntries, "MemberID=" + m.ID.ToString() +
                " AND ClassStart>=#" + minSearchDate.ToString() + "#", "ClassStart DESC",
                DojoAttendanceEntryFlags.Class);

            DojoMember instructor1 = m.Instructor1;
            DojoMember instructor2 = m.Instructor2;
            DojoMember instructor3 = m.Instructor3;
            
            if (attendance.Count < displayEntries)
                displayEntries = attendance.Count;

            addRow(table, Localization.Strings.LastSignin + " : " +
                m.LastSignin.ToShortDateString());

            Table instructorTable = new Table();

            addCells(table, new LiteralControl(Localization.Strings.NinetyDayInstructors), instructorTable);
            if(instructor1 != null)
                addRow(instructorTable, instructor1.PrivateContact.FullName);
            if(instructor2 != null)
                addRow(instructorTable, instructor2.PrivateContact.FullName);
            if(instructor3 != null)
                addRow(instructorTable, instructor3.PrivateContact.FullName);

            Table attendanceTable = new Table();

            foreach (DojoAttendanceEntry a in attendance)
            {
                addRow(attendanceTable, a.Class.Name);
                addRow(attendanceTable, a.Class.ClassStart.ToString("dddd, MMMM d - h:mm tt"));
            }

            // MEMBERSHIPS ========================================================

            membershipsTable = new Table();
            addCells(table, new LiteralControl(Localization.Strings.MembershipsCurrent), membershipsTable);

            availableMembershipsTable = new Table();
            addCells(table, new LiteralControl(Localization.Strings.MembershipsAvailable), availableMembershipsTable);

            DojoMembershipCollection memberships = m.CollateMemberships();

            if (memberships.Count == 0)
                addRow(membershipsTable, Localization.Strings.NoMemberships);
            else
                foreach(DojoMembership membership in memberships)
                    addMembership(membership);

            List<MembershipPackage> packages = b.GetMembershipPackages(m);
            if (packages.Count == 0)
                addRow(availableMembershipsTable, Localization.Strings.NoMemberships);
            else
                foreach (MembershipPackage package in packages)
                {
                    if (package.Memberships.Count == 0)
                    {
                        addRow(availableMembershipsTable, package.TypeTemplate.Name);
                        addRow(availableMembershipsTable, Localization.Strings.NoMemberships);
                    }
                    else
                    {
                        addRow(availableMembershipsTable, package.TypeTemplate.Name, package.TotalFee.ToString("c"));
                        foreach (DojoMembership membership in package.Memberships)
                        {
                            addRow(availableMembershipsTable, membership.MembershipTemplate.Name +
                                (membership.PriorMembership == null ? " " + Localization.Strings.StartupMembershipAbbreviation : "") +
                                (membership.IsProRated ? " " + Localization.Strings.ProrateMembershipAbbreviation : ""),
                                membership.Fee.ToString("c"));
                            addRow(availableMembershipsTable,
                                string.Format(Localization.Strings.MembershipDates,
                                membership.StartDate,
                                membership.EndDate), "&nbsp;");
                        }
                    }
                }

            // SECURITY ===========================================================


        }

        private void addMembership(DojoMembership membership)
        {
            if (membership != null)
            {
                // TODO : CONVERT UTC TO LOCAL TIME FOR DOJO
                addRow(membershipsTable,
                    membership.MembershipTemplate.Name,
                    membership.StartDate.ToShortDateString(),
                    membership.EndDate.ToShortDateString());
            }
        }

        private void addRow(Table table, params string[] text)
        {
            TableRow row = new TableRow();
            foreach (string s in text)
            {
                TableCell cell = new TableCell();
                cell.Text = s;
                row.Cells.Add(cell);
            }
            table.Rows.Add(row);
        }

        public void callBack_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            Populate(e.Parameter);

            table.RenderControl(e.Output);
        }        
    }
}
