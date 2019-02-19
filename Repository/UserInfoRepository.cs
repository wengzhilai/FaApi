
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

namespace Repository
{
    public class UserInfoRepository : IUserInfoRepository
    {
        DapperHelper<FaUserInfoEntityView> dbHelper = new DapperHelper<FaUserInfoEntityView>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<FaUserInfoEntityView> SingleByKey(int key)
        {
            return new DapperHelper<FaUserInfoEntityView>().SingleByKey(key);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public Task<IEnumerable<FaUserInfoEntityView>> FindAll(Expression<Func<FaUserInfoEntityView, bool>> inParm = null)
        {
            return dbHelper.FindAll(inParm);
        }

        public Task<IEnumerable<FaUserInfoEntityView>> List(DtoSearch<FaUserInfoEntityView> inEnt)
        {
            return dbHelper.FindAll(inEnt);
        }

        async public Task<Result> RegUserInfo(RegUserInfo inEnt)
        {
            Result reObj = new Result();
            DateTime? brithDay = null;
            if (!string.IsNullOrEmpty(inEnt.BirthdayTime))
            {
                brithDay = Convert.ToDateTime(inEnt.BirthdayTime);
            }

            #region 判断父亲级是否正常，并添加不存在的用户
            //表示新增加用户
            if (string.IsNullOrEmpty(inEnt.ParentArr[0].K))
            {
                #region 添加新用户

                int fatherId = 0;
                int.TryParse(inEnt.ParentArr[1].K, out fatherId);
                if (fatherId == 0)
                {
                    reObj.IsSuccess = false;
                    reObj.Msg = "父级ID有误";
                    return reObj;
                }
                var fatherEnt = await dbHelper.Single(x => x.ID == fatherId);
                if (fatherEnt == null)
                {
                    reObj.IsSuccess = false;
                    reObj.Msg = "父级有误";
                    return reObj;
                }
                DapperHelper<FaLoginEntity> dbHelperLogin = new DapperHelper<FaLoginEntity>();
                dbHelperLogin.TranscationBegin();
                try
                {
                    //注册账号，返回UserId
                    var rustle = await new LoginRepository().LoginReg(new LogingDto
                    {
                        userName = inEnt.ParentArr?[0].V,
                        LoginName = inEnt.LoginName,
                        Password = inEnt.Password,
                        code = inEnt.Code
                    }, dbHelperLogin);

                    if (rustle.IsSuccess)
                    {
                        DapperHelper<FaUserInfoEntity> dbUserInfo = new DapperHelper<FaUserInfoEntity>(dbHelperLogin.GetConnection(), dbHelperLogin.GetTransaction());
                        var userinfoId = rustle.Data;
                        var userInfo = await dbUserInfo.Single(x => x.ID == userinfoId);
                        if (userInfo == null)
                        {
                            reObj.IsSuccess = await dbUserInfo.Save(new DtoSave<FaUserInfoEntity>
                            {
                                Data = new FaUserInfoEntity
                                {
                                    ID = rustle.Data,
                                    LEVEL_ID = inEnt.LevelId,
                                    FAMILY_ID = 1,
                                    FATHER_ID = fatherId,
                                    ELDER_ID = fatherEnt.ELDER_ID + 1,
                                    BIRTHDAY_TIME = brithDay,
                                    CREATE_USER_NAME = inEnt.ParentArr[0].V,
                                    UPDATE_USER_NAME = inEnt.ParentArr[0].V,
                                    BIRTHDAY_PLACE = inEnt.BirthdayPlace,
                                    SEX = inEnt.Sex,
                                    YEARS_TYPE = inEnt.YearsType,
                                    STATUS = "正常",
                                    CREATE_TIME = DateTime.Now,
                                    AUTHORITY = 7
                                }
                            }) > 0 ? true : false;

                            if (!reObj.IsSuccess)
                            {
                                reObj.IsSuccess = false;
                                reObj.Code = "-6";
                                reObj.Msg = string.Format("添加userinfo失败");
                                return reObj;
                            }
                        }

                        dbHelperLogin.TranscationCommit();
                    }
                    else
                    {
                        reObj.IsSuccess = false;
                        reObj.Msg = rustle.Msg;
                        dbHelperLogin.TranscationRollback();
                    }
                }
                catch (Exception e)
                {
                    dbHelperLogin.TranscationRollback();
                    reObj.IsSuccess = false;
                    reObj.Msg = e.Message;
                }
                return reObj;


                #endregion
            }
            else  //修改用户
            {
                #region 修改用户信息
                var userId = Convert.ToInt32(inEnt.ParentArr[0].K);
                var user = await new DapperHelper<FaUserEntity>().Single(x => x.ID == userId);
                var upUser = await new LoginRepository().UserEditLoginName(user.LOGIN_NAME, inEnt.LoginName, inEnt.ParentArr[0].V);
                DapperHelper<FaUserInfoEntity> dbUserInfo = new DapperHelper<FaUserInfoEntity>();
                reObj.IsSuccess = await dbUserInfo.Update(new DtoSave<FaUserInfoEntity>
                {

                    Data = new FaUserInfoEntity
                    {
                        ID = userId,
                        LEVEL_ID = inEnt.LevelId,
                        FAMILY_ID = 1,
                        BIRTHDAY_TIME = brithDay,
                        BIRTHDAY_PLACE = inEnt.BirthdayPlace,
                        SEX = inEnt.Sex,
                        YEARS_TYPE = inEnt.YearsType,
                        STATUS = "正常",
                        CREATE_TIME = DateTime.Now,
                        AUTHORITY = 7
                    },
                    SaveFieldList = new List<string>{
                        "LEVEL_ID",
                        "FAMILY_ID",
                        "BIRTHDAY_TIME",
                        "BIRTHDAY_PLACE",
                        "SEX",
                        "YEARS_TYPE",
                        "STATUS",
                        "CREATE_TIME",
                        "AUTHORITY"
                        },
                    WhereList = null
                }) > 0 ? true : false;

                if (!reObj.IsSuccess)
                {
                    reObj.IsSuccess = false;
                    reObj.Code = "-6";
                    reObj.Msg = string.Format("修改userinfo失败");
                    return reObj;
                }
                #endregion
            }
            #endregion

            return reObj;
        }
    }
}
