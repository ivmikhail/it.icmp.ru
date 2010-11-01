using System;
using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class HeaderListModel : PaginatedModel {

        public List<Header> List {
            get;
            private set;
        }

        public HeaderListModel(int? page)
            : base(page) {
            PerPage = Config.HeadersPerPage;
            List = GetHeaders();
        }

        protected virtual List<Header> GetHeaders() {
            return Headers.GetPaged(Page, PerPage, ref TotalCount);
        }
    }
}
