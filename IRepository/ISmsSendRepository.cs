
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models.Entity;

namespace IRepository
{
    public interface ISmsSendRepository 
    {
        Task<FaSmsSendEntity> SingleByKey<t>(t key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        Task<IEnumerable<FaSmsSendEntity>> FindAll(Expression<Func<FaSmsSendEntity, bool>> inParm = null);

    }
}
