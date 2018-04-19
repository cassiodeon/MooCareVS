using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public static class RScript
    {
        static string rCodeFilePath = $"C:\\temp\\R\\";
        static string rScriptExecutablePath = @"C:\Program Files\R\R-3.4.2\bin\Rscript.exe";

        public static string Run(string fileName, string[] param = null)
        {
            //string rCodeFilePath = $"D:\\DemoApps\\R\\NorthwindDashboard\\Analytics\\{filename}.R";
            string rCodeFile = $"{rCodeFilePath}{fileName}.R";
            if (param != null)
                rCodeFile = $"{rCodeFile} {string.Join(" ",param)}";

            string result = string.Empty;

            try
            {
                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName = rScriptExecutablePath,
                    WorkingDirectory = Path.GetDirectoryName(rScriptExecutablePath),
                    Arguments = rCodeFile,
                    RedirectStandardInput = false,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };


                using (Process proc = new Process())
                {
                    proc.StartInfo = info;
                    proc.Start();
                    result = proc.StandardOutput.ReadToEnd();
                    proc.Close();
                }

                return result;
            }
            catch (Exception ex)
            {
                //return false;
                throw new Exception("Falha ao executar a previsão. Detalhes: R Script failed: " + result, ex);

            }
        }

        public static bool Save(string script, string fileName, bool replaceFile = false)
        {

            string rCodeFile = $"{rCodeFilePath}{fileName}.R";

            if (replaceFile && File.Exists(rCodeFile))
                File.Delete(rCodeFile);

            if (!File.Exists(rCodeFile))
                File.WriteAllText(rCodeFile, script);
            else
                return false; //File already exists

            return true;
        }
    }
}
