
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Models;
using Models.Entity;
using Models.EntityView;

namespace IRepository
{
    public interface IFamilyRepository
    {
        void SetMapper(IMapper mapper);
        Task<FaFamilyEntity> SingleByKey(int key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        Task<IEnumerable<FaFamilyEntity>> FindAll(Expression<Func<FaFamilyEntity, bool>> inParm = null);
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<Result<Relative>> Relative(DtoDo<int> inObj);

        /// <summary>
        /// 获取用户的家谱成员
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="targerElderId">查找辈字用户，如果为0表示取当前用户</param>
        /// <returns></returns>
        Task<Result<FaElderEntity>> GetUserBooksAsync(int userId,int targerId=24);

        /// <summary>
        /// 获取用户的父节点
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="parentNum">最大层级数</param>
        /// <returns></returns>
        Task<List<KV>> GetUserTreeAsync(int userId,int parentNum);
    }
}
