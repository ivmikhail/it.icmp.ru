using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITCommunity.Core;
using ITCommunity.Db.Tables;
using ITCommunity.Util;
using System.Web.Mvc;

namespace ITCommunity.Db {

    /// <summary>
    /// Пост хранящийся в БД
    /// </summary>
    public partial class Post {

        public User GetAuthor() {
            return Users.Get(AuthorId);
        }

        /// <summary>
        /// Полностью форматированное в безопасный хтмл описание
        /// </summary>
        public string DescriptionFormatted {
            get { return BBCodeParser.Format(HttpUtility.HtmlEncode(Description)); }
        }

        /// <summary>
        /// Полностью форматированный в безопасный хтмл текст
        /// </summary>
        public string TextFormatted {
            get { return BBCodeParser.Format(HttpUtility.HtmlEncode(Text)); }
        }

        /// <summary>
        /// Полностью форматированный в безопасный хтмл тайтл, ббкод не действует
        /// </summary>
        public string TitleFormatted {
            get { return HttpUtility.HtmlEncode(Title); }
        }

        public List<Category> Categories { get; set; }

        public bool IsFavorite {
            get {
                if (CurrentUser.isAuth) {
                    using (var db = Database.Connect()) {
                        var favorite =
                            from fav in db.Favorites
                            where
                                fav.PostId == Id &&
                                fav.UserId == CurrentUser.User.Id
                            select fav;
                        return favorite.Any();
                    }
                }
                return false;
            }
        }

        partial void OnLoaded() {
            Comments.Load();
            Categories = Tables.Categories.GetByPost(Id);
        }

        public void Validate() {

        }
    }
}
