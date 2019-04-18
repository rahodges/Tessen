using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amns.GreyFox.Web.UI.WebControls;
using Amns.Tessen.Utilities;
using Amns.Rappahanock;
using Amns.Rappahanock.Web.Utilities;

namespace Amns.Tessen.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:MembershipsControl runat=server></{0}:MembershipsControl>")]
    public class MembershipsControl : Control, IPostBackEventHandler
    {
        Panel panel;
        DojoMember member;
        string memberTypeTemplateCssClass;
        EvenMoreCornersDialog.DialogMode memberTypeTemplateMode;
        string membershipsCssClass;
        string membershipTemplateCssClass;
        EvenMoreCornersDialog.DialogMode membershipTemplateMode;

        public DojoMember Member
        {
            get
            {
                if (member == null & ViewState["MemberID"] != null)
                    member = DojoMember.NewPlaceHolder(((int)ViewState["MemberID"]));
                return member;
            }
            set
            {
                member = value;
                ViewState["MemberID"] = member.ID;
            }
        }

        /// <summary>
        /// The CSS Class for MemberTypeTemplates, this should utilize the
        /// Even More Corners for CSS stylesheet.
        /// </summary>
        public string MemberTypeTemplateCssClass
        {
            get { return memberTypeTemplateCssClass; }
            set { memberTypeTemplateCssClass = value; }
        }

        /// <summary>
        /// The CSS Class for Memberships.
        /// </summary>
        public string MembershipsCssClass
        {
            get { return membershipsCssClass; }
            set { membershipsCssClass = value; }
        }

        /// <summary>
        /// The CSS Class for MembershipTemplates, this should utilize the
        /// Even More Corners for CSS stylesheet.
        /// </summary>
        public string MembershipTemplateCssClass
        {
            get { return membershipTemplateCssClass; }
            set { membershipTemplateCssClass = value; }
        }

        public EvenMoreCornersDialog.DialogMode MemberTypeTemplateMode
        {
            get { return memberTypeTemplateMode; }
            set { memberTypeTemplateMode = value; }
        }

        public EvenMoreCornersDialog.DialogMode MembershipTemplateMode
        {
            get { return membershipTemplateMode; }
            set { membershipTemplateMode = value; }
        }

        public MembershipsControl()
        {
            membershipTemplateMode = EvenMoreCornersDialog.DialogMode.Downgrade;
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            panel = new Panel();
            panel.ID = this.ID;
            Controls.Add(panel);                  

            ChildControlsCreated = true;
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument.Length > 0)
            {
                MembershipBuilder builder = new MembershipBuilder();
                builder.Load(Member);
                builder.ProcessTrees();
                builder.ProcessHashes();

                List<MembershipPackage> packages = builder.GetMembershipPackages(Member);

                // Add selected memberships to cart
                foreach (MembershipPackage package in packages)
                {
                    if (eventArgument == package.TypeTemplate.ID.ToString())
                    {
                        package.RemoveFromCart();
                        package.AddToCart();

                        Page.Response.Redirect("~/Cart.aspx");
                    }
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {            
            base.OnLoad(e);

            if (!Page.IsPostBack)
            {
                MembershipBuilder builder = new MembershipBuilder();
                builder.Load(Member);
                builder.ProcessTrees();
                builder.ProcessHashes();

                List<MembershipPackage> packages = builder.GetMembershipPackages(Member);

                EnsureChildControls();
                bind(packages);
            }
        }

        public void bind(List<MembershipPackage> packages)
        {
            if (packages.Count == 0)
            {
                addEmpty(panel);
            }
            else
            {
                foreach (MembershipPackage package in
                    packages)
                {
                    addTemplate(panel, package);
                }
            }
        }

        protected void addEmpty(Panel container)
        {
            EvenMoreCornersDialog d = new EvenMoreCornersDialog();
            d.CssClass = memberTypeTemplateCssClass;
            d.Header.Add(new LiteralControl(
                string.Format(Localization.Strings.Memberships_Empty,
                Member.PrivateContact.FullName)));
            container.Controls.Add(d);
        }

        protected void addTemplate(Panel container, MembershipPackage package)
        {
            EvenMoreCornersDialog d = new EvenMoreCornersDialog();
            d.CssClass = memberTypeTemplateCssClass;
            d.Mode = memberTypeTemplateMode;
            d.Header.Add(new LiteralControl("<h1>" + package.TypeTemplate.Name + "</h1>"));
            d.Body.Add(new LiteralControl(package.TypeTemplate.Description));
            container.Controls.Add(d);

            Panel membershipsPanel = new Panel();
            membershipsPanel.CssClass = membershipsCssClass;
            d.Body.Add(membershipsPanel);

            if (package.Memberships.Count == 0)
            {
                EvenMoreCornersDialog noMembershipsDialog = new EvenMoreCornersDialog();
                noMembershipsDialog.CssClass = membershipTemplateCssClass;
                noMembershipsDialog.Mode = membershipTemplateMode;
                noMembershipsDialog.Header.Add(new LiteralControl("<h2>" +
                    Localization.Strings.NoMembershipsFriendly + "</h2>"));
                membershipsPanel.Controls.Add(noMembershipsDialog);
            }
            else
            {
                foreach (DojoMembership membership in package.Memberships)
                {
                    addTemplate(membershipsPanel, membership);
                }
            }

            membershipsPanel.Controls.Add(new LiteralControl(
                "<input type=\"button\" name=\"packageselect\" value=\"select\" " +
                "onclick=\"" + 
                Page.ClientScript.GetPostBackEventReference(this, package.TypeTemplate.ID.ToString()) + "\"" +
                " />"));
        }

        protected void addTemplate(Panel container, DojoMembership membership)
        {
            EvenMoreCornersDialog d = new EvenMoreCornersDialog();
            d.CssClass = membershipTemplateCssClass;
            d.Mode = membershipTemplateMode;
            d.Header.Add(new LiteralControl("<h2>" + 
                membership.MembershipTemplate.Name + 
                (membership.PriorMembership == null ? " " + Localization.Strings.StartupMembershipSuffix : "") +
                (membership.IsProRated ? " " + Localization.Strings.ProrateMembershipSuffix : "") +
                "</h2>"));
            if(membership.Member.ID != member.ID)
                d.Header.Add(new LiteralControl("<h3>" + membership.Member.PrivateContact.FullName + "</h3>"));
            d.Header.Add(new LiteralControl("<p>" + string.Format(
                Localization.Strings.MembershipTemplates_DateFormat,
                membership.StartDate,
                membership.EndDate) + "</p>"));
            d.Body.Add(new LiteralControl("<p>" + membership.Fee.ToString("c") + "</p>"));
            container.Controls.Add(d);
        }
    }
}