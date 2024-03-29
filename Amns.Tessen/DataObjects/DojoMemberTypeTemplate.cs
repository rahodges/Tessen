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
using Amns.GreyFox.Security;

namespace Amns.Tessen
{
	/// <summary>
	/// A template group for organizing and applying templates to members.
	/// </summary>
	public class DojoMemberTypeTemplate : ICloneable, IComparable
	{
		#region Private Fields

		internal int iD = 0;
		internal bool isPlaceHolder;		// Placeholders only store an ID; marked as unsynced.
		internal bool isSynced;				// Shows that data is synced with database.
		internal DateTime createDate;
		internal DateTime modifyDate;
		internal string name;
		internal string description;
		internal int orderNum;
		internal DojoMemberType memberType;
		internal string memberTypeTreeHash;
		internal DojoMemberTypeTemplate parent;
		internal DojoMemberTypeTemplate root;
		internal DojoRank initialRank;
		internal GreyFoxRole initialRole;
		internal string initialEmailFrom;
		internal string initialEmailBody;
		internal bool allowGuestPurchase;
		internal bool allowPurchase;
		internal bool allowRenewal;
		internal bool allowAutoRenewal;
		internal int ageYearsMax;
		internal int ageYearsMin;
		internal int memberForMin;
		internal int memberForMax;
		internal DojoRank rankMin;
		internal DojoRank rankMax;
		internal DojoMembershipTemplate membershipTemplate1;
		internal DojoMembershipTemplate membershipTemplate2;
		internal DojoMembershipTemplate membershipTemplate3;
		internal DojoMembershipTemplate membershipTemplate4;
		internal DojoMembershipTemplate membershipTemplate5;

		#endregion

		#region Public Properties

		/// <summary>
		/// DojoMemberTypeTemplate Record ID, assigned by database. Readonly.
		/// </summary>
		public int ID
		{
			get
			{
				return iD;
			}
		}

		/// <summary>
		/// Identifies the DojoMemberTypeTemplate as a Placeholder. Placeholders only contain 
		/// a DojoMemberTypeTemplate ID. Record late-binds data when it is accessed.
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
		public DateTime CreateDate
		{
			get
			{
				EnsurePreLoad();
				return createDate;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= createDate == value;
				createDate = value;
			}
		}

		/// <summary>
		/// </summary>
		public DateTime ModifyDate
		{
			get
			{
				EnsurePreLoad();
				return modifyDate;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= modifyDate == value;
				modifyDate = value;
			}
		}

