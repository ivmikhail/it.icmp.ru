using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ITCommunity.DB;

namespace ITCommunity.DB.Tables
{
    public class Rfcs
    {
        public static List<Rfc> getByNum(string number) {
       		if (number.Length > 4) {
				throw new ArgumentOutOfRangeException(number, "Допустимое кол-во символов от 0 до 4");
			}
			string num = number;
			while (num.Length != 4) {
				num = "0" + num;
			}
            using (var db = Database.Connect()) {
                var finded = 
                    from rfcs in db.Rfcs
                    where rfcs.Number == num
                    select rfcs;

                return finded.ToList();
            }
        }
        public static List<Rfc> getByTitle(string title) {
            using (var db = Database.Connect()) {
                var finded =
                    from rfcs in db.Rfcs
                    where rfcs.Title.Contains(title)
                    select rfcs;

                return finded.ToList();
            }
        }
    }
}