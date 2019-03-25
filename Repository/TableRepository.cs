
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helper;
using IRepository;
using Models;
using System.Linq;
using Models.Entity;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using System.Linq.Expressions;

namespace Repository
{
    public class TableRepository : ITableRepository
    {
        DapperHelper<FaTableTypeEntity> dbHelper = new DapperHelper<FaTableTypeEntity>();

        public async Task<Result<KTV>> GetTableSelect()
        {
            var reObj = new Result<KTV>();
            var entList = await dbHelper.FindAll(x => x.STAUTS == "启用");
            reObj.DataList = entList.Select(i => new KTV() { K = i.ID.ToString(), V = i.NAME }).ToList();
            return reObj;
        }

        public async Task<Result<bool>> Save(DtoSave<FaTableTypeEntity> inEnt)
        {
            Result<bool> reObj = new Result<bool>();
            try
            {
                dbHelper.TranscationBegin();
                if (inEnt.Data.ID == 0)
                {
                    inEnt.Data.ID = await new SequenceRepository().GetNextID<FaTableTypeEntity>();
                    var opNum = await dbHelper.Save(inEnt);
                    reObj.IsSuccess = opNum > 0;
                    reObj.Msg = "添加成功";
                }
                else
                {
                    var opNum = await dbHelper.Update(inEnt);
                    reObj.IsSuccess = opNum > 0;
                    reObj.Msg = "修改成功";
                }
                DapperHelper<FaTableColumnEntity> dapperCol = new DapperHelper<FaTableColumnEntity>(dbHelper.GetConnection(), dbHelper.GetTransaction());
                foreach (var item in inEnt.Data.AllColumns)
                {
                    if (item.ID == 0)
                    {
                        item.ID = await new SequenceRepository().GetNextID<FaTableColumnEntity>();
                        var opNum = await dapperCol.Save(new DtoSave<FaTableColumnEntity>
                        {
                            Data = item
                        });
                        if (opNum < 1)
                        {
                            LogHelper.WriteErrorLog(this.GetType(), "保存字段失败");
                            dbHelper.TranscationRollback();
                        }
                    }
                    else
                    {
                        var opNum = await dapperCol.Update(new DtoSave<FaTableColumnEntity>
                        {
                            Data = item,
                            SaveFieldList = dapperCol.modelHelper.GetDirct().Select(x => x.Key).ToList(),
                            IgnoreFieldList = null
                        });
                        if (opNum < 1)
                        {
                            LogHelper.WriteErrorLog(this.GetType(), "修改字段失败");
                            dbHelper.TranscationRollback();
                        }
                    }
                }

                dbHelper.TranscationCommit();
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "保存自定义表失败", e);
                dbHelper.TranscationRollback();
            }
            return reObj;
        }

        public async Task<FaTableTypeEntity> SingleByKey(int key)
        {
            var ent = await dbHelper.SingleByKey(key);
            var allColumns = await new DapperHelper<FaTableColumnEntity>().FindAll(x => x.TABLE_TYPE_ID == key);
            ent.AllColumns = allColumns.ToList();
            return ent;
        }


        /// <summary>
        /// 生成创建表SQL
        /// </summary>
        /// <returns></returns>
        public string MakeSqlCreateTable(FaTableTypeEntity inEnt)
        {
            string reObj = "";
            return reObj;
        }

        /// <summary>
        /// 修改字段类型和注释
        /// alter table {0}  modify column description varchar(255) null COMMENT '应用描述';
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public string MakeSqlAlterTable(string tableName, FaTableColumnEntity inEnt)
        {

            string reObj = string.Format(
                "alter table {0}  modify column {1} {2} {3} COMMENT '{4}';",
                tableName,
                inEnt.COLUMN_NAME,
                GetTypeStr(inEnt),
                (inEnt.IS_REQUIRED>1)?"not null":"null",
                inEnt.NAME
                );
            return reObj;
        }

        /// <summary>
        /// 生成添加字段字段
        /// alert table sys_application add `url` varchar(255) not null comment '应用访问地址';  
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public string MakeSqlAlterAddColumn(string tableName, FaTableColumnEntity inEnt)
        {
            string reObj = string.Format(
                "alter table {0}  add {1} {2} {3} COMMENT '{4}';",
                tableName,
                inEnt.COLUMN_NAME,
                GetTypeStr(inEnt),
                (inEnt.IS_REQUIRED>1)?"not null":"null",
                inEnt.NAME
                );
            return reObj;
        }

        /// <summary>
        /// 修改字段名字
        /// alter table {0} change name app_name varchar(255) not null comment '应用访问地址'; 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="oldName"></param>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public string MakeSqlAlterChangeColumn(string tableName,string oldName, FaTableColumnEntity inEnt)
        {
            string reObj = string.Format(
                "alter table {0}  change {5} {1} {2} {3} COMMENT '{4}';",
                tableName,
                inEnt.COLUMN_NAME,
                GetTypeStr(inEnt),
                (inEnt.IS_REQUIRED>1)?"not null":"null",
                inEnt.NAME,
                oldName
                );
            return reObj;
        }

        /// <summary>
        /// MySQL 修改字段为不可重复
        /// ALTER TABLE dbname.table ADD UNIQUE (fieldname);
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public string MakeSqlAlterUniqueColumn(string tableName, FaTableColumnEntity inEnt)
        {
            string reObj = string.Format(
                "alter table {0}  add UNIQUE ({1})",
                tableName,
                inEnt.COLUMN_NAME,
                GetTypeStr(inEnt),
                (inEnt.IS_REQUIRED>1)?"not null":"null",
                inEnt.NAME
                );
            return reObj;
        }

        public string GetTypeStr(FaTableColumnEntity inEnt)
        {
            //text,int,datatime,pic,textarea,Checkbox,Radio,auto
            if (inEnt.COLUMN_LONG == 0) inEnt.COLUMN_LONG = 50;
            switch (inEnt.COLUMN_TYPE)
            {
                case "text":
                case "textarea":
                case "Checkbox":
                case "Radio":
                case "auto":
                    return string.Format("varchar({0})", inEnt.COLUMN_LONG);
                case "int":
                case "pic":
                    return string.Format("int", inEnt.COLUMN_LONG);
                default:
                    return string.Format("varchar({0})", inEnt.COLUMN_LONG);
            }
        }
    }
}