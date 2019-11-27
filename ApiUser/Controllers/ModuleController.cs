using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using IRepository;
using System;
using Helper;
using Microsoft.AspNetCore.Cors;

namespace ApiUser.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors]
    [Authorize]
    public class ModuleController : ControllerBase
    {
        IModuleRepository _module;

        public ModuleController(IModuleRepository module)
        {
            this._module = module;
        }

        [HttpPost]
        async public Task<Result> GetUserMenu()
        {
            var reObj = new Result();
            try
            {
                var userId = User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value;
                reObj = await this._module.GetMGetMenuByUserId(Convert.ToInt32(userId));
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户详情失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }
    }
}