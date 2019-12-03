
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

        public async Task<ResultObj<KTV>> GetTableSelect()
        {
            DapperHelper<FaTableTypeEntity> dbHelper = new DapperHelper<FaTableTypeEntity>();
            var reObj = new ResultObj<KTV>();
            var entList = await dbHelper.FindAll(x => x.STAUTS == "启用");
            reObj.dataList = entList.Select(i => new KTV() { K = i.ID.ToString(), V = i.NAME }).ToList();
            return reObj;
        }

        public async Task<ResultObj<int>> Save(DtoSave<FaTableTypeEntity> inEnt)
        {
            ResultObj<int> reObj = new ResultObj<int>();
            if (inEnt.data.AllColumns == null || inEnt.data.AllColumns.Count() == 0)
            {
                reObj.success = false;
                reObj.msg = "配置列不能为空";
                return reObj;
            }
            var dapper = new DapperHelper();
            dapper.TranscationBegin();

            DapperHelper<FaTableTypeEntity> dbHelper = new DapperHelper<FaTableTypeEntity>(dapper.GetConnection(), dapper.GetTransaction());

            FaTableTypeEntity oldEnt = new FaTableTypeEntity();
            try
            {
                bool isAdd = false;
                if (inEnt.data.ID == 0)
                {
                    isAdd = true;
                    inEnt.data.ID = await new SequenceRepository().GetNextID<FaTableTypeEntity>();
                    inEnt.data.ADD_TIME = DateTime.Now;
                    var opNum = await dbHelper.Save(inEnt);
                    reObj.success = opNum > 0;
                    reObj.msg = "添加成功";
                }
                else
                {
                    oldEnt = await this.SingleByKey(inEnt.data.ID);
                    var opNum = await dbHelper.Update(inEnt);
                    reObj.success = opNum > 0;
                    reObj.msg = "修改成功";

                    if (!oldEnt.TABLE_NAME.Equals(inEnt.data.TABLE_NAME))
                    {
                        var t = dapper.Exec(MakeSqlChangeTableName(oldEnt.TABLE_NAME, inEnt.data.TABLE_NAME));
                    }
                }
                DapperHelper<FaTableColumnEntity> dapperCol = new DapperHelper<FaTableColumnEntity>(dbHelper.GetConnection(), dbHelper.GetTransaction());


                foreach (var item in inEnt.data.AllColumns)
                {
                    if (isAdd || item.ID == 0) //如果是新增加，或列ID为空
                    {
                        item.TABLE_TYPE_ID = inEnt.data.ID;
                        item.ID = await new SequenceRepository().GetNextID<FaTableColumnEntity>();
                        var opNum = await dapperCol.Save(new DtoSave<FaTableColumnEntity>
                        {
                            data = item
                        });
                        if (opNum < 1)
                        {
                            LogHelper.WriteErrorLog(this.GetType(), "保存字段失败");
                            dapper.TranscationRollback();
                            reObj.success = false;
                            reObj.msg = "保存字段失败";
                            return reObj;
                        }
                        //如果是修改，才修改数据库
                        if (!isAdd)
                        {
                            //添加字段,线程添加
                            var t = dapper.Exec(MakeSqlAlterAddColumn(inEnt.data.TABLE_NAME, item));
                        }
                    }
                    else
                    {
                        var oldCol = oldEnt.AllColumns.Single(x => x.ID == item.ID);
                        if (
                            oldCol != null
                            && oldCol.COLUMN_NAME == item.COLUMN_NAME
                            && oldCol.NAME == item.NAME
                            && oldCol.COLUMN_TYPE == item.COLUMN_TYPE
                            && oldCol.COLUMN_LONG == item.COLUMN_LONG
                            )
                        {
                            continue;
                        }
                        int opNum = 0;
                        if (oldCol == null)
                        {
                            item.ID = await new SequenceRepository().GetNextID<FaTableColumnEntity>();
                            opNum = await dapperCol.Save(new DtoSave<FaTableColumnEntity>
                            {
                                data = item
                            });
                            //添加字段
                            var t = dapper.Exec(MakeSqlAlterAddColumn(inEnt.data.TABLE_NAME, item));
                        }
                        else
                        {
                            opNum = await dapperCol.Update(new DtoSave<FaTableColumnEntity>
                            {
                                data = item,
                                saveFieldList = dapperCol.modelHelper.GetDirct().Select(x => x.Key).ToList(),
                                ignoreFieldList = null
                            });
                            if (oldCol.COLUMN_NAME == item.COLUMN_NAME)
                            {
                                var t = dapper.Exec(MakeSqlAlterTable(inEnt.data.TABLE_NAME, item));
                            }
                            else
                            {
                                var t = dapper.Exec(MakeSqlAlterChangeColumn(inEnt.data.TABLE_NAME, oldCol.COLUMN_NAME, item));
                            }
                        }

                        if (opNum < 1)
                        {
                            LogHelper.WriteErrorLog(this.GetType(), "修改字段失败");
                            dbHelper.TranscationRollback();
                            reObj.success = false;
                            reObj.msg = "修改字段失败";
                            return reObj;
                        }
                    }
                }

                if (isAdd)
                {
                    string createSql = MakeSqlCreateTable(inEnt.data);
                    int opNum = await dapper.Exec(createSql);

                }

                dbHelper.TranscationCommit();
            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.msg = "保存自定义表失败" + e.Message;
                LogHelper.WriteErrorLog(this.GetType(), "保存自定义表失败", e);
                dbHelper.TranscationRollback();
            }
            return reObj;
        }

        public async Task<FaTableTypeEntity> SingleByKey(int key)
        {
            DapperHelper<FaTableTypeEntity> dbHelper = new DapperHelper<FaTableTypeEntity>();
            var ent = await dbHelper.SingleByKey(key);
            var allColumns = await new DapperHelper<FaTableColumnEntity>().FindAll(x => x.TABLE_TYPE_ID == key);
            ent.AllColumns = allColumns.ToList();
            return ent;
        }

        public async Task<ResultObj<int>> Delete(int key)
        {
            ResultObj<int> reObj = new ResultObj<int>();
            DapperHelper<FaTableTypeEntity> typeDapper = new DapperHelper<FaTableTypeEntity>();
            DapperHelper<FaTableColumnEntity> columnHelper = new DapperHelper<FaTableColumnEntity>(typeDapper.GetConnection(), typeDapper.GetTransaction());
            try
            {
                typeDapper.TranscationBegin();
                var opNum = await columnHelper.Delete(i => i.TABLE_TYPE_ID == key);
                if (opNum < 1)
                {
                    reObj.success = false;
                    reObj.msg = "删除表类型为空";
                    return reObj;
                }
                opNum = await typeDapper.Delete(i => i.ID == key);
                if (opNum != 1)
                {
                    reObj.success = false;
                    reObj.msg = "删除表类字段失败";
                    return reObj;
                }

                typeDapper.TranscationCommit();
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "删除表类型失败", e);
                typeDapper.TranscationRollback();
            }
            return reObj;
        }


        /// <summary>
        /// 生成创建表SQL
        /// </summary>
        /// <returns></returns>
        public string MakeSqlCreateTable(FaTableTypeEntity inEnt)
        {
            List<string> allColumns = new List<string>();
            foreach (var item in inEnt.AllColumns)
            {
                allColumns.Add(
                    string.Format("\r\n  {0} {1} {2} COMMENT '{3}'",
                        item.COLUMN_NAME,
                        GetTypeStr(item),
                        (item.IS_REQUIRED > 1) ? "not null" : "null",
                        item.NAME
                        )
                );
            }

            string reObj = @"
create table {0}(
   Id INT NOT NULL AUTO_INCREMENT,
{1},
   PRIMARY KEY ( Id )
);
            ";
            reObj = string.Format(reObj, inEnt.TABLE_NAME, string.Join(",", allColumns));
            return reObj;
        }


        /// <summary>
        /// 修改表名
        /// </summary>
        /// <param name="oldTableName"></param>
        /// <param name="nowTableName"></param>
        /// <returns></returns>
        public string MakeSqlChangeTableName(string oldTableName, string nowTableName)
        {

            string reObj = string.Format(
                "ALTER TABLE {0} RENAME TO {1};",
                oldTableName,
                nowTableName
                );
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
                (inEnt.IS_REQUIRED > 1) ? "not null" : "null",
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
                (inEnt.IS_REQUIRED > 1) ? "not null" : "null",
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
        public string MakeSqlAlterChangeColumn(string tableName, string oldName, FaTableColumnEntity inEnt)
        {
            string reObj = string.Format(
                "alter table {0}  change {5} {1} {2} {3} COMMENT '{4}';",
                tableName,
                inEnt.COLUMN_NAME,
                GetTypeStr(inEnt),
                (inEnt.IS_REQUIRED > 1) ? "not null" : "null",
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
                (inEnt.IS_REQUIRED > 1) ? "not null" : "null",
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
                    return string.Format("varchar({0}) CHARACTER SET utf8", inEnt.COLUMN_LONG);
                case "int":
                case "pic":
                    return string.Format("int", inEnt.COLUMN_LONG);
                case "datatime":
                    return string.Format("datatime", inEnt.COLUMN_LONG);
                default:
                    return string.Format("varchar({0}) CHARACTER SET utf8", inEnt.COLUMN_LONG);
            }
        }


    }
}