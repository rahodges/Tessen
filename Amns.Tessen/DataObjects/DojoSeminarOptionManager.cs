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
using Amns.Rappahanock;

namespace Amns.Tessen
{
	#region Child Flags Enumeration

	public enum DojoSeminarOptionFlags : int { Item,
				ItemParentItem,
				ItemPurchaseVendor,
				ItemPurchaseAccount,
				ItemInventoryAccount,
				ItemTax,
				ItemSalesIncomeAccount,
				ItemWebRelatedItems};

	#endregion

	/// <summary>
	/// Datamanager for DojoSeminarOption objects.
	/// </summary>
	public class DojoSeminarOptionManager
	{
		#region Manager Fields

		// Static Fields
		static bool isInitialized;

		// Private Fields
		string tableName = "kitTessen_SeminarOptions";


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
			"DojoSeminarOptionID",
			"Name",
			"Description",
			"Fee",
			"MaxQuantity",
			"ItemID"
		};

		#endregion

		#region Join Field Array

		public static readonly string[,] JoinFields = new string[,] {
			{ "DojoSeminarOptionID", "LONG", "-1" },
			{ "Name", "TEXT(75)", "" },
			{ "Description", "MEMO", "" },
			{ "Fee", "CURRENCY", "" },
			{ "MaxQuantity", "LONG", "" },
			{ "ItemID", "LONG", "null" }
		};

		#endregion

		#region Default NitroCast Constructors

		static DojoSeminarOptionManager()
		{
		}

		public DojoSeminarOptionManager()
		{
		}

		#endregion

		#region Default NitroCast Constructors

		// Initialize
		public void Initialize(string connectionString)
		{
			if(!DojoSeminarOptionManager.isInitialized)
			{
				DojoSeminarOptionManager.isInitialized = true;
			}
		}
		#endregion

		#region Default NitroCast Insert Method

		/// <summary>
		/// Inserts a DojoSeminarOption into the database. All children should have been
		/// saved to the database before insertion. New children will not be
		/// related to this object in the database.
		/// </summary>
		/// <param name="_DojoSeminarOption">The DojoSeminarOption to insert into the database.</param>
		internal static int _insert(DojoSeminarOption dojoSeminarOption)
		{
			int id;
			string query;
			Database database;
			DbCommand dbCommand;

			database = DatabaseFactory.CreateDatabase();

			query = "INSERT INTO kitTessen_SeminarOptions " +
				"(" +
				"Name," +
				"Description," +
				"Fee," +
				"MaxQuantity," +
				"ItemID) VALUES (" +
				"@Name," +
				"@Description," +
				"@Fee," +
				"@MaxQuantity," +
				"@ItemID);";

			if (database.ConnectionStringWithoutCredentials.StartsWith("provider=microsoft.jet.oledb.4.0"))
			{
				// Microsoft Access
				// Connection must remain open for IDENTITY to return correct value,
				// therefore use the dbCommand object's Connection directly to control
				// connection state.
				dbCommand = database.GetSqlStringCommand(query);
				fillParameters(database, dbCommand, dojoSeminarOption);
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
				fillParameters(database, dbCommand, dojoSeminarOption);
				database.AddOutParameter(dbCommand, "@LastID", DbType.Int32, 10);
				database.ExecuteNonQuery(dbCommand);
				id = (int)dbCommand.Parameters["@LastID"].Value;
			}
			// Store dojoSeminarOption in cache.
			if(cacheEnabled) cacheStore(dojoSeminarOption);
			// ************************* WARNING **************************** 
			// Insert operations must invalidate the cached collections.
			// Invalidation MUST invalidate any foreign cached collections that 
			// with children objects this manager provides or else the foreign 
			// caches retain invalidated and potentially corrupt data! 
			// NOTE:
			// NitroCast only allows collection caching on objects that do not 
			// have any children objects to minimize potential corruption. 
			invalidateCachedCollections();
			return id;
		}

		#endregion

		#region Default NitroCast Update Method

