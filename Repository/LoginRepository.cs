
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
        DapperHelper<FaLoginEntity> dbHelper = new DapperHelper<FaLoginEntity>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FaLoginEntity SingleByKey(int key)
        {
            return dbHelper.SingleByKey(key);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public List<FaLoginEntity> FindAll(Expression<Func<FaLoginEntity, bool>> inParm = null)
        {
            return dbHelper.FindAll(inParm);
        }


        /// <summary>
        /// 注册账号
        /// <para>1、添加登录工号 </para>
        /// <para>2、添加用户</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public Result LoginReg(LogingDto inEnt)
        {
            Result reObj = new Result();
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


            if (!inEnt.loginName.IsOnlyNumber() || inEnt.loginName.Length != 11)
            {
                reObj.IsSuccess = false;
                reObj.Code = "-1";
                reObj.Msg = "电话号码格式不正确";
                return reObj;
            }

            if (!Fun.CheckPassword(inEnt.passWord))
            {
                reObj.IsSuccess = false;
                reObj.Code = "-2";
                reObj.Msg = string.Format("密码复杂度不够：{0}");
                return reObj;
            }
            #endregion

            #region 检测验证码
            if (AppSettingsManager.Config.VerifyCode)
            {
                var nowDate = DateTime.Now.AddMinutes(-30);

                var codeNum = new SmsSendRepository().Count(inEnt.loginName, inEnt.code);
                if (codeNum == 0)
                {
                    reObj.IsSuccess = false;
                    reObj.Code = "-3";
                    reObj.Msg = string.Format("验证码无效");
                    return reObj;
                }
            }
            #endregion

            var userList = new UserRepository().FindAll(x => x.LOGIN_NAME == inEnt.loginName);
            #region 检测电话号码是否存在
            if (userList.Count() > 0)
            {
                reObj.IsSuccess = false;
                reObj.Code = "-4";
                reObj.Msg = string.Format("电话号码已经存在，请更换电话号码");
            }
            #endregion

            //开始事务
            dbHelper.TranscationBegin();
            var loginList = new LoginRepository().FindAll(x => x.LOGIN_NAME == inEnt.loginName);
            #region 添加登录账号
            if (loginList.Count() == 0)
            {
                FaLoginEntity inLogin = new FaLoginEntity();
                inLogin.LOGIN_NAME = inEnt.loginName;
                inLogin.PASSWORD = inEnt.passWord.Md5();
                reObj.IsSuccess = dbHelper.Save(new DtoSave<FaLoginEntity>()
                {
                    Data = inLogin
                }) > 0 ? true : false;
                if (!reObj.IsSuccess)
                {
                    reObj.IsSuccess = false;
                    reObj.Code = "-5";
                    reObj.Msg = string.Format("添加账号失败");
                    dbHelper.TranscationRollback();
                    return reObj;
                }
            }
            #endregion

            #region 添加user

            FaUserEntity inUser = new FaUserEntity();
            inUser.LOGIN_NAME = inEnt.loginName;
            inUser.NAME = inEnt.userName;
            inUser.ID = new SequenceRepository().GetNextID<FaUserEntity>();
            inUser.DISTRICT_ID = 1;
            reObj.IsSuccess = new DapperHelper<FaUserEntity>(dbHelper.GetConnection(), dbHelper.GetTransaction()).Save(new DtoSave<FaUserEntity>
            {
                Data = inUser,
                IgnoreFieldList = new List<string>()
            }) > 0 ? true : false;
            if (!reObj.IsSuccess)
            {
                reObj.IsSuccess = false;
                reObj.Code = "-6";
                reObj.Msg = string.Format("添加user失败");
                dbHelper.TranscationRollback();
                return reObj;
            }
            #endregion

            DapperHelper<FaUserInfoEntity> dbUser = new DapperHelper<FaUserInfoEntity>(dbHelper.GetConnection(), dbHelper.GetTransaction());

            var userInfo = dbUser.Single(x => x.ID == inUser.ID);
            if (userInfo == null)
            {
                reObj.IsSuccess = dbUser.Save(new DtoSave<FaUserInfoEntity>
                {
                    Data = new FaUserInfoEntity { ID = inUser.ID }
                }) > 0 ? true : false;
                if (!reObj.IsSuccess)
                {
                    reObj.IsSuccess = false;
                    reObj.Code = "-6";
                    reObj.Msg = string.Format("添加user失败");
                    dbHelper.TranscationRollback();
                    return reObj;
                }
            }
            dbHelper.TranscationCommit();

            return reObj;
        }
        /// <summary>
        /// 注销用户登录状态
        /// <para>清除用户的缓存状态</para>
        /// <para>记录退出日志</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public Result LoginOut(DtoDo<string> inEnt)
        {
            Result reObj = new Result();
            return reObj;
        }
        /// <summary>
        /// 用户登录
        /// <para>只验证用户账号</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>

        public GlobalUser UserLogin(LogingDto inEnt)
        {
            Result reObj = new Result();
            return new GlobalUser();
        }
        /// <summary>
        /// 重置用户密码
        /// <para>VerifyCode:短信验证码</para>
        /// <para>LoginName:登录名</para>
        /// <para>NewPwd:新密码</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public Result ResetPassword(DtoDo<string> inEnt)
        {
            Result reObj = new Result();
            return reObj;
        }
        /// <summary>
        /// 修改用户密码
        /// <para>entity:旧密码</para>
        /// <para>NewPwd:新密码</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public Result UserEditPwd(DtoSave<string> inEnt)
        {
            Result reObj = new Result();
            return reObj;
        }
    }
}
