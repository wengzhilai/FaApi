
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;
using Models.Entity;

namespace IRepository
{
    public interface ITaskRepository
    {
        /// <summary>
        /// 获取下一步的可操作的信息，默认第一步是1，最后一步的ID是999
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="fromNodeId">开始节点1表示开始</param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        Task<Result<TaskNode>> GetNextNode(int flowId, int fromNodeId = 1, int? toNodeId = null);

        /// <summary>
        /// 检测是用户是否允许操作下一步
        /// </summary>
        /// <param name="inNode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Result> CheckIsAllowHandNext(TaskNode inNode, int userId);


        Task<Result> CreateTask();


        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="t"></typeparam>
        /// <returns></returns>
        Task<FaTaskEntity> SingleByKey<t>(t key);

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        Task<IEnumerable<FaTaskEntity>> FindAll(Expression<Func<FaTaskEntity, bool>> inParm = null);

    }
}