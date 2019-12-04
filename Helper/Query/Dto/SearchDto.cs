using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Query.Dto
{
    public class SearchDto
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string sort { get; set; }
        /// <summary>
        /// 排序类型 asc|desc
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// Query代码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 每页显示数
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 复杂筛选条件
        /// </summary>
        public IList<SearchWhereDto> whereList { get; set; }
        public string whereListStr { get; set; }
    }
}