		internal static int _update(DojoSeminarOption dojoSeminarOption)
		{
			Database database;
			DbCommand dbCommand;

			database = DatabaseFactory.CreateDatabase();

			dbCommand = database.GetSqlStringCommand("UPDATE kitTessen_SeminarOptions SET Name=@Name," +
				"Description=@Description," +
				"Fee=@Fee," +
				"MaxQuantity=@MaxQuantity," +
				"ItemID=@ItemID WHERE DojoSeminarOptionID=@DojoSeminarOptionID;");

			fillParameters(database, dbCommand, dojoSeminarOption);
			database.AddInParameter(dbCommand, "DojoSeminarOptionID", DbType.Int32, dojoSeminarOption.iD);
			// Abandon remaining updates if no rows have been updated by returning false immediately.
			if (database.ExecuteNonQuery(dbCommand) == 0) return -1;

			// Store dojoSeminarOption in cache.
			if (cacheEnabled) cacheStore(dojoSeminarOption);
			// ************************* WARNING **************************** 
			// Update operations must invalidate the cached collections.
			// Invalidation MUST invalidate any foreign cached collections that 
			// with children objects this manager provides or else the foreign 
			// caches retain invalidated and potentially corrupt data! 
			// NOTE:
			// NitroCast only allows collection caching on objects that do not 
			// have any children objects to minimize potential corruption. 
			// ************************* WARNING **************************** 

			invalidateCachedCollections();

			return dojoSeminarOption.iD;
		}

		#endregion

		#region Default NitroCast Fill Parameters Method

