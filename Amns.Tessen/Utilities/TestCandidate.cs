/* ****************************************************** 
 * Amns.Tessen
 * Copyright © 2004 Roy A.E. Hodges. All Rights Reserved.
 * ****************************************************** */

using System;

namespace Amns.Tessen.Utilities
{
	/// <summary>
	/// Summary description for TestListCandidate.
	/// </summary>
	public class TestCandidate
	{
		DojoMember _member;

		DojoTestListJournalEntryCollection _eligibility;
		DojoTestListJournalEntryCollection _certification;

		DojoTestListJournalEntry _lastEntry;

		bool _isRemoved;
		
		public DojoMember Member
		{
			get { return _member; }
			set { _member = value; }
		}

		public DojoTestListJournalEntryCollection Eligibility
		{
			get { return _eligibility; }
			set { _eligibility = value; }
		}

		public DojoTestListJournalEntryCollection Certification
		{
			get { return _certification; }
			set { _eligibility = value; }
		}
		
		public DojoTestListJournalEntry LastEntry
		{
			get { return _lastEntry; }
			set { _lastEntry = value; }
		}
		
		public DojoTestListJournalEntryType Status
		{
			get { return _lastEntry.EntryType; }
		}

		public bool IsRemoved
		{
			get { return _isRemoved; }
			set { _isRemoved = value; }
		}

		public TestCandidate(DojoMember member)
		{
			_member = member;
			_eligibility = new DojoTestListJournalEntryCollection();
			_certification = new DojoTestListJournalEntryCollection();
		}
	}
}