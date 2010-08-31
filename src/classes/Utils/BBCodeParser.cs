using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using ITCommunity.Core;


namespace ITCommunity.Utils {

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

        private static List<IHtmlFormatter> _formatters;

        static BBCodeParser() {
            _formatters = new List<IHtmlFormatter>();

            _formatters.Add(new SearchReplaceFormatter("\r", ""));

            // <hr /> вместо <br />
            _formatters.Add(new RegexFormatter("\n\n", "\n<hr />\n"));

            // модификаторы текста
            _formatters.Add(new RegexFormatter(@"\[b\](.*?)\[/b\]", "<b>$1</b>"));
            _formatters.Add(new RegexFormatter(@"\[i\](.*?)\[/i\]", "<i>$1</i>"));
            _formatters.Add(new RegexFormatter(@"\[u\](.*?)\[/u\]", "<u>$1</u>"));
            _formatters.Add(new RegexFormatter(@"\[s\](.*?)\[/s\]", "<strike>$1</strike>"));
            _formatters.Add(new RegexFormatter(@"\[size=(\d*?)px\](.*?)\[/size\]", "<span style=\"font-size:$1px\">$2</span>"));

            // расположение
            _formatters.Add(new RegexFormatter(@"\[left\]((.|\n)*?)\[/left]", "<div class=\"float-left\">$1</div>"));
            _formatters.Add(new RegexFormatter(@"\[right\]((.|\n)*?)\[/right]", "<div class=\"float-right\">$1</div>"));
            _formatters.Add(new RegexFormatter(@"\[center\]((.|\n)*?)\[/center]", "<div class=\"center\">$1</div><div class=\"clear\"></div>"));

            // цитата и код
            _formatters.Add(new RegexFormatter(@"\[quote\]((.|\n)*?)\[/quote]", "<blockquote>$1</blockquote>"));
            _formatters.Add(new RegexFormatter(@"\[code\](?:\s*)((.|\n)*?)\[/code\]", "<pre><code>$1</code></pre>"));
            _formatters.Add(new RegexFormatter(@"\[code=(\w*)\](?:\s*)((.|\n)*?)\[/code\]", "<pre><code class=\"$1\">$2</code></pre>"));

            // мега регексп, который решает какие урлы конвертировать в ссылки
            // через 10 тысяч лет наши потомки найдут этот код и смогут прочесть
            // в этих иероглифах историю всего человечества =)
            _formatters.Add(new RegexFormatter(@"(^?[^""\]=])(?:http://)(.*?)([,;\.\!]?[\s$])", "$1<a href=\"http://$2\" title=\"$2\">$2</a>$3"));
            // ссылка
            _formatters.Add(new RegexFormatter(@"\[url\](?:http://)?(.*?)\[/url\]", "<a href=\"http://$1\" title=\"$1\">$1</a>"));
            _formatters.Add(new RegexFormatter(@"\[url=(?:http://)?(.*?)\](.*?)\[/url\]", "<a href=\"http://$1\" title=\"$1\">$2</a>"));
            _formatters.Add(new RegexFormatter(@"\[email\](.*?)\[/email\]", "<a href=\"mailto:$1\">$1</a>"));
            // попап
            _formatters.Add(new RegexFormatter(@"\[popup=(.*?)\](.*?)\[/popup\]", "<a href=\"$1\" >$2</a>"));

            // рисунок http://it.icmp.ru/postimages/2174/6529/thumb/648611.jpg
            var trustedSites = Config.Get("TrustdedSites").Replace(" ", "").Replace(',', '|');
            var imgPattern = @"((?:postimages|http://(?:" + trustedSites + @"))/.*?)";
            _formatters.Add(new RegexFormatter(@"\[img\]" + imgPattern + @"\[/img\]", "<img src=\"$1\" alt=\"$1\" />"));
            _formatters.Add(new RegexFormatter(@"\[img=(\d*)x(\d*)px\]" + imgPattern + @"\[/img\]", "<img width=\"$1px\" height=\"$2px\" src=\"$3\" alt=\"$3\" />"));

            // убираем whitespaces в списке
            _formatters.Add(new RegexFormatter(@"(\[list.*?\])\s+", "$1"));
            _formatters.Add(new RegexFormatter(@"\s*(\[\*\])\s*", "$1"));
            _formatters.Add(new RegexFormatter(@"\s+(\[/list\])", "$1"));
            // список
            string sListFormat = "<ol style=\"list-style-type:{0};\">$1</ol>";
            _formatters.Add(new RegexFormatter(@"\[\*\]([^\[]*)", "<li>$1</li>"));
            _formatters.Add(new RegexFormatter(@"\[list\]((.|\n)*?)\[/list\]", "<ul>$1</ul>"));
            _formatters.Add(new RegexFormatter(@"\[list=1\]((.|\n)*?)\[/list\]", string.Format(sListFormat, "decimal"), false));
            _formatters.Add(new RegexFormatter(@"\[list=i\]((.|\n)*?)\[/list\]", string.Format(sListFormat, "lower-roman"), false));
            _formatters.Add(new RegexFormatter(@"\[list=I\]((.|\n)*?)\[/list\]", string.Format(sListFormat, "upper-roman"), false));
            _formatters.Add(new RegexFormatter(@"\[list=a\]((.|\n)*?)\[/list\]", string.Format(sListFormat, "lower-alpha"), false));
            _formatters.Add(new RegexFormatter(@"\[list=A\]((.|\n)*?)\[/list\]", string.Format(sListFormat, "upper-alpha"), false));

            // убираем whitespaces в таблице
            _formatters.Add(new RegexFormatter(@"\s*(\[/?tr.*?\])\s*", "$1"));
            _formatters.Add(new RegexFormatter(@"\s*(\[/?td.*?\])\s*", "$1"));
            // таблица
            _formatters.Add(new RegexFormatter(@"\[table\]((.|\n)*?)\[/table\]", "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">$1</table>"));
            _formatters.Add(new RegexFormatter(@"\[table=(\d*%)\]((.|\n)*?)\[/table\]", "<table cellpadding=\"0\" cellspacing=\"0\" width=\"$1\">$2</table>"));
            _formatters.Add(new RegexFormatter(@"\[tr\]((.|\n)*?)\[/tr\]", "<tr>$1</tr>"));
            _formatters.Add(new RegexFormatter(@"\[td\]((.|\n)*?)\[/td\]", "<td>$1</td>"));
            _formatters.Add(new RegexFormatter(@"\[td=(\d*)\]((.|\n)*?)\[/td\]", "<td colspan=\"$1\">$2</td>"));

            // убираем whitespaces в video
            _formatters.Add(new RegexFormatter(@"\[video\]\s*(.*?)\s*\[/video\]", "[video]$1[/video]"));
            // play.ykt.ru
            _formatters.Add(new RegexFormatter(@"\[video\]http://play\.ykt\.ru/video/(\d+)/.+?\[/video\]", @"
<object data=""http://play.ykt.ru/player.swf"" width=""640"" height=""480"" type=""application/x-shockwave-flash"">
	<param name=""allowscriptaccess"" value=""always"" />
	<param name=""allowfullscreen"" value=""true"" />
	<param name=""flashvars"" value=""width=640&amp;height=480&amp;file=http://play.ykt.ru/flvideo/$1.flv&amp;image=http://play.ykt.ru/thumb/$1.jpg&amp;displayheight=480&amp;link=http://play.ykt.ru/video/$1&amp;searchbar=false&amp;linkfromdisplay=true"" />
	<param name=""pluginspage"" value=""http://www.macromedia.com/go/getflashplayer"" />
</object>
", true));
            // tv.ykt.ru http://tv.ykt.ru/media/videos/SPECREPORT_2_APR_01_sd.mp4
            _formatters.Add(new RegexFormatter(@"\[video\]http://tv\.ykt\.ru/media/videos/(.+?)\.(.+?)\[/video\]", @"
<embed
    width=""540"" height=""350""
    quality=""high"" bgcolor=""#ffffff""
    menu=""false"" allowfullscreen=""true"" allowscriptaccess=""always""
    flashvars=""provider=http&amp;file=/media/videos/$1.$2&amp;image=/media/thumbnails/full/$1.jpg&amp;playerready=playerReadyCallback&amp;stretching=fill"" 
    src=""http://tv.ykt.ru/media/player.swf"" type=""application/x-shockwave-flash"" />
", true));
            // Для Abunda надо высчитывать хеш MD5, к счастью дураки соль не использовали.
            _formatters.Add(new RegexFuncFormatter(@"\[video\]http://tube\.abunda\.ru/video/(\d+)/.+?\[/video\]", abundaEvaluator));

        }

        private static string abundaEvaluator(Match match) {
            int videoId;
            if (Int32.TryParse(match.Groups[1].Value, out videoId) == false) {
                return "";
            }
            string hash = Hash.CalculateMD5(match.Groups[1].Value).Substring(11, 20);
            return @"
<embed
    width=""452"" height=""361""
    quality=""high"" bgcolor=""#000000""
    allowfullscreen=""true"" allowscriptaccess=""always""
    src=""http://tube.abunda.ru/player/vPlayer.swf?f=http://tube.abunda.ru/player/vConfig_embed.php?vkey=" + hash + @"""
    type=""application/x-shockwave-flash"" />
";
        }

        #endregion

        #region Format

        public static string Format(string data) {
            if (data == null) {
                return "";
            }

            foreach (IHtmlFormatter formatter in _formatters) {
                data = formatter.Format(data);
            }

            return data;
        }

        #endregion
    }
}
