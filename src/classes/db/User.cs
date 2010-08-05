using ITCommunity.Core;

namespace ITCommunity.Db {

    public enum UserRoles {

        /// <summary>
        /// Права роли "poster" + может управлять голосовалками, пользователями, 
        /// может аттачить посты, может редактировать любые посты.
        /// </summary>
        Admin = 1,

        /// <summary>
        /// Права роли "user" + может добавлять/редактировать свои новости
        /// </summary>
        Poster = 2,

        /// <summary>
        /// Простой пользователь, может голосовать, комментировать без капчи, 
        /// доступные закрытые разделы сайтов. Readonly короче.
        /// </summary>
        User = 3,

        /// <summary>
        /// Аккаунт забанен(не может залогиниться)
        /// </summary>
        Banned = 4
    }

    /// <summary>
    /// Пользователь хранящийся в БД
    /// </summary>
    public partial class User {

        private static User _anonymous = new User { Id = -1, Nick = "anonymous" };

        public bool IsAnonymous {
            get {
                return (Id == -1);
            }
        }

        public bool AbleToAddHeaderText {
            get {
                bool result = CanAddHeader;
                result &= (HeadersCounter >= Config.GetInt("HeaderPostsCount"));
                result |= (this.Role == UserRoles.Admin);
                return result;
            }
        }

        public static User Anonymous {
            get {
                return _anonymous;
            }
        }
    }
}
