
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


        /// <summary>
        /// 生成创建表SQL
        /// </summary>
        /// <returns></returns>
        string MakeSqlCreateTable(FaTableTypeEntity inEnt);

        /// <summary>
        /// 修改字段类型和注释
        /// alter table {0}  modify column description varchar(255) null COMMENT '应用描述';
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        string MakeSqlAlterTable(string tableName,FaTableColumnEntity inEnt);

        /// <summary>
        /// 生成添加字段字段
        /// alert table sys_application add `url` varchar(255) not null comment '应用访问地址';  
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        string MakeSqlAlterAddColumn(string tableName,FaTableColumnEntity inEnt);

        /// <summary>
        /// 修改字段名字
        /// alter table {0} change name app_name varchar(255) not null comment '应用访问地址'; 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="oldName"></param>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        string MakeSqlAlterChangeColumn(string tableName,string oldName,FaTableColumnEntity inEnt);

        /// <summary>
        /// MySQL 修改字段为不可重复
        /// ALTER TABLE dbname.table ADD UNIQUE (fieldname);
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        string MakeSqlAlterUniqueColumn(string tableName,FaTableColumnEntity inEnt);
    }
}
