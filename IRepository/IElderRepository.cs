

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models.Entity;

namespace IRepository
{
    public interface IElderRepository 
    {
        FaElderEntity SingleByKey(int key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        List<FaElderEntity> FindAll(Expression<Func<FaElderEntity, bool>> inParm = null);

    }
}
