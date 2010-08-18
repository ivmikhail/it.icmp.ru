using ITCommunity.Core;
using ITCommunity.Db.Tables;


namespace ITCommunity.Db {

    /// <summary>
    /// Пользователь хранящийся в БД
    /// </summary>
    public partial class User {

        public enum Roles {

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

        private static User _anonymous = new User { Id = -1, Nick = "anonymous" };

        public bool IsAnonymous {
            get { return (Id == -1); }
        }

        public static User Anonymous {
            get { return _anonymous; }
        }

        public int UnreadMessagesCount {
            get { return Messages.GetUnreadsCount(Id); }
        }
    }
}
