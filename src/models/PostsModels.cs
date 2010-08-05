using ITCommunity.Util;
using ITCommunity.Db;
using ITCommunity.Db.Tables;
using System.Web;
using System.Collections.Generic;
using ITCommunity.Core;
using System;

namespace ITCommunity.Models {

    public class PostsModel : PaginatedModel {

        protected List<Post> _posts;

        public enum SortBy {
            Date,
            Views,
            Comments
        }

        public enum Period {
            Day = 1,
            Week = 7,
            Month = 30,
            Year = 365,
            All = 0
        }

        public SortBy Sorting { get; set; }

        public Period PeriodDays { get; set; }

        public bool IsPerioded {
            get {
                return Sorting != SortBy.Date;
            }
        }

        public List<Post> List {
            get {
                if (_posts == null) {
                    _posts = LoadList();
                }
                return _posts;
            }
        }

        public PostsModel(SortBy sortBy, int? page) :
            base(page) {
            Sorting = sortBy;
            PerPage = Config.GetInt("PostsPerPage");
            PeriodDays = Period.All;
        }

        public PostsModel(SortBy sortBy, string period, int? page) :
            this(sortBy, page) {
            PeriodDays = ParsePeriod(period);
        }

        protected virtual List<Post> LoadList() {
            int period = (int)PeriodDays;

            switch (Sorting) {
                case SortBy.Views:
                    return Posts.GetPagedPopular(Page, PerPage, ref ItemsCount, period);
                case SortBy.Comments:
                    return Posts.GetPagedDiscussible(Page, PerPage, ref ItemsCount, period);
                default:
                    return Posts.GetPaged(Page, PerPage, ref ItemsCount, period);
            }
        }

        private static Period ParsePeriod(string period) {
            string periodDays = period ?? "all";
            Period result;

            if (Enum.TryParse<Period>(periodDays, true, out result)) {
                return result;
            }

            return Period.All;
        }
    }


    public class CategoryPostsModel : PostsModel {

        public Category Category { get; set; }

        protected override List<Post> LoadList() {
            return Posts.GetPagedByCategory(Page, PerPage, Category.Id, ref ItemsCount);
        }

        public CategoryPostsModel(int id, int? page) :
            base(SortBy.Date, page) {
            Category = Categories.Get(id);
        }
    }


    public class FavoritePostsModel : PostsModel {

        protected override List<Post> LoadList() {
            return Posts.GetPagedFavorite(Page, PerPage, ref ItemsCount);
        }

        public FavoritePostsModel(int? page) :
            base(SortBy.Date, page) {
        }
    }

}
