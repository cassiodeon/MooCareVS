using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;
using Domain.Entities;
using MooServer.Models.DTO;
using MooServer.Models;

namespace MooServer.Controllers
{
    public class InterfaceServiceController : ApiController
    {
        InterfaceService interfaceService;
        public InterfaceServiceController()
        {
            interfaceService = new InterfaceService();
        }

        [HttpGet]
        [Route("~/api/InterfaceService/GetAllCows")]
        public List<Cow> GetAllCows()
        {
            
            List<Cow> x = interfaceService.GetAllCows();
            return x;
        }

        [HttpGet]
        [Route("~/api/InterfaceService/GetLactation/{id}")]
        public DTO_LactationDetails GetLactation(int id)
        {
            //Tratar referencia ciclica Loop
            Lactation lac = interfaceService.GetLactationById(id);

            DTO_LactationDetails dtoLac = new DTO_LactationDetails {
                idLactation = lac.idLactation,
                yields = (from y in lac.yields
                          select new DTO_Yield
                          {
                              date = y.date,
                              dayLactation = y.dayLactation,
                              totalYield = y.totalYield

                          }).ToList(),
                yieldEMA = interfaceService.GetLactationEMA(id)
            };
            
            return dtoLac;
        }

        [HttpGet]
        [Route("~/api/InterfaceService/GetNewsNotifications")]
        public List<DTO_NewNotification> GetNewsNotifications()
        {
            List<Notification> notif = interfaceService.GetNotificationNotRead();
            List<DTO_NewNotification> news = (from n in notif
                                              select new DTO_NewNotification
                                              {
                                                  idNotification = n.idNotification,
                                                  idCow = n.lactation.idCow,
                                                  description = n.description,
                                                  idLactation = n.idLactation,
                                                  read = n.read,
                                                  type = n.type
                                              }).ToList();

            return news;
        }
    }
}
