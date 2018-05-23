using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using System.Net.Http;
using Domain.Entities;
using System.Configuration;

namespace Services
{
    public class NotificationService
    {
        CowRepository cowRepo;
        LactationRepository lactationRepo;
        NotificationRepository notificationRepo;

        public NotificationService()
        {
            cowRepo = new CowRepository();
            lactationRepo = new LactationRepository();
            notificationRepo = new NotificationRepository();
        }

        public void ValidateDataCow(int idCow, Lactation currentLactation, double forecastValue)
        {
            VerifyPredictionLactation(idCow, currentLactation, forecastValue);
            VerifyCurrentLactation(idCow, currentLactation);
        }
        
        private void VerifyCurrentLactation(int idCow, Lactation currentLactation)
        {
            Lactation lastCompleteLactation = lactationRepo.GetLastLactationFinished(idCow, new string[] { "yields" });
            //Lactation currentLactation = lactationRepo.GetLactationByQuery(l => l.finished == false, new string[] { "yields" }).FirstOrDefault(); //GetLastLactationFinished(idCow, new string[] { "yields" });
            
            int currentDayLactation = currentLactation.yields.Max(y => y.dayLactation); //Receber como parâmetro???

            int indexYieldLastLactation = lastCompleteLactation.yields.ToList().IndexOf(lastCompleteLactation.yields.FirstOrDefault(y => y.dayLactation == currentDayLactation));
            int indexCurrentLastLactation = currentLactation.yields.ToList().IndexOf(currentLactation.yields.FirstOrDefault(y => y.dayLactation == currentDayLactation));

            int goahead = int.Parse(ConfigurationManager.AppSettings["goahead"]);
            
            //Excuta script em R para calcular o EMA das lactações
            string resultLastLactation = string.Empty;
            string resultCurrentLactation = string.Empty;
            
            //Parametros windowSize e idLactation para última lactação
            string[] param = new string[] { "5", lastCompleteLactation.idLactation.ToString() };
            resultLastLactation = Helper.RScript.Run("lactation-EMA", param);
            
            //Parametros windowSize e idLactation para lactação atual
            param = new string[] { "5", currentLactation.idLactation.ToString() };
            resultCurrentLactation = Helper.RScript.Run("lactation-EMA", param);
            if (resultLastLactation != "" && resultCurrentLactation != "")
            {
                //Obtem os dados
                string[] arrayResultLastLactation = resultLastLactation.Replace("NA", "0").Split(' ');
                string[] arrayResultCurrentLactation = resultCurrentLactation.Replace("NA", "0").Split(' ');

                //Obtem o valor da produção com EMA para o período do mesmo dia de lactação atual
                double yieldEMALastLactation = double.Parse(arrayResultLastLactation[indexYieldLastLactation], new System.Globalization.CultureInfo("en-US"));
                double yieldEMACurrentLactation = double.Parse(arrayResultCurrentLactation[indexCurrentLastLactation], new System.Globalization.CultureInfo("en-US"));
                double threshould = (double.Parse(ConfigurationManager.AppSettings["threshould"]) / 100); //0.2;

                //Verifica se a produção atual da lactação corrente é inferior a produção da lactação anterior aplicado um threshould
                if (yieldEMACurrentLactation < (yieldEMALastLactation - yieldEMALastLactation * threshould))
                {
                    //NOTIFICAÇÃO!
                    AddNotification("CURRENT", currentLactation.idLactation, "");
                    throw new Exception("NOTIFICATION - BAD CURRENT LACTATION");
                }
            }
        }

        private void VerifyPredictionLactation(int idCow, Lactation currentLactation, double forecastValue)
        {
            //Obtem a última lactação para comparação
            Lactation lastLactation = lactationRepo.GetLastLactationFinished(idCow, new string[]{"yields"});
            int currentDayLactation = currentLactation.yields.Max(y => y.dayLactation); //Receber como parâmetro???

            //int currentDayLactation = 27; //Receber como parâmetro???
            int goahead = int.Parse(ConfigurationManager.AppSettings["goahead"]);
            int dayPredicted = currentDayLactation + goahead;
            
            int indexYield = lastLactation.yields.ToList().IndexOf(lastLactation.yields.FirstOrDefault(y => y.dayLactation == dayPredicted));

            //Excuta script em R para calcular o EMA da última lactação
            string result = string.Empty;
            //Parametros windowSize e idLactation
            string[] param = new string[] { goahead.ToString(), lastLactation.idLactation.ToString() };
            result = Helper.RScript.Run("lactation-EMA", param);
            if (result != "")
            {
                //Obtem os dados
                string[] arrayResult = result.Replace("NA", "0").Split(' ');

                //Obtem o valor da produção para o mesmo período  mesmo dia da lactação
                double yieldEMALastLactation = double.Parse(arrayResult[indexYield], new System.Globalization.CultureInfo("en-US"));
                double threshould = (double.Parse(ConfigurationManager.AppSettings["threshould"])/100); //0.2;

                //Verifica se a produção prevista para a lactação corrente é inferior a produção da lactação anterior aplicado um threshould
                if (forecastValue < (yieldEMALastLactation - yieldEMALastLactation * threshould))
                {
                    //NOTIFICAÇÃO!
                    AddNotification("PREDICTION", currentLactation.idLactation, "");
                    throw new Exception("NOTIFICATION - BAD PREDICTION");
                }
            }
        }

        public void AddNotification(string type, int idLactation, string description = "")
        {
            Notification notification = new Notification()
            {
                type = type,
                idLactation = idLactation,
                read = false,
                description = description
            };
            notificationRepo.AddNotification(notification);
        }

        public void UpdateNotification(Notification notification)
        {
            notificationRepo.UpdateNotification(notification);
        }
        
    }
}
