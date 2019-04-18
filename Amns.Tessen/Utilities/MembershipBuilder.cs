/***************************************************************
/* Membership Builder                                          *
/* Copyright 2007-2008 Roy A.E. Hodges                         *
/* -----------------------------------                         *
/* This code works! 18FEB08                                    *
/***************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using Amns.Rappahanock.Utilities;

namespace Amns.Tessen.Utilities
{
    public sealed class MembershipBuilder
    {
        DateTime buildTime;           
        
        SalesOrderBuilder orderBuilder;
        InvoiceBuilder invoiceBuilder;

        bool isLoaded;
        bool isTreeProcessed;
        bool isHashProcessed;
        DojoMemberCollection members;
        DojoMemberTypeTemplateCollection memberTypeTemplates;
        Dictionary<int, DojoMember> memberDictionary;
        Dictionary<string, DojoMemberTypeTemplateCollection> memberTypeTemplateDictionary;

        public DateTime BuildTime { get { return buildTime; } set { buildTime = value; } }
        public bool IsLoaded { get { return isLoaded; } }
        public bool IsTreeProcessed { get { return isTreeProcessed; } }
        public bool IsHashProcessed { get { return isHashProcessed; } }
        public DojoMemberCollection Members { get { return members; } }
        public DojoMemberTypeTemplateCollection MemberTypeTemplates { get { return memberTypeTemplates; } }

        public MembershipBuilder()
        {            
            orderBuilder = new SalesOrderBuilder();
            invoiceBuilder = new InvoiceBuilder();
            buildTime = DateTime.Now.ToUniversalTime();
        }

        #region Load Methods

        public void Load()
        {
            members =
                new DojoMemberManager().GetCollection(
                string.Empty,
                "PrivateContact.LastName, PrivateContact.FirstName, PrivateContact.MiddleName",
                DojoMemberFlags.PrivateContact);
            memberTypeTemplates =
                new DojoMemberTypeTemplateManager().GetCollection(
                string.Empty,
                string.Empty,
                null);

            isLoaded = true;
        }

        public void Load(DojoMember member)
        {
            if (member.Root != null)
            {
                members =
                    new DojoMemberManager().GetCollection(
                    "RootID=" + member.Root.ID.ToString(),
                    "PrivateContact.LastName, PrivateContact.FirstName, PrivateContact.MiddleName",
                    DojoMemberFlags.PrivateContact);
                memberTypeTemplates =
                    new DojoMemberTypeTemplateManager().GetCollection(
                    string.Empty,
                    string.Empty,
                    null);

                isLoaded = true;
            }
            else
            {
                Load(); // Fallback to loading all members if the member has no root.
            }
        }

        #endregion

        #region Membership Package Class Support

        /// <summary>
        /// Pulls a portion of the member tree to a provided member, this is an
        /// internal method to support MembershipPackage.
        /// </summary>
        /// <param name="member">Provided member to pull tree for.</param>
        internal void pullData(DojoMember member)
        {
            DojoMember workingMember;

            if (member == null)
                return;

            // Try to find the member in the builder trees
            if (!memberDictionary.TryGetValue(member.iD,
                out workingMember))
            {
                throw new MembershipBuilderException(
                    Localization.Strings.MembershipBuilder_CannotFindMember);
            }

            if (member.root != null && 
                member.root.isPlaceHolder &&
                member.root.iD == workingMember.root.iD &&
                !workingMember.root.isPlaceHolder)
                member.root = workingMember.root;

            if (member.parent != null &&
                member.parent.isPlaceHolder && 
                member.parent.iD == workingMember.parent.iD &&
                !workingMember.parent.isPlaceHolder)
                member.parent = workingMember.parent;

            if (workingMember.subMembers != null)
            {
                member.subMembers = new DojoMemberCollection(workingMember.subMembers.Count);
                foreach (DojoMember subMember in workingMember.subMembers)
                    member.subMembers.Add(subMember);
            }
        }

        #endregion

        #region Processing Methods

        public bool ProcessTrees()
        {
            if (isLoaded && !isTreeProcessed)
            {
                // Make the Member Tree, the result needs to be saved for lookups.
                memberDictionary = TreeMaker.Make(members, TreeMaker.MakeMode.Flat);
                
                // Make the MemberType Tree, the result doesn't need saved since the
                // MemberTypeTemplate Tree is indexed by hash value;
                TreeMaker.Make(memberTypeTemplates, TreeMaker.MakeMode.Flat);
                
                isTreeProcessed = true;

                return true;
            }

            return false;
        }

        public bool ProcessHashes()
        {
            if (isTreeProcessed && !isHashProcessed)
            {
                HashBuilder.Build(members);
                memberTypeTemplateDictionary = new Dictionary<string, DojoMemberTypeTemplateCollection>();
                HashBuilder.Build(memberTypeTemplates, memberTypeTemplateDictionary);

                foreach (DojoMember member in members)
                {
                    member.MemberTypeTreeHash = member.treeHash;
                    member.Root = member.treeRoot;
                }

                foreach (DojoMemberTypeTemplate template in memberTypeTemplates)
                {
                    template.MemberTypeTreeHash = template.treeHash;
                    template.Root = template.treeRoot;
                }

                isHashProcessed = true;

                return true;
            }

            return false;
        }

        public void ClearAll()
        {
            orderBuilder.ClearAll();
            invoiceBuilder.ClearAll();
            members = null;
            memberDictionary = null;
            memberTypeTemplates = null;
            memberTypeTemplateDictionary = null;
            isLoaded = false;
            isTreeProcessed = false;
            isHashProcessed = false;
        }

        #endregion

        #region Availability Processing

        /// <summary>
        /// Gets the templates available to the member.
        /// </summary>
        /// <param name="member">Member to check; this member must be directly
        /// from the MembershipBuilder.</param>
        /// <returns>Available templates.</returns>
        public List<MembershipPackage> GetMembershipPackages(DojoMember member)
        {
            DojoMember workingMember;
            DojoMemberTypeTemplateCollection rawTypeTemplates;
            DojoMemberTypeTemplateCollection memberTypeTemplates;
            List<MembershipPackage> packages;
            
            // THIS MUST USE THE MEMBER FROM MEMBERSHIP BUILDER
            // OR THIS WILL NOT FUNCTION PROPERLY
            if (member.treeHash != null && member.treeHash.Length > 0)
            {
                workingMember = member;
            }
            else
            {
                // Try to find the member in the builder trees
                if (!memberDictionary.TryGetValue(member.iD,
                    out workingMember))
                {
                    throw new MembershipBuilderException(
                        Localization.Strings.MembershipBuilder_CannotFindMember);
                }
            } 

            if (memberTypeTemplateDictionary.TryGetValue(workingMember.treeHash,
                out rawTypeTemplates))
            {            
                memberTypeTemplates = filterTemplates(workingMember, rawTypeTemplates);
            }
            else
            {
                memberTypeTemplates = new DojoMemberTypeTemplateCollection();
            }

            packages = new List<MembershipPackage>(memberTypeTemplates.Count);
            foreach (DojoMemberTypeTemplate template in memberTypeTemplates)
            {
                MembershipPackage p = new MembershipPackage();
                p.Build(member, template, this);
                packages.Add(p);
            }

            return packages;
        }
        
        #region Filter Checks

        private DojoMemberTypeTemplateCollection
            filterTemplates(DojoMember member,
            DojoMemberTypeTemplateCollection rawTemplates)
        {
            DojoMemberTypeTemplateCollection templates =
                new DojoMemberTypeTemplateCollection();

            if (member != null && rawTemplates != null)
            {
                foreach (DojoMemberTypeTemplate template in rawTemplates)
                {
                    if (filterCheck(member, template))
                    {
                        templates.Add(template);
                    }
                }
            }

            return templates;
        }

        /// <summary>
        /// Returns a collection of Membership Templates that currently can be used
        /// by the provided member. This is used in processing which memberships in
        /// a Member Type Template can be applied to a member. The template is first
        /// checked to see if the member matches.
        /// </summary>
        /// <param name="member">Member to examine.</param>
        /// <param name="template">Template to apply.</param>
        /// <returns></returns>
        internal DojoMembershipTemplateCollection
            getMembershipTemplates(DojoMember member, 
            DojoMemberTypeTemplate template)
        {
            DojoMembershipTemplateCollection templates
                = new DojoMembershipTemplateCollection();

            if (member != null && template != null)
            {
                if (filterCheck(member, template.MembershipTemplate1))
                    templates.Add(template.MembershipTemplate1);

                if (filterCheck(member, template.MembershipTemplate2))
                    templates.Add(template.MembershipTemplate2);

                if (filterCheck(member, template.MembershipTemplate3))
                    templates.Add(template.MembershipTemplate3);

                if (filterCheck(member, template.MembershipTemplate4))
                    templates.Add(template.MembershipTemplate4);

                if (filterCheck(member, template.MembershipTemplate5))
                    templates.Add(template.MembershipTemplate5);
            }

            return templates;
        }

        /// <summary>
        /// Tests a Member Type Template against a member.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        private bool filterCheck(DojoMember member, 
            DojoMemberTypeTemplate template)        
        {
            if (template == null)
                return false;

            if (member == null)
                return false;

            // Only root templates are available!
            if (template.Parent != null)
                return false;

            if (template.AgeYearsMin != -1 &&
                member.PrivateContact.BirthDate.
                    AddYears(template.AgeYearsMin) >
                buildTime)
                return false;

            if (template.AgeYearsMax != 0 &&
                member.PrivateContact.BirthDate.
                    AddYears(template.AgeYearsMax) <
                buildTime)
                return false;

            if (template.MemberForMin != 0 &&
                buildTime.Subtract(member.MemberSince).Days <
                template.MemberForMin)
                return false;

            if (template.MemberForMax != 0 &&
                buildTime.Subtract(member.MemberSince).Days >
                template.MemberForMax)
                return false;

            if (template.RankMin != null &&
                member.Rank != null &&
                member.Rank.OrderNum <
                template.RankMin.OrderNum)
                return false;

            if (template.RankMax != null &&
                member.Rank != null &&
                member.Rank.OrderNum >
                template.RankMax.OrderNum)
                return false;

            return true;
        }

        /// <summary>
        /// Tests a Membership Template against the member.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        private bool filterCheck(DojoMember member, 
            DojoMembershipTemplate template)
        {
            if (template == null)
                return false;

            if (member == null)
                return false;

            if (template.AgeYearsMin != -1 &&
                member.PrivateContact.BirthDate.
                    AddYears(template.AgeYearsMin) >
                buildTime)
                return false;

            if (template.AgeYearsMax != 0 &&
                member.PrivateContact.BirthDate.
                    AddYears(template.AgeYearsMax) <
                buildTime)
                return false;

            if (template.MemberForMin != 0 &&
                buildTime.Subtract(member.MemberSince).Days <
                template.MemberForMin)
                return false;

            if (template.MemberForMax != 0 &&
                buildTime.Subtract(member.MemberSince).Days >
                template.MemberForMax)
                return false;

            if (template.RankMin != null &&
                member.Rank != null &&
                member.Rank.OrderNum <
                template.RankMin.OrderNum)
                return false;

            if (template.RankMax != null &&
                member.Rank != null &&
                member.Rank.OrderNum >
                template.RankMax.OrderNum)
                return false;

            return true;
        }

        #endregion

        #endregion
    }
}
