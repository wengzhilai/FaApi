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
        
        public static void Init(ILog _logger){
            logger=_logger;
        }



         #region 输出错误日志到Log4Net

        /// <summary>
        /// 输出错误日志到Log4Net
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteErrorLog<T>(string msg,Exception ec=null)
        {
            //LogManager.GetLogger("NETCoreRepository",typeof(T)).Error(msg, ec);
            //log.ErrorFormat(msg);
        }

        public static void WriteErrorLog(Type type,string msg,Exception ec=null)
        {
            //LogManager.GetLogger("NETCoreRepository",type).Error(msg, ec);
        }
        #endregion

        #region 输出记录日志到Log4Net
        /// <summary>
        /// 输出记录日志到Log4Net
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLog<T>(string msg)
        {
            LogManager.GetLogger("NETCoreRepository",typeof(T)).Info(msg);
        }
        #endregion
    }
}