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
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Result<fa_user> UserLogin(string username,string password);

    }
}
