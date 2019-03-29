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
using Models.EntityView;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    /// <summary>
    /// 关系
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        IConfiguration config;
        IUserInfoRepository userInfo;
        private IMapper mapper;
        IUserRepository user;
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="_config"></param>
        /// <param name="_userInfo"></param>
        /// <param name="_mapper"></param>
        /// <param name="_user"></param>
        public FamilyController(
            IConfiguration _config,
            IUserInfoRepository _userInfo,
            IMapper _mapper,
             IUserRepository _user)
        {
            config = _config;
            userInfo = _userInfo;
            mapper = _mapper;
            user = _user;
        }
        /// <summary>
        /// 获取关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<Relative>> Relative(DtoKey inObj)
        {
            Result<Relative> reobj = new Result<Relative>();

            Relative reEnt = new Relative();
            var userId=Convert.ToInt32(inObj.Key);
            var userInfo = await this.userInfo.SingleByKey(userId);
            if (userInfo == null)
            {
                reobj.IsSuccess = false;
                reobj.Msg = "用户ID有误";
                return reobj;
            }
            var tmpXY = await AddSonItem(reEnt.ItemList, userInfo, 1, 10, new XYZ { X = 0, Y = 0, Z = 0 });
            var nowInfo = InfoToItem(userInfo, (tmpXY[0] + tmpXY[1]) / 2, 0);
            reEnt.ItemList.Add(nowInfo);
            await AddFatherItem(reEnt.ItemList, userInfo, 1, 3, new XYZ { X = nowInfo.x, Y = nowInfo.y, Z = userInfo.ChildNum }, tmpXY[0], tmpXY[1]);


            var minX = reEnt.ItemList.Min(x => x.x);
            var minY = reEnt.ItemList.Max(x => x.y);
            minY = -minY;
            if (minX > 0) minX = 0;
            if (minY > 0) minY = 0;
            foreach (var item in reEnt.ItemList)
            {
                item.y = -item.y;
                item.y = item.y - minY;
                item.x = item.x - minX;
            }

            reEnt.RelativeList = reEnt.ItemList.Select(x => new KV { K = x.Id.ToString(), V = x.FatherId.ToString() }).ToList();
            reEnt.RelativeList.RemoveAt(reEnt.RelativeList.Count()-1);
            reobj.Data = reEnt;
            return reobj;
        }
        private RelativeItem InfoToItem(FaUserInfoEntityView userInfo, int x, int y)
        {
            var ent = mapper.Map<RelativeItem>(userInfo);
            ent.x = x;
            ent.y = y;
            return ent;
        }
        private async Task<IList<int>> AddSonItem(IList<RelativeItem> mainList, FaUserInfoEntityView inFather, int levelId, int maxLevelId, XYZ xyz)
        {

            if (levelId > maxLevelId) return new[] { xyz.X, xyz.X, xyz.X };
            DapperHelper<FaUserInfoEntityView> dal = new DapperHelper<FaUserInfoEntityView>();
            var allSon = (await dal.FindAll(x => x.FATHER_ID == inFather.ID)).ToList();
            allSon = allSon.OrderBy(x => x.LEVEL_ID).ToList();

            if (allSon.Count() == 0) return new[] { xyz.X, xyz.X, xyz.X };
            var allSonItem = mapper.Map<IList<RelativeItem>>(allSon);

            int startX = xyz.X;
            for (var i = 0; i < allSonItem.Count(); i++)
            {
                var tmpXY = await AddSonItem(mainList, allSon[i], levelId + 1, maxLevelId, new XYZ { X = startX, Y = xyz.Y - 1 });
                startX = tmpXY[1] + 2;
                if (tmpXY[2] > startX) startX = tmpXY[2];
                allSonItem[i].x = (tmpXY[0] + tmpXY[1]) / 2;
                allSonItem[i].y = xyz.Y - 1;
                mainList.Add(allSonItem[i]);
            }
            var maxX = allSonItem.Max(x => x.x);
            if (maxX < startX) maxX = startX;
            return new[] { allSonItem.Min(x => x.x), allSonItem.Max(x => x.x), maxX };
        }

        private async Task<bool> AddFatherItem(IList<RelativeItem> mainList, FaUserInfoEntityView inSon, int levelId, int maxLevelId, XYZ xyz, int toLeft, int toRigth)
        {
            if (levelId > maxLevelId) return true;
            if (inSon.FATHER_ID == null) return true;

            List<FaUserInfoEntityView> allSon = (await this.userInfo.FindAll(x => x.FATHER_ID == inSon.FATHER_ID)).OrderBy(x => x.LEVEL_ID).ToList();

            var sonList = mapper.Map<IList<RelativeItem>>(allSon);
            #region 计算坐标
            var myPlace = 0;
            for (var i = 0; i < sonList.Count; i++)
            {
                if (sonList[i].Id == inSon.ID) myPlace = i;
            }
            //表示开始的位置            
            sonList[myPlace].x = xyz.X;
            sonList[myPlace].y = xyz.Y;
            for (var i = 0; i < myPlace; i++)
            {
                var thisItme = sonList[myPlace - i - 1];
                thisItme.y = xyz.Y;
                thisItme.x = toLeft - i * 2 - 2;
                mainList.Add(thisItme);
            }

            int startX = toRigth + 2;
            for (var i = myPlace + 1; i < sonList.Count; i++)
            {
                var tmpXY = await AddSonItem(mainList, allSon[i], 1, 1, new XYZ { X = startX, Y = xyz.Y });
                startX = tmpXY[1] + 2;
                sonList[i].x = (tmpXY[0] + tmpXY[1]) / 2;
                sonList[i].y = xyz.Y;
                mainList.Add(sonList[i]);

            }
            #endregion

            var minX = sonList.Min(x => x.x);
            var maxX = sonList.Max(x => x.x);
            if (inSon.FATHER_ID != null)
            {
                var father = InfoToItem(await this.userInfo.SingleByKey(inSon.FATHER_ID.Value), (minX + maxX) / 2, xyz.Y + 1);
                mainList.Add(father);
                await AddFatherItem(mainList, await this.userInfo.SingleByKey(inSon.FATHER_ID.Value), levelId + 1, maxLevelId, new XYZ { X = father.x, Y = father.y }, minX, maxX);
            }

            return true;
        }
    }
}