using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Food", Schema = "public")]
    public class Food
    {
        [Key]
        public int idFood { get; set; }
        public double quantity { get; set; }
        public DateTime date { get; set; }
        public int idCow { get; set; }

        [ForeignKey("idCow")]
        public virtual Cow cow { get; set; }
    }
}
