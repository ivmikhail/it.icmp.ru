using System;
using System.Text.RegularExpressions;
using System.Web;

using ITCommunity.Core;
using ITCommunity.Db.Tables;
using ITCommunity.Utils;


namespace ITCommunity.Db {

    public partial class Comment {

        /// <summary>
        /// Пользователь оставивший комментарий, если это сделал не авторизованный человек,
        /// то возвращается анонимный пользователь
        /// </summary>
        public User Author {
            get;
            private set;
        }

        public string ShortText {
            get {
                var safelyText = HttpUtility.HtmlEncode(Text);
                safelyText = Regex.Replace(safelyText, "\\[(.*?)\\](.*?)\\[\\/(.*?)\\]", "$2", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                if (safelyText.Length < Config.GetInt("LastCommentsSize")) {
                    return safelyText;
                }

                return safelyText.Substring(0, Config.GetInt("LastCommentsSize")) + "...";
            }
        }

        public bool Editable {
            get {
                if (!CurrentUser.IsAuth || CurrentUser.User.Id != AuthorId) {
                    return false;
                }

                var editableTime = CreateDate.AddMinutes(Config.GetDouble("CommentEditableMinutes"));
                return editableTime.CompareTo(DateTime.Now) > 0;
            }
        }

        public string TimePassed() {
            var ticks = DateTime.Now.AddTicks(-CreateDate.Ticks).Ticks;
            var timespan = new TimeSpan(ticks);
            var time = "Добавлено ";

            if (timespan.Days != 0)
                time += timespan.Days.ToString() + "д ";
            if (timespan.Hours != 0)
                time += timespan.Hours.ToString() + "ч ";
            if (timespan.Minutes != 0)
                time += timespan.Minutes.ToString() + "м ";
            if (time.Length == 0)
                time += timespan.Seconds.ToString() + "с ";

            return time + "назад";
        }

        /// <summary>
        /// Полностью форматированное в безопасный хтмл текст
        /// </summary>
        public string TextFormatted {
            get { return BBCodeParser.Format(HttpUtility.HtmlEncode(Text)); }
        }

        partial void OnLoaded() {
            Author = Users.Get(AuthorId);
        }
    }
}
