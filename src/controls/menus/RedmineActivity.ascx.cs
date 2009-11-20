using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Diagnostics;
using System.IO;
using Rss;
using System.Xml;

namespace ITCommunity
{
    /**
     Всякие методы типа загрузка из кеша, получение данных по rss нужно запихать в какой нибудь App_Code/RedmineActivity.cs
     */ 

    public partial class RedmineActivity : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            List<RedmineActivityItem> items = RedmineActivityData.GetItems();
            ActivityItems.DataSource = items;
            ActivityItems.DataBind();
        }
    }
}