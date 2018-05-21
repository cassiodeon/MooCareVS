using Domain.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooServer.Controllers
{
    public class CowController : Controller
    {
        // GET: Cow
        InterfaceService interfaceService;
        public CowController()
        {
            interfaceService = new InterfaceService();
        }
        public ActionResult Index()
        {
            List<Cow> cows = interfaceService.GetAllCows();
            return View(cows);
        }

        public ActionResult Details(int id)
        {
            List<Lactation> lactations = interfaceService.GetLactationCow(id);

            Lactation currentLactation = lactations.FirstOrDefault(l => l.finished == false);
            Lactation lastLactation = lactations.Where(l => l.idLactation != currentLactation.idLactation).OrderBy(l => l.dateBirth).ToList()[lactations.Count -2];
            
            ViewBag.currentLactation = interfaceService.GetLactationEMA(currentLactation.idLactation);
            ViewBag.lastLactation = interfaceService.GetLactationEMA(lastLactation.idLactation);
            ViewBag.notifications = interfaceService.GetNotificationCow(id);
            ViewBag.foods = interfaceService.GetAllFoodByCow(id).OrderByDescending(f => f.date).ToList();

            double forecastValue = interfaceService.GetPrediction(id);
            ViewBag.forecastValue = forecastValue;

            return View(lactations);
        }
    }
}