using System;
using System.Web.Services;

namespace Amns.Tessen.WebServices
{
	/// <summary>
	/// Summary description for ClassServices.
	/// </summary>
	public class MemberServices : WebService
	{
		public MemberServices()
		{						
		}

		[WebMethod]
		public DojoMember RequestMember(DojoOrganization organization, string organizationID)
		{
			DojoMembershipManager m = 
				new DojoMembershipManager();
			
			DojoMembershipCollection c =
				m.GetCollection("OrganizationID=" + organization.ID.ToString() + " AND " +
				"OrganizationMembershipID='" + organizationID + "'", 
				string.Empty,				
				DojoMembershipFlags.Member,
				DojoMembershipFlags.MemberMemberType,
				DojoMembershipFlags.MemberPrivateContact,				
				DojoMembershipFlags.MemberEmergencyContact);

			if(c.Count < 1)
				throw(new Exception("Cannot find member with the id requested."));
			
			if(c.Count > 1)
				throw(new Exception("More than one member exists with the id requested."));

            return c[0].Member;
		}
	}
}