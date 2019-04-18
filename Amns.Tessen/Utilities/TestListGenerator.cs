/* ****************************************************** 
 * Amns.Tessen
 * Copyright © 2004 Roy A.E. Hodges. All Rights Reserved.
 * ****************************************************** */

using System;
using Amns.Tessen;

namespace Amns.Tessen.Utilities
{
	/// <summary>
	/// Summary description for TestListGenerator.
	/// </summary>
	public class TestListGenerator
	{
		string _connectionString;
		DojoTestListStatus _draftStatus;
		DojoTestListJournalEntryType _journalTypeAdd;
		DojoTestListJournalEntryType _journalTypeRemove;

		public TestListGenerator(string connectionString)
		{
			_connectionString = connectionString;

			// Create Draft Status Types
			_draftStatus = 
				DojoTestListStatus.NewPlaceHolder(1);

			// Create Journal Types
			_journalTypeAdd = 
				DojoTestListJournalEntryType.NewPlaceHolder(1);
			_journalTypeRemove = 
				DojoTestListJournalEntryType.NewPlaceHolder(2);
		}

		public DojoMemberCollection GetEligibleMembers()
		{
			DojoTest tempTest = new DojoTest();
			tempTest.TestDate = DateTime.Now;
			return GetEligibleMembers(tempTest);
		}

		public DojoMemberCollection GetEligibleMembers(DojoTest test)
		{
			DojoMemberManager m;
			DojoMemberCollection members;
			DojoMemberCollection eligibleMembers;

			// Load active members
			eligibleMembers = new DojoMemberCollection();
			m = new DojoMemberManager();
			members = m.GetCollection("DojoMember.IsPrimaryOrgActive=true", 
				"DojoMember.RankID, DojoMember.RankDate DESC",
				new DojoMemberFlags[]
				{
					DojoMemberFlags.PrivateContact, 
					DojoMemberFlags.Rank
				});

			// Create temp test if none specified
			if(test == null)
			{
				test = new DojoTest();
				test.TestDate = DateTime.Now;
			}

			// Add members that satisfy requirements
			foreach(DojoMember member in members)
			{	
				if(member.Rank.PromotionRank != null)
				{
					if(member.TestEligibilityDate <= DateTime.Now &
						member.TestEligibilityHoursBalance >= TimeSpan.Zero)
					{
						eligibleMembers.Add(member);
					}
				}
			}

			return eligibleMembers;
		}

		public DojoTestList Generate()
		{
			return Generate(null);
		}

		public DojoTestList Generate(DojoTest test)
		{
			DojoMemberCollection eligibles;
			DojoTestList list;
			DojoTestListJournalEntry entry;
			
			eligibles = GetEligibleMembers(test);

			// Create List
			list = new DojoTestList();
			list.Editor = null;
			list.EditorComments = "";
			list.Status = _draftStatus;
			list.Test = test;
			list.Candidates = eligibles;
			list.CandidatesCompileDate = list.CreateDate;
			list.Save();

			// Create Journal Items
			foreach(DojoMember member in eligibles)
			{
				entry = new DojoTestListJournalEntry();
				
				if(member.TestEligibilityHoursBalance.TotalHours > 0)
				{
					entry.Comment = "Eligible for " + member.Rank.PromotionRank.Name + " on " +
						member.TestEligibilityDate.ToShortDateString() + " with an additional " +
						member.TestEligibilityHoursBalance.TotalHours.ToString("f") + " hours training.";
				}
				else
				{
					entry.Comment = "Newly eligible for " + member.Rank.PromotionRank.Name + " on " +
						member.TestEligibilityDate.ToShortDateString() + ".";
				}
				entry.Editor = null;
				entry.EntryType = _journalTypeAdd;
				entry.Member = member;
				entry.Promotion = null;
				entry.TestList = list;
				
				entry.Save();
			}

			test.ActiveTestList = list;
			test.Save();

			return list;
		}		

		public void RemoveMember(DojoTestList list, DojoMember member)
		{
			DojoTestListJournalEntry entry;

			entry = new DojoTestListJournalEntry();
				
			entry.Comment = "Removed member.";
			entry.Editor = null;
			entry.EntryType = _journalTypeRemove;
			entry.Member = member;
			entry.Promotion = null;
			entry.TestList = list;
				
			entry.Save();
		}

		public TestCandidateCollection BuildTestList(DojoTest test)
		{
			if(test.ActiveTestList != null)
			{
				return BuildTestList(test.ActiveTestList);
			}

			return null;
		}

		public TestCandidateCollection BuildTestList(DojoTestList list)
		{
			DojoTestListJournalEntryManager journal;
			DojoTestListJournalEntryCollection entries;
			DojoTestListJournalEntry entry;
			DojoTestListJournalEntry lastEntry;
			DojoTestListJournalEntryType type;
			TestCandidate candidate;
			TestCandidateCollection candidates;

			journal =
				new DojoTestListJournalEntryManager();
			
			// Get journal entries by testlist and be sure to sort by
			// member and create date. This will allow processing members
			// without nested loops.
			entries =
				journal.GetCollection("TestListID=" + list.ID.ToString(),
                "RankID, " +
                "LastName, " +
                "FirstName, " +
                "MemberID, " +
                "Member.CreateDate ASC", 
				DojoTestListJournalEntryFlags.Member,
				DojoTestListJournalEntryFlags.MemberPrivateContact);

			if(entries.Count > 0)
			{
				candidates = new TestCandidateCollection();
				candidate = new TestCandidate(entries[0].Member);
				lastEntry = entries[0];
				type = lastEntry.EntryType;
			
				for(int i = 0; i < entries.Count; i++)
				{
					entry = entries[i];

					// If the new entry's member does not match the
					// last member, then check to see if the last
					// member was left in the add state. If so, add
					// the member to the candidates list.
					// The candidate's last type becomes the status
					if(candidate.Member.ID != entry.Member.ID)
					{
						candidate.LastEntry = lastEntry;	
						candidates.Add(candidate);
						candidate = new TestCandidate(entry.Member);
					}

					lastEntry = entry;
					type = entry.EntryType;

					// Enable memberAdd flag if the entry
					// was added.
					if(entry.EntryType.Eligible)
					{
						candidate.IsRemoved = false;
						candidate.Eligibility.Add(entry);
					}

					// Disable memberAdd flag if the entry
					// was removed.
					if(entry.EntryType.Ineligible)
					{
						candidate.IsRemoved = true;
						candidate.Eligibility.Add(entry);
					}

					if(entry.EntryType.CertificateRequest |
						entry.EntryType.CertificatePending |
						entry.EntryType.CertificateReceived)
					{
						candidate.Certification.Add(entry);
					}
				}

				// Process Last Entry
				candidate.LastEntry = lastEntry;
				candidates.Add(candidate);
			}
			else
			{
				candidates = null;
			}

			return candidates;
		}

		public void CompileTestList(DojoTest test)
		{
			if(test.ActiveTestList != null)
			{
				CompileTestList(test.ActiveTestList);
			}
		}

		public void CompileTestList(DojoTestList list)
		{
			TestCandidateCollection buildResults;
			DojoMemberCollection compile;			
			TestCandidate candidate;
            		
			buildResults = BuildTestList(list);
			compile = new DojoMemberCollection(buildResults.Count);

			for(int i = 0; i < buildResults.Count; i++)
			{
				candidate = buildResults[i];

				if(!candidate.IsRemoved)
				{
					compile.Add(candidate.Member);
				}
			}

			list.Candidates = compile;
			list.CandidatesCompileDate = DateTime.Now;
			list.Save();
		}
	}
}