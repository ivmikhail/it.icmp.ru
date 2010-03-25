using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITCommunity.IndexerLib {
    public class SearchedPost {
        private int _id;
        private String _title;
        private String _description;

        public SearchedPost(int id, String title, String description) {
            this._id = id;
            this._title = title;
            this._description = description;
        }
        public int Id {
            get {
                return _id;
            }
        }
        public String Title {
            get {
                return _title;
            }
        }
        public String Description {
            get {
                return _description;
            }
        }
    }
}
