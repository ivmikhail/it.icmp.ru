using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO;
using System.Web.Caching;
using System.Text;

/// <summary>
/// Summary description for Greeting
/// </summary>
namespace ITCommunity {
    public class Greeting {
        private static Greeting instance = null;

        private const String GREETINGS_KEY = "GREETING_KEY";
        private const String GREETINGS_PERSONAL_KEY = "GREETING_PERSONAL_KEY";
        private const String PATH_TO_COMMON_GREETINGS_FILE = "\\media\\other\\greetings.txt";
        private const String PATH_TO_PERSONAL_GREETINGS_FILE = "\\media\\other\\greetings_personal.txt";

        private readonly Random random;
        private readonly String fullPathToCommonGreetingsFile;
        private readonly String fullPathToPersonalGreetingsFile;
        private List<String> commonGreetings {
            get {
                if (HttpContext.Current.Cache[GREETINGS_KEY] == null) {
                    HttpContext.Current.Cache.Insert(GREETINGS_KEY, readFromCommonGreetingsFile(), null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
                }
                return (List<String>) HttpContext.Current.Cache[GREETINGS_KEY];
            }
        }
        private Dictionary<String, String> personalGreetings {
            get {
                if (HttpContext.Current.Cache[GREETINGS_PERSONAL_KEY] == null) {
                    HttpContext.Current.Cache.Insert(GREETINGS_PERSONAL_KEY, readFromPersonalGreetingsFile(), null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
                }
                return (Dictionary<String, String>)HttpContext.Current.Cache[GREETINGS_PERSONAL_KEY];
            }
        }

        private Greeting() {
            random = new Random();
            fullPathToCommonGreetingsFile = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + PATH_TO_COMMON_GREETINGS_FILE;
            fullPathToPersonalGreetingsFile = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + PATH_TO_PERSONAL_GREETINGS_FILE;
        }

        private List<string> readFromCommonGreetingsFile() {
            List<String> result = new List<string>();
            if (File.Exists(fullPathToCommonGreetingsFile)) {
                string[] lines = File.ReadAllLines(fullPathToCommonGreetingsFile, Encoding.GetEncoding(1251));
                foreach (string s in lines) {
                    if (!s.StartsWith(";")) {
                        result.Add(s);
                    }
                }
            }
            return result;
        }
        private Dictionary<String, String> readFromPersonalGreetingsFile() {
            Dictionary<String, String> result = new Dictionary<string, string>();
            if (File.Exists(fullPathToPersonalGreetingsFile)) {
                string[] lines = File.ReadAllLines(fullPathToPersonalGreetingsFile, Encoding.GetEncoding(1251));
                foreach (string s in lines) {
                    if (!s.StartsWith(";")) {
                        int spacePos = s.IndexOf(" ");
                        if (spacePos > 1) {
                            string nick = s.Substring(0, spacePos).ToLower();
                            string greeting = s.Substring(spacePos + 1);
                            result.Add(nick, greeting);
                        }
                    }
                }
            }
            return result;
        }
        public static Greeting GetInstance() {
            if (instance == null) {
                instance = new Greeting();
            }
            return instance;
        }
        public String GetGreeting(String nick) {
            String result;
            List<String> list = commonGreetings;
            if (list.Count == 0) {
                result = "типа привет, " + nick;
            } else {
                if (personalGreetings.ContainsKey(nick.ToLower())) {
                    result = personalGreetings[nick.ToLower()] + ", " + nick + "!";
                    //int r = random.Next(list.Count + 1);
                    //if (r == 0) {
                    //    result = personalGreetings[nick.ToLower()] + ", " + nick + "!";
                    //} else {
                    //    result = list[r - 1] + ", " + nick + "!";
                    //}
                } else {
                    result = list[random.Next(list.Count)] + ", " + nick + "!";
                }
            }
            return result;
        }

    }
}