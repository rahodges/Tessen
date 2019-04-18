using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amns.Tessen.Utilities
{
    public sealed class TreeMaker
    {
        public enum MakeMode
        {
            Flat,
            Collapse
        }

        public static Dictionary<int, DojoMemberTypeTemplate>
            Make(DojoMemberTypeTemplateCollection templates, MakeMode style)
        {
            Dictionary<int, DojoMemberTypeTemplate> dic;
            DojoMemberTypeTemplate template;

            if (templates == null || templates.Count == 0)
                return new Dictionary<int, DojoMemberTypeTemplate>();

            if (templates.Count > 0 && templates[0].isPlaceHolder)                
                throw new TreeMakerException("Templates must not be placeholders.");

            dic = new Dictionary<int, DojoMemberTypeTemplate>(templates.Count);

            for (int i = 0; i < templates.Count; i++)
            {
                dic.Add(templates[i].iD, templates[i]);
            }

            for (int i = 0; i < templates.Count; i++)
            {
                template = templates[i];

                if (template.parent != null)
                {
                    template.parent = dic[template.parent.iD];

                    if (template.parent.subTemplates == null)
                        template.parent.subTemplates = new
                            DojoMemberTypeTemplateCollection();

                    template.parent.subTemplates.Add(template);
                }
            }

            if (style == MakeMode.Collapse)
            {
                for (int i = 0; i < templates.Count; i++)
                {
                    if (templates[i].parent == null)
                    {
                        dic.Remove(templates[i].iD);
                    }
                }
            }

            return dic;
        }

        public static Dictionary<int, DojoMember>
            Make(DojoMemberCollection members, MakeMode style)
        {
            Dictionary<int, DojoMember> dic;
            DojoMember member;

            if (members == null || members.Count == 0)
                return new Dictionary<int, DojoMember>();

            if (members.Count > 0 && members[0].isPlaceHolder)
                throw new TreeMakerException("Members must not be placeholders.");

            dic = new Dictionary<int, DojoMember>(members.Count);

            for (int i = 0; i < members.Count; i++)
            {
                dic.Add(members[i].iD, members[i]);
            }

            for (int i = 0; i < members.Count; i++)
            {
                member = members[i];

                if (member.parent != null)
                {
                    member.parent = dic[member.parent.iD];

                    if (member.parent.subMembers == null)
                        member.parent.subMembers = 
                            new DojoMemberCollection();

                    member.parent.subMembers.Add(member);
                }
            }

            if (style == MakeMode.Collapse)
            {
                for (int i = 0; i < members.Count; i++)
                {
                    if (members[i].parent == null)
                    {
                        dic.Remove(members[i].iD);
                    }
                }
            }

            return dic;
        }
    }
}
