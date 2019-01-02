using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{

    /// <summary>
    /// 自定义错误类
    /// </summary>
    public sealed class ExceptionExtend : Exception
    {
        /// <summary>
        /// 取得当前方法命名空间  
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// 取得当前方法类全名 包括命名空间  
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 取得当前方法名
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string RealMsg { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string RealCode { get; set; }

        /// <summary>
        /// 真实错误
        /// </summary>
        public Exception RealException { get; set; }

        public ExceptionExtend(Exception ex,MethodBase methodBase, string realMsg = null, string realCode = null)
        {
            RealException = ex;
            Namespace = methodBase.DeclaringType.Namespace;
            FullName = methodBase.DeclaringType.FullName;
            MethodName = methodBase.Name;
            RealMsg = realMsg;
            RealCode = realCode;
            LogHelper.WriteErrorLog<ExceptionExtend>(ex.ToString());

        }

        public ExceptionExtend(string realMsg = null, string realCode = null)
        {
            RealMsg = realMsg;
            RealCode = realCode;
            LogHelper.WriteLog<ExceptionExtend>(realMsg);
        }
        
    }

}
