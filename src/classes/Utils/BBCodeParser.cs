using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using ITCommunity.Core;


namespace ITCommunity.Utils {

    public class BBCodeParser {

        #region Constants

        const string URL = @"(?<url>(?:\w+://)(?:www\.)?(?<site>\w[\w-\.]*\.\w{2,10})(?::[0-9]+)?(?:/.*?)?)";

        const string PLAY_YKT_RU_VIDEO = @"
<object data=""http://play.ykt.ru/player.swf"" width=""640"" height=""480"" type=""application/x-shockwave-flash"">
	<param name=""allowscriptaccess"" value=""always"" />
	<param name=""allowfullscreen"" value=""true"" />
	<param name=""flashvars"" value=""width=640&amp;height=480&amp;file=http://play.ykt.ru/flvideo/$1.flv&amp;image=http://play.ykt.ru/thumb/$1.jpg&amp;displayheight=480&amp;link=http://play.ykt.ru/video/$1&amp;searchbar=false&amp;linkfromdisplay=true"" />
	<param name=""pluginspage"" value=""http://www.macromedia.com/go/getflashplayer"" />
</object>
";

        const string TV_YKT_RU_VIDEO = @"
<embed
    width=""540"" height=""350""
    quality=""high"" bgcolor=""#ffffff""
    menu=""false"" allowfullscreen=""true"" allowscriptaccess=""always""
    flashvars=""provider=http&amp;file=/media/videos/$1.$2&amp;image=/media/thumbnails/full/$1.jpg&amp;playerready=playerReadyCallback&amp;stretching=fill"" 
    src=""http://tv.ykt.ru/media/player.swf"" type=""application/x-shockwave-flash"" />
";

        const string ABUNDA_VIDEO_FORMAT = @"
<embed
    width=""452"" height=""361""
    quality=""high"" bgcolor=""#000000""
    allowfullscreen=""true"" allowscriptaccess=""always""
    src=""http://tube.abunda.ru/player/vPlayer.swf?f=http://tube.abunda.ru/player/vConfig_embed.php?vkey={0}""
    type=""application/x-shockwave-flash"" />
";

        #endregion

        #region Helper Classes

        interface IHtmlFormatter {
            string Format(string data);
        }

        protected class ReplaceFormatter : IHtmlFormatter {
            private string _pattern;
            private string _replace;

            public ReplaceFormatter(string pattern, string replace) {
                _pattern = pattern;
                _replace = replace;
            }

            public string Format(string data) {
                return data.Replace(_pattern, _replace);
            }
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

        protected class TagFormatter : IHtmlFormatter {

            private const string _defaultBBText = "(.|\n)*?";
            private const string _defaultHtmlText = "${text}";
            private const string _patternFormat = @"\[{0}{1}\]\s*(?<text>{2})\s*\[/{0}\]";
            private const string _replaceFormat = "<{0}{1}>{2}</{0}>";

            private string _replace;
            private Regex _regex;
			private bool _cleanCaretReturn = false;

            /// <summary>
            /// Тупо заменяет bbcode на html
            /// </summary>
            /// <param name="htmlTag"></param>
            public TagFormatter(string htmlTag) :
                this(htmlTag, htmlTag) {
            }

            public TagFormatter(string htmlTag, string bbTag) :
                this(htmlTag, bbTag, null) {
            }

			public TagFormatter(string htmlTag, string bbTag, string htmlAttrs) :
				this(htmlTag, bbTag, htmlAttrs, null, false) {
			}
			public TagFormatter(string htmlTag, string bbTag, string htmlAttrs, bool cleanCaretReturn) :
				this(htmlTag, bbTag, htmlAttrs, null, cleanCaretReturn) {
			}

            public TagFormatter(string htmlTag, string bbTag, string htmlAttrs, string bbAttr, bool cleanCaretReturn) :
                this(htmlTag, bbTag, htmlAttrs, bbAttr, _defaultHtmlText, _defaultBBText) {
				_cleanCaretReturn = cleanCaretReturn;
            }

