using System;
using System.Configuration;

namespace ITCommunity {
	public static class Config {

		public static string Get(string param) {
			string val = null;
			try {
				val = ConfigurationManager.AppSettings[param];
			}
			catch (ConfigurationErrorsException ex) {
				Logger.Log.Fatal("Ошибка при чтении конфигурации(web.config секция appSettings), параметр " + param, ex);
			}
			return val;
		}

		public static int GetInt(string param) {
			return Convert.ToInt32(Config.Get(param));
		}

		public static double GetDouble(string param) {
			return Convert.ToDouble(Config.Get(param));
		}

	}
}
