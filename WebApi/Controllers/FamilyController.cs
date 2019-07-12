using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Comon;
using IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models.Entity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Models.EntityView;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebApi.Controllers
{
    /// <summary>
    /// 关系
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FamilyController : ControllerBase
    {
        private readonly IHostingEnvironment env;
        IConfiguration config;
        IUserInfoRepository userInfo;
        private IMapper mapper;
        IUserRepository user;
        IFamilyRepository family;
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="_config"></param>
        /// <param name="_userInfo"></param>
        /// <param name="_mapper"></param>
        /// <param name="_family"></param>
        /// <param name="_user"></param>
        /// <param name="_env"></param>
        public FamilyController(
            IConfiguration _config,
            IUserInfoRepository _userInfo,
            IMapper _mapper,
            IFamilyRepository _family,
            IHostingEnvironment _env,
             IUserRepository _user)
        {
            config = _config;
            userInfo = _userInfo;
            mapper = _mapper;
            user = _user;
            family = _family;
            env = _env;

            family.SetMapper(mapper);
        }
        /// <summary>
        /// 获取关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result<Relative>> Relative(DtoDo<int> inObj)
        {
            Result<Relative> reObj = new Result<Relative>();
            try
            {
                reObj = await family.Relative(inObj);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户获取关系图失败", e);
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<FaElderEntity>> GetUserBooks(DtoDo<int> inObj)
        {
            Result<FaElderEntity> reObj = new Result<FaElderEntity>();
            try
            {
                reObj = await family.GetUserBooksAsync(inObj.Key);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户获取前谱失败", e);
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 生成DOC文档
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result> MakeUserBooks(DtoDo<int> inObj)
        {
            Result reObj = new Result();
            try
            {
                var tmp = await family.GetUserBooksAsync(inObj.Key);
                var allPath = Path.Combine(env.ContentRootPath, "..\\Doc\\Family.docx");
                WordHelper word = new WordHelper();
                var doc = word.MakeXWPFDocument(allPath);

                for (int i = 0; i < tmp.DataList.Count; i++)
                {
                    if (i >= 5) break;
                    var item = tmp.DataList[i];
                    var cell = doc.Tables[1].Rows[0].GetCell(0).Tables[0].Rows[i + 1].GetCell(0);
                    // word.AddElder(cell, "第" + item.NAME + "世");
                    int clm=0;
                    foreach (var user in item.AllUser)
                    {
                        if(string.IsNullOrEmpty(user.MsgFormat))continue;
                        word.AddName(cell, user.NAME);
                        word.AddRemark(cell, Helper.Fun.FormatNumToChinese(user.MsgFormat));
                        clm+=user.MsgFormat.Length/8;
                        clm+=(user.MsgFormat.Length%8 ==0)?1:2;
                        if(clm>11){
                            cell = doc.Tables[0].Rows[0].GetCell(1).Tables[0].Rows[i + 1].GetCell(0);
                        }
                    }
                }
                word.SaveDoc(doc, allPath);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户获取前谱失败", e);
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;
        }



    }
}