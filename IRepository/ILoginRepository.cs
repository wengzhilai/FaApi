

using System.Collections.Generic;
using Models.Entity;

namespace IRepository
{
    public interface ILoginRepository 
    {
        FaLoginEntity SingleByKey(int key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        List<FaLoginEntity> FindAll(object inParm = null);

    }
}
