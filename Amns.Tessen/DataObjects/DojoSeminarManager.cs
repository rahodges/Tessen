/* ********************************************************** *
 * AMNS NitroCast v1.0 DAABManager Data Tier                    *
 * Autogenerated by NitroCast © 2007 Roy A.E Hodges             *
 * All Rights Reserved                                        *
 * ---------------------------------------------------------- *
 * Source code may not be reproduced or redistributed without *
 * written expressed permission from the author.              *
 * Permission is granted to modify source code by licencee.   *
 * These permissions do not extend to third parties.          *
 * ********************************************************** */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Amns.GreyFox.People;
using Amns.Rappahanock;

namespace Amns.Tessen
{
	#region Child Flags Enumeration

	public enum DojoSeminarFlags : int { Location,
				Item,
				ItemParentItem,
				ItemPurchaseVendor,
				ItemPurchaseAccount,
				ItemInventoryAccount,
				ItemTax,
				ItemSalesIncomeAccount,
				ItemWebRelatedItems};

	#endregion

	/// <summary>
	/// Datamanager for DojoSeminar objects.
	/// </summary>
	public class DojoSeminarManager
	{
		#region Manager Fields

		// Static Fields
		static bool isInitialized;

		// Private Fields
		string tableName = "kitTessen_Seminars";
		public static readonly string LocationTable = "kitTessen_Locations";


		public string TableName
		{
			get { return tableName; }
			set { tableName = value; }
		}

		// Hashtable to cache separate tables
		static bool cacheEnabled	= true;
		public static bool CacheEnabled
		{
			get { return cacheEnabled; }
			set { cacheEnabled = value; }
		}

		#endregion

		#region Inner Join Field Array

		public static readonly string[] InnerJoinFields = new string[] {
			"DojoSeminarID",
			"Name",
			"StartDate",
			"EndDate",
			"Description",
			"IsLocal",
			"LocationID",
			"ClassUnitFee",
			"ClassUnitType",
			"BaseRegistrationFee",
			"RegistrationEnabled",
			"RegistrationStart",
			"FullEarlyRegistrationFee",
			"EarlyEndDate",
			"FullRegistrationFee",
			"LateStartDate",
			"FullLateRegistrationFee",
			"RegistrationEnd",
			"Details",
			"DetailsOverrideUrl",
			"PdfUrl",
			"ItemID"
		};

		#endregion

		#region Join Field Array

		public static readonly string[,] JoinFields = new string[,] {
			{ "DojoSeminarID", "LONG", "-1" },
			{ "Name", "TEXT(75)", "" },
			{ "StartDate", "DATETIME", "" },
			{ "EndDate", "DATETIME", "" },
			{ "Description", "MEMO", "" },
			{ "IsLocal", "BIT", "" },
			{ "LocationID", "LONG", "null" },
			{ "ClassUnitFee", "CURRENCY", "" },
			{ "ClassUnitType", "BYTE", "0" },
			{ "BaseRegistrationFee", "CURRENCY", "" },
			{ "RegistrationEnabled", "BIT", "" },
			{ "RegistrationStart", "DATETIME", "" },
			{ "FullEarlyRegistrationFee", "CURRENCY", "" },
			{ "EarlyEndDate", "DATETIME", "" },
			{ "FullRegistrationFee", "CURRENCY", "" },
			{ "LateStartDate", "DATETIME", "" },
			{ "FullLateRegistrationFee", "CURRENCY", "" },
			{ "RegistrationEnd", "DATETIME", "" },
			{ "Details", "MEMO", "" },
			{ "DetailsOverrideUrl", "TEXT(255)", "" },
			{ "PdfUrl", "TEXT(255)", "" },
			{ "ItemID", "LONG", "null" }
		};

		#endregion

		#region Default NitroCast Constructors

		static DojoSeminarManager()
		{
		}

		public DojoSeminarManager()
		{
		}

		#endregion

		#region Default NitroCast Constructors

		// Initialize
		public void Initialize(string connectionString)
		{
			if(!DojoSeminarManager.isInitialized)
			{
				DojoSeminarManager.isInitialized = true;
			}
		}
		#endregion

		#region Default NitroCast Insert Method

