using System;
using System.Linq;
using System.Threading.Tasks;

using IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Repository;

namespace ApiFamily.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FamilyBooksController : ControllerBase
    {

        /// <summary>
        /// 
        /// </summary>
        public FamilyBooksController(
            )
        {
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<FaFamilyBooksEntity>> Single(DtoDo<int> inEnt)
        {
            ResultObj<FaFamilyBooksEntity> reObj = new ResultObj<FaFamilyBooksEntity>();
            try
            {
                DapperHelper<FaFamilyBooksEntity> dapp = new DapperHelper<FaFamilyBooksEntity>();
                var ent = await dapp.SingleByKey(inEnt.Key);
                reObj.data = ent;
                reObj.success = true;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取详情失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        async public Task<Result> Save(DtoSave<FaFamilyBooksEntity> inEnt)
        {
            var reObj = new ResultObj<int>();
            try
            {
                DapperHelper<FaFamilyBooksEntity> dapp = new DapperHelper<FaFamilyBooksEntity>();
                if (inEnt.data.id == 0)
                {
                    inEnt.data.id = await new SequenceRepository().GetNextID<FaFamilyBooksEntity>();
                }
                reObj.success = await dapp.Save(inEnt) > 0;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "修改角色信息失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        async public Task<Result> Delete(DtoDo<int> inEnt)
        {
            var reObj = new Result();
            try
            {
                DapperHelper<FaFamilyBooksEntity> dapp = new DapperHelper<FaFamilyBooksEntity>();
                reObj.success = await dapp.Delete(x => x.id == inEnt.Key) > 0;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户详情失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取下一个关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<FaFamilyBooksEntity>> GetSingleNext(DtoDo<int> inObj)
        {
            ResultObj<FaFamilyBooksEntity> reObj = new ResultObj<FaFamilyBooksEntity>();
            try
            {
                DapperHelper<FaFamilyBooksEntity> dapp = new DapperHelper<FaFamilyBooksEntity>();
                var single =await dapp.Single(x=>x.sort>inObj.Key,"order by SORT");
                reObj.data = single;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取下一个关系图失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取所有家谱
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResultObj<FaFamilyBooksEntity>> GetAllBooks()
        {
            ResultObj<FaFamilyBooksEntity> reObj = new ResultObj<FaFamilyBooksEntity>();
            try
            {
                DapperHelper<FaFamilyBooksEntity> dapp = new DapperHelper<FaFamilyBooksEntity>();
                var allUser =await dapp.FindAll("");
                allUser=allUser.OrderBy(i=>i.sort);
                reObj.dataList = allUser.ToList();
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取下一个关系图失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取上一个关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<FaFamilyBooksEntity>> GetSinglePrev(DtoDo<int> inObj)
        {
            ResultObj<FaFamilyBooksEntity> reObj = new ResultObj<FaFamilyBooksEntity>();
            try
            {
                DapperHelper<FaFamilyBooksEntity> dapp = new DapperHelper<FaFamilyBooksEntity>();
                var single =await dapp.Single(x=>x.sort<inObj.Key,"order by SORT desc");
                reObj.data = single;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取上一个关系图失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取指定页码
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<FaFamilyBooksEntity>> GetSingleBySort(DtoDo<int> inObj)
        {
            ResultObj<FaFamilyBooksEntity> reObj = new ResultObj<FaFamilyBooksEntity>();
            try
            {
                DapperHelper<FaFamilyBooksEntity> dapp = new DapperHelper<FaFamilyBooksEntity>();
                var single =await dapp.Single(x=>x.sort==inObj.Key);
                reObj.data = single;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取指定页码", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }
    }
}