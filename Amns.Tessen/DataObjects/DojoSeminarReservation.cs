/* ********************************************************** *
 * AMNS DbModel v1.0 Class Object Business Tier               *
 * Copyright © 2003-2006 Roy A.E. Hodges                      *
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
	public class DojoSeminarReservation : ICloneable, IComparable, IRHSalesOrderLine
	{
		#region Private Fields

		internal int iD = 0;
		internal bool isPlaceHolder;		// Placeholders only store an ID; marked as unsynced.
		internal bool isSynced;				// Shows that data is synced with database.
		internal DojoSeminarRegistration registration;
		internal DojoSeminarReservation parentReservation;
		internal bool isBlockReservation;
		internal DateTime checkIn;
		internal bool checkOut;
		internal bool isClassReservation;
		internal DojoClass class1;
		internal DojoClass class2;
		internal DojoClass class3;
		internal DojoClass class4;
		internal DojoClass class5;
		internal DojoClass class6;
		internal DojoClass class7;
		internal DojoClass class8;
		internal DojoClass class9;
		internal DojoClass class10;
		internal bool isDefinitionReservation;
		internal DojoClassDefinition definition1;
		internal DojoClassDefinition definition2;
		internal DojoClassDefinition definition3;

		#endregion

		#region Public Properties

		/// <summary>
		/// DojoSeminarReservation Record ID, assigned by database. Readonly.
		/// </summary>
		public int ID
		{
			get
			{
				return iD;
			}
		}

		/// <summary>
		/// Identifies the DojoSeminarReservation as a Placeholder. Placeholders only contain 
		/// a DojoSeminarReservation ID. Record late-binds data when it is accessed.
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
		public DojoSeminarRegistration Registration
		{
			get
			{
				EnsurePreLoad();
				return registration;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(registration == null)
					{
						return;
					}
					else
					{
						registration = value;
						isSynced = false;
					}
				}
				else
				{
					if(registration != null && value.ID == registration.ID)
					{
						return; 
					}
					else
					{
						registration = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoSeminarReservation ParentReservation
		{
			get
			{
				EnsurePreLoad();
				return parentReservation;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(parentReservation == null)
					{
						return;
					}
					else
					{
						parentReservation = value;
						isSynced = false;
					}
				}
				else
				{
					if(parentReservation != null && value.ID == parentReservation.ID)
					{
						return; 
					}
					else
					{
						parentReservation = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public bool IsBlockReservation
		{
			get
			{
				EnsurePreLoad();
				return isBlockReservation;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= isBlockReservation == value;
				isBlockReservation = value;
			}
		}

		/// <summary>
		/// </summary>
		public DateTime CheckIn
		{
			get
			{
				EnsurePreLoad();
				return checkIn;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= checkIn == value;
				checkIn = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool CheckOut
		{
			get
			{
				EnsurePreLoad();
				return checkOut;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= checkOut == value;
				checkOut = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool IsClassReservation
		{
			get
			{
				EnsurePreLoad();
				return isClassReservation;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= isClassReservation == value;
				isClassReservation = value;
			}
		}

		/// <summary>
		/// </summary>
		public DojoClass Class1
		{
			get
			{
				EnsurePreLoad();
				return class1;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(class1 == null)
					{
						return;
					}
					else
					{
						class1 = value;
						isSynced = false;
					}
				}
				else
				{
					if(class1 != null && value.ID == class1.ID)
					{
						return; 
					}
					else
					{
						class1 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoClass Class2
		{
			get
			{
				EnsurePreLoad();
				return class2;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(class2 == null)
					{
						return;
					}
					else
					{
						class2 = value;
						isSynced = false;
					}
				}
				else
				{
					if(class2 != null && value.ID == class2.ID)
					{
						return; 
					}
					else
					{
						class2 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoClass Class3
		{
			get
			{
				EnsurePreLoad();
				return class3;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(class3 == null)
					{
						return;
					}
					else
					{
						class3 = value;
						isSynced = false;
					}
				}
				else
				{
					if(class3 != null && value.ID == class3.ID)
					{
						return; 
					}
					else
					{
						class3 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoClass Class4
		{
			get
			{
				EnsurePreLoad();
				return class4;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(class4 == null)
					{
						return;
					}
					else
					{
						class4 = value;
						isSynced = false;
					}
				}
				else
				{
					if(class4 != null && value.ID == class4.ID)
					{
						return; 
					}
					else
					{
						class4 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoClass Class5
		{
			get
			{
				EnsurePreLoad();
				return class5;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(class5 == null)
					{
						return;
					}
					else
					{
						class5 = value;
						isSynced = false;
					}
				}
				else
				{
					if(class5 != null && value.ID == class5.ID)
					{
						return; 
					}
					else
					{
						class5 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoClass Class6
		{
			get
			{
				EnsurePreLoad();
				return class6;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(class6 == null)
					{
						return;
					}
					else
					{
						class6 = value;
						isSynced = false;
					}
				}
				else
				{
					if(class6 != null && value.ID == class6.ID)
					{
						return; 
					}
					else
					{
						class6 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoClass Class7
		{
			get
			{
				EnsurePreLoad();
				return class7;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(class7 == null)
					{
						return;
					}
					else
					{
						class7 = value;
						isSynced = false;
					}
				}
				else
				{
					if(class7 != null && value.ID == class7.ID)
					{
						return; 
					}
					else
					{
						class7 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoClass Class8
		{
			get
			{
				EnsurePreLoad();
				return class8;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(class8 == null)
					{
						return;
					}
					else
					{
						class8 = value;
						isSynced = false;
					}
				}
				else
				{
					if(class8 != null && value.ID == class8.ID)
					{
						return; 
					}
					else
					{
						class8 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoClass Class9
		{
			get
			{
				EnsurePreLoad();
				return class9;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(class9 == null)
					{
						return;
					}
					else
					{
						class9 = value;
						isSynced = false;
					}
				}
				else
				{
					if(class9 != null && value.ID == class9.ID)
					{
						return; 
					}
					else
					{
						class9 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoClass Class10
		{
			get
			{
				EnsurePreLoad();
				return class10;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(class10 == null)
					{
						return;
					}
					else
					{
						class10 = value;
						isSynced = false;
					}
				}
				else
				{
					if(class10 != null && value.ID == class10.ID)
					{
						return; 
					}
					else
					{
						class10 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public bool IsDefinitionReservation
		{
			get
			{
				EnsurePreLoad();
				return isDefinitionReservation;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= isDefinitionReservation == value;
				isDefinitionReservation = value;
			}
		}

		/// <summary>
		/// </summary>
		public DojoClassDefinition Definition1
		{
			get
			{
				EnsurePreLoad();
				return definition1;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(definition1 == null)
					{
						return;
					}
					else
					{
						definition1 = value;
						isSynced = false;
					}
				}
				else
				{
					if(definition1 != null && value.ID == definition1.ID)
					{
						return; 
					}
					else
					{
						definition1 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoClassDefinition Definition2
		{
			get
			{
				EnsurePreLoad();
				return definition2;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(definition2 == null)
					{
						return;
					}
					else
					{
						definition2 = value;
						isSynced = false;
					}
				}
				else
				{
					if(definition2 != null && value.ID == definition2.ID)
					{
						return; 
					}
					else
					{
						definition2 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoClassDefinition Definition3
		{
			get
			{
				EnsurePreLoad();
				return definition3;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(definition3 == null)
					{
						return;
					}
					else
					{
						definition3 = value;
						isSynced = false;
					}
				}
				else
				{
					if(definition3 != null && value.ID == definition3.ID)
					{
						return; 
					}
					else
					{
						definition3 = value;
						isSynced = false;
					}
				}
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Instantiates a new instance of DojoSeminarReservation.
		/// </summary>
		public DojoSeminarReservation()
		{
		}

		public DojoSeminarReservation(int id)
		{
			this.iD = id;
			isSynced = DojoSeminarReservationManager._fill(this);
		}
		#endregion

		#region Default DbModel Methods

		/// <summary>
		/// Ensures that the object's fields and children are 
		/// pre-loaded before any updates or reads.
		/// </summary>
		public void EnsurePreLoad()
		{
			if(!isPlaceHolder)
				return;

			DojoSeminarReservationManager._fill(this);
			isPlaceHolder = false;
		}

		/// <summary>
		/// Saves the DojoSeminarReservation object state to the database.
		/// </summary>
		public int Save()
		{

			if(isSynced)
				return iD;

			if(iD == -1) throw (new Exception("Invalid record; cannot be saved."));
			if(iD == 0)
				iD = DojoSeminarReservationManager._insert(this);
			else
				DojoSeminarReservationManager._update(this);
			isSynced = iD != -1;
			return iD;
		}

		public void Delete()
		{
			DojoSeminarReservationManager._delete(this.iD);
			this.iD = 0;
			isSynced = false;
		}
		/// <summary>
		/// Duplicates DojoSeminarReservation object into a database; may or may not be the same database
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoSeminarReservation object reflecting the replicated DojoSeminarReservation object.</returns>
		public DojoSeminarReservation Duplicate()
		{
			DojoSeminarReservation clonedDojoSeminarReservation = this.Clone();

			// Insert must be called after children are replicated!
			clonedDojoSeminarReservation.iD = DojoSeminarReservationManager._insert(clonedDojoSeminarReservation);
			clonedDojoSeminarReservation.isSynced = true;
			return clonedDojoSeminarReservation;
		}

		/// <summary>
		/// Overwrites and existing DojoSeminarReservation object in the database.
		/// </summary>
		public void Overwrite(int id)
		{
			iD = id;
			DojoSeminarReservationManager._update(this);
			isSynced = true;
		}

		/// <summary>
		/// Clones DojoSeminarReservation object and clones child objects with cloning or replication.
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoSeminarReservation object reflecting the replicated DojoSeminarReservation object.</returns>
		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Clones DojoSeminarReservation object and clones child objects with cloning or replication.
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoSeminarReservation object reflecting the replicated DojoSeminarReservation object.</returns>
		public DojoSeminarReservation Clone()
		{
			DojoSeminarReservation clonedDojoSeminarReservation = new DojoSeminarReservation();
			clonedDojoSeminarReservation.iD = iD;
			clonedDojoSeminarReservation.isSynced = isSynced;
			clonedDojoSeminarReservation.isBlockReservation = isBlockReservation;
			clonedDojoSeminarReservation.checkIn = checkIn;
			clonedDojoSeminarReservation.checkOut = checkOut;
			clonedDojoSeminarReservation.isClassReservation = isClassReservation;
			clonedDojoSeminarReservation.isDefinitionReservation = isDefinitionReservation;


			if(registration != null)
				clonedDojoSeminarReservation.registration = registration;

			if(parentReservation != null)
				clonedDojoSeminarReservation.parentReservation = parentReservation;

			if(class1 != null)
				clonedDojoSeminarReservation.class1 = class1;

			if(class2 != null)
				clonedDojoSeminarReservation.class2 = class2;

			if(class3 != null)
				clonedDojoSeminarReservation.class3 = class3;

			if(class4 != null)
				clonedDojoSeminarReservation.class4 = class4;

			if(class5 != null)
				clonedDojoSeminarReservation.class5 = class5;

			if(class6 != null)
				clonedDojoSeminarReservation.class6 = class6;

			if(class7 != null)
				clonedDojoSeminarReservation.class7 = class7;

			if(class8 != null)
				clonedDojoSeminarReservation.class8 = class8;

			if(class9 != null)
				clonedDojoSeminarReservation.class9 = class9;

			if(class10 != null)
				clonedDojoSeminarReservation.class10 = class10;

			if(definition1 != null)
				clonedDojoSeminarReservation.definition1 = definition1;

			if(definition2 != null)
				clonedDojoSeminarReservation.definition2 = definition2;

			if(definition3 != null)
				clonedDojoSeminarReservation.definition3 = definition3;

			return clonedDojoSeminarReservation;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoSeminarReservation.
		/// </summary>
		/// <returns> A new DojoSeminarReservation object reflecting the cloned DojoSeminarReservation object.</returns>
		public DojoSeminarReservation Copy()
		{
			DojoSeminarReservation dojoSeminarReservation = new DojoSeminarReservation();
			CopyTo(dojoSeminarReservation);
			return dojoSeminarReservation;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoSeminarReservation.
		/// </summary>
		/// <returns> A new DojoSeminarReservation object reflecting the cloned DojoSeminarReservation object.</returns>
		/// <param name="isolation">Placeholders are used to isolate the DojoSeminarReservation from its children.</param>
		public DojoSeminarReservation Copy(bool isolation)
		{
			DojoSeminarReservation dojoSeminarReservation = new DojoSeminarReservation();
			CopyTo(dojoSeminarReservation, isolation);
			return dojoSeminarReservation;
		}

		/// <summary>
		/// Deep copies the current DojoSeminarReservation to another instance of DojoSeminarReservation.
		/// This method does not provide isolated copies; use overriden method for this feature.
		/// </summary>
		/// <param name="DojoSeminarReservation">The DojoSeminarReservation to copy to.</param>
		public void CopyTo(DojoSeminarReservation dojoSeminarReservation)
		{
			CopyTo(dojoSeminarReservation, false);
		}

		/// <summary>
		/// Deep copies the current DojoSeminarReservation to another instance of DojoSeminarReservation.
		/// </summary>
		/// <param name="DojoSeminarReservation">The DojoSeminarReservation to copy to.</param>
		/// <param name="isolation">Placeholders are used to isolate the DojoSeminarReservation from its children.</param>
		public void CopyTo(DojoSeminarReservation dojoSeminarReservation, bool isolation)
		{
			dojoSeminarReservation.iD = iD;
			dojoSeminarReservation.isPlaceHolder = isPlaceHolder;
			dojoSeminarReservation.isSynced = isSynced;
			dojoSeminarReservation.isBlockReservation = isBlockReservation;
			dojoSeminarReservation.checkIn = checkIn;
			dojoSeminarReservation.checkOut = checkOut;
			dojoSeminarReservation.isClassReservation = isClassReservation;
			dojoSeminarReservation.isDefinitionReservation = isDefinitionReservation;

			if(registration != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.registration = registration.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.registration = registration.Copy(false);
				}
			}
			if(parentReservation != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.parentReservation = parentReservation.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.parentReservation = parentReservation.Copy(false);
				}
			}
			if(class1 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.class1 = class1.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.class1 = class1.Copy(false);
				}
			}
			if(class2 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.class2 = class2.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.class2 = class2.Copy(false);
				}
			}
			if(class3 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.class3 = class3.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.class3 = class3.Copy(false);
				}
			}
			if(class4 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.class4 = class4.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.class4 = class4.Copy(false);
				}
			}
			if(class5 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.class5 = class5.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.class5 = class5.Copy(false);
				}
			}
			if(class6 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.class6 = class6.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.class6 = class6.Copy(false);
				}
			}
			if(class7 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.class7 = class7.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.class7 = class7.Copy(false);
				}
			}
			if(class8 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.class8 = class8.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.class8 = class8.Copy(false);
				}
			}
			if(class9 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.class9 = class9.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.class9 = class9.Copy(false);
				}
			}
			if(class10 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.class10 = class10.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.class10 = class10.Copy(false);
				}
			}
			if(definition1 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.definition1 = definition1.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.definition1 = definition1.Copy(false);
				}
			}
			if(definition2 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.definition2 = definition2.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.definition2 = definition2.Copy(false);
				}
			}
			if(definition3 != null)
			{
				if(isolation)
				{
					dojoSeminarReservation.definition3 = definition3.NewPlaceHolder();
				}
				else
				{
					dojoSeminarReservation.definition3 = definition3.Copy(false);
				}
			}
		}

		public DojoSeminarReservation NewPlaceHolder()
		{
			DojoSeminarReservation dojoSeminarReservation = new DojoSeminarReservation();
			dojoSeminarReservation.iD = iD;
			dojoSeminarReservation.isPlaceHolder = true;
			dojoSeminarReservation.isSynced = true;
			return dojoSeminarReservation;
		}

		public static DojoSeminarReservation NewPlaceHolder(int iD)
		{
			DojoSeminarReservation dojoSeminarReservation = new DojoSeminarReservation();
			dojoSeminarReservation.iD = iD;
			dojoSeminarReservation.isPlaceHolder = true;
			dojoSeminarReservation.isSynced = true;
			return dojoSeminarReservation;
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
			DojoSeminarReservation dojoSeminarReservation = (DojoSeminarReservation) obj;
			return this.iD - dojoSeminarReservation.iD;
		}

		/// <summary>
		/// Compares the object's ID to another object's ID.
		/// </summary>
		public int CompareTo(DojoSeminarReservation dojoSeminarReservation)
		{
			return this.iD - dojoSeminarReservation.iD;
		}

		public override bool Equals(object dojoSeminarReservation)
		{
			if(dojoSeminarReservation == null)
				return false;

			return Equals((DojoSeminarReservation) dojoSeminarReservation);
		}

		public bool Equals(DojoSeminarReservation dojoSeminarReservation)
		{
			if(dojoSeminarReservation == null)
				return false;

			return this.iD == dojoSeminarReservation.iD;
		}

		public override int GetHashCode()
		{
			return iD.GetHashCode();
		}

		#endregion

	}
}