		/// <summary>
		/// Inserts a DojoSeminar into the database. All children should have been
		/// saved to the database before insertion. New children will not be
		/// related to this object in the database.
		/// </summary>
		/// <param name="_DojoSeminar">The DojoSeminar to insert into the database.</param>
		internal static int _insert(DojoSeminar dojoSeminar)
		{
			int id;
			string query;
			Database database;
			DbCommand dbCommand;

			database = DatabaseFactory.CreateDatabase();

			query = "INSERT INTO kitTessen_Seminars " +
				"(" +
				"Name," +
				"StartDate," +
				"EndDate," +
				"Description," +
				"IsLocal," +
				"LocationID," +
				"ClassUnitFee," +
				"ClassUnitType," +
				"BaseRegistrationFee," +
				"RegistrationEnabled," +
				"RegistrationStart," +
				"FullEarlyRegistrationFee," +
				"EarlyEndDate," +
				"FullRegistrationFee," +
				"LateStartDate," +
				"FullLateRegistrationFee," +
				"RegistrationEnd," +
				"Details," +
				"DetailsOverrideUrl," +
				"PdfUrl," +
				"ItemID) VALUES (" +
				"@Name," +
				"@StartDate," +
				"@EndDate," +
				"@Description," +
				"@IsLocal," +
				"@LocationID," +
				"@ClassUnitFee," +
				"@ClassUnitType," +
				"@BaseRegistrationFee," +
				"@RegistrationEnabled," +
				"@RegistrationStart," +
				"@FullEarlyRegistrationFee," +
				"@EarlyEndDate," +
				"@FullRegistrationFee," +
				"@LateStartDate," +
				"@FullLateRegistrationFee," +
				"@RegistrationEnd," +
				"@Details," +
				"@DetailsOverrideUrl," +
				"@PdfUrl," +
				"@ItemID);";

			if (database.ConnectionStringWithoutCredentials.StartsWith("provider=microsoft.jet.oledb.4.0"))
			{
				// Microsoft Access
				// Connection must remain open for IDENTITY to return correct value,
				// therefore use the dbCommand object's Connection directly to control
				// connection state.
				dbCommand = database.GetSqlStringCommand(query);
				fillParameters(database, dbCommand, dojoSeminar);
				dbCommand.Connection = database.CreateConnection();
				dbCommand.Connection.Open();
				dbCommand.ExecuteNonQuery();
				dbCommand.CommandText = "SELECT @@IDENTITY AS LastID";
				id = (int)dbCommand.ExecuteScalar();
				dbCommand.Connection.Close();
			}
			else
			{
				//// Microsoft SQL Server
				dbCommand = database.GetSqlStringCommand(query + " SELECT @LastID = SCOPE_IDENTITY();");
				fillParameters(database, dbCommand, dojoSeminar);
				database.AddOutParameter(dbCommand, "@LastID", DbType.Int32, 10);
				database.ExecuteNonQuery(dbCommand);
				id = (int)dbCommand.Parameters["@LastID"].Value;
			}

			// Save child relationships for Options.
			if(dojoSeminar.options != null)
			{
				dbCommand = database.GetSqlStringCommand("INSERT INTO kitTessen_SeminarsChildren_Options " +
					"(DojoSeminarID, DojoSeminarOptionID)" + 
					" VALUES (@DojoSeminarID, @DojoSeminarOptionID);");
				addParameter(database, dbCommand, "@DojoSeminarID", DbType.Int32);
				addParameter(database, dbCommand, "@DojoSeminarOptionID", DbType.Int32);
				foreach(DojoSeminarOption item in dojoSeminar.options)
				{
					dbCommand.Parameters["@DojoSeminarID"].Value = id;
					dbCommand.Parameters["@DojoSeminarOptionID"].Value = item.ID;
					database.ExecuteNonQuery(dbCommand);
				}
			}
			// Store dojoSeminar in cache.
			if(cacheEnabled) cacheStore(dojoSeminar);
			return id;
		}

		#endregion

		#region Default NitroCast Update Method

		internal static int _update(DojoSeminar dojoSeminar)
		{
			Database database;
			DbCommand dbCommand;

			database = DatabaseFactory.CreateDatabase();

			dbCommand = database.GetSqlStringCommand("UPDATE kitTessen_Seminars SET Name=@Name," +
				"StartDate=@StartDate," +
				"EndDate=@EndDate," +
				"Description=@Description," +
				"IsLocal=@IsLocal," +
				"LocationID=@LocationID," +
				"ClassUnitFee=@ClassUnitFee," +
				"ClassUnitType=@ClassUnitType," +
				"BaseRegistrationFee=@BaseRegistrationFee," +
				"RegistrationEnabled=@RegistrationEnabled," +
				"RegistrationStart=@RegistrationStart," +
				"FullEarlyRegistrationFee=@FullEarlyRegistrationFee," +
				"EarlyEndDate=@EarlyEndDate," +
				"FullRegistrationFee=@FullRegistrationFee," +
				"LateStartDate=@LateStartDate," +
				"FullLateRegistrationFee=@FullLateRegistrationFee," +
				"RegistrationEnd=@RegistrationEnd," +
				"Details=@Details," +
				"DetailsOverrideUrl=@DetailsOverrideUrl," +
				"PdfUrl=@PdfUrl," +
				"ItemID=@ItemID WHERE DojoSeminarID=@DojoSeminarID;");

			fillParameters(database, dbCommand, dojoSeminar);
			database.AddInParameter(dbCommand, "DojoSeminarID", DbType.Int32, dojoSeminar.iD);
			// Abandon remaining updates if no rows have been updated by returning false immediately.
			if (database.ExecuteNonQuery(dbCommand) == 0) return -1;

			if(dojoSeminar.options != null)
			{

				// Delete child relationships for Options.
				dbCommand = database.GetSqlStringCommand("DELETE  FROM kitTessen_SeminarsChildren_Options WHERE DojoSeminarID=@DojoSeminarID;");
				database.AddInParameter(dbCommand, "@DojoSeminarID", DbType.Int32, dojoSeminar.iD);
				database.ExecuteNonQuery(dbCommand);

				// Save child relationships for Options.
				dbCommand = database.GetSqlStringCommand("INSERT INTO kitTessen_SeminarsChildren_Options (DojoSeminarID, DojoSeminarOptionID) VALUES (@DojoSeminarID, @DojoSeminarOptionID);");
				database.AddInParameter(dbCommand, "@DojoSeminarID", DbType.Int32, dojoSeminar.iD);
				database.AddInParameter(dbCommand, "@DojoSeminarOptionID", DbType.Int32);
				foreach(DojoSeminarOption dojoSeminarOption in dojoSeminar.options)
				{
					dbCommand.Parameters["@DojoSeminarOptionID"].Value = dojoSeminarOption.ID;
					database.ExecuteNonQuery(dbCommand);
				}
			}

			// Store dojoSeminar in cache.
			if (cacheEnabled) cacheStore(dojoSeminar);

			return dojoSeminar.iD;
		}

