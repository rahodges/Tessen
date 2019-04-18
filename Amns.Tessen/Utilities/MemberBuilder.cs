using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amns.GreyFox.People;
using Amns.GreyFox.Security;
using Amns.GreyFox.Utilities;
using Amns.Rappahanock;
using Amns.Rappahanock.Utilities;

namespace Amns.Tessen.Utilities
{
    public class MemberBuilder
    {
        public static DojoMember GetMember(GreyFoxUser user)
        {
            DojoMemberManager memberManager;
            DojoMemberCollection members;
            memberManager = new DojoMemberManager();
            members = memberManager.GetCollection(
                "UserAccountID=" + user.ID.ToString(), string.Empty);
            if (members.Count > 0)
                return members[0];
            return null;
        }

        /// <summary>
        /// Tries to switches a member's user account to the one specified. If
        /// the account cannot be found or if the account is already associated
        /// with an existing customer, then create a new user account for the 
        /// member if allowed.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="username"></param>
        /// <param name="allowCreate">Allows the creation of a new user account 
        /// if one cannot be found.</param>
        /// <returns></returns>
        public static bool UpdateUser(DojoMember member, string username, bool allowCreate)
        {
            GreyFoxUser user;

            user = SecurityManager.GetUser(username);
            
            if (user != null)
                return UpdateUser(member, user);
            if (allowCreate)
                return createUser(member, username);
            
            return false;
        }

        /// <summary>
        /// Tries to switches a member's user account to the one specified. If
        /// the account cannot be found or if the account is already associated
        /// with an existing member, then create a new user account for the 
        /// member if allowed. This will also switch the customer account if the
        /// user account specified is tied to a different customer account.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="user"></param>
        /// <param name="allowCreate">Allows the creation of a new user account 
        /// if one cannot be found.</param>
        /// <returns></returns>
        public static bool UpdateUser(DojoMember member, 
            GreyFoxUser user)
        {
            DojoMember existingMember;
            RHCustomer customer;

            existingMember = GetMember(user);

            if (existingMember == null)
            {
                member.UserAccount = user;

                // Update Customer Reference
                customer = CustomerBuilder.GetCustomer(user);
                if (customer != null)
                    if (member.Customer == null ||
                        member.Customer.ID != customer.ID)
                        member.Customer = customer;

                return true;
            }
            else if (existingMember.ID == member.ID)
            {
                return true; // Member is the same;
            }

            return false;
        }                

        /// <summary>
        /// Creates a user account for a member and associates it with the
        /// member. The member must call Save() on the customer after this
        /// process.
        /// </summary>
        /// <param name="member">The member to make an account for.</param>
        /// <param name="username">The username to associate with the account.</param>
        /// <returns></returns>
        private static bool createUser(DojoMember member, string username)
        {
            GreyFoxUser user;

            if (member.UserAccount != null)
            {
                return false;
            }

            user = SecurityManager.GetUser(username);

            if (user == null)
            {
                user = new GreyFoxUser();

                user.Contact = new GreyFoxContact(GreyFoxUserManager.ContactTable);
                member.PrivateContact.CopyValuesTo(user.Contact, false);

                user.UserName = username;
                user.RandomizePassword(7);
                user.ActivationID = DateTime.Now.GetHashCode().ToString();

                member.UserAccount = user;

                // TODO: SEND EMAIL TO USER?
            }

            if (member.MemberTypeTemplate != null && 
                member.MemberTypeTemplate.InitialRole != null)
                user.Roles.Add(member.MemberTypeTemplate.InitialRole);

            return true;
        }
    }
}
