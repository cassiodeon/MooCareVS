using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooServer.Models
{
    public class DTO_NewNotification
    {
        public int idNotification { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public bool read { get; set; }
        public int idLactation { get; set; }
        public int idCow { get; set; }
    }
}