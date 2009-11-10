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

	public partial class UsersList : System.Web.UI.UserControl
    {
        private List<User> users;

        public List<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                FillRepeater();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            RepeaterUsers.DataSource = users;
            RepeaterUsers.DataBind();
        }

        private void FillRepeater()
        {
            RepeaterUsers.DataSource = users;
            RepeaterUsers.DataBind();
        }
    }
}