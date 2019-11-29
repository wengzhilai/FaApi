
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
    public interface IUserInfoRepository
    {
        /// <summary>
        /// 单个查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<FaUserInfoEntityView> SingleByKey(int key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        Task<IEnumerable<FaUserInfoEntityView>> FindAll(Expression<Func<FaUserInfoEntityView, bool>> inParm = null);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<IEnumerable<FaUserInfoEntityView>> List(DtoSearch<FaUserInfoEntityView> inEnt);

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<Result> RegUserInfo(RegUserInfo inEnt);

        /// <summary>
        /// 保存用户基本信息
        /// </summary>
        /// <param name="inEnt"></param>
        /// <param name="opUserName"></param>
        /// <param name="opUserId"></param>
        /// <returns></returns>
        Task<ResultObj<bool>> Save(DtoSave<FaUserInfoEntityView> inEnt, string opUserName, int opUserId);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Result> Delete(int userId);
        /// <summary>
        /// 获取可以编辑的用户ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<int>> GetCanEditUserIdListAsync(int userId);

        /// <summary>
        /// 获取指定ElderId的用户ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="elderId"></param>
        /// <returns></returns>
        Task<int> GetUserIdByElderAsync(int userId,int elderId);



    }
}
