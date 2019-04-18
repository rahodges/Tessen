using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amns

namespace Amns.Tessen.Utilities
{
    public class DojoMembershipTemplateGroupWrapper
    {
        private DojoMembershipTemplateGroup templateGroup;
        private DojoMembershipTemplateGroupCollection templateGroups =
            new DojoMembershipTemplateGroupCollection();

        public int AgeYearsMax { get { return templateGroup.ageYearsMax; } }
        public int AgeYearsMin { get { return templateGroup.ageYearsMin; } }
        public bool AllowAutoRenewal { get { return templateGroup.allowAutoRenewal; } }
        public bool AllowGuestPurchase { get { return templateGroup.allowGuestPurchase; } }
        public DateTime CreateDate { get { return templateGroup.createDate; } }
        public string Description { get { return templateGroup.description; } }
        public int ID { get { return templateGroup.iD; } }
        public DojoRank InitialRank { get { return templateGroup.initialRank; } }
        public 

        public DojoMembershipTemplateGroupCollection TemplateGroups
        {
            get { return templateGroups; }
        }

        public DojoMembershipTemplateGroupWrapper(DojoMembershipTemplateGroup templateGroup)
        {
            templateGroups = new DojoMembershipTemplateGroupCollection();

            templateGroup = templateGroup;
            templateGroup.AgeYearsMax;
            templateGroup.AgeYearsMin;
            templateGroup.AllowAutoRenewal;
            templateGroup.AllowGuestPurchase;
            templateGroup.AllowRenewal;
            templateGroup.CreateDate;
            templateGroup.Description;
            templateGroup.ID;
            templateGroup.InitialRank;
            templateGroup.Item;
            templateGroup.ItemClass;
            templateGroup.MemberForMax;
            templateGroup.MemberForMin;
            templateGroup.MembershipTreeHash;
            templateGroup.MemberType;
            templateGroup.ModifyDate;
            templateGroup.Name;
            templateGroup.OrderNum;
            templateGroup.Parent;
            templateGroup.RankMax;
            templateGroup.RankMin;
            templateGroup.Templates;
        }

        public DojoAccessControlGroupCollection TemplateGroups
        {
            get
            {
                return templateGroups;
            }
        }
    }
}
