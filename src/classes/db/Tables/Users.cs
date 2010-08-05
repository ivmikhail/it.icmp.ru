using System.Linq;
using ITCommunity.Db;

namespace ITCommunity.Db.Tables {

    public static class Users {

        public static User Add(User user) {
            using (var db = Database.Connect()) {
                db.Users.InsertOnSubmit(user);

                db.SubmitChanges();

                return user;
            }
        }

        public static User Update(User user) {
            using (var db = Database.Connect()) {
                var dbUser = (
                    from u in db.Users
                    where u.Id == user.Id
                    select u
                ).Single();

                dbUser.Password = user.Password;
                dbUser.Role = user.Role;
                dbUser.Email = user.Email;
                dbUser.CanAddHeader = user.CanAddHeader;
                dbUser.HeadersCounter = user.HeadersCounter;

                db.SubmitChanges();

                return dbUser;
            }
        }

        /// <summary>
        /// Получаем пользователя из базы по id
        /// </summary>
        /// <param name="id">id</param>
        public static User Get(int id) {
            using (var db = Database.Connect()) {
                var result = (
                    from user in db.Users
                    where user.Id == id
                    select user
                ).ToList();

                if (result.Count == 1) {
                    return result[0];
                }
                return User.Anonymous;
            }
        }

        /// <summary>
        /// Получаем пользователя из базы по логину
        /// </summary>
        /// <param name="nick">nick</param>
        public static User Get(string nick) {
            using (var db = Database.Connect()) {
                var result = (
                    from user in db.Users
                    where user.Nick.ToLower() == nick.ToLower()
                    select user
                ).ToList();

                if (result.Count == 1) {
                    return result[0];
                }
                return User.Anonymous;
            }
        }
    }
}
