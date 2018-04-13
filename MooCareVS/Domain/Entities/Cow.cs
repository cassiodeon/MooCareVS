using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Cows", Schema = "public")]
    public class Cow
    {
        [Key]
        public int idCow { get; set; }
        public DateTime birthday { get; set; }

        [ForeignKey("idCow")]
        public virtual ICollection<Lactation> lactations { get; set; }
    }
}
