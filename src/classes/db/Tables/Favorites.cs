using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.Db.Tables {

    public static class Favorites {

        public static Favorite Add(Favorite favorite) {
            using (var db = Database.Connect()) {

                db.Favorites.InsertOnSubmit(favorite);
                db.SubmitChanges();

                return favorite;
            }
        }

        public static void Delete(int postId, int userId) {
            using (var db = Database.Connect()) {

                var favorite = (
                    from fav in db.Favorites
                    where 
                        fav.PostId == postId && 
                        fav.UserId == userId
                    select fav
                ).First();

                db.Favorites.DeleteOnSubmit(favorite);
                db.SubmitChanges();
            }
        }

        public static bool IsFavorite(int postId) {
            using (var db = Database.Connect()) {
                var favorite =
                    from fav in db.Favorites
                    where
                        fav.PostId == postId &&
                        fav.UserId == CurrentUser.User.Id
                    select fav;

                return favorite.Any();
            }
        }
    }
}
