using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ITCommunity.Core;
using ITCommunity.Utils;


namespace ITCommunity.Modules {

    public static class Greeting {

        public const string GREETINGS_KEY = "Greeting";
        public const string PERSONAL_GREETINGS_KEY = "PersonalGreeting";

        private static readonly string _commonsPath;
        private static readonly string _personalsPath;

        static Greeting() {
            var appPath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath);

            _commonsPath = appPath + Config.Get("GreetingPath");
            _personalsPath = appPath + Config.Get("PersonalGreetingPath");
        }

        public static string Get() {
            string result;
            string nick = CurrentUser.User.Nick;

            var personals = AppCache.Get(PERSONAL_GREETINGS_KEY, LoadPersonals);

            if (personals.ContainsKey(nick.ToLower())) {
                result = personals[nick.ToLower()];
            } else {
                var commons = AppCache.Get(GREETINGS_KEY, LoadCommons);
                if (commons.Count == 0) {
                    result = "Привет";
                } else {
                    result = commons.Random();
                }
            }

            return result;
        }

        private static List<string> LoadCommons() {
            var greetings = new List<string>();

            if (File.Exists(_commonsPath)) {
                string[] lines = File.ReadAllLines(_commonsPath, Encoding.GetEncoding(1251));
                foreach (var line in lines) {
                    if (line.StartsWith(";") == false) {
                        greetings.Add(line);
                    }
                }
            }

            return greetings;
        }

        private static Dictionary<string, string> LoadPersonals() {
            var greetings = new Dictionary<string, string>();

            if (File.Exists(_personalsPath)) {
                string[] lines = File.ReadAllLines(_personalsPath, Encoding.GetEncoding(1251));
                foreach (var line in lines) {
                    if (line.StartsWith(";") == false) {
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
