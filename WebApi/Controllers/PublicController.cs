
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Comon;

using IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models.Entity;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;
using System.Globalization;

namespace WebApi.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PublicController : ControllerBase
    {

        private readonly IWebHostEnvironment _env;
        private IFileRepository _file;
        IPublicRepository _public;
        /// <summary>
        /// 
        /// </summary>
        public PublicController(
            IPublicRepository pub,
            IFileRepository file,
             IWebHostEnvironment hostingEnvironment)
        {
            _file = file;
            _public = pub;
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
            if (string.IsNullOrEmpty(inEnt.Key) || !inEnt.Key.IsPhoneNumber())
            {
                reEnt.IsSuccess = false;
                reEnt.Msg = "电话号码格式有误";
                return reEnt;
            }
            reEnt = await _public.SendCode(inEnt.Key);

            return reEnt;
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [RequestSizeLimit(100_000_000)] //最大100m左右
        async public Task<Result<FaFilesEntity>> UploadPhotos()
        {
            Result<FaFilesEntity> reEnt = new Result<FaFilesEntity>();

            var files = Request.Form.Files;
            var fileFolder = string.Format("{0}", DateTime.Now.ToString("yyyyMM"));
            if (!Directory.Exists(Path.Combine("UpFiles", fileFolder)))
                Directory.CreateDirectory(Path.Combine("UpFiles", fileFolder));

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = DateTime.Now.ToString("ddHHmmssfff") +
                                   Path.GetExtension(formFile.FileName);
                    var filePath = Path.Combine(fileFolder, fileName);
                    var allPath = Path.Combine(_env.ContentRootPath, "UpFiles/" + filePath);
                    using (var stream = new FileStream(allPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        reEnt.IsSuccess = true;
                        reEnt.Msg = filePath;
                        reEnt.Data = new FaFilesEntity
                        {
                            NAME = fileName,
                            PATH = allPath,
                            URL = "api/Public/LookfileByPath/" + filePath,
                            LENGTH = stream.Length,
                            UPLOAD_TIME = DateTime.Now,
                            FILE_TYPE = Path.GetExtension(formFile.FileName),
                        };
                        stream.Flush();
                    }
                }
            }
            return reEnt;
        }

        /// <summary>
        /// 查看文件 key为文件id
        /// 测试地址：http://localhost:5000/api/Public/Lookfile/a.jpg
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{fileId}.jpg")]
        async public Task<IActionResult> Lookfile(int fileId)
        {
            Response.Body.Dispose();
            var fileEnt = await _file.SingleByKey(fileId);
            if (fileEnt == null || fileEnt.PATH.IsNullOrEmpty())
            {
                return File(System.IO.File.ReadAllBytes(_env.ContentRootPath + "/assets/images/defaultPng.png"), @"image/png");
            }
            else
            {
                if (string.IsNullOrEmpty(fileEnt.FILE_TYPE))
                {
                    fileEnt.FILE_TYPE = "png";
                }
                else
                {
                    fileEnt.FILE_TYPE = fileEnt.FILE_TYPE.Replace(".", "");
                }
                return File(System.IO.File.ReadAllBytes(fileEnt.PATH), @"image/" + fileEnt.FILE_TYPE);
            }
        }

        /// <summary>
        /// 根据路径查看图片
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{dir}/{fileName}.{type}")]
        public IActionResult LookfileByPath(string dir, string fileName, string type)
        {
            Response.Body.Dispose();
            var allPath = Path.Combine(_env.ContentRootPath, "UpFiles", dir, fileName + "." + type);
            return File(System.IO.File.ReadAllBytes(allPath), @"image/png");
        }


        /// <summary>
        /// 检测版本 key为当前版本号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result<FaAppVersionEntity>> CheckUpdate(DtoDo<int> inEnt)
        {
            Result<FaAppVersionEntity> reObj = new Result<FaAppVersionEntity>();
            var dapper=new DapperHelper<FaAppVersionEntity>();
            reObj.Data=await dapper.Single(i=>i.TYPE>inEnt.Key,"order by id desc");
            return reObj;
        }

        /// <summary>
        /// 获取阴历 key为时间字符串
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Result GetLunarDate(DtoKey inEnt)
        {
            var reObj = new Result();
            try
            {
                if (string.IsNullOrEmpty(inEnt.Key)) return reObj;
                inEnt.Key = inEnt.Key.Replace("T", " ");
                DateTime datetime = Convert.ToDateTime(inEnt.Key);
                return _public.GetLunarDate(datetime);
            }
            catch (ExceptionExtend e)
            {
                reObj.IsSuccess = false;
                reObj.Code = e.RealCode;
                reObj.Msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取阳历 key为时间字符串
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Result GetSolarDate(DtoKey inEnt)
        {
            var reObj = new Result();
            try
            {
                if (string.IsNullOrEmpty(inEnt.Key)) return reObj;
                inEnt.Key = inEnt.Key.Replace("T", " ");
                DateTime datetime = Convert.ToDateTime(inEnt.Key);
                return _public.GetSolarDate(datetime);
            }
            catch (ExceptionExtend e)
            {
                reObj.IsSuccess = false;
                reObj.Code = e.RealCode;
                reObj.Msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;
        }


    }
}