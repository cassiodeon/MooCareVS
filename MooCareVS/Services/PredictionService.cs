using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;
using System.Diagnostics;
using System.IO;
using Repositories;
using System.Configuration;
using System.Data.SqlClient;
using Domain.Entities;

namespace Services
{
    public class PredictionService
    {
        CowRepository repoCow;
        NotificationService notificationService;

        public PredictionService()
        {
            repoCow = new CowRepository();
            notificationService = new NotificationService();
        }

        public double ForecastYieldCow(int idCow)
        {
            double forecastValue = 0;
            
            //Verifica se a vaca existe
            Cow cowValid = repoCow.GetCow(idCow);
            if (cowValid != null)
            {
                Lactation currentLact = cowValid.lactations.FirstOrDefault(l => l.finished == false);
                int numYields = currentLact.yields.ToList().Count;
                int minRecords = 0;
                int.TryParse(ConfigurationManager.AppSettings["minRecords"], out minRecords);

                //Verifica se existe registros suficientes
                if (numYields >= minRecords)
                    //Executa o script em R que calcula o valor de previsão
                    forecastValue = ForecastValue(idCow);
                else
                    throw new Exception("Number of records is insufficient");

                //Task.Run(() => notificationService.ValidateDataCow(idCow, currentLact, forecastValue));
                //CHAMA SERVIÇO DE NOTIFICAÇÃO
                notificationService.ValidateDataCow(idCow, currentLact, forecastValue);
            }
            
            return forecastValue;
        }

