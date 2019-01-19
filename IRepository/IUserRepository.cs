
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;
using Models.Entity;

namespace IRepository
{
    public interface IUserRepository 
    {
        /// <summary>
        /// 单个查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<FaUserEntity> SingleByKey(int key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        Task<IEnumerable<FaUserEntity>> FindAll(Expression<Func<FaUserEntity, bool>> inParm = null);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<Result<FaUserEntity>> UserLogin(string username,string password);

    }
}
