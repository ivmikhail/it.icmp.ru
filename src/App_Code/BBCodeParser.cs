using System;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ITCommunity {
	public class BBCodeParser {
		#region Helper Classes
		interface IHtmlFormatter {
			string Format(string data);
		}

		protected class RegexFormatter : IHtmlFormatter {
			private string _replace;
			private Regex _regex;

			public RegexFormatter(string pattern, string replace)
				: this(pattern, replace, true) {

			}

			public RegexFormatter(string pattern, string replace, bool ignoreCase) {
				RegexOptions options = RegexOptions.Compiled;

				if (ignoreCase) {
					options |= RegexOptions.IgnoreCase;
				}

				_replace = replace;
				_regex = new Regex(pattern, options);
			}

			public string Format(string data) {
				return _regex.Replace(data, _replace);
			}
		}

		protected class SearchReplaceFormatter : IHtmlFormatter {
			private string _pattern;
			private string _replace;

			public SearchReplaceFormatter(string pattern, string replace) {
				_pattern = pattern;
				_replace = replace;
			}

			public string Format(string data) {
				return data.Replace(_pattern, _replace);
			}
		}
		private static string abundaEvaluator(Match m) {
			int videoId;
			if (!Int32.TryParse(m.Groups[1].Value, out videoId)) {
				return "";
			}
			String hash = Hash.CalculateMD5(m.Groups[1].Value).Substring(11, 20);
			return "<embed width='452' height='361' quality='high' "
				+ "bgcolor='#000000' name='main' id='main' allowfullscreen='true' "
				+ "allowscriptaccess='always' src='http://tube.abunda.ru/player/vPlayer.swf"
				+ "?f=http://tube.abunda.ru/player/vConfig_embed.php?vkey="
				+ hash + "' "
				+ "type='application/x-shockwave-flash' />";
		}
		protected class RegexFuncFormatter : IHtmlFormatter {
			private Regex _regex;
			MatchEvaluator _function;
			public RegexFuncFormatter(string pattern, MatchEvaluator evaluator) {
				this._regex = new Regex(pattern);
				this._function = evaluator;
			}
			public string Format(string data) {
				return this._regex.Replace(data, _function);
			}
		}
		#endregion

		#region BBCode
		static List<IHtmlFormatter> _formatters;

		static BBCodeParser() {
			_formatters = new List<IHtmlFormatter>();

			_formatters.Add(new SearchReplaceFormatter("\r", ""));

			// убираем все whitespases внутри тегов ббкода
			_formatters.Add(new RegexFormatter(@"\[\s*(.*?)\s*\]", "[$1]"));

			// убираем переходы на след. строку в таблице
			_formatters.Add(new RegexFormatter(@"(\[table.*?\])\n+", "$1"));
			_formatters.Add(new RegexFormatter(@"\n*(\[/?tr.*?\])\n+", "$1"));
			_formatters.Add(new RegexFormatter(@"\n*(\[/?td.*?\])\n+", "$1"));

			// убираем переходы на след. строку в списке
			_formatters.Add(new RegexFormatter(@"(\[list.*?\])\s+", "$1"));
			_formatters.Add(new RegexFormatter(@"\s*(\[\*\])\s*", "$1"));
			_formatters.Add(new RegexFormatter(@"\s+(\[/list\])", "$1"));

			_formatters.Add(new RegexFormatter("\n", "<br />"));

			_formatters.Add(new RegexFormatter(@"\[b\]((.|\n)*?)\[/b\]", "<b>$1</b>"));
			_formatters.Add(new RegexFormatter(@"\[i\]((.|\n)*?)\[/i\]", "<i>$1</i>"));
			_formatters.Add(new RegexFormatter(@"\[u\]((.|\n)*?)\[/u\]", "<u>$1</u>"));
			_formatters.Add(new RegexFormatter(@"\[s\]((.|\n)*?)\[/s\]", "<strike>$1</strike>"));

			_formatters.Add(new RegexFormatter(@"\[left\]((.|\n)*?)\[/left]", "<div style=\"text-align:left\">$1</div>"));
			_formatters.Add(new RegexFormatter(@"\[center\]((.|\n)*?)\[/center]", "<div style=\"text-align:center\">$1</div>"));
			_formatters.Add(new RegexFormatter(@"\[right\]((.|\n)*?)\[/right]", "<div style=\"text-align:right\">$1</div>"));

			_formatters.Add(new RegexFormatter(@"\[quote\]((.|\n)*?)\[/quote]", "<blockquote>$1</blockquote>"));
			_formatters.Add(new RegexFormatter(@"\[code\]((.|\n)*?)\[/code\]", "<pre><code>$1</code></pre>"));
			_formatters.Add(new RegexFormatter(@"\[code=(.*?)\]((.|\n)*?)\[/code\]", "<pre><code class=\"$1\">$2</code></pre>"));

			_formatters.Add(new RegexFormatter(@"\[url\]www\.(.*?)\[/url\]", "<a class=\"bbcode-link\" href=\"http://www.$1\" target=\"_blank\" title=\"$1\">$1</a>"));
			_formatters.Add(new RegexFormatter(@"\[url\]((.|\n)*?)\[/url\]", "<a class=\"bbcode-link\" href=\"$1\" target=\"_blank\" title=\"$1\">$1</a>"));
			_formatters.Add(new RegexFormatter(@"\[url=""((.|\n)*?)""\]((.|\n)*?)\[/url\]", "<a class=\"bbcode-link\" href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>"));
			_formatters.Add(new RegexFormatter(@"\[url=((.|\n)*?)\]((.|\n)*?)\[/url\]", "<a class=\"bbcode-link\" href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>"));
			_formatters.Add(new RegexFormatter(@"\[link\]((.|\n)*?)\[/link\]", "<a class=\"bbcode-link\" href=\"$1\" target=\"_blank\" title=\"$1\">$1</a>"));
			_formatters.Add(new RegexFormatter(@"\[link=((.|\n)*?)\]((.|\n)*?)\[/link\]", "<a class=\"bbcode-link\" href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>"));

			_formatters.Add(new RegexFormatter(@"\[popup=((.|\n)*?)\]((.|\n)*?)\[/popup\]", "<a href=\"javascript:popup('$1')\" >$3</a>"));


			_formatters.Add(new RegexFormatter(@"\[img\]((.|\n)*?)\[/img\]", "<img src=\"$1\" border=\"0\" alt=\"\" class=\"bbcode-img\" />"));
			_formatters.Add(new RegexFormatter(@"\[img align=((.|\n)*?)\]((.|\n)*?)\[/img\]", "<img src=\"$3\" border=\"0\" align=\"$1\" alt=\"\" class=\"bbcode-img align-$1\" />"));
			_formatters.Add(new RegexFormatter(@"\[img=((.|\n)*?)x((.|\n)*?)\]((.|\n)*?)\[/img\]", "<img width=\"$1\" height=\"$3\" src=\"$5\" border=\"0\" alt=\"\" class=\"bbcode-img\" />"));

			//_formatters.Add(new RegexFormatter(@"\[color=((.|\n)*?)\]((.|\n)*?)\[/color\]", "<span style=\"color=$1;\">$3</span>"));

			_formatters.Add(new RegexFormatter(@"\[hr\]", "<hr />"));

			_formatters.Add(new RegexFormatter(@"\[email\]((.|\n)*?)\[/email\]", "<a href=\"mailto:$1\">$1</a>"));

			_formatters.Add(new RegexFormatter(@"\[size=((.|\n)*?)\]((.|\n)*?)\[/size\]", "<span style=\"font-size:$1\">$3</span>"));
			//_formatters.Add(new RegexFormatter(@"\[font=((.|\n)*?)\]((.|\n)*?)\[/font\]", "<span style=\"font-family:$1;\">$3</span>"));
			//_formatters.Add(new RegexFormatter(@"\[align=((.|\n)*?)\]((.|\n)*?)\[/align\]", "<span style=\"text-align:$1;\">$3</span>"));
			_formatters.Add(new RegexFormatter(@"\[float=((.|\n)*?)\]((.|\n)*?)\[/float\]", "<div style=\"float:$1;margin: 10px;\">$3</div>"));


			string sListFormat = "<ol class=\"bbcode-list\" style=\"list-style:{0};\">$1</ol>";

			_formatters.Add(new RegexFormatter(@"\[\*\]\s*([^\[]*)", "<li>$1</li>"));
			_formatters.Add(new RegexFormatter(@"\[list\]((.|\n)*?)\[/list\]", "<ul class=\"bbcode-list\">$1</ul>"));
			_formatters.Add(new RegexFormatter(@"\[list=1\]((.|\n)*?)\[/list\]", string.Format(sListFormat, "decimal"), false));
			_formatters.Add(new RegexFormatter(@"\[list=i\]((.|\n)*?)\[/list\]", string.Format(sListFormat, "lower-roman"), false));
			_formatters.Add(new RegexFormatter(@"\[list=I\]((.|\n)*?)\[/list\]", string.Format(sListFormat, "upper-roman"), false));
			_formatters.Add(new RegexFormatter(@"\[list=a\]((.|\n)*?)\[/list\]", string.Format(sListFormat, "lower-alpha"), false));
			_formatters.Add(new RegexFormatter(@"\[list=A\]((.|\n)*?)\[/list\]", string.Format(sListFormat, "upper-alpha"), false));

			// Video tag formatter test
			_formatters.Add(new RegexFormatter(@"\[video]http://play\.ykt\.ru/video/(\d+)/.+?\s*\[/video]",
				@"<embed src='http://play.ykt.ru/player.swf' width='640' height='480' allowscriptaccess='always' allowfullscreen='true' " +
				@"flashvars='width=640&height=480&file=http://play.ykt.ru/flvideo/$1" +
				@".flv&image=http://play.ykt.ru/thumb/$1.jpg" +
				@"&displayheight=480&link=http://play.ykt.ru/video/$1&searchbar=false&linkfromdisplay=true' " +
				@"pluginspage='http://www.macromedia.com/go/getflashplayer' type='application/x-shockwave-flash' />", true));
			// Для Abunda надо высчитывать хеш MD5, к счастью дураки соль не использовали.
			_formatters.Add(new RegexFuncFormatter(@"\[video]http://tube\.abunda\.ru/video/(\d+)/.+?\[/video]"
				+ "", abundaEvaluator));

			_formatters.Add(new RegexFormatter(@"\[table\]((.|\n)*?)\[/table\]", "<table class='user-table' cellpadding='0' cellspacing='0' width='100%'>$1</table>"));
			_formatters.Add(new RegexFormatter(@"\[table=([0-9]*%)\]((.|\n)*?)\[/table\]", "<table class='user-table' cellpadding='0' cellspacing='0' width='$1'>$2</table>"));
			_formatters.Add(new RegexFormatter(@"\[tr\]((.|\n)*?)\[/tr\]", "<tr>$1</tr>"));
			_formatters.Add(new RegexFormatter(@"\[td\]((.|\n)*?)\[/td\]", "<td>$1</td>"));
			_formatters.Add(new RegexFormatter(@"\[td=([0-9]*)\]((.|\n)*?)\[/td\]", "<td colspan='$1'>$2</td>"));
		}
		#endregion

		#region Format
		public static string Format(string data) {
			foreach (IHtmlFormatter formatter in _formatters) {
				data = formatter.Format(data);
			}

			return data;
		}
		#endregion

		public BBCodeParser() {
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
