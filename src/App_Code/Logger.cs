using System;
using log4net;
using log4net.Config;


namespace ITCommunity
{
    public static class Logger
    {
        private static ILog log_instance;

        public static ILog Log
        {
            get 
            {
                return GetInstance();
            }
        }

        private static ILog GetInstance()
        {
            if (log_instance == null)
            {
                log_instance = LogManager.GetLogger(typeof(Logger));
                XmlConfigurator.Configure();
            }
            return log_instance;
        }
    }
}
