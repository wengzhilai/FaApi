using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

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
        List<User> UserLogin(string username,string password);

    }
}
