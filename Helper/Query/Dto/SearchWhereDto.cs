using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Query.Dto
{
    public class SearchWhereDto
    {
        /// <summary>
        /// 对象字段
        /// </summary>
        public string objFiled { get; set; }
        /// <summary>
        /// 操作符 如：between、in、《、》、=、like
        /// </summary>
        public string opType { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string fieldType { get; set; }
    }
}
