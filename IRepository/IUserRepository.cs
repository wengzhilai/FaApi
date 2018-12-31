using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace IRepository
{
    public interface IUserRepository 
    {
        List<User> UserLogin();

    }
}
