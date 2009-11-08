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
        private const String PATH_TO_FILE = "\\media\\other\\greetings.txt";

        private readonly Random random;
        private readonly String fullPathToFile;
        private String[] greetings {
            get {
                if (HttpContext.Current.Cache[GREETINGS_KEY] == null) {
                    HttpContext.Current.Cache.Insert(GREETINGS_KEY, readFromFile(), null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
                }
                return (String[]) HttpContext.Current.Cache[GREETINGS_KEY];
            }
        }
        private Greeting() {
            random = new Random();
            fullPathToFile = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + PATH_TO_FILE;
        }

        private string[] readFromFile() {
            if (File.Exists(fullPathToFile)) {
                return File.ReadAllLines(fullPathToFile, Encoding.GetEncoding(1251));
            } else {
                return new string[0];
            }
        }
        public static Greeting GetInstance() {
            if (instance == null) {
                instance = new Greeting();
            }
            return instance;
        }
        public String GetGreeting() {
            String[] list = greetings;
            if (list.Length == 0) {
                return "типа привет";
            }
            return list[random.Next(list.Length)];
        }

    }
}