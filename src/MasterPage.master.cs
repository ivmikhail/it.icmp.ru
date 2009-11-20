using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using ITCommunity;

namespace ITCommunity
{
	public partial class MasterPage : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				HeaderTextLiteral.Text = HeaderText.GetCurrent().Text;
				ThisYear.Text = DateTime.Now.Year.ToString();
			}
		}

		private int GetCatId()
		{
			int id = 0;
			Int32.TryParse(Request.QueryString["cat"], out id);
			return id;
		}

		private int GetPageNum()
		{
			int pageNum = 0;
			Int32.TryParse(Request.QueryString["page"], out pageNum);
			if (pageNum == 1)
			{
				pageNum = 0;
			}
			return pageNum;
		}
	}
}
