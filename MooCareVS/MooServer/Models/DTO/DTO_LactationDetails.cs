using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooServer.Models.DTO
{
    public class DTO_LactationDetails
    {
        public int idLactation { get; set; }
        public List<DTO_Yield> yields { get; set; }
        public string[] yieldEMA { get; set; }
    }

    public class DTO_Yield
    {
        public DateTime date { get; set; }
        public double totalYield { get; set; }
        public int dayLactation { get; set; }
    }
}