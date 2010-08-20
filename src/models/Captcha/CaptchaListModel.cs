using System;
using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class CaptchaListModel : PaginatedModel {

        public List<Captcha> List {
            get;
            private set;
        }

        public CaptchaListModel(int? page)
            : base(page) {
            PerPage = Config.GetInt("CaptchasPerPage");
            List = GetCaptchas();
        }

        protected virtual List<Captcha> GetCaptchas() {
            return Captchas.GetPaged(Page, PerPage, ref TotalCount);
        }
    }
}
