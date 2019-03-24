
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
    }
}