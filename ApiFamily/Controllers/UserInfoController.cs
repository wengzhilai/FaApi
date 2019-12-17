using System;
using System.Linq;
using System.Threading.Tasks;

using IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Models.EntityView;
using System.Security.Claims;

namespace ApiFamily.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserInfoController : ControllerBase
    {
        private IUserInfoRepository _userInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>

        public UserInfoController(
            IUserInfoRepository userInfo
            )
        {
            _userInfo = userInfo;
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<FaUserInfoEntityView>> Single(DtoKey inEnt)
        {
            ResultObj<FaUserInfoEntityView> reObj = new ResultObj<FaUserInfoEntityView>();
            int key = Convert.ToInt32(inEnt.Key);
            FaUserInfoEntityView user = await _userInfo.SingleByKey(key);
            if (user != null)
            {
                if (user.birthdayTime != null)
                {
                    if (user.yearsType == ("阴历") || user.yearsType == ("国号"))
                    {
                        user.birthdaysolarDate = TypeChange.DateToSolar(user.birthdayTime.Value).ToString("yyyy-MM-dd HH:mm");
                        user.birthdaylunlarDate = user.birthdayTime.Value.ToString("yyyy-MM-dd HH:mm");

                    }
                    else
                    {
                        user.birthdaylunlarDate = TypeChange.DateToLunar(user.birthdayTime.Value).ToString("yyyy-MM-dd HH:mm");
                        user.birthdaysolarDate = user.birthdayTime.Value.ToString("yyyy-MM-dd HH:mm");
                    }
                }
                if (user.diedTime != null && user.isLive == 0)
                {
                    if (user.yearsType == ("阴历") || user.yearsType == ("国号"))
                    {
                        user.diedlunlarDate = TypeChange.DateToSolar(user.diedTime.Value).ToString("yyyy-MM-dd HH:mm");
                        user.diedlunlarDate = user.diedTime.Value.ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        user.diedlunlarDate = TypeChange.DateToLunar(user.diedTime.Value).ToString("yyyy-MM-dd HH:mm");
                        user.diedsolarDate = user.diedTime.Value.ToString("yyyy-MM-dd HH:mm");
                    }
                }
            }
            else
            {
                user = new FaUserInfoEntityView();
            }
            reObj.data = user;
            reObj.success = true;
            return reObj;
        }

        /// <summary>
        /// 根据姓名查用户
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResultObj<FaUserInfoEntityView>> SingleByName(DtoKey inEnt)
        {
            ResultObj<FaUserInfoEntityView> reObj = new ResultObj<FaUserInfoEntityView>();
            var user = await _userInfo.FindAll(x => x.name == inEnt.Key);
            reObj.dataList = user.ToList();
            reObj.success = true;
            return reObj;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<FaUserInfoEntityView>> List(DtoSearch<FaUserInfoEntityView> inEnt)
        {
            ResultObj<FaUserInfoEntityView> reObj = new ResultObj<FaUserInfoEntityView>();
            inEnt.FilterList = x => x.loginName.Length == 11;
            inEnt.OrderType = "id asc";
            var user = await _userInfo.List(inEnt);
            reObj.dataList = user.ToList();
            reObj.success = true;
            return reObj;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result> UserInfoReg(RegUserInfo inEnt)
        {
            var reObj = new Result();
            try
            {
                var smsResult = HttpHelper.HttpPostJson<Result>(AppSettingsManager.self.SmsUrl + "/Sms/ValidSms", new { msgId = inEnt.msg_id, code = inEnt.Code });
                if (!smsResult.success)
                {
                    smsResult.msg += "短信验证码失败";
                    return smsResult;
                }

                return await _userInfo.RegUserInfo(inEnt);
            }
            catch (ExceptionExtend e)
            {
                reObj.success = false;
                reObj.code = e.RealCode;
                reObj.msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        async public Task<Result> Save(DtoSave<FaUserInfoEntityView> inEnt)
        {
            var reObj = new ResultObj<bool>();
            try
            {
                ClaimsIdentity claimsIden = (ClaimsIdentity)User.Identity;
                var name = claimsIden.Claims.SingleOrDefault(x => x.Type == "name");
                var id = claimsIden.Claims.SingleOrDefault(x => x.Type == "id");

                reObj = await this._userInfo.Save(inEnt, name == null ? "" : name.Value, id == null ? 0 : Convert.ToInt32(id.Value));
            }
            catch (ExceptionExtend e)
            {
                reObj.success = false;
                reObj.code = e.RealCode;
                reObj.msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        async public Task<Result> Delete(DtoDo<int> inEnt)
        {
            var reObj = new Result();
            try
            {
                reObj = await this._userInfo.Delete(inEnt.Key);
            }
            catch (ExceptionExtend e)
            {
                reObj.success = false;
                reObj.code = e.RealCode;
                reObj.msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

    }
}