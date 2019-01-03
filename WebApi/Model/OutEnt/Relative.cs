using System.Collections.Generic;
using Models;

namespace WebApi.Model.InEnt
{
    /// <summary>
    /// 操作对象
    /// </summary>
    public class Relative
    {
        public Relative() {
            ItemList = new List<FaUserInfoRelativeItem>();
            ElderList = new List<FA_ELDER>();
            RelativeList = new List<KV>();
        }

        /// <summary>
        /// 展示所有用户
        /// </summary>
        public IList<FaUserInfoRelativeItem> ItemList { get; set; }
        /// <summary>
        /// 所有辈分
        /// </summary>
        public IList<FA_ELDER> ElderList { get; set; }
        /// <summary>
        /// 所有关系
        /// </summary>
        public IList<KV> RelativeList { get; set; }
    }
}