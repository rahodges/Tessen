using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amns.GreyFox.Scheduling;
using Amns.Rappahanock;
using Amns.Rappahanock.Utilities;
using Amns.Rappahanock.Web.Utilities;

namespace Amns.Tessen.Utilities
{
    public sealed class MembershipPackage
    {
        private DojoMember member;
        private DojoMemberTypeTemplate typeTemplate;
        private DojoMembershipCollection memberships;
        private decimal totalFee;

        /// <summary>
        /// The MemberTypeTemplate for this package.
        /// </summary>
        public DojoMemberTypeTemplate TypeTemplate
        {
            get
            {
                return typeTemplate;
            }
        }

        /// <summary>
        /// The memberships provided in this package.
        /// </summary>
        public DojoMembershipCollection Memberships
        {
            get
            {
                return memberships;
            }
        }

        /// <summary>
        /// The total fee for all memberships in this package.
        /// </summary>
        public decimal TotalFee
        {
            get { return totalFee; }
        }

        public MembershipPackage()
        {
            Clear();            
        }

        public void AddToCart()
        {
            ShoppingCart cart = ShoppingCart.GetCart();

            foreach (DojoMembership membership in memberships)
            {
                membership.Save();
                cart.Lines.Add(RHFactory.SalesOrderLine(cart.Order,
                    membership.membershipTemplate.Item, membership));
            }

            cart.Calc();
        }

        public void RemoveFromCart()
        {
            ShoppingCart cart;
            RHSalesOrderLineCollection deleteLines;
            IRHLineExtension lineExtension;
            
            cart = ShoppingCart.GetCart();
            deleteLines = new RHSalesOrderLineCollection();

            foreach (RHSalesOrderLine line in cart.Lines)
            {
                lineExtension = line.GetLineExtension();

                if (lineExtension is DojoMembership)
                {
                    lineExtension.Delete();
                    deleteLines.Add(line);
                }
            }

            foreach (RHSalesOrderLine line in deleteLines)
            {
                cart.Lines.Remove(line);
            }

            cart.Calc();
        }

        /// <summary>
        /// Applies a payment to all the memberships in this package and returns
        /// an amount remaining of a payment if there was an over payment.
        /// </summary>
        /// <param name="payment">Amount to apply.</param>
        /// <returns>Credit amount.</returns>
        public decimal ApplyPayment(decimal payment)
        {
            decimal applyAmount;

            foreach (DojoMembership membership in memberships)
            {
                // Find out how much this membership needs for a payment
                applyAmount = membership.Fee - membership.PaymentAmount;
                                
                // Apply a payment if there is payment left
                if (applyAmount <= payment)
                {
                    membership.PaymentAmount = applyAmount;
                    payment -= applyAmount;
                }
                else if (payment > decimal.Zero)
                {
                    membership.PaymentAmount = payment;
                    payment = decimal.Zero;
                }
            }

            return payment;
        }

        /// <summary>
        /// Grants this membership package to a member.
        /// </summary>
        public void ApplyGrant()
        {
            foreach (DojoMembership membership in memberships)
            {
                membership.Fee = decimal.Zero;
                membership.PaymentAmount = decimal.Zero;
            }
        }

        public void Save()
        {
            foreach (DojoMembership membership in memberships)
            {                
                membership.Save();
                applyMembership(membership);
                membership.Member.Save();
            } 
        }

