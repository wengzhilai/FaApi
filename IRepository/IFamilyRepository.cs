
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        List<FaFamilyEntity> FindAll(Expression<Func<FaFamilyEntity, bool>> inParm = null);

    }
}
