
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
    public class RoleRepository : IRoleRepository
    {
        DapperHelper<FaRoleEntity> dbHelper = new DapperHelper<FaRoleEntity>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<FaRoleEntity> SingleByKey(int key)
        {
            return dbHelper.SingleByKey(key);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public Task<IEnumerable<FaRoleEntity>> FindAll(Expression<Func<FaRoleEntity, bool>> where)
        {
            return dbHelper.FindAll(where);
        }

        async public Task<List<FaUserRoleEntityView>> UserRoleList(int userId)
        {
            DapperHelper<FaUserRoleEntityView> dp = new DapperHelper<FaUserRoleEntityView>();
            var reList = await dp.FindAll(x => x.USER_ID == userId);
            return reList.ToList();
        }

        /// <summary>
        ///  权限字符串，第一位表示创建者，第二位管理员，第三位表示超级管理员
        ///  判断的权限，1添加，2修改，4查看
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="authority"></param>
        /// <param name="prowNum">验证的权限，1添加，2修改，4查看</param>
        /// <param name="isCreater">是否是创建者</param>
        /// <returns></returns>
        async public Task<bool> CheckAuth(CheckAuthDto inEnt)
        {

            if (string.IsNullOrEmpty(inEnt.Authority)) return true;
            if (!inEnt.Authority.IsOnlyNumber()) return true;
            //是创建者
            if (inEnt.IsCreater)
            {
                if (Fun.GetPowerList(inEnt.Authority.Substring(0, 1)).Contains(inEnt.PowerNum)) return true;
            }

            var allRole = await UserRoleList(inEnt.UserId);
            //是普通管理员
            if (allRole.SingleOrDefault(i => i.ROLE_ID == 2) != null && inEnt.Authority.Length > 1)
            {
                if (Fun.GetPowerList(inEnt.Authority.Substring(1, 1)).Contains(inEnt.PowerNum)) return true;
            }

            //是超级管理员
            if (allRole.SingleOrDefault(i => i.ROLE_ID == 1) != null && inEnt.Authority.Length > 2)
            {
                if (Fun.GetPowerList(inEnt.Authority.Substring(2, 1)).Contains(inEnt.PowerNum)) return true;
            }
            return true;
        }
    }
}
