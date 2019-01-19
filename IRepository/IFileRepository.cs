
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models.Entity;

namespace IRepository
{
    public interface IFileRepository 
    {
        Task<FaFilesEntity> SingleByKey(int key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        Task<IEnumerable<FaFilesEntity>> FindAll(Expression<Func<FaFilesEntity, bool>> inParm = null);

    }
}
