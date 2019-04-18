/* ****************************************************** 
 * Amns.Tessen
 * Copyright © 2004 Roy A.E. Hodges. All Rights Reserved.
 * ****************************************************** */

using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Amns.GreyFox.People;

namespace Amns.Tessen.Utilities
{
	/// <summary>
	/// Summary description for MembershipScan.
	/// </summary>
	public class MembershipScan
	{
		private bool _testMode;
		private TimeSpan _gracePeriod;
		private DojoOrganization _primaryOrganization;
		private DojoOrganization _parentOrganization;

		public TimeSpan GracePeriod
		{
			get { return _gracePeriod; }
			set { _gracePeriod = value; }
		}

		public DojoOrganization PrimaryOrganization
		{
			get { return _primaryOrganization; }
			set { _primaryOrganization = value; }
		}

		public DojoOrganization ParentOrganization
		{
			get { return _parentOrganization; }
			set { _parentOrganization = value; }
		}

		public MembershipScan()
		{
			_testMode = true;
			_gracePeriod = TimeSpan.FromDays(14);
			_primaryOrganization = 
				DojoOrganization.NewPlaceHolder(1);
		}

        public void PromotionAdjustment()
        {
            DojoPromotionManager promotionManager;
            DojoPromotionCollection promotions;

            promotionManager = new DojoPromotionManager();
            promotions = promotionManager.GetCollection(string.Empty,
                string.Empty, DojoPromotionFlags.Member);

            // Loop through promotions and promote member to higher ranks incrementally.
            // Fix their attendance while doing so if required.
            foreach (DojoPromotion promotion in promotions)
            {
                if (promotion.PromotionDate > promotion.Member.RankDate)
                {
                    promotion.Member.Rank = promotion.PromotionRank;
                    promotion.Member.RankDate = promotion.PromotionDate;
                    promotion.Member.Save();

                    // WHOAH! WAY SLOW!
                    AttendanceAdjustment(promotion.Member);
                }
            }
        }

        ///// <summary>
        ///// Applies the promotion to the attendance database. This will update the ranks on
        ///// the promotion member's attendance using the dates of the member's attendance. 
        ///// This is necissary to do, because the attendance tracker may have recorded incorrect
        ///// ranks before the promotion was entered into the database and after the actual test.
        ///// </summary>
        ///// <param name="promotion"></param>
        //public void AttendanceAdjustment(DojoPromotion promotion)
        //{
        //    DojoPromotionManager promotionManager;
        //    DojoPromotion nextPromotion;
        //    DojoAttendanceEntryManager attendanceManager;
        //    DojoAttendanceEntryCollection promotionAttendance;

        //    // Try to find member's next promotion if it exists.
        //    promotionManager = new DojoPromotionManager();
        //    attendanceManager = new DojoAttendanceEntryManager();

        //    nextPromotion =
        //        promotionManager.FindPromotionByMember(promotion.Member.iD,
        //        promotion.PromotionRank.PromotionRank.iD);

        //    if (nextPromotion == null)
        //        promotionAttendance = attendanceManager.GetCollection(
        //            "DojoClass.ClassStart>=#" + promotion.PromotionDate + "#" +
        //            " AND MemberID=" + promotion.member.iD,
        //            string.Empty, DojoAttendanceEntryFlags.Class);
        //    else
        //        promotionAttendance = attendanceManager.GetCollection(
        //            "DojoClass.ClassStart>=#" + promotion.PromotionDate + "#" +
        //            " AND DojoClass.ClassStart<=#" + nextPromotion.PromotionDate.Subtract(TimeSpan.FromDays(1)) + "#" +
        //            " AND MemberID=" + promotion.member.iD,
        //            string.Empty, DojoAttendanceEntryFlags.Class);

        //    for (int x = 0; x < promotionAttendance.Count; x++)
        //    {
        //        if (promotionAttendance[x].Rank.iD != promotion.PromotionRank.iD)
        //        {
        //            promotionAttendance[x].Rank = promotion.PromotionRank;
        //            promotionAttendance[x].Save();
        //        }
        //    }
        //}

