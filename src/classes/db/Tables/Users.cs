using System;
using System.Linq;
using System.Collections.Generic;
using ITCommunity.Core;


namespace ITCommunity.DB.Tables {

    public static class Users {

        public static User Add(User user) {
            using (var db = Database.Connect()) {
                db.Users.InsertOnSubmit(user);
                db.SubmitChanges();

                return user;
            }
        }

        public static User Update(User editedUser) {
            using (var db = Database.Connect()) {
                var user = (
                    from usr in db.Users
                    where usr.Id == editedUser.Id
                    select usr
                ).Single();

                user.Password = editedUser.Password;
                user.Role = editedUser.Role;
                user.Email = editedUser.Email;
                user.HeadersCounter = editedUser.HeadersCounter;

                db.SubmitChanges();

                return user;
            }
        }

        /// <summary>
        /// Получаем пользователя из базы по id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Если не находит пользователя с таким id, то позвращает anonymous-а</returns>
        public static User Get(int id) {
            using (var db = Database.Connect()) {
                var user = (
                    from usr in db.Users
                    where usr.Id == id
                    select usr
                ).SingleOrDefault();

                return user ?? User.Anonymous;
            }
        }

        /// <summary>
        /// Получаем пользователя из базы по логину
        /// </summary>
        /// <param name="nick">nick</param>
        /// <returns>Если не находит пользователя с таким ником, то позвращает anonymous-а</returns>
        public static User Get(string nick) {
            using (var db = Database.Connect()) {
                var user = (
                    from usr in db.Users
                    where usr.Nick.ToLower() == nick.ToLower()
                    select usr
                ).SingleOrDefault();

                return user ?? User.Anonymous;
            }
        }

        public static List<User> GetLastRegistered(int count) {
            using (var db = Database.Connect()) {
                var users =
                    from usr in db.Users
                    orderby usr.CreateDate descending
                    select usr;

                return users.Take(count).ToList();
            }
        }

        public static List<User> GetLastRegistered() {
            var count = Config.LastRegisteredUsersCount;

            return AppCache.Get("LastRegisteredUsers", () => GetLastRegistered(count));
        }

        public static Dictionary<User, int> GetActivePosters(int count, int days) {
            var date = DateTime.Now.AddDays(-days);

            using (var db = Database.Connect()) {
                var users = (
                    from usr in db.Users
                    let cnt = (
                        from post in db.Posts
                        where
                            post.AuthorId == usr.Id &&
                            post.CreateDate >= date
                        select post
                    ).Count()
                    orderby cnt descending
                    select new { User = usr, PostsCount = cnt }
                ).Take(count);

                var result = new Dictionary<User, int>();

                foreach (var item in users) {
                    result.Add(item.User, item.PostsCount);
                }

                return result;
            }
        }

        public static Dictionary<User, int> GetActivePosters() {
            int count = Config.ActivePostersCount;
            int days = Config.ActivePostersDays;

            return AppCache.Get("ActivePosters", () => GetActivePosters(count, days));
        }

        public static Dictionary<User, int> GetTopPosters(int count) {
            using (var db = Database.Connect()) {
                var users = (
                    from usr in db.Users
                    let cnt = (
                        from post in db.Posts
                        where post.AuthorId == usr.Id
                        select post
                    ).Count()
                    orderby cnt descending
                    select new { User = usr, PostsCount = cnt }
                ).Take(count);

                var result = new Dictionary<User, int>();

                foreach (var item in users) {
                    result.Add(item.User, item.PostsCount);
                }

                return result;
            }
        }

        public static Dictionary<User, int> GetTopPosters() {
            int count = Config.TopPostersCount;

            return AppCache.Get("TopPosters", () => GetTopPosters(count));
        }

        public static Dictionary<User, int> GetActiveCommentators(int count, int days) {
            var date = DateTime.Now.AddDays(-days);

            using (var db = Database.Connect()) {
                var users = (
                    from usr in db.Users
                    let cnt = (
                        from com in db.Comments
                        where
                            com.AuthorId == usr.Id &&
                            com.CreateDate >= date
                        select com
                    ).Count()
                    orderby cnt descending
                    select new { User = usr, CommentsCount = cnt }
                ).Take(count);

                var result = new Dictionary<User, int>();

                foreach (var item in users) {
                    result.Add(item.User, item.CommentsCount);
                }

                return result;
            }
        }

        public static Dictionary<User, int> GetActiveCommentators() {
            int count = Config.ActiveCommentatorsCount;
            int days = Config.ActiveCommentatorsDays;

            return AppCache.Get("ActiveCommentators", () => GetActiveCommentators(count, days));
        }

        public static Dictionary<User, int> GetTopCommentators(int count) {
            using (var db = Database.Connect()) {
                var users = (
                      from usr in db.Users
                      let cnt = (
                          from com in db.Comments
                          where com.AuthorId == usr.Id
                          select com
                      ).Count()
                      orderby cnt descending
                      select new { User = usr, CommentsCount = cnt }
                    ).Take(count);

                var result = new Dictionary<User, int>();

                foreach (var item in users) {
                    result.Add(item.User, item.CommentsCount);
                }

                return result;
            }
        }

        public static Dictionary<User, int> GetTopCommentators() {
            var count = Config.TopCommentatorsCount;

            return AppCache.Get("TopCommentators", () => GetTopCommentators(count));
        }

        public static List<User> GetPaged(int page, int count, ref int totalCount) {
            using (var db = Database.Connect()) {
                var users =
                    from usr in db.Users
                    orderby usr.Nick ascending
                    select usr;

                return users.Paged(page, count, ref totalCount);
            }
        }

        public static List<User> GetPagedByRole(User.Roles role, int page, int count, ref int totalCount) {
            using (var db = Database.Connect()) {
                var users =
                    from usr in db.Users
                    where usr.Role == role
                    orderby usr.Nick ascending
                    select usr;

                return users.Paged(page, count, ref totalCount);
            }
        }

        public static User GetByEmail(string email) {
            using (var db = Database.Connect()) {
                var user = (
                    from usr in db.Users
                    where usr.Email.ToLower() == email.ToLower()
                    select usr
                ).FirstOrDefault();

                return user ?? User.Anonymous;
            }
        }
    }
}
