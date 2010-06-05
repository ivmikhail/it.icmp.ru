using System;
using System.Linq;
using System.Timers;
using ITCommunity.Core;
using ITCommunity.Db.Models;

namespace ITCommunity.Db.Tables {

    public static class Recoveries {

        /// <summary>
        /// ƒобавл€ем в базу id пользовател€ дл€ восстановлени€ парол€
        /// </summary>
        /// <param name="userId">id пользовател€</param>
        /// <returns>¬озвращает добавленную запись со сгенерированным Guid</returns>
        public static Recovery Add(int userId) {
            using (var db = Database.Connect()) {
                var result = db.RecoveriesAdd(userId);

                return result.Single();
            }
        }

        public static void Delete(string guid) {
            using (var db = Database.Connect()) {
                db.RecoveriesDelete(guid);
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
                int days = Config.GetInt("RecoveryPurgeDays");
                db.RecoveriesPurge(days);
            }
        }
    }
}