		public void AttendanceAdjustment(DojoMember member)
		{	
            DateTime nextPromotionDate;
            int promotionFirstClassIndex;
            DojoPromotion promotion;
			DojoPromotionManager promotionManager;
			DojoPromotionCollection promotions;
            DojoAttendanceEntryManager attendanceManager;
            DojoAttendanceEntryCollection attendance;

            Database database;
            DbCommand dbCommand;
            
            database = DatabaseFactory.CreateDatabase();

            // Load member promotions oldest to newest
            promotionManager = new DojoPromotionManager();
            promotions = promotionManager.GetCollection(
                "MemberID=" + member.ID.ToString(),
                "PromotionDate",
                DojoPromotionFlags.PromotionRank);
			
            // If the member has no promotions set all attendance
            // for the member to the member's rank.
            if(promotions.Count == 0)
			{                
                dbCommand = database.GetSqlStringCommand(
                    "UPDATE kitTessen_Attendance " +
                    "SET RankID=@RankID " +
                    "WHERE MemberID=@MemberID;");
                database.AddInParameter(dbCommand, "@MemberID", DbType.Int32, member.ID);
                database.AddInParameter(dbCommand, "@RankID", DbType.Int32, member.Rank.ID);
				database.ExecuteNonQuery(dbCommand);
                return;
			}            
           
            // Load Attendance oldest to newest
            attendanceManager = new DojoAttendanceEntryManager();
            attendance = attendanceManager.GetCollection("MemberID=" +
                member.ID.ToString(), "ClassStart",
                DojoAttendanceEntryFlags.Class);

            //                  0                            1                 2
            //                  First Promotion >>>>>>>>>>>> Next Promotion
            //   DON'T CHANGE   11/04/2003                   5/2/2007          1/3/2008
            //   ...............p............................p.................p
            //   beginner       rokyu                        sankyu            nikyu
            //
            //   12/13/2001     11/04/2003                   5/2/2007        1/1/2008
            //   c c  c    c    c   c            c   c c c ccc              cc   c c cc c
            //   0 1  2    3    4   5            6   7 8 9 012              34   5 6 78 9
            //   0=========================================1=============================

            // Loop through the classes and find the first class
            // of the first promotion
            promotionFirstClassIndex = 0;
            for(int i = 0; i < attendance.Count; i++)
            {
                // i=4, 11/04/2003 >= 11/04/2003 true
                if(attendance[i].Class.ClassStart >=
                    promotions[0].PromotionDate)
                {
                    promotionFirstClassIndex = i;
                    break;
                }
            }

            // Loop through promotions - oldest to newest
            for(int p = 0; p < promotions.Count; p++)
            {
                promotion = promotions[p];
        
                // Find when the next promotion starts, otherwise
                // if there is no next promotion change all ranks
                // on the classes to the current rank.
                // 0 + 1 = 1 < 3 True
                // 2 + 1 = 3 < 3 False
                if(p + 1 < promotions.Count)
                {
                    // Preload the next promotion date.
                    // 5/2/2007
                    nextPromotionDate = 
                        promotions[p + 1].PromotionDate;

                    // Find first class of next promotion. The last
                    // class of the current promotion is therefore
                    // one behind this one. (-1)
                    // i=4; i<15
                    for(int i = promotionFirstClassIndex;
                        i < attendance.Count; i++)
                    {   
                        // [04] 11/04/2003 < 5/2/2007 True
                        // [10] 04/31/2003 < 5/2/2007 True
                        // [12] 05/02/2007 < 5/2/2007 False
                        if(attendance[i].Class.ClassStart >=
                            nextPromotionDate)
                        {
                            promotionFirstClassIndex = i;
                            break;
                        }

                        if (attendance[i].rank.iD !=
                            promotion.promotionRank.iD)
                        {
                            // use internals for speed
                            attendance[i].rank =
                                promotion.promotionRank;
                            attendance[i].isSynced = false;
                            attendance[i].Save();
                        }
                    }
                }
            }
		}

		/// <summary>
		/// Processes dues invoices, payments and organization memberships
		/// </summary>
		/// <returns>Returns active student count.</returns>
		public int RunDuesScan()
		{
			bool isActive = false;
			bool isParentActive = false;
			bool isPastDue = false;		
			int activeCount = 0;

			DojoMemberManager mm = 
				new DojoMemberManager();
			DojoMemberCollection members = 
				mm.GetCollection(string.Empty, string.Empty, null);

			DojoMembershipManager membershipManager = 
				new DojoMembershipManager();
			DojoMembershipCollection memberships =
				membershipManager.GetCollection(string.Empty, string.Empty,
				DojoMembershipFlags.InvoiceLine,
				DojoMembershipFlags.InvoiceLineInvoice);

			foreach(DojoMember member in members)
			{
				isActive = false;
				isParentActive = false;
				isPastDue = false;				

				// Search Memberships
				foreach(DojoMembership membership in memberships)
				{
					if(membership.Member.ID == member.ID)
					{
						// Check Primary Organization Membership
						if(membership.Organization.ID == _primaryOrganization.ID)
						{	
							isActive |= activityCheck(membership);
							isPastDue |= pastDueCheck(membership);
						}

						if(membership.Organization.ID == _parentOrganization.ID)
						{
							isParentActive |= activityCheck(membership);
						}
					}					
				}

				if(member.IsPrimaryOrgActive != isActive |
					member.IsPastDue != isPastDue)
				{
					member.IsPrimaryOrgActive = isActive;
					member.IsPastDue = isPastDue;
					member.LastMembershipScan = DateTime.Now;

					if(!_testMode)
					{
						member.Save();
					}
				}

				if(isActive)
				{
					activeCount++;
				}
			}

			return activeCount;
		}

		/// <summary>
		/// Checks a membership for active status.
		/// </summary>
		/// <param name="membership">Dojo membership to check.</param>
		/// <returns>Returns true if the membership is active, otherwise false.</returns>
		private bool activityCheck(DojoMembership membership)
		{
			// Activate on active membership that is paid.
			// Activate on active membership that is unpaid and not yet due.
			// Activate on active membership that is noninvoiced.
			if(membership.StartDate <= DateTime.Now &
				membership.EndDate >= DateTime.Now)
			{
				if(membership.InvoiceLine != null)
				{
					if(membership.InvoiceLine.Invoice.BalanceRemaining == 0 |
						membership.InvoiceLine.Invoice.DueDate > DateTime.Now)
					{
						return true;
					}
				}
				else
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Checks a membership for past due status.
		/// </summary>
		/// <param name="membership">Membership to check.</param>
		/// <returns>Returns true if the membership is past due, otherwise false.</returns>
		private bool pastDueCheck(DojoMembership membership)
		{
			// Past due on balance remaining and past due.
			if(membership.InvoiceLine != null)
			{
				if(membership.InvoiceLine.Invoice.BalanceRemaining > 0 &
					membership.InvoiceLine.Invoice.DueDate <= DateTime.Now)
				{
					return true;
				}
			}

			return false;
		}
	}
}