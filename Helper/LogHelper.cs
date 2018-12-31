using System;
using System.IO;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace Helper
{
    public class LogHelper
    {
        private static log4net.ILog logger = null;
        public static void logInfo<T>(string logstr)
        {
            Console.WriteLine(logstr);
            if (initLog4net())
                logger.Info(typeof(T).Namespace + typeof(T).Name + "\r\n" + logstr);
        }

        public static void logInfo(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            if (initLog4net())
                logger.InfoFormat(format, args);
        }

        public static void logError<T>(string logstr)
        {
            Console.WriteLine(logstr);
            if (initLog4net())
                logger.Error(typeof(T).Namespace + typeof(T).Name + "\r\n" + logstr);
        }

        public static void logError(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            if (initLog4net())
                logger.ErrorFormat(format, args);
        }

        private static object objlock = new object();//初始化log用的锁
        private static bool initLog4net()
        {
            if (logger != null)
                return true;
            lock (objlock)
            {
                if (logger == null)
                {
                    ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
                    XmlConfigurator.Configure(repository, new FileInfo(AppSettingsManager.Logging.Log4netConfigPath));
                    logger = LogManager.GetLogger(repository.Name, "NETCorelog4net");
                    return true;
                }
            }
            return false;
        }
    }
}