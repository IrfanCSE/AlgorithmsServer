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
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            var path = PythonFile.Test;
            var input = new List<KeyValue>();
            var output = new List<KeyValue>();
            output.Add(new KeyValue { Key = "output", Value = "c#" });

            var result = _script.RunFromString(path, input, output);
            var value = result.Find(x => x.Key == "output");
            return Ok(value.Value.ToString());
        }

        [HttpGet]
        [Route("getFunc")]
        public async Task<IActionResult> getFunc()
        {
            var path = PythonFile.Test;
            var className = "Main";
            var MethodName = "fun";
            var argg = new Object[] { "C#" };
            // argg[0]="c#";
            try
            {
                var result = _script.RunFromFunc(path, className, MethodName, argg);
                return Ok(result.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("EncryptionMessage")]
        public async Task<IActionResult> EncryptionMessage(string message, string key)
        {
            var path = PythonFile.AES;
            var className = "AES";
            var MethodName = "Call";
            var argg = new Object[] { message, key, (int)AesMode.MessageEncryption };
            try
            {
                var result = _script.RunFromFunc(path, className, MethodName, argg);
                return Ok(result);

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
            var argg = new Object[] { message, key, (int)AesMode.MessageDecryption };
            try
            {
                var result = _script.RunFromFunc(path, className, MethodName, argg);
                // result = result.ToString().Replace("\0","");
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("EncryptionImage")]
        public async Task<IActionResult> EncryptionImage(IFormFile image, string key)
        {

            try
            {
                if (image.ContentType != "image/png") return BadRequest("Invalid File Formate");

                var path = PythonFile.AES;
                var className = "AES";
                var MethodName = "CallImg";
                string name = "";

                var imgpath = Upload.UploadedFile(image, out name);

                var argg = new Object[] { imgpath, name.ToString(), key, (int)AesMode.ImageEncryption };

                var result = _script.RunFromFunc(path, className, MethodName, argg);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("DecryptionImage")]
        public async Task<IActionResult> DecryptionImage(IFormFile image, string key)
        {

            try
            {
                if (image.ContentType != "image/png") return BadRequest("Invalid File Formate");

                var path = PythonFile.AES;
                var className = "AES";
                var MethodName = "CallImg";
                string name = "";

                var imgpath = Upload.UploadedFile(image, out name);

                var argg = new Object[] { imgpath, name, key, (int)AesMode.ImageDecryption };

                var result = _script.RunFromFunc(path, className, MethodName, argg);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}