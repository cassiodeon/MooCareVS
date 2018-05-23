using Domain.Entities;
using MooServer.Models.DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MooServer.Controllers
{
    public class HomeController : Controller
    {
        InterfaceService interfaceService;
        public HomeController()
        {
            interfaceService = new InterfaceService();
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home MooCare";
            int quantityCowsWONotify = 0;
            int quantityCowsWNotify = 0;

            int quantityNotifyRead = 0;
            int quantityNotifyNotRead = 0;

            List<Cow> cowsWithNotify = interfaceService.GetCowsWithNotification();
            List<Cow> cows = interfaceService.GetCowsByStatus(false);

            quantityCowsWNotify = cowsWithNotify.Count();
            quantityCowsWONotify = cows.Count() - quantityCowsWNotify;

            List<Notification> notifications = interfaceService.GetAllNotifications();
            quantityNotifyRead = notifications.Where(n => n.read).Count();
            quantityNotifyNotRead = notifications.Where(n => n.read == false).Count();

            DTO_Dashboard dashboard = new DTO_Dashboard {
                quantityCowsWithNotify = quantityCowsWNotify,
                quantityCowsWithoutNotify = quantityCowsWONotify,
                quantityNotifyRead = quantityNotifyRead,
                quantityNotifyNotRead = quantityNotifyNotRead
            };
            return View(dashboard);
        }
    }
}
