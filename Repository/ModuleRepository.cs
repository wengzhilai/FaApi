
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
    public class ModuleRepository : IModuleRepository
    {
        DapperHelper<FaModuleEntity> dbHelper = new DapperHelper<FaModuleEntity>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<FaModuleEntity> SingleByKey(int key)
        {
            return dbHelper.SingleByKey(key);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public async Task<Result<KTV>> GetMenu(Expression<Func<FaModuleEntity, bool>> where)
        {
            Result<KTV> reObj = new Result<KTV>();
            var allModel = await dbHelper.FindAll(where);
            reObj.DataList = GetChildItems(allModel, null);
            return reObj;
        }

        private List<KTV> GetChildItems(IEnumerable<FaModuleEntity> inList, int? parentId)
        {
            var childList = inList.Where(i => i.PARENT_ID == parentId).ToList();
            List<KTV> reObj = new List<KTV>();
            foreach (var item in childList)
            {
                var addItem = new KTV();
                addItem.K = item.ID.ToString();
                addItem.V = item.NAME;
                addItem.T = item.LOCATION;
                addItem.child = GetChildItems(inList, item.ID);
                reObj.Add(addItem);
            }
            return reObj;
        }

        public async Task<Result<int>> Delete(int key)
        {
            Result<int> reObj = new Result<int>();
            reObj.Data = await dbHelper.Delete(i => i.ID == key);
            reObj.IsSuccess = reObj.Data > 0;
            return reObj;
        }

        public async Task<Result<int>> Save(DtoSave<FaModuleEntity> inEnt)
        {
            Result<int> reObj = new Result<int>();
            if (inEnt.Data.ID == 0)
            {
                inEnt.Data.ID = await new SequenceRepository().GetNextID<FaModuleEntity>();
                reObj.Data = await dbHelper.Save(inEnt);
            }
            else
            {
                reObj.Data = await dbHelper.Update(inEnt);
            }

            reObj.IsSuccess = reObj.Data > 0;
            return reObj;
        }

        public async Task<Result<KTV>> GetMenuByRoleId(List<int> roleIdList)
        {
            Result<KTV> reObj = new Result<KTV>();
            if (!roleIdList.Contains(1))
            {
                DapperHelper<FaRoleModuleEntityView> roleModule = new DapperHelper<FaRoleModuleEntityView>();
                var allModel = await roleModule.FindAll("");
                allModel = await roleModule.FindAll(string.Format("a.ROLE_ID in ({0})", string.Join(",", roleIdList)));
                reObj.DataList = GetChildItems(Fun.ClassListToCopy<FaRoleModuleEntityView, FaModuleEntity>(allModel.ToList()), null);
            }
            else
            {
                reObj.DataList = GetChildItems(await new DapperHelper<FaModuleEntity>().FindAll(""), null);
            }
            return reObj;
        }

        public async Task<Result<KTV>> GetMGetMenuByUserId(int userId)
        {

            DapperHelper<FaUserRoleEntityView> userRole = new DapperHelper<FaUserRoleEntityView>();
            var allRole = await userRole.FindAll(i => i.USER_ID == userId);
            return await GetMenuByRoleId(allRole.Select(i => i.ROLE_ID).ToList());
        }
    }
}
