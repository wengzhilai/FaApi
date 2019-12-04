using Helper.Query.Dto;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helper.Query
{
    public interface IQuery
    {


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="querySearchModel"></param>
        /// <returns></returns>
        Task<ResultObj<Dictionary<String, Object>>> getListData(SearchDto inObj);

        /// <summary>
        /// 生成Query配置
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<Dictionary<string, Dictionary<string, object>>>> makeQueryCfg(DtoKey inObj);

        /// <summary>
        /// 查询单个Query
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<QueryEntity>> singleByKey(DtoDo<int> inObj);


        /// <summary>
        /// 根据代码查询Query
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<QueryEntity>> getSingleQuery(DtoKey inObj);

        /// <summary>
        /// 保存Query
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<int>> save(DtoSave<QueryEntity> inObj);

        /// <summary>
        /// 删除Query
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<int>> delete(DtoDo<int> inObj);

    }
}
