

namespace ITCommunity.Models {

    public class PaginatedModel {

        public int TotalCount;

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
                return (Page - 1) / (PagesCountOnPage - 1) == PagesCount / (PagesCountOnPage - 1  );
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
                return TotalCount / PerPage;
            }
        }

        public PaginatedModel(int? page) {
            Page = (page == null) ? 1 : page.Value;
            PerPage = 10;
            PagesCountOnPage = 11;
        }
    }
}