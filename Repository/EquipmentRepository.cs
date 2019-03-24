
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
                item.child =(await GetTree(Convert.ToInt32(item.K))).DataList;
            }
            return reObj;
        }

        public async Task<Result<bool>> Save(DtoSave<FaEquipmentEntity> inEnt)
        {
            Result<bool> reObj = new Result<bool>();
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


    }
}