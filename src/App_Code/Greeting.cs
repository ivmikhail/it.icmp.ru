using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ITCommunity {
	/// <summary>
	/// Summary description for Greeting
	/// </summary>
	public static class Greeting {
		private delegate object GreetingLoader();

		#region Properties

		private static GreetingLoader _commonGreetingLoader = new GreetingLoader(ReadFromCommonGreetingsFile);
		private static GreetingLoader _personalGreetingLoader = new GreetingLoader(ReadFromPersonalGreetingsFile);
		private static readonly Random _random;
		private static readonly string _fullPathToCommonGreetingsFile;
		private static readonly string _fullPathToPersonalGreetingsFile;

		#endregion

		static Greeting() {
			_random = new Random();
			_fullPathToCommonGreetingsFile = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + Config.String("GreetingPath");
			_fullPathToPersonalGreetingsFile = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + Config.String("PersonalGreetingPath");
		}

		public static string Get(string nick) {
			string result;

			List<string> common = GetCommonGreetings();
			Dictionary<string, string> personal = GetPersonalGreetings();

			if (personal.ContainsKey(nick.ToLower())) {
				result = personal[nick.ToLower()];
			}
			else {
				if (common.Count == 0) {
					result = "Привет";
				}
				else {
					result = common[_random.Next(common.Count)];
				}
			}

			return result + ", " + nick + "!";
		}

		private static List<string> GetCommonGreetings() {
			object greetings = AppCache.Get(
				Config.String("GreetingCacheName"),
				_commonGreetingLoader,
				null,
				Config.Double("GreetingCachePer")
			);
			return (List<string>)greetings;
		}

		private static Dictionary<string, string> GetPersonalGreetings() {
			object greetings = AppCache.Get(
				Config.String("PersonalGreetingCacheName"),
				_personalGreetingLoader,
				null,
				Config.Double("PersonalGreetingCachePer")
			);
			return (Dictionary<string, string>)greetings;
		}

		private static List<string> ReadFromCommonGreetingsFile() {
			List<string> result = new List<string>();

			if (File.Exists(_fullPathToCommonGreetingsFile)) {
				string[] lines = File.ReadAllLines(_fullPathToCommonGreetingsFile, Encoding.GetEncoding(1251));
				foreach (string s in lines) {
					if (!s.StartsWith(";")) {
						result.Add(s);
					}
				}
			}

			return result;
		}

		private static Dictionary<string, string> ReadFromPersonalGreetingsFile() {
			Dictionary<string, string> result = new Dictionary<string, string>();

			if (File.Exists(_fullPathToPersonalGreetingsFile)) {
				string[] lines = File.ReadAllLines(_fullPathToPersonalGreetingsFile, Encoding.GetEncoding(1251));
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
	}
}
