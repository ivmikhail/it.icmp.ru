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

	public partial class Favorites : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string action = GetFavoritesAction();
				Post action_post = Post.GetById(GetFavoritesActionPost());

				int user_id = CurrentUser.User.Id;
				if (action == "del")
				{
					Post.FavoriteDelete(action_post.Id, user_id);
				}
				else if (action == "add" && action_post.Id > 0 && !action_post.IsFavorites(user_id))
				{
					Post.FavoriteAdd(action_post.Id, user_id);
				}
				LoadFavorites();
			}
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
			int favoritesPerPage = Global.ConfigNumParam("FavoritesCount");
			List<Post> favorites = Post.GetFavorites(CurrentUser.User.Id, page, favoritesPerPage, ref total_records);

			FavoritesList.DataBind(favorites, total_records, favoritesPerPage);
		}
	}
}