            /// <summary>
            /// Класс для преобразования bbcode тега в html тег.
            /// В htmlAttrs и htmlText можно использовать regex группу ${text}.
            /// htmlText по-умолчанию равен ${text}
            /// </summary>
            /// <param name="htmlTag">название html тега заменяющий bbcode тег (без треугольных скобок)</param>
            /// <param name="bbTag">название bbcode тега (без квадратных скобок)</param>
            /// <param name="htmlAttrs">паттерн для атрибутов html тега (по-умолчанию null)</param>
            /// <param name="bbAttr">regex паттерн для атрибута bbcode тега, да-да только 1 атрибут (по-умолчанию null)</param>
            /// <param name="htmlText">паттерн внутреннего текста html тега (по-умолчанию ${text})</param>
            /// <param name="bbText">regex паттерн внутреннего текста bbcode тега (по-умолчанию (.|\n)*?)</param>
            public TagFormatter(string htmlTag, string bbTag, string htmlAttrs, string bbAttr, string htmlText, string bbText) {
                string pattern;

                if (bbAttr == null) {
                    pattern = string.Format(_patternFormat, bbTag, "", bbText);
                } else {
                    pattern = string.Format(_patternFormat, bbTag, "=" + bbAttr, bbText);
                }

                if (htmlAttrs == null) {
                    _replace = string.Format(_replaceFormat, htmlTag, "", htmlText);
                } else {
                    _replace = string.Format(_replaceFormat, htmlTag, " " + htmlAttrs, htmlText);
                }

                _regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }

            public string Format(string data) {
				if (_cleanCaretReturn) {
					data = _regex.Replace(data, caretCleaner);
				}
                return _regex.Replace(data, _replace);
            }

			private String caretCleaner(Match match) {
				return match.Value.Replace("\n", "");
			}
		}

        #endregion

        #region BBCode

        private static List<IHtmlFormatter> _formatters;

