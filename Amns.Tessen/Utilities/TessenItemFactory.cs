using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using Amns.GreyFox.People;
using Amns.GreyFox.Security;
using Amns.GreyFox.Utilities;
using Amns.Rappahanock;
using Amns.Rappahanock.Utilities;

namespace Amns.Tessen.Utilities
{
    public sealed class TessenFactory
    {
        public static DojoOrganization PrimaryOrganization()
        {
            string value;

            value = ConfigurationManager.AppSettings["Tessen_PrimaryOrganizatonID"];

            if (value != null)
            {
                try { return DojoOrganization.NewPlaceHolder(int.Parse(value)); }
                catch { }
            }
            else
            {
                return DojoOrganization.NewPlaceHolder(1);
            }

            return null;
        }

        public static RHCustomer Customer(DojoMember member)
        {
            RHCustomer customer = RHFactory.Customer(member.UserAccount);

            return customer;
        }

        public static DojoMemberType MemberType(string name, string description)
        {
            DojoMemberType t = new DojoMemberType();
            t.Name = name;
            t.Description = description;
            return t;
        }

        public static GreyFoxContact Location(string name)
        {
            GreyFoxContact location = new GreyFoxContact(DojoOrganizationManager.LocationTable);
            location.BusinessName = name;
            location.BirthDate = new DateTime(1980, 1, 1);
            return location;
        }

        public static DojoMembershipTemplate MembershipTemplate(
            string name,
            string description, 
            bool purchaseRequired, bool testRequired, bool attendanceRequired,
            decimal fee, decimal startupFee,
            DojoMembershipDayType membershipStart,
            DojoMembershipDayType membershipEnd,
            DojoOrganization organization,
            int orderNum,
            int proRateMonthA, int proRateDayA, decimal proRateFeeA, DojoMembershipDayType proRateEndA,
            int proRateMonthB, int proRateDayB, decimal proRateFeeB, DojoMembershipDayType proRateEndB,
            int proRateMonthC, int proRateDayC, decimal proRateFeeC, DojoMembershipDayType proRateEndC,
            int ageYearsMin, int ageYearsMax,
            int memberForMin, int memberForMax,
            DojoRank rankMin, DojoRank rankMax)
        {
            DojoMembershipTemplate template = new DojoMembershipTemplate();
            template.Name = name;
            template.Description = description;
            template.PurchaseRequired = purchaseRequired;
            template.TestRequired = testRequired;
            template.AttendanceRequired = attendanceRequired;
            template.MembershipStart = membershipStart;
            template.MembershipEnd = membershipEnd;
            template.Organization = organization;
            template.OrderNum = orderNum;
            template.ProRateMonthA = proRateMonthA;
            template.ProRateDayA = proRateDayA;
            template.ProRateFeeA = proRateFeeA;
            template.ProRateEndA = proRateEndA;
            template.ProRateMonthB = proRateMonthB;
            template.ProRateDayB = proRateDayB;
            template.ProRateFeeB = proRateFeeB;
            template.ProRateEndB = proRateEndB;
            template.ProRateMonthC = proRateMonthC;
            template.ProRateDayC = proRateDayC;
            template.ProRateFeeC = proRateFeeC;
            template.ProRateEndC = proRateEndC;
            template.AgeYearsMin = ageYearsMin;
            template.AgeYearsMax = ageYearsMax;
            template.MemberForMin = memberForMin;
            template.MemberForMax = memberForMax;
            template.RankMin = rankMin;
            template.RankMax = rankMax;
            return template;
        }

        private static DojoOrganization Organization(
            string name,
            string description,
            GreyFoxContact location)
        {
            DojoOrganization organization = new DojoOrganization();
            organization.Name = name;
            organization.Description = description;
            organization.Location = location;
            return organization;
        }

        public static DojoMemberTypeTemplate MemberTypeTemplate(
            string name,
            string description,
            DojoMemberTypeTemplate parent,
            DojoMemberType memberType,            
            bool allowPurchase,
            bool allowGuestPurchase,
            bool allowRenewal,
            bool allowAutoRenewal,
            int ageYearsMin, int ageYearsMax,
            int memberForMin, int memberForMax,
            DojoRank rankMin, DojoRank rankMax,
            DojoMembershipTemplate template1,
            DojoMembershipTemplate template2,
            DojoMembershipTemplate template3,
            DojoMembershipTemplate template4,
            DojoMembershipTemplate template5)
        {
            DojoMemberTypeTemplate template = new DojoMemberTypeTemplate();            
            template.Name = name;
            template.Description = description;
            template.Parent = parent;
            template.MemberType = memberType;
            template.AllowPurchase = allowPurchase;
            template.AllowGuestPurchase = allowGuestPurchase;
            template.AllowRenewal = allowRenewal;
            template.AllowAutoRenewal = allowAutoRenewal;
            template.AgeYearsMin = ageYearsMin;
            template.AgeYearsMax = ageYearsMax;
            template.MemberForMin = memberForMin;
            template.MemberForMax = memberForMax;
            template.RankMin = rankMin;
            template.RankMax = rankMax;            
            template.MembershipTemplate1 = template1;
            template.MembershipTemplate2 = template2;
            template.MembershipTemplate3 = template3;
            template.MembershipTemplate4 = template4;
            template.MembershipTemplate5 = template5;
            return template;
        }

        public DojoRank Rank(
            string name,
            string description,
            int orderNum,
            TimeSpan timeFromLastTest,
            TimeSpan timeInRank)
        {
            DojoRank rank = new DojoRank();
            rank.name = name;
            rank.description = description;
            rank.orderNum = orderNum;
            rank.PromotionTimeFromLastTest = timeFromLastTest;
            rank.PromotionTimeInRank = timeInRank;
            return rank;
        }
    }
}