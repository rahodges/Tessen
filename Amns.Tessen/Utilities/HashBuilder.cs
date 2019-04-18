using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Amns.Tessen.Utilities
{
    /// <summary>
    /// Builds hash tables on templates and members. This is Patent Pending.
    /// </summary>
    internal sealed class HashBuilder
    {
        static readonly int maxTreeIterations = 5;
        static readonly char[] treeIterationMarkers = new char[] { 'a', 'b', 'c', 'd', 'e', 'f' };

        static object lockObject = string.Empty;
        static bool isCompiling;
        static bool abortRequest;
        static int itemNumber = 0;
        static int itemCount = 0;

        #region Static Properties

        public static bool IsCompiling
        {
            get
            {
                lock (lockObject)
                {
                    return isCompiling;
                }
            }
        }

        public static int ItemNumber
        {
            get
            {
                lock (lockObject)
                {
                    return itemNumber;
                }
            }
        }

        public static int ItemCount
        {
            get
            {
                lock (lockObject)
                {
                    return itemCount;
                }
            }
        }

        #endregion

        public static bool Compile(Type type)
        {
            lock (lockObject)
            {
                if (!isCompiling)
                {
                    isCompiling = true;
                    Thread compilerThread = new Thread(compileTask);
                    compilerThread.Start(type);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool Abort()
        {
            lock (lockObject)
            {
                if (isCompiling)
                {
                    abortRequest = true;                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static void compileTask(object type)
        {
            if (type == null)
                return;

            if (type == typeof(DojoMemberTypeTemplateCollection) |
                type == typeof(DojoMemberTypeTemplate))
            {
                DojoMemberTypeTemplateCollection templates =
                    new DojoMemberTypeTemplateManager().GetCollection(
                    string.Empty,
                    string.Empty,
                    null);

                lock (lockObject)
                {
                    itemCount = templates.Count;
                }

                TreeMaker.Make(templates, TreeMaker.MakeMode.Flat);
                Build(templates);

                for(int i = 0; i < templates.Count; i++)
                {                    
                    lock (lockObject)
                    {
                        itemNumber = i;
                    }
                    
                    if (abortRequest)
                        break;

                    templates[i].MemberTypeTreeHash = templates[i].treeHash;
                    templates[i].Root = templates[i].treeRoot;
                    templates[i].Save(); 
                }
            }
            else if (type == typeof(DojoMemberCollection) |
                type == typeof(DojoMember))
            {
                DojoMemberCollection members =
                    new DojoMemberManager().GetCollection(
                    string.Empty,
                    "PrivateContact.LastName, PrivateContact.FirstName, PrivateContact.MiddleName",
                    DojoMemberFlags.PrivateContact);

                lock (lockObject)
                {
                    itemCount = members.Count;
                }

                TreeMaker.Make(members, TreeMaker.MakeMode.Flat);
                Build(members);

                for (int i = 0; i < members.Count; i++)
                {
                    lock (lockObject)
                    {
                        itemNumber = i;
                    }
                    if (abortRequest)
                        break;

                    members[i].MemberTypeTreeHash = members[i].treeHash;
                    members[i].Root = members[i].treeRoot;
                    members[i].Save();                    
                }
            }

            lock (lockObject)
            {
                isCompiling = false;
            }
        }

        #region DojoMemberTypeTemplate Hashing

        public static void Build(DojoMemberTypeTemplateCollection templates)
        {
            Build(templates, null);
        }

        public static void Build(DojoMemberTypeTemplateCollection templates, 
            Dictionary<string, DojoMemberTypeTemplateCollection> hashDictionary)
        {
            foreach (DojoMemberTypeTemplate template in templates)
            {
                // Reset the tree hash
                template.treeHash = string.Empty;

                // If there is no tree root for the template
                // set the template's root to the current template.
                if(template.treeRoot == null)
                    template.treeRoot = template;

                buildTypeHash(template, template, 0);

                if (hashDictionary != null)
                {
                    // Makes sure the dictionary has a collection for
                    // this tree hash.
                    if (!hashDictionary.ContainsKey(template.treeHash))
                    {
                        hashDictionary.Add(template.treeHash,
                            new DojoMemberTypeTemplateCollection());
                    }

                    // Add the template to the collection under the
                    // tree has dictionary.                
                    hashDictionary[template.treeHash].Add(template);
                }
            }
        }

        private static void buildTypeHash(
            DojoMemberTypeTemplate rootTemplate,
            DojoMemberTypeTemplate template, 
            int iteration)
        {
            // This code works correctly 7FEB08, Roy Hodges
            // DO NOT MODIFY!

            // If the root is not the template, this means that
            // we came into this method as an iteration, therefore
            // this template's root is higher, so we set the
            // template's root to the root.
            if (rootTemplate != template)
                template.treeRoot = rootTemplate;

            if (template.memberType == null)
            {
                rootTemplate.treeHash +=
                    treeIterationMarkers[iteration] +
                    "?";
            }
            else
            {
                rootTemplate.treeHash +=
                    treeIterationMarkers[iteration] +
                    template.memberType.iD.ToString();
            }

            if (template.subTemplates != null &&
                template.subTemplates.Count > 0 &&
                iteration < maxTreeIterations)
            {
                foreach (DojoMemberTypeTemplate subTemplate
                    in template.subTemplates)
                {
                    buildTypeHash(rootTemplate,
                        subTemplate, iteration + 1);
                }
            }
        }

        #endregion

        #region DojoMember Hashing

        public static void Build(DojoMemberCollection members)
        {
            // This code works correctly 7FEB08, Roy Hodges
            // DO NOT MODIFY!

            foreach (DojoMember member in members)
            {
                member.treeHash = string.Empty;
                if(member.treeRoot == null)
                    member.treeRoot = member;
                buildTypeHash(member, member, 0);
            }
        }

        private static void buildTypeHash(
            DojoMember rootMember, 
            DojoMember member, 
            int iteration)
        {
            // This code works correctly 7FEB08, Roy Hodges
            // DO NOT MODIFY!

            // If the root is not the member, this means that
            // we came into this method as an iteration, therefore
            // this member's root is higher, so we set the
            // member's root to the root.
            if (rootMember != member)
                member.treeRoot = rootMember;
            
            if (member.memberType == null)
            {
                rootMember.treeHash +=
                    treeIterationMarkers[iteration] +
                    "?";
            }
            else
            {
                rootMember.treeHash +=
                    treeIterationMarkers[iteration] +
                    member.memberType.iD.ToString();
            }

            if (member.subMembers != null &&
                member.subMembers.Count > 0 &&
                iteration < maxTreeIterations)
            {
                foreach (DojoMember subMember in member.subMembers)
                {
                    buildTypeHash(rootMember, subMember, iteration + 1);
                }
            }
        }

        #endregion
    }
}
