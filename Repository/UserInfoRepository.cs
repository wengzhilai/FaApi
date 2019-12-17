
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


            //表示新增加用户
            if (string.IsNullOrEmpty(inEnt.ParentArr[0].k))
            {
                #region 添加新用户

                int fatherId = 0;
                int.TryParse(inEnt.ParentArr[1].k, out fatherId);
                if (fatherId == 0)
                {
                    reObj.success = false;
                    reObj.msg = "父级ID有误";
                    return reObj;
                }
                var fatherEnt = await dbHelper.Single(x => x.id == fatherId);
                if (fatherEnt == null)
                {
                    reObj.success = false;
                    reObj.msg = "父级有误";
                    return reObj;
                }
                DapperHelper<FaLoginEntity> dbHelperLogin = new DapperHelper<FaLoginEntity>();
                dbHelperLogin.TranscationBegin();
                try
                {
                    //注册账号，返回UserId
                    var rustle = await new LoginRepository().LoginReg(new LogingDto
                    {
                        userName = inEnt.ParentArr?[0].v,
                        loginName = inEnt.LoginName,
                        password = inEnt.Password,
                        code = inEnt.Code
                    }, dbHelperLogin);

                    if (rustle.success)
                    {
                        DapperHelper<FaUserInfoEntity> dbUserInfo = new DapperHelper<FaUserInfoEntity>(dbHelperLogin.GetConnection(), dbHelperLogin.GetTransaction());
                        var userinfoId = rustle.data;
                        var userInfo = await dbUserInfo.Single(x => x.id == userinfoId);
                        if (userInfo == null)
                        {
                            reObj.success = await dbUserInfo.Save(new DtoSave<FaUserInfoEntity>
                            {
                                data = new FaUserInfoEntity
                                {
                                    id = rustle.data,
                                    levelId = inEnt.LevelId,
                                    familyId = 1,
                                    fatherId = fatherId,
                                    elderId = fatherEnt.elderId + 1,
                                    birthdayTime = brithDay.Value,
                                    createUserName = inEnt.ParentArr[0].v,
                                    updateUserName = inEnt.ParentArr[0].v,
                                    birthdayPlace = inEnt.BirthdayPlace,
                                    sex = inEnt.Sex,
                                    yearsType = inEnt.YearsType,
                                    status = "正常",
                                    createTime = DateTime.Now,
                                    authority = 7
                                }
                            }) > 0 ? true : false;

                            if (!reObj.success)
                            {
                                reObj.success = false;
                                reObj.code = "-6";
                                reObj.msg = string.Format("添加userinfo失败");
                                return reObj;
                            }
                        }

                        dbHelperLogin.TranscationCommit();
                    }
                    else
                    {
                        reObj.success = false;
                        reObj.msg = rustle.msg;
                        dbHelperLogin.TranscationRollback();
                    }
                }
                catch (Exception e)
                {
                    dbHelperLogin.TranscationRollback();
                    reObj.success = false;
                    reObj.msg = e.Message;
                }
                return reObj;


                #endregion
            }
            else  //修改用户
            {
                #region 修改用户信息
                var userId = Convert.ToInt32(inEnt.ParentArr[0].k);
                var user = await new DapperHelper<FaUserEntity>().Single(x => x.id == userId);




                //更新用户信息
                var upUser = await new LoginRepository().UserEditLoginName(user.loginName, inEnt.LoginName, inEnt.ParentArr[0].v, Convert.ToInt32(inEnt.ParentArr[0].k), inEnt.Password, inEnt.iconFiles);
                //更新失败则返回
                if (!upUser.success)
                {
                    return upUser;
                }
                DapperHelper<FaUserInfoEntity> dbUserInfo = new DapperHelper<FaUserInfoEntity>();
                reObj.success = await dbUserInfo.Update(new DtoSave<FaUserInfoEntity>
                {

                    data = new FaUserInfoEntity
                    {
                        id = userId,
                        levelId = inEnt.LevelId,
                        familyId = 1,
                        birthdayTime = brithDay.Value,
                        birthdayPlace = inEnt.BirthdayPlace,
                        sex = inEnt.Sex,
                        yearsType = inEnt.YearsType,
                        status = "正常",
                        createTime = DateTime.Now,
                        authority = 7
                    },
                    saveFieldListExp = x => new object[] { 
                        x.levelId,
                        x.familyId,
                        x.birthdayTime,
                        x.birthdayPlace,
                        x.sex,
                        x.yearsType,
                        x.status,
                        x.createTime,
                        x.authority
                    },
                    whereList = null
                }) > 0 ? true : false;

                if (!reObj.success)
                {
                    reObj.success = false;
                    reObj.code = "-6";
                    reObj.msg = string.Format("修改userinfo失败");
                    return reObj;
                }
                #endregion
            }

            return reObj;
        }

        /// <summary>
        /// 只添加信息不添加账号，可用于保存
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        async public Task<ResultObj<bool>> Save(DtoSave<FaUserInfoEntityView> inEnt, string opUserName, int opUserId)
        {
            var reObj = new ResultObj<bool>();

            DapperHelper<FaUserEntity> dapperUser = new DapperHelper<FaUserEntity>();
            #region 验证信息是否有误



            #endregion

            dapperUser.TranscationBegin();
            DapperHelper<FaUserInfoEntity> dapperUserInfo = new DapperHelper<FaUserInfoEntity>(dapperUser.GetConnection(), dapperUser.GetTransaction());
            DapperHelper<FaLoginEntity> dapperLogin = new DapperHelper<FaLoginEntity>(dapperUser.GetConnection(), dapperUser.GetTransaction());



            try
            {
                var father = await dapperUserInfo.Single(x => x.id == inEnt.data.fatherId);
                if (father == null && inEnt.data.coupleId == 0)
                {
                    reObj.success = false;
                    reObj.msg = "ID有误";
                    return reObj;
                }



                if (inEnt.data.id == 0)
                {
                    #region 添加新用户

                    #region 保存配

                    var userId = await new SequenceRepository().GetNextID<FaUserEntity>();
                    if (inEnt.data.coupleId != 0)
                    {
                        var coupleEnt = await dapperUserInfo.Single(i => i.id == inEnt.data.coupleId);
                        if (coupleEnt == null)
                        {
                            reObj.success = false;
                            reObj.msg = "选择的COUPLE_ID不存在";
                            return reObj;
                        }

                        #region 修改COUPLE用户信息

                        coupleEnt.coupleId = userId;


                        var addUserInfoNum = await dapperUserInfo.Update(new DtoSave<FaUserInfoEntity>
                        {
                            data = coupleEnt,
                            saveFieldListExp = x => new object[] { x.coupleId },
                            whereList = null
                        });
                        if (addUserInfoNum < 1)
                        {
                            dapperUser.TranscationRollback();
                            reObj.success = false;
                            reObj.msg = "修改用户失败";
                            return reObj;
                        }
                        #endregion

                    }
                    #endregion

                    #region 保存用户
                    //如果没有输入登录账号，账号默认为用户的ID
                    var addUserId = await dapperUser.Save(new DtoSave<FaUserEntity>
                    {
                        data = new FaUserEntity
                        {
                            id = userId,
                            name = inEnt.data.name,
                            iconFiles = inEnt.data.iconFiles,
                            createTime = DateTime.Now,
                            loginName = string.IsNullOrEmpty(inEnt.data.loginName) ? userId.ToString() : inEnt.data.loginName,
                            districtId = 1
                        }
                    });
                    if (addUserId < 1)
                    {
                        dapperUser.TranscationRollback();
                        reObj.success = false;
                        reObj.msg = "保存用户失败";
                        return reObj;
                    }
                    #endregion


                    #region 保存用户登录信息
                    if (!string.IsNullOrEmpty(inEnt.data.loginName))
                    {
                        //如果没有输入登录账号，账号默认为用户的ID
                        var addLoginId = await dapperLogin.Save(new DtoSave<FaLoginEntity>
                        {
                            data = new FaLoginEntity
                            {
                                id = await new SequenceRepository().GetNextID<FaLoginEntity>(),
                                loginName = inEnt.data.loginName,
                                password = inEnt.data.loginName.Md5()
                            }
                        });
                        if (addLoginId < 1)
                        {
                            dapperUser.TranscationRollback();
                            reObj.success = false;
                            reObj.msg = "保存用户登录账号失败";
                            return reObj;
                        }
                    }

                    #endregion

                    #region 保存UserInfo


                    var addUserInfoId = await dapperUserInfo.Save(new DtoSave<FaUserInfoEntity>
                    {
                        data = new FaUserInfoEntity
                        {
                            id = userId,
                            levelId = inEnt.data.levelId,
                            coupleId = inEnt.data.coupleId,
                            birthdayTime = inEnt.data.birthdayTime == null ? DateTime.MinValue : inEnt.data.birthdayTime.Value,
                            birthdayPlace = inEnt.data.birthdayPlace,
                            isLive = inEnt.data.isLive,
                            diedTime = inEnt.data.diedTime == null ? DateTime.MinValue : inEnt.data.diedTime.Value,
                            diedPlace = inEnt.data.diedPlace,
                            sex = inEnt.data.sex,
                            yearsType = inEnt.data.yearsType,
                            alias = inEnt.data.alias,
                            industry = inEnt.data.industry,
                            education = inEnt.data.education,
                            diedChinaYear = inEnt.data.diedChinaYear,
                            birthdayChinaYear = inEnt.data.birthdayChinaYear,
                            fatherId = inEnt.data.fatherId,
                            createUserName = opUserName,
                            createUserId = opUserId,
                            updateTime = DateTime.Now,
                            updateUserName = opUserName,
                            updateUserId = opUserId,
                            createTime = DateTime.Now,
                            authority = 0,
                            status = "正常",
                            elderId = (father == null) ? 0 : father.elderId + 1
                        }
                    });
                    if (addUserInfoId < 1)
                    {
                        dapperUser.TranscationRollback();
                        reObj.success = false;
                        reObj.msg = "保存用户信息失败";
                        return reObj;
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    var userId = inEnt.data.id;
                    var user = await dapperUser.Single((x) => x.id == userId);
                    var userInfo = await dapperUserInfo.Single((x) => x.id == userId);
                    #region 检测数据是否正确权限
                    // 正常：所有人都可修改
                    // 锁定：只有创建人可以修改
                    // 存档：任何人都不可修改
                    if (userInfo.status == "存档")
                    {
                        dapperUser.TranscationRollback();
                        reObj.success = false;
                        reObj.msg = "用户信息已经存档，不可修改，请联系管理员";
                        return reObj;
                    }
                    if (userInfo.status == "锁定" && userInfo.createUserId != opUserId)
                    {
                        dapperUser.TranscationRollback();
                        reObj.success = false;
                        reObj.msg = "用户信息已经锁定，不可修改，请联系管理员，和添加用户";
                        return reObj;
                    }

                    if (userInfo.authority > 0)
                    {
                        var isPower = await new RoleRepository().CheckAuth(new CheckAuthDto
                        {
                            UserId = opUserId,
                            Authority = userInfo.authority.ToString(),
                            PowerNum = 2,
                            IsCreater = userInfo.createUserId == opUserId
                        });
                        if (!isPower)
                        {
                            dapperUser.TranscationRollback();
                            reObj.success = false;
                            reObj.msg = "您无权限操作该用户请联系管理员";
                            return reObj;
                        }
                    }

                    if (user == null || userInfo == null)
                    {
                        dapperUser.TranscationRollback();
                        reObj.success = false;
                        reObj.msg = "用户信息不存在";
                        return reObj;
                    }
                    #endregion

                    #region 修改账号
                    //如果账号变动需修改登录账号
                    if (inEnt.data.loginName != user.loginName)
                    {
                        //原账号
                        var login = await dapperLogin.Single(i => i.loginName == user.loginName);

                        if (login != null)
                        {
                            #region 修改原账号

                            #region 判断该用账号是否存在
                            // if (await dapperLogin.Count(i => i.ID != login.ID && i.LOGIN_NAME == inEnt.Data.LOGIN_NAME) > 0)
                            if (await dapperLogin.Count(string.Format("ID<>{0} && LOGIN_NAME='{1}'", login.id, inEnt.data.loginName)) > 0)
                            {
                                dapperUser.TranscationRollback();
                                reObj.success = false;
                                reObj.msg = "登录账号已经存在";
                                return reObj;
                            }
                            #endregion
                            //如果账号为空则删除原有登录账号
                            if (string.IsNullOrEmpty(inEnt.data.loginName))
                            {
                                var delNum = await dapperLogin.Delete(i => i.id == login.id);
                                if (delNum < 1)
                                {
                                    dapperUser.TranscationRollback();
                                    reObj.success = false;
                                    reObj.msg = "删除账号失败";
                                    return reObj;
                                }
                            }
                            else
                            {
                                login.loginName = inEnt.data.loginName;
                                var updateLoginNum = await dapperLogin.Update(new DtoSave<FaLoginEntity>
                                {
                                    data = login,
                                    saveFieldListExp = x => new object[] { x.loginName },
                                    whereList = null
                                });
                                if (updateLoginNum < 1)
                                {
                                    dapperUser.TranscationRollback();
                                    reObj.success = false;
                                    reObj.msg = "修改账号失败";
                                    return reObj;
                                }
                            }
                            #endregion

                        }
                        else
                        {
                            #region 保存用户登录信息
                            if (!string.IsNullOrEmpty(inEnt.data.loginName))
                            {
                                //如果没有输入登录账号，账号默认为用户的ID
                                var addLoginId = await dapperLogin.Save(new DtoSave<FaLoginEntity>
                                {
                                    data = new FaLoginEntity
                                    {
                                        id = await new SequenceRepository().GetNextID<FaLoginEntity>(),
                                        loginName = inEnt.data.loginName,
                                        password = inEnt.data.loginName.Md5()
                                    }
                                });
                                if (addLoginId < 1)
                                {
                                    dapperUser.TranscationRollback();
                                    reObj.success = false;
                                    reObj.msg = "保存用户登录账号失败";
                                    return reObj;
                                }
                            }

                            #endregion

                        }

                    }

                    #endregion

                    #region 修改用户
                    user.name = inEnt.data.name;
                    user.loginName = inEnt.data.loginName;
                    user.iconFiles = inEnt.data.iconFiles;
                    var addUserNum = await dapperUser.Update(new DtoSave<FaUserEntity>
                    {
                        data = user,
                        saveFieldListExp = x => new object[] { x.name, x.iconFiles, x.loginName },
                        whereList = null
                    });
                    if (addUserNum < 1)
                    {
                        dapperUser.TranscationRollback();
                        reObj.success = false;
                        reObj.msg = "修改用户失败";
                        return reObj;
                    }
                    #endregion

                    #region 修改用户信息

                    userInfo.levelId = inEnt.data.levelId;
                    userInfo.coupleId = inEnt.data.coupleId;
                    userInfo.birthdayTime = inEnt.data.birthdayTime.Value;
                    userInfo.birthdayPlace = inEnt.data.birthdayPlace;
                    userInfo.isLive = inEnt.data.isLive;
                    userInfo.diedTime = inEnt.data.diedTime.Value;
                    userInfo.diedPlace = inEnt.data.diedPlace;
                    userInfo.sex = inEnt.data.sex;
                    userInfo.yearsType = inEnt.data.yearsType;
                    userInfo.diedChinaYear = inEnt.data.diedChinaYear;
                    userInfo.birthdayChinaYear = inEnt.data.birthdayChinaYear;
                    userInfo.alias = inEnt.data.alias;
                    userInfo.remark = inEnt.data.remark;
                    userInfo.industry = inEnt.data.industry;
                    userInfo.education = inEnt.data.education;
                    userInfo.authority = inEnt.data.authority;
                    userInfo.updateTime = DateTime.Now;
                    userInfo.updateUserName = opUserName;
                    userInfo.updateUserId = opUserId;

                    var addUserInfoNum = await dapperUserInfo.Update(new DtoSave<FaUserInfoEntity>
                    {
                        data = userInfo,
                        saveFieldListExp = x => new object[] {
                            x.levelId,
                            x.coupleId,
                            x.birthdayTime,
                            x.birthdayPlace,
                            x.isLive,
                            x.diedTime,
                            x.diedPlace,
                            x.sex,
                            x.yearsType,
                            x.alias,
                            x.remark,
                            x.industry,
                            x.diedChinaYear,
                            x.birthdayChinaYear,
                            x.education,
                            x.updateTime,
                            x.updateUserName,
                            x.updateUserId,
                            x.authority,
                        },
                        whereList = null
                    }); ;
                    if (addUserInfoNum < 1)
                    {
                        dapperUser.TranscationRollback();
                        reObj.success = false;
                        reObj.msg = "修改用户失败";
                        return reObj;
                    }
                    #endregion
                }
                dapperUser.TranscationCommit();
                reObj.data = true;
                reObj.success = true;
                reObj.msg = "保存成功";
                return reObj;

            }
            catch (Exception e)
            {
                dapperUser.TranscationRollback();
                reObj.success = false;
                reObj.msg = e.Message;
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
                var children = await dapperUserInfo.Count(i => i.familyId == userId);
                if (children > 0)
                {
                    reObj.success = false;
                    reObj.msg = "该用户有项子项，不能删除";
                    return reObj;

                }
                var opNum = await dapperUserInfo.Delete(i => i.id == userId);
                // reObj.IsSuccess = opNum > 0;
                // if (!reObj.IsSuccess)
                // {
                //     reObj.Msg="删除UserINfo失败";
                //     dapperUserInfo.TranscationRollback();
                //     return reObj;
                // }
                DapperHelper<FaUserEntity> dapperUser = new DapperHelper<FaUserEntity>(dapperUserInfo.GetConnection(), dapperUserInfo.GetTransaction());
                opNum = await dapperUser.Delete(i => i.id == userId);
                reObj.success = opNum > 0;
                if (!reObj.success)
                {
                    reObj.msg = "删除User失败";
                    dapperUserInfo.TranscationRollback();
                    return reObj;
                }
                dapperUserInfo.TranscationCommit();
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog<UserInfoRepository>(e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
                dapperUserInfo.TranscationRollback();
            }
            return reObj;
        }

        public async Task<List<int>> GetCanEditUserIdListAsync(int userId)
        {
            DapperHelper<FaUserInfoEntity> dapperUserInfo = new DapperHelper<FaUserInfoEntity>();
            var allUser = await dapperUserInfo.FindAll(string.Format("FATHER_ID = {0} OR ID = (select FATHER_ID from {1} where ID = {0})", userId, dapperUserInfo.modelHelper.GetTableName()));
            var reList = allUser.Select(i => i.id).ToList();
            reList.Add(userId);
            return reList;
        }

        public async Task<int> GetUserIdByElderAsync(int userId, int elderId)
        {
            DapperHelper<FaUserInfoEntity> dapperUserInfo = new DapperHelper<FaUserInfoEntity>();
            var user = await dapperUserInfo.Single(i => i.id == userId);
            // ELDER_ID 小表示高
            if (user.elderId == 0 || user.elderId <= elderId) return userId;
            int disElder = user.elderId - elderId;
            for (int i = 0; i < disElder; i++)
            {
                if (i == disElder - 1)
                {
                    return user.fatherId;
                }
                user = await dapperUserInfo.Single(a => a.id == user.fatherId);
            }
            return userId;
        }

    }
}
