using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MooServer.Controllers
{
    public class PredictionServiceController : ApiController
    {
        PredictionService predictionService;

        public double getForecastCow(int idCow)
        {
            predictionService = new PredictionService();
            double forecastValue = predictionService.ForecastYieldCow(idCow);

            return forecastValue;
        }
    }
}
