using System;
using System.Linq;

namespace ITCommunity.Db.Tables {

    public static class Captchas {

        private static Random _rand = new Random();
        
        public static Captcha GetRandom() {
            using (var db = Database.Connect()) {
                var captchas =
                    from captcha in db.Captchas
                    select captcha;

                var count = captchas.Count();

                var result = captchas.Skip(_rand.Next(count)).First();

                return result;
            }
        }

        public static Captcha Get(int id) {
            using (var db = Database.Connect()) {

                var result = (
                    from captcha in db.Captchas
                    where captcha.Id == id
                    select captcha
                ).First();

                return result;
            }
        }
    }
}
