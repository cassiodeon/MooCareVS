using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooServer.Models.DTO
{
    public class DTO_Dashboard
    {
        public int quantityCowsWithoutNotify { get; set; }
        public int quantityCowsWithNotify { get; set; }
        public int quantityNotifyRead { get; set; }
        public int quantityNotifyNotRead { get; set; }
    }
}