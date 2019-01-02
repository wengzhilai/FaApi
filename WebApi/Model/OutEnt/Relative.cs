using System.Collections.Generic;

namespace WebApi.Model.InEnt
{
    /// <summary>
    /// 操作对象
    /// </summary>
    public class Relative
    {
        /// <summary>
        /// 所有人
        /// </summary>
        /// <value></value>
        public List<dynamic> ItemList { get; set; }
        /// <summary>
        /// 人员关系
        /// </summary>
        /// <value></value>
        public List<dynamic> RelativeList { get; set; }
    }
}