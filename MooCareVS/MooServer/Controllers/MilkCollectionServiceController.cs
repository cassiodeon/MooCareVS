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

        [HttpGet]
        public bool AddYieldMilkByDay(int idCow, double yieldMilk)
        {
            milkService = new MilkCollectionService();
            return milkService.AddYield(idCow, yieldMilk);
        }

        [HttpPost]
        public bool addYieldMilkByLactation(YieldMilkLactation yieldLactation)
        {
            PredictionService service = new PredictionService();
            service.ForecastYieldCow(1);
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
