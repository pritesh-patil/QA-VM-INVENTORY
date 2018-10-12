using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Approva.QA.Common;

namespace RegisterVMAsm
{
    public class RegAsm
    {

        public static bool registerAssemblies()
        {
            String assemblyPath = AppDomain.CurrentDomain.BaseDirectory + "Resources";
            String copyToNetFolder = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319";

            DirectoryInfo di = new DirectoryInfo(assemblyPath);
            FileInfo[] dlls = di.GetFiles();
            if (Directory.Exists(copyToNetFolder))
            {
                foreach (FileInfo dll in dlls)
                {
                    if (!String.IsNullOrEmpty(dll.Name.ToString()))
                    {
                        try
                        {

                           // File.Copy(Path.Combine(assemblyPath+"\\" + dll.Name.ToString()), Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\" + dll.Name.ToString()), true);
                            File.Copy(Path.Combine(assemblyPath + "\\" + dll.Name.ToString()), Path.Combine(copyToNetFolder + "\\" + dll.Name.ToString()), true);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteError("Error while copying DLL's" + ex.StackTrace);
                        }
                    }
                }
                return true;
            }
            else
            {
                Logger.WriteError(".NET framework 64 folder is missing");
                return false;
            }

        }
    }
}
