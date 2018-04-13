using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;
using System.Diagnostics;

namespace Services
{
    public class PredictionService
    {
        public Process proc;

        public void test()
        {
            REngine.SetEnvironmentVariables();
            using (REngine engine = REngine.GetInstance())
            {

                engine.Initialize();

                NumericVector testGroup = engine.CreateNumericVector(new double[] { 30.02, 29.99, 30.11, 29.97, 30.01, 29.99 });
                engine.SetSymbol("testGroup", testGroup);
                engine.Evaluate("testTs <- c(testGroup)");
                NumericVector ts = engine.GetSymbol("testTs").AsNumeric();

                engine.Evaluate("tsValue <- ts(testTs, frequency=1)");
                engine.Evaluate("library(forecast)");
                engine.Evaluate("arimaFit <- auto.arima(tsValue)");
                engine.Evaluate("fcast <- forecast(tsValue, h=5)");
                //engine.Evaluate("plot(fcast)");

                NumericVector nv = engine.GetSymbol("fcast").AsNumeric();
            }
        }

        public void teste2()
        {
            proc = new Process();
            ProcessStartInfo si = new ProcessStartInfo();
            si.FileName = "C:\\Program Files\\R\\R-3.4.2\\bin\\RScript.exe";
            si.Arguments = "C:\\Temp\\script.r";
            si.CreateNoWindow = true;
            si.RedirectStandardOutput = true;
            si.UseShellExecute = false;

            proc = Process.Start(si);
            proc.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
            proc.Exited += new EventHandler(process_Exited);
            proc.EnableRaisingEvents = true;
            proc.BeginOutputReadLine();
            //ProcessStartInfo t = new ProcessStartInfo("C:\\Program Files\\R\\R-3.4.2\\bin\\RScript.exe", "C:\\Temp\\script.r");
            //Process p = new Process();
            //p.StartInfo = t;
            //p.Start();            
            //var saida = p.StandardOutput;
        }

        private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
                get_valor(e.Data);
        }

        private void process_Exited(object sender, EventArgs e)
        {
            proc.OutputDataReceived -= new DataReceivedEventHandler(process_OutputDataReceived);
            proc.Exited -= new EventHandler(process_Exited);
            proc.Close();
            proc.Dispose();
            proc = null;
        }

        private void get_valor(string valor)
        {
            var teste = valor;

        }
    }
}
