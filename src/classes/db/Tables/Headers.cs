using System;
using System.Collections.Generic;
using System.Data;
using ITCommunity.Core;
using ITCommunity.Db;
using System.Linq;
using ITCommunity.Db.Models;

namespace ITCommunity.Db.Tables {

    public static class Headers {

        public const string ACTUALS_KEY = "Headers";

        private static Random _random = new Random();

        public static Header Add(int userId, string text) {
            using (var db = Database.Connect()) {
                var showingHours = Config.GetDouble("HeaderTextShowingHours");
                var endDate = DateTime.Now.AddHours(showingHours);

                var result = db.HeadersAdd(
                    userId,
                    text,
                    endDate
                ).Single();

                return result;
            }
        }

        public static void Delete(int id) {
            using (var db = Database.Connect()) {
                db.HeadersDelete(id);
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
