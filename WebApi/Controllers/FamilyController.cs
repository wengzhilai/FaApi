using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Comon;
using WebApi.Model;
using IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models.Entity;
using WebApi.Model.InEnt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;

namespace WebApi.Controllers
{
    /// <summary>
    /// 关系
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        IConfiguration config;
        IUserRepository user;
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="_config"></param>
        /// <param name="_user"></param>
        public FamilyController(IConfiguration _config, IUserRepository _user)
        {
            config = _config;
            user = _user;
        }
        /// <summary>
        /// 获取关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Result<Relative> Relative(DtoKey inObj)
        {
            Result<Relative> reobj = new Result<Relative>();
       
            // Relative reEnt = new Relative();
            // DapperHelper<FaUserInfoEntity> dal=new DapperHelper<FaUserInfoEntity>();

            //     var userInfo = dal.SingleByKey(inObj.Key);
            //     if (userInfo == null)
            //     {
            //         reobj.IsSuccess=false;
            //         reobj.Msg="用户ID有误";
            //         return reobj;
            //     }
            //     var tmpXY = AddSonItem(reEnt.ItemList, userInfo, 1, 6, new XYZ { X = 0, Y = 0, Z = 0 });
            //     var nowInfo = InfoToItem(userInfo, (tmpXY[0] + tmpXY[1]) / 2, 0);
            //     reEnt.ItemList.Add(nowInfo);
            //     AddFatherItem(reEnt.ItemList, userInfo, 1, 2, new XYZ { X = nowInfo.x, Y = nowInfo.y, Z = userInfo.fa_user_info1.Count() }, tmpXY[0], tmpXY[1]);


            //     var minX = reEnt.ItemList.Min(x => x.x);
            //     var minY = reEnt.ItemList.Max(x => x.y);
            //     minY = -minY;
            //     if (minX > 0) minX = 0;
            //     if (minY > 0) minY = 0;
            //     foreach (var item in reEnt.ItemList)
            //     {
            //         item.y = -item.y;
            //         item.y = item.y - minY;
            //         item.x = item.x - minX;
            //     }

            //     reEnt.RelativeList = reEnt.ItemList.Where(x => x.FatherId != null).Select(x => new KV { K = x.Id.ToString(), V = x.FatherId.ToString() }).ToList();



            return reobj;
        }

    //     private IList<int> AddSonItem(IList<RelativeItem> mainList, FaUserInfoEntity inFather, int levelId, int maxLevelId, XYZ xyz)
    //     {

    //         if (levelId > maxLevelId) return new[] { xyz.X, xyz.X, xyz.X };
    //         DapperHelper<FaUserInfoEntity> dal=new DapperHelper<FaUserInfoEntity>();
    //         var allSon = dal.FindAll(new {FATHER_ID=inFather.ID}).OrderBy(x => x.LEVEL_ID).ToList();;
    //         if (allSon.Count() == 0) return new[] { xyz.X, xyz.X, xyz.X };
    //         var allSonItem = Mapper.Map<IList<RelativeItem>>(allSon);

    //         int startX = xyz.X;
    //         for (var i = 0; i < allSonItem.Count(); i++)
    //         {
    //             var tmpXY = AddSonItem(mainList, allSon[i], levelId + 1, maxLevelId, new XYZ { X = startX, Y = xyz.Y - 1 });
    //             startX = tmpXY[1] + 2;
    //             if (tmpXY[2] > startX) startX = tmpXY[2];
    //             allSonItem[i].x = (tmpXY[0] + tmpXY[1]) / 2;
    //             allSonItem[i].y = xyz.Y - 1;
    //             mainList.Add(allSonItem[i]);
    //         }
    //         var maxX = allSonItem.Max(x => x.x);
    //         if (maxX < startX) maxX = startX;
    //         return new[] { allSonItem.Min(x => x.x), allSonItem.Max(x => x.x), maxX };
    //     }
    }
}