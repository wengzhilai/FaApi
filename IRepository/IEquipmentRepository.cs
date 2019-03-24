
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;
using Models.Entity;

namespace IRepository
{
    public interface IEquipmentRepository 
    {
        /// <summary>
        /// 获取表的单个对象
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="t"></typeparam>
        /// <returns></returns>
        Task<FaEquipmentEntity> SingleByKey<t>(t key);

        /// <summary>
        /// 保存设备
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<Result<bool>> Save(DtoSave<FaEquipmentEntity> inEnt);

        /// <summary>
        ///  获取所有自定义表的列表
        /// </summary>
        /// <returns></returns>
        Task<Result<KTV>> GetTree();

    }
}
