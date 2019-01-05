using System;
using System.Collections.Generic;
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
        FaUserEntity SingleByKey(int key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        List<FaUserEntity> FindAll(object inParm = null);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Result<FaUserEntity> UserLogin(string username,string password);

    }
}
