
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

        /// <summary>
        /// 验证是否有效,验证码是否有效
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<int> Count(string phone, string code);
    }
}
