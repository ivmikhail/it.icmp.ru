using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace ITCommunity {
    /// <summary>
    /// Данные в RSS активности проекта Redmine
    /// </summary>
    public class RedmineActivityItem {
        private String _author;
        public String Author {
            get {
                return _author;
            }
            set {
                this._author = value;
            }
        }
        private String _title;
        public String Title {
            get {
                return _title;
            }
            set {
                _title = value;
            }
        }
        private String _text;
        public String Text {
            get {
                return _text;
            }
            set {
                _text = value;
            }
        }
        private String _url;
        public String Url {
            get {
                return _url;
            }
            set {
                _url = value;
            }
        }
        public RedmineActivityItem() {
        }
    }
}