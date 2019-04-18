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
	public class DojoTestListStatus : ICloneable, IComparable
	{
		#region Private Fields

		internal int iD = 0;
		internal bool isPlaceHolder;		// Placeholders only store an ID; marked as unsynced.
		internal bool isSynced;				// Shows that data is synced with database.
		internal string name;
		internal string description;
		internal int orderNum;
		internal bool isDraft;
		internal bool isFinal;
		internal bool isComplete;
		internal bool teacherEditingEnabled;
		internal DojoTestListStatus onFinalized;
		internal DojoTestListStatus onCompleted;

		#endregion

		#region Public Properties

		/// <summary>
		/// DojoTestListStatus Record ID, assigned by database. Readonly.
		/// </summary>
		public int ID
		{
			get
			{
				return iD;
			}
		}

		/// <summary>
		/// Identifies the DojoTestListStatus as a Placeholder. Placeholders only contain 
		/// a DojoTestListStatus ID. Record late-binds data when it is accessed.
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
		public string Name
		{
			get
			{
				EnsurePreLoad();
				return name;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= name == value;
				name = value;
			}
		}

		/// <summary>
		/// </summary>
		public string Description
		{
			get
			{
				EnsurePreLoad();
				return description;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= description == value;
				description = value;
			}
		}

		/// <summary>
		/// </summary>
		public int OrderNum
		{
			get
			{
				EnsurePreLoad();
				return orderNum;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= orderNum == value;
				orderNum = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool IsDraft
		{
			get
			{
				EnsurePreLoad();
				return isDraft;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= isDraft == value;
				isDraft = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool IsFinal
		{
			get
			{
				EnsurePreLoad();
				return isFinal;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= isFinal == value;
				isFinal = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool IsComplete
		{
			get
			{
				EnsurePreLoad();
				return isComplete;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= isComplete == value;
				isComplete = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool TeacherEditingEnabled
		{
			get
			{
				EnsurePreLoad();
				return teacherEditingEnabled;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= teacherEditingEnabled == value;
				teacherEditingEnabled = value;
			}
		}

		/// <summary>
		/// </summary>
		public DojoTestListStatus OnFinalized
		{
			get
			{
				EnsurePreLoad();
				return onFinalized;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(onFinalized == null)
					{
						return;
					}
					else
					{
						onFinalized = value;
						isSynced = false;
					}
				}
				else
				{
					if(onFinalized != null && value.ID == onFinalized.ID)
					{
						return; 
					}
					else
					{
						onFinalized = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoTestListStatus OnCompleted
		{
			get
			{
				EnsurePreLoad();
				return onCompleted;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(onCompleted == null)
					{
						return;
					}
					else
					{
						onCompleted = value;
						isSynced = false;
					}
				}
				else
				{
					if(onCompleted != null && value.ID == onCompleted.ID)
					{
						return; 
					}
					else
					{
						onCompleted = value;
						isSynced = false;
					}
				}
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Instantiates a new instance of DojoTestListStatus.
		/// </summary>
		public DojoTestListStatus()
		{
		}

		public DojoTestListStatus(int id)
		{
			this.iD = id;
			isSynced = DojoTestListStatusManager._fill(this);
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

			DojoTestListStatusManager._fill(this);
			isPlaceHolder = false;
		}

		/// <summary>
		/// Saves the DojoTestListStatus object state to the database.
		/// </summary>
		public int Save()
		{

			if(isSynced)
				return iD;

			if(iD == -1) throw (new Exception("Invalid record; cannot be saved."));
			if(iD == 0)
				iD = DojoTestListStatusManager._insert(this);
			else
				DojoTestListStatusManager._update(this);
			isSynced = iD != -1;
			return iD;
		}

		public void Delete()
		{
			DojoTestListStatusManager._delete(this.iD);
			this.iD = 0;
			isSynced = false;
		}
		/// <summary>
		/// Duplicates DojoTestListStatus object into a database; may or may not be the same database
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoTestListStatus object reflecting the replicated DojoTestListStatus object.</returns>
		public DojoTestListStatus Duplicate()
		{
			DojoTestListStatus clonedDojoTestListStatus = this.Clone();

			// Insert must be called after children are replicated!
			clonedDojoTestListStatus.iD = DojoTestListStatusManager._insert(clonedDojoTestListStatus);
			clonedDojoTestListStatus.isSynced = true;
			return clonedDojoTestListStatus;
		}

		/// <summary>
		/// Overwrites and existing DojoTestListStatus object in the database.
		/// </summary>
		public void Overwrite(int id)
		{
			iD = id;
			DojoTestListStatusManager._update(this);
			isSynced = true;
		}

		/// <summary>
		/// Clones DojoTestListStatus object and clones child objects with cloning or replication.
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoTestListStatus object reflecting the replicated DojoTestListStatus object.</returns>
		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Clones DojoTestListStatus object and clones child objects with cloning or replication.
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoTestListStatus object reflecting the replicated DojoTestListStatus object.</returns>
		public DojoTestListStatus Clone()
		{
			DojoTestListStatus clonedDojoTestListStatus = new DojoTestListStatus();
			clonedDojoTestListStatus.iD = iD;
			clonedDojoTestListStatus.isSynced = isSynced;
			clonedDojoTestListStatus.name = name;
			clonedDojoTestListStatus.description = description;
			clonedDojoTestListStatus.orderNum = orderNum;
			clonedDojoTestListStatus.isDraft = isDraft;
			clonedDojoTestListStatus.isFinal = isFinal;
			clonedDojoTestListStatus.isComplete = isComplete;
			clonedDojoTestListStatus.teacherEditingEnabled = teacherEditingEnabled;


			if(onFinalized != null)
				clonedDojoTestListStatus.onFinalized = onFinalized;

			if(onCompleted != null)
				clonedDojoTestListStatus.onCompleted = onCompleted;

			return clonedDojoTestListStatus;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoTestListStatus.
		/// </summary>
		/// <returns> A new DojoTestListStatus object reflecting the cloned DojoTestListStatus object.</returns>
		public DojoTestListStatus Copy()
		{
			DojoTestListStatus dojoTestListStatus = new DojoTestListStatus();
			CopyTo(dojoTestListStatus);
			return dojoTestListStatus;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoTestListStatus.
		/// </summary>
		/// <returns> A new DojoTestListStatus object reflecting the cloned DojoTestListStatus object.</returns>
		/// <param name="isolation">Placeholders are used to isolate the DojoTestListStatus from its children.</param>
		public DojoTestListStatus Copy(bool isolation)
		{
			DojoTestListStatus dojoTestListStatus = new DojoTestListStatus();
			CopyTo(dojoTestListStatus, isolation);
			return dojoTestListStatus;
		}

		/// <summary>
		/// Deep copies the current DojoTestListStatus to another instance of DojoTestListStatus.
		/// This method does not provide isolated copies; use overriden method for this feature.
		/// </summary>
		/// <param name="DojoTestListStatus">The DojoTestListStatus to copy to.</param>
		public void CopyTo(DojoTestListStatus dojoTestListStatus)
		{
			CopyTo(dojoTestListStatus, false);
		}

		/// <summary>
		/// Deep copies the current DojoTestListStatus to another instance of DojoTestListStatus.
		/// </summary>
		/// <param name="DojoTestListStatus">The DojoTestListStatus to copy to.</param>
		/// <param name="isolation">Placeholders are used to isolate the DojoTestListStatus from its children.</param>
		public void CopyTo(DojoTestListStatus dojoTestListStatus, bool isolation)
		{
			dojoTestListStatus.iD = iD;
			dojoTestListStatus.isPlaceHolder = isPlaceHolder;
			dojoTestListStatus.isSynced = isSynced;
			dojoTestListStatus.name = name;
			dojoTestListStatus.description = description;
			dojoTestListStatus.orderNum = orderNum;
			dojoTestListStatus.isDraft = isDraft;
			dojoTestListStatus.isFinal = isFinal;
			dojoTestListStatus.isComplete = isComplete;
			dojoTestListStatus.teacherEditingEnabled = teacherEditingEnabled;
			if(onFinalized != null)
			{
				if(isolation)
				{
					dojoTestListStatus.onFinalized = onFinalized.NewPlaceHolder();
				}
				else
				{
					dojoTestListStatus.onFinalized = onFinalized.Copy(false);
				}
			}
			if(onCompleted != null)
			{
				if(isolation)
				{
					dojoTestListStatus.onCompleted = onCompleted.NewPlaceHolder();
				}
				else
				{
					dojoTestListStatus.onCompleted = onCompleted.Copy(false);
				}
			}
		}

		public DojoTestListStatus NewPlaceHolder()
		{
			DojoTestListStatus dojoTestListStatus = new DojoTestListStatus();
			dojoTestListStatus.iD = iD;
			dojoTestListStatus.isPlaceHolder = true;
			dojoTestListStatus.isSynced = true;
			return dojoTestListStatus;
		}

		public static DojoTestListStatus NewPlaceHolder(int iD)
		{
			DojoTestListStatus dojoTestListStatus = new DojoTestListStatus();
			dojoTestListStatus.iD = iD;
			dojoTestListStatus.isPlaceHolder = true;
			dojoTestListStatus.isSynced = true;
			return dojoTestListStatus;
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
			DojoTestListStatus dojoTestListStatus = (DojoTestListStatus) obj;
			return this.iD - dojoTestListStatus.iD;
		}

		/// <summary>
		/// Compares the object's ID to another object's ID.
		/// </summary>
		public int CompareTo(DojoTestListStatus dojoTestListStatus)
		{
			return this.iD - dojoTestListStatus.iD;
		}

		public override int GetHashCode()
		{
			return iD.GetHashCode();
		}

		#endregion

		//--- Begin Custom Code ---
																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																													
		public override string ToString()
		{
			return name;
		}

		//--- End Custom Code ---
	}
}
