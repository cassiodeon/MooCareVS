using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Predictions", Schema = "public")]
    public class Prediction
    {
        [Key]
        public int idPrediction { get; set; }
        public double yield { get; set; }
        public int dayLactationPredicted { get; set; }
        public int idLactation { get; set; }

        [ForeignKey("idLactation")]
        public virtual Lactation lactation { get; set; }
    }
}
