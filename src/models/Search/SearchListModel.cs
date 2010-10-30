using System.Web.UI.MobileControls;
using ITCommunity.Core;
using System.Collections.Generic;
using ITCommunity.DB;
using System;
namespace ITCommunity.Models {
    public class SearchListModel : PaginatedModel {
        public string Query {
            get;
            private set;
        }
        public List<Post> Posts {
            get;
            private set;
        }
        public SearchListModel(string query, List<Post> posts, int? page, int totalPages) : base(page, totalPages) {
            this.Query = query;
            this.Posts = posts;

            
        }
    }
}