using System.Linq;
using ITCommunity.Db.Models;

namespace ITCommunity.Db.Tables {

    public static class Users {

        public static User Add(User user) {
            using (var db = Database.Connect()) {
                var result = db.UsersAdd(
                    user.Nick,
                    user.Password,
                    (byte)user.Role,
                    user.Email
                );

                return result.Single();
            }
        }

        public static User Update(User user) {
            using (var db = Database.Connect()) {
                var userRole = (byte)user.Role;
                var userCanAddHeader = (byte)(user.CanAddHeader ? 1 : 0);

                var result = db.UsersUpdate(
                    user.Id,
                    user.Password,
                    userRole,
                    user.Email,
                    userCanAddHeader,
                    user.HeaderCounter
                );

                return result.Single();
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

                return (result.Count == 1) ? result[0] : User.Anonymous;
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

                return (result.Count == 1) ? result[0] : User.Anonymous;
            }
        }
    }
}
