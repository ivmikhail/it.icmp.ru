using System;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Всякое - разное
/// </summary>
namespace ITCommunity {
	public static class Util {

		public static string HtmlEncode(string value) {
			string result = "";
			if (value != null) {
				result = HttpUtility.HtmlEncode(value);
				result = Regex.Replace(result, "'", "&#39;"); // "&apos;" не работает в IE говорят, поэтому используем "&#39;"
			}
			return result;
		}

		public static string HtmlDecode(string value) {
			string result = "";
			if (value != null) {
				result = HttpUtility.HtmlDecode(value);
				result = Regex.Replace(result, "&#39;", "'"); // "&apos;" не работает в IE говорят, поэтому используем "&#39;"
			}
			return result;
		}
	}
}
