using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AdminNG.Models
{
    [Table("Recibos")]
    public class Recibo
    {
        public int ID { get; set; }
        [Required]
        public int Numero{ get; set; }
        public DateTime Fecha { get; set; }
        [Required]
        public double Importe { get; set; }
        public DateTime FechaAlta { get; set; }
        
        public int UserID { get; set; } //Usuario que crea o modifica el recibo
        [Required]
        public virtual Familia Familia  { get; set; }

        [Required]
        public virtual FormaPago FormaPago { get; set; }

    }
}