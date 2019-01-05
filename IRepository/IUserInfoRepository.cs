

using System.Collections.Generic;
using Models.Entity;

namespace IRepository
{
    public interface IUserInfoRepository 
    {
        /// <summary>
        /// 单个查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        FaUserInfoEntity SingleByKey(int key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        List<FaUserInfoEntity> FindAll(object inParm = null);

    }
}
