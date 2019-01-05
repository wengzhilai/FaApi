using System.Collections.Generic;
using Models;
using Models.Entity;

namespace WebApi.Model.InEnt
{
    /// <summary>
    /// 操作对象
    /// </summary>
    public class Relative
    {
        /// <summary>
        /// 构造
        /// </summary>
        public Relative() {
            ItemList = new List<RelativeItem>();
            ElderList = new List<FaElderEntity>();
            RelativeList = new List<KV>();
        }

        /// <summary>
        /// 展示所有用户
        /// </summary>
        public IList<RelativeItem> ItemList { get; set; }
        /// <summary>
        /// 所有辈分
        /// </summary>
        public IList<FaElderEntity> ElderList { get; set; }
        /// <summary>
        /// 所有关系
        /// </summary>
        public IList<KV> RelativeList { get; set; }
    }
}