		private static void fillParameters(Database database, DbCommand dbCommand, DojoSeminarOption dojoSeminarOption)
		{
			#region Default

			addParameter(database, dbCommand, "@Name", DbType.String, dojoSeminarOption.name);
			addParameter(database, dbCommand, "@Description", DbType.String, dojoSeminarOption.description);
			addParameter(database, dbCommand, "@Fee", DbType.Currency, dojoSeminarOption.fee);
			addParameter(database, dbCommand, "@MaxQuantity", DbType.Int32, dojoSeminarOption.maxQuantity);

			#endregion

			#region Rappahanock

			if(dojoSeminarOption.item == null)
			{
				addParameter(database, dbCommand, "@ItemID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "@ItemID", DbType.Int32, dojoSeminarOption.item.ID);
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

		internal static bool _fill(DojoSeminarOption dojoSeminarOption)
		{
			// Clone item from cache.
			if(cacheEnabled)
			{
				object cachedObject = cacheFind(dojoSeminarOption.iD);
				if(cachedObject != null)
				{
					((DojoSeminarOption)cachedObject).CopyTo(dojoSeminarOption, true);
					return dojoSeminarOption.isSynced;
				}
			}

			StringBuilder query;
			Database database;
			DbCommand dbCommand;

			query = new StringBuilder("SELECT ");
			query.Append(string.Join(",", InnerJoinFields));
			query.Append(" FROM kitTessen_SeminarOptions WHERE DojoSeminarOptionID=");
			query.Append(dojoSeminarOption.iD);
			query.Append(";");

			database = DatabaseFactory.CreateDatabase();
			dbCommand = database.GetSqlStringCommand(query.ToString());
			IDataReader r = database.ExecuteReader(dbCommand);

			if(!r.Read())
			{
				throw(new Exception(string.Format("Cannot find DojoSeminarOptionID '{0}'.", 
					dojoSeminarOption.iD)));
			}

			FillFromReader(dojoSeminarOption, r, 0, 1);

			// Microsoft DAAB still needs to have the reader closed.
			r.Close();

			// Store dojoSeminarOption in cache.
			if(cacheEnabled) cacheStore(dojoSeminarOption);

			return true;
		}

		#endregion

		#region Default NitroCast GetCollection Method

		public DojoSeminarOptionCollection GetCollection(string whereClause, string sortClause)
		{
			return GetCollection(0, whereClause, sortClause, null);
		}

		public DojoSeminarOptionCollection GetCollection(string whereClause, string sortClause, params DojoSeminarOptionFlags[] optionFlags)
		{
			return GetCollection(0, whereClause, sortClause, optionFlags);
		}

		public DojoSeminarOptionCollection GetCollection(int topCount, string whereClause, string sortClause, params DojoSeminarOptionFlags[] optionFlags)
		{
			StringBuilder query;
			Database database;
			DbCommand dbCommand;
			IDataReader r;
			DojoSeminarOptionCollection dojoSeminarOptionCollection;
			int hashcode;

			// Cache Handling

			hashcode = 0;

			if(cacheEnabled)
			{
				hashcode = topCount.GetHashCode() + 
					whereClause.GetHashCode() +
					sortClause.GetHashCode() +
					tableName.GetHashCode();

				DojoSeminarOptionCollection collection = cacheFindCollection(hashcode);
				if(collection != null)
				{
					return collection;
				}
			}

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
				query.Append("DojoSeminarOption.");
				query.Append(columnName);
				query.Append(",");
			}

			innerJoinOffset = InnerJoinFields.GetUpperBound(0) + 1;
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
						case DojoSeminarOptionFlags.Item:
							for(int i = 0; i <= RHItemManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item.");
								query.Append(RHItemManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemOffset = innerJoinOffset;
							innerJoinOffset = itemOffset + RHItemManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarOptionFlags.ItemParentItem:
							for(int i = 0; i <= RHItemManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item_ParentItem.");
								query.Append(RHItemManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemParentItemOffset = innerJoinOffset;
							innerJoinOffset = itemParentItemOffset + RHItemManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarOptionFlags.ItemPurchaseVendor:
							for(int i = 0; i <= RHVendorManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item_PurchaseVendor.");
								query.Append(RHVendorManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemPurchaseVendorOffset = innerJoinOffset;
							innerJoinOffset = itemPurchaseVendorOffset + RHVendorManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarOptionFlags.ItemPurchaseAccount:
							for(int i = 0; i <= RHAccountManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item_PurchaseAccount.");
								query.Append(RHAccountManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemPurchaseAccountOffset = innerJoinOffset;
							innerJoinOffset = itemPurchaseAccountOffset + RHAccountManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarOptionFlags.ItemInventoryAccount:
							for(int i = 0; i <= RHAccountManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item_InventoryAccount.");
								query.Append(RHAccountManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemInventoryAccountOffset = innerJoinOffset;
							innerJoinOffset = itemInventoryAccountOffset + RHAccountManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarOptionFlags.ItemTax:
							for(int i = 0; i <= RHTaxTypeManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Item_Tax.");
								query.Append(RHTaxTypeManager.InnerJoinFields[i]);
								query.Append(",");
							}
							itemTaxOffset = innerJoinOffset;
							innerJoinOffset = itemTaxOffset + RHTaxTypeManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarOptionFlags.ItemSalesIncomeAccount:
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

				query.Append("kitTessen_SeminarOptions AS DojoSeminarOption");
			}
			else
			{
				query.Append(" FROM kitTessen_SeminarOptions AS DojoSeminarOption");
			}
			//
			// Finish INNER JOIN expressions
			//
			if(optionFlags != null)
				for(int x = 0; x < optionFlags.Length; x++)
				{
					switch(optionFlags[x])
					{
						case DojoSeminarOptionFlags.Item:
							query.Append(" LEFT JOIN RH_Items AS Item ON DojoSeminarOption.ItemID = Item.RHItemID)");
							break;
						case DojoSeminarOptionFlags.ItemParentItem:
							query.Append(" LEFT JOIN RH_Items AS Item_ParentItem ON Item.ParentItemID = Item_ParentItem.RHItemID)");
							break;
						case DojoSeminarOptionFlags.ItemPurchaseVendor:
							query.Append(" LEFT JOIN RH_Vendors AS Item_PurchaseVendor ON Item.PurchaseVendorID = Item_PurchaseVendor.RHVendorID)");
							break;
						case DojoSeminarOptionFlags.ItemPurchaseAccount:
							query.Append(" LEFT JOIN RH_Accounts AS Item_PurchaseAccount ON Item.PurchaseAccountID = Item_PurchaseAccount.RHAccountID)");
							break;
						case DojoSeminarOptionFlags.ItemInventoryAccount:
							query.Append(" LEFT JOIN RH_Accounts AS Item_InventoryAccount ON Item.InventoryAccountID = Item_InventoryAccount.RHAccountID)");
							break;
						case DojoSeminarOptionFlags.ItemTax:
							query.Append(" LEFT JOIN RH_TaxTypes AS Item_Tax ON Item.TaxID = Item_Tax.RHTaxTypeID)");
							break;
						case DojoSeminarOptionFlags.ItemSalesIncomeAccount:
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

			dojoSeminarOptionCollection = new DojoSeminarOptionCollection();

			while(r.Read())
			{
				DojoSeminarOption dojoSeminarOption = ParseFromReader(r, 0, 1);

				// Fill Item
				if(itemOffset != -1 && !r.IsDBNull(itemOffset))
				{
					RHItemManager.FillFromReader(dojoSeminarOption.item, r, itemOffset, itemOffset+1);

					// Fill 
					if(itemParentItemOffset != -1 && !r.IsDBNull(itemParentItemOffset))
						RHItemManager.FillFromReader(dojoSeminarOption.item.ParentItem, r, itemParentItemOffset, itemParentItemOffset+1);

					// Fill Vendor
					if(itemPurchaseVendorOffset != -1 && !r.IsDBNull(itemPurchaseVendorOffset))
						RHVendorManager.FillFromReader(dojoSeminarOption.item.PurchaseVendor, r, itemPurchaseVendorOffset, itemPurchaseVendorOffset+1);

					// Fill Purchase Account
					if(itemPurchaseAccountOffset != -1 && !r.IsDBNull(itemPurchaseAccountOffset))
						RHAccountManager.FillFromReader(dojoSeminarOption.item.PurchaseAccount, r, itemPurchaseAccountOffset, itemPurchaseAccountOffset+1);

					// Fill Asset Account
					if(itemInventoryAccountOffset != -1 && !r.IsDBNull(itemInventoryAccountOffset))
						RHAccountManager.FillFromReader(dojoSeminarOption.item.InventoryAccount, r, itemInventoryAccountOffset, itemInventoryAccountOffset+1);

					// Fill 
					if(itemTaxOffset != -1 && !r.IsDBNull(itemTaxOffset))
						RHTaxTypeManager.FillFromReader(dojoSeminarOption.item.Tax, r, itemTaxOffset, itemTaxOffset+1);

					// Fill 
					if(itemSalesIncomeAccountOffset != -1 && !r.IsDBNull(itemSalesIncomeAccountOffset))
						RHAccountManager.FillFromReader(dojoSeminarOption.item.SalesIncomeAccount, r, itemSalesIncomeAccountOffset, itemSalesIncomeAccountOffset+1);

				}

				dojoSeminarOptionCollection.Add(dojoSeminarOption);
			}

			// Microsoft DAAB still needs to close readers.
			r.Close();

			if(cacheEnabled)
			{
				cacheStoreCollection(hashcode, dojoSeminarOptionCollection);
			}

			return dojoSeminarOptionCollection;
		}

		#endregion

		#region Default NitroCast ParseFromReader Method

		public static DojoSeminarOption ParseFromReader(IDataReader r, int idOffset, int dataOffset)
		{
			DojoSeminarOption dojoSeminarOption = new DojoSeminarOption();
			FillFromReader(dojoSeminarOption, r, idOffset, dataOffset);
			return dojoSeminarOption;
		}

		#endregion

		#region Default NitroCast FillFromReader Method

		/// <summary>
		/// Fills the {0} from a OleIDataReader.
		/// </summary>
		public static void FillFromReader(DojoSeminarOption dojoSeminarOption, IDataReader r, int idOffset, int dataOffset)
		{
			dojoSeminarOption.iD = r.GetInt32(idOffset);
			dojoSeminarOption.isSynced = true;
			dojoSeminarOption.isPlaceHolder = false;

			dojoSeminarOption.name = r.GetString(0+dataOffset);
			dojoSeminarOption.description = r.GetString(1+dataOffset);
			dojoSeminarOption.fee = r.GetDecimal(2+dataOffset);
			dojoSeminarOption.maxQuantity = r.GetInt32(3+dataOffset);
			if(!r.IsDBNull(4+dataOffset) && r.GetInt32(4+dataOffset) > 0)
			{
				dojoSeminarOption.item = RHItem.NewPlaceHolder(r.GetInt32(4+dataOffset));
			}
		}

		#endregion

		#region Default NitroCast Fill Methods

		#endregion

		#region Default NitroCast Delete Method

		internal static void _delete(int id)
		{
			StringBuilder query;
			Database database;
			DbCommand dbCommand;

			query = new StringBuilder("DELETE FROM kitTessen_SeminarOptions WHERE DojoSeminarOptionID=");
			query.Append(id);
			query.Append(';');

			database = DatabaseFactory.CreateDatabase();
			dbCommand = database.GetSqlStringCommand(query.ToString());
			database.ExecuteNonQuery(dbCommand);

			cacheRemove(id);
			// ************************* WARNING **************************** 
			// Insert operations must invalidate the cached collections.
			// Invalidation MUST invalidate any foreign cached collections that 
			// with children objects this manager provides or else the foreign 
			// caches retain invalidated and potentially corrupt data! 
			// NOTE:
			// NitroCast only allows collection caching on objects that do not 
			// have any children objects to minimize potential corruption. 
			invalidateCachedCollections();
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
			query.Append("ALTER TABLE kitTessen_SeminarOptions ADD ");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarOptions_Item FOREIGN KEY (ItemID) REFERENCES RH_Items (RHItemID);");
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
				query = new StringBuilder("CREATE TABLE kitTessen_SeminarOptions ");
				query.Append(" (DojoSeminarOptionID COUNTER(1,1) CONSTRAINT PK_kitTessen_SeminarOptions PRIMARY KEY, " +
					"Name TEXT(75)," +
					"Description MEMO," +
					"Fee CURRENCY," +
					"MaxQuantity LONG," +
					"ItemID LONG);");
			}
			else
			{
				// Microsoft SQL Server
				query = new StringBuilder("CREATE TABLE kitTessen_SeminarOptions ");
				query.Append(" (DojoSeminarOptionID INT IDENTITY(1,1) CONSTRAINT PK_kitTessen_SeminarOptions PRIMARY KEY, " +
					"Name NVARCHAR(75)," +
					"Description NTEXT," +
					"Fee MONEY," +
					"MaxQuantity INT," +
					"ItemID INT);");
			}

			dbCommand = database.GetSqlStringCommand(query.ToString());
			database.ExecuteNonQuery(dbCommand);

		}

		#endregion

		#region Cache Methods

		private static void cacheStore(DojoSeminarOption dojoSeminarOption)
		{
			CacheManager cache = CacheFactory.GetCacheManager();
			cache.Add("kitTessen_SeminarOptions_" + dojoSeminarOption.iD.ToString(), dojoSeminarOption);
		}

		private static DojoSeminarOption cacheFind(int id)
		{
			object cachedObject;
			CacheManager cache = CacheFactory.GetCacheManager();
			cachedObject = cache.GetData("kitTessen_SeminarOptions_" + id.ToString());
			if(cachedObject == null)
				return null;
			return (DojoSeminarOption)cachedObject;
		}

		private static void cacheRemove(int id)
		{
			CacheManager cache = CacheFactory.GetCacheManager();
			cache.Remove("kitTessen_SeminarOptions_" + id.ToString());
		}

		private void cacheStoreCollection(int hashCode, DojoSeminarOptionCollection dojoSeminarOptionCollection)
		{
			CacheManager cache = CacheFactory.GetCacheManager();
			cache.Add(tableName + "_Collection_" + hashCode.ToString(), dojoSeminarOptionCollection);
		}

		private DojoSeminarOptionCollection cacheFindCollection(int hashCode)
		{
			object cachedObject;
			CacheManager cache = CacheFactory.GetCacheManager();
			cachedObject = cache.GetData(tableName + "_Collection_" + hashCode.ToString());
			if(cachedObject == null)
				return null;
			return (DojoSeminarOptionCollection)cachedObject;
		}

		private void cacheRemoveCollection(int hashCode)
		{
			CacheManager cache = CacheFactory.GetCacheManager();
			cache.Remove(tableName + "_Collection_" + hashCode.ToString());
		}

		private static void invalidateCachedCollections()
		{
			CacheManager cache = CacheFactory.GetCacheManager();
			cache.Flush();
		}

		#endregion

		//--- Begin Custom Code ---

		//--- End Custom Code ---
	}
}

