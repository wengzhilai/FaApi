using Models;
using Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFamily.Interface
{
 
    /// <summary>
    /// 
    /// </summary>
    public interface IFamily
    {
        /// <summary>
        /// 获取关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<Relative>> Relative(DtoDo<int> inObj);

        /// <summary>
        /// 获取关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<FaElderEntity>> GetUserBooks(DtoDo<int> inObj);

        /// <summary>
        /// 生成DOC文档
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<Result> MakeUserBooks(DtoDo<int> inObj);
    }
}
