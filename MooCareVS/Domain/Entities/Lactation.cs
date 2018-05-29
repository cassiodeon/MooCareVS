using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Lactations", Schema = "public")]
    public class Lactation
    {
        [Key]
        public int idLactation { get; set; }
        public DateTime dateBirth { get; set; }
        public bool finished { get; set; }
        public int idCow { get; set; }

        [ForeignKey("idCow")]
        public virtual Cow cow { get; set; }
        [ForeignKey("idLactation")]
        public virtual ICollection<Yield> yields { get; set; }
        [ForeignKey("idLactation")]
        public virtual ICollection<Notification> notifications { get; set; }
        [ForeignKey("idLactation")]
        public virtual ICollection<Prediction> predictions { get; set; }
    }
}
