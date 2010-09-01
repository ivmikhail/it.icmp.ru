using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using System;


namespace ITCommunity.Controllers {

    public class RatingController : BaseController {

        public ActionResult Up(RatingLog log) {
            return Log(log, true);
        }

        public ActionResult Down(RatingLog log) {
            return Log(log, false);
        }

        private ActionResult Log(RatingLog log, bool isGoodRate) {
            if (Request.IsAjaxRequest() == false) {
                return AccessDenied();
            }

            if (CurrentUser.IsAuth == false) {
                return PartialView("Login");
            }

            var passedDays = Config.GetDouble("PostRatingRegistrationPassedDays");
            if (CurrentUser.User.CreateDate.AddDays(passedDays) < DateTime.Now) {
                return PartialView("RecentRegistration");
            }

            var value = Rating.GetValue(log.EntityType);

            log.UserId = CurrentUser.User.Id;
            log.Value = isGoodRate ? value : -value;

            var rating = Ratings.Log(log);
            rating.IsRated = true;

            return PartialView("Rated", rating);
        }
    }
}
