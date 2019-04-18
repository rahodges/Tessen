/* ********************************************************** *
 * AMNS NitroCast v1.0 Class Object Business Tier               *
 * Autogenerated by NitroCast © 2007 Roy A.E Hodges             *
 * All Rights Reserved                                        *
 * ---------------------------------------------------------- *
 * Source code may not be reproduced or redistributed without *
 * written expressed permission from the author.              *
 * Permission is granted to modify source code by licencee.   *
 * These permissions do not extend to third parties.          *
 * ********************************************************** */

using System;

namespace Amns.Tessen
{
	/// <summary>
	/// Summary of MyClass
	/// </summary>
	public class DojoBulkAttendanceEntry : ICloneable, IComparable
	{
		#region Private Fields

		internal int iD = 0;
		internal bool isPlaceHolder;		// Placeholders only store an ID; marked as unsynced.
		internal bool isSynced;				// Shows that data is synced with database.
		internal DateTime startDate;
		internal DateTime endDate;
		internal TimeSpan duration;
		internal DojoMember member;
		internal DojoRank rank;

		#endregion

		#region Public Properties

		/// <summary>
		/// DojoBulkAttendanceEntry Record ID, assigned by database. Readonly.
		/// </summary>
		public int ID
		{
			get
			{
				return iD;
			}
		}

		/// <summary>
		/// Identifies the DojoBulkAttendanceEntry as a Placeholder. Placeholders only contain 
		/// a DojoBulkAttendanceEntry ID. Record late-binds data when it is accessed.
		/// </summary>
		public bool IsPlaceHolder
		{
			get
			{
				return isPlaceHolder;
			}
		}

		/// <summary>
		/// True if the object is synced with the database.
		/// </summary>
		public bool IsSynced
		{
			get
			{
				return isSynced;
			}
			set
			{
				if(value == true)
				{
					throw (new Exception("Cannot set IsSynced to true."));
				}
				isSynced = value;
			}
		}

		/// <summary>
		/// </summary>
		public DateTime StartDate
		{
			get
			{
				EnsurePreLoad();
				return startDate;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= startDate == value;
				startDate = value;
			}
		}

		/// <summary>
		/// </summary>
		public DateTime EndDate
		{
			get
			{
				EnsurePreLoad();
				return endDate;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= endDate == value;
				endDate = value;
			}
		}

		/// <summary>
		/// </summary>
		public TimeSpan Duration
		{
			get
			{
				EnsurePreLoad();
				return duration;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= duration == value;
				duration = value;
			}
		}

