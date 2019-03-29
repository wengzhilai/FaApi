
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
    public class EquipmentRepository : IEquipmentRepository
    {
        DapperHelper<FaEquipmentEntity> dbHelper = new DapperHelper<FaEquipmentEntity>();
        public Task<FaEquipmentEntity> SingleByKey<t>(t key)
        {
            return dbHelper.SingleByKey(key);
        }
        public async Task<Result<int>> Delete(int key)
        {
            Result<int> reObj = new Result<int>();
            reObj.Data = await dbHelper.Delete(i => i.ID == key);
            reObj.IsSuccess = reObj.Data > 0;
            return reObj;
        }

        public async Task<Result<KTV>> GetTree(int? parentId)
        {
            var reObj = new Result<KTV>();
            var entList = await dbHelper.FindAll(i => i.PARENT_ID == parentId);
            reObj.DataList = entList.Select(i => new KTV() { K = i.ID.ToString(), V = i.NAME }).ToList();
            foreach (var item in reObj.DataList)
            {
                item.child = (await GetTree(Convert.ToInt32(item.K))).DataList;
            }
            return reObj;
        }

        public async Task<Result<int>> Save(DtoSave<FaEquipmentEntity> inEnt)
        {
            Result<int> reObj = new Result<int>();
            if (inEnt.Data.ID == 0)
            {
                inEnt.Data.ID = await new SequenceRepository().GetNextID<FaEquipmentEntity>();
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
            return reObj;
        }

        public async Task<Result> SaveEquiment(DtoEquipment inEnt)
        {
            var reObj = new Result();
            var tableType = await new TableRepository().SingleByKey(inEnt.TypeId);
            if (tableType == null)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "表不存在";
                return reObj;
            }
            string sql = string.Format("insert into {0}({1}) values(2) ",
            tableType.TABLE_NAME,
            string.Join(",", tableType.AllColumns.Select(i => i.COLUMN_NAME)),
            string.Join(",", tableType.AllColumns.Select(i => "@" + i.COLUMN_NAME))
            );
            var opNum = await DapperHelper.Exec(sql, TypeChange.JsonToObject<KeyValuePair<string, object>>(inEnt.DataStr));
            if (opNum != 1)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "保存失败";
                return reObj;
            }
            reObj.IsSuccess = true;
            reObj.Msg = "保存成功";
            return reObj;
        }

        public async Task<Result> DeleteEquiment(DtoEquipment inEnt)
        {
            var reObj = new Result();
            var tableType = await new TableRepository().SingleByKey(inEnt.TypeId);
            if (tableType == null)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "表不存在";
                return reObj;
            }
            string sql = string.Format("delete {0} where ID={1}",
            tableType.TABLE_NAME,
            inEnt.Id
            );
            var opNum = await DapperHelper.Exec(sql, TypeChange.JsonToObject<KeyValuePair<string, object>>(inEnt.DataStr));
            if (opNum != 1)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "删除失败";
                return reObj;
            }
            reObj.IsSuccess = true;
            reObj.Msg = "删除成功";
            return reObj;
        }

        public async Task<Result> UpdateEquiment(DtoEquipment inEnt)
        {
            var reObj = new Result();
            var tableType = await new TableRepository().SingleByKey(inEnt.TypeId);
            if (tableType == null)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "表不存在";
                return reObj;
            }
            string sql = string.Format("update {0} set {1} where ID={2}",
            tableType.TABLE_NAME,
            string.Join(",", tableType.AllColumns.Select(i => i.COLUMN_NAME + "=@" + i.COLUMN_NAME)),
            inEnt.Id
            );
            var opNum = await DapperHelper.Exec(sql, TypeChange.JsonToObject<KeyValuePair<string, object>>(inEnt.DataStr));
            if (opNum != 1)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "修改失败";
                return reObj;
            }
            reObj.IsSuccess = true;
            reObj.Msg = "修改成功";
            return reObj;
        }

        public async Task<Result<DataGridDataJson>> GetConfigAndData(QuerySearchModel inEnt)
        {
            var reObj = new Result<DataGridDataJson>();
            if (!inEnt.Code.IsInt32())
            {
                reObj.IsSuccess = false;
                reObj.Msg = "代码有误";
                return reObj;
            }
            var tableType = await new TableRepository().SingleByKey(Convert.ToInt32(inEnt.Code));
            if (tableType == null)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "表不存在";
                return reObj;
            }
            var bindEnt = new DataGridDataJson();
            bindEnt.Config = new FaQueryEntity();
            bindEnt.Config.HEARD_BTN = "[]";
            var allColumns = tableType.AllColumns.Select(x => new QueryCfg
            {
                FieldName = x.COLUMN_NAME,
                Alias = x.NAME,
                CanSearch = true,
                Show = true,
                Sortable = true,
                FieldType = x.COLUMN_TYPE
            });

            bindEnt.Config.QUERY_CFG_JSON = TypeChange.ObjectToStr(allColumns);
            bindEnt.Config.ROWS_BTN="[]";
            bindEnt.Config.SHOW_CHECKBOX=true;

            string sql=string.Format("select * from {0}",tableType.TABLE_NAME);

            var dal=new QueryRepository();
            string whereStr="";
            sql=dal.MakeSql(inEnt,sql,ref whereStr);
            sql = dal.MakePageSql(sql, inEnt.page, inEnt.rows, inEnt.OrderStr, whereStr);

            try
            {
                var sqlList = reObj.Msg.Split(';');
                if (sqlList.Count() > 0)
                {
                    bindEnt.rows = DapperHelper.GetDataTable(sqlList[0]);
                }

                if (sqlList.Count() > 1)
                {
                    int allNum = 0;
                    int.TryParse(await DapperHelper.ExecuteScalarAsync(sqlList[1]), out allNum);
                    bindEnt.total = allNum;
                }
                reObj.Data = bindEnt;
            }
            catch(Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(),"执行分页数据失败",e);
                return reObj;
            }
            return reObj;
        }
    }
}