using System;
using System.Collections.Generic;
using System.Data;
using ITCommunity.Core;
using ITCommunity.Db;
using System.Linq;

namespace ITCommunity.Db.Tables {

    public static class Headers {

        public const string ACTUALS_KEY = "Headers";

        private static Random _random = new Random();

        public static Header Add(int userId, string text, double showingHours) {
            using (var db = Database.Connect()) {
                var endDate = DateTime.Now.AddHours(showingHours);

                var header = new Header { UserId = userId, Text = text, EndDate = endDate };

                db.Headers.InsertOnSubmit(header);

                return header;
            }
        }

        public static void Delete(int id) {
            using (var db = Database.Connect()) {
                var deletingHeader = (
                    from header in db.Headers
                    where header.Id == id
                    select header
                ).Single();

                db.Headers.DeleteOnSubmit(deletingHeader);

                db.SubmitChanges();
            }
        }

        public static List<Header> GetActuals() {
            using (var db = Database.Connect()) {
                var result =
                    from header in db.Headers
                    where header.EndDate > DateTime.Now
                    select header;

                return result.ToList();
            }
        }

        public static Header GetRandom() {
            var actuals = AppCache.Get(ACTUALS_KEY, () => GetActuals());

            if (actuals.Count == 0) {
                var header = new Header();
                header.Text = string.Format(Config.Get("HeaderDefaultFormat"), CurrentUser.User.Nick);
                return header;
            }

            return actuals[_random.Next(actuals.Count)];
        }
    }
}
