using System;
using System.Configuration;

namespace ITCommunity.Core {
	/// <summary>
	/// Summary description for Config
	/// </summary>
	public static class Config {

		public static string String(string param) {
			string val = "";
			try {
				val = ConfigurationManager.AppSettings[param];
			}
			catch (Exception ex) {
				Logger.Log.Fatal("Ошибка при чтении конфигурации(web.config секция appSettings), параметр " + param, ex);
			}
			return (val == null) ? "" : val;
		}

		public static int Num(string param) {
			int val = -1;
			try {
				val = Convert.ToInt32(ConfigurationManager.AppSettings[param]);
			}
			catch (Exception ex) {
				Logger.Log.Fatal("Ошибка при чтении конфигурации(web.config секция appSettings), параметр " + param, ex);
			}
			return val;
		}

		public static double Double(string param) {
			double val = -1;
			try {
				val = Convert.ToDouble(ConfigurationManager.AppSettings[param]);
			}
			catch (Exception ex) {
				Logger.Log.Fatal("Ошибка при чтении конфигурации(web.config секция appSettings), параметр " + param, ex);
			}
			return val;
		}
	}
}
