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
            return View();
        }

        public ActionResult Details(int id)
        {
            List<Lactation> lactations = interfaceService.GetLactationCow(id);

            Lactation currentLactation = lactations.FirstOrDefault(l => l.finished == false);
            Lactation lastLactation = lactations.Where(l => l.idLactation != currentLactation.idLactation).OrderBy(l => l.dateBirth).ToList()[lactations.Count -2];

            ViewBag.currentLactation = interfaceService.GetLactationEMA(currentLactation.idLactation);
            ViewBag.lastLactation = interfaceService.GetLactationEMA(lastLactation.idLactation);
            return View(lactations);
        }
    }
}