using System;
using System.Web.Mvc;


namespace ITCommunity.Utils {

    public static class HtmlExtension {

        public static string FileSize(this HtmlHelper html, long size) {
            if (size > 1048576) {
                return Math.Round(size / 1048576f, 2) + " MB";
            }
            if (size > 1024) {
                return Math.Round(size / 1024f, 2) + " KB";
            }
            return size.ToString() + " B";
        }

        public static string Date(this HtmlHelper html, DateTime date) {
            return date.ToString("dd MMMM yyyy, HH:mm");
        }

        public static string Icon(this HtmlHelper html, string extension) {
            if (extension == null) {
                return "folder.gif";
            }

            if (extension.Equals(".exe", StringComparison.CurrentCultureIgnoreCase)) {
                return "exe.ico";
            }

            return "any.ico";
        }
    }
}