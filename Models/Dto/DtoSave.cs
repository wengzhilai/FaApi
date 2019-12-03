
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// 用于保存
    /// </summary>
    public class DtoSave<T>
    {
        public string token { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        public DtoSave()
        {
            saveFieldList = new List<string>();
            ignoreFieldList = new List<string>();
        }
        /// <summary>
        /// 用于操作的对象
        /// </summary>
        public T data { get; set; }
        /// <summary>
        /// 需要保存的字段
        /// </summary>
        public List<string> saveFieldList { get; set; }
        /// <summary>
        /// 需要忽略的字段
        /// </summary>
        public List<string> ignoreFieldList { get; set; }
        /// <summary>
        /// 更新条件,如果为空.则以主键为更新条件
        /// </summary>
        public List<string> whereList { get; set; }
        
    }
}