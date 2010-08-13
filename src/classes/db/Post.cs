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

        public List<Category> Categories { get; set; }

        public bool IsFavorite { get; set; }

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

        partial void OnLoaded() {
            var loadAuthor = Author;
            Categories = Tables.Categories.GetByPost(Id);
            if (CurrentUser.isAuth) {
                IsFavorite = Posts.IsFavorite(Id);
            }
        }
    }
}
