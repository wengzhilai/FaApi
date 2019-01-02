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



         #region 输出错误日志到Log4Net
        /// <summary>
        /// 输出错误日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void WriteLog<T>(Exception ex)
        {
            if (!initLog4net()){
                return;
            }
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            log.Error("Error", ex);
        }
        /// <summary>
        /// 输出错误日志到Log4Net
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteErrorLog<T>(string msg)
        {
            if (!initLog4net()){
                return;
            }
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            Exception ec = new Exception(msg);
            log.Error("Error", ec);
            //log.ErrorFormat(msg);
        }
        #endregion

        #region 输出记录日志到Log4Net
        /// <summary>
        /// 输出记录日志到Log4Net
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLog<T>(string msg)
        {
            if (!initLog4net()){
                return;
            }
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(T));
            log.Info(msg);
        }
        #endregion
    }
}