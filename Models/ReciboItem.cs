using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("ReciboItems")]
    public class ReciboItem
    {
        public int ID { get; set; }
        public int ReciboID { get; set; }
        public int CargoID { get; set; }
        [Required]
        public double Importe { get; set; }
        [Required]
        public virtual Recibo Recibo{ get; set; }
        [Required]
        public virtual Cargo Cargo{ get; set; }
    }
}