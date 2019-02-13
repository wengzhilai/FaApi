
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;
using Models.Entity;
using Models.EntityView;

namespace IRepository
{
    public interface IUserInfoRepository
    {
        /// <summary>
        /// 单个查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<FaUserInfoEntityView> SingleByKey(int key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        Task<IEnumerable<FaUserInfoEntityView>> FindAll(Expression<Func<FaUserInfoEntityView, bool>> inParm = null);

        Task<IEnumerable<FaUserInfoEntityView>> List(DtoSearch<FaUserInfoEntityView> inEnt);
    }
}
