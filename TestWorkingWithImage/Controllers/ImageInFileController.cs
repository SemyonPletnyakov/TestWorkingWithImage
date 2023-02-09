using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestWorkingWithImage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageInFileController : ControllerBase
    {
        private IWebHostEnvironment _webHostEnvironment;
        public ImageInFileController (IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment; //загрузка окружения, чтобы указать путь, куда будет сохранятся картинка
        }
        [HttpGet]
        public VirtualFileResult GetImg(string name)
        {
            return File(name, "image/jpeg");
        }
        [HttpPost]
        public void CreateImg(IFormFile image)
        {
            if (image != null && image.Length >0)
            {
                using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\" + image.FileName))
                {
                    image.CopyTo(fileStream);
                    fileStream.Flush();
                    Response.StatusCode = 200;
                }
            }
            else Response.StatusCode = 400;
        }
    }
}
