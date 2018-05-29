using Domain.Entities;
using MooServer.Models.DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MooServer.Controllers
{
    public class MilkCollectionServiceController : ApiController
    {
        MilkCollectionService milkService;
        PredictionService predictionService;

        public MilkCollectionServiceController()
        {
            milkService = new MilkCollectionService();
            predictionService = new PredictionService();
        }

        [HttpGet]
        public bool AddYieldMilkByDay(int idCow, double yieldMilk, DateTime date)
        {
            try
            {
                bool insert = milkService.AddYield(idCow, yieldMilk, date);
                predictionService.ForecastYieldCow(idCow);

                return insert;
            }
            catch (Exception)
            {
                return false;
            }
            

            
        }

        public bool AddYieldMilkToday(int idCow, double yieldMilk)
        {
            milkService = new MilkCollectionService();
            return milkService.AddYield(idCow, yieldMilk);
        }

        [HttpPost]
        public bool addYieldMilkByLactation(YieldMilkLactation yieldLactation)
        {
            predictionService.ForecastYieldCow(1);
            List<Yield> yields = new List<Yield>(); //= yieldLactation.yieldLactation.Cast<Yield>().ToList();
            //ServiceMilkCollection.AddYield(yieldLactation);
            if (yields == null)
            {
                return false;
            }
            return true;
        }
    }
}
