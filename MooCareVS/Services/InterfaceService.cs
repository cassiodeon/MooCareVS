using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Repositories;
using System.Configuration;

namespace Services
{
    public class InterfaceService
    {
        CowRepository cowRepo;
        LactationRepository lactationRepo;
        NotificationRepository notificationRepo;
        PredictionService predictionService;

        public InterfaceService()
        {
            cowRepo = new CowRepository();
            lactationRepo = new LactationRepository();
            notificationRepo = new NotificationRepository();
            predictionService = new PredictionService();
        }

        public List<Cow> GetAllCows()
        {
            return cowRepo.GetAllCows().ToList();
        }

        public List<Lactation> GetLactationCow(int idCow)
        {
            return lactationRepo.GetLactationByCow(idCow).ToList();
        }

        public Lactation GetLactationById(int idLactation)
        {
            return lactationRepo.GetLactationById(idLactation);
        }

        public List<Notification> GetAllNotifications()
        {
            return notificationRepo.GetAllNotification().ToList();
        }

        public List<Notification> GetNotificationNotRead()
        {
            return notificationRepo.GetNotificationByRead(false).ToList();
        }

        public List<Notification> GetNotificationCow(int idCow)
        {
            return notificationRepo.GetNotificationByCow(idCow).ToList();
        }

        public double[] GetLactationEMA(int idLactation)
        {
            double[] lactationEMA;
            string result = string.Empty;
            //Parametros windowSize e idLactation
            int goahead = int.Parse(ConfigurationManager.AppSettings["goahead"]);
            string[] param = new string[] { goahead.ToString(), idLactation.ToString() };
            result = Helper.RScript.Run("lactation-EMA", param);
            if (result != "")
            {
                //Obtem os dados
                result = result.Replace('.', ',');
                string[] arrayResult = result.Replace("NA", "0").Split(' ');
                lactationEMA = Array.ConvertAll(arrayResult, Double.Parse);
            }
            else
            {
                lactationEMA = null;
            }

            return lactationEMA;
        }

        public double GetPrediction(int idCow)
        {
            double predictionValue;
            predictionValue = predictionService.ForecastValue(idCow);
            return predictionValue;
        }
    }
}
