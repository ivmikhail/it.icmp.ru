using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.DB.Tables {

    public static class Rsses {

        public const string CACHE_KEY = "RSS";

        public static List<Rss> All {
            get {
                return AppCache.Get(CACHE_KEY, GetAll);
            }
        }

        public static Rss Add(Rss rss) {
            using (var db = Database.Connect()) {
                db.Rsses.InsertOnSubmit(rss);
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
                return rss;
            }
        }

        public static void Delete(int id) {
            using (var db = Database.Connect()) {
                var dbRss = (
                    from rss in db.Rsses
                    where rss.Id == id
                    select rss
                ).SingleOrDefault();

                if (dbRss != null) {
                    AppCache.Remove(CACHE_KEY);

                    db.Rsses.DeleteOnSubmit(dbRss);
                    db.SubmitChanges();
                }
            }
        }

        public static void Update(Rss editedRss) {
            using (var db = Database.Connect()) {
                var dbRss = (
                    from rss in db.Rsses
                    where rss.Id == editedRss.Id
                    select rss
                ).Single();

                dbRss.Title = editedRss.Title;
                dbRss.Uri = editedRss.Uri;

                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
            }
        }

        public static Rss Get(int id) {
            var dbRss = (
                from rss in All
                where rss.Id == id
                select rss
            ).SingleOrDefault();

            return dbRss;
        }

        public static List<Rss> GetAll() {
            using (var db = Database.Connect()) {
                return db.Rsses.ToList();
            }
        }
    }
}
