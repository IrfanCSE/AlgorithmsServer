using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AlgorithmsServer.DTO;
using AlgorithmsServer.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlgorithmsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlgorithmController : ControllerBase
    {
        private readonly PythonScript _script;
        private readonly IWebHostEnvironment _host;
        public AlgorithmController(PythonScript script, IWebHostEnvironment host)
        {
            _host = host;
            _script = script;
        }

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> test(string key)
        {
            return Ok(KeyGenerator.Generator(key));
        }

        [HttpGet]
        [Route("EncryptionMessage")]
        public async Task<IActionResult> EncryptionMessage(string message, string key)
        {
            var path = PythonFile.AES;
            var className = "AES";
            var MethodName = "Call";
            key = KeyGenerator.Generator(key);

            var argg = new Object[] { message, key, (int)AesMode.MessageEncryption };
            try
            {
                var result = _script.RunFromFunc(path, className, MethodName, argg);
                var Response = new Response { Value = result.ToString() };
                return Ok(Response);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("DecryptionMessage")]
        public async Task<IActionResult> DecryptionMessage(string message, string key)
        {
            var path = PythonFile.AES;
            var className = "AES";
            var MethodName = "Call";
            key = KeyGenerator.Generator(key);

            var argg = new Object[] { message, key, (int)AesMode.MessageDecryption };
            try
            {
                var result = _script.RunFromFunc(path, className, MethodName, argg);
                var Response = new Response { Value = result.ToString() };
                return Ok(Response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("EncryptionImage")]
        public async Task<IActionResult> EncryptionImage(IFormFile file, string key)
        {

            try
            {
                if (file == null) return BadRequest("Empty File");
                if (file.ContentType != "image/png" && file.ContentType != "image/jpeg") return BadRequest("Invalid File Formate");

                var path = PythonFile.AES;
                var className = "AES";
                var MethodName = "CallImg";
                string name = "";

                key = KeyGenerator.Generator(key);

                var imgpath = Upload.UploadedFile(file, out name);

                var argg = new Object[] { imgpath, name, key, (int)AesMode.ImageEncryption };

                var result = _script.RunFromFunc(path, className, MethodName, argg);

                var getImage = System.IO.File.OpenRead(result.ToString());

                var upImg = new FileInfo(imgpath);
                if (upImg.Exists)
                {
                    upImg.Delete();
                }

                var encImg = new FileInfo(result.ToString());
                if (encImg.Exists)
                {
                    encImg.Delete();
                }

                return File(getImage, "image/jpeg");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("DecryptionImage")]
        public async Task<IActionResult> DecryptionImage(IFormFile file, string key)
        {

            try
            {
                if (file == null) return BadRequest("Empty File");
                if (file.ContentType != "image/png" && file.ContentType != "image/jpeg") return BadRequest("Invalid File Formate");

                var path = PythonFile.AES;
                var className = "AES";
                var MethodName = "CallImg";
                string name = "";

                key = KeyGenerator.Generator(key);

                var imgpath = Upload.UploadedFile(file, out name);

                var argg = new Object[] { imgpath, name, key, (int)AesMode.ImageDecryption };

                var result = _script.RunFromFunc(path, className, MethodName, argg);

                var getImage = System.IO.File.OpenRead(result.ToString());

                var upImg = new FileInfo(imgpath);
                if (upImg.Exists)
                {
                    upImg.Delete();
                }

                var encImg = new FileInfo(result.ToString());
                if (encImg.Exists)
                {
                    encImg.Delete();
                }

                return File(getImage, "image/jpeg");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}