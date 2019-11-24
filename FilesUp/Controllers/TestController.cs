
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace FilesUp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "superadmin")]
        public async Task<Result> GetUser(DtoKey inEnt)
        {
            Result reEnt = new Result();
            reEnt.IsSuccess = true;
            reEnt.Msg = "User.Identity:" + TypeChange.ObjectToStr(User.Identity);
            reEnt.Msg += "User.Claims:" + TypeChange.ObjectToStr(from c in User.Claims select new { c.Type, c.Value });

            return reEnt;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<Result> Test(DtoKey inEnt)
        {
            Result reEnt = new Result();
            reEnt.IsSuccess = true;
            reEnt.Msg = "测试成功";
            return reEnt;
        }
    }
}