/* ****************************************************** 
 * Amns.Tessen
 * Copyright © 2004 Roy A.E. Hodges. All Rights Reserved.
 * ****************************************************** */

using System;
using System.Data;
using System.Data.OleDb;
using Amns.GreyFox.Scheduling;

namespace Amns.Tessen.Utilities
{
	/// <summary>
	/// AttendanceScan retreives member statistics on the fly. It also monitors statistics,
	/// by subscribing to the AttendanceManager when statistics are modified for this member.
	/// </summary>
	public class AttendanceScan
	{
		private DojoMember member;

		private double totalBulkHours = 0;
		private double totalBulkHoursInRank = 0;
		private double totalWeightedHours = 0;
		private double totalWeightedHoursInRank = 0;
		private double totalHours = 0;
		private double totalHoursInRank = 0;
		private double totalHoursThisWeek = 0;
		private double totalHoursLastWeek = 0;

		private DojoMember instructor1;
		private DojoMember instructor2;
		private DojoMember instructor3;

		private double maxDayHours = 1;

		private DateTime lastSignin = DateTime.MinValue;

		public double TotalBulkHours { get { return totalBulkHours; } }
		public double TotalBulkHoursInRank { get { return totalBulkHoursInRank; } }
		public double TotalWeightedHours { get { return totalWeightedHours; } }
		public double TotalWeightedHoursInRank { get { return totalWeightedHoursInRank; } }
		public double TotalHours { get { return totalHours; } }
		public double TotalHoursInRank { get { return totalHoursInRank; } }
		public double TotalHoursThisWeek { get { return totalHoursThisWeek; } }
		public double TotalHoursLastWeek { get { return totalHoursLastWeek; } }
		public DateTime LastSignin { get { return lastSignin; } }

		public DojoMember Instructor1 { get { return instructor1; } }
		public DojoMember Instructor2 { get { return instructor2; } }
		public DojoMember Instructor3 { get { return instructor3; } }

		public AttendanceScan(DojoMember member, TimeSpan maxDayHours)
		{
			this.member = member;
			this.maxDayHours = maxDayHours.TotalHours;
		}

		public void RunScan()
		{
			DojoBulkAttendanceEntryManager bulkManager;
			DojoBulkAttendanceEntryCollection bulkAttendance;
			DojoAttendanceEntryManager m;
			DojoAttendanceEntryCollection attendance;
			double classLength;
			DateTime dayIndex;
			double dayWeightedHours;
			double dayWeightedHoursInRank;			
			DateTime firstDayOfWeek;
			DateTime lastDayOfWeek;
			DateTime firstDayOfLastWeek;
			DateTime lastDayOfLastWeek;

			bulkManager = new DojoBulkAttendanceEntryManager();

			bulkAttendance =
				bulkManager.GetCollection("MemberID=" + member.iD, string.Empty, null);

			// Load the attendance entry collection and be sure to sort by ClassStart
			// so that the system can calculate weighted hours properly.
			m = new DojoAttendanceEntryManager();
			attendance =
				m.GetCollection("MemberID=" + member.iD, "ClassStart", DojoAttendanceEntryFlags.Class);


			// Clear Data
			this.totalBulkHours = 0;
			this.totalBulkHoursInRank = 0;
			this.totalHours = 0;
			this.totalHoursInRank = 0;
			this.totalWeightedHours = 0;
			this.totalWeightedHoursInRank = 0;
			this.totalHoursThisWeek = 0;
			this.totalHoursLastWeek = 0;

			classLength = 0;
			dayIndex = DateTime.MinValue;
			dayWeightedHours = 0;
			dayWeightedHoursInRank = 0;		

			firstDayOfWeek = DateManipulator.FirstOfWeek(DateTime.Now);
			lastDayOfWeek = firstDayOfWeek.Add(new TimeSpan(6, 23, 59, 59, 999));
			firstDayOfLastWeek = DateManipulator.FirstOfWeek(DateTime.Now.Subtract(TimeSpan.FromDays(7)));
			lastDayOfLastWeek = firstDayOfLastWeek.Add(new TimeSpan(6, 23, 59, 59, 999));

			// Scan Bulk Hours
			for(int x = 0; x < bulkAttendance.Count; x++)
			{
				totalBulkHours += bulkAttendance[x].Duration.TotalHours;

				if(bulkAttendance[x].rank.iD == member.rank.iD)
				{
					totalBulkHoursInRank += bulkAttendance[x].Duration.TotalHours;
				}
			}

			if(attendance.Count > 0)
			{
				dayIndex = attendance[0].Class.ClassStart.Date;
			}

			for(int x = 0; x < attendance.Count; x++)
			{
				classLength = (attendance[x].Class.ClassEnd - attendance[x].Class.ClassStart).TotalHours;

				// Total Hours
				totalHours += classLength;

				// Total Hours in Rank
				if(attendance[x].rank.iD == member.rank.iD)
				{
					totalHoursInRank += classLength;
				}

				// Total Weighted Hours
				if(attendance[x].Class.ClassStart.Date != dayIndex)
				{
					// Add Prior Temporary Values
					totalWeightedHours += dayWeightedHours;
					totalWeightedHoursInRank += dayWeightedHoursInRank;

					// Reset Hours for Day
					dayWeightedHours = 0;
					dayWeightedHoursInRank = 0;
					dayIndex = attendance[x].Class.ClassStart.Date;
				}
				
				dayWeightedHours += classLength;

				if(attendance[x].rank.iD == member.rank.iD)
				{
					dayWeightedHoursInRank += classLength;
				}

				if(dayWeightedHours > maxDayHours)
				{
					dayWeightedHours = maxDayHours;
				}

				if(dayWeightedHoursInRank > maxDayHours)
				{
					dayWeightedHoursInRank = maxDayHours;
				}

				if(attendance[x].Class.ClassStart >= firstDayOfLastWeek &&
					attendance[x].Class.ClassStart <= lastDayOfLastWeek)
				{
					totalHoursLastWeek += classLength;
				}

				if(attendance[x].Class.ClassStart >= firstDayOfWeek &&
					attendance[x].Class.ClassStart <= lastDayOfWeek)
				{
					totalHoursThisWeek += classLength;
				}

				// Update Last Signin
				if(attendance[x].signinTime > lastSignin)
				{
					lastSignin = attendance[x].signinTime;
				}
			}

			// Tally Remaining Temporary Values
			totalWeightedHours += dayWeightedHours;
			totalWeightedHoursInRank += dayWeightedHoursInRank;

			// Find Ninety Day Instructors
			DateTime maxDate = DateTime.Now;
			DateTime minDate = maxDate.Subtract(TimeSpan.FromDays(90));
			instructor1 = attendance.FindTopInstructor(minDate, maxDate, null);
			instructor2 = attendance.FindTopInstructor(minDate, maxDate, instructor1);
			instructor3 = attendance.FindTopInstructor(minDate, maxDate, instructor1, instructor2);
		}
	}
}
