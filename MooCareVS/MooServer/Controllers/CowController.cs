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
            List<Cow> cows = interfaceService.GetCowsByStatus(false);
            return View(cows);
        }

        public ActionResult Details(int id)
        {
            List<Lactation> lactations = interfaceService.GetLactationCow(id);
            ViewBag.lastLactation = null;
            ViewBag.lastLactation = null;

            Lactation currentLactation = lactations.FirstOrDefault(l => l.finished == false);
            Lactation lastLactation = null;
            if (currentLactation != null)
            {
                ViewBag.currentLactation = interfaceService.GetLactationEMA(currentLactation.idLactation);   
            }

            if (lactations.Where(l => l.finished == true).Count() >= 1)
            {
                List<Lactation> lactationFinished = lactations.Where(l => l.finished == true).OrderBy(l => l.dateBirth).ToList();
                lastLactation = lactationFinished[lactationFinished.Count - 1];
                ViewBag.lastLactation = interfaceService.GetLactationEMA(lastLactation.idLactation);
            }

            ViewBag.notifications = interfaceService.GetNotificationCow(id);
            ViewBag.foods = interfaceService.GetAllFoodByCow(id).OrderByDescending(f => f.date).ToList();

            double forecastValue = interfaceService.GetPrediction(id);
            ViewBag.forecastValue = forecastValue;

            return View(lactations);
        }
    }
}