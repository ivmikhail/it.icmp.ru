using System;
using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.Db.Tables {

    public static class Captchas {

        public const string CACHE_KEY = "Captchas";

        public static List<Captcha> All {
            get { return AppCache.Get(CACHE_KEY, GetAll); }
        }

        public static Captcha Add(Captcha captcha) {
            using (var db = Database.Connect()) {
                db.Captchas.InsertOnSubmit(captcha);
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
                return captcha;
            }
        }

        public static CaptchaAnswer AddAnswer(CaptchaAnswer answer) {
            using (var db = Database.Connect()) {
                db.CaptchaAnswers.InsertOnSubmit(answer);
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
                return answer;
            }
        }

        public static void Delete(int id) {
            using (var db = Database.Connect()) {
                var captcha = (
                    from cap in db.Captchas
                    where cap.Id == id
                    select cap
                ).SingleOrDefault();

                db.Captchas.DeleteOnSubmit(captcha);
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
            }
        }

        public static void Update(Captcha editedCaptcha) {
            using (var db = Database.Connect()) {
                var captcha = (
                    from cap in db.Captchas
                    where cap.Id == editedCaptcha.Id
                    select cap
                ).SingleOrDefault();

                captcha.Question = editedCaptcha.Question;
                captcha.CaptchaAnswers = editedCaptcha.CaptchaAnswers;

                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
            }
        }

        public static List<Captcha> GetAll() {
            using (var db = Database.Connect()) {
                return db.Captchas.ToList();
            }
        }

        public static Captcha Get(int id) {
            using (var db = Database.Connect()) {
                var captcha = (
                    from cap in db.Captchas
                    where cap.Id == id
                    select cap
                ).SingleOrDefault();

                return captcha;
            }
        }

        public static Captcha GetRandom() {
            return All.Random();
        }

        public static void DeleteAnswer(int id) {
            using (var db = Database.Connect()) {
                var answer = (
                    from ans in db.CaptchaAnswers
                    where ans.Id == id
                    select ans
                ).SingleOrDefault();

                db.CaptchaAnswers.DeleteOnSubmit(answer);
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
            }
        }

        public static bool IsRightAnswer(int answerId) {
            using (var db = Database.Connect()) {
                var answer = (
                    from ans in db.CaptchaAnswers
                    where ans.Id == answerId
                    select ans
                ).SingleOrDefault();

                return (answer != null) ? answer.IsRight : false;
            }
        }

        public static List<Captcha> GetPaged(int page, int count, ref int totalCount) {
            using (var db = Database.Connect()) {
                var captchas =
                    from cap in db.Captchas
                    orderby cap.Id descending
                    select cap;

                return captchas.Paged(page, count, ref totalCount);
            }
        }

        public static int GetIdByAnswer(int answerId) {
            using (var db = Database.Connect()) {
                var answer = (
                    from ans in db.CaptchaAnswers
                    where ans.Id == answerId
                    select ans
                ).SingleOrDefault();

                return answer.Captcha.Id;
            }
        }
    }
}
