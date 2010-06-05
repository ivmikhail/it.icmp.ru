using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ITCommunity.Core;

namespace ITCommunity.Module {

    public static class Greeting {

        #region For caching

        public const string GREETINGS_CACHE_KEY = "Greeting";
        public const string PERSONAL_GREETINGS_CACHE_KEY = "PersonalGreeting";

        #endregion

        #region Properties

        private static readonly Random _random = new Random();
        private static readonly string _commonGreetingsPath;
        private static readonly string _personalGreetingsPath;

        #endregion

        static Greeting() {
            var appPath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath);

            _commonGreetingsPath = appPath + Config.Get("GreetingPath");
            _personalGreetingsPath = appPath + Config.Get("PersonalGreetingPath");
        }

        public static string Get() {
            string result;
            string nick = CurrentUser.User.Nick;

            var personalGreetings = GetPersonalGreetings();

            if (personalGreetings.ContainsKey(nick.ToLower())) {
                result = personalGreetings[nick.ToLower()];
            } else {
                var commonGreetings = GetCommonGreetings();
                if (commonGreetings.Count == 0) {
                    result = "Привет";
                } else {
                    result = commonGreetings[_random.Next(commonGreetings.Count)];
                }
            }

            return result + ", " + nick + "!";
        }

        private static List<string> GetCommonGreetings() {
            var greetings = AppCache.Get(
                GREETINGS_CACHE_KEY,
                LoadCommonGreetings
            );

            return greetings;
        }

        private static Dictionary<string, string> GetPersonalGreetings() {
            var greetings = AppCache.Get(
                PERSONAL_GREETINGS_CACHE_KEY,
                LoadPersonalGreetings
            );

            return greetings;
        }

        private static List<string> LoadCommonGreetings() {
            var greetings = new List<string>();

            if (File.Exists(_commonGreetingsPath)) {
                string[] lines = File.ReadAllLines(_commonGreetingsPath, Encoding.GetEncoding(1251));
                foreach (var line in lines) {
                    if (!line.StartsWith(";")) {
                        greetings.Add(line);
                    }
                }
            }

            return greetings;
        }

        private static Dictionary<string, string> LoadPersonalGreetings() {
            var greetings = new Dictionary<string, string>();

            if (File.Exists(_personalGreetingsPath)) {
                string[] lines = File.ReadAllLines(_personalGreetingsPath, Encoding.GetEncoding(1251));
                foreach (var line in lines) {
                    if (!line.StartsWith(";")) {
                        int spacePos = line.IndexOf(" ");
                        if (spacePos > 1) {
                            string nick = line.Substring(0, spacePos).ToLower();
                            string text = line.Substring(spacePos + 1);
                            greetings.Add(nick, text);
                        }
                    }
                }
            }

            return greetings;
        }
    }
}
