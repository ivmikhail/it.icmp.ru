using System;
using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;

namespace ITCommunity.Db.Tables {

    public static class Headers {

        public const string CACHE_KEY = "ActualHeaders";

        public static List<Header> Actuals {
            get { return AppCache.Get(CACHE_KEY, GetActuals); }
        }

        public static Header Add(Header header) {
            using (var db = Database.Connect()) {

                db.Headers.InsertOnSubmit(header);
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);

                return header;
            }
        }

        public static void Delete(int id) {
            using (var db = Database.Connect()) {
                var header = (
                    from hdr in db.Headers
                    where hdr.Id == id
                    select hdr
                ).Single();

                db.Headers.DeleteOnSubmit(header);
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
            }
        }

        public static void Stop(int id) {
            using (var db = Database.Connect()) {
                var header = (
                    from hdr in db.Headers
                    where hdr.Id == id
                    select hdr
                ).Single();

                header.EndDate = DateTime.Now;
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
            }
        }

        public static void Show(int id) {
            using (var db = Database.Connect()) {
                var header = (
                    from hdr in db.Headers
                    where hdr.Id == id
                    select hdr
                ).Single();

                header.EndDate = DateTime.Now.AddHours(Config.GetDouble("HeaderShowingHours"));
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
            }
        }

        public static List<Header> GetActuals() {
            using (var db = Database.Connect()) {
                var headers =
                    from hea in db.Headers
                    where hea.EndDate > DateTime.Now
                    select hea;

                return headers.ToList();
            }
        }

        public static Header GetRandom() {
            if (Actuals.Count == 0) {
                var header = new Header();
                header.Text = string.Format(Config.Get("HeaderDefaultFormat"), CurrentUser.User.Nick);
                return header;
            }

            return Actuals.Random();
        }

        public static List<Header> GetPaged(int page, int count, ref int totalCount) {
            using (var db = Database.Connect()) {
                var headers =
                    from hdr in db.Headers
                    orderby hdr.EndDate descending
                    select hdr;

                return headers.Paged(page, count, ref totalCount);
            }
        }
    }
}
