/* ****************************************************** 
 * Amns.Tessen
 * Copyright © 2004 Roy A.E. Hodges. All Rights Reserved.
 * ****************************************************** */

using System;
using System.Text;

namespace Amns.Tessen.Utilities
{
	/// <summary>
	/// Summary description for MemberExporter.
	/// </summary>
	public class SeminarExporter
	{
		public SeminarExporter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string ConstructThirdPartyMailingList(DojoSeminarRegistrationCollection registrations)
		{
			StringBuilder s = new StringBuilder();

			s.Append("\"ID\"," +
				"\"Seminar\"," +
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
				"\"BadAddressMemo\"\r\n");

			foreach(DojoSeminarRegistration registration in registrations)
			{
				// Skip members without a contact
				if(registration.Contact == null)
					continue;

				s.Append("\"");
				
				s.Append(string.Join("\",\"", 
					new string[] { 
						registration.ID.ToString(),
						registration.ParentSeminar.Name,
						registration.Contact.FullName,
						registration.Contact.FirstName,
						registration.Contact.MiddleName,
						registration.Contact.LastName,
						registration.Contact.Suffix,
						registration.Contact.Address1,
						registration.Contact.Address2,
						registration.Contact.City,
						registration.Contact.StateProvince,
						registration.Contact.PostalCode,
						registration.Contact.Country,
						registration.Contact.ValidationMemo
					}));

				s.Append("\"\r\n");
			}

			return s.ToString();
		}
	}
}
