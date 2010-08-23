using System;
using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.DB.Tables {

    public static class Ratings {

        public static Rating Log(RatingLog log) {
            using (var db = Database.Connect()) {
                var rating = (
                    from rat in db.Ratings
                    where
                        rat.EntityId == log.EntityId &&
                        rat.EntityType == log.EntityType
                    select rat
                ).SingleOrDefault();

                if (rating == null) {
                    rating = new Rating {
                        EntityId = log.EntityId,
                        EntityType = log.EntityType,
                        Value = log.Value
                    };

                    db.Ratings.InsertOnSubmit(rating);
                } else {
                    rating.Value += log.Value;
                }

                db.RatingLogs.InsertOnSubmit(log);
                db.SubmitChanges();

                return rating;
            }
        }

        public static Rating Get(int entityId, Rating.EntityTypes entityType) {
            using (var db = Database.Connect()) {
                var rating = (
                   from rat in db.Ratings
                   where
                       rat.EntityId == entityId &&
                       rat.EntityType == entityType
                   select rat
               ).SingleOrDefault();

                return rating;
            }
        }

        public static bool IsRated(int userId, int entityId, Rating.EntityTypes entityType) {
            using (var db = Database.Connect()) {
                var isRated = (
                   from log in db.RatingLogs
                   where
                       log.UserId == userId &&
                       log.EntityId == entityId &&
                       log.EntityType == entityType
                   select log
               ).Any();

                return isRated;
            }
        }
    }
}
