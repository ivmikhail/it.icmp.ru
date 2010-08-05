using ITCommunity.Core;
using ITCommunity.Util;
using ITCommunity.Db.Tables;
using System.Web;
using System;
using System.Text.RegularExpressions;

namespace ITCommunity.Db {

    /// <summary>
    /// Пост хранящийся в БД
    /// </summary>
    public partial class Comment {

        /// <summary>
        /// Пользователь оставивший комментарий, если это сделал не авторизованный человек,
        /// то возвращается "пустой" пользователь
        /// </summary>
        public User getAuthor() {
            return Users.Get(UserId);
        }

        public bool Editable {
            get {
                if (!CurrentUser.isAuth || CurrentUser.User.Id != UserId) {
                    return false;
                }

                var editableTime = CreateDate.AddMinutes(Config.GetDouble("CommentEditableMinutes"));
                return editableTime.CompareTo(DateTime.Now) > 0;
            }
        }

        public string howMuchTimeHasPassed() {
            long ticks = DateTime.Now.AddTicks(-CreateDate.Ticks).Ticks;
            TimeSpan timespan = new TimeSpan(ticks);
            string time = "";
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

        /// <summary>
        /// Полностью форматированное в безопасный хтмл текст
        /// </summary>
        public string TextFormatted {
            get { return BBCodeParser.Format(HttpUtility.HtmlEncode(Text)); }
        }

    }
}
