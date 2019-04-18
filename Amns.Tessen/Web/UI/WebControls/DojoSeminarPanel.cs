using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Amns.Tessen;

namespace Amns.Tessen.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for DojoSeminarPanel.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:DojoSeminarPanel runat=server></{0}:DojoSeminarPanel>")]
	public class DojoSeminarPanel : System.Web.UI.Control
	{
		int seminarID;
		string titleCssClass;
		string subtitleCssClass;
		string menuCssClass;
		string datesCssClass;
		Unit cellSpacing;
		Unit cellPadding;
		string errorPath;
		string registrationUrlFormat = "Register.aspx?seminarid={0}";
                	
		[Bindable(true), Category("Appearance"), DefaultValue("")] 
		public string TitleCssClass
		{
			get { return titleCssClass; }
			set { titleCssClass = value; }
		}

		[Bindable(true), Category("Appearance"), DefaultValue("")] 
		public string SubtitleCssClass
		{
			get { return subtitleCssClass; }
			set { subtitleCssClass = value; }
		}

		[Bindable(true), Category("Appearance"), DefaultValue("")] 
		public string DatesCssClass
		{
			get { return datesCssClass; }
			set { datesCssClass = value; }
		}

		[Bindable(true), Category("Appearance"), DefaultValue("")] 
		public string MenuCssClass
		{
			get { return menuCssClass; }
			set { menuCssClass = value; }
		}

		[Bindable(true), Category("Source"), DefaultValue("")]
		public int SeminarID 
		{
			get { return seminarID; }
			set { seminarID = value; }
		}

		[Bindable(true), Category("Source"), DefaultValue("")] 
		public string ErrorPath
		{
			get { return errorPath; }
			set { errorPath = value; }
		}

		[Bindable(true), Category("Source"), DefaultValue("Register.aspx?seminarid={0}")] 
		public string RegistrationUrlFormat
		{
			get { return registrationUrlFormat; }
			set { registrationUrlFormat = value; }
		}

		public Unit CellPadding
		{
			get { return cellPadding; }
			set { cellPadding = value; }
		}

		public Unit CellSpacing
		{
			get { return cellSpacing; }
			set { cellSpacing = value; }
		}
        
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			DojoSeminar seminar = new DojoSeminar(seminarID);
            
			output.Write("<table id=\"{0}\"", UniqueID);
			
			if(!CellPadding.IsEmpty)
				output.Write(" CellPadding=\"{0}\"", CellPadding.ToString());
			if(!CellSpacing.IsEmpty)
				output.Write(" CellPadding=\"{0}\"", CellSpacing.ToString());
			output.Write(" border=\"0\"");
			output.Write(" width=\"100%\"");
			output.Write(">");

			// Output Title
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			if(subtitleCssClass != "") output.WriteAttribute("class", subtitleCssClass);
			output.WriteLine(HtmlTextWriter.TagRightChar);
			output.Write(seminar.Name);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			// Output Subtitle
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			if(subtitleCssClass != "") output.WriteAttribute("class", subtitleCssClass);
			output.WriteLine(HtmlTextWriter.TagRightChar);
			output.Write(seminar.Description);
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
				
			// Output Dates
			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			if(datesCssClass != "") output.WriteAttribute("class", datesCssClass);
			output.WriteLine(HtmlTextWriter.TagRightChar);
			output.Write(seminar.ConstructDateLongString());
			output.WriteEndTag("td");
			output.WriteEndTag("tr");

			// Output Menu
			if(seminar.RegistrationEnabled)
			{
				output.WriteFullBeginTag("tr");
				output.WriteBeginTag("td");
				if(menuCssClass != "") output.WriteAttribute("class", menuCssClass);
				output.WriteLine(HtmlTextWriter.TagRightChar);
				output.WriteBeginTag("a");
				output.WriteAttribute("href", Page.ResolveUrl(string.Format(registrationUrlFormat, seminar.ID)));
				output.Write(HtmlTextWriter.TagRightChar);
				output.Write("Register");
				output.WriteEndTag("a");
				output.WriteEndTag("td");
				output.WriteEndTag("tr");
			}

			output.WriteFullBeginTag("tr");
			output.WriteBeginTag("td");
			output.Write(HtmlTextWriter.TagRightChar);

			if(seminar.DetailsOverrideUrl.Length > 0)
			{
				try
				{
					HttpContext.Current.Server.Execute(seminar.DetailsOverrideUrl, output);
				}
				catch
				{
					if(errorPath != null)
						HttpContext.Current.Server.Execute(errorPath, output);
					else
						output.Write("Could not find overriden details for seminar!");
				}
			}
			else if(seminar.Details.Length > 0)
			{
				output.Write(seminar.Details);
			}
			else
			{
				output.Write("<b>No details were specified for this seminar.</b>");
			}
								
			output.WriteEndTag("td");
			output.WriteEndTag("tr");
			output.WriteEndTag("table");
		}

		protected override void LoadViewState(object savedState) 
		{
			// Customize state management to handle saving state of contained objects.

			if (savedState != null) 
			{
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);					
				if (myState[1] != null)
					seminarID = (int) myState[1];
			}
		}

		protected override object SaveViewState() 
		{
			// Customized state management to handle saving state of contained objects  such as styles.

			object baseState = base.SaveViewState();
			
			object[] myState = new object[2];
			myState[0] = baseState;
			myState[1] = seminarID;

			return myState;
		}
	}
}
