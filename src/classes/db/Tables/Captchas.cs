using System.Linq;
using ITCommunity.Db;
using System.Collections.Generic;
using System;

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

                if (result != null) {
                    result.CaptchaAnswers.Load();
                    return result;
                }

                return null;
            }
        }


        public static Captcha Get(int id) {
            using (var db = Database.Connect()) {

                var result = (
                    from question in db.Captchas
                    where question.Id == id
                    select question).ToList();

                if (result.Count == 1) {
                    result[0].CaptchaAnswers.Load();
                    return result[0];
                }

                return null;
            }
        }


    }
}
