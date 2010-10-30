using System;
using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.DB.Tables {

    public static class Recoveries {

        /// <summary>
        /// ƒобавл€ем в базу id пользовател€ дл€ восстановлени€ парол€
        /// </summary>
        /// <param name="userId">id пользовател€</param>
        /// <returns>¬озвращает добавленную запись со сгенерированным Guid</returns>
        public static Recovery Add(int userId) {
            using (var db = Database.Connect()) {
                var recovery = new Recovery { UserId = userId };

                db.Recoveries.InsertOnSubmit(recovery);
                db.SubmitChanges();

                return recovery;
            }
        }

        public static void Delete(Guid guid) {
            using (var db = Database.Connect()) {
                var deletigRecovery = (
                    from rec in db.Recoveries
                    where rec.Guid == guid
                    select rec
                ).Single();

                db.Recoveries.DeleteOnSubmit(deletigRecovery);
                db.SubmitChanges();
            }
        }

        public static Recovery Get(string guid) {
            using (var db = Database.Connect()) {
                var recoveryGuid = new Guid(guid);

                var result = (
                    from rec in db.Recoveries
                    where rec.Guid == recoveryGuid
                    select rec
                ).SingleOrDefault();

                return result;
            }
        }

        public static void Purge() {
            using (var db = Database.Connect()) {
                var days = Config.RecoveryPurgeDays;
                var date = DateTime.Now.AddDays(-days);

                var recoveries =
                    from rec in db.Recoveries
                    where rec.CreateDate < date
                    select rec;

                db.Recoveries.DeleteAllOnSubmit(recoveries);
                db.SubmitChanges();
            }
        }
    }
}
