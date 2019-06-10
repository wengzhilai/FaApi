
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helper;
using IRepository;
using Models;
using System.Linq;
using Models.Entity;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using System.Linq.Expressions;
using Models.EntityView;
using AutoMapper;

namespace Repository
{
    public class FamilyRepository : IFamilyRepository
    {
        UserInfoRepository userInfo = new UserInfoRepository();

        DapperHelper<FaFamilyEntity> dbHelper = new DapperHelper<FaFamilyEntity>();
        private IMapper mapper;


        public void SetMapper(IMapper _mapper)
        {
            mapper = _mapper;
        }
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<FaFamilyEntity> SingleByKey(int key)
        {
            return dbHelper.SingleByKey(key);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public Task<IEnumerable<FaFamilyEntity>> FindAll(Expression<Func<FaFamilyEntity, bool>> inParm = null)
        {
            return dbHelper.FindAll(inParm);
        }

        public async Task<Result<Relative>> Relative(DtoDo<int> inObj)
        {
            Result<Relative> reobj = new Result<Relative>();

            Relative reEnt = new Relative();
            var userId = Convert.ToInt32(inObj.Key);
            var userInfo = await this.userInfo.SingleByKey(userId);
            if (userInfo == null)
            {
                reobj.IsSuccess = false;
                reobj.Msg = "用户ID有误";
                return reobj;
            }

            reEnt.ItemList = await GetRelativeItems(userInfo);

            reEnt.RelativeList = reEnt.ItemList.Select(x => new KV { K = x.Id.ToString(), V = x.FatherId.ToString() }).ToList();
            reEnt.RelativeList.RemoveAt(reEnt.RelativeList.Count() - 1);
            reobj.Data = reEnt;
            return reobj;
        }

        /// <summary>
        /// 获取用户的关系数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public async Task<List<RelativeItem>> GetRelativeItems(FaUserInfoEntityView userInfo)
        {
            List<RelativeItem> riList = new List<RelativeItem>();

            var tmpXY = await AddSonItem(riList, userInfo, 1, 10, new XYZ { X = 0, Y = 0, Z = 0 });
            var nowInfo = InfoToItem(userInfo, (tmpXY[0] + tmpXY[1]) / 2, 0);
            riList.Add(nowInfo);
            await AddFatherItem(riList, userInfo, 1, 3, new XYZ { X = nowInfo.x, Y = nowInfo.y, Z = userInfo.ChildNum }, tmpXY[0], tmpXY[1]);
            var minX = riList.Min(x => x.x);
            var minY = riList.Max(x => x.y);
            minY = -minY;
            if (minX > 0) minX = 0;
            if (minY > 0) minY = 0;
            foreach (var item in riList)
            {
                item.y = -item.y;
                item.y = item.y - minY;
                item.x = item.x - minX;
            }
            return riList;
        }


        public async Task<Result<FaElderEntity>> GetUserBooksAsync(int userId)
        {
            var reObj = new Result<FaElderEntity>();

            UserInfoRepository userInfoDal = new UserInfoRepository();

            //德字辈排号是24
            var maskUserId = await userInfoDal.GetUserIdByElderAsync(userId, 24);

            //获取所有五福图
            var userInfo = await userInfoDal.SingleByKey(maskUserId);
            var all = await GetRelativeItems(userInfo);

            //转换成用户信息
            var allUserIdList = all.Select(i => i.Id).ToList();
            DapperHelper<FaUserBookEntityView> dapperBooks = new DapperHelper<FaUserBookEntityView>();
            var allBooks = await dapperBooks.FindAll(string.Format("a.ID IN ({0})", string.Join(",", allUserIdList)));

            var elderList = allBooks.GroupBy(x => x.ELDER_ID).Select(x => x.Key).Where(x => x != null).ToList();

            DapperHelper<FaElderEntity> dappElder = new DapperHelper<FaElderEntity>();
            var allElder = await dappElder.FindAll(string.Format("ID IN ({0}) ORDER BY ID", string.Join(",", elderList)));
            foreach (var item in allElder)
            {
                item.AllUser = allBooks.Where(x => x.ELDER_ID == item.ID).OrderBy(i => i.FATHER_ID).ThenBy(i => i.SEX).ThenBy(i => i.LEVEL_ID).ToList();
                foreach (var tmpUser in item.AllUser)
                {
                    if (tmpUser.BIRTHDAY_TIME != null) tmpUser.BirthdaylunlarDate = tmpUser.BIRTHDAY_TIME.Value.ToString("yyyy年MM月dd日HH时");
                    if (tmpUser.SEX == "男" || tmpUser.BIRTHDAY_TIME != null)
                    {

                        var msg = string.Format("{0}行{1}", tmpUser.NAME, tmpUser.LEVEL_ID);

                        msg += (tmpUser.BIRTHDAY_TIME != null) ? string.Format(",生于{0}", tmpUser.BirthdaylunlarDate) : ",生庚未详";

                        if (!string.IsNullOrEmpty(tmpUser.EDUCATION)) msg += string.Format(",毕业于{0}", tmpUser.EDUCATION);
                        if (!string.IsNullOrEmpty(tmpUser.INDUSTRY)) msg += string.Format(",从事{0}行业", tmpUser.INDUSTRY);
                        if (tmpUser.DIED_TIME != null) msg += string.Format(",逝于{0}", tmpUser.DIED_TIME.Value.Hour != 0 ? tmpUser.DIED_TIME.Value.ToString("yyyy年MM月dd日HH时") : tmpUser.DIED_TIME.Value.ToString("yyyy年MM月dd日"));

                        if (string.IsNullOrEmpty(tmpUser.REMARK))
                        {
                            if (tmpUser.CoupleName != null) msg += string.Format(",妻{0}", tmpUser.CoupleName);
                            if (tmpUser.ChildSons != null) msg += string.Format(",生子{0}", tmpUser.ChildSons);
                            if (tmpUser.ChildDaughters != null) msg += string.Format(",生女{0}", tmpUser.ChildDaughters);
                        }
                        else
                        {
                            msg += tmpUser.REMARK;
                        }

                        tmpUser.MsgFormat = msg;
                    }
                }
            }

            reObj.DataList = allElder.ToList();

            reObj.Msg = userInfo.NAME;
            DapperHelper<FaFamilyBooksEntity> dapperFb = new DapperHelper<FaFamilyBooksEntity>();
            var page = await dapperFb.Single(i => i.UserID == userInfo.ID && i.TYPE_ID == 2);
            reObj.Code = (page == null) ? "10" : page.SORT.ToString();
            return reObj;
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
                var tmpXY = await AddSonItem(mainList, allSon[i], 1, 0, new XYZ { X = startX, Y = xyz.Y });
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
