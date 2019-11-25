
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUser.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 测试超级管理员用户
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "superadmin")]
        public Result TestUser(DtoKey inEnt)
        {
            Result reEnt = new Result();
            reEnt.IsSuccess = true;
            reEnt.Msg = "User.Identity:" + TypeChange.ObjectToStr(User.Identity);
            reEnt.Msg += "User.Claims:" + TypeChange.ObjectToStr(from c in User.Claims select new { c.Type, c.Value });

            return reEnt;
        }
        /// <summary>
        /// 接口测试
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Test(DtoKey inEnt)
        {
            Result reEnt = new Result();
            reEnt.IsSuccess = true;
            reEnt.Msg = "接口测试成功";
            return reEnt;
        }
        /// <summary>
        /// 权限接口测试
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public Result TestAuth(DtoKey inEnt)
        {
            Result reEnt = new Result();
            reEnt.IsSuccess = true;
            reEnt.Msg = "权限接口测试成功";
            return reEnt;
        }
    }
}