//using System;
//using System.Drawing;
//
//namespace Amns.GreyFox.Tessen.Reporting
//{
//	/// <summary>
//	/// Summary description for AttendanceReport.
//	/// </summary>
//	public class AttendanceReport
//	{
//		string						__title				= "Dojo";
//		DojoMember					__member			= null;
//		DojoAttendanceCollection	__attendance		= null;
//		Bitmap						__attendanceCard	= null;
//
//		public AttendanceReport(DojoMember member)
//		{
//			__member = member;
//		}
//
//		public void Initialize()
//		{
//			DojoAttendanceEntryManager aem = new DojoAttendanceEntryManager(member.ConnectionString);
//			__attendance = aem.GetCollection("MemberID=" + member.ID.ToString(), "ClassStart", DojoAttendanceEntryFlags.Class);
//		}
//
//		public void GenerateAttendanceCard()
//		{
//			Bitmap b = new Bitmap(0,
//			Graphics g 
//
//		}
//	}
//}
