using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Result
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        /// <value></value>
        public bool IsSuccess { get; set; } = true;
        /// <summary>
        /// 消息
        /// </summary>
        /// <value></value>
        public string Msg { get; set; } = "";
        /// <summary>
        /// 代码
        /// </summary>
        /// <value></value>
        public string Code { get; set; } = "";
    }
    /// <summary>
    /// 返回的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>:Result
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        /// <value></value>
        public T Data { get; set; }
        /// <summary>
        /// 返回列表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> DataList{get;set;}=new List<T>();
    }
}