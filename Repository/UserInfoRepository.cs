
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

                #region 保存头像
                //如果有新添加的头像，则保存像头地址到数据库
                if (inEnt.ICON_FILES_ID == 0 && inEnt.IconFiles != null && !string.IsNullOrEmpty(inEnt.IconFiles.PATH))
                {
                    DapperHelper<FaFilesEntity> dapperFile = new DapperHelper<FaFilesEntity>();
                    inEnt.ICON_FILES_ID = await new SequenceRepository().GetNextID<FaFilesEntity>();
                    inEnt.IconFiles.ID = inEnt.ICON_FILES_ID;
                    inEnt.IconFiles.UPLOAD_TIME = DateTime.Now;
                    var saveNum = await dapperFile.Save(new DtoSave<FaFilesEntity>
                    {
                        Data = inEnt.IconFiles
                    });
                    if (saveNum < 1)
                    {
                        reObj.IsSuccess = false;
                        reObj.Msg = "保存文件失败";
                        return reObj;
                    }
                }
                #endregion


                //更新用户信息
                var upUser = await new LoginRepository().UserEditLoginName(user.LOGIN_NAME, inEnt.LoginName, inEnt.ParentArr[0].V, Convert.ToInt32(inEnt.ParentArr[0].K), inEnt.Password, inEnt.ICON_FILES_ID);
                //更新失败则返回
                if (!upUser.IsSuccess)
                {
                    return upUser;
                }
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

        /// <summary>
        /// 只添加信息不添加账号，可用于保存
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        async public Task<Result<bool>> Save(DtoSave<FaUserInfoEntityView> inEnt, string opUserName, int opUserId)
        {
            var reObj = new Result<bool>();

            DapperHelper<FaUserEntity> dapperUser = new DapperHelper<FaUserEntity>();
            #region 验证信息是否有误
            // if (string.IsNullOrEmpty(inEnt.Data.LOGIN_NAME))
            // {
            //     reObj.IsSuccess = false;
            //     reObj.Msg = "电话号码不能为空";
            //     return reObj;
            // }
            if (inEnt.Data.ID == 0 && !string.IsNullOrEmpty(inEnt.Data.LOGIN_NAME))
            {
                if (await dapperUser.Count(i => i.LOGIN_NAME == inEnt.Data.LOGIN_NAME) > 0)
                {
                    reObj.IsSuccess = false;
                    reObj.Msg = "该电话号码已经被注册";
                    return reObj;
                }
            }
            #endregion

            dapperUser.TranscationBegin();
            DapperHelper<FaUserInfoEntity> dapperUserInfo = new DapperHelper<FaUserInfoEntity>(dapperUser.GetConnection(), dapperUser.GetTransaction());
            DapperHelper<FaLoginEntity> dapperLogin = new DapperHelper<FaLoginEntity>(dapperUser.GetConnection(), dapperUser.GetTransaction());



            try
            {
                #region 保存头像
                //如果有新添加的头像，则保存像头地址到数据库
                if ((inEnt.Data.ICON_FILES_ID == null || inEnt.Data.ICON_FILES_ID == 0) && inEnt.Data.IconFiles != null && !string.IsNullOrEmpty(inEnt.Data.IconFiles.PATH))
                {
                    DapperHelper<FaFilesEntity> dapperFile = new DapperHelper<FaFilesEntity>(dapperUser.GetConnection(), dapperUser.GetTransaction());
                    inEnt.Data.ICON_FILES_ID = await new SequenceRepository().GetNextID<FaFilesEntity>();
                    inEnt.Data.IconFiles.ID = inEnt.Data.ICON_FILES_ID.Value;
                    inEnt.Data.IconFiles.UPLOAD_TIME = DateTime.Now;
                    var saveNum = await dapperFile.Save(new DtoSave<FaFilesEntity>
                    {
                        Data = inEnt.Data.IconFiles
                    });
                    if (saveNum < 1)
                    {
                        dapperUser.TranscationRollback();
                        reObj.IsSuccess = false;
                        reObj.Msg = "保存文件失败";
                        return reObj;
                    }
                }
                #endregion

                if (inEnt.Data.ID == 0)
                {
                    #region 添加新用户

                    #region 保存配

                    var userId = await new SequenceRepository().GetNextID<FaUserEntity>();
                    if (inEnt.Data.COUPLE_ID != null)
                    {
                        var coupleEnt = await dapperUserInfo.Single(i => i.ID == inEnt.Data.COUPLE_ID);
                        if (coupleEnt == null)
                        {
                            reObj.IsSuccess = false;
                            reObj.Msg = "选择的COUPLE_ID不存在";
                            return reObj;
                        }

                        #region 修改COUPLE用户信息

                        coupleEnt.COUPLE_ID = userId;

                        var saveList = new List<string>();
                        saveList.Add("COUPLE_ID");

                        var addUserInfoNum = await dapperUserInfo.Update(new DtoSave<FaUserInfoEntity>
                        {
                            Data = coupleEnt,
                            SaveFieldList = saveList,
                            WhereList = null
                        });
                        if (addUserInfoNum < 1)
                        {
                            dapperUser.TranscationRollback();
                            reObj.IsSuccess = false;
                            reObj.Msg = "修改用户失败";
                            return reObj;
                        }
                        #endregion

                    }
                    #endregion

                    #region 保存用户
                    //如果没有输入登录账号，账号默认为用户的ID
                    var addUserId = await dapperUser.Save(new DtoSave<FaUserEntity>
                    {
                        Data = new FaUserEntity
                        {
                            ID = userId,
                            NAME = inEnt.Data.NAME,
                            ICON_FILES_ID = inEnt.Data.ICON_FILES_ID,
                            CREATE_TIME = DateTime.Now,
                            LOGIN_NAME = string.IsNullOrEmpty(inEnt.Data.LOGIN_NAME) ? userId.ToString() : inEnt.Data.LOGIN_NAME,
                            DISTRICT_ID = 1
                        }
                    });
                    if (addUserId < 1)
                    {
                        dapperUser.TranscationRollback();
                        reObj.IsSuccess = false;
                        reObj.Msg = "保存用户失败";
                        return reObj;
                    }
                    #endregion


                    #region 保存用户登录信息
                    if (!string.IsNullOrEmpty(inEnt.Data.LOGIN_NAME))
                    {
                        //如果没有输入登录账号，账号默认为用户的ID
                        var addLoginId = await dapperLogin.Save(new DtoSave<FaLoginEntity>
                        {
                            Data = new FaLoginEntity
                            {
                                ID = await new SequenceRepository().GetNextID<FaLoginEntity>(),
                                LOGIN_NAME = inEnt.Data.LOGIN_NAME,
                                PASSWORD = inEnt.Data.LOGIN_NAME.Md5()
                            }
                        });
                        if (addLoginId < 1)
                        {
                            dapperUser.TranscationRollback();
                            reObj.IsSuccess = false;
                            reObj.Msg = "保存用户登录账号失败";
                            return reObj;
                        }
                    }

                    #endregion

                    #region 保存UserInfo
                    var addUserInfoId = await dapperUserInfo.Save(new DtoSave<FaUserInfoEntity>
                    {
                        Data = new FaUserInfoEntity
                        {
                            ID = userId,
                            LEVEL_ID = inEnt.Data.LEVEL_ID,
                            COUPLE_ID = inEnt.Data.COUPLE_ID,
                            BIRTHDAY_TIME = inEnt.Data.BIRTHDAY_TIME,
                            BIRTHDAY_PLACE = inEnt.Data.BIRTHDAY_PLACE,
                            IS_LIVE = inEnt.Data.IS_LIVE,
                            DIED_TIME = inEnt.Data.DIED_TIME,
                            DIED_PLACE = inEnt.Data.DIED_PLACE,
                            SEX = inEnt.Data.SEX,
                            YEARS_TYPE = inEnt.Data.YEARS_TYPE,
                            ALIAS = inEnt.Data.ALIAS,
                            INDUSTRY = inEnt.Data.INDUSTRY,
                            EDUCATION = inEnt.Data.EDUCATION,
                            DIED_CHINA_YEAR = inEnt.Data.DIED_CHINA_YEAR,
                            BIRTHDAY_CHINA_YEAR = inEnt.Data.BIRTHDAY_CHINA_YEAR,
                            FATHER_ID = inEnt.Data.FATHER_ID,
                            CREATE_USER_NAME = opUserName,
                            CREATE_USER_ID = opUserId,
                            UPDATE_TIME = DateTime.Now,
                            UPDATE_USER_NAME = opUserName,
                            UPDATE_USER_ID = opUserId,
                            CREATE_TIME = DateTime.Now,
                            AUTHORITY = 0,
                            STATUS = "正常",

                        }
                    });
                    if (addUserInfoId < 1)
                    {
                        dapperUser.TranscationRollback();
                        reObj.IsSuccess = false;
                        reObj.Msg = "保存用户信息失败";
                        return reObj;
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    var userId = inEnt.Data.ID;
                    var user = await dapperUser.Single((x) => x.ID == userId);
                    var userInfo = await dapperUserInfo.Single((x) => x.ID == userId);
                    #region 检测数据是否正确权限
                    // 正常：所有人都可修改
                    // 锁定：只有创建人可以修改
                    // 存档：任何人都不可修改
                    if (userInfo.STATUS == "存档")
                    {
                        dapperUser.TranscationRollback();
                        reObj.IsSuccess = false;
                        reObj.Msg = "用户信息已经存档，不可修改，请联系管理员";
                        return reObj;
                    }
                    if (userInfo.STATUS == "锁定" && userInfo.CREATE_USER_ID != opUserId)
                    {
                        dapperUser.TranscationRollback();
                        reObj.IsSuccess = false;
                        reObj.Msg = "用户信息已经锁定，不可修改，请联系管理员，和添加用户";
                        return reObj;
                    }

                    if (userInfo.AUTHORITY > 0)
                    {
                        var isPower = await new RoleRepository().CheckAuth(new CheckAuthDto
                        {
                            UserId = opUserId,
                            Authority = userInfo.AUTHORITY.ToString(),
                            PowerNum = 2,
                            IsCreater = userInfo.CREATE_USER_ID == opUserId
                        });
                        if (!isPower)
                        {
                            dapperUser.TranscationRollback();
                            reObj.IsSuccess = false;
                            reObj.Msg = "您无权限操作该用户请联系管理员";
                            return reObj;
                        }
                    }

                    if (user == null || userInfo == null)
                    {
                        dapperUser.TranscationRollback();
                        reObj.IsSuccess = false;
                        reObj.Msg = "用户信息不存在";
                        return reObj;
                    }
                    #endregion

                    #region 修改账号
                    //如果账号变动需修改登录账号
                    if (inEnt.Data.LOGIN_NAME != user.LOGIN_NAME)
                    {
                        //原账号
                        var login = await dapperLogin.Single(i => i.LOGIN_NAME == user.LOGIN_NAME);

                        if (login != null)
                        {
                            #region 修改原账号

                            #region 判断该用账号是否存在
                            // if (await dapperLogin.Count(i => i.ID != login.ID && i.LOGIN_NAME == inEnt.Data.LOGIN_NAME) > 0)
                            if (await dapperLogin.Count(string.Format("ID<>{0} && LOGIN_NAME='{1}'", login.ID, inEnt.Data.LOGIN_NAME)) > 0)
                            {
                                dapperUser.TranscationRollback();
                                reObj.IsSuccess = false;
                                reObj.Msg = "登录账号已经存在";
                                return reObj;
                            }
                            #endregion
                            //如果账号为空则删除原有登录账号
                            if (string.IsNullOrEmpty(inEnt.Data.LOGIN_NAME))
                            {
                                var delNum = await dapperLogin.Delete(i => i.ID == login.ID);
                                if (delNum < 1)
                                {
                                    dapperUser.TranscationRollback();
                                    reObj.IsSuccess = false;
                                    reObj.Msg = "删除账号失败";
                                    return reObj;
                                }
                            }
                            else
                            {
                                login.LOGIN_NAME = inEnt.Data.LOGIN_NAME;
                                var updateLoginNum = await dapperLogin.Update(new DtoSave<FaLoginEntity>
                                {
                                    Data = login,
                                    SaveFieldList = new List<string> { "LOGIN_NAME" },
                                    WhereList = null
                                });
                                if (updateLoginNum < 1)
                                {
                                    dapperUser.TranscationRollback();
                                    reObj.IsSuccess = false;
                                    reObj.Msg = "修改账号失败";
                                    return reObj;
                                }
                            }
                            #endregion

                        }
                        else
                        {
                            #region 保存用户登录信息
                            if (!string.IsNullOrEmpty(inEnt.Data.LOGIN_NAME))
                            {
                                //如果没有输入登录账号，账号默认为用户的ID
                                var addLoginId = await dapperLogin.Save(new DtoSave<FaLoginEntity>
                                {
                                    Data = new FaLoginEntity
                                    {
                                        ID = await new SequenceRepository().GetNextID<FaLoginEntity>(),
                                        LOGIN_NAME = inEnt.Data.LOGIN_NAME,
                                        PASSWORD = inEnt.Data.LOGIN_NAME.Md5()
                                    }
                                });
                                if (addLoginId < 1)
                                {
                                    dapperUser.TranscationRollback();
                                    reObj.IsSuccess = false;
                                    reObj.Msg = "保存用户登录账号失败";
                                    return reObj;
                                }
                            }

                            #endregion

                        }

                    }

                    #endregion

                    #region 修改用户
                    user.NAME = inEnt.Data.NAME;
                    user.LOGIN_NAME = inEnt.Data.LOGIN_NAME;
                    user.ICON_FILES_ID = inEnt.Data.ICON_FILES_ID;
                    var addUserNum = await dapperUser.Update(new DtoSave<FaUserEntity>
                    {
                        Data = user,
                        SaveFieldList = new List<string> { "NAME", "ICON_FILES_ID", "LOGIN_NAME" },
                        WhereList = null
                    });
                    if (addUserNum < 1)
                    {
                        dapperUser.TranscationRollback();
                        reObj.IsSuccess = false;
                        reObj.Msg = "修改用户失败";
                        return reObj;
                    }
                    #endregion

                    #region 修改用户信息

                    userInfo.LEVEL_ID = inEnt.Data.LEVEL_ID;
                    userInfo.COUPLE_ID = inEnt.Data.COUPLE_ID;
                    userInfo.BIRTHDAY_TIME = inEnt.Data.BIRTHDAY_TIME;
                    userInfo.BIRTHDAY_PLACE = inEnt.Data.BIRTHDAY_PLACE;
                    userInfo.IS_LIVE = inEnt.Data.IS_LIVE;
                    userInfo.DIED_TIME = inEnt.Data.DIED_TIME;
                    userInfo.DIED_PLACE = inEnt.Data.DIED_PLACE;
                    userInfo.SEX = inEnt.Data.SEX;
                    userInfo.YEARS_TYPE = inEnt.Data.YEARS_TYPE;
                    userInfo.DIED_CHINA_YEAR = inEnt.Data.DIED_CHINA_YEAR;
                    userInfo.BIRTHDAY_CHINA_YEAR = inEnt.Data.BIRTHDAY_CHINA_YEAR;
                    userInfo.ALIAS = inEnt.Data.ALIAS;
                    userInfo.REMARK = inEnt.Data.REMARK;
                    userInfo.INDUSTRY = inEnt.Data.INDUSTRY;
                    userInfo.EDUCATION = inEnt.Data.EDUCATION;
                    userInfo.AUTHORITY = inEnt.Data.AUTHORITY;
                    userInfo.UPDATE_TIME = DateTime.Now;
                    userInfo.UPDATE_USER_NAME = opUserName;
                    userInfo.UPDATE_USER_ID = opUserId;
                    var saveList = new List<string>();
                    saveList.Add("LEVEL_ID");
                    saveList.Add("COUPLE_ID");
                    saveList.Add("BIRTHDAY_TIME");
                    saveList.Add("BIRTHDAY_PLACE");
                    saveList.Add("IS_LIVE");
                    saveList.Add("DIED_TIME");
                    saveList.Add("DIED_PLACE");
                    saveList.Add("SEX");
                    saveList.Add("YEARS_TYPE");
                    saveList.Add("ALIAS");
                    saveList.Add("REMARK");
                    saveList.Add("INDUSTRY");
                    saveList.Add("DIED_CHINA_YEAR");
                    saveList.Add("BIRTHDAY_CHINA_YEAR");
                    saveList.Add("EDUCATION");
                    saveList.Add("UPDATE_TIME");
                    saveList.Add("UPDATE_USER_NAME");
                    saveList.Add("UPDATE_USER_ID");
                    saveList.Add("AUTHORITY");
                    var addUserInfoNum = await dapperUserInfo.Update(new DtoSave<FaUserInfoEntity>
                    {
                        Data = userInfo,
                        SaveFieldList = saveList,
                        WhereList = null
                    });
                    if (addUserInfoNum < 1)
                    {
                        dapperUser.TranscationRollback();
                        reObj.IsSuccess = false;
                        reObj.Msg = "修改用户失败";
                        return reObj;
                    }
                    #endregion
                }
                dapperUser.TranscationCommit();
                reObj.Data = true;
                reObj.IsSuccess = true;
                reObj.Msg = "保存成功";
                return reObj;

            }
            catch (Exception e)
            {
                dapperUser.TranscationRollback();
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
                return reObj;
            }
        }

        async public Task<Result> Delete(int userId)
        {
            Result reObj = new Result();
            DapperHelper<FaUserInfoEntity> dapperUserInfo = new DapperHelper<FaUserInfoEntity>();
            dapperUserInfo.TranscationBegin();
            try
            {
                var children = await dapperUserInfo.Count(i => i.FAMILY_ID == userId);
                if (children > 0)
                {
                    reObj.IsSuccess = false;
                    reObj.Msg = "该用户有项子项，不能删除";
                    return reObj;

                }
                var opNum = await dapperUserInfo.Delete(i => i.ID == userId);
                // reObj.IsSuccess = opNum > 0;
                // if (!reObj.IsSuccess)
                // {
                //     reObj.Msg="删除UserINfo失败";
                //     dapperUserInfo.TranscationRollback();
                //     return reObj;
                // }
                DapperHelper<FaUserEntity> dapperUser = new DapperHelper<FaUserEntity>(dapperUserInfo.GetConnection(), dapperUserInfo.GetTransaction());
                opNum = await dapperUser.Delete(i => i.ID == userId);
                reObj.IsSuccess = opNum > 0;
                if (!reObj.IsSuccess)
                {
                    reObj.Msg = "删除User失败";
                    dapperUserInfo.TranscationRollback();
                    return reObj;
                }
                dapperUserInfo.TranscationCommit();
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog<UserInfoRepository>(e.ToString());
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
                dapperUserInfo.TranscationRollback();
            }
            return reObj;
        }

        public async Task<List<int>> GetCanEditUserIdListAsync(int userId)
        {
            DapperHelper<FaUserInfoEntity> dapperUserInfo = new DapperHelper<FaUserInfoEntity>();
            var allUser = await dapperUserInfo.FindAll(string.Format("FATHER_ID = {0} or ID (select FATHER_ID from {1} where ID = {0})", userId, dapperUserInfo.modelHelper.GetTableName()));
            var reList = allUser.Select(i => i.ID).ToList();
            reList.Add(userId);
            return reList;
        }

        public async Task<int> GetUserIdByElderAsync(int userId, int elderId)
        {
            DapperHelper<FaUserInfoEntity> dapperUserInfo = new DapperHelper<FaUserInfoEntity>();
            var user = await dapperUserInfo.Single(i => i.ID == userId);
            // ELDER_ID 小表示高
            if (user.ELDER_ID == null || user.ELDER_ID <= elderId) return userId;
            int disElder = user.ELDER_ID.Value - elderId;
            for (int i = 0; i < disElder; i++)
            {
                if (i == disElder - 1)
                {
                    return user.FATHER_ID.Value;
                }
                user = await dapperUserInfo.Single(a => a.ID == user.FATHER_ID);
            }
            return userId;
        }

    }
}
