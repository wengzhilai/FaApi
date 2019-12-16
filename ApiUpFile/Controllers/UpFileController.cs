
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;
using Models;
using Models.Entity;

namespace WebApi.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [Route("[controller]/[action]")]
    [EnableCors]
    [ApiController]
    public class UpFileController : ControllerBase
    {

        private readonly IWebHostEnvironment _env;
        /// <summary>
        /// 
        /// </summary>
        public UpFileController(
             IWebHostEnvironment hostingEnvironment)
        {
            _env = hostingEnvironment;
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(100_000_000)] //最大100m左右
        async public Task<ResultObj<FaFilesEntity>> UploadPhotos()
        {
            ResultObj<FaFilesEntity> reEnt = new ResultObj<FaFilesEntity>();

            var files = Request.Form.Files;
            var savePath = "wwwroot/UpFiles";
            var fileFolder = string.Format("{0}", DateTime.Now.ToString("yyyyMM"));
            if (!Directory.Exists(Path.Combine(savePath, fileFolder)))
                Directory.CreateDirectory(Path.Combine(savePath, fileFolder));

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = DateTime.Now.ToString("ddHHmmssfff") +
                                   Path.GetExtension(formFile.FileName);
                    var filePath = Path.Combine(fileFolder, fileName);
                    
                    var allPath = Path.Combine(_env.ContentRootPath, savePath , filePath);
                    using (var stream = new FileStream(allPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        reEnt.success = true;
                        reEnt.msg = filePath;
                        reEnt.data = new FaFilesEntity
                        {
                            name = fileName,
                            path = allPath,
                            url = filePath,
                            length = stream.Length,
                            uploadTime = DateTime.Now,
                            fileType = Path.GetExtension(formFile.FileName),
                        };
                        stream.Flush();
                    }
                }
            }
            return reEnt;
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