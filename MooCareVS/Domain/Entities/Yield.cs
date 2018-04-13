using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Yields", Schema = "public")]
    public class Yield
    {
        [Key]
        public int idYield { get; set; }
        public DateTime date { get; set; }
        public double totalYield { get; set; }
        public int idLactation { get; set; }

        [ForeignKey("idLactation")]
        public virtual Lactation lactation { get; set; }
    }
}
