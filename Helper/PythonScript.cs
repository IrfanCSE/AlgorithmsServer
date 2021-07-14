using System.Collections.Generic;
using System.IO;
using System.Text;
using AlgorithmsServer.DTO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace AlgorithmsServer.Helper
{
    public class PythonScript
    {
        private ScriptEngine _engine;

        public PythonScript()
        {
            _engine = Python.CreateEngine();
        }

        public List<KeyValue> RunFromString(string PythonPath, List<KeyValue> Input, List<KeyValue> Output)
        {
            // for easier debugging write it out to a file and call: _engine.CreateScriptSourceFromFile(filePath);
            var source = _engine.CreateScriptSourceFromFile(PythonPath);

            Input.ForEach(x =>
            {
                _engine.GetSysModule().SetVariable(x.Key, x.Value);
            });
            // _engine.GetSysModule().SetVariable("variableName", 1);

            var eIo = _engine.Runtime.IO;

            var error = new MemoryStream();
            var result = new MemoryStream();

            eIo.SetErrorOutput(error, Encoding.Default);
            eIo.SetOutput(result, Encoding.Default);

            CompiledCode cc = source.Compile();

            ScriptScope scope = _engine.CreateScope();

            cc.Execute(scope);

            Output.ForEach(x =>
            {
                x.Value = scope.GetVariable<object>(x.Key);
            });

            return Output;
            // return scope.GetVariable<TResult>(variableName);
        }

        public object RunFromFunc(string PythonPath, string Class, string Method, params dynamic[] arguments)
        {
            var source = _engine.CreateScriptSourceFromFile(PythonPath);

            CompiledCode cc = source.Compile();

            ScriptScope scope = _engine.CreateScope();

            cc.Execute(scope);

            var pythonClass = _engine.Operations.Invoke(scope.GetVariable(Class));

            return _engine.Operations.InvokeMember(pythonClass, Method,arguments);

        }

        //now creating an object that could be used to access the stuff inside a python script
        // pythonClass = engine.Operations.Invoke(scope.GetVariable(className));

        // public void SetVariable(string variable, dynamic value)
        // {
        //     scope.SetVariable(variable, value);
        // }

        // public dynamic GetVariable(string variable)
        // {
        //     return scope.GetVariable(variable);
        // }

        // public void CallMethod(string method, params dynamic[] arguments)
        // {
        //     engine.Operations.InvokeMember(pythonClass, method, arguments);
        // }

        // public dynamic CallFunction(string method, params dynamic[] arguments)
        // {
        //     return engine.Operations.InvokeMember(pythonClass, method, arguments);
    }

}