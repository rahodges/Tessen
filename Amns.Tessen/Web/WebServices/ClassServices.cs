using System;
using System.Web.Services;

namespace Amns.Tessen.WebServices
{
	/// <summary>
	/// Summary description for ClassServices.
	/// </summary>
	public class ClassServices : WebService
	{
		public ClassServices()
		{			
		}

		[WebMethod]
		public DojoClassCollection RequestClasses(DateTime start, DateTime end)
		{
			DojoClassCollection results = new DojoClassCollection();
			DojoClassManager m = new DojoClassManager();
			DojoClassCollection classes = m.GetCollection("ClassStart>=#" + start.ToString() + "# AND ClassEnd<=#" +
				end.ToString() + "#", "ClassStart", null);
			return classes;
		}
	}
}