        private void applyMembership(DojoMembership membership)
        {
            DojoMember parentMember;
            DojoOrganization primaryOrganization;

            // Don't update a new membership which has not yet begun.
            if (membership.StartDate > DateTime.Now.ToUniversalTime() |
                membership.EndDate < DateTime.Now.ToUniversalTime())
                return;

            parentMember = membership.Member;

            // Keep a stack of the memberships that need to be saved            
            DojoMembershipCollection oldMemberships = parentMember.CollateMemberships();

            if (parentMember.Membership1 != null && 
                parentMember.Membership1.MembershipTemplate.ID == membership.MembershipTemplate.ID)
                parentMember.Membership1 = membership;
            else if (parentMember.Membership2 != null && 
                parentMember.Membership2.MembershipTemplate.ID == membership.MembershipTemplate.ID)
                parentMember.Membership2 = membership;
            else if (parentMember.Membership3 != null && 
                parentMember.Membership3.MembershipTemplate.ID == membership.MembershipTemplate.ID)
                parentMember.Membership3 = membership;
            else if (parentMember.Membership4 != null && 
                parentMember.Membership4.MembershipTemplate.ID == membership.MembershipTemplate.ID)
                parentMember.Membership4 = membership;
            else if (parentMember.Membership5 != null && 
                parentMember.Membership5.MembershipTemplate.ID == membership.MembershipTemplate.ID)
                parentMember.Membership5 = membership;
            else 
            {
                if (parentMember.Membership1 == null)
                    parentMember.Membership1 = membership;
                else if (parentMember.Membership2 == null)
                    parentMember.Membership2 = membership;
                else if (parentMember.Membership3 == null)
                    parentMember.Membership3 = membership;
                else if (parentMember.Membership4 == null)
                    parentMember.Membership4 = membership;
                else if (parentMember.Membership5 == null)
                    parentMember.Membership5 = membership;
                else
                {
                    // WHOAH! THEY ARE ALL FULL!
                    // TODO: WHAT DO I DO NOW?
                }
            }

            // Now that the memberships are updated, flag the member
            // with the appropriate state.
            primaryOrganization = TessenFactory.PrimaryOrganization();
            if (primaryOrganization != null)
            {
                if (membership.StartDate < DateTime.Now.ToUniversalTime() &&
                    membership.EndDate > DateTime.Now.ToUniversalTime())
                {
                    parentMember.IsPrimaryOrgActive = true;
                }
            }
        }

        public void Clear()
        {
            member = null;
            typeTemplate = null;
            memberships = null;
            totalFee = decimal.Zero;
        }

        /// <summary>
        /// Builds the memberships for the current member. If the member has
        /// submembers associated, these submemberships will be added to the
        /// member's memberships listing and associated to the submembers.
        /// 
        /// Members are responsible for their submember's fees, but not any
        /// members beneath the submembers.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="typeTemplate"></param>
        /// <param name="builder"></param>
        public void Build(
            DojoMember member,
            DojoMemberTypeTemplate typeTemplate,
            MembershipBuilder builder)
        {
            DojoMembershipTemplateCollection membershipTemplates;

            // Builds new memberships for the member.
            
            // * Pull old memberships associated with the member.
            // * Build the next membership based of the template.
            // * Add membership.

            Clear();

            this.member = member;
            this.typeTemplate = typeTemplate;
            this.memberships = new DojoMembershipCollection();

            if (typeTemplate != null)
            {
                membershipTemplates = builder.getMembershipTemplates(member, typeTemplate);

                if (membershipTemplates != null)
                {
                    foreach (DojoMembershipTemplate template in membershipTemplates)
                        addTemplate(member, template, builder);
                }

                // WALK TEMPLATE TREE AND SUB MEMBERS LOCK STEP AND ADD EACH
                // SUBMEMBER'S MEMBERSHIPS TO THE CURRENT PACKAGE.
                builder.pullData(member);
                if (member.subMembers != null)
                {   
                    if (typeTemplate.subTemplates != null)
                    {
                        for (int i = 0; i < typeTemplate.subTemplates.Count; i++)
                        {
                            // This is for safety although the builder should only return
                            // members that have submembers that equal the type template's sub templates                        
                            if (i < member.subMembers.Count)
                            {
                                membershipTemplates =
                                    builder.getMembershipTemplates(member.subMembers[i],
                                    typeTemplate.subTemplates[i]);

                                foreach (DojoMembershipTemplate template in membershipTemplates)
                                    addTemplate(member.subMembers[i], template, builder);
                            }
                        }
                    }
                }
            }
        }

