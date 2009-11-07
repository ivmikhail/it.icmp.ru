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

namespace ITCommunity
{
    public partial class StructurePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SqlDataSourceCategories.ConnectionString = this.SqlDataSourceMenu.ConnectionString = Global.ConnectionString();
            
            if (!IsPostBack)
            {
                RestoreNewCatData();
                RestoreNewMenuItemData();
            }
        }
        protected void LinkButtonDropCache_Click(object sender, EventArgs e)
        {
            DropCache();
            LiteralCacheMessage.Text = "Кеш категорий и меню сброшен";
        }
        private void DropCache()
        {
            AppCache.Remove(Global.ConfigStringParam("CategoriesCacheName"));
            AppCache.Remove(Global.ConfigStringParam("MenuCacheName"));
        }
        protected void LinkButtonAddCat_Click(object sender, EventArgs e)
        {
            Category.Add(new Category(-1, TextBoxCatName.Text, Convert.ToInt32(TextBoxCatSort.Text)));
            RestoreNewCatData();
            gv_categories.DataBind();
        }
        protected void LinkButtonAddMenu_Click(object sender, EventArgs e)
        {
            MenuItem.Add(new MenuItem(-1, Convert.ToInt32(TextBoxMenuParent.Text), TextBoxMenuUrl.Text, Convert.ToInt32(TextBoxMenuSort.Text), TextBoxMenuName.Text, byte.Parse(TextBoxMenuWindow.Text)));
            gv_menuitems.DataBind();
            RestoreNewMenuItemData();
        }
        private void RestoreNewCatData()
        {
            TextBoxCatName.Text = "";
            TextBoxCatSort.Text = "0";
        }
        private void RestoreNewMenuItemData()
        {
            TextBoxMenuParent.Text = "0";
            TextBoxMenuUrl.Text = "";
            TextBoxMenuSort.Text = "0";
            TextBoxMenuName.Text = "";
            TextBoxMenuWindow.Text = "1";
        }
}
}
