using System;
using log4net;
using log4net.Config;


namespace ITCommunity.Core {
	public static class Logger {

		private static ILog _instance;

		public static ILog Log {
			get {
				return GetInstance();
			}
		}

		private static ILog GetInstance() {
			if (_instance == null) {
				_instance = LogManager.GetLogger(typeof(Logger));
				XmlConfigurator.Configure();
			}
			return _instance;
		}
	}
}
