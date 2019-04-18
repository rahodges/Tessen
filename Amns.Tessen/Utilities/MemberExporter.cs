/* ****************************************************** 
 * Amns.Tessen
 * Copyright © 2004 Roy A.E. Hodges. All Rights Reserved.
 * ****************************************************** */

using System;
using System.Text;
using Amns.GreyFox.People;

namespace Amns.Tessen.Utilities
{
	/// <summary>
	/// Summary description for MemberExporter.
	/// </summary>
	public class MemberExporter
	{
		public MemberExporter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string ConstructThirdPartyMailingList(DojoMemberCollection members)
		{
			StringBuilder s = new StringBuilder();

			s.Append("\"ID\"," +
				"\"Rank\"," +
				"\"FullName\"," +
				"\"FirstName\"," +
				"\"MiddleName\"," +
				"\"LastName\"," +
				"\"Suffix\"," + 
				"\"Address1\"," +
				"\"Address2\"," +
				"\"City\"," +
				"\"StateProvince\"," +
				"\"PostalCode\"," +
				"\"Country\"," +
				"\"BadAddress\"," +
				"\"BadAddressMemo\"\r\n");

			foreach(DojoMember member in members)
			{
				// Skip members without a contact
				if(member.PrivateContact == null)
					continue;

				s.Append("\"");
				
				s.Append(string.Join("\",\"", 
					new string[] { 
						member.ID.ToString(),
						member.Rank.Name,
						member.PrivateContact.FullName,
						member.PrivateContact.FirstName,
						member.PrivateContact.MiddleName,
						member.PrivateContact.LastName,
						member.PrivateContact.Suffix,
						member.PrivateContact.Address1,
						member.PrivateContact.Address2,
						member.PrivateContact.City,
						member.PrivateContact.StateProvince,
						member.PrivateContact.PostalCode,
						member.PrivateContact.Country,
						((member.PrivateContact.ValidationFlags & GreyFoxContactValidationFlag.BadAddress) 
                        == GreyFoxContactValidationFlag.BadAddress).ToString(),
						member.PrivateContact.ValidationMemo
					}));

				s.Append("\"\r\n");
			}

			return s.ToString();
		}
	}
}
