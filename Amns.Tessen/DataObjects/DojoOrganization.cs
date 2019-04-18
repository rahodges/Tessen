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
using Amns.GreyFox.People;

namespace Amns.Tessen
{
	/// <summary>
	/// Summary of MyClass
	/// </summary>
	public class DojoOrganization : ICloneable, IComparable
	{
		#region Private Fields

		internal int iD = 0;
		internal bool isPlaceHolder;		// Placeholders only store an ID; marked as unsynced.
		internal bool isSynced;				// Shows that data is synced with database.
		internal string name;
		internal string description;
		internal GreyFoxContact location;
		internal GreyFoxContactCollection classLocations;
		internal GreyFoxContact administrativeContact;
		internal string webServiceUrl;
		internal TimeSpan refreshTime;
		internal bool isPrimary;
		internal DojoMemberType defaultMemberType;
		internal bool promotionFlagEnabled;

		#endregion

		#region Public Properties

		/// <summary>
		/// DojoOrganization Record ID, assigned by database. Readonly.
		/// </summary>
		public int ID
		{
			get
			{
				return iD;
			}
		}

		/// <summary>
		/// Identifies the DojoOrganization as a Placeholder. Placeholders only contain 
		/// a DojoOrganization ID. Record late-binds data when it is accessed.
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
		/// Name of Organization
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
		public GreyFoxContact Location
		{
			get
			{
				EnsurePreLoad();
				return location;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(location == null)
					{
						return;
					}
					else
					{
						location = value;
						isSynced = false;
					}
				}
				else
				{
					if(value.TableName != "kitTessen_Locations") throw(new Exception("Cannot set Location. Table names mismatched."));
					if(location != null && value.ID == location.ID)
					{
						return; 
					}
					else
					{
						location = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// Class locations for organization.
		/// </summary>
		public GreyFoxContactCollection ClassLocations
		{
			get
			{
				EnsurePreLoad();
				if(classLocations == null)
				{
					DojoOrganizationManager.FillClassLocations(this);
					classLocations.CollectionChanged += new System.EventHandler(childrenCollection_Changed);
				}
				return classLocations;
			}
			set
			{
				EnsurePreLoad();
				if(!object.Equals(classLocations, value))
				{
					if(value == null)
						ClassLocations = new GreyFoxContactCollection();
					else
						classLocations = value;
					classLocations.CollectionChanged += new System.EventHandler(childrenCollection_Changed);
					isSynced = false;
				}
			}
		}

		/// <summary>
		/// </summary>
		public GreyFoxContact AdministrativeContact
		{
			get
			{
				EnsurePreLoad();
				return administrativeContact;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(administrativeContact == null)
					{
						return;
					}
					else
					{
						administrativeContact = value;
						isSynced = false;
					}
				}
				else
				{
					if(value.TableName != "sysGlobal_Contacts") throw(new Exception("Cannot set AdministrativeContact. Table names mismatched."));
					if(administrativeContact != null && value.ID == administrativeContact.ID)
					{
						return; 
					}
					else
					{
						administrativeContact = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public string WebServiceUrl
		{
			get
			{
				EnsurePreLoad();
				return webServiceUrl;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= webServiceUrl == value;
				webServiceUrl = value;
			}
		}

		/// <summary>
		/// </summary>
		public TimeSpan RefreshTime
		{
			get
			{
				EnsurePreLoad();
				return refreshTime;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= refreshTime == value;
				refreshTime = value;
			}
		}

		/// <summary>
		/// New members will automatically be assigned a membership in this organization. 
		/// Only one primary organization can be specified.
		/// </summary>
		public bool IsPrimary
		{
			get
			{
				EnsurePreLoad();
				return isPrimary;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= isPrimary == value;
				isPrimary = value;
			}
		}

		/// <summary>
		/// </summary>
		public DojoMemberType DefaultMemberType
		{
			get
			{
				EnsurePreLoad();
				return defaultMemberType;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(defaultMemberType == null)
					{
						return;
					}
					else
					{
						defaultMemberType = value;
						isSynced = false;
					}
				}
				else
				{
					if(defaultMemberType != null && value.ID == defaultMemberType.ID)
					{
						return; 
					}
					else
					{
						defaultMemberType = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// When enabled, the promotion scanner will take into account the member's 
		/// activity in the organization. If the member is not active in the organization, 
		/// their promotion will be flagged.
		/// </summary>
		public bool PromotionFlagEnabled
		{
			get
			{
				EnsurePreLoad();
				return promotionFlagEnabled;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= promotionFlagEnabled == value;
				promotionFlagEnabled = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Instantiates a new instance of DojoOrganization.
		/// </summary>
		public DojoOrganization()
		{
		}

		public DojoOrganization(int id)
		{
			this.iD = id;
			isSynced = DojoOrganizationManager._fill(this);
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

			DojoOrganizationManager._fill(this);
			isPlaceHolder = false;
		}

		/// <summary>
		/// Saves the DojoOrganization object state to the database.
		/// </summary>
		public int Save()
		{
			if(classLocations != null)
				foreach(GreyFoxContact item in classLocations)
					item.Save();
			if(administrativeContact != null)
				administrativeContact.Save();

			if(isSynced)
				return iD;

			if(iD == -1) throw (new Exception("Invalid record; cannot be saved."));
			if(iD == 0)
				iD = DojoOrganizationManager._insert(this);
			else
				DojoOrganizationManager._update(this);
			isSynced = iD != -1;
			return iD;
		}

		public void Delete()
		{
			DojoOrganizationManager._delete(this.iD);
			this.iD = 0;
			isSynced = false;
		}
		/// <summary>
		/// Duplicates DojoOrganization object into a database; may or may not be the same database
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoOrganization object reflecting the replicated DojoOrganization object.</returns>
		public DojoOrganization Duplicate()
		{
			DojoOrganization clonedDojoOrganization = this.Clone();

			// Insert must be called after children are replicated!
			clonedDojoOrganization.iD = DojoOrganizationManager._insert(clonedDojoOrganization);
			clonedDojoOrganization.isSynced = true;
			return clonedDojoOrganization;
		}

		/// <summary>
		/// Overwrites and existing DojoOrganization object in the database.
		/// </summary>
		public void Overwrite(int id)
		{
			iD = id;
			DojoOrganizationManager._update(this);
			isSynced = true;
		}

		/// <summary>
		/// Clones DojoOrganization object and clones child objects with cloning or replication.
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoOrganization object reflecting the replicated DojoOrganization object.</returns>
		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Clones DojoOrganization object and clones child objects with cloning or replication.
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoOrganization object reflecting the replicated DojoOrganization object.</returns>
		public DojoOrganization Clone()
		{
			DojoOrganization clonedDojoOrganization = new DojoOrganization();
			clonedDojoOrganization.iD = iD;
			clonedDojoOrganization.isSynced = isSynced;
			clonedDojoOrganization.name = name;
			clonedDojoOrganization.description = description;
			clonedDojoOrganization.webServiceUrl = webServiceUrl;
			clonedDojoOrganization.refreshTime = refreshTime;
			clonedDojoOrganization.isPrimary = isPrimary;
			clonedDojoOrganization.promotionFlagEnabled = promotionFlagEnabled;


			if(location != null)
				clonedDojoOrganization.location = location;

			if(classLocations != null)
				clonedDojoOrganization.classLocations = classLocations.Clone();

			if(administrativeContact != null)
				clonedDojoOrganization.administrativeContact = administrativeContact;

			if(defaultMemberType != null)
				clonedDojoOrganization.defaultMemberType = defaultMemberType;

			return clonedDojoOrganization;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoOrganization.
		/// </summary>
		/// <returns> A new DojoOrganization object reflecting the cloned DojoOrganization object.</returns>
		public DojoOrganization Copy()
		{
			DojoOrganization dojoOrganization = new DojoOrganization();
			CopyTo(dojoOrganization);
			return dojoOrganization;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoOrganization.
		/// </summary>
		/// <returns> A new DojoOrganization object reflecting the cloned DojoOrganization object.</returns>
		/// <param name="isolation">Placeholders are used to isolate the DojoOrganization from its children.</param>
		public DojoOrganization Copy(bool isolation)
		{
			DojoOrganization dojoOrganization = new DojoOrganization();
			CopyTo(dojoOrganization, isolation);
			return dojoOrganization;
		}

		/// <summary>
		/// Deep copies the current DojoOrganization to another instance of DojoOrganization.
		/// This method does not provide isolated copies; use overriden method for this feature.
		/// </summary>
		/// <param name="DojoOrganization">The DojoOrganization to copy to.</param>
		public void CopyTo(DojoOrganization dojoOrganization)
		{
			CopyTo(dojoOrganization, false);
		}

		/// <summary>
		/// Deep copies the current DojoOrganization to another instance of DojoOrganization.
		/// </summary>
		/// <param name="DojoOrganization">The DojoOrganization to copy to.</param>
		/// <param name="isolation">Placeholders are used to isolate the DojoOrganization from its children.</param>
		public void CopyTo(DojoOrganization dojoOrganization, bool isolation)
		{
			dojoOrganization.iD = iD;
			dojoOrganization.isPlaceHolder = isPlaceHolder;
			dojoOrganization.isSynced = isSynced;
			dojoOrganization.name = name;
			dojoOrganization.description = description;
			if(location != null)
			{
				if(isolation)
				{
					dojoOrganization.location = location.NewPlaceHolder();
				}
				else
				{
					dojoOrganization.location = location.Copy(false);
				}
			}
			if(classLocations != null)
			{
				if(isolation)
				{
					dojoOrganization.classLocations = classLocations.Copy(true);
				}
				else
				{
					dojoOrganization.classLocations = classLocations.Copy(false);
				}
			}
			if(administrativeContact != null)
			{
				if(isolation)
				{
					dojoOrganization.administrativeContact = administrativeContact.NewPlaceHolder();
				}
				else
				{
					dojoOrganization.administrativeContact = administrativeContact.Copy(false);
				}
			}
			dojoOrganization.webServiceUrl = webServiceUrl;
			dojoOrganization.refreshTime = refreshTime;
			dojoOrganization.isPrimary = isPrimary;
			if(defaultMemberType != null)
			{
				if(isolation)
				{
					dojoOrganization.defaultMemberType = defaultMemberType.NewPlaceHolder();
				}
				else
				{
					dojoOrganization.defaultMemberType = defaultMemberType.Copy(false);
				}
			}
			dojoOrganization.promotionFlagEnabled = promotionFlagEnabled;
		}

		public DojoOrganization NewPlaceHolder()
		{
			DojoOrganization dojoOrganization = new DojoOrganization();
			dojoOrganization.iD = iD;
			dojoOrganization.isPlaceHolder = true;
			dojoOrganization.isSynced = true;
			return dojoOrganization;
		}

		public static DojoOrganization NewPlaceHolder(int iD)
		{
			DojoOrganization dojoOrganization = new DojoOrganization();
			dojoOrganization.iD = iD;
			dojoOrganization.isPlaceHolder = true;
			dojoOrganization.isSynced = true;
			return dojoOrganization;
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
			DojoOrganization dojoOrganization = (DojoOrganization) obj;
			return this.iD - dojoOrganization.iD;
		}

		/// <summary>
		/// Compares the object's ID to another object's ID.
		/// </summary>
		public int CompareTo(DojoOrganization dojoOrganization)
		{
			return this.iD - dojoOrganization.iD;
		}

		public override int GetHashCode()
		{
			return iD.GetHashCode();
		}

		#endregion

		//--- Begin Custom Code ---
																																																																																																																																																																								
		public override string ToString()
		{
			EnsurePreLoad();
			return name;
		}

		//--- End Custom Code ---
	}
}