		#endregion

		#region Default NitroCast Fill Parameters Method

		private static void fillParameters(Database database, DbCommand dbCommand, DojoSeminar dojoSeminar)
		{
			#region General

			addParameter(database, dbCommand, "@Name", DbType.String, dojoSeminar.name);
			addParameter(database, dbCommand, "@StartDate", DbType.Date, dojoSeminar.startDate);
			addParameter(database, dbCommand, "@EndDate", DbType.Date, dojoSeminar.endDate);
			addParameter(database, dbCommand, "@Description", DbType.String, dojoSeminar.description);
			addParameter(database, dbCommand, "@IsLocal", DbType.Boolean, dojoSeminar.isLocal);
			if(dojoSeminar.location == null)
			{
				addParameter(database, dbCommand, "@LocationID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "@LocationID", DbType.Int32, dojoSeminar.location.ID);
			}

			#endregion

			#region Registration

			addParameter(database, dbCommand, "@ClassUnitFee", DbType.Currency, dojoSeminar.classUnitFee);
			addParameter(database, dbCommand, "@ClassUnitType", DbType.Byte, (Byte)dojoSeminar.classUnitType);
			addParameter(database, dbCommand, "@BaseRegistrationFee", DbType.Currency, dojoSeminar.baseRegistrationFee);
			addParameter(database, dbCommand, "@RegistrationEnabled", DbType.Boolean, dojoSeminar.registrationEnabled);
			addParameter(database, dbCommand, "@RegistrationStart", DbType.Date, dojoSeminar.registrationStart);
			addParameter(database, dbCommand, "@FullEarlyRegistrationFee", DbType.Currency, dojoSeminar.fullEarlyRegistrationFee);
			addParameter(database, dbCommand, "@EarlyEndDate", DbType.Date, dojoSeminar.earlyEndDate);
			addParameter(database, dbCommand, "@FullRegistrationFee", DbType.Currency, dojoSeminar.fullRegistrationFee);
			addParameter(database, dbCommand, "@LateStartDate", DbType.Date, dojoSeminar.lateStartDate);
			addParameter(database, dbCommand, "@FullLateRegistrationFee", DbType.Currency, dojoSeminar.fullLateRegistrationFee);
			addParameter(database, dbCommand, "@RegistrationEnd", DbType.Date, dojoSeminar.registrationEnd);

			#endregion

			#region Details

			addParameter(database, dbCommand, "@Details", DbType.String, dojoSeminar.details);
			addParameter(database, dbCommand, "@DetailsOverrideUrl", DbType.String, dojoSeminar.detailsOverrideUrl);
			addParameter(database, dbCommand, "@PdfUrl", DbType.String, dojoSeminar.pdfUrl);

			#endregion

			#region Rappahanock

			if(dojoSeminar.item == null)
			{
				addParameter(database, dbCommand, "@ItemID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "@ItemID", DbType.Int32, dojoSeminar.item.ID);
			}

			#endregion

		}

		#endregion

		#region Parameters

		private static void addParameter(Database database, DbCommand command,
			string name, DbType dbType)
		{
			database.AddInParameter(command, name, dbType);
		}

		private static void addParameter(Database database, DbCommand command,
			string name, DbType dbType, object value)
		{
			database.AddInParameter(command, name, dbType, value);
		}

		private static void addParameter(Database database, DbCommand command,
			string name, DbType dbType, object value, object nullValue)
		{
			if (value == null)
				database.AddInParameter(command, name, dbType, nullValue);
			else
				database.AddInParameter(command, name, dbType, value);
		}

		private static void addParameter(Database database, DbCommand command,
			string name, DbType dbType, object value, object nullValue, object nullSubValue)
		{
			if (value == null || value == nullSubValue)
				database.AddInParameter(command, name, dbType, nullValue);
			else
				database.AddInParameter(command, name, dbType, value);
		}

		#endregion

		#region Default NitroCast Fill Method

		internal static bool _fill(DojoSeminar dojoSeminar)
		{
			// Clone item from cache.
			if(cacheEnabled)
			{
				object cachedObject = cacheFind(dojoSeminar.iD);
				if(cachedObject != null)
				{
					((DojoSeminar)cachedObject).CopyTo(dojoSeminar, true);
					return dojoSeminar.isSynced;
				}
			}

			StringBuilder query;
			Database database;
			DbCommand dbCommand;

			query = new StringBuilder("SELECT ");
			query.Append(string.Join(",", InnerJoinFields));
			query.Append(" FROM kitTessen_Seminars WHERE DojoSeminarID=");
			query.Append(dojoSeminar.iD);
			query.Append(";");

			database = DatabaseFactory.CreateDatabase();
			dbCommand = database.GetSqlStringCommand(query.ToString());
			IDataReader r = database.ExecuteReader(dbCommand);

			if(!r.Read())
			{
				throw(new Exception(string.Format("Cannot find DojoSeminarID '{0}'.", 
					dojoSeminar.iD)));
			}

			FillFromReader(dojoSeminar, r, 0, 1);

			// Microsoft DAAB still needs to have the reader closed.
			r.Close();

			// Store dojoSeminar in cache.
			if(cacheEnabled) cacheStore(dojoSeminar);

			return true;
		}

		#endregion

		#region Default NitroCast GetCollection Method

		public DojoSeminarCollection GetCollection(string whereClause, string sortClause)
		{
			return GetCollection(0, whereClause, sortClause, null);
		}

		public DojoSeminarCollection GetCollection(string whereClause, string sortClause, params DojoSeminarFlags[] optionFlags)
		{
			return GetCollection(0, whereClause, sortClause, optionFlags);
		}

		public DojoSeminarCollection GetCollection(int topCount, string whereClause, string sortClause, params DojoSeminarFlags[] optionFlags)
		{
			StringBuilder query;
			Database database;
			DbCommand dbCommand;
			IDataReader r;
			DojoSeminarCollection dojoSeminarCollection;

			int innerJoinOffset;

			query = new StringBuilder("SELECT ");

			if(topCount > 0)
			{
				query.Append("TOP ");
				query.Append(topCount);
				query.Append(" ");
			}

			foreach(string columnName in InnerJoinFields)
			{
				query.Append("DojoSeminar.");
				query.Append(columnName);
				query.Append(",");
			}

			innerJoinOffset = InnerJoinFields.GetUpperBound(0) + 1;
			int locationOffset = -1;
			int itemOffset = -1;
			int itemParentItemOffset = -1;
			int itemPurchaseVendorOffset = -1;
			int itemPurchaseAccountOffset = -1;
			int itemInventoryAccountOffset = -1;
			int itemTaxOffset = -1;
			int itemSalesIncomeAccountOffset = -1;

			//
			// Append Option Flag Fields
			//
			if(optionFlags != null)
				for(int x = 0; x < optionFlags.Length; x++)
				{
					switch(optionFlags[x])
					{
						case DojoSeminarFlags.Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							locationOffset = innerJoinOffset;
							innerJoinOffset = locationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarFlags.Item:
							for(int i = 0; i <= RHItemManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item.");
								query.Append(RHItemManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemOffset = innerJoinOffset;
							innerJoinOffset = itemOffset + RHItemManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarFlags.ItemParentItem:
							for(int i = 0; i <= RHItemManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item_ParentItem.");
								query.Append(RHItemManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemParentItemOffset = innerJoinOffset;
							innerJoinOffset = itemParentItemOffset + RHItemManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarFlags.ItemPurchaseVendor:
							for(int i = 0; i <= RHVendorManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item_PurchaseVendor.");
								query.Append(RHVendorManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemPurchaseVendorOffset = innerJoinOffset;
							innerJoinOffset = itemPurchaseVendorOffset + RHVendorManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarFlags.ItemPurchaseAccount:
							for(int i = 0; i <= RHAccountManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item_PurchaseAccount.");
								query.Append(RHAccountManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemPurchaseAccountOffset = innerJoinOffset;
							innerJoinOffset = itemPurchaseAccountOffset + RHAccountManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarFlags.ItemInventoryAccount:
							for(int i = 0; i <= RHAccountManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item_InventoryAccount.");
								query.Append(RHAccountManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemInventoryAccountOffset = innerJoinOffset;
							innerJoinOffset = itemInventoryAccountOffset + RHAccountManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarFlags.ItemTax:
							for(int i = 0; i <= RHTaxTypeManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item_Tax.");
								query.Append(RHTaxTypeManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemTaxOffset = innerJoinOffset;
							innerJoinOffset = itemTaxOffset + RHTaxTypeManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarFlags.ItemSalesIncomeAccount:
							for(int i = 0; i <= RHAccountManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item_SalesIncomeAccount.");
								query.Append(RHAccountManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemSalesIncomeAccountOffset = innerJoinOffset;
							innerJoinOffset = itemSalesIncomeAccountOffset + RHAccountManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
					}
				}

			//
			// Remove trailing comma
			//
			query.Length--;
			if(optionFlags != null)
			{
				query.Append(" FROM ");

				//
				// Start INNER JOIN expressions
				//
				for(int x = 0; x < optionFlags.Length; x++)
					query.Append("(");

				query.Append("kitTessen_Seminars AS DojoSeminar");
			}
			else
			{
				query.Append(" FROM kitTessen_Seminars AS DojoSeminar");
			}
			//
			// Finish INNER JOIN expressions
			//
			if(optionFlags != null)
				for(int x = 0; x < optionFlags.Length; x++)
				{
					switch(optionFlags[x])
					{
						case DojoSeminarFlags.Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Location ON DojoSeminar.LocationID = Location.GreyFoxContactID)");
							break;
						case DojoSeminarFlags.Item:
							query.Append(" LEFT JOIN RH_Items AS Item ON DojoSeminar.ItemID = Item.RHItemID)");
							break;
						case DojoSeminarFlags.ItemParentItem:
							query.Append(" LEFT JOIN RH_Items AS Item_ParentItem ON Item.ParentItemID = Item_ParentItem.RHItemID)");
							break;
						case DojoSeminarFlags.ItemPurchaseVendor:
							query.Append(" LEFT JOIN RH_Vendors AS Item_PurchaseVendor ON Item.PurchaseVendorID = Item_PurchaseVendor.RHVendorID)");
							break;
						case DojoSeminarFlags.ItemPurchaseAccount:
							query.Append(" LEFT JOIN RH_Accounts AS Item_PurchaseAccount ON Item.PurchaseAccountID = Item_PurchaseAccount.RHAccountID)");
							break;
						case DojoSeminarFlags.ItemInventoryAccount:
							query.Append(" LEFT JOIN RH_Accounts AS Item_InventoryAccount ON Item.InventoryAccountID = Item_InventoryAccount.RHAccountID)");
							break;
						case DojoSeminarFlags.ItemTax:
							query.Append(" LEFT JOIN RH_TaxTypes AS Item_Tax ON Item.TaxID = Item_Tax.RHTaxTypeID)");
							break;
						case DojoSeminarFlags.ItemSalesIncomeAccount:
							query.Append(" LEFT JOIN RH_Accounts AS Item_SalesIncomeAccount ON Item.SalesIncomeAccountID = Item_SalesIncomeAccount.RHAccountID)");
							break;
					}
				}

			//
			// Render where clause
			//
			if(whereClause != string.Empty)
			{
				query.Append(" WHERE ");
				query.Append(whereClause);
			}

			//
			// Render sort clause 
			//
			if(sortClause != string.Empty)
			{
				query.Append(" ORDER BY ");
				query.Append(sortClause);
			}

			//
			// Render final semicolon
			//
			query.Append(";");
			database = DatabaseFactory.CreateDatabase();
			dbCommand = database.GetSqlStringCommand(query.ToString());
			#if DEBUG

			try
			{
				r = database.ExecuteReader(dbCommand);
			}
			catch (Exception e)
			{
				string msg = e.Message;
				throw(new Exception(msg + " --- Query: " + query.ToString()));

			}
			#else

			r = database.ExecuteReader(dbCommand);

			#endif

			dojoSeminarCollection = new DojoSeminarCollection();

			while(r.Read())
			{
				DojoSeminar dojoSeminar = ParseFromReader(r, 0, 1);

				// Fill Location
				if(locationOffset != -1 && !r.IsDBNull(locationOffset))
					GreyFoxContactManager.FillFromReader(dojoSeminar.location, "kitTessen_Locations", r, locationOffset, locationOffset+1);

				// Fill Item
				if(itemOffset != -1 && !r.IsDBNull(itemOffset))
				{
					RHItemManager.FillFromReader(dojoSeminar.item, r, itemOffset, itemOffset+1);

					// Fill 
					if(itemParentItemOffset != -1 && !r.IsDBNull(itemParentItemOffset))
						RHItemManager.FillFromReader(dojoSeminar.item.ParentItem, r, itemParentItemOffset, itemParentItemOffset+1);

					// Fill Vendor
					if(itemPurchaseVendorOffset != -1 && !r.IsDBNull(itemPurchaseVendorOffset))
						RHVendorManager.FillFromReader(dojoSeminar.item.PurchaseVendor, r, itemPurchaseVendorOffset, itemPurchaseVendorOffset+1);

					// Fill Purchase Account
					if(itemPurchaseAccountOffset != -1 && !r.IsDBNull(itemPurchaseAccountOffset))
						RHAccountManager.FillFromReader(dojoSeminar.item.PurchaseAccount, r, itemPurchaseAccountOffset, itemPurchaseAccountOffset+1);

					// Fill Asset Account
					if(itemInventoryAccountOffset != -1 && !r.IsDBNull(itemInventoryAccountOffset))
						RHAccountManager.FillFromReader(dojoSeminar.item.InventoryAccount, r, itemInventoryAccountOffset, itemInventoryAccountOffset+1);

					// Fill 
					if(itemTaxOffset != -1 && !r.IsDBNull(itemTaxOffset))
						RHTaxTypeManager.FillFromReader(dojoSeminar.item.Tax, r, itemTaxOffset, itemTaxOffset+1);

					// Fill 
					if(itemSalesIncomeAccountOffset != -1 && !r.IsDBNull(itemSalesIncomeAccountOffset))
						RHAccountManager.FillFromReader(dojoSeminar.item.SalesIncomeAccount, r, itemSalesIncomeAccountOffset, itemSalesIncomeAccountOffset+1);

				}

				dojoSeminarCollection.Add(dojoSeminar);
			}

			// Microsoft DAAB still needs to close readers.
			r.Close();

			return dojoSeminarCollection;
		}

		#endregion

		#region Default NitroCast ParseFromReader Method

		public static DojoSeminar ParseFromReader(IDataReader r, int idOffset, int dataOffset)
		{
			DojoSeminar dojoSeminar = new DojoSeminar();
			FillFromReader(dojoSeminar, r, idOffset, dataOffset);
			return dojoSeminar;
		}

		#endregion

		#region Default NitroCast FillFromReader Method

		/// <summary>
		/// Fills the {0} from a OleIDataReader.
		/// </summary>
		public static void FillFromReader(DojoSeminar dojoSeminar, IDataReader r, int idOffset, int dataOffset)
		{
			dojoSeminar.iD = r.GetInt32(idOffset);
			dojoSeminar.isSynced = true;
			dojoSeminar.isPlaceHolder = false;

			dojoSeminar.name = r.GetString(0+dataOffset);
			dojoSeminar.startDate = r.GetDateTime(1+dataOffset);
			dojoSeminar.endDate = r.GetDateTime(2+dataOffset);
			dojoSeminar.description = r.GetString(3+dataOffset);
			dojoSeminar.isLocal = r.GetBoolean(4+dataOffset);
			if(!r.IsDBNull(5+dataOffset) && r.GetInt32(5+dataOffset) > 0)
			{
				dojoSeminar.location = GreyFoxContact.NewPlaceHolder("kitTessen_Locations", r.GetInt32(5+dataOffset));
			}
			dojoSeminar.classUnitFee = r.GetDecimal(6+dataOffset);
			dojoSeminar.classUnitType = (DojoSeminarClassUnitType)r.GetByte(7+dataOffset);
			dojoSeminar.baseRegistrationFee = r.GetDecimal(8+dataOffset);
			dojoSeminar.registrationEnabled = r.GetBoolean(9+dataOffset);
			dojoSeminar.registrationStart = r.GetDateTime(10+dataOffset);
			dojoSeminar.fullEarlyRegistrationFee = r.GetDecimal(11+dataOffset);
			dojoSeminar.earlyEndDate = r.GetDateTime(12+dataOffset);
			dojoSeminar.fullRegistrationFee = r.GetDecimal(13+dataOffset);
			dojoSeminar.lateStartDate = r.GetDateTime(14+dataOffset);
			dojoSeminar.fullLateRegistrationFee = r.GetDecimal(15+dataOffset);
			dojoSeminar.registrationEnd = r.GetDateTime(16+dataOffset);
			dojoSeminar.details = r.GetString(17+dataOffset);
			dojoSeminar.detailsOverrideUrl = r.GetString(18+dataOffset);
			dojoSeminar.pdfUrl = r.GetString(19+dataOffset);
			if(!r.IsDBNull(20+dataOffset) && r.GetInt32(20+dataOffset) > 0)
			{
				dojoSeminar.item = RHItem.NewPlaceHolder(r.GetInt32(20+dataOffset));
			}
		}

		#endregion

		#region Default NitroCast Fill Methods

		public static void FillOptions(DojoSeminar dojoSeminar)
		{
			StringBuilder s;
			Database database;
			DbCommand dbCommand;
			IDataReader r;
			s = new StringBuilder("SELECT DojoSeminarOptionID FROM kitTessen_SeminarsChildren_Options ");
			s.Append("WHERE DojoSeminarID=");
			s.Append(dojoSeminar.iD);
			s.Append(";");

			database = DatabaseFactory.CreateDatabase();
			dbCommand = database.GetSqlStringCommand(s.ToString());
			r = database.ExecuteReader(dbCommand);

			DojoSeminarOptionCollection options;
			if(dojoSeminar.options != null)
			{
				options = dojoSeminar.options;
				options.Clear();
			}
			else
			{
				options = new DojoSeminarOptionCollection();
				dojoSeminar.options = options;
			}

			while(r.Read())
				options.Add(DojoSeminarOption.NewPlaceHolder(r.GetInt32(0)));

			dojoSeminar.Options = options;
			// Store DojoSeminar in cache.
			if(cacheEnabled) cacheStore(dojoSeminar);
		}

		public static void FillOptions(DojoSeminarCollection dojoSeminarCollection)
		{
			StringBuilder s;
			Database database;
			DbCommand dbCommand;
			IDataReader r;
			if(dojoSeminarCollection.Count > 0)
			{
				s = new StringBuilder("SELECT DojoSeminarID, DojoSeminarOptionID FROM kitTessen_SeminarsChildren_Options ORDER BY DojoSeminarID; ");

				// Clone and sort collection by ID first to fill children in one pass
				DojoSeminarCollection clonedCollection = dojoSeminarCollection.Clone();
				clonedCollection.Sort();

				database = DatabaseFactory.CreateDatabase();
				dbCommand = database.GetSqlStringCommand(s.ToString());
				r = database.ExecuteReader(dbCommand);

				bool more = r.Read();

				foreach(DojoSeminar dojoSeminar in clonedCollection)
				{
					DojoSeminarOptionCollection options;
					if(dojoSeminar.options != null)
					{
						options = dojoSeminar.options;
						options.Clear();
					}
					else
					{
						options = new DojoSeminarOptionCollection();
						dojoSeminar.options = options;
					}

					while(more)
					{
						if(r.GetInt32(0) < dojoSeminar.iD)
						{
							more = r.Read();
						}
						else if(r.GetInt32(0) == dojoSeminar.iD)
						{
							options.Add(DojoSeminarOption.NewPlaceHolder(r.GetInt32(1)));
							more = r.Read();
						}
						else
						{
							break;
						}
					}

					// No need to continue if there are no more records
					if(!more) break;
				}

			}
		}

		#endregion

		#region Default NitroCast Delete Method

		internal static void _delete(int id)
		{
			StringBuilder query;
			Database database;
			DbCommand dbCommand;

			query = new StringBuilder("DELETE FROM kitTessen_Seminars WHERE DojoSeminarID=");
			query.Append(id);
			query.Append(';');

			database = DatabaseFactory.CreateDatabase();
			dbCommand = database.GetSqlStringCommand(query.ToString());
			database.ExecuteNonQuery(dbCommand);


			// Delete child relationships for Options.
			query.Length = 0;
			query.Append("DELETE FROM kitTessen_SeminarsChildren_Options WHERE ");
			query.Append("DojoSeminarID=");
			query.Append(id);
			query.Append(";");
			dbCommand = database.GetSqlStringCommand(query.ToString());
			database.ExecuteNonQuery(dbCommand);
			cacheRemove(id);
		}

		#endregion

		#region Verify Table Methods

		public string VerifyTable(bool repair)
		{
			Database database;
			DbConnection dbConnection;
			DbCommand dbCommand;
			bool match;
			string[] restrictions1;
			StringBuilder msg;

			msg = new StringBuilder();
			restrictions1 = new string[] { null, null, tableName, null };

			database = DatabaseFactory.CreateDatabase();
			dbConnection = database.CreateConnection();
			dbConnection.Open();

			System.Data.DataTable schemaTable = dbConnection.GetSchema("Columns", restrictions1);

			// Loop through the join fields and columns in the
			// table schema to find which fields are missing.
			// Note that this search cannot use BinarySearch due
			// to the fact that JoinFields is unsorted.
			// A sorted JoinFields need not be used because this
			// method should be used sparingly.

			for(int i = 0; i <= JoinFields.GetUpperBound(0); i++)
			{
				match = false;
				foreach(System.Data.DataRow row in schemaTable.Rows)
				{
					if(JoinFields[i,0] == row[3].ToString())
					{
						match = true;
						break;
					}
				}
				if(!match)
				{
					if(repair)
					{
						dbCommand = database.GetSqlStringCommand("ALTER TABLE " + tableName + " ADD COLUMN " + JoinFields[i,0] + " " + JoinFields[i,1] + ";");
						database.ExecuteNonQuery(dbCommand);
						msg.AppendFormat("Added column '{0}'.", JoinFields[i,0]);
					}
					else
					{
						msg.AppendFormat("Missing column '{0}'.", JoinFields[i,0]);
					}
				}
			}

			GreyFoxContactManager locationManager = 
				new GreyFoxContactManager("kitTessen_Locations");
			msg.Append(locationManager.VerifyTable(repair));

			dbConnection.Close();
			return msg.ToString();
		}

		#endregion

		#region Default NitroCast Create Table Methods

		public void CreateReferences()
		{
			StringBuilder query;
			Database database;
			DbCommand dbCommand;

			query = new StringBuilder();
			database = DatabaseFactory.CreateDatabase();
			query.Append("ALTER TABLE kitTessen_Seminars ADD ");
			query.Append(" CONSTRAINT FK_kitTessen_Seminars_Location FOREIGN KEY (LocationID) REFERENCES kitTessen_Locations (GreyFoxContactID),");
			query.Append(" CONSTRAINT FK_kitTessen_Seminars_Item FOREIGN KEY (ItemID) REFERENCES RH_Items (RHItemID);");
			dbCommand = database.GetSqlStringCommand(query.ToString());
			database.ExecuteNonQuery(dbCommand);

			query.Length = 0;
			query.Append("ALTER TABLE kitTessen_SeminarsChildren_Options ADD");
			query.Append(" CONSTRAINT FK_kitTessen_Seminars_kitTessen_SeminarsChildren_Options FOREIGN KEY (DojoSeminarID) REFERENCES kitTessen_Seminars (DojoSeminarID) ON DELETE CASCADE, ");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarsChildren_Options_kitTessen_SeminarOptions FOREIGN KEY (DojoSeminarOptionID) REFERENCES kitTessen_SeminarOptions (DojoSeminarOptionID) ON DELETE CASCADE;");
			dbCommand = database.GetSqlStringCommand(query.ToString());
			database.ExecuteNonQuery(dbCommand);
		}

		public void CreateTable()
		{
			StringBuilder query;
			Database database;
			DbCommand dbCommand;

			database = DatabaseFactory.CreateDatabase();

			if (database.ConnectionStringWithoutCredentials.StartsWith("provider=microsoft.jet.oledb.4.0"))
			{
				// Microsoft Jet SQL
				query = new StringBuilder("CREATE TABLE kitTessen_Seminars ");
				query.Append(" (DojoSeminarID COUNTER(1,1) CONSTRAINT PK_kitTessen_Seminars PRIMARY KEY, " +
					"Name TEXT(75)," +
					"StartDate DATETIME," +
					"EndDate DATETIME," +
					"Description MEMO," +
					"IsLocal BIT," +
					"LocationID LONG," +
					"ClassUnitFee CURRENCY," +
					"ClassUnitType BYTE," +
					"BaseRegistrationFee CURRENCY," +
					"RegistrationEnabled BIT," +
					"RegistrationStart DATETIME," +
					"FullEarlyRegistrationFee CURRENCY," +
					"EarlyEndDate DATETIME," +
					"FullRegistrationFee CURRENCY," +
					"LateStartDate DATETIME," +
					"FullLateRegistrationFee CURRENCY," +
					"RegistrationEnd DATETIME," +
					"Details MEMO," +
					"DetailsOverrideUrl TEXT(255)," +
					"PdfUrl TEXT(255)," +
					"ItemID LONG);");
			}
			else
			{
				// Microsoft SQL Server
				query = new StringBuilder("CREATE TABLE kitTessen_Seminars ");
				query.Append(" (DojoSeminarID INT IDENTITY(1,1) CONSTRAINT PK_kitTessen_Seminars PRIMARY KEY, " +
					"Name NVARCHAR(75)," +
					"StartDate DATETIME," +
					"EndDate DATETIME," +
					"Description NTEXT," +
					"IsLocal BIT," +
					"LocationID INT," +
					"ClassUnitFee MONEY," +
					"ClassUnitType TINYINT," +
					"BaseRegistrationFee MONEY," +
					"RegistrationEnabled BIT," +
					"RegistrationStart DATETIME," +
					"FullEarlyRegistrationFee MONEY," +
					"EarlyEndDate DATETIME," +
					"FullRegistrationFee MONEY," +
					"LateStartDate DATETIME," +
					"FullLateRegistrationFee MONEY," +
					"RegistrationEnd DATETIME," +
					"Details NTEXT," +
					"DetailsOverrideUrl NVARCHAR(255)," +
					"PdfUrl NVARCHAR(255)," +
					"ItemID INT);");
			}

			dbCommand = database.GetSqlStringCommand(query.ToString());
			database.ExecuteNonQuery(dbCommand);

			//
			// Create object level table for Location.
			//
			GreyFoxContactManager locationManager = new GreyFoxContactManager("kitTessen_Locations");
			locationManager.CreateTable();

			//
			// Create children table for Options.
			//
			query.Length = 0;
			if (database.ConnectionStringWithoutCredentials.StartsWith("provider=microsoft.jet.oledb.4.0"))
			{
				query.Append("CREATE TABLE kitTessen_SeminarsChildren_Options ");
				query.Append("(DojoSeminarID LONG, DojoSeminarOptionID LONG);");
				dbCommand = database.GetSqlStringCommand(query.ToString());
				database.ExecuteNonQuery(dbCommand);

			}
			else
			{
				query.Append("CREATE TABLE kitTessen_SeminarsChildren_Options ");
				query.Append("(DojoSeminarID INT, DojoSeminarOptionID INT);");
				dbCommand = database.GetSqlStringCommand(query.ToString());
				database.ExecuteNonQuery(dbCommand);

			}
		}

		#endregion

		#region Cache Methods

		private static void cacheStore(DojoSeminar dojoSeminar)
		{
			CacheManager cache = CacheFactory.GetCacheManager();
			cache.Add("kitTessen_Seminars_" + dojoSeminar.iD.ToString(), dojoSeminar);
		}

		private static DojoSeminar cacheFind(int id)
		{
			object cachedObject;
			CacheManager cache = CacheFactory.GetCacheManager();
			cachedObject = cache.GetData("kitTessen_Seminars_" + id.ToString());
			if(cachedObject == null)
				return null;
			return (DojoSeminar)cachedObject;
		}

		private static void cacheRemove(int id)
		{
			CacheManager cache = CacheFactory.GetCacheManager();
			cache.Remove("kitTessen_Seminars_" + id.ToString());
		}

		#endregion

		//--- Begin Custom Code ---

        public static int CountClassesBySeminar(int iD)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = database.GetSqlStringCommand("SELECT COUNT(*) " +
                "FROM kitTessen_Classes WHERE ParentSeminarID=@ParentSeminarID;");
            database.AddInParameter(dbCommand, "@ParentSeminarID", DbType.Int32, iD);
            int count = (int)database.ExecuteScalar(dbCommand);
            return count;
        }

		//--- End Custom Code ---
	}
}
