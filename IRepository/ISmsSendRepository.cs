

using System.Collections.Generic;
using Models.Entity;

namespace IRepository
{
    public interface ISmsSendRepository 
    {
        FaSmsSendEntity SingleByKey<t>(t key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        List<FaSmsSendEntity> FindAll(object inParm = null);

    }
}
