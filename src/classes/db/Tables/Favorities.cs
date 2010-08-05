using System.Linq;
using ITCommunity.Db;
using System.Collections.Generic;
using System;

namespace ITCommunity.Db.Tables {

    public static class Favorities {

       
        public static Favorite Add(Favorite favorite) {
            using (var db = Database.Connect()) {

                db.Favorites.InsertOnSubmit(favorite);
                db.SubmitChanges();


                return favorite;
            }
        }


        public static Favorite Delete(int postid, int userid) {
            using (var db = Database.Connect()) {

                var favorite = (
                    from fav in db.Favorites
                    where fav.PostId == postid && fav.UserId == userid
                    select fav
                ).First();
                db.Favorites.DeleteOnSubmit(favorite);
                db.SubmitChanges();

                return favorite;
            }
        }

    }
}