		/// <summary>
		/// Name of template (ie. "Adult Member and 2 Children")
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
		/// A description of the membership template.
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
		/// The member type to associate this membership template with.
		/// </summary>
		public DojoMemberType MemberType
		{
			get
			{
				EnsurePreLoad();
				return memberType;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(memberType == null)
					{
						return;
					}
					else
					{
						memberType = value;
						isSynced = false;
					}
				}
				else
				{
					if(memberType != null && value.ID == memberType.ID)
					{
						return; 
					}
					else
					{
						memberType = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public string MemberTypeTreeHash
		{
			get
			{
				EnsurePreLoad();
				return memberTypeTreeHash;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= memberTypeTreeHash == value;
				memberTypeTreeHash = value;
			}
		}

		/// <summary>
		/// The parent template group, used for MembershipTrees. If it is null, this 
		/// is a root template.
		/// </summary>
		public DojoMemberTypeTemplate Parent
		{
			get
			{
				EnsurePreLoad();
				return parent;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(parent == null)
					{
						return;
					}
					else
					{
						parent = value;
						isSynced = false;
					}
				}
				else
				{
					if(parent != null && value.ID == parent.ID)
					{
						return; 
					}
					else
					{
						parent = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoMemberTypeTemplate Root
		{
			get
			{
				EnsurePreLoad();
				return root;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(root == null)
					{
						return;
					}
					else
					{
						root = value;
						isSynced = false;
					}
				}
				else
				{
					if(root != null && value.ID == root.ID)
					{
						return; 
					}
					else
					{
						root = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoRank InitialRank
		{
			get
			{
				EnsurePreLoad();
				return initialRank;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(initialRank == null)
					{
						return;
					}
					else
					{
						initialRank = value;
						isSynced = false;
					}
				}
				else
				{
					if(initialRank != null && value.ID == initialRank.ID)
					{
						return; 
					}
					else
					{
						initialRank = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public GreyFoxRole InitialRole
		{
			get
			{
				EnsurePreLoad();
				return initialRole;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(initialRole == null)
					{
						return;
					}
					else
					{
						initialRole = value;
						isSynced = false;
					}
				}
				else
				{
					if(initialRole != null && value.ID == initialRole.ID)
					{
						return; 
					}
					else
					{
						initialRole = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public string InitialEmailFrom
		{
			get
			{
				EnsurePreLoad();
				return initialEmailFrom;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= initialEmailFrom == value;
				initialEmailFrom = value;
			}
		}

		/// <summary>
		/// </summary>
		public string InitialEmailBody
		{
			get
			{
				EnsurePreLoad();
				return initialEmailBody;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= initialEmailBody == value;
				initialEmailBody = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool AllowGuestPurchase
		{
			get
			{
				EnsurePreLoad();
				return allowGuestPurchase;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= allowGuestPurchase == value;
				allowGuestPurchase = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool AllowPurchase
		{
			get
			{
				EnsurePreLoad();
				return allowPurchase;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= allowPurchase == value;
				allowPurchase = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool AllowRenewal
		{
			get
			{
				EnsurePreLoad();
				return allowRenewal;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= allowRenewal == value;
				allowRenewal = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool AllowAutoRenewal
		{
			get
			{
				EnsurePreLoad();
				return allowAutoRenewal;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= allowAutoRenewal == value;
				allowAutoRenewal = value;
			}
		}

		/// <summary>
		/// </summary>
		public int AgeYearsMax
		{
			get
			{
				EnsurePreLoad();
				return ageYearsMax;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= ageYearsMax == value;
				ageYearsMax = value;
			}
		}

		/// <summary>
		/// This is used for children and senior requirements.
		/// </summary>
		public int AgeYearsMin
		{
			get
			{
				EnsurePreLoad();
				return ageYearsMin;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= ageYearsMin == value;
				ageYearsMin = value;
			}
		}

		/// <summary>
		/// </summary>
		public int MemberForMin
		{
			get
			{
				EnsurePreLoad();
				return memberForMin;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= memberForMin == value;
				memberForMin = value;
			}
		}

		/// <summary>
		/// </summary>
		public int MemberForMax
		{
			get
			{
				EnsurePreLoad();
				return memberForMax;
			}
			set
			{
				EnsurePreLoad();
				isSynced &= memberForMax == value;
				memberForMax = value;
			}
		}

		/// <summary>
		/// </summary>
		public DojoRank RankMin
		{
			get
			{
				EnsurePreLoad();
				return rankMin;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(rankMin == null)
					{
						return;
					}
					else
					{
						rankMin = value;
						isSynced = false;
					}
				}
				else
				{
					if(rankMin != null && value.ID == rankMin.ID)
					{
						return; 
					}
					else
					{
						rankMin = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoRank RankMax
		{
			get
			{
				EnsurePreLoad();
				return rankMax;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(rankMax == null)
					{
						return;
					}
					else
					{
						rankMax = value;
						isSynced = false;
					}
				}
				else
				{
					if(rankMax != null && value.ID == rankMax.ID)
					{
						return; 
					}
					else
					{
						rankMax = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoMembershipTemplate MembershipTemplate1
		{
			get
			{
				EnsurePreLoad();
				return membershipTemplate1;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(membershipTemplate1 == null)
					{
						return;
					}
					else
					{
						membershipTemplate1 = value;
						isSynced = false;
					}
				}
				else
				{
					if(membershipTemplate1 != null && value.ID == membershipTemplate1.ID)
					{
						return; 
					}
					else
					{
						membershipTemplate1 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoMembershipTemplate MembershipTemplate2
		{
			get
			{
				EnsurePreLoad();
				return membershipTemplate2;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(membershipTemplate2 == null)
					{
						return;
					}
					else
					{
						membershipTemplate2 = value;
						isSynced = false;
					}
				}
				else
				{
					if(membershipTemplate2 != null && value.ID == membershipTemplate2.ID)
					{
						return; 
					}
					else
					{
						membershipTemplate2 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoMembershipTemplate MembershipTemplate3
		{
			get
			{
				EnsurePreLoad();
				return membershipTemplate3;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(membershipTemplate3 == null)
					{
						return;
					}
					else
					{
						membershipTemplate3 = value;
						isSynced = false;
					}
				}
				else
				{
					if(membershipTemplate3 != null && value.ID == membershipTemplate3.ID)
					{
						return; 
					}
					else
					{
						membershipTemplate3 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoMembershipTemplate MembershipTemplate4
		{
			get
			{
				EnsurePreLoad();
				return membershipTemplate4;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(membershipTemplate4 == null)
					{
						return;
					}
					else
					{
						membershipTemplate4 = value;
						isSynced = false;
					}
				}
				else
				{
					if(membershipTemplate4 != null && value.ID == membershipTemplate4.ID)
					{
						return; 
					}
					else
					{
						membershipTemplate4 = value;
						isSynced = false;
					}
				}
			}
		}

		/// <summary>
		/// </summary>
		public DojoMembershipTemplate MembershipTemplate5
		{
			get
			{
				EnsurePreLoad();
				return membershipTemplate5;
			}
			set
			{
				EnsurePreLoad();
				if(value == null)
				{
					if(membershipTemplate5 == null)
					{
						return;
					}
					else
					{
						membershipTemplate5 = value;
						isSynced = false;
					}
				}
				else
				{
					if(membershipTemplate5 != null && value.ID == membershipTemplate5.ID)
					{
						return; 
					}
					else
					{
						membershipTemplate5 = value;
						isSynced = false;
					}
				}
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Instantiates a new instance of DojoMemberTypeTemplate.
		/// </summary>
		public DojoMemberTypeTemplate()
		{
			createDate = DateTime.Now;
			modifyDate = DateTime.Now;
			name = string.Empty;
			description = string.Empty;
			memberTypeTreeHash = string.Empty;
			initialEmailFrom = string.Empty;
			initialEmailBody = string.Empty;
		}

		public DojoMemberTypeTemplate(int id)
		{
			this.iD = id;
			isSynced = DojoMemberTypeTemplateManager._fill(this);
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

			DojoMemberTypeTemplateManager._fill(this);
			isPlaceHolder = false;
		}

		/// <summary>
		/// Saves the DojoMemberTypeTemplate object state to the database.
		/// </summary>
		public int Save()
		{

			if(isSynced)
				return iD;

			if(iD == -1) throw (new Exception("Invalid record; cannot be saved."));
			if(iD == 0)
				iD = DojoMemberTypeTemplateManager._insert(this);
			else
				DojoMemberTypeTemplateManager._update(this);
			isSynced = iD != -1;
			return iD;
		}

		public void Delete()
		{
			DojoMemberTypeTemplateManager._delete(this.iD);
			this.iD = 0;
			isSynced = false;
		}
		/// <summary>
		/// Duplicates DojoMemberTypeTemplate object into a database; may or may not be the same database
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoMemberTypeTemplate object reflecting the replicated DojoMemberTypeTemplate object.</returns>
		public DojoMemberTypeTemplate Duplicate()
		{
			DojoMemberTypeTemplate clonedDojoMemberTypeTemplate = this.Clone();

			// Insert must be called after children are replicated!
			clonedDojoMemberTypeTemplate.iD = DojoMemberTypeTemplateManager._insert(clonedDojoMemberTypeTemplate);
			clonedDojoMemberTypeTemplate.isSynced = true;
			return clonedDojoMemberTypeTemplate;
		}

		/// <summary>
		/// Overwrites and existing DojoMemberTypeTemplate object in the database.
		/// </summary>
		public void Overwrite(int id)
		{
			iD = id;
			DojoMemberTypeTemplateManager._update(this);
			isSynced = true;
		}

		/// <summary>
		/// Clones DojoMemberTypeTemplate object and clones child objects with cloning or replication.
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoMemberTypeTemplate object reflecting the replicated DojoMemberTypeTemplate object.</returns>
		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Clones DojoMemberTypeTemplate object and clones child objects with cloning or replication.
		/// as the parent object.
		/// </summary>
		/// <returns> A new DojoMemberTypeTemplate object reflecting the replicated DojoMemberTypeTemplate object.</returns>
		public DojoMemberTypeTemplate Clone()
		{
			DojoMemberTypeTemplate clonedDojoMemberTypeTemplate = new DojoMemberTypeTemplate();
			clonedDojoMemberTypeTemplate.iD = iD;
			clonedDojoMemberTypeTemplate.isSynced = isSynced;
			clonedDojoMemberTypeTemplate.createDate = createDate;
			clonedDojoMemberTypeTemplate.modifyDate = modifyDate;
			clonedDojoMemberTypeTemplate.name = name;
			clonedDojoMemberTypeTemplate.description = description;
			clonedDojoMemberTypeTemplate.orderNum = orderNum;
			clonedDojoMemberTypeTemplate.memberTypeTreeHash = memberTypeTreeHash;
			clonedDojoMemberTypeTemplate.initialEmailFrom = initialEmailFrom;
			clonedDojoMemberTypeTemplate.initialEmailBody = initialEmailBody;
			clonedDojoMemberTypeTemplate.allowGuestPurchase = allowGuestPurchase;
			clonedDojoMemberTypeTemplate.allowPurchase = allowPurchase;
			clonedDojoMemberTypeTemplate.allowRenewal = allowRenewal;
			clonedDojoMemberTypeTemplate.allowAutoRenewal = allowAutoRenewal;
			clonedDojoMemberTypeTemplate.ageYearsMax = ageYearsMax;
			clonedDojoMemberTypeTemplate.ageYearsMin = ageYearsMin;
			clonedDojoMemberTypeTemplate.memberForMin = memberForMin;
			clonedDojoMemberTypeTemplate.memberForMax = memberForMax;


			if(memberType != null)
				clonedDojoMemberTypeTemplate.memberType = memberType;

			if(parent != null)
				clonedDojoMemberTypeTemplate.parent = parent;

			if(root != null)
				clonedDojoMemberTypeTemplate.root = root;

			if(initialRank != null)
				clonedDojoMemberTypeTemplate.initialRank = initialRank;

			if(initialRole != null)
				clonedDojoMemberTypeTemplate.initialRole = initialRole;

			if(rankMin != null)
				clonedDojoMemberTypeTemplate.rankMin = rankMin;

			if(rankMax != null)
				clonedDojoMemberTypeTemplate.rankMax = rankMax;

			if(membershipTemplate1 != null)
				clonedDojoMemberTypeTemplate.membershipTemplate1 = membershipTemplate1;

			if(membershipTemplate2 != null)
				clonedDojoMemberTypeTemplate.membershipTemplate2 = membershipTemplate2;

			if(membershipTemplate3 != null)
				clonedDojoMemberTypeTemplate.membershipTemplate3 = membershipTemplate3;

			if(membershipTemplate4 != null)
				clonedDojoMemberTypeTemplate.membershipTemplate4 = membershipTemplate4;

			if(membershipTemplate5 != null)
				clonedDojoMemberTypeTemplate.membershipTemplate5 = membershipTemplate5;

			return clonedDojoMemberTypeTemplate;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoMemberTypeTemplate.
		/// </summary>
		/// <returns> A new DojoMemberTypeTemplate object reflecting the cloned DojoMemberTypeTemplate object.</returns>
		public DojoMemberTypeTemplate Copy()
		{
			DojoMemberTypeTemplate dojoMemberTypeTemplate = new DojoMemberTypeTemplate();
			CopyTo(dojoMemberTypeTemplate);
			return dojoMemberTypeTemplate;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoMemberTypeTemplate.
		/// </summary>
		/// <returns> A new DojoMemberTypeTemplate object reflecting the cloned DojoMemberTypeTemplate object.</returns>
		/// <param name="isolation">Placeholders are used to isolate the DojoMemberTypeTemplate from its children.</param>
		public DojoMemberTypeTemplate Copy(bool isolation)
		{
			DojoMemberTypeTemplate dojoMemberTypeTemplate = new DojoMemberTypeTemplate();
			CopyTo(dojoMemberTypeTemplate, isolation);
			return dojoMemberTypeTemplate;
		}

		/// <summary>
		/// Deep copies the current DojoMemberTypeTemplate to another instance of DojoMemberTypeTemplate.
		/// This method does not provide isolated copies; use overriden method for this feature.
		/// </summary>
		/// <param name="DojoMemberTypeTemplate">The DojoMemberTypeTemplate to copy to.</param>
		public void CopyTo(DojoMemberTypeTemplate dojoMemberTypeTemplate)
		{
			CopyTo(dojoMemberTypeTemplate, false);
		}

		/// <summary>
		/// Deep copies the current DojoMemberTypeTemplate to another instance of DojoMemberTypeTemplate.
		/// </summary>
		/// <param name="DojoMemberTypeTemplate">The DojoMemberTypeTemplate to copy to.</param>
		/// <param name="isolation">Placeholders are used to isolate the DojoMemberTypeTemplate from its children.</param>
		public void CopyTo(DojoMemberTypeTemplate dojoMemberTypeTemplate, bool isolation)
		{
			dojoMemberTypeTemplate.iD = iD;
			dojoMemberTypeTemplate.isPlaceHolder = isPlaceHolder;
			dojoMemberTypeTemplate.isSynced = isSynced;
			dojoMemberTypeTemplate.createDate = createDate;
			dojoMemberTypeTemplate.modifyDate = modifyDate;
			dojoMemberTypeTemplate.name = name;
			dojoMemberTypeTemplate.description = description;
			dojoMemberTypeTemplate.orderNum = orderNum;
			if(memberType != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.memberType = memberType.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.memberType = memberType.Copy(false);
				}
			}
			dojoMemberTypeTemplate.memberTypeTreeHash = memberTypeTreeHash;
			if(parent != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.parent = parent.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.parent = parent.Copy(false);
				}
			}
			if(root != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.root = root.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.root = root.Copy(false);
				}
			}
			if(initialRank != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.initialRank = initialRank.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.initialRank = initialRank.Copy(false);
				}
			}
			if(initialRole != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.initialRole = initialRole.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.initialRole = initialRole.Copy(false);
				}
			}
			dojoMemberTypeTemplate.initialEmailFrom = initialEmailFrom;
			dojoMemberTypeTemplate.initialEmailBody = initialEmailBody;
			dojoMemberTypeTemplate.allowGuestPurchase = allowGuestPurchase;
			dojoMemberTypeTemplate.allowPurchase = allowPurchase;
			dojoMemberTypeTemplate.allowRenewal = allowRenewal;
			dojoMemberTypeTemplate.allowAutoRenewal = allowAutoRenewal;
			dojoMemberTypeTemplate.ageYearsMax = ageYearsMax;
			dojoMemberTypeTemplate.ageYearsMin = ageYearsMin;
			dojoMemberTypeTemplate.memberForMin = memberForMin;
			dojoMemberTypeTemplate.memberForMax = memberForMax;
			if(rankMin != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.rankMin = rankMin.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.rankMin = rankMin.Copy(false);
				}
			}
			if(rankMax != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.rankMax = rankMax.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.rankMax = rankMax.Copy(false);
				}
			}
			if(membershipTemplate1 != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.membershipTemplate1 = membershipTemplate1.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.membershipTemplate1 = membershipTemplate1.Copy(false);
				}
			}
			if(membershipTemplate2 != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.membershipTemplate2 = membershipTemplate2.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.membershipTemplate2 = membershipTemplate2.Copy(false);
				}
			}
			if(membershipTemplate3 != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.membershipTemplate3 = membershipTemplate3.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.membershipTemplate3 = membershipTemplate3.Copy(false);
				}
			}
			if(membershipTemplate4 != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.membershipTemplate4 = membershipTemplate4.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.membershipTemplate4 = membershipTemplate4.Copy(false);
				}
			}
			if(membershipTemplate5 != null)
			{
				if(isolation)
				{
					dojoMemberTypeTemplate.membershipTemplate5 = membershipTemplate5.NewPlaceHolder();
				}
				else
				{
					dojoMemberTypeTemplate.membershipTemplate5 = membershipTemplate5.Copy(false);
				}
			}
		}

		public DojoMemberTypeTemplate NewPlaceHolder()
		{
			DojoMemberTypeTemplate dojoMemberTypeTemplate = new DojoMemberTypeTemplate();
			dojoMemberTypeTemplate.iD = iD;
			dojoMemberTypeTemplate.isPlaceHolder = true;
			dojoMemberTypeTemplate.isSynced = true;
			return dojoMemberTypeTemplate;
		}

		public static DojoMemberTypeTemplate NewPlaceHolder(int iD)
		{
			DojoMemberTypeTemplate dojoMemberTypeTemplate = new DojoMemberTypeTemplate();
			dojoMemberTypeTemplate.iD = iD;
			dojoMemberTypeTemplate.isPlaceHolder = true;
			dojoMemberTypeTemplate.isSynced = true;
			return dojoMemberTypeTemplate;
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
			DojoMemberTypeTemplate dojoMemberTypeTemplate = (DojoMemberTypeTemplate) obj;
			return this.iD - dojoMemberTypeTemplate.iD;
		}

		/// <summary>
		/// Compares the object's ID to another object's ID.
		/// </summary>
		public int CompareTo(DojoMemberTypeTemplate dojoMemberTypeTemplate)
		{
			return this.iD - dojoMemberTypeTemplate.iD;
		}

		public override int GetHashCode()
		{
			return iD.GetHashCode();
		}

		#endregion

		//--- Begin Custom Code ---

        #region Internals for Template Processing

        /// <summary>
        /// This is for internal template processing only.
        /// </summary>
        internal DojoMemberTypeTemplateCollection subTemplates;

        /// <summary>
        /// This is for internal template processing only.
        /// </summary>
        internal string treeHash;

        /// <summary>
        /// This is for internal template processing only.
        /// </summary>
        internal DojoMemberTypeTemplate treeRoot;

        #endregion

		//--- End Custom Code ---
	}
}
