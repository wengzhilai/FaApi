using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json.Linq;

namespace ApiSms.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public async Task<ResultObj<String>> UserLogin(LogingDto inEnt)
        {
            ResultObj<String> reobj = new ResultObj<String>();
            
            return reobj;
        }


    }
}