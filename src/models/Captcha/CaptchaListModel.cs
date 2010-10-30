using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class CaptchaListModel : PaginatedModel {

        public List<Captcha> List {
            get;
            private set;
        }

        public CaptchaListModel(int? page)
            : base(page) {
            PerPage = Config.CaptchasPerPage;
            List = GetCaptchas();
        }

        protected virtual List<Captcha> GetCaptchas() {
            return Captchas.GetPaged(Page, PerPage, ref TotalCount);
        }
    }
}
