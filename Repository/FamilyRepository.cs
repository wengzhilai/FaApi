
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

        public async Task<ResultObj<Relative>> Relative(DtoDo<int> inObj)
        {
            ResultObj<Relative> reobj = new ResultObj<Relative>();

            Relative reEnt = new Relative();
            var userId = Convert.ToInt32(inObj.Key);
            var userInfo = await this.userInfo.SingleByKey(userId);
            if (userInfo == null)
            {
                reobj.success = false;
                reobj.msg = "用户ID有误";
                return reobj;
            }

            reEnt.ItemList = await GetRelativeItems(userInfo);

            reEnt.RelativeList = reEnt.ItemList.Select(x => new KV { k = x.Id.ToString(), v = x.FatherId.ToString() }).ToList();
            reEnt.RelativeList.RemoveAt(reEnt.RelativeList.Count() - 1);
            reobj.data = reEnt;
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
            await AddFatherItem(riList, userInfo, 1, 1, new XYZ { X = nowInfo.x, Y = nowInfo.y, Z = userInfo.childNum }, tmpXY[0], tmpXY[1]);
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


        public async Task<ResultObj<FaElderEntity>> GetUserBooksAsync(int userId, int targerEderId = 24)
        {
            var reObj = new ResultObj<FaElderEntity>();
            UserInfoRepository userInfoDal = new UserInfoRepository();

            //德字辈排号是24
            if (targerEderId != 0)
            {
                userId = await userInfoDal.GetUserIdByElderAsync(userId, targerEderId);
            }
            reObj.tmp = userId;

            //获取所有五福图
            var userInfo = await userInfoDal.SingleByKey(userId);
            var all = await GetRelativeItems(userInfo);

            //转换成用户信息
            var allUserIdList = all.Select(i => i.Id).ToList();
            DapperHelper<FaUserBookEntityView> dapperBooks = new DapperHelper<FaUserBookEntityView>();
            var allBooks = await dapperBooks.FindAll(string.Format("a.ID IN ({0})", string.Join(",", allUserIdList)));

            var elderList = allBooks.GroupBy(x => x.elderId).Select(x => x.Key).Where(x => x != 0).ToList();

            DapperHelper<FaElderEntity> dappElder = new DapperHelper<FaElderEntity>();
            var allElder = await dappElder.FindAll(string.Format("ID IN ({0}) ORDER BY ID", string.Join(",", elderList)));
            foreach (var item in allElder)
            {
                item.allUser = allBooks.Where(x => x.elderId == item.id).OrderBy(i => i.fatherId).ThenBy(i => i.sex).ThenBy(i => i.levelId).ToList();
                foreach (var tmpUser in item.allUser)
                {
       
                    if (tmpUser.sex == "男" || tmpUser.birthdayTime != null)
                    {
                        var msg = string.Format("行{1}", tmpUser.name, tmpUser.levelId);
                        msg += (tmpUser.birthdayTime != null) ? string.Format("，生于{0}", Fun.FormatLunlarTime(tmpUser.birthdayTime)) : "，生庚未详";

                        if (!string.IsNullOrEmpty(tmpUser.education)) msg += string.Format("，毕业于{0}", tmpUser.education);
                        if (!string.IsNullOrEmpty(tmpUser.industry)) msg += string.Format("，从事{0}", tmpUser.industry);
                        if (tmpUser.diedTime != null) msg += string.Format("，殁于{0}", Fun.FormatLunlarTime(tmpUser.diedTime));
                        if (!string.IsNullOrEmpty(tmpUser.diedPlace)) msg += string.Format("，殁葬{0}", tmpUser.diedPlace);

                        if (string.IsNullOrEmpty(tmpUser.remark))
                        {
                            if (tmpUser.coupleName != null)
                            {
                                msg += string.Format("，{0}{1}", (tmpUser.sex == "男") ? "妻" : "夫", tmpUser.coupleName);
                                msg += (tmpUser.coupleBirthday != null) ? string.Format("，生于{0}", Fun.FormatLunlarTime(tmpUser.coupleBirthday)) : "，生庚未详";
                                if (tmpUser.coupleDiedTime != null) msg += string.Format("，殁于{0}", Fun.FormatLunlarTime(tmpUser.coupleDiedTime));
                                if (!string.IsNullOrEmpty(tmpUser.coupleDiedPlace)) msg += string.Format("，殁葬{0}", tmpUser.coupleDiedPlace);
                            }
                            if (tmpUser.childSons != null) msg += string.Format("，生子{0}", tmpUser.childSons.Replace(",", "，"));
                            if (tmpUser.childDaughters != null) msg += string.Format("，生女{0}", tmpUser.childDaughters.Replace(",", "，"));
                        }
                        else if(tmpUser.remark.IndexOf("生子")>0 || tmpUser.remark.IndexOf("生女")>0)
                        {
                            msg += tmpUser.remark;
                        }
                        else
                        {
                            msg += tmpUser.remark;
                            if (tmpUser.childSons != null) msg += string.Format("，生子{0}", tmpUser.childSons.Replace(",", "，"));
                            if (tmpUser.childDaughters != null) msg += string.Format("，生女{0}", tmpUser.childDaughters.Replace(",", "，"));
                        }

                        tmpUser.msgFormat = msg;
                    }
                }
            }

            reObj.dataList = allElder.ToList();

            reObj.msg = userInfo.name;
            DapperHelper<FaFamilyBooksEntity> dapperFb = new DapperHelper<FaFamilyBooksEntity>();
            var page = await dapperFb.Single(i => i.userId == userInfo.id && i.typeId == 2);
            reObj.code = (page == null) ? "10" : page.sort.ToString();
            return reObj;
        }


        private RelativeItem InfoToItem(FaUserInfoEntityView userInfo, int x, int y)
        {
            var ent = mapper.Map<RelativeItem>(userInfo);
            ent.x = x;
            ent.y = y;
            ent.CompletionRatio = 0;
            if (userInfo.birthdayTime != null)
            {
                ent.CompletionRatio = 60;
            }
            return ent;
        }
        private async Task<IList<int>> AddSonItem(IList<RelativeItem> mainList, FaUserInfoEntityView inFather, int levelId, int maxLevelId, XYZ xyz)
        {

            if (levelId > maxLevelId) return new[] { xyz.X, xyz.X, xyz.X };
            DapperHelper<FaUserInfoEntityView> dal = new DapperHelper<FaUserInfoEntityView>();
            var allSon = (await dal.FindAll(x => x.fatherId == inFather.id)).ToList();
            allSon = allSon.OrderBy(x => x.levelId).ToList();

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
            if (inSon.fatherId == 0) return true;

            List<FaUserInfoEntityView> allSon = (await this.userInfo.FindAll(x => x.fatherId == inSon.fatherId && x.id == inSon.id)).OrderBy(x => x.levelId).ToList();

            var sonList = mapper.Map<IList<RelativeItem>>(allSon);
            #region 计算坐标，并添加兄弟项
            var myPlace = 0;
            for (var i = 0; i < sonList.Count; i++)
            {
                if (sonList[i].Id == inSon.id) myPlace = i;
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
            if (inSon.fatherId != 0)
            {
                var father = InfoToItem(await this.userInfo.SingleByKey(inSon.fatherId), (minX + maxX) / 2, xyz.Y + 1);
                mainList.Add(father);
                await AddFatherItem(mainList, await this.userInfo.SingleByKey(inSon.fatherId), levelId + 1, maxLevelId, new XYZ { X = father.x, Y = father.y }, minX, maxX);
            }

            return true;
        }

        public async Task<List<KV>> GetUserTreeAsync(int userId, int parentNum)
        {
            var reObj = new List<KV>();
            DapperHelper<FaUserInfoEntityView> dapperUserInfo = new DapperHelper<FaUserInfoEntityView>();
            var user = await dapperUserInfo.Single(i => i.id == userId);
            if (user == null) return reObj;
            reObj.Add(new KV { k = user.id.ToString(), v = user.name });
            for (int i = 0; i < parentNum; i++)
            {
                user = await dapperUserInfo.Single(a => a.id == user.fatherId);
                if (user == null) return reObj;
                reObj.Add(new KV { k = user.id.ToString(), v = user.name });
            }
            return reObj;
        }

        public async Task<List<FaFamilyBooksEntity>> GetUserBooks()
        {
            DapperHelper<FaFamilyBooksEntity> dapper = new DapperHelper<FaFamilyBooksEntity>();
            var reObj = await dapper.FindAll(x => x.typeId == 2);
            return reObj.ToList();
        }
    }
}