		/// <summary>
		/// </summary>
		public DojoMember Member
		{
			get
			{
				EnsurePreLoad();
				return member;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(member == null)
					{
						return;
					}
					else
					{
						member = value;
						isSynced = false;
					}
				}
				else
				{
					if(member != null && value.ID == member.ID)
					{
						return; 
					}
					else
					{
						member = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoRank Rank
		{
			get
			{
				EnsurePreLoad();
				return rank;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(rank == null)
					{
						return;
					}
					else
					{
						rank = value;
						isSynced = false;
					}
				}
				else
				{
					if(rank != null && value.ID == rank.ID)
					{
						return; 
					}
					else
					{
						rank = value;
						isSynced = false;
					}
				}
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Instantiates a new instance of DojoBulkAttendanceEntry.
		/// </summary>
		public DojoBulkAttendanceEntry()
		{
		}

		public DojoBulkAttendanceEntry(int id)
		{
			this.iD = id;
			isSynced = DojoBulkAttendanceEntryManager._fill(this);
		}
		#endregion

		#region Default NitroCast Methods

		/// <summary>
		/// Ensures that the object's fields and children are 
		/// pre-loaded before any updates or reads.
		/// </summary>
		public void EnsurePreLoad()
		{
			if(!isPlaceHolder)
				return;

			DojoBulkAttendanceEntryManager._fill(this);
			isPlaceHolder = false;
		}

		/// <summary>
		/// Saves the DojoBulkAttendanceEntry object state to the database.
		/// </summary>
		public int Save()
		{
			if(member != null)
				member.Save();
			if(rank != null)
				rank.Save();

			if(isSynced)
				return iD;

			if(iD == -1) throw (new Exception("Invalid record; cannot be saved."));
			if(iD == 0)
				iD = DojoBulkAttendanceEntryManager._insert(this);
			else
				DojoBulkAttendanceEntryManager._update(this);
			isSynced = iD != -1;
			return iD;
		}

		public void Delete()
		{
			DojoBulkAttendanceEntryManager._delete(this.iD);
			this.iD = 0;
			isSynced = false;
		}
		/// <summary>
		/// Duplicates DojoBulkAttendanceEntry object into a database; may or may not be the same database
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoBulkAttendanceEntry object reflecting the replicated DojoBulkAttendanceEntry object.</returns>
		public DojoBulkAttendanceEntry Duplicate()
		{
			DojoBulkAttendanceEntry clonedDojoBulkAttendanceEntry = this.Clone();

			// Insert must be called after children are replicated!
			clonedDojoBulkAttendanceEntry.iD = DojoBulkAttendanceEntryManager._insert(clonedDojoBulkAttendanceEntry);
			clonedDojoBulkAttendanceEntry.isSynced = true;
			return clonedDojoBulkAttendanceEntry;
		}

		/// <summary>
		/// Overwrites and existing DojoBulkAttendanceEntry object in the database.
		/// </summary>
		public void Overwrite(int id)
		{
			iD = id;
			DojoBulkAttendanceEntryManager._update(this);
			isSynced = true;
		}

		/// <summary>
		/// Clones DojoBulkAttendanceEntry object and clones child objects with cloning or replication.
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoBulkAttendanceEntry object reflecting the replicated DojoBulkAttendanceEntry object.</returns>
		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Clones DojoBulkAttendanceEntry object and clones child objects with cloning or replication.
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoBulkAttendanceEntry object reflecting the replicated DojoBulkAttendanceEntry object.</returns>
		public DojoBulkAttendanceEntry Clone()
		{
			DojoBulkAttendanceEntry clonedDojoBulkAttendanceEntry = new DojoBulkAttendanceEntry();
			clonedDojoBulkAttendanceEntry.iD = iD;
			clonedDojoBulkAttendanceEntry.isSynced = isSynced;
			clonedDojoBulkAttendanceEntry.startDate = startDate;
			clonedDojoBulkAttendanceEntry.endDate = endDate;
			clonedDojoBulkAttendanceEntry.duration = duration;


			if(member != null)
				clonedDojoBulkAttendanceEntry.member = member;

			if(rank != null)
				clonedDojoBulkAttendanceEntry.rank = rank;

			return clonedDojoBulkAttendanceEntry;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoBulkAttendanceEntry.
		/// </summary>
		/// <returns> A new DojoBulkAttendanceEntry object reflecting the cloned DojoBulkAttendanceEntry object.</returns>
		public DojoBulkAttendanceEntry Copy()
		{
			DojoBulkAttendanceEntry dojoBulkAttendanceEntry = new DojoBulkAttendanceEntry();
			CopyTo(dojoBulkAttendanceEntry);
			return dojoBulkAttendanceEntry;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoBulkAttendanceEntry.
		/// </summary>
		/// <returns> A new DojoBulkAttendanceEntry object reflecting the cloned DojoBulkAttendanceEntry object.</returns>
		/// <param name="isolation">Placeholders are used to isolate the DojoBulkAttendanceEntry from its children.</param>
		public DojoBulkAttendanceEntry Copy(bool isolation)
		{
			DojoBulkAttendanceEntry dojoBulkAttendanceEntry = new DojoBulkAttendanceEntry();
			CopyTo(dojoBulkAttendanceEntry, isolation);
			return dojoBulkAttendanceEntry;
		}

		/// <summary>
		/// Deep copies the current DojoBulkAttendanceEntry to another instance of DojoBulkAttendanceEntry.
		/// This method does not provide isolated copies; use overriden method for this feature.
		/// </summary>
		/// <param name="DojoBulkAttendanceEntry">The DojoBulkAttendanceEntry to copy to.</param>
		public void CopyTo(DojoBulkAttendanceEntry dojoBulkAttendanceEntry)
		{
			CopyTo(dojoBulkAttendanceEntry, false);
		}

		/// <summary>
		/// Deep copies the current DojoBulkAttendanceEntry to another instance of DojoBulkAttendanceEntry.
		/// </summary>
		/// <param name="DojoBulkAttendanceEntry">The DojoBulkAttendanceEntry to copy to.</param>
		/// <param name="isolation">Placeholders are used to isolate the DojoBulkAttendanceEntry from its children.</param>
		public void CopyTo(DojoBulkAttendanceEntry dojoBulkAttendanceEntry, bool isolation)
		{
			dojoBulkAttendanceEntry.iD = iD;
			dojoBulkAttendanceEntry.isPlaceHolder = isPlaceHolder;
			dojoBulkAttendanceEntry.isSynced = isSynced;
			dojoBulkAttendanceEntry.startDate = startDate;
			dojoBulkAttendanceEntry.endDate = endDate;
			dojoBulkAttendanceEntry.duration = duration;
			if(member != null)
			{
				if(isolation)
				{
					dojoBulkAttendanceEntry.member = member.NewPlaceHolder();
				}
				else
				{
					dojoBulkAttendanceEntry.member = member.Copy(false);
				}
			}
			if(rank != null)
			{
				if(isolation)
				{
					dojoBulkAttendanceEntry.rank = rank.NewPlaceHolder();
				}
				else
				{
					dojoBulkAttendanceEntry.rank = rank.Copy(false);
				}
			}
		}

		public DojoBulkAttendanceEntry NewPlaceHolder()
		{
			DojoBulkAttendanceEntry dojoBulkAttendanceEntry = new DojoBulkAttendanceEntry();
			dojoBulkAttendanceEntry.iD = iD;
			dojoBulkAttendanceEntry.isPlaceHolder = true;
			dojoBulkAttendanceEntry.isSynced = true;
			return dojoBulkAttendanceEntry;
		}

		public static DojoBulkAttendanceEntry NewPlaceHolder(int iD)
		{
			DojoBulkAttendanceEntry dojoBulkAttendanceEntry = new DojoBulkAttendanceEntry();
			dojoBulkAttendanceEntry.iD = iD;
			dojoBulkAttendanceEntry.isPlaceHolder = true;
			dojoBulkAttendanceEntry.isSynced = true;
			return dojoBulkAttendanceEntry;
		}

		private void childrenCollection_Changed(object sender, System.EventArgs e)
		{
			isSynced = false;
		}

		#endregion

		#region IComparable Methods

		/// <summary>
		/// Compares the object's ID to another object's ID.
		/// </summary>
		int IComparable.CompareTo(object obj)
		{
			DojoBulkAttendanceEntry dojoBulkAttendanceEntry = (DojoBulkAttendanceEntry) obj;
			return this.iD - dojoBulkAttendanceEntry.iD;
		}

		/// <summary>
		/// Compares the object's ID to another object's ID.
		/// </summary>
		public int CompareTo(DojoBulkAttendanceEntry dojoBulkAttendanceEntry)
		{
			return this.iD - dojoBulkAttendanceEntry.iD;
		}

		public override int GetHashCode()
		{
			return iD.GetHashCode();
		}

		#endregion

		//--- Begin Custom Code ---
																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																											
		public string Name
		{
			get
			{
				return member.PrivateContact.FullName;
			}
		}

		//--- End Custom Code ---
	}
}