        public double ForecastValue(int idCow)
        {
            double forecastValue = 0;
            //string rScript = "";
            //string dataYield = "";

            string goahead = ConfigurationManager.AppSettings["goahead"];
            //string host = ConfigurationManager.AppSettings["server"];
            //string port = ConfigurationManager.AppSettings["port"];
            //string user = ConfigurationManager.AppSettings["user"];
            //string password = ConfigurationManager.AppSettings["password"];
            //string database = ConfigurationManager.AppSettings["database"];

            //string con = $"dbname = \"{database}\",host = \"{host}\", port = {port},user = \"{user}\", password = \"{password}\"";

            //rScript += "library(forecast)" + Environment.NewLine;
            //rScript += "library(TTR)" + Environment.NewLine;
            //rScript += "library(RPostgreSQL)" + Environment.NewLine;

            //rScript += "drv <- dbDriver(\"PostgreSQL\")" + Environment.NewLine;
            //rScript += "con <- dbConnect(drv, " + con + ")" + Environment.NewLine;
            //rScript += "rs <- dbGetQuery(con, \"SELECT * from \\\"vYieldsCow\\\" where \\\"idCow\\\" = " + idCow + " ORDER BY dateyield; \")" + Environment.NewLine;

            //rScript += "yield_ajust_ema <- EMA(rs$yield, 7)" + Environment.NewLine;
            //rScript += "fit <- auto.arima(yield_ajust_ema)" + Environment.NewLine;
            //rScript += "result <- forecast(fit, h = " + goahead + ")" + Environment.NewLine;
            //rScript += "result$mean[" + goahead + "]" + Environment.NewLine;

            ///dataYield = "yield <- c(25.89,23.54,24.01,24.13,21.04,21.00,26.69,23.07,26.60,21.75,24.19,23.07,28.42,29.14,18.58,31.33,19.44,20.72,29.17,25.76,20.86,24.46,17.72,31.33,21.14,19.11,26.05,16.95,15.10,17.29,15.33,10.10,10.27,12.74,7.90,7.89,9.08,7.10,6.33,1.34,18.71,15.77,28.51,26.62,16.55,29.99,24.95,28.34,36.15,21.27,30.45,28.15,21.18,32.34,25.02,33.17,33.32,25.04,42.10,39.58,34.00,34.72,26.59,43.75,23.08,48.18,31.74,38.97,36.01,39.20,36.06,51.64,37.07,37.44,42.25,31.54,37.61,48.71,32.95,31.54,41.45,50.84,28.03,50.08,43.27,35.72,29.13,49.53,32.97,27.91,50.01,38.60,39.20,30.56,43.87,37.16,40.58,23.21,53.93,33.74,42.16,40.61,41.25,39.01,32.46,52.51,42.72,35.30,45.07,37.88,32.11,44.99,41.37,36.42,46.23,43.20,30.51,56.93,32.00,56.85,44.01,44.87,50.93,46.45,38.45,52.95,41.78,38.60,26.46,54.87,28.98,33.72,44.42,28.31,34.57,33.74,43.64,27.47,27.51,23.91,36.93,31.49,43.43,38.53,34.29,35.48,46.55,30.39,43.44,39.43,42.26,49.01,26.24,40.58,39.08,25.45,35.55,35.93,48.01,31.90,43.24,43.32,31.24,41.65,37.04,49.36,29.07,43.48,28.64,37.93,44.81,33.29,37.09,39.48,38.43,29.38,39.65,27.52,38.81,40.26,28.16,34.80,24.77,23.13,5.32,21.09,27.43,20.81,14.87,23.44,24.83,28.10,24.51,20.56,25.88,25.99,37.71,27.46,19.37,37.13,35.71,29.12,20.17,44.79,31.47,24.75,30.70,35.59,41.47,25.69,29.33,35.33,25.13,37.66,33.48,28.75,39.29,32.19,25.37,26.17,33.54,31.22,23.96,29.29,35.55,20.23,30.53,32.42,30.00,28.23,35.86,21.25,37.17,22.34,33.09,28.71,23.25,35.20,25.63,20.77,30.08,34.87,21.91,27.73,30.99,18.25,28.98,25.21,26.43,29.56,28.49,18.37,34.82,18.25,32.45,33.46,27.43,28.68,32.77,26.88,30.46,14.79,34.59,20.65,26.78,25.59,22.46,34.31,24.40,18.15,17.96,30.59,16.35,27.21,25.97,24.74,21.51,21.69,31.05,16.80,21.21,21.74,24.51,19.47,22.66,22.53,24.67,25.25,24.36,10.10,24.91,33.99,18.59,25.18,30.55,23.66,23.34,24.41,23.87,23.87,23.54,21.57,28.98,24.90,25.84,32.52,25.52,28.52,32.99,31.16,28.78,25.89,26.67,23.06,34.26,17.18,29.11,35.51,18.72,31.03,29.60,25.77,29.75,28.86,27.95,27.83,11.03,32.28,26.57,20.21,36.54,31.71,18.70,26.18,24.00,26.84,28.15,22.14,19.81,25.69,24.98,21.96,25.59,29.33,19.90,24.41,29.52,20.17,23.87,22.67,26.24,27.05,13.21,20.07,19.70,18.82,15.00,9.64,15.30,16.42,8.94,13.11,13.42,18.86,7.12,10.61,10.38,4.56,20.17,27.83,19.98,21.34,28.88,26.95)";
            //rScript += dataYield + Environment.NewLine;
            //rScript += "yield <- EMA(yield,7)" + Environment.NewLine;
            //rScript += "fit <- auto.arima(yield)" + Environment.NewLine;
            //rScript += "result <- forecast(fit, h=7)" + Environment.NewLine;

            //bool saved = Helper.RScript.Save(rScript, "yield", true);
            //if (saved)
            //{
                string[] param = new string[] { idCow.ToString(), goahead };
                string result = Helper.RScript.Run("prediction-yield", param);
                result = result.Substring(result.IndexOf(" ") + 1).Replace(Environment.NewLine, "");
                if (result != "")
                    forecastValue = double.Parse(result, new System.Globalization.CultureInfo("en-US"));
            //}

            return forecastValue;
        }
        
    }
}
