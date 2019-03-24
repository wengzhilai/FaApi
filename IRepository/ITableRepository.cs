
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;
using Models.Entity;

namespace IRepository
{
    public interface ITableRepository 
    {
        /// <summary>
        /// 获取表的单个对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<FaTableTypeEntity> SingleByKey(int key);

        /// <summary>
        /// 保存和自定义Table
        /// 包括列，对列只更新不删除，但可以禁用和户用
        /// 保存成功后，生成创建脚本和修改脚本对物理表的列修改和添加
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<Result<bool>> Save(DtoSave<FaTableTypeEntity> inEnt);

        /// <summary>
        ///  获取所有自定义表的列表
        /// </summary>
        /// <returns></returns>
        Task<Result<KTV>> GetTableSelect();

    }
}
