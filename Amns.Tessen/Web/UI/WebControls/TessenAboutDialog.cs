using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Amns.GreyFox.Web.UI.WebControls;
using System.Reflection;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for TessenAboutDialog.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:TessenAboutDialog runat=server></{0}:TessenAboutDialog>")]
	public class TessenAboutDialog : AboutDialog
	{
		Assembly aspNetAssembly;
		double timeOffset;

		protected override void Render(HtmlTextWriter output)
		{
			// Retrieve reflection information on ASP.net
            aspNetAssembly = Assembly.GetCallingAssembly();
			// Retrieve reflection information on this assembly
			EnsureAssembly();
			// Retrieve Tessen runtime configuration
			timeOffset = double.Parse(ConfigurationManager.AppSettings["Tessen_TimeOffset"]);

			base.Render(output);
		}

		protected override void RenderDetails(HtmlTextWriter output)
		{
			// Output Tessen Configuration
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Tessen Runtime Configuration");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			// Output Tessen DateTime Offset
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Time Offset");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.WriteAttribute("width", "100%");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(timeOffset);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			// Output Tessen DateTime Offset
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Local Time");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.WriteAttribute("width", "100%");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(DateTime.Now.AddHours(timeOffset));
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			#region Client Details

			// Output Server Asp.Net Version
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Client Details");
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			// Output Client Host Name
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("valign", "top");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Client Agent");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.WriteAttribute("width", "100%");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(Context.Request.UserAgent);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			// Output Client Host Address
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Client Host Address");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.WriteAttribute("width", "100%");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(Context.Request.UserHostAddress);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			#endregion

			#region ASP.net Details

			// Output Server Asp.Net Version
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", SubHeaderCssClass);
			output.WriteAttribute("colspan", "2");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Server Details on ");
			output.Write(Context.Server.MachineName);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			// Output Server Asp.Net Version
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("ASP.net Version");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.WriteAttribute("width", "100%");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(aspNetAssembly.GetName().Version.ToString(4));
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			// Output Server DateTime
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row1");
			output.WriteAttribute("nowrap", "true");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write("Time");
			output.WriteEndTag("td");
			output.WriteBeginTag("td");
			output.WriteAttribute("class", "row2");
			output.WriteAttribute("width", "100%");
			output.Write(HtmlTextWriter.TagRightChar);
			output.Write(DateTime.Now);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			#endregion
		}
	}
}