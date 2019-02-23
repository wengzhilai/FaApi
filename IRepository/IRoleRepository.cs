

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;
using Models.Entity;

namespace IRepository
{
    public interface IRoleRepository
    {
        Task<FaRoleEntity> SingleByKey(int key);
        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        Task<IEnumerable<FaRoleEntity>> FindAll(Expression<Func<FaRoleEntity, bool>> inParm = null);

        /// <summary>
        /// 查找用户的所有角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<FaUserRoleEntityView>> UserRoleList(int userId);

        /// <summary>
        ///   检测用户是否有权限
        ///  权限字符串，第一位表示创建者，第二位管理员，第三位表示超级管理员
        ///  判断的权限，1添加，2修改，4查看
        /// </summary>
         /// <returns></returns>
        Task<bool> CheckAuth(CheckAuthDto inEnt);

    }
}
