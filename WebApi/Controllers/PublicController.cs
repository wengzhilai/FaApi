using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Comon;
using WebApi.Model;
using IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models.Entity;
using WebApi.Model.InEnt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApi.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PublicController : ControllerBase
    {

        private readonly IHostingEnvironment _env;
        IPublicRepository _public;
        /// <summary>
        /// 
        /// </summary>
        public PublicController(IPublicRepository pub,IHostingEnvironment hostingEnvironment)
        {
            _public=pub;
            _env = hostingEnvironment;
        }

        /// <summary>
        /// 发送验证码到手机
        /// <para>发送时会在用户的Login表里修改VERIFY_CODE，并在fa_sms_send增加记录</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result> SendCode(DtoKey inEnt)
        {
            Result reEnt = new Result();
            if(!inEnt.Key.IsPhoneNumber()){
                reEnt.IsSuccess=false;
                reEnt.Msg="电话号码格式有误";
                return reEnt;
            }
            var code= PicFunHelper.ValidateMake(4);
            var t=await _public.SmsSendCode(inEnt.Key,code);
            // dynamic reEnt = await Task.Run(() => Fun<ErrorInfo>.Func(api.PublicApi.SendCode, ref err, inEnt));
            // if (err.IsError) return err;
            return reEnt;
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [RequestSizeLimit(100_000_000)] //最大100m左右
        public async Task<Result> UploadPhotos(List<IFormFile> files)
        {
            Result reEnt = new Result();
            long size = files.Sum(f => f.Length);
            // var fileFolder = Path.Combine(_env.ContentRootPath, "UpFiles");
            var fileFolder = "UpFiles";

            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                                   Path.GetExtension(formFile.FileName);
                    var filePath = Path.Combine(fileFolder, fileName);
                    var allPath=Path.Combine(_env.ContentRootPath, filePath);
                    using (var stream = new FileStream(allPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        reEnt.IsSuccess=true;
                        reEnt.Msg=filePath;
                        stream.Flush();
                    }
                }
            }
            return reEnt;
        }

        /// <summary>
        /// 查看文件 key为文件id
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public Result Lookfile(DtoKey inEnt){
            Result reObj=new Result();
            return reObj;
        }

        /// <summary>
        /// 检测版本 key为当前版本号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public Result CheckUpdate(DtoKey inEnt){
            Result reObj=new Result();
            return reObj;
        }

        /// <summary>
        /// 获取阴历 key为时间字符串
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result GetLunarDate(DtoKey inEnt){
            Result reObj=new Result();
            return reObj;
        }

        /// <summary>
        /// 获取阳历 key为时间字符串
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result GetSolarDate(DtoKey inEnt){
            Result reObj=new Result();
            return reObj;
        }


    }
}