        private void addTemplate(
            DojoMember member,
            DojoMembershipTemplate template,
            MembershipBuilder builder)
        {
            // Get Prior
            DojoMembership prior = getPrior(member, template);

            // If the prior membership has not yet ended and the prior membership's
            // end date preceeds the template's pre-purchase date, skip this template.
            if (prior != null)
                if (prior.EndDate >= builder.BuildTime)
                    if (prior.EndDate >= builder.BuildTime.Subtract(template.PrePurchaseTime))
                        return;

            // Since this membership is either a new, renewal or pre-purchased renewal,
            // add it to the membership list.
            DojoMembership membership = new DojoMembership();
            membership.Member = member;
            membership.MembershipTemplate = template;
            membership.PriorMembership = prior;
            membership.Organization = template.Organization;
            membership.OrganizationMemberID = 
                prior != null ? prior.OrganizationMemberID : string.Empty;
            calcMembership(membership, builder);
            // calcMembership takes care of this!
            // membership.EndDate = calcDate(membership.startDate,
            //     template.MembershipEnd);
            // membership.Fee = template.Fee;
            
            memberships.Add(membership);

            totalFee += membership.Fee;
        }

        /// <summary>
        /// Calculates a fee and end date for a membership based on the 
        /// provided membership's start date. This will take into account the
        /// ProRate settings on the membership's parent template. ProRate
        /// settings can use either the day, the month or the month/day 
        /// combination. This will NOT ProRate memberships that do not have
        /// a prior membership.
        /// 
        /// This will add the startup fees for each member at the end.
        /// </summary>
        /// <param name="membership">The membership to calculate.</param>
        private void calcMembership(DojoMembership membership, MembershipBuilder builder)
        {
            DateTime now = builder.BuildTime;
            DojoMember member = membership.member;
            DojoMembershipTemplate template = membership.MembershipTemplate;

            if (membership.priorMembership == null)
            {
                membership.StartDate =
                    calcDate(now, template.MembershipStart);
            }
            else
            {
                membership.StartDate = DateManipulator.FirstOfNextDay(
                    membership.PriorMembership.EndDate);
            }

            membership.EndDate =
                calcDate(membership.StartDate,
                template.MembershipEnd);
            membership.Fee = template.Fee;

            if (membership.PriorMembership == null)
            {
                // PRO RATE A ==================================
                if (template.ProRateMonthA > 0 &&
                    template.ProRateMonthA <= now.Month)
                {
                    if (template.ProRateDayA > 0)
                    {
                        if (template.ProRateDayA <= now.Day)
                        {
                            membership.EndDate =
                                calcDate(membership.StartDate,
                                template.ProRateEndA);
                            membership.Fee =
                                template.ProRateFeeA;
                            membership.IsProRated = true;
                        }
                    }
                    else
                    {
                        membership.EndDate =
                                calcDate(membership.StartDate,
                                template.ProRateEndA);
                        membership.Fee =
                            template.ProRateFeeA;
                        membership.IsProRated = true;
                    }

                    // PRO RATE B ==================================
                    if (template.ProRateMonthB > 0 &&
                        template.ProRateMonthB <= now.Month)
                    {
                        if (template.ProRateDayB > 0)
                        {
                            if (template.ProRateDayB <= now.Day)
                            {
                                membership.EndDate =
                                    calcDate(membership.StartDate,
                                    template.ProRateEndB);
                                membership.Fee =
                                    template.ProRateFeeB;
                                membership.IsProRated = true;
                            }
                        }
                        else
                        {
                            membership.EndDate =
                                calcDate(membership.StartDate,
                                template.ProRateEndB);
                            membership.Fee =
                                template.ProRateFeeB;
                            membership.IsProRated = true;
                        }

                        // PRO RATE C ==================================
                        if (template.ProRateMonthC > 0 &&
                            template.ProRateMonthC <= now.Month)
                        {
                            if (template.ProRateDayC > 0)
                            {
                                if (template.ProRateDayC <= now.Day)
                                {
                                    membership.EndDate =
                                        calcDate(membership.StartDate,
                                        template.ProRateEndC);
                                    membership.Fee =
                                        template.ProRateFeeC;
                                    membership.IsProRated = true;
                                }
                            }
                            else
                            {
                                membership.EndDate =
                                        calcDate(membership.StartDate,
                                        template.ProRateEndC);
                                membership.Fee =
                                    template.ProRateFeeC;
                                membership.IsProRated = true;
                            }
                        }
                        else if (template.ProRateDayC > 0 &&
                            template.ProRateDayC <= now.Day)
                        {
                            membership.EndDate =
                                calcDate(membership.StartDate,
                                template.ProRateEndC);
                            membership.Fee =
                                template.ProRateFeeC;
                            membership.IsProRated = true;
                        }
                    }
                    else if (template.ProRateDayB > 0 &&
                        template.ProRateDayB <= now.Day)
                    {
                        membership.EndDate =
                            calcDate(membership.StartDate,
                            template.ProRateEndB);
                        membership.Fee =
                            template.ProRateFeeB;
                        membership.IsProRated = true;
                    }
                }
                else if (template.ProRateDayA > 0 &&
                    template.ProRateDayA <= now.Day)
                {
                    membership.EndDate =
                        calcDate(membership.StartDate,
                        template.ProRateEndA);
                    membership.Fee =
                        template.ProRateFeeA;
                    membership.IsProRated = true;
                }
            }

            if (membership.PriorMembership == null)
                membership.Fee += template.StartupFee;
        }

        /// <summary>
        /// Gets a member's prior membership based on the template provided. If the member's
        /// membership has an associated root membership template that matches the template,
        /// then it will still be matched.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public DojoMembership getPrior(DojoMember member, DojoMembershipTemplate template)
        {
            if (priorMatch(member.Membership1, template))
                return member.Membership1;

            if (priorMatch(member.Membership2, template))
                return member.Membership2;

            if (priorMatch(member.Membership3, template))
                return member.Membership3;

            if (priorMatch(member.Membership4, template))
                return member.Membership4;

            if (priorMatch(member.Membership5, template))
                return member.Membership5;

            else if (member.Membership5 != null)
            {
                // Go looking for the latest membership that matches
                // the template's ID, since every membership slot was
                // full. SORT NEWEST ON TOP
                DojoMembershipManager m = new DojoMembershipManager();
                
                DojoMembershipCollection memberships =                    
                    m.GetCollection(1, 
                    "MembershipTemplateID=" +
                    template.ID.ToString() +                     
                    (template.RootTemplate != null ? " OR " + 
                    "MembershipTemplate.RootTemplateID=" + template.RootTemplate.ID : ""),                     
                    "EndDate DESC", null);

                if (memberships.Count > 0)
                    return memberships[0];
            }

            return null;
        }

        /// <summary>
        /// Tests to see if the existing membership can be matched to the template.
        /// </summary>
        /// <param name="membership"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        private bool priorMatch(DojoMembership membership, DojoMembershipTemplate template)
        {
            if (membership != null)
            {
                // If the membership's root template is the same as the one provided, then
                // this counts as a prior membership.
                if (membership.MembershipTemplate.RootTemplate != null)
                    if (membership.MembershipTemplate.RootTemplate.ID == template.ID)
                        return true;

                // If the membership's template is the same as the template provided, then
                // this counts as a match.
                if (membership.MembershipTemplate.ID == template.ID)
                    return true;
            }

            return false;
        }

        private DateTime calcDate(DateTime from, DojoMembershipDayType dateType)
        {
            switch (dateType)
            {
                case DojoMembershipDayType.CurrentDay:
                    return from;
                case DojoMembershipDayType.FirstOfYear:
                    return Amns.GreyFox.Scheduling.DateManipulator.FirstOfYear(from);
                case DojoMembershipDayType.FirstOfMonth:
                    return Amns.GreyFox.Scheduling.DateManipulator.FirstOfMonth(from);
                case DojoMembershipDayType.EndOfMonth:
                    return Amns.GreyFox.Scheduling.DateManipulator.EndOfMonth(from);                    
                case DojoMembershipDayType.EndOfFollowingMonth:
                    return Amns.GreyFox.Scheduling.DateManipulator.EndOfFollowingMonth(from);
                case DojoMembershipDayType.EndOfFollowingYear:
                    return Amns.GreyFox.Scheduling.DateManipulator.EndOfFollowingYear(from);
                case DojoMembershipDayType.EndOfYear:
                    return Amns.GreyFox.Scheduling.DateManipulator.EndOfYear(from);
                default:
                    goto case DojoMembershipDayType.CurrentDay;
            }
        }

    }
}
