
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models.Entity;

namespace IRepository
{
    public interface IFileRepository 
    {
        FaFilesEntity SingleByKey(int key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        List<FaFilesEntity> FindAll(Expression<Func<FaFilesEntity, bool>> inParm = null);

    }
}
