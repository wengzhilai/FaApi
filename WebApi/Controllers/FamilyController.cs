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
            // try
            // {
            // var allUser = await family.GetUserBooks();
            // foreach (var userItem in allUser)
            // {
            //     var tmp = await family.GetUserBooksAsync(userItem.UserID.Value, 0);
                var tmp = await family.GetUserBooksAsync(inObj.Key, 0);
                var userId = Convert.ToInt32(tmp.Tmp);
                //获取父节点数
                var parentList = await family.GetUserTreeAsync(userId, 6);
                parentList.Reverse();
                var allPath = Path.Combine(env.ContentRootPath, "../Doc/Family.docx");
                WordHelper word = new WordHelper();
                WordHelper word1 = new WordHelper();
                var doc = word.MakeXWPFDocument(allPath);
                var doc1 = word1.MakeXWPFDocument(allPath);
                //标题
                var cellTitle = doc.Tables[1].Rows[0].GetCell(0).Tables[0].Rows[0].GetCell(0);
                word.AddNavigation(cellTitle, string.Join("----", parentList.Select(x => x.V.Substring(1))));

                #region 设置页码
                {
                    var cell1 = doc.Tables[1].Rows[3].GetCell(1);
                    var cell2 = doc.Tables[0].Rows[3].GetCell(0);
                    var cell3 = doc1.Tables[1].Rows[3].GetCell(1);
                    var cell4 = doc1.Tables[0].Rows[3].GetCell(0);
                    word.AddPageNum(cell1, tmp.Code);
                    word.AddPageNum(cell2, tmp.Code);
                    word1.AddPageNum(cell3, tmp.Code + "-1");
                    word1.AddPageNum(cell4, tmp.Code + "-1");
                }
                #endregion
                int elder = Convert.ToInt32(tmp.Code);
                bool isSec = false;
                for (int i = 0; i < tmp.DataList.Count; i++)
                {
                    if (i >= 5) break;
                    var item = tmp.DataList[i];
                    var cell1 = doc.Tables[1].Rows[0].GetCell(0).Tables[0].Rows[i + 1].GetCell(0);
                    var cell2 = doc.Tables[0].Rows[0].GetCell(1).Tables[0].Rows[i + 1].GetCell(0);
                    var cell3 = doc1.Tables[1].Rows[0].GetCell(0).Tables[0].Rows[i + 1].GetCell(0);
                    var cell4 = doc1.Tables[0].Rows[0].GetCell(1).Tables[0].Rows[i + 1].GetCell(0);
                    var cell = cell1;
                    int clm = 0;
                    if (elder < 100)
                    {
                        word.AddElder(cell, "第" + Helper.Fun.NumberToChinese(item.ID) + "世");
                        clm++;
                    }
                    foreach (var user in item.AllUser)
                    {
                        if (string.IsNullOrEmpty(user.MsgFormat)) continue;
                        if (clm >= 26) cell = cell3;
                        else if (clm >= 13) cell = cell2;
                        word.AddName(cell, user.NAME);
                        //判断加了姓名是否该换行
                        clm = clm + 1;
                        if (user.NAME == "翁炳富")
                        {
                            user.NAME = "翁炳富";
                        }
                        var msgCNum = user.MsgFormat.Length / 8;
                        msgCNum += (user.MsgFormat.Length % 8 == 0) ? 0 : 1;
                        if (clm > 39)
                        {
                            word.AddRemark(cell4, Helper.Fun.FormatNumToChinese(user.MsgFormat));
                        }
                        else if ((clm + msgCNum) > 39)
                        {
                            var tmpMsg = user.MsgFormat.Substring(0, 8 * (39 - clm));
                            word.AddRemark(cell3, Helper.Fun.FormatNumToChinese(tmpMsg));
                            word.AddRemark(cell4, Helper.Fun.FormatNumToChinese(user.MsgFormat.Substring(8 * (39 - clm))));
                        }
                        else if (clm > 26)
                        {
                            word.AddRemark(cell3, Helper.Fun.FormatNumToChinese(user.MsgFormat));
                        }
                        else if ((clm + msgCNum) > 26)
                        {
                            var tmpMsg = user.MsgFormat.Substring(0, 8 * (26 - clm));
                            word.AddRemark(cell2, Helper.Fun.FormatNumToChinese(tmpMsg));
                            word.AddRemark(cell3, Helper.Fun.FormatNumToChinese(user.MsgFormat.Substring(8 * (26 - clm))));
                        }
                        else if (clm > 13)
                        {
                            word.AddRemark(cell2, Helper.Fun.FormatNumToChinese(user.MsgFormat));
                        }
                        else if ((clm + msgCNum) > 13)
                        {
                            var tmpMsg = user.MsgFormat.Substring(0, 8 * (13 - clm));
                            word.AddRemark(cell, Helper.Fun.FormatNumToChinese(tmpMsg));
                            word.AddRemark(cell2, Helper.Fun.FormatNumToChinese(user.MsgFormat.Substring(8 * (13 - clm))));
                        }
                        else if ((clm + msgCNum) <= 13)
                        {
                            word.AddRemark(cell, Helper.Fun.FormatNumToChinese(user.MsgFormat));
                        }
                        clm += msgCNum;
                        if (clm > 26) isSec = true;
                    }
                }
                var savePath1 = Path.Combine(env.ContentRootPath, string.Format("UpFiles/Doc/{0}{1}.docx", tmp.Code, tmp.Msg));
                var savePath2 = Path.Combine(env.ContentRootPath, string.Format("UpFiles/Doc/{0}{1}1.docx", tmp.Code, tmp.Msg));
                word.SaveDoc(doc, savePath1);

                if (isSec)
                {
                    word1.SaveDoc(doc1, savePath2);
                }
                doc1.Close();
                doc.Close();
            // }

            // }
            // catch (Exception e)
            // {
            //     LogHelper.WriteErrorLog(this.GetType(), "获取用户获取前谱失败", e);
            //     reObj.IsSuccess = false;
            //     reObj.Msg = e.Message;
            // }
            return reObj;
        }


    }
}