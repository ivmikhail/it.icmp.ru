using System;
using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class PostListModel : PaginatedModel {

        public enum SortBy {
            Date,
            Views,
            Comments
        }

        public enum Periods {
            Day = 1,
            Week = 7,
            Month = 30,
            Year = 365,
            All = 0
        }

        protected List<Post> _posts;

        public SortBy Sorting { get; set; }

        public Periods PeriodDays { get; set; }

        public bool IsPerioded {
            get { return Sorting != SortBy.Date; }
        }

        public List<Post> List {
            get {
                if (_posts == null) {
                    Load();
                }
                return _posts;
            }
            set {
                _posts = value;
            }
        }

        public PostListModel(SortBy sortBy, int? page) :
            base(page) {
            Sorting = sortBy;
            PerPage = Config.PostsPerPage;
            PeriodDays = Periods.All;
        }

        public PostListModel(SortBy sortBy, string period, int? page) :
            this(sortBy, page) {
            PeriodDays = ParsePeriod(period);
        }

        public void Load() {
            _posts = GetList();
        }

        protected virtual List<Post> GetList() {
            int period = (int)PeriodDays;

            switch (Sorting) {
                case SortBy.Views:
                    return Posts.GetPagedPopular(Page, PerPage, ref TotalCount, period);
                case SortBy.Comments:
                    return Posts.GetPagedDiscussible(Page, PerPage, ref TotalCount, period);
                default:
                    return Posts.GetPaged(Page, PerPage, ref TotalCount);
            }
        }

        private static Periods ParsePeriod(string period) {
            string periodDays = period ?? "all";
            Periods result;

            if (Enum.TryParse<Periods>(periodDays, true, out result)) {
                return result;
            }

            return Periods.All;
        }
    }
}
