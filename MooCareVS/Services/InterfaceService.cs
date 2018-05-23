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
        FoodService foodService;

        public InterfaceService()
        {
            cowRepo = new CowRepository();
            lactationRepo = new LactationRepository();
            notificationRepo = new NotificationRepository();
            predictionService = new PredictionService();
            foodService = new FoodService();
        }

        public List<Cow> GetAllCows(string[] includes = null)
        {
            return cowRepo.GetAllCows(includes).ToList();
        }

        public List<Cow> GetCowsByStatus(bool deleted)
        {
            return cowRepo.GetCowByQuery(c => c.deleted == deleted).ToList();
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

        public string[] GetLactationEMA(int idLactation)
        {
            double[] lactationEMA = null;
            string[] lactation = null;
            string result = string.Empty;
            //Parametros windowSize e idLactation
            int goahead = int.Parse(ConfigurationManager.AppSettings["goahead"]);
            string[] param = new string[] { goahead.ToString(), idLactation.ToString() };
            result = Helper.RScript.Run("lactation-EMA", param);
            if (result != "")
            {
                //Obtem os dados
                //result = result.Replace('.', ',');
                //string[] arrayResult = result.Replace("NA", "0").Split(' ');
                lactation = result.Split(' ');
                //lactationEMA = Array.ConvertAll(arrayResult, Double.Parse);
            }

            return lactation;
        }

        public double GetPrediction(int idCow)
        {
            double predictionValue;
            predictionValue = predictionService.ForecastValue(idCow);
            return predictionValue;
        }

        public List<Food> GetAllFoodByCow(int idCow)
        {
            return foodService.GetAllFoodByCow(idCow);
        }

        public List<Food> GetFoodByLactation(int idLactation)
        {
            return foodService.GetFoodByLactation(idLactation);
        }

        public double GetFoodByCowToday(int idCow)
        {
            return foodService.GetFoodByCowToday(idCow);
        }

        public List<Cow> GetCowsWithNotification()
        {
            return notificationRepo.GetCowsWithNotification().ToList();
        }
    }
}
