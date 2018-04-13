using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooServer.Models.DTO
{
    public class YieldMilkLactation
    {
        public int idCow { get; set; }
        public DateTime dateBirthLactation { get; set; }
        public bool finished { get; set; }
        public IEnumerable<YieldDTO> yieldLactation { get; set; }
    }
    public class YieldDTO
    {
        public DateTime date { get; set; }
        public double totalYield { get; set; }
    }
}