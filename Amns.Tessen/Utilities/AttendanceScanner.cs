/* ****************************************************** 
 * Amns.Tessen
 * Copyright © 2004 Roy A.E. Hodges. All Rights Reserved.
 * ****************************************************** */

using System;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace Amns.Tessen.Utilities
{
	/// <summary>
	/// Summary description for AttendanceScanner.
	/// </summary>
	public class AttendanceScanner
	{
		private string connectionString;

		public AttendanceScanner()
		{
			this.connectionString = Amns.GreyFox.Data.ManagerCore.GetInstance().ConnectionString;
		}

		public AttendanceScan RunMemberAttendanceScan(DojoMember member, TimeSpan maxDaySpan)
		{
			AttendanceScan aScan = new AttendanceScan(member, maxDaySpan);
			aScan.RunScan();

			TimeSpan timeInMembership = 
				TimeSpan.FromHours(aScan.TotalHours + aScan.TotalBulkHours);
			TimeSpan timeInRank = 
				TimeSpan.FromHours(aScan.TotalWeightedHoursInRank + aScan.TotalBulkHoursInRank);
			DateTime lastSignin = 
				aScan.LastSignin;

			member.TimeInRank = timeInRank;
			member.TimeInMembership = timeInMembership;
			member.LastAttendanceScan = DateTime.Now;
			member.LastSignin = lastSignin;
			member.Instructor1 = aScan.Instructor1;
			member.Instructor2 = aScan.Instructor2;
			member.Instructor3 = aScan.Instructor3;

			return aScan;
		}

		/// <summary>
		/// Runs an attendance scan on the entire membership database.
		/// </summary>
		/// <param name="defaultClassSpan">The TimeSpan for classes without a specific training time credit.</param>
		/// <param name="maxDaySpan">The maximum TimeSpan allowed for a single day.</param>
		public void RunAttendanceScan(TimeSpan maxDaySpan)
		{
			DojoMember member;
			DojoMemberManager memberManager = new DojoMemberManager();
			DojoMemberCollection members =
				memberManager.GetCollection(string.Empty, string.Empty, null);

			for(int x = 0; x < members.Count; x++)
			{
				member = members[x];
				if(member.IsPrimaryOrgActive)
					RunMemberAttendanceScan(member, maxDaySpan);
				member.Save();
			}
		}
	}
}