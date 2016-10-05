using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models.CtaCte
{
    [Table("Imputaciones")]
    public class Imputacion
    {
        public int ID { get; set; }
        [Required]       
        public DateTime Fecha { get; set; }
       [ForeignKey("MovimientoDebe")]
        public int MovimientoDebeID { get; set; }
        [ForeignKey("MovimientoHaber")]
        public int MovimientoHaberID { get; set; }
        public double Importe { get; set; }
        public virtual MovimientoCuenta MovimientoDebe { get; set; }
        public virtual MovimientoCuenta MovimientoHaber { get; set; }

    }
}