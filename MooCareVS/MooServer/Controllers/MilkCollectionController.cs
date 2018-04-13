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
    public class MilkCollectionController : ApiController
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
            //ServiceMilkCollection.AddYield(yieldLactation);
            if(yieldLactation == null)
            {
                return false;
            }
            return true;
        }
    }
}
