using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Notifications", Schema = "public")]
    public class Notification
    {
        [Key]
        public int idNotification { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public bool read { get; set; }
        public int idLactation { get; set; }

        [ForeignKey("idLactation")]
        public virtual Lactation lactation { get; set; }
    }
}