        static BBCodeParser() {
            _formatters = new List<IHtmlFormatter>();

            _formatters.Add(new ReplaceFormatter("\r", ""));

            // модификаторы текста
            _formatters.Add(new TagFormatter("b"));
            _formatters.Add(new TagFormatter("i"));
            _formatters.Add(new TagFormatter("u"));
            _formatters.Add(new TagFormatter("strike", "s"));
            _formatters.Add(new TagFormatter("span", "size", "style=\"font-size:$1px\"", @"(\d*?)px", false));

            // расположение
            _formatters.Add(new TagFormatter("div", "left", "class=\"left\""));
            _formatters.Add(new TagFormatter("div", "right", "class=\"right\""));
            _formatters.Add(new TagFormatter("div", "center", "class=\"center\""));

            // цитата и код
            _formatters.Add(new TagFormatter("blockquote", "quote"));
            _formatters.Add(new TagFormatter("code"));
            _formatters.Add(new TagFormatter("code", "code", "class=\"$1\"", @"(\w*?)", false));
            // знаю, что это глюк
            _formatters.Add(new RegexFormatter("<code(.*?)>((.|\n)*?)</code>", "<pre><code$1>$2</code></pre>"));

            // ссылка
            _formatters.Add(new TagFormatter("a", "url", "href=\"${url}\" title=\"${url}\"", null, "${site}", URL));
            _formatters.Add(new TagFormatter("a", "url", "href=\"${url}\" title=\"${url}\"", URL, false));
            _formatters.Add(new TagFormatter("a", "email", "href=\"mailto:${text}\" title=\"Написать письмо\""));

            // рисунок http://it.icmp.ru/postimages/2174/6529/thumb/648611.jpg 
            var trustedSites = Config.TrustedSites.Replace(" ", "").Replace(',', '|');
            var imgUrl = @"((?:http://(?:www\.)?(?:" + trustedSites + @"))(?::[0-9]+)?/.*?)";
            _formatters.Add(new TagFormatter("img", "img", "src=\"${text}\" alt=\"${text}\"", null, "", imgUrl));
            _formatters.Add(new TagFormatter("img", "img", "src=\"${text}\" alt=\"${text}\" width=\"$1px\" height=\"$2px\"", @"(\d*)x(\d*)px", "", imgUrl));
            _formatters.Add(new RegexFormatter(@"\[img=" + imgUrl + @"\]\s*" + imgUrl + @"\s*\[/img\]", "<a href=\"$1\"><img src=\"$2\" alt=\"$2\" /></a>"));

            var relativeUrl = @"(postimages/.*?)";
            _formatters.Add(new TagFormatter("img", "img", "src=\"http://it.icmp.ru/${text}\" alt=\"${text}\"", null, "", relativeUrl));
            _formatters.Add(new TagFormatter("img", "img", "src=\"http://it.icmp.ru/${text}\" alt=\"${text}\" width=\"$1px\" height=\"$2px\"", @"(\d*)x(\d*)px", "", relativeUrl));
            _formatters.Add(new RegexFormatter(@"\[img=" + relativeUrl + @"\]\s*" + relativeUrl + @"\s*\[/img\]", "<a href=\"http://it.icmp.ru/$1\"><img src=\"/$2\" alt=\"$2\" /></a>"));

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

            // таблица
            _formatters.Add(new TagFormatter("table", "table", "cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"", true));
            _formatters.Add(new TagFormatter("table", "table", "cellpadding=\"0\" cellspacing=\"0\" width=\"$1%\"", @"(\d*%)", true));
            _formatters.Add(new TagFormatter("tr"));
            _formatters.Add(new TagFormatter("td"));
            _formatters.Add(new TagFormatter("td", "td", "colspan=\"$1\"", @"(\d*)", false));

            // убираем whitespaces в video
            _formatters.Add(new RegexFormatter(@"\[video\]\s*(.*?)\s*\[/video\]", "[video]$1[/video]"));
            // play.ykt.ru
            _formatters.Add(new RegexFormatter(@"\[video\]http://play\.ykt\.ru/video/(\d+)/.+?\[/video\]", PLAY_YKT_RU_VIDEO, true));
            // tv.ykt.ru http://tv.ykt.ru/media/videos/SPECREPORT_2_APR_01_sd.mp4
            _formatters.Add(new RegexFormatter(@"\[video\]http://tv\.ykt\.ru/media/videos/(.+?)\.(.+?)\[/video\]", TV_YKT_RU_VIDEO, true));
            // Для Abunda надо высчитывать хеш MD5, к счастью дураки соль не использовали.
            _formatters.Add(new RegexFuncFormatter(@"\[video\]http://tube\.abunda\.ru/video/(\d+)/.+?\[/video\]", abundaEvaluator));

            // мега регексп, который решает какие урлы конвертировать в ссылки
            // через 10 тысяч лет наши потомки найдут этот код и смогут прочесть
            // в этих иероглифах всю историю человечества =)
            _formatters.Add(new RegexFormatter(@"(^|\s)" + URL + @"($|\s)", "$1<a href=\"${url}\" title=\"${url}\">${url}</a>$2"));

            _formatters.Add(new ReplaceFormatter("\n", "<br />"));
        }

        private static string abundaEvaluator(Match match) {
            int videoId;
            if (Int32.TryParse(match.Groups[1].Value, out videoId) == false) {
                return "";
            }
            string hash = Hash.CalculateMD5(match.Groups[1].Value).Substring(11, 20);
            return string.Format(ABUNDA_VIDEO_FORMAT, hash);
        }

        #endregion

        #region Format

        public static string Format(string data) {
            if (data == null) {
                return "";
            }

            data = data.Trim();

            foreach (IHtmlFormatter formatter in _formatters) {
                data = formatter.Format(data);
            }

            return data;
        }

        #endregion
    }
}
