

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    /// <summary>
    /// 用于查询
    /// </summary>
    public class DtoSearch
    {
        public DtoSearch()
        {
            PageIndex = 1;
            PageSize = 10;
            FilterList = new Dictionary<string, object>();
        }
        /// <summary>
        /// 筛选条件
        /// </summary>
        public Dictionary<string, object> FilterList { get; set; }
        /// <summary>
        /// 排序字符串，包括字段和类型，如 ："ID DESC"
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 当前页面
        /// </summary>
        [Range(1, 10000, ErrorMessage = "当前页码值不能小于1")]
        public int PageIndex { get; set; }
        /// <summary>
        /// 页码大小
        /// </summary>
        [Range(1, 100, ErrorMessage = "页码大小值不能小于1")]
        public int PageSize { get; set; }
    }

    /// <summary>
    /// 用于查询和删除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DtoDo<T>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public DtoDo()
        {

        }
        /// <summary>
        /// 传入的值
        /// </summary>
        public T Key { get; set; }
    }
    

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
