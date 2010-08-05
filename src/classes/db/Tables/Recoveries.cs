using System;
using System.Linq;
using System.Timers;
using ITCommunity.Core;
using ITCommunity.Db;

namespace ITCommunity.Db.Tables {

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

        public static void Delete(string guid) {
            using (var db = Database.Connect()) {
                var recoveryGuid = new Guid(guid);

                var deletigRecovery = (
                    from recovery in db.Recoveries
                    where recovery.Guid == recoveryGuid
                    select recovery
                ).Single();

                db.Recoveries.DeleteOnSubmit(deletigRecovery);

                db.SubmitChanges();
            }
        }

        public static Recovery Get(string guid) {
            using (var db = Database.Connect()) {
                var guidObj = new Guid(guid);

                var result = (
                    from recovery in db.Recoveries
                    where recovery.Guid == guidObj
                    select recovery
                ).ToList();

                return (result.Count == 1) ? result[0] : null;
            }
        }

        public static void Purge() {
            using (var db = Database.Connect()) {
                var days = Config.GetInt("RecoveryPurgeDays");
                var date = DateTime.Now.AddDays(-days);

                var recoveries =
                    from recovery in db.Recoveries
                    where recovery.CreateDate < date
                    select recovery;

                db.Recoveries.DeleteAllOnSubmit(recoveries);

                db.SubmitChanges();
            }
        }
    }
}
