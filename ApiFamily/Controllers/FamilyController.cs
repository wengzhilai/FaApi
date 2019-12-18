using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

namespace ApiFamily.Controllers
{
    /// <summary>
    /// 关系
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FamilyController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private IMapper mapper;
        IFamilyRepository family;
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="_mapper"></param>
        /// <param name="_family"></param>
        /// <param name="_env"></param>
        public FamilyController(
            IMapper _mapper,
            IFamilyRepository _family,
            IWebHostEnvironment _env)
        {
            mapper = _mapper;
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
        public async Task<ResultObj<Relative>> Relative(DtoDo<int> inObj)
        {
            ResultObj<Relative> reObj = new ResultObj<Relative>();
            try
            {
                reObj = await family.Relative(inObj);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户获取关系图失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }




        /// <summary>
        /// 获取关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<FaElderEntity>> GetUserBooks(DtoDo<int> inObj)
        {
            ResultObj<FaElderEntity> reObj = new ResultObj<FaElderEntity>();
            try
            {
                reObj = await family.GetUserBooksAsync(inObj.Key);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户获取前谱失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
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
            int maxCum = 19;
            int titleNum = 2;
            Result reObj = new Result();
            // try
            // {
            // var allUser = await family.GetUserBooks();
            // foreach (var userItem in allUser)
            // {
                // var tmp = await family.GetUserBooksAsync(userItem.UserID.Value, 0);
                var tmp = await family.GetUserBooksAsync(137, 0);
                var userId = Convert.ToInt32(tmp.tmp);
                //获取父节点数
                var parentList = await family.GetUserTreeAsync(userId, 6);
                parentList.Reverse();
                var allPath = Path.Combine(env.ContentRootPath, "../Doc/Family1.docx");
                WordHelper word = new WordHelper();
                WordHelper word1 = new WordHelper();
                var doc = word.MakeXWPFDocument(allPath);
                var doc1 = word1.MakeXWPFDocument(allPath);
                //标题
                var cellTitle = doc.Tables[0].Rows[0].GetCell(0).Tables[0].Rows[0].GetCell(0);
                word.AddNavigation(cellTitle, string.Join("----", parentList.Select(x => x.v.Substring(1))));

                #region 设置页码
                {
                    var cell1 = doc.Tables[0].Rows[3].GetCell(1);
                    var cell2 = doc.Tables[1].Rows[3].GetCell(0);
                    var cell3 = doc1.Tables[0].Rows[3].GetCell(1);
                    var cell4 = doc1.Tables[1].Rows[3].GetCell(0);
                    word.AddPageNum(cell1, tmp.msg);
                    word.AddPageNum(cell2, "");
                    word1.AddPageNum(cell3, "");
                    word1.AddPageNum(cell4, "");
                }
                #endregion
                int elder = Convert.ToInt32(tmp.code);
                bool isSec = false;
                for (int i = 0; i < tmp.dataList.Count; i++)
                {
                    if (i >= 5) break;
                    var item = tmp.dataList[i];
                    var cell1 = doc.Tables[0].Rows[0].GetCell(0).Tables[0].Rows[i + 1].GetCell(0);
                    var cell2 = doc.Tables[1].Rows[0].GetCell(1).Tables[0].Rows[i + 1].GetCell(0);
                    var cell3 = doc1.Tables[0].Rows[0].GetCell(0).Tables[0].Rows[i + 1].GetCell(0);
                    var cell4 = doc1.Tables[1].Rows[0].GetCell(1).Tables[0].Rows[i + 1].GetCell(0);
                    var cell = cell1;
                    int clm = 0;
                    // if (elder < 100)
                    // {
                        word.AddElder(cell, "第" + Helper.Fun.NumberToChinese(item.id) + "世");
                        clm += titleNum;
                    // }
                    foreach (var user in item.allUser)
                    {
                        if (user.name == "翁定学")
                        {
                            user.name = "翁定学";
                        }

                        if (string.IsNullOrEmpty(user.msgFormat)) continue;

                        if (clm > maxCum * 3 - titleNum) cell = cell4;
                        else if (clm >= maxCum * 2 - titleNum) cell = cell3;
                        else if (clm >= maxCum * 1 - titleNum) cell = cell2;

                        word.AddName(cell, user.name);
                        //判断加了姓名是否该换行
                        clm = clm + titleNum;

                        var msgCNum = user.msgFormat.Length / 8;
                        msgCNum += (user.msgFormat.Length % 8 == 0) ? 0 : 1;
                        if (clm > maxCum * 3)
                        {
                            word.AddRemark(cell4, Helper.Fun.FormatNumToChinese(user.msgFormat));
                        }
                        else if ((clm + msgCNum) > maxCum * 3)
                        {
                            var tmpMsg = user.msgFormat.Substring(0, 8 * (maxCum * 3 - clm));
                            word.AddRemark(cell3, Helper.Fun.FormatNumToChinese(tmpMsg));
                            word.AddRemark(cell4, Helper.Fun.FormatNumToChinese(user.msgFormat.Substring(8 * (maxCum * 3 - clm))));
                        }
                        else if (clm > maxCum * 2)
                        {
                            word.AddRemark(cell3, Helper.Fun.FormatNumToChinese(user.msgFormat));
                        }
                        else if ((clm + msgCNum) > maxCum * 2)
                        {
                            var tmpMsg = user.msgFormat.Substring(0, 8 * (maxCum * 2 - clm));
                            word.AddRemark(cell2, Helper.Fun.FormatNumToChinese(tmpMsg));
                            word.AddRemark(cell3, Helper.Fun.FormatNumToChinese(user.msgFormat.Substring(8 * (maxCum * 2 - clm))));
                        }
                        else if (clm > maxCum * 1)
                        {
                            word.AddRemark(cell2, Helper.Fun.FormatNumToChinese(user.msgFormat));
                        }
                        else if ((clm + msgCNum) > maxCum * 1)
                        {
                            var tmpMsg = user.msgFormat.Substring(0, 8 * (maxCum * 1 - clm));
                            word.AddRemark(cell, Helper.Fun.FormatNumToChinese(tmpMsg));
                            word.AddRemark(cell2, Helper.Fun.FormatNumToChinese(user.msgFormat.Substring(8 * (maxCum * 1 - clm))));
                        }
                        else if ((clm + msgCNum) <= maxCum * 1)
                        {
                            word.AddRemark(cell, Helper.Fun.FormatNumToChinese(user.msgFormat));
                        }

                        clm += msgCNum;
                        if (clm > maxCum * 2) isSec = true;
                    }
                }
                var savePath1 = Path.Combine(env.ContentRootPath, string.Format("UpFiles/Doc/{0}{1}.docx", tmp.code, tmp.msg));
                var savePath2 = Path.Combine(env.ContentRootPath, string.Format("UpFiles/Doc/{0}{1}1.docx", tmp.code, tmp.msg));
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