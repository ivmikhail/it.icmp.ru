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
using ITCommunity;

namespace ITCommunity
{

    public partial class Favorites : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = GetFavoritesAction();
            int action_post = GetFavoritesActionPost();

            if (action == "del")
            {
                Post.FavoriteDelete(action_post, CurrentUser.User.Id);
            }
            else if (action == "add" && action_post > 0)
            {
                Post.FavoriteAdd(CurrentUser.User.Id, action_post);
            }
            LoadFavorites();
        }

        private string GetFavoritesAction()
        {
            string res = Request.QueryString["a"];
            return res == null ? "" : res; 
        }

        private int GetFavoritesActionPost()
        {
            int post_id;
            Int32.TryParse(Request.QueryString["post"], out post_id);
            return post_id == 0 ? -1 : post_id;
        }

        private int GetPage()
        {
            int page_num;
            Int32.TryParse(Request.QueryString["page"], out page_num);
            return page_num == 0 ? 1 : page_num;
        }
        private void LoadFavorites()
        {
            int total_records = 0;
            int page = GetPage();
            PostsView.PostSource = Post.GetFavorites(CurrentUser.User.Id, page, Global.FavoritesCount, ref total_records);


            FavoritesPager.Fill("favorites.aspx", "", "page", page, total_records, Global.PostsCount);
        }
    }
}
