
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// 用于保存
    /// </summary>
    public class DtoSave<T>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public DtoSave()
        {
            SaveFieldList = new List<string>();
            IgnoreFieldList = new List<string>();
        }
        /// <summary>
        /// 用于操作的对象
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 需要保存的字段
        /// </summary>
        public List<string> SaveFieldList { get; set; }
        /// <summary>
        /// 需要忽略的字段
        /// </summary>
        public List<string> IgnoreFieldList { get; set; }
        /// <summary>
        /// 更新条件,如果为空.则以主键为更新条件
        /// </summary>
        public List<string> WhereList { get; set; }
        
    }
}