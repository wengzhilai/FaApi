

using System.Collections.Generic;
using Models.Entity;

namespace IRepository
{
    public interface IFamilyRepository 
    {
        FaFamilyEntity SingleByKey(int key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        List<FaFamilyEntity> FindAll(object inParm = null);

    }
}
