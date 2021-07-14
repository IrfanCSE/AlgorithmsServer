using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgorithmsServer.DTO;
using AlgorithmsServer.Helper;
using Microsoft.AspNetCore.Mvc;

namespace AlgorithmsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlgorithmController : ControllerBase
    {
        private readonly PythonScript _script;
        public AlgorithmController(PythonScript script)
        {
            _script = script;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            var path = PythonFile.Test;
            var input = new List<KeyValue>();
            var output = new List<KeyValue>();
            output.Add(new KeyValue { Key = "output",Value="c#" });

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
            var argg = new Object[]{"C#"};
            // argg[0]="c#";
            try
            {
                var result = _script.RunFromFunc(path, className, MethodName,argg);
                return Ok(result.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}