using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using Amns.QBXmlConnector;

namespace Amns.Tessen.Finance
{
	/// <summary>
	/// Summary description for QuickbooksExporter.
	/// </summary>
	public class QBConnector
	{
		int percentComplete;

		public int PercentComplete
		{
			get { return percentComplete; }
		}

		public QBConnector()
		{
		}

		public string GenerateWDC()
		{
			HttpContext context = HttpContext.Current;

		    return 
				"<QBWCXML>\r\n" +
				"\t<AppName>AMNS GreyFox Tessen</AppName>\r\n" +
				"\t<AppID>2.0</AppID>\r\n" +
				"\t<AppDescription>Dojo Membership application for ASP.net</AppDescription>\r\n" +
				"\t<AppSupport>http://www.shugyokai.org/tessen</AppSupport>\r\n" +
				"\t<AppURL>https://jenova/asdweb/dojomembership/administration/connectors/quickbooks</AppURL>\r\n" +
				"\t<OwnerID>6AE089C1-088A-4099-ABE6-283D1C883A05</OwnerID>\r\n" +
				"\t<FileID>CA3805F8-A3B7-40f7-AF58-BE4FEDC32DCA</FileID>\r\n" +
				"\t<QBType>QuickBooks</QBType>\r\n" +
				"</QBWCXML>";
		}

		public ArrayList BuildRequest()
		{
			ArrayList requests;
			QBInvoiceCollection newInvoiceRequests;
			QBReceivePaymentCollection newReceivePaymentRequests;
			StringBuilder sb;
			StringWriter sw;
			QBXmlTextWriter w;
			int count;

			// Initialize Local Variables
			requests = new ArrayList();
			sb = new StringBuilder();
			sw = new StringWriter(sb);
			w = new QBXmlTextWriter(sw);
			count = 1;

			// Request New Invoices without QBTransactionID
			newInvoiceRequests = buildNewInvoiceRequests();
			foreach(QBInvoice invoice in newInvoiceRequests)
			{
				invoice.WriteAddRequestXml(w, count);
				requests.Add(sb.ToString());
				sb.Length = 0;
				count++;
			}

			// Request New Payments without QBTransactionID
			newReceivePaymentRequests = buildNewReceivePaymentRequests();
			foreach(QBReceivePayment receivePayment in newReceivePaymentRequests)
			{
				receivePayment.WriteAddRequestXml(w, count);
				requests.Add(sb.ToString());
				sb.Length = 0;
				count++;
			}

			w.Close();
			sw.Close();
			sb.Length = 0;

			percentComplete = 100;

			return requests;
		}

		public QBInvoiceCollection buildNewInvoiceRequests()
		{
			QBInvoiceCollection invoices;

			invoices = new QBInvoiceCollection();

			return invoices;
		}

		public QBReceivePaymentCollection buildNewReceivePaymentRequests()
		{
			QBReceivePaymentCollection payments;

			payments = new QBReceivePaymentCollection();

			return payments;
		}

//		public string ExportDuesPayments(DateTime startDate, DateTime endDate)
//		{
//			string query;
//
//			query = "UPDATE kitTessen_DuesPayments " +
//				"SET kitTessen_DuesPayments.ExportFlag = true;";
//
//			OleDbConnection dbConnection = new OleDbConnection(_connectionString);
//			OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
//			dbConnection.Open();
//
////			dbCommand.ExecuteNonQuery();
//
//			query = "SELECT " +
//				"kitTessen_DuesPayments.Amount, " +
//				"kitTessen_DuesPayments.Name, " +
//				"kitTessen_DuesPayments.Address, " +
//				"kitTessen_DuesPayments.City, " +
//				"kitTessen_DuesPayments.State, " +
//				"kitTessen_DuesPayments.PostalCode, " +
//				"kitTessen_DuesInvoices.DojoDuesInvoiceID, " +
//				"kitTessen_MemberTypes.Name, " +
//				"kitTessen_DuesInvoices.StartDate, " +
//				"kitTessen_DuesInvoices.EndDate, " +
//				"kitTessen_DuesInvoices.MemberID, " +
//				"kitTessen_Members_PrivateContacts.FirstName, " +
//				"kitTessen_Members_PrivateContacts.MiddleName, " +
//				"kitTessen_Members_PrivateContacts.LastName " +
//				"FROM (((kitTessen_DuesInvoices " +
//				"INNER JOIN kitTessen_DuesPayments " +
//				"ON kitTessen_DuesInvoices.DojoDuesInvoiceID = kitTessen_DuesPayments.InvoiceID) " +
//				"INNER JOIN kitTessen_Members " +
//				"ON kitTessen_DuesInvoices.MemberID = kitTessen_Members.DojoMemberID) " +
//				"INNER JOIN kitTessen_Members_PrivateContacts " +
//				"ON kitTessen_Members.PrivateContactID = kitTessen_Members_PrivateContacts.GreyFoxContactID) " +
//				"INNER JOIN kitTessen_MemberTypes " +
//				"ON kitTessen_DuesInvoices.MemberTypeID = kitTessen_MemberTypes.DojoMemberTypeID;";
//				//"ORDER BY kitTessenDuesPayments.PaymentDate;";
//			
//			dbCommand.CommandText = query;
//			OleDbDataReader r = dbCommand.ExecuteReader();
//
//			// Instantiate IIFWriter
//			Amns.IIFWriter.TransactionCollection transactions = new Amns.IIFWriter.TransactionCollection();
//			while(r.Read())
//			{
//				Amns.IIFWriter.Transaction t = new Amns.IIFWriter.Transaction();
//				
//				t.TransactionID = -1;
//				t.TransactionType = Amns.IIFWriter.QuickbooksTransactionType.DEPOSIT;
//				t.Date = DateTime.Now;
//				t.Account = "Membership Dues";
//				t.Name = r[11].ToString() + " " + r[12].ToString() + " " + r[13].ToString();
//				t.Amount = r.GetDecimal(0);
//				t.Class = "Dues";
//				transactions.Add(t);
//			}
//
//			r.Close();
//			dbConnection.Close();
//
//			Amns.IIFWriter.IIFExport exporter = new Amns.IIFWriter.IIFExport();
//			exporter.Transactions = transactions;
//			string report = exporter.WriteDeposits();
//
//			return report;
//		}
	}
}
