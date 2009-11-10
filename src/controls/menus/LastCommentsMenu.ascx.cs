using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity
{
	public partial class LastCommentsMenu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadLastComments();
			}
		}
		private void LoadLastComments()
		{
			LastComments.DataSource = Comment.GetLasts(Global.ConfigNumParam("LastCommentsCount"));
			LastComments.DataBind();
		}
	}
}
