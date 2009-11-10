using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity
{
	public partial class SearchMenu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				TextBoxQuery.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) __doPostBack('" + LinkButtonSearch.UniqueID + "','')");
			}
		}

		protected void LinkButtonSearch_Click(object sender, EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath + "/search.aspx?q=" + TextBoxQuery.Text);
		}
	}
}
