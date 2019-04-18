/* ********************************************************** *
 * AMNS DbModel v1.0 DAABManager Data Tier                   *
 * Copyright © 2003-2006 Roy A.E. Hodges                      *
 * All Rights Reserved                                        *
 * ---------------------------------------------------------- *
 * Source code may not be reproduced or redistributed without *
 * written expressed permission from the author.              *
 * Permission is granted to modify source code by licencee.   *
 * These permissions do not extend to third parties.          *
 * ********************************************************** */

using System;
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

	public enum DojoSeminarReservationFlags : int { Registration,
				RegistrationParentSeminar,
				RegistrationContact,
				RegistrationCustomer,
				RegistrationInvoiceLine,
				RegistrationSalesOrderLine,
				ParentReservation,
				ParentReservationRegistration,
				ParentReservationParentReservation,
				ParentReservationClass1,
				ParentReservationClass2,
				ParentReservationClass3,
				ParentReservationClass4,
				ParentReservationClass5,
				ParentReservationClass6,
				ParentReservationClass7,
				ParentReservationClass8,
				ParentReservationClass9,
				ParentReservationClass10,
				ParentReservationDefinition1,
				ParentReservationDefinition2,
				ParentReservationDefinition3,
				Class1,
				Class1Instructor,
				Class1ParentSeminar,
				Class1ParentDefinition,
				Class1Location,
				Class1AccessControlGroup,
				Class2,
				Class2Instructor,
				Class2ParentSeminar,
				Class2ParentDefinition,
				Class2Location,
				Class2AccessControlGroup,
				Class3,
				Class3Instructor,
				Class3ParentSeminar,
				Class3ParentDefinition,
				Class3Location,
				Class3AccessControlGroup,
				Class4,
				Class4Instructor,
				Class4ParentSeminar,
				Class4ParentDefinition,
				Class4Location,
				Class4AccessControlGroup,
				Class5,
				Class5Instructor,
				Class5ParentSeminar,
				Class5ParentDefinition,
				Class5Location,
				Class5AccessControlGroup,
				Class6,
				Class6Instructor,
				Class6ParentSeminar,
				Class6ParentDefinition,
				Class6Location,
				Class6AccessControlGroup,
				Class7,
				Class7Instructor,
				Class7ParentSeminar,
				Class7ParentDefinition,
				Class7Location,
				Class7AccessControlGroup,
				Class8,
				Class8Instructor,
				Class8ParentSeminar,
				Class8ParentDefinition,
				Class8Location,
				Class8AccessControlGroup,
				Class9,
				Class9Instructor,
				Class9ParentSeminar,
				Class9ParentDefinition,
				Class9Location,
				Class9AccessControlGroup,
				Class10,
				Class10Instructor,
				Class10ParentSeminar,
				Class10ParentDefinition,
				Class10Location,
				Class10AccessControlGroup,
				Definition1,
				Definition1AccessControlGroup,
				Definition1Instructor,
				Definition1Location,
				Definition2,
				Definition2AccessControlGroup,
				Definition2Instructor,
				Definition2Location,
				Definition3,
				Definition3AccessControlGroup,
				Definition3Instructor,
				Definition3Location};

	#endregion

	/// <summary>
	/// Datamanager for DojoSeminarReservation objects.
	/// </summary>
	public class DojoSeminarReservationManager
	{
		#region Manager Fields

		// Static Fields
		static bool isInitialized;

		// Private Fields
		string tableName = "kitTessen_SeminarReservations";


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
			"DojoSeminarReservationID",
			"RegistrationID",
			"ParentReservationID",
			"IsBlockReservation",
			"CheckIn",
			"CheckOut",
			"IsClassReservation",
			"Class1ID",
			"Class2ID",
			"Class3ID",
			"Class4ID",
			"Class5ID",
			"Class6ID",
			"Class7ID",
			"Class8ID",
			"Class9ID",
			"Class10ID",
			"IsDefinitionReservation",
			"Definition1ID",
			"Definition2ID",
			"Definition3ID"
		};

		#endregion

		#region Join Field Array

		public static readonly string[,] JoinFields = new string[,] {
			{ "DojoSeminarReservationID", "LONG", "-1" },
			{ "RegistrationID", "LONG", "null" },
			{ "ParentReservationID", "LONG", "null" },
			{ "IsBlockReservation", "BIT", "" },
			{ "CheckIn", "DATETIME", "" },
			{ "CheckOut", "BIT", "" },
			{ "IsClassReservation", "BIT", "" },
			{ "Class1ID", "LONG", "null" },
			{ "Class2ID", "LONG", "null" },
			{ "Class3ID", "LONG", "null" },
			{ "Class4ID", "LONG", "null" },
			{ "Class5ID", "LONG", "null" },
			{ "Class6ID", "LONG", "null" },
			{ "Class7ID", "LONG", "null" },
			{ "Class8ID", "LONG", "null" },
			{ "Class9ID", "LONG", "null" },
			{ "Class10ID", "LONG", "null" },
			{ "IsDefinitionReservation", "BIT", "" },
			{ "Definition1ID", "LONG", "null" },
			{ "Definition2ID", "LONG", "null" },
			{ "Definition3ID", "LONG", "null" }
		};

		#endregion

		#region Default DbModel Constructors

		static DojoSeminarReservationManager()
		{
		}

		public DojoSeminarReservationManager()
		{
		}

		#endregion

		#region Default DbModel Constructors

		// Initialize
		public void Initialize(string connectionString)
		{
			if(!DojoSeminarReservationManager.isInitialized)
			{
				DojoSeminarReservationManager.isInitialized = true;
			}
		}
		#endregion

		#region Default DbModel Insert Method

		/// <summary>
		/// Inserts a DojoSeminarReservation into the database. All children should have been
		/// saved to the database before insertion. New children will not be
		/// related to this object in the database.
		/// </summary>
		/// <param name="_DojoSeminarReservation">The DojoSeminarReservation to insert into the database.</param>
		internal static int _insert(DojoSeminarReservation dojoSeminarReservation)
		{
			int id;
			string query;
			Database database;
			DbCommand dbCommand;

			database = DatabaseFactory.CreateDatabase();

			query = "INSERT INTO kitTessen_SeminarReservations " +
				"(" +
				"RegistrationID," +
				"ParentReservationID," +
				"IsBlockReservation," +
				"CheckIn," +
				"CheckOut," +
				"IsClassReservation," +
				"Class1ID," +
				"Class2ID," +
				"Class3ID," +
				"Class4ID," +
				"Class5ID," +
				"Class6ID," +
				"Class7ID," +
				"Class8ID," +
				"Class9ID," +
				"Class10ID," +
				"IsDefinitionReservation," +
				"Definition1ID," +
				"Definition2ID," +
				"Definition3ID) VALUES (" +
				"@RegistrationID," +
				"@ParentReservationID," +
				"@IsBlockReservation," +
				"@CheckIn," +
				"@CheckOut," +
				"@IsClassReservation," +
				"@Class1ID," +
				"@Class2ID," +
				"@Class3ID," +
				"@Class4ID," +
				"@Class5ID," +
				"@Class6ID," +
				"@Class7ID," +
				"@Class8ID," +
				"@Class9ID," +
				"@Class10ID," +
				"@IsDefinitionReservation," +
				"@Definition1ID," +
				"@Definition2ID," +
				"@Definition3ID);";

			if (database.ConnectionStringWithoutCredentials.StartsWith("provider=microsoft.jet.oledb.4.0"))
			{
				// Microsoft Access
				// Connection must remain open for IDENTITY to return correct value,
				// therefore use the dbCommand object's Connection directly to control
				// connection state.
				dbCommand = database.GetSqlStringCommand(query);
				fillParameters(database, dbCommand, dojoSeminarReservation);
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
				fillParameters(database, dbCommand, dojoSeminarReservation);
				database.AddOutParameter(dbCommand, "@LastID", DbType.Int32, 10);
				database.ExecuteNonQuery(dbCommand);
				id = (int)dbCommand.Parameters["@LastID"].Value;
			}
			// Store dojoSeminarReservation in cache.
			if(cacheEnabled) cacheStore(dojoSeminarReservation);
			return id;
		}

		#endregion

		#region Default DbModel Update Method

		internal static int _update(DojoSeminarReservation dojoSeminarReservation)
		{
			Database database;
			DbCommand dbCommand;

			database = DatabaseFactory.CreateDatabase();

			dbCommand = database.GetSqlStringCommand("UPDATE kitTessen_SeminarReservations SET RegistrationID=@RegistrationID," +
				"ParentReservationID=@ParentReservationID," +
				"IsBlockReservation=@IsBlockReservation," +
				"CheckIn=@CheckIn," +
				"CheckOut=@CheckOut," +
				"IsClassReservation=@IsClassReservation," +
				"Class1ID=@Class1ID," +
				"Class2ID=@Class2ID," +
				"Class3ID=@Class3ID," +
				"Class4ID=@Class4ID," +
				"Class5ID=@Class5ID," +
				"Class6ID=@Class6ID," +
				"Class7ID=@Class7ID," +
				"Class8ID=@Class8ID," +
				"Class9ID=@Class9ID," +
				"Class10ID=@Class10ID," +
				"IsDefinitionReservation=@IsDefinitionReservation," +
				"Definition1ID=@Definition1ID," +
				"Definition2ID=@Definition2ID," +
				"Definition3ID=@Definition3ID WHERE DojoSeminarReservationID=@DojoSeminarReservationID;");

			fillParameters(database, dbCommand, dojoSeminarReservation);
			database.AddInParameter(dbCommand, "DojoSeminarReservationID", DbType.Int32, dojoSeminarReservation.iD);
			// Abandon remaining updates if no rows have been updated by returning false immediately.
			if (database.ExecuteNonQuery(dbCommand) == 0) return -1;

			// Store dojoSeminarReservation in cache.
			if (cacheEnabled) cacheStore(dojoSeminarReservation);

			return dojoSeminarReservation.iD;
		}

		#endregion

		#region Default DbModel Fill Parameters Method

		private static void fillParameters(Database database, DbCommand dbCommand, DojoSeminarReservation dojoSeminarReservation)
		{
			#region General

			if(dojoSeminarReservation.registration == null)
			{
				addParameter(database, dbCommand, "RegistrationID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "RegistrationID", DbType.Int32, dojoSeminarReservation.registration.ID);
			}
			if(dojoSeminarReservation.parentReservation == null)
			{
				addParameter(database, dbCommand, "ParentReservationID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "ParentReservationID", DbType.Int32, dojoSeminarReservation.parentReservation.ID);
			}

			#endregion

			#region Dates

			addParameter(database, dbCommand, "IsBlockReservation", DbType.Boolean, dojoSeminarReservation.isBlockReservation);
			addParameter(database, dbCommand, "CheckIn", DbType.Date, dojoSeminarReservation.checkIn);
			addParameter(database, dbCommand, "CheckOut", DbType.Boolean, dojoSeminarReservation.checkOut);

			#endregion

			#region Classes

			addParameter(database, dbCommand, "IsClassReservation", DbType.Boolean, dojoSeminarReservation.isClassReservation);
			if(dojoSeminarReservation.class1 == null)
			{
				addParameter(database, dbCommand, "Class1ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Class1ID", DbType.Int32, dojoSeminarReservation.class1.ID);
			}
			if(dojoSeminarReservation.class2 == null)
			{
				addParameter(database, dbCommand, "Class2ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Class2ID", DbType.Int32, dojoSeminarReservation.class2.ID);
			}
			if(dojoSeminarReservation.class3 == null)
			{
				addParameter(database, dbCommand, "Class3ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Class3ID", DbType.Int32, dojoSeminarReservation.class3.ID);
			}
			if(dojoSeminarReservation.class4 == null)
			{
				addParameter(database, dbCommand, "Class4ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Class4ID", DbType.Int32, dojoSeminarReservation.class4.ID);
			}
			if(dojoSeminarReservation.class5 == null)
			{
				addParameter(database, dbCommand, "Class5ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Class5ID", DbType.Int32, dojoSeminarReservation.class5.ID);
			}
			if(dojoSeminarReservation.class6 == null)
			{
				addParameter(database, dbCommand, "Class6ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Class6ID", DbType.Int32, dojoSeminarReservation.class6.ID);
			}
			if(dojoSeminarReservation.class7 == null)
			{
				addParameter(database, dbCommand, "Class7ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Class7ID", DbType.Int32, dojoSeminarReservation.class7.ID);
			}
			if(dojoSeminarReservation.class8 == null)
			{
				addParameter(database, dbCommand, "Class8ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Class8ID", DbType.Int32, dojoSeminarReservation.class8.ID);
			}
			if(dojoSeminarReservation.class9 == null)
			{
				addParameter(database, dbCommand, "Class9ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Class9ID", DbType.Int32, dojoSeminarReservation.class9.ID);
			}
			if(dojoSeminarReservation.class10 == null)
			{
				addParameter(database, dbCommand, "Class10ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Class10ID", DbType.Int32, dojoSeminarReservation.class10.ID);
			}

			#endregion

			#region Definitions

			addParameter(database, dbCommand, "IsDefinitionReservation", DbType.Boolean, dojoSeminarReservation.isDefinitionReservation);
			if(dojoSeminarReservation.definition1 == null)
			{
				addParameter(database, dbCommand, "Definition1ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Definition1ID", DbType.Int32, dojoSeminarReservation.definition1.ID);
			}
			if(dojoSeminarReservation.definition2 == null)
			{
				addParameter(database, dbCommand, "Definition2ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Definition2ID", DbType.Int32, dojoSeminarReservation.definition2.ID);
			}
			if(dojoSeminarReservation.definition3 == null)
			{
				addParameter(database, dbCommand, "Definition3ID", DbType.Int32, DBNull.Value);
			}
			else
			{
				addParameter(database, dbCommand, "Definition3ID", DbType.Int32, dojoSeminarReservation.definition3.ID);
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

		#region Default DbModel Fill Method

		internal static bool _fill(DojoSeminarReservation dojoSeminarReservation)
		{
			// Clone item from cache.
			if(cacheEnabled)
			{
				object cachedObject = cacheFind(dojoSeminarReservation.iD);
				if(cachedObject != null)
				{
					((DojoSeminarReservation)cachedObject).CopyTo(dojoSeminarReservation, true);
					return dojoSeminarReservation.isSynced;
				}
			}

			StringBuilder query;
			Database database;
			DbCommand dbCommand;

			query = new StringBuilder("SELECT ");
			query.Append(string.Join(",", InnerJoinFields));
			query.Append(" FROM kitTessen_SeminarReservations WHERE DojoSeminarReservationID=");
			query.Append(dojoSeminarReservation.iD);
			query.Append(";");

			database = DatabaseFactory.CreateDatabase();
			dbCommand = database.GetSqlStringCommand(query.ToString());
			IDataReader r = database.ExecuteReader(dbCommand);

			if(!r.Read())
			{
				throw(new Exception(string.Format("Cannot find DojoSeminarReservationID '{0}'.", 
					dojoSeminarReservation.iD)));
			}

			FillFromReader(dojoSeminarReservation, r, 0, 1);

			// Store dojoSeminarReservation in cache.
			if(cacheEnabled) cacheStore(dojoSeminarReservation);

			return true;
		}

		#endregion

		#region Default DbModel GetCollection Method

		public DojoSeminarReservationCollection GetCollection(string whereClause, string sortClause, params DojoSeminarReservationFlags[] optionFlags)
		{
			return GetCollection(0, whereClause, sortClause, optionFlags);
		}

		public DojoSeminarReservationCollection GetCollection(int topCount, string whereClause, string sortClause, params DojoSeminarReservationFlags[] optionFlags)
		{
			StringBuilder query;
			Database database;
			DbCommand dbCommand;
			IDataReader r;
			DojoSeminarReservationCollection dojoSeminarReservationCollection;

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
				query.Append("DojoSeminarReservation.");
				query.Append(columnName);
				query.Append(",");
			}

			innerJoinOffset = InnerJoinFields.GetUpperBound(0) + 1;
			int registrationOffset = -1;
			int registrationParentSeminarOffset = -1;
			int registrationContactOffset = -1;
			int registrationCustomerOffset = -1;
			int registrationInvoiceLineOffset = -1;
			int registrationSalesOrderLineOffset = -1;
			int parentReservationOffset = -1;
			int parentReservationRegistrationOffset = -1;
			int parentReservationParentReservationOffset = -1;
			int parentReservationClass1Offset = -1;
			int parentReservationClass2Offset = -1;
			int parentReservationClass3Offset = -1;
			int parentReservationClass4Offset = -1;
			int parentReservationClass5Offset = -1;
			int parentReservationClass6Offset = -1;
			int parentReservationClass7Offset = -1;
			int parentReservationClass8Offset = -1;
			int parentReservationClass9Offset = -1;
			int parentReservationClass10Offset = -1;
			int parentReservationDefinition1Offset = -1;
			int parentReservationDefinition2Offset = -1;
			int parentReservationDefinition3Offset = -1;
			int class1Offset = -1;
			int class1InstructorOffset = -1;
			int class1ParentSeminarOffset = -1;
			int class1ParentDefinitionOffset = -1;
			int class1LocationOffset = -1;
			int class1AccessControlGroupOffset = -1;
			int class2Offset = -1;
			int class2InstructorOffset = -1;
			int class2ParentSeminarOffset = -1;
			int class2ParentDefinitionOffset = -1;
			int class2LocationOffset = -1;
			int class2AccessControlGroupOffset = -1;
			int class3Offset = -1;
			int class3InstructorOffset = -1;
			int class3ParentSeminarOffset = -1;
			int class3ParentDefinitionOffset = -1;
			int class3LocationOffset = -1;
			int class3AccessControlGroupOffset = -1;
			int class4Offset = -1;
			int class4InstructorOffset = -1;
			int class4ParentSeminarOffset = -1;
			int class4ParentDefinitionOffset = -1;
			int class4LocationOffset = -1;
			int class4AccessControlGroupOffset = -1;
			int class5Offset = -1;
			int class5InstructorOffset = -1;
			int class5ParentSeminarOffset = -1;
			int class5ParentDefinitionOffset = -1;
			int class5LocationOffset = -1;
			int class5AccessControlGroupOffset = -1;
			int class6Offset = -1;
			int class6InstructorOffset = -1;
			int class6ParentSeminarOffset = -1;
			int class6ParentDefinitionOffset = -1;
			int class6LocationOffset = -1;
			int class6AccessControlGroupOffset = -1;
			int class7Offset = -1;
			int class7InstructorOffset = -1;
			int class7ParentSeminarOffset = -1;
			int class7ParentDefinitionOffset = -1;
			int class7LocationOffset = -1;
			int class7AccessControlGroupOffset = -1;
			int class8Offset = -1;
			int class8InstructorOffset = -1;
			int class8ParentSeminarOffset = -1;
			int class8ParentDefinitionOffset = -1;
			int class8LocationOffset = -1;
			int class8AccessControlGroupOffset = -1;
			int class9Offset = -1;
			int class9InstructorOffset = -1;
			int class9ParentSeminarOffset = -1;
			int class9ParentDefinitionOffset = -1;
			int class9LocationOffset = -1;
			int class9AccessControlGroupOffset = -1;
			int class10Offset = -1;
			int class10InstructorOffset = -1;
			int class10ParentSeminarOffset = -1;
			int class10ParentDefinitionOffset = -1;
			int class10LocationOffset = -1;
			int class10AccessControlGroupOffset = -1;
			int definition1Offset = -1;
			int definition1AccessControlGroupOffset = -1;
			int definition1InstructorOffset = -1;
			int definition1LocationOffset = -1;
			int definition2Offset = -1;
			int definition2AccessControlGroupOffset = -1;
			int definition2InstructorOffset = -1;
			int definition2LocationOffset = -1;
			int definition3Offset = -1;
			int definition3AccessControlGroupOffset = -1;
			int definition3InstructorOffset = -1;
			int definition3LocationOffset = -1;

			//
			// Append Option Flag Fields
			//
			if(optionFlags != null)
				for(int x = 0; x < optionFlags.Length; x++)
				{
					switch(optionFlags[x])
					{
						case DojoSeminarReservationFlags.Registration:
							for(int i = 0; i <= DojoSeminarRegistrationManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Registration.");
								query.Append(DojoSeminarRegistrationManager.InnerJoinFields[i]);
								query.Append(",");
							}
							registrationOffset = innerJoinOffset;
							innerJoinOffset = registrationOffset + DojoSeminarRegistrationManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.RegistrationParentSeminar:
							for(int i = 0; i <= DojoSeminarManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Registration_ParentSeminar.");
								query.Append(DojoSeminarManager.InnerJoinFields[i]);
								query.Append(",");
							}
							registrationParentSeminarOffset = innerJoinOffset;
							innerJoinOffset = registrationParentSeminarOffset + DojoSeminarManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.RegistrationContact:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Registration_Contact.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							registrationContactOffset = innerJoinOffset;
							innerJoinOffset = registrationContactOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.RegistrationCustomer:
							for(int i = 0; i <= RHCustomerManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Registration_Customer.");
								query.Append(RHCustomerManager.InnerJoinFields[i]);
								query.Append(",");
							}
							registrationCustomerOffset = innerJoinOffset;
							innerJoinOffset = registrationCustomerOffset + RHCustomerManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.RegistrationInvoiceLine:
							for(int i = 0; i <= RHInvoiceLineManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Registration_InvoiceLine.");
								query.Append(RHInvoiceLineManager.InnerJoinFields[i]);
								query.Append(",");
							}
							registrationInvoiceLineOffset = innerJoinOffset;
							innerJoinOffset = registrationInvoiceLineOffset + RHInvoiceLineManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.RegistrationSalesOrderLine:
							for(int i = 0; i <= RHSalesOrderLineManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Registration_SalesOrderLine.");
								query.Append(RHSalesOrderLineManager.InnerJoinFields[i]);
								query.Append(",");
							}
							registrationSalesOrderLineOffset = innerJoinOffset;
							innerJoinOffset = registrationSalesOrderLineOffset + RHSalesOrderLineManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservation:
							for(int i = 0; i <= DojoSeminarReservationManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation.");
								query.Append(DojoSeminarReservationManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationOffset = innerJoinOffset;
							innerJoinOffset = parentReservationOffset + DojoSeminarReservationManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationRegistration:
							for(int i = 0; i <= DojoSeminarRegistrationManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Registration.");
								query.Append(DojoSeminarRegistrationManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationRegistrationOffset = innerJoinOffset;
							innerJoinOffset = parentReservationRegistrationOffset + DojoSeminarRegistrationManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationParentReservation:
							for(int i = 0; i <= DojoSeminarReservationManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_ParentReservation.");
								query.Append(DojoSeminarReservationManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationParentReservationOffset = innerJoinOffset;
							innerJoinOffset = parentReservationParentReservationOffset + DojoSeminarReservationManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationClass1:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Class1.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationClass1Offset = innerJoinOffset;
							innerJoinOffset = parentReservationClass1Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationClass2:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Class2.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationClass2Offset = innerJoinOffset;
							innerJoinOffset = parentReservationClass2Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationClass3:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Class3.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationClass3Offset = innerJoinOffset;
							innerJoinOffset = parentReservationClass3Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationClass4:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Class4.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationClass4Offset = innerJoinOffset;
							innerJoinOffset = parentReservationClass4Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationClass5:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Class5.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationClass5Offset = innerJoinOffset;
							innerJoinOffset = parentReservationClass5Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationClass6:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Class6.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationClass6Offset = innerJoinOffset;
							innerJoinOffset = parentReservationClass6Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationClass7:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Class7.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationClass7Offset = innerJoinOffset;
							innerJoinOffset = parentReservationClass7Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationClass8:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Class8.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationClass8Offset = innerJoinOffset;
							innerJoinOffset = parentReservationClass8Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationClass9:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Class9.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationClass9Offset = innerJoinOffset;
							innerJoinOffset = parentReservationClass9Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationClass10:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Class10.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationClass10Offset = innerJoinOffset;
							innerJoinOffset = parentReservationClass10Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationDefinition1:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Definition1.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationDefinition1Offset = innerJoinOffset;
							innerJoinOffset = parentReservationDefinition1Offset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationDefinition2:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Definition2.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationDefinition2Offset = innerJoinOffset;
							innerJoinOffset = parentReservationDefinition2Offset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.ParentReservationDefinition3:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("ParentReservation_Definition3.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							parentReservationDefinition3Offset = innerJoinOffset;
							innerJoinOffset = parentReservationDefinition3Offset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class1:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class1.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class1Offset = innerJoinOffset;
							innerJoinOffset = class1Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class1Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class1_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class1InstructorOffset = innerJoinOffset;
							innerJoinOffset = class1InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class1ParentSeminar:
							for(int i = 0; i <= DojoSeminarManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class1_ParentSeminar.");
								query.Append(DojoSeminarManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class1ParentSeminarOffset = innerJoinOffset;
							innerJoinOffset = class1ParentSeminarOffset + DojoSeminarManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class1ParentDefinition:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class1_ParentDefinition.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class1ParentDefinitionOffset = innerJoinOffset;
							innerJoinOffset = class1ParentDefinitionOffset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class1Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class1_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class1LocationOffset = innerJoinOffset;
							innerJoinOffset = class1LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class1AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class1_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class1AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = class1AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class2:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class2.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class2Offset = innerJoinOffset;
							innerJoinOffset = class2Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class2Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class2_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class2InstructorOffset = innerJoinOffset;
							innerJoinOffset = class2InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class2ParentSeminar:
							for(int i = 0; i <= DojoSeminarManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class2_ParentSeminar.");
								query.Append(DojoSeminarManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class2ParentSeminarOffset = innerJoinOffset;
							innerJoinOffset = class2ParentSeminarOffset + DojoSeminarManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class2ParentDefinition:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class2_ParentDefinition.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class2ParentDefinitionOffset = innerJoinOffset;
							innerJoinOffset = class2ParentDefinitionOffset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class2Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class2_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class2LocationOffset = innerJoinOffset;
							innerJoinOffset = class2LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class2AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class2_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class2AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = class2AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class3:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class3.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class3Offset = innerJoinOffset;
							innerJoinOffset = class3Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class3Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class3_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class3InstructorOffset = innerJoinOffset;
							innerJoinOffset = class3InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class3ParentSeminar:
							for(int i = 0; i <= DojoSeminarManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class3_ParentSeminar.");
								query.Append(DojoSeminarManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class3ParentSeminarOffset = innerJoinOffset;
							innerJoinOffset = class3ParentSeminarOffset + DojoSeminarManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class3ParentDefinition:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class3_ParentDefinition.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class3ParentDefinitionOffset = innerJoinOffset;
							innerJoinOffset = class3ParentDefinitionOffset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class3Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class3_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class3LocationOffset = innerJoinOffset;
							innerJoinOffset = class3LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class3AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class3_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class3AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = class3AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class4:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class4.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class4Offset = innerJoinOffset;
							innerJoinOffset = class4Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class4Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class4_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class4InstructorOffset = innerJoinOffset;
							innerJoinOffset = class4InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class4ParentSeminar:
							for(int i = 0; i <= DojoSeminarManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class4_ParentSeminar.");
								query.Append(DojoSeminarManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class4ParentSeminarOffset = innerJoinOffset;
							innerJoinOffset = class4ParentSeminarOffset + DojoSeminarManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class4ParentDefinition:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class4_ParentDefinition.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class4ParentDefinitionOffset = innerJoinOffset;
							innerJoinOffset = class4ParentDefinitionOffset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class4Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class4_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class4LocationOffset = innerJoinOffset;
							innerJoinOffset = class4LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class4AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class4_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class4AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = class4AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class5:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class5.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class5Offset = innerJoinOffset;
							innerJoinOffset = class5Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class5Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class5_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class5InstructorOffset = innerJoinOffset;
							innerJoinOffset = class5InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class5ParentSeminar:
							for(int i = 0; i <= DojoSeminarManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class5_ParentSeminar.");
								query.Append(DojoSeminarManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class5ParentSeminarOffset = innerJoinOffset;
							innerJoinOffset = class5ParentSeminarOffset + DojoSeminarManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class5ParentDefinition:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class5_ParentDefinition.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class5ParentDefinitionOffset = innerJoinOffset;
							innerJoinOffset = class5ParentDefinitionOffset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class5Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class5_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class5LocationOffset = innerJoinOffset;
							innerJoinOffset = class5LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class5AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class5_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class5AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = class5AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class6:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class6.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class6Offset = innerJoinOffset;
							innerJoinOffset = class6Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class6Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class6_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class6InstructorOffset = innerJoinOffset;
							innerJoinOffset = class6InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class6ParentSeminar:
							for(int i = 0; i <= DojoSeminarManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class6_ParentSeminar.");
								query.Append(DojoSeminarManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class6ParentSeminarOffset = innerJoinOffset;
							innerJoinOffset = class6ParentSeminarOffset + DojoSeminarManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class6ParentDefinition:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class6_ParentDefinition.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class6ParentDefinitionOffset = innerJoinOffset;
							innerJoinOffset = class6ParentDefinitionOffset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class6Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class6_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class6LocationOffset = innerJoinOffset;
							innerJoinOffset = class6LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class6AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class6_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class6AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = class6AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class7:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class7.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class7Offset = innerJoinOffset;
							innerJoinOffset = class7Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class7Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class7_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class7InstructorOffset = innerJoinOffset;
							innerJoinOffset = class7InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class7ParentSeminar:
							for(int i = 0; i <= DojoSeminarManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class7_ParentSeminar.");
								query.Append(DojoSeminarManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class7ParentSeminarOffset = innerJoinOffset;
							innerJoinOffset = class7ParentSeminarOffset + DojoSeminarManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class7ParentDefinition:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class7_ParentDefinition.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class7ParentDefinitionOffset = innerJoinOffset;
							innerJoinOffset = class7ParentDefinitionOffset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class7Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class7_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class7LocationOffset = innerJoinOffset;
							innerJoinOffset = class7LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class7AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class7_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class7AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = class7AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class8:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class8.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class8Offset = innerJoinOffset;
							innerJoinOffset = class8Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class8Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class8_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class8InstructorOffset = innerJoinOffset;
							innerJoinOffset = class8InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class8ParentSeminar:
							for(int i = 0; i <= DojoSeminarManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class8_ParentSeminar.");
								query.Append(DojoSeminarManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class8ParentSeminarOffset = innerJoinOffset;
							innerJoinOffset = class8ParentSeminarOffset + DojoSeminarManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class8ParentDefinition:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class8_ParentDefinition.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class8ParentDefinitionOffset = innerJoinOffset;
							innerJoinOffset = class8ParentDefinitionOffset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class8Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class8_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class8LocationOffset = innerJoinOffset;
							innerJoinOffset = class8LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class8AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class8_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class8AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = class8AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class9:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class9.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class9Offset = innerJoinOffset;
							innerJoinOffset = class9Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class9Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class9_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class9InstructorOffset = innerJoinOffset;
							innerJoinOffset = class9InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class9ParentSeminar:
							for(int i = 0; i <= DojoSeminarManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class9_ParentSeminar.");
								query.Append(DojoSeminarManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class9ParentSeminarOffset = innerJoinOffset;
							innerJoinOffset = class9ParentSeminarOffset + DojoSeminarManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class9ParentDefinition:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class9_ParentDefinition.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class9ParentDefinitionOffset = innerJoinOffset;
							innerJoinOffset = class9ParentDefinitionOffset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class9Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class9_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class9LocationOffset = innerJoinOffset;
							innerJoinOffset = class9LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class9AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class9_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class9AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = class9AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class10:
							for(int i = 0; i <= DojoClassManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class10.");
								query.Append(DojoClassManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class10Offset = innerJoinOffset;
							innerJoinOffset = class10Offset + DojoClassManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class10Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class10_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class10InstructorOffset = innerJoinOffset;
							innerJoinOffset = class10InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class10ParentSeminar:
							for(int i = 0; i <= DojoSeminarManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class10_ParentSeminar.");
								query.Append(DojoSeminarManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class10ParentSeminarOffset = innerJoinOffset;
							innerJoinOffset = class10ParentSeminarOffset + DojoSeminarManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class10ParentDefinition:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class10_ParentDefinition.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class10ParentDefinitionOffset = innerJoinOffset;
							innerJoinOffset = class10ParentDefinitionOffset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class10Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class10_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class10LocationOffset = innerJoinOffset;
							innerJoinOffset = class10LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Class10AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Class10_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							class10AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = class10AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition1:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition1.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition1Offset = innerJoinOffset;
							innerJoinOffset = definition1Offset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition1AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition1_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition1AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = definition1AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition1Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition1_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition1InstructorOffset = innerJoinOffset;
							innerJoinOffset = definition1InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition1Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition1_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition1LocationOffset = innerJoinOffset;
							innerJoinOffset = definition1LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition2:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition2.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition2Offset = innerJoinOffset;
							innerJoinOffset = definition2Offset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition2AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition2_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition2AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = definition2AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition2Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition2_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition2InstructorOffset = innerJoinOffset;
							innerJoinOffset = definition2InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition2Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition2_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition2LocationOffset = innerJoinOffset;
							innerJoinOffset = definition2LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition3:
							for(int i = 0; i <= DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition3.");
								query.Append(DojoClassDefinitionManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition3Offset = innerJoinOffset;
							innerJoinOffset = definition3Offset + DojoClassDefinitionManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition3AccessControlGroup:
							for(int i = 0; i <= DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition3_AccessControlGroup.");
								query.Append(DojoAccessControlGroupManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition3AccessControlGroupOffset = innerJoinOffset;
							innerJoinOffset = definition3AccessControlGroupOffset + DojoAccessControlGroupManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition3Instructor:
							for(int i = 0; i <= DojoMemberManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition3_Instructor.");
								query.Append(DojoMemberManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition3InstructorOffset = innerJoinOffset;
							innerJoinOffset = definition3InstructorOffset + DojoMemberManager.InnerJoinFields.GetUpperBound(0) + 1;
							break;
						case DojoSeminarReservationFlags.Definition3Location:
							for(int i = 0; i <= GreyFoxContactManager.InnerJoinFields.GetUpperBound(0); i++)
							{
								query.Append("Definition3_Location.");
								query.Append(GreyFoxContactManager.InnerJoinFields[i]);
								query.Append(",");
							}
							definition3LocationOffset = innerJoinOffset;
							innerJoinOffset = definition3LocationOffset + GreyFoxContactManager.InnerJoinFields.GetUpperBound(0) + 1;
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

				query.Append("kitTessen_SeminarReservations AS DojoSeminarReservation");
			}
			else
			{
				query.Append(" FROM kitTessen_SeminarReservations AS DojoSeminarReservation");
			}
			//
			// Finish INNER JOIN expressions
			//
			if(optionFlags != null)
				for(int x = 0; x < optionFlags.Length; x++)
				{
					switch(optionFlags[x])
					{
						case DojoSeminarReservationFlags.Registration:
							query.Append(" LEFT JOIN kitTessen_SeminarRegistrations AS Registration ON DojoSeminarReservation.RegistrationID = Registration.DojoSeminarRegistrationID)");
							break;
						case DojoSeminarReservationFlags.RegistrationParentSeminar:
							query.Append(" LEFT JOIN kitTessen_Seminars AS Registration_ParentSeminar ON Registration.ParentSeminarID = Registration_ParentSeminar.DojoSeminarID)");
							break;
						case DojoSeminarReservationFlags.RegistrationContact:
							query.Append(" LEFT JOIN kitTessen_SeminarRegistrations_Contacts AS Registration_Contact ON Registration.ContactID = Registration_Contact.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.RegistrationCustomer:
							query.Append(" LEFT JOIN RH_Customers AS Registration_Customer ON Registration.CustomerID = Registration_Customer.RHCustomerID)");
							break;
						case DojoSeminarReservationFlags.RegistrationInvoiceLine:
							query.Append(" LEFT JOIN RH_InvoiceLines AS Registration_InvoiceLine ON Registration.InvoiceLineID = Registration_InvoiceLine.RHInvoiceLineID)");
							break;
						case DojoSeminarReservationFlags.RegistrationSalesOrderLine:
							query.Append(" LEFT JOIN RH_SalesOrderLines AS Registration_SalesOrderLine ON Registration.SalesOrderLineID = Registration_SalesOrderLine.RHSalesOrderLineID)");
							break;
						case DojoSeminarReservationFlags.ParentReservation:
							query.Append(" LEFT JOIN kitTessen_SeminarReservations AS ParentReservation ON DojoSeminarReservation.ParentReservationID = ParentReservation.DojoSeminarReservationID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationRegistration:
							query.Append(" LEFT JOIN kitTessen_SeminarRegistrations AS ParentReservation_Registration ON ParentReservation.RegistrationID = ParentReservation_Registration.DojoSeminarRegistrationID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationParentReservation:
							query.Append(" LEFT JOIN kitTessen_SeminarReservations AS ParentReservation_ParentReservation ON ParentReservation.ParentReservationID = ParentReservation_ParentReservation.DojoSeminarReservationID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationClass1:
							query.Append(" LEFT JOIN kitTessen_Classes AS ParentReservation_Class1 ON ParentReservation.Class1ID = ParentReservation_Class1.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationClass2:
							query.Append(" LEFT JOIN kitTessen_Classes AS ParentReservation_Class2 ON ParentReservation.Class2ID = ParentReservation_Class2.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationClass3:
							query.Append(" LEFT JOIN kitTessen_Classes AS ParentReservation_Class3 ON ParentReservation.Class3ID = ParentReservation_Class3.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationClass4:
							query.Append(" LEFT JOIN kitTessen_Classes AS ParentReservation_Class4 ON ParentReservation.Class4ID = ParentReservation_Class4.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationClass5:
							query.Append(" LEFT JOIN kitTessen_Classes AS ParentReservation_Class5 ON ParentReservation.Class5ID = ParentReservation_Class5.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationClass6:
							query.Append(" LEFT JOIN kitTessen_Classes AS ParentReservation_Class6 ON ParentReservation.Class6ID = ParentReservation_Class6.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationClass7:
							query.Append(" LEFT JOIN kitTessen_Classes AS ParentReservation_Class7 ON ParentReservation.Class7ID = ParentReservation_Class7.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationClass8:
							query.Append(" LEFT JOIN kitTessen_Classes AS ParentReservation_Class8 ON ParentReservation.Class8ID = ParentReservation_Class8.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationClass9:
							query.Append(" LEFT JOIN kitTessen_Classes AS ParentReservation_Class9 ON ParentReservation.Class9ID = ParentReservation_Class9.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationClass10:
							query.Append(" LEFT JOIN kitTessen_Classes AS ParentReservation_Class10 ON ParentReservation.Class10ID = ParentReservation_Class10.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationDefinition1:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS ParentReservation_Definition1 ON ParentReservation.Definition1ID = ParentReservation_Definition1.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationDefinition2:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS ParentReservation_Definition2 ON ParentReservation.Definition2ID = ParentReservation_Definition2.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.ParentReservationDefinition3:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS ParentReservation_Definition3 ON ParentReservation.Definition3ID = ParentReservation_Definition3.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Class1:
							query.Append(" LEFT JOIN kitTessen_Classes AS Class1 ON DojoSeminarReservation.Class1ID = Class1.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.Class1Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Class1_Instructor ON Class1.InstructorID = Class1_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Class1ParentSeminar:
							query.Append(" LEFT JOIN kitTessen_Seminars AS Class1_ParentSeminar ON Class1.ParentSeminarID = Class1_ParentSeminar.DojoSeminarID)");
							break;
						case DojoSeminarReservationFlags.Class1ParentDefinition:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Class1_ParentDefinition ON Class1.ParentDefinitionID = Class1_ParentDefinition.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Class1Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Class1_Location ON Class1.LocationID = Class1_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Class1AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Class1_AccessControlGroup ON Class1.AccessControlGroupID = Class1_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Class2:
							query.Append(" LEFT JOIN kitTessen_Classes AS Class2 ON DojoSeminarReservation.Class2ID = Class2.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.Class2Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Class2_Instructor ON Class2.InstructorID = Class2_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Class2ParentSeminar:
							query.Append(" LEFT JOIN kitTessen_Seminars AS Class2_ParentSeminar ON Class2.ParentSeminarID = Class2_ParentSeminar.DojoSeminarID)");
							break;
						case DojoSeminarReservationFlags.Class2ParentDefinition:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Class2_ParentDefinition ON Class2.ParentDefinitionID = Class2_ParentDefinition.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Class2Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Class2_Location ON Class2.LocationID = Class2_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Class2AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Class2_AccessControlGroup ON Class2.AccessControlGroupID = Class2_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Class3:
							query.Append(" LEFT JOIN kitTessen_Classes AS Class3 ON DojoSeminarReservation.Class3ID = Class3.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.Class3Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Class3_Instructor ON Class3.InstructorID = Class3_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Class3ParentSeminar:
							query.Append(" LEFT JOIN kitTessen_Seminars AS Class3_ParentSeminar ON Class3.ParentSeminarID = Class3_ParentSeminar.DojoSeminarID)");
							break;
						case DojoSeminarReservationFlags.Class3ParentDefinition:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Class3_ParentDefinition ON Class3.ParentDefinitionID = Class3_ParentDefinition.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Class3Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Class3_Location ON Class3.LocationID = Class3_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Class3AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Class3_AccessControlGroup ON Class3.AccessControlGroupID = Class3_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Class4:
							query.Append(" LEFT JOIN kitTessen_Classes AS Class4 ON DojoSeminarReservation.Class4ID = Class4.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.Class4Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Class4_Instructor ON Class4.InstructorID = Class4_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Class4ParentSeminar:
							query.Append(" LEFT JOIN kitTessen_Seminars AS Class4_ParentSeminar ON Class4.ParentSeminarID = Class4_ParentSeminar.DojoSeminarID)");
							break;
						case DojoSeminarReservationFlags.Class4ParentDefinition:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Class4_ParentDefinition ON Class4.ParentDefinitionID = Class4_ParentDefinition.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Class4Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Class4_Location ON Class4.LocationID = Class4_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Class4AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Class4_AccessControlGroup ON Class4.AccessControlGroupID = Class4_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Class5:
							query.Append(" LEFT JOIN kitTessen_Classes AS Class5 ON DojoSeminarReservation.Class5ID = Class5.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.Class5Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Class5_Instructor ON Class5.InstructorID = Class5_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Class5ParentSeminar:
							query.Append(" LEFT JOIN kitTessen_Seminars AS Class5_ParentSeminar ON Class5.ParentSeminarID = Class5_ParentSeminar.DojoSeminarID)");
							break;
						case DojoSeminarReservationFlags.Class5ParentDefinition:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Class5_ParentDefinition ON Class5.ParentDefinitionID = Class5_ParentDefinition.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Class5Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Class5_Location ON Class5.LocationID = Class5_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Class5AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Class5_AccessControlGroup ON Class5.AccessControlGroupID = Class5_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Class6:
							query.Append(" LEFT JOIN kitTessen_Classes AS Class6 ON DojoSeminarReservation.Class6ID = Class6.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.Class6Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Class6_Instructor ON Class6.InstructorID = Class6_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Class6ParentSeminar:
							query.Append(" LEFT JOIN kitTessen_Seminars AS Class6_ParentSeminar ON Class6.ParentSeminarID = Class6_ParentSeminar.DojoSeminarID)");
							break;
						case DojoSeminarReservationFlags.Class6ParentDefinition:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Class6_ParentDefinition ON Class6.ParentDefinitionID = Class6_ParentDefinition.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Class6Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Class6_Location ON Class6.LocationID = Class6_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Class6AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Class6_AccessControlGroup ON Class6.AccessControlGroupID = Class6_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Class7:
							query.Append(" LEFT JOIN kitTessen_Classes AS Class7 ON DojoSeminarReservation.Class7ID = Class7.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.Class7Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Class7_Instructor ON Class7.InstructorID = Class7_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Class7ParentSeminar:
							query.Append(" LEFT JOIN kitTessen_Seminars AS Class7_ParentSeminar ON Class7.ParentSeminarID = Class7_ParentSeminar.DojoSeminarID)");
							break;
						case DojoSeminarReservationFlags.Class7ParentDefinition:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Class7_ParentDefinition ON Class7.ParentDefinitionID = Class7_ParentDefinition.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Class7Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Class7_Location ON Class7.LocationID = Class7_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Class7AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Class7_AccessControlGroup ON Class7.AccessControlGroupID = Class7_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Class8:
							query.Append(" LEFT JOIN kitTessen_Classes AS Class8 ON DojoSeminarReservation.Class8ID = Class8.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.Class8Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Class8_Instructor ON Class8.InstructorID = Class8_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Class8ParentSeminar:
							query.Append(" LEFT JOIN kitTessen_Seminars AS Class8_ParentSeminar ON Class8.ParentSeminarID = Class8_ParentSeminar.DojoSeminarID)");
							break;
						case DojoSeminarReservationFlags.Class8ParentDefinition:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Class8_ParentDefinition ON Class8.ParentDefinitionID = Class8_ParentDefinition.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Class8Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Class8_Location ON Class8.LocationID = Class8_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Class8AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Class8_AccessControlGroup ON Class8.AccessControlGroupID = Class8_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Class9:
							query.Append(" LEFT JOIN kitTessen_Classes AS Class9 ON DojoSeminarReservation.Class9ID = Class9.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.Class9Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Class9_Instructor ON Class9.InstructorID = Class9_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Class9ParentSeminar:
							query.Append(" LEFT JOIN kitTessen_Seminars AS Class9_ParentSeminar ON Class9.ParentSeminarID = Class9_ParentSeminar.DojoSeminarID)");
							break;
						case DojoSeminarReservationFlags.Class9ParentDefinition:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Class9_ParentDefinition ON Class9.ParentDefinitionID = Class9_ParentDefinition.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Class9Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Class9_Location ON Class9.LocationID = Class9_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Class9AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Class9_AccessControlGroup ON Class9.AccessControlGroupID = Class9_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Class10:
							query.Append(" LEFT JOIN kitTessen_Classes AS Class10 ON DojoSeminarReservation.Class10ID = Class10.DojoClassID)");
							break;
						case DojoSeminarReservationFlags.Class10Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Class10_Instructor ON Class10.InstructorID = Class10_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Class10ParentSeminar:
							query.Append(" LEFT JOIN kitTessen_Seminars AS Class10_ParentSeminar ON Class10.ParentSeminarID = Class10_ParentSeminar.DojoSeminarID)");
							break;
						case DojoSeminarReservationFlags.Class10ParentDefinition:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Class10_ParentDefinition ON Class10.ParentDefinitionID = Class10_ParentDefinition.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Class10Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Class10_Location ON Class10.LocationID = Class10_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Class10AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Class10_AccessControlGroup ON Class10.AccessControlGroupID = Class10_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Definition1:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Definition1 ON DojoSeminarReservation.Definition1ID = Definition1.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Definition1AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Definition1_AccessControlGroup ON Definition1.AccessControlGroupID = Definition1_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Definition1Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Definition1_Instructor ON Definition1.InstructorID = Definition1_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Definition1Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Definition1_Location ON Definition1.LocationID = Definition1_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Definition2:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Definition2 ON DojoSeminarReservation.Definition2ID = Definition2.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Definition2AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Definition2_AccessControlGroup ON Definition2.AccessControlGroupID = Definition2_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Definition2Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Definition2_Instructor ON Definition2.InstructorID = Definition2_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Definition2Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Definition2_Location ON Definition2.LocationID = Definition2_Location.GreyFoxContactID)");
							break;
						case DojoSeminarReservationFlags.Definition3:
							query.Append(" LEFT JOIN kitTessen_ClassDefinitions AS Definition3 ON DojoSeminarReservation.Definition3ID = Definition3.DojoClassDefinitionID)");
							break;
						case DojoSeminarReservationFlags.Definition3AccessControlGroup:
							query.Append(" LEFT JOIN kitTessen_AccessControlGroups AS Definition3_AccessControlGroup ON Definition3.AccessControlGroupID = Definition3_AccessControlGroup.DojoAccessControlGroupID)");
							break;
						case DojoSeminarReservationFlags.Definition3Instructor:
							query.Append(" LEFT JOIN kitTessen_Members AS Definition3_Instructor ON Definition3.InstructorID = Definition3_Instructor.DojoMemberID)");
							break;
						case DojoSeminarReservationFlags.Definition3Location:
							query.Append(" LEFT JOIN kitTessen_Locations AS Definition3_Location ON Definition3.LocationID = Definition3_Location.GreyFoxContactID)");
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

			dojoSeminarReservationCollection = new DojoSeminarReservationCollection();

			while(r.Read())
			{
				DojoSeminarReservation dojoSeminarReservation = ParseFromReader(r, 0, 1);

				// Fill Registration
				if(registrationOffset != -1 && !r.IsDBNull(registrationOffset))
				{
					DojoSeminarRegistrationManager.FillFromReader(dojoSeminarReservation.registration, r, registrationOffset, registrationOffset+1);

					// Fill 
					if(registrationParentSeminarOffset != -1 && !r.IsDBNull(registrationParentSeminarOffset))
						DojoSeminarManager.FillFromReader(dojoSeminarReservation.registration.ParentSeminar, r, registrationParentSeminarOffset, registrationParentSeminarOffset+1);

					// Fill 
					if(registrationContactOffset != -1 && !r.IsDBNull(registrationContactOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.registration.Contact, "kitTessen_SeminarRegistrations_Contacts", r, registrationContactOffset, registrationContactOffset+1);

					// Fill Registrant
					if(registrationCustomerOffset != -1 && !r.IsDBNull(registrationCustomerOffset))
						RHCustomerManager.FillFromReader(dojoSeminarReservation.registration.Customer, r, registrationCustomerOffset, registrationCustomerOffset+1);

					// Fill Invoice Line
					if(registrationInvoiceLineOffset != -1 && !r.IsDBNull(registrationInvoiceLineOffset))
						RHInvoiceLineManager.FillFromReader(dojoSeminarReservation.registration.InvoiceLine, r, registrationInvoiceLineOffset, registrationInvoiceLineOffset+1);

					// Fill Sales Order Line
					if(registrationSalesOrderLineOffset != -1 && !r.IsDBNull(registrationSalesOrderLineOffset))
						RHSalesOrderLineManager.FillFromReader(dojoSeminarReservation.registration.SalesOrderLine, r, registrationSalesOrderLineOffset, registrationSalesOrderLineOffset+1);

				}

				// Fill ParentReservation
				if(parentReservationOffset != -1 && !r.IsDBNull(parentReservationOffset))
				{
					DojoSeminarReservationManager.FillFromReader(dojoSeminarReservation.parentReservation, r, parentReservationOffset, parentReservationOffset+1);

					// Fill 
					if(parentReservationRegistrationOffset != -1 && !r.IsDBNull(parentReservationRegistrationOffset))
						DojoSeminarRegistrationManager.FillFromReader(dojoSeminarReservation.parentReservation.Registration, r, parentReservationRegistrationOffset, parentReservationRegistrationOffset+1);

					// Fill 
					if(parentReservationParentReservationOffset != -1 && !r.IsDBNull(parentReservationParentReservationOffset))
						DojoSeminarReservationManager.FillFromReader(dojoSeminarReservation.parentReservation.ParentReservation, r, parentReservationParentReservationOffset, parentReservationParentReservationOffset+1);

					// Fill 
					if(parentReservationClass1Offset != -1 && !r.IsDBNull(parentReservationClass1Offset))
						DojoClassManager.FillFromReader(dojoSeminarReservation.parentReservation.Class1, r, parentReservationClass1Offset, parentReservationClass1Offset+1);

					// Fill 
					if(parentReservationClass2Offset != -1 && !r.IsDBNull(parentReservationClass2Offset))
						DojoClassManager.FillFromReader(dojoSeminarReservation.parentReservation.Class2, r, parentReservationClass2Offset, parentReservationClass2Offset+1);

					// Fill 
					if(parentReservationClass3Offset != -1 && !r.IsDBNull(parentReservationClass3Offset))
						DojoClassManager.FillFromReader(dojoSeminarReservation.parentReservation.Class3, r, parentReservationClass3Offset, parentReservationClass3Offset+1);

					// Fill 
					if(parentReservationClass4Offset != -1 && !r.IsDBNull(parentReservationClass4Offset))
						DojoClassManager.FillFromReader(dojoSeminarReservation.parentReservation.Class4, r, parentReservationClass4Offset, parentReservationClass4Offset+1);

					// Fill 
					if(parentReservationClass5Offset != -1 && !r.IsDBNull(parentReservationClass5Offset))
						DojoClassManager.FillFromReader(dojoSeminarReservation.parentReservation.Class5, r, parentReservationClass5Offset, parentReservationClass5Offset+1);

					// Fill 
					if(parentReservationClass6Offset != -1 && !r.IsDBNull(parentReservationClass6Offset))
						DojoClassManager.FillFromReader(dojoSeminarReservation.parentReservation.Class6, r, parentReservationClass6Offset, parentReservationClass6Offset+1);

					// Fill 
					if(parentReservationClass7Offset != -1 && !r.IsDBNull(parentReservationClass7Offset))
						DojoClassManager.FillFromReader(dojoSeminarReservation.parentReservation.Class7, r, parentReservationClass7Offset, parentReservationClass7Offset+1);

					// Fill 
					if(parentReservationClass8Offset != -1 && !r.IsDBNull(parentReservationClass8Offset))
						DojoClassManager.FillFromReader(dojoSeminarReservation.parentReservation.Class8, r, parentReservationClass8Offset, parentReservationClass8Offset+1);

					// Fill 
					if(parentReservationClass9Offset != -1 && !r.IsDBNull(parentReservationClass9Offset))
						DojoClassManager.FillFromReader(dojoSeminarReservation.parentReservation.Class9, r, parentReservationClass9Offset, parentReservationClass9Offset+1);

					// Fill 
					if(parentReservationClass10Offset != -1 && !r.IsDBNull(parentReservationClass10Offset))
						DojoClassManager.FillFromReader(dojoSeminarReservation.parentReservation.Class10, r, parentReservationClass10Offset, parentReservationClass10Offset+1);

					// Fill 
					if(parentReservationDefinition1Offset != -1 && !r.IsDBNull(parentReservationDefinition1Offset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.parentReservation.Definition1, r, parentReservationDefinition1Offset, parentReservationDefinition1Offset+1);

					// Fill 
					if(parentReservationDefinition2Offset != -1 && !r.IsDBNull(parentReservationDefinition2Offset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.parentReservation.Definition2, r, parentReservationDefinition2Offset, parentReservationDefinition2Offset+1);

					// Fill 
					if(parentReservationDefinition3Offset != -1 && !r.IsDBNull(parentReservationDefinition3Offset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.parentReservation.Definition3, r, parentReservationDefinition3Offset, parentReservationDefinition3Offset+1);

				}

				// Fill Class1
				if(class1Offset != -1 && !r.IsDBNull(class1Offset))
				{
					DojoClassManager.FillFromReader(dojoSeminarReservation.class1, r, class1Offset, class1Offset+1);

					// Fill 
					if(class1InstructorOffset != -1 && !r.IsDBNull(class1InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.class1.Instructor, r, class1InstructorOffset, class1InstructorOffset+1);

					// Fill 
					if(class1ParentSeminarOffset != -1 && !r.IsDBNull(class1ParentSeminarOffset))
						DojoSeminarManager.FillFromReader(dojoSeminarReservation.class1.ParentSeminar, r, class1ParentSeminarOffset, class1ParentSeminarOffset+1);

					// Fill 
					if(class1ParentDefinitionOffset != -1 && !r.IsDBNull(class1ParentDefinitionOffset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.class1.ParentDefinition, r, class1ParentDefinitionOffset, class1ParentDefinitionOffset+1);

					// Fill 
					if(class1LocationOffset != -1 && !r.IsDBNull(class1LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.class1.Location, "kitTessen_Locations", r, class1LocationOffset, class1LocationOffset+1);

					// Fill 
					if(class1AccessControlGroupOffset != -1 && !r.IsDBNull(class1AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.class1.AccessControlGroup, r, class1AccessControlGroupOffset, class1AccessControlGroupOffset+1);

				}

				// Fill Class2
				if(class2Offset != -1 && !r.IsDBNull(class2Offset))
				{
					DojoClassManager.FillFromReader(dojoSeminarReservation.class2, r, class2Offset, class2Offset+1);

					// Fill 
					if(class2InstructorOffset != -1 && !r.IsDBNull(class2InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.class2.Instructor, r, class2InstructorOffset, class2InstructorOffset+1);

					// Fill 
					if(class2ParentSeminarOffset != -1 && !r.IsDBNull(class2ParentSeminarOffset))
						DojoSeminarManager.FillFromReader(dojoSeminarReservation.class2.ParentSeminar, r, class2ParentSeminarOffset, class2ParentSeminarOffset+1);

					// Fill 
					if(class2ParentDefinitionOffset != -1 && !r.IsDBNull(class2ParentDefinitionOffset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.class2.ParentDefinition, r, class2ParentDefinitionOffset, class2ParentDefinitionOffset+1);

					// Fill 
					if(class2LocationOffset != -1 && !r.IsDBNull(class2LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.class2.Location, "kitTessen_Locations", r, class2LocationOffset, class2LocationOffset+1);

					// Fill 
					if(class2AccessControlGroupOffset != -1 && !r.IsDBNull(class2AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.class2.AccessControlGroup, r, class2AccessControlGroupOffset, class2AccessControlGroupOffset+1);

				}

				// Fill Class3
				if(class3Offset != -1 && !r.IsDBNull(class3Offset))
				{
					DojoClassManager.FillFromReader(dojoSeminarReservation.class3, r, class3Offset, class3Offset+1);

					// Fill 
					if(class3InstructorOffset != -1 && !r.IsDBNull(class3InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.class3.Instructor, r, class3InstructorOffset, class3InstructorOffset+1);

					// Fill 
					if(class3ParentSeminarOffset != -1 && !r.IsDBNull(class3ParentSeminarOffset))
						DojoSeminarManager.FillFromReader(dojoSeminarReservation.class3.ParentSeminar, r, class3ParentSeminarOffset, class3ParentSeminarOffset+1);

					// Fill 
					if(class3ParentDefinitionOffset != -1 && !r.IsDBNull(class3ParentDefinitionOffset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.class3.ParentDefinition, r, class3ParentDefinitionOffset, class3ParentDefinitionOffset+1);

					// Fill 
					if(class3LocationOffset != -1 && !r.IsDBNull(class3LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.class3.Location, "kitTessen_Locations", r, class3LocationOffset, class3LocationOffset+1);

					// Fill 
					if(class3AccessControlGroupOffset != -1 && !r.IsDBNull(class3AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.class3.AccessControlGroup, r, class3AccessControlGroupOffset, class3AccessControlGroupOffset+1);

				}

				// Fill Class4
				if(class4Offset != -1 && !r.IsDBNull(class4Offset))
				{
					DojoClassManager.FillFromReader(dojoSeminarReservation.class4, r, class4Offset, class4Offset+1);

					// Fill 
					if(class4InstructorOffset != -1 && !r.IsDBNull(class4InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.class4.Instructor, r, class4InstructorOffset, class4InstructorOffset+1);

					// Fill 
					if(class4ParentSeminarOffset != -1 && !r.IsDBNull(class4ParentSeminarOffset))
						DojoSeminarManager.FillFromReader(dojoSeminarReservation.class4.ParentSeminar, r, class4ParentSeminarOffset, class4ParentSeminarOffset+1);

					// Fill 
					if(class4ParentDefinitionOffset != -1 && !r.IsDBNull(class4ParentDefinitionOffset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.class4.ParentDefinition, r, class4ParentDefinitionOffset, class4ParentDefinitionOffset+1);

					// Fill 
					if(class4LocationOffset != -1 && !r.IsDBNull(class4LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.class4.Location, "kitTessen_Locations", r, class4LocationOffset, class4LocationOffset+1);

					// Fill 
					if(class4AccessControlGroupOffset != -1 && !r.IsDBNull(class4AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.class4.AccessControlGroup, r, class4AccessControlGroupOffset, class4AccessControlGroupOffset+1);

				}

				// Fill Class5
				if(class5Offset != -1 && !r.IsDBNull(class5Offset))
				{
					DojoClassManager.FillFromReader(dojoSeminarReservation.class5, r, class5Offset, class5Offset+1);

					// Fill 
					if(class5InstructorOffset != -1 && !r.IsDBNull(class5InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.class5.Instructor, r, class5InstructorOffset, class5InstructorOffset+1);

					// Fill 
					if(class5ParentSeminarOffset != -1 && !r.IsDBNull(class5ParentSeminarOffset))
						DojoSeminarManager.FillFromReader(dojoSeminarReservation.class5.ParentSeminar, r, class5ParentSeminarOffset, class5ParentSeminarOffset+1);

					// Fill 
					if(class5ParentDefinitionOffset != -1 && !r.IsDBNull(class5ParentDefinitionOffset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.class5.ParentDefinition, r, class5ParentDefinitionOffset, class5ParentDefinitionOffset+1);

					// Fill 
					if(class5LocationOffset != -1 && !r.IsDBNull(class5LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.class5.Location, "kitTessen_Locations", r, class5LocationOffset, class5LocationOffset+1);

					// Fill 
					if(class5AccessControlGroupOffset != -1 && !r.IsDBNull(class5AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.class5.AccessControlGroup, r, class5AccessControlGroupOffset, class5AccessControlGroupOffset+1);

				}

				// Fill Class6
				if(class6Offset != -1 && !r.IsDBNull(class6Offset))
				{
					DojoClassManager.FillFromReader(dojoSeminarReservation.class6, r, class6Offset, class6Offset+1);

					// Fill 
					if(class6InstructorOffset != -1 && !r.IsDBNull(class6InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.class6.Instructor, r, class6InstructorOffset, class6InstructorOffset+1);

					// Fill 
					if(class6ParentSeminarOffset != -1 && !r.IsDBNull(class6ParentSeminarOffset))
						DojoSeminarManager.FillFromReader(dojoSeminarReservation.class6.ParentSeminar, r, class6ParentSeminarOffset, class6ParentSeminarOffset+1);

					// Fill 
					if(class6ParentDefinitionOffset != -1 && !r.IsDBNull(class6ParentDefinitionOffset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.class6.ParentDefinition, r, class6ParentDefinitionOffset, class6ParentDefinitionOffset+1);

					// Fill 
					if(class6LocationOffset != -1 && !r.IsDBNull(class6LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.class6.Location, "kitTessen_Locations", r, class6LocationOffset, class6LocationOffset+1);

					// Fill 
					if(class6AccessControlGroupOffset != -1 && !r.IsDBNull(class6AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.class6.AccessControlGroup, r, class6AccessControlGroupOffset, class6AccessControlGroupOffset+1);

				}

				// Fill Class7
				if(class7Offset != -1 && !r.IsDBNull(class7Offset))
				{
					DojoClassManager.FillFromReader(dojoSeminarReservation.class7, r, class7Offset, class7Offset+1);

					// Fill 
					if(class7InstructorOffset != -1 && !r.IsDBNull(class7InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.class7.Instructor, r, class7InstructorOffset, class7InstructorOffset+1);

					// Fill 
					if(class7ParentSeminarOffset != -1 && !r.IsDBNull(class7ParentSeminarOffset))
						DojoSeminarManager.FillFromReader(dojoSeminarReservation.class7.ParentSeminar, r, class7ParentSeminarOffset, class7ParentSeminarOffset+1);

					// Fill 
					if(class7ParentDefinitionOffset != -1 && !r.IsDBNull(class7ParentDefinitionOffset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.class7.ParentDefinition, r, class7ParentDefinitionOffset, class7ParentDefinitionOffset+1);

					// Fill 
					if(class7LocationOffset != -1 && !r.IsDBNull(class7LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.class7.Location, "kitTessen_Locations", r, class7LocationOffset, class7LocationOffset+1);

					// Fill 
					if(class7AccessControlGroupOffset != -1 && !r.IsDBNull(class7AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.class7.AccessControlGroup, r, class7AccessControlGroupOffset, class7AccessControlGroupOffset+1);

				}

				// Fill Class8
				if(class8Offset != -1 && !r.IsDBNull(class8Offset))
				{
					DojoClassManager.FillFromReader(dojoSeminarReservation.class8, r, class8Offset, class8Offset+1);

					// Fill 
					if(class8InstructorOffset != -1 && !r.IsDBNull(class8InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.class8.Instructor, r, class8InstructorOffset, class8InstructorOffset+1);

					// Fill 
					if(class8ParentSeminarOffset != -1 && !r.IsDBNull(class8ParentSeminarOffset))
						DojoSeminarManager.FillFromReader(dojoSeminarReservation.class8.ParentSeminar, r, class8ParentSeminarOffset, class8ParentSeminarOffset+1);

					// Fill 
					if(class8ParentDefinitionOffset != -1 && !r.IsDBNull(class8ParentDefinitionOffset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.class8.ParentDefinition, r, class8ParentDefinitionOffset, class8ParentDefinitionOffset+1);

					// Fill 
					if(class8LocationOffset != -1 && !r.IsDBNull(class8LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.class8.Location, "kitTessen_Locations", r, class8LocationOffset, class8LocationOffset+1);

					// Fill 
					if(class8AccessControlGroupOffset != -1 && !r.IsDBNull(class8AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.class8.AccessControlGroup, r, class8AccessControlGroupOffset, class8AccessControlGroupOffset+1);

				}

				// Fill Class9
				if(class9Offset != -1 && !r.IsDBNull(class9Offset))
				{
					DojoClassManager.FillFromReader(dojoSeminarReservation.class9, r, class9Offset, class9Offset+1);

					// Fill 
					if(class9InstructorOffset != -1 && !r.IsDBNull(class9InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.class9.Instructor, r, class9InstructorOffset, class9InstructorOffset+1);

					// Fill 
					if(class9ParentSeminarOffset != -1 && !r.IsDBNull(class9ParentSeminarOffset))
						DojoSeminarManager.FillFromReader(dojoSeminarReservation.class9.ParentSeminar, r, class9ParentSeminarOffset, class9ParentSeminarOffset+1);

					// Fill 
					if(class9ParentDefinitionOffset != -1 && !r.IsDBNull(class9ParentDefinitionOffset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.class9.ParentDefinition, r, class9ParentDefinitionOffset, class9ParentDefinitionOffset+1);

					// Fill 
					if(class9LocationOffset != -1 && !r.IsDBNull(class9LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.class9.Location, "kitTessen_Locations", r, class9LocationOffset, class9LocationOffset+1);

					// Fill 
					if(class9AccessControlGroupOffset != -1 && !r.IsDBNull(class9AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.class9.AccessControlGroup, r, class9AccessControlGroupOffset, class9AccessControlGroupOffset+1);

				}

				// Fill Class10
				if(class10Offset != -1 && !r.IsDBNull(class10Offset))
				{
					DojoClassManager.FillFromReader(dojoSeminarReservation.class10, r, class10Offset, class10Offset+1);

					// Fill 
					if(class10InstructorOffset != -1 && !r.IsDBNull(class10InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.class10.Instructor, r, class10InstructorOffset, class10InstructorOffset+1);

					// Fill 
					if(class10ParentSeminarOffset != -1 && !r.IsDBNull(class10ParentSeminarOffset))
						DojoSeminarManager.FillFromReader(dojoSeminarReservation.class10.ParentSeminar, r, class10ParentSeminarOffset, class10ParentSeminarOffset+1);

					// Fill 
					if(class10ParentDefinitionOffset != -1 && !r.IsDBNull(class10ParentDefinitionOffset))
						DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.class10.ParentDefinition, r, class10ParentDefinitionOffset, class10ParentDefinitionOffset+1);

					// Fill 
					if(class10LocationOffset != -1 && !r.IsDBNull(class10LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.class10.Location, "kitTessen_Locations", r, class10LocationOffset, class10LocationOffset+1);

					// Fill 
					if(class10AccessControlGroupOffset != -1 && !r.IsDBNull(class10AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.class10.AccessControlGroup, r, class10AccessControlGroupOffset, class10AccessControlGroupOffset+1);

				}

				// Fill Definition1
				if(definition1Offset != -1 && !r.IsDBNull(definition1Offset))
				{
					DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.definition1, r, definition1Offset, definition1Offset+1);

					// Fill 
					if(definition1AccessControlGroupOffset != -1 && !r.IsDBNull(definition1AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.definition1.AccessControlGroup, r, definition1AccessControlGroupOffset, definition1AccessControlGroupOffset+1);

					// Fill 
					if(definition1InstructorOffset != -1 && !r.IsDBNull(definition1InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.definition1.Instructor, r, definition1InstructorOffset, definition1InstructorOffset+1);

					// Fill 
					if(definition1LocationOffset != -1 && !r.IsDBNull(definition1LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.definition1.Location, "kitTessen_Locations", r, definition1LocationOffset, definition1LocationOffset+1);

				}

				// Fill Definition2
				if(definition2Offset != -1 && !r.IsDBNull(definition2Offset))
				{
					DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.definition2, r, definition2Offset, definition2Offset+1);

					// Fill 
					if(definition2AccessControlGroupOffset != -1 && !r.IsDBNull(definition2AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.definition2.AccessControlGroup, r, definition2AccessControlGroupOffset, definition2AccessControlGroupOffset+1);

					// Fill 
					if(definition2InstructorOffset != -1 && !r.IsDBNull(definition2InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.definition2.Instructor, r, definition2InstructorOffset, definition2InstructorOffset+1);

					// Fill 
					if(definition2LocationOffset != -1 && !r.IsDBNull(definition2LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.definition2.Location, "kitTessen_Locations", r, definition2LocationOffset, definition2LocationOffset+1);

				}

				// Fill Definition3
				if(definition3Offset != -1 && !r.IsDBNull(definition3Offset))
				{
					DojoClassDefinitionManager.FillFromReader(dojoSeminarReservation.definition3, r, definition3Offset, definition3Offset+1);

					// Fill 
					if(definition3AccessControlGroupOffset != -1 && !r.IsDBNull(definition3AccessControlGroupOffset))
						DojoAccessControlGroupManager.FillFromReader(dojoSeminarReservation.definition3.AccessControlGroup, r, definition3AccessControlGroupOffset, definition3AccessControlGroupOffset+1);

					// Fill 
					if(definition3InstructorOffset != -1 && !r.IsDBNull(definition3InstructorOffset))
						DojoMemberManager.FillFromReader(dojoSeminarReservation.definition3.Instructor, r, definition3InstructorOffset, definition3InstructorOffset+1);

					// Fill 
					if(definition3LocationOffset != -1 && !r.IsDBNull(definition3LocationOffset))
						GreyFoxContactManager.FillFromReader(dojoSeminarReservation.definition3.Location, "kitTessen_Locations", r, definition3LocationOffset, definition3LocationOffset+1);

				}

				dojoSeminarReservationCollection.Add(dojoSeminarReservation);
			}

			return dojoSeminarReservationCollection;
		}

		#endregion

		#region Default DbModel ParseFromReader Method

		public static DojoSeminarReservation ParseFromReader(IDataReader r, int idOffset, int dataOffset)
		{
			DojoSeminarReservation dojoSeminarReservation = new DojoSeminarReservation();
			FillFromReader(dojoSeminarReservation, r, idOffset, dataOffset);
			return dojoSeminarReservation;
		}

		#endregion

		#region Default DbModel FillFromReader Method

		/// <summary>
		/// Fills the {0} from a OleIDataReader.
		/// </summary>
		public static void FillFromReader(DojoSeminarReservation dojoSeminarReservation, IDataReader r, int idOffset, int dataOffset)
		{
			dojoSeminarReservation.iD = r.GetInt32(idOffset);
			dojoSeminarReservation.isSynced = true;
			dojoSeminarReservation.isPlaceHolder = false;

			if(!r.IsDBNull(0+dataOffset) && r.GetInt32(0+dataOffset) > 0)
			{
				dojoSeminarReservation.registration = DojoSeminarRegistration.NewPlaceHolder(r.GetInt32(0+dataOffset));
			}
			if(!r.IsDBNull(1+dataOffset) && r.GetInt32(1+dataOffset) > 0)
			{
				dojoSeminarReservation.parentReservation = DojoSeminarReservation.NewPlaceHolder(r.GetInt32(1+dataOffset));
			}
			dojoSeminarReservation.isBlockReservation = r.GetBoolean(2+dataOffset);
			dojoSeminarReservation.checkIn = r.GetDateTime(3+dataOffset);
			dojoSeminarReservation.checkOut = r.GetBoolean(4+dataOffset);
			dojoSeminarReservation.isClassReservation = r.GetBoolean(5+dataOffset);
			if(!r.IsDBNull(6+dataOffset) && r.GetInt32(6+dataOffset) > 0)
			{
				dojoSeminarReservation.class1 = DojoClass.NewPlaceHolder(r.GetInt32(6+dataOffset));
			}
			if(!r.IsDBNull(7+dataOffset) && r.GetInt32(7+dataOffset) > 0)
			{
				dojoSeminarReservation.class2 = DojoClass.NewPlaceHolder(r.GetInt32(7+dataOffset));
			}
			if(!r.IsDBNull(8+dataOffset) && r.GetInt32(8+dataOffset) > 0)
			{
				dojoSeminarReservation.class3 = DojoClass.NewPlaceHolder(r.GetInt32(8+dataOffset));
			}
			if(!r.IsDBNull(9+dataOffset) && r.GetInt32(9+dataOffset) > 0)
			{
				dojoSeminarReservation.class4 = DojoClass.NewPlaceHolder(r.GetInt32(9+dataOffset));
			}
			if(!r.IsDBNull(10+dataOffset) && r.GetInt32(10+dataOffset) > 0)
			{
				dojoSeminarReservation.class5 = DojoClass.NewPlaceHolder(r.GetInt32(10+dataOffset));
			}
			if(!r.IsDBNull(11+dataOffset) && r.GetInt32(11+dataOffset) > 0)
			{
				dojoSeminarReservation.class6 = DojoClass.NewPlaceHolder(r.GetInt32(11+dataOffset));
			}
			if(!r.IsDBNull(12+dataOffset) && r.GetInt32(12+dataOffset) > 0)
			{
				dojoSeminarReservation.class7 = DojoClass.NewPlaceHolder(r.GetInt32(12+dataOffset));
			}
			if(!r.IsDBNull(13+dataOffset) && r.GetInt32(13+dataOffset) > 0)
			{
				dojoSeminarReservation.class8 = DojoClass.NewPlaceHolder(r.GetInt32(13+dataOffset));
			}
			if(!r.IsDBNull(14+dataOffset) && r.GetInt32(14+dataOffset) > 0)
			{
				dojoSeminarReservation.class9 = DojoClass.NewPlaceHolder(r.GetInt32(14+dataOffset));
			}
			if(!r.IsDBNull(15+dataOffset) && r.GetInt32(15+dataOffset) > 0)
			{
				dojoSeminarReservation.class10 = DojoClass.NewPlaceHolder(r.GetInt32(15+dataOffset));
			}
			dojoSeminarReservation.isDefinitionReservation = r.GetBoolean(16+dataOffset);
			if(!r.IsDBNull(17+dataOffset) && r.GetInt32(17+dataOffset) > 0)
			{
				dojoSeminarReservation.definition1 = DojoClassDefinition.NewPlaceHolder(r.GetInt32(17+dataOffset));
			}
			if(!r.IsDBNull(18+dataOffset) && r.GetInt32(18+dataOffset) > 0)
			{
				dojoSeminarReservation.definition2 = DojoClassDefinition.NewPlaceHolder(r.GetInt32(18+dataOffset));
			}
			if(!r.IsDBNull(19+dataOffset) && r.GetInt32(19+dataOffset) > 0)
			{
				dojoSeminarReservation.definition3 = DojoClassDefinition.NewPlaceHolder(r.GetInt32(19+dataOffset));
			}
		}

		#endregion

		#region Default DbModel Fill Methods

		#endregion

		#region Default DbModel Delete Method

		internal static void _delete(int id)
		{
			StringBuilder query;
			Database database;
			DbCommand dbCommand;

			query = new StringBuilder("DELETE FROM kitTessen_SeminarReservations WHERE DojoSeminarReservationID=");
			query.Append(id);
			query.Append(';');

			database = DatabaseFactory.CreateDatabase();
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

			return msg.ToString();
		}

		#endregion

		#region Default DbModel Create Table Methods

		public void CreateReferences()
		{
			StringBuilder query;
			Database database;
			DbCommand dbCommand;

			query = new StringBuilder();
			database = DatabaseFactory.CreateDatabase();
			query.Append("ALTER TABLE kitTessen_SeminarReservations ADD ");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Registration FOREIGN KEY (RegistrationID) REFERENCES kitTessen_SeminarRegistrations (DojoSeminarRegistrationID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_ParentReservation FOREIGN KEY (ParentReservationID) REFERENCES kitTessen_SeminarReservations (DojoSeminarReservationID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Class1 FOREIGN KEY (Class1ID) REFERENCES kitTessen_Classes (DojoClassID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Class2 FOREIGN KEY (Class2ID) REFERENCES kitTessen_Classes (DojoClassID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Class3 FOREIGN KEY (Class3ID) REFERENCES kitTessen_Classes (DojoClassID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Class4 FOREIGN KEY (Class4ID) REFERENCES kitTessen_Classes (DojoClassID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Class5 FOREIGN KEY (Class5ID) REFERENCES kitTessen_Classes (DojoClassID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Class6 FOREIGN KEY (Class6ID) REFERENCES kitTessen_Classes (DojoClassID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Class7 FOREIGN KEY (Class7ID) REFERENCES kitTessen_Classes (DojoClassID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Class8 FOREIGN KEY (Class8ID) REFERENCES kitTessen_Classes (DojoClassID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Class9 FOREIGN KEY (Class9ID) REFERENCES kitTessen_Classes (DojoClassID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Class10 FOREIGN KEY (Class10ID) REFERENCES kitTessen_Classes (DojoClassID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Definition1 FOREIGN KEY (Definition1ID) REFERENCES kitTessen_ClassDefinitions (DojoClassDefinitionID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Definition2 FOREIGN KEY (Definition2ID) REFERENCES kitTessen_ClassDefinitions (DojoClassDefinitionID),");
			query.Append(" CONSTRAINT FK_kitTessen_SeminarReservations_Definition3 FOREIGN KEY (Definition3ID) REFERENCES kitTessen_ClassDefinitions (DojoClassDefinitionID);");
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
				query = new StringBuilder("CREATE TABLE kitTessen_SeminarReservations ");
				query.Append(" (DojoSeminarReservationID COUNTER(1,1) CONSTRAINT PK_kitTessen_SeminarReservations PRIMARY KEY, " +
					"RegistrationID LONG," +
					"ParentReservationID LONG," +
					"IsBlockReservation BIT," +
					"CheckIn DATETIME," +
					"CheckOut BIT," +
					"IsClassReservation BIT," +
					"Class1ID LONG," +
					"Class2ID LONG," +
					"Class3ID LONG," +
					"Class4ID LONG," +
					"Class5ID LONG," +
					"Class6ID LONG," +
					"Class7ID LONG," +
					"Class8ID LONG," +
					"Class9ID LONG," +
					"Class10ID LONG," +
					"IsDefinitionReservation BIT," +
					"Definition1ID LONG," +
					"Definition2ID LONG," +
					"Definition3ID LONG);");
			}
			else
			{
				// Microsoft SQL Server
				query = new StringBuilder("CREATE TABLE kitTessen_SeminarReservations ");
				query.Append(" (DojoSeminarReservationID INT IDENTITY(1,1) CONSTRAINT PK_kitTessen_SeminarReservations PRIMARY KEY, " +
					"RegistrationID INT," +
					"ParentReservationID INT," +
					"IsBlockReservation BIT," +
					"CheckIn DATETIME," +
					"CheckOut BIT," +
					"IsClassReservation BIT," +
					"Class1ID INT," +
					"Class2ID INT," +
					"Class3ID INT," +
					"Class4ID INT," +
					"Class5ID INT," +
					"Class6ID INT," +
					"Class7ID INT," +
					"Class8ID INT," +
					"Class9ID INT," +
					"Class10ID INT," +
					"IsDefinitionReservation BIT," +
					"Definition1ID INT," +
					"Definition2ID INT," +
					"Definition3ID INT);");
			}

			dbCommand = database.GetSqlStringCommand(query.ToString());
			database.ExecuteNonQuery(dbCommand);

		}

		#endregion

		#region Cache Methods

		private static void cacheStore(DojoSeminarReservation dojoSeminarReservation)
		{
			CacheManager cache = CacheFactory.GetCacheManager();
			cache.Add("kitTessen_SeminarReservations_" + dojoSeminarReservation.iD.ToString(), dojoSeminarReservation);
		}

		private static DojoSeminarReservation cacheFind(int id)
		{
			object cachedObject;
			CacheManager cache = CacheFactory.GetCacheManager();
			cachedObject = cache.GetData("kitTessen_SeminarReservations_" + id.ToString());
			if(cachedObject == null)
				return null;
			return (DojoSeminarReservation)cachedObject;
		}

		private static void cacheRemove(int id)
		{
			CacheManager cache = CacheFactory.GetCacheManager();
			cache.Remove("kitTessen_SeminarReservations_" + id.ToString());
		}

		#endregion

	}
}

