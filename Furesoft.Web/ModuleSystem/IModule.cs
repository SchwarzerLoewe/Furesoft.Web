using System;
using System.IO;

namespace Furesoft.Web.ModuleSystem
{
    public interface IModule
    {
        FunctionService GetFuntions();
        TypeService GetTypes();

        ModuleConfig GetConfig();

        bool Invoke(FileInfo fi, Uri url);
    }
  
    public class ModuleConfig
    {
    }
  
    public class TypeService
    {
    }
  
    public class FunctionService
    {
    }
}