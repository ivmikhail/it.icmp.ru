using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ITCommunity.Db;
using System.Web.Mvc;

namespace ITCommunity.Utils {

    public static class ExtensionHtmlHelper {

        public static string AddPost(this UrlHelper url) {
            return "post/add";
        }

        public static string ViewPost(this UrlHelper url, int postId) {
            return "post/view" + postId;
        }

        public static string EditPost(this UrlHelper url, int postId) {
            return "post/edit" + postId;
        }

        public static string DeletePost(this UrlHelper url, int postId) {
            return "post/delete" + postId;
        }

    }
}