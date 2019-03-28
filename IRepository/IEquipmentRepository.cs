
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
        Task<Result<int>> Save(DtoSave<FaEquipmentEntity> inEnt);



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">主键 ID</param>
        /// <returns></returns>
        Task<Result<int>> Delete(int Id);

        /// <summary>
        ///  获取所有自定义表的列表
        /// </summary>
        /// <returns></returns>
        Task<Result<KTV>> GetTree(int? parentId);

        /// <summary>
        /// 保存设备
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<Result> SaveEquiment(DtoEquipment inEnt);

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<Result> DeleteEquiment(DtoEquipment inEnt);

        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<Result> UpdateEquiment(DtoEquipment inEnt);

        /// <summary>
        /// 获取配置信息和数据
        /// Code=TableId
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<Result<DataGridDataJson>> GetConfigAndData(QuerySearchModel inEnt);
    }
}
