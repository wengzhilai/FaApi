
using System;
using System.Threading.Tasks;
using IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entity;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UpFileController : ControllerBase
    {

        private readonly IWebHostEnvironment _env;
        private IFileRepository _file;
        IPublicRepository _public;
        /// <summary>
        /// 
        /// </summary>
        public UpFileController(
            IPublicRepository pub,
            IFileRepository file,
             IWebHostEnvironment hostingEnvironment)
        {
            _file = file;
            _public = pub;
            _env = hostingEnvironment;
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
            if (fileEnt == null || string.IsNullOrEmpty( fileEnt.PATH))
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

    }
}