using ITCommunity.Util;
using ITCommunity.Db;
using ITCommunity.Db.Tables;
using System.Web;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ITCommunity.Models {

    public class PaginatedModel {

        protected int ItemsCount;

        public int Page { get; set; }

        public int PerPage { get; set; }

        public int PagesCountOnPage { get; set; }

        public bool IsStart {
            get {
                return Page < PagesCountOnPage;
            }
        }

        public bool IsEnd {
            get {
                return PagesCount - Page <= PagesCountOnPage;
            }
        }

        public int StartPage {
            get {
                if (IsStart) {
                    return 1;
                }
                return ((Page - 1) / (PagesCountOnPage - 1)) * (PagesCountOnPage - 1) + 1;
            }
        }

        public int EndPage {
            get {
                if (IsEnd) {
                    return PagesCount;
                }
                return StartPage + PagesCountOnPage - 1;
            }
        }

        public int PagesCount {
            get {
                return ItemsCount / PerPage;
            }
        }

        public PaginatedModel(int? page) {
            Page = (page == null) ? 1 : page.Value;
            PerPage = 10;
            PagesCountOnPage = 11;
        }
    }
}