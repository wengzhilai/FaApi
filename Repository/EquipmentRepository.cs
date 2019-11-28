
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
            reObj.data = await dbHelper.Delete(i => i.ID == key);
            reObj.success = reObj.data > 0;
            return reObj;
        }

        public async Task<Result<KTV>> GetTree(int? parentId)
        {
            var reObj = new Result<KTV>();
            var entList = await dbHelper.FindAll(i => i.PARENT_ID == parentId);
            reObj.dataList = entList.Select(i => new KTV() { K = i.ID.ToString(), V = i.NAME }).ToList();
            foreach (var item in reObj.dataList)
            {
                item.child = (await GetTree(Convert.ToInt32(item.K))).dataList;
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
                reObj.success = opNum > 0;
                reObj.msg = "添加成功";
            }
            else
            {
                var opNum = await dbHelper.Update(inEnt);
                reObj.success = opNum > 0;
                reObj.msg = "修改成功";
            }
            return reObj;
        }

        public async Task<Result<DataTable>> SingleEquiment(DtoEquipment inEnt)
        {
            var reObj = new Result<DataTable>();


            var tableType = await new TableRepository().SingleByKey(inEnt.TypeId);
            if (tableType == null)
            {
                reObj.success = false;
                reObj.msg = "表不存在";
                return reObj;
            }


            string sql = string.Format("select * from {0} where ID={1}", tableType.TABLE_NAME, inEnt.Id);

            try
            {
                reObj.data = DapperHelper.GetDataTable(sql);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取单条数据失败", e);
                return reObj;
            }
            return reObj;
        }

        public async Task<Result> SaveEquiment(DtoEquipment inEnt)
        {
            var reObj = new Result();

            var equType = await new EquipmentRepository().SingleByKey(inEnt.TypeId);
            if (equType == null)
            {
                reObj.success = false;
                reObj.msg = "设备类型ID有误";
                return reObj;
            }
            var tableType = await new TableRepository().SingleByKey(equType.TABLE_TYPE_ID);
            if (tableType == null)
            {
                reObj.success = false;
                reObj.msg = "表不存在";
                return reObj;
            }
            string sql = string.Format("insert into {0}({1}) values({2}) ",
            tableType.TABLE_NAME,
            string.Join(",", tableType.AllColumns.Select(i => i.COLUMN_NAME)),
            string.Join(",", tableType.AllColumns.Select(i => "@" + i.COLUMN_NAME))
            );
            var par = TypeChange.JsonToObject(inEnt.DataStr);
            DynamicParameters tmp = new DynamicParameters();
            foreach (var item in par)
            {
                tmp.Add(item.Key, item.Value.ToString());
            }
            var opNum = await DapperHelper.Exec(sql, tmp);
            if (opNum != 1)
            {
                reObj.success = false;
                reObj.msg = "保存失败";
                return reObj;
            }
            reObj.success = true;
            reObj.msg = "保存成功";
            return reObj;
        }

        public async Task<Result> DeleteEquiment(DtoEquipment inEnt)
        {
            var reObj = new Result();
            var equType = await new EquipmentRepository().SingleByKey(inEnt.TypeId);
            if (equType == null)
            {
                reObj.success = false;
                reObj.msg = "设备类型ID有误";
                return reObj;
            }
            var tableType = await new TableRepository().SingleByKey(equType.TABLE_TYPE_ID);
            if (tableType == null)
            {
                reObj.success = false;
                reObj.msg = "表不存在";
                return reObj;
            }
            string sql = string.Format("delete from {0} where Id=@Id",
            tableType.TABLE_NAME,
            inEnt.Id
            );
            DynamicParameters tmp = new DynamicParameters();
            tmp.Add("Id", inEnt.Id);
            var opNum = await DapperHelper.Exec(sql, tmp);
            if (opNum != 1)
            {
                reObj.success = false;
                reObj.msg = "删除失败";
                return reObj;
            }
            reObj.success = true;
            reObj.msg = "删除成功";
            return reObj;
        }

        public async Task<Result> UpdateEquiment(DtoEquipment inEnt)
        {
            var reObj = new Result();
            var equType = await new EquipmentRepository().SingleByKey(inEnt.TypeId);
            if (equType == null)
            {
                reObj.success = false;
                reObj.msg = "设备类型ID有误";
                return reObj;
            }
            var tableType = await new TableRepository().SingleByKey(equType.TABLE_TYPE_ID);
            if (tableType == null)
            {
                reObj.success = false;
                reObj.msg = "表不存在";
                return reObj;
            }
            string sql = string.Format("update {0} set {1} where ID={2}",
            tableType.TABLE_NAME,
            string.Join(",", tableType.AllColumns.Select(i => i.COLUMN_NAME + "=@" + i.COLUMN_NAME)),
            inEnt.Id
            );

            var par = TypeChange.JsonToObject(inEnt.DataStr);
            DynamicParameters tmp = new DynamicParameters();
            foreach (var item in par)
            {
                tmp.Add(item.Key, item.Value.ToString());
            }
            var opNum = await DapperHelper.Exec(sql, tmp);
            if (opNum != 1)
            {
                reObj.success = false;
                reObj.msg = "修改失败";
                return reObj;
            }
            reObj.success = true;
            reObj.msg = "修改成功";
            return reObj;
        }

        public async Task<Result<DataGridDataJson>> GetData(QuerySearchModel inEnt)
        {
            var reObj = new Result<DataGridDataJson>();
            if (!inEnt.code.IsInt32())
            {
                reObj.success = false;
                reObj.msg = "代码有误";
                return reObj;
            }
            var equType = await new EquipmentRepository().SingleByKey(Convert.ToInt32(inEnt.code));
            if (equType == null)
            {
                reObj.success = false;
                reObj.msg = "设备类型ID有误";
                return reObj;
            }
            var tableType = await new TableRepository().SingleByKey(equType.TABLE_TYPE_ID);
            if (tableType == null)
            {
                reObj.success = false;
                reObj.msg = "表不存在";
                return reObj;
            }
            var bindEnt = new DataGridDataJson();


            string sql = string.Format("select * from {0}", tableType.TABLE_NAME);

            var dal = new QueryRepository();
            string whereStr = "";
            sql = dal.MakeSql(inEnt, sql, ref whereStr);
            sql = dal.MakePageSql(sql, inEnt.page, inEnt.rows, inEnt.orderStr, whereStr);
            try
            {
                var sqlList = sql.Split(';');
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
                reObj.data = bindEnt;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "执行分页数据失败", e);
                return reObj;
            }
            return reObj;
        }

        public async Task<Result<SmartTableSetting>> GetConfig(DtoDo<int> inEnt)
        {
            var reObj = new Result<SmartTableSetting>();

            var equType = await new EquipmentRepository().SingleByKey(inEnt.Key);
            if (equType == null)
            {
                reObj.success = false;
                reObj.msg = "设备类型ID有误";
                return reObj;
            }
            var tableType = await new TableRepository().SingleByKey(equType.TABLE_TYPE_ID);
            if (tableType == null)
            {
                reObj.success = false;
                reObj.msg = "表不存在";
                return reObj;
            }
            var setting = new SmartTableSetting();
            setting.HEARD_BTN = "[]";
            var allColumns = tableType.AllColumns.Select(x => new SmartTableColumnSetting
            {
                ColumnName = x.COLUMN_NAME,
                title = x.NAME,
                editable = true,
                filter = true,
                Show = true,
                sort = true,
                type = x.COLUMN_TYPE
            }).ToList();

            setting.ColumnsList = allColumns;
            setting.ROWS_BTN = "[]";
            setting.SHOW_CHECKBOX = true;
            reObj.data = setting;
            return reObj;
        }
    }
}