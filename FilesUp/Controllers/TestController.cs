
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace FilesUp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public async Task<Result> SendCode(DtoKey inEnt)
        {
            Result reEnt = new Result();
            reEnt.IsSuccess = true;
            reEnt.Msg = "测试成功";
            return reEnt;
        }
    }
}