
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// 用于设备操作
    /// </summary>
    public class DtoEquipment
    {
        /// <summary>
        /// 构造
        /// </summary>
        public DtoEquipment()
        {

        }
        /// <summary>
        /// 传入的值
        /// </summary>
        public int TypeId { get; set; }

        public int Id{ get; set; }
    }
}