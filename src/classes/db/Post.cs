using System.Collections.Generic;
using System.Web;

using ITCommunity.Core;
using ITCommunity.Utils;
using ITCommunity.DB.Tables;


namespace ITCommunity.DB {

    /// <summary>
    /// Пост хранящийся в БД
    /// </summary>
    public partial class Post {

        public enum EntityTypes {
            Post = 0,
            Poll = 1
        }

        public List<Category> Categories { get; set; }

        public Rating Rating { get; set; }

        public bool IsFavorite { get; private set; }

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
            // будет думать что Author измененился и поэтому не сможет увеличить 
            // количество просмотров. Поэтому не использую это:
            // Author = Users.Get(AuthorId); 
            var loadAuthor = Author;
            
            // а вот тут уже другое, т.к. Categories - не сгенерированное свойство
            Categories = Tables.Categories.GetByPost(Id);

            if (CurrentUser.IsAuth) {
                IsFavorite = Tables.Favorites.IsFavorite(Id);
            }

            Rating = Ratings.Get(Id, Rating.EntityTypes.Post) ?? new Rating { EntityId = Id, EntityType = DB.Rating.EntityTypes.Post };

            if (EntityType == null) {
                EntityType = EntityTypes.Post;
            }
        }
    }
}
