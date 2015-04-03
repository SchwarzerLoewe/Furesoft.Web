using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using Furesoft.Web.Internal;
using Furesoft.Web.Modules;

namespace Furesoft.Web
{
    public class AssemblyInitializer
    {
        public static Assembly Init(CodeDomProvider bcp, string src, StreamWriter sw, IniFile ini)
        {
            var options = new CompilerParameters();

            options.GenerateExecutable = false;
            options.GenerateInMemory = true;

            options.ReferencedAssemblies.AddRange(new[] { "System.dll", "System.Core.dll", typeof(IScriptLanguage).Assembly.Location, typeof(Mono.Net.HttpListener).Assembly.Location });

            foreach (var refs in ini.GetSection("References"))
            {
                options.ReferencedAssemblies.Add(refs.Value);
            }

            var res = bcp.CompileAssemblyFromSource(options, src);

            if (res.Errors.HasErrors)
            {
                if (sw != null)
                {
                    foreach (CompilerError item in res.Errors)
                    {
                        LoggerModule.Log(item.Line + ": " + item.ErrorText);
                    }
                }
            }
            else
            {
                return res.CompiledAssembly;
            }

            return null;
        }
    }
}