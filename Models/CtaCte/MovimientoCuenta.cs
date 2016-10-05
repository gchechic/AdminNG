using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models.CtaCte
{
    [Table("MovCuentas")]
    public abstract class MovimientoCuenta
    {
        public int ID { get; set; }
        public int MovimientoCuentaTipoID { get; protected  set; }
        public int FamiliaID { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Required]
        public double Importe { get; set; }
        [Required]
        public double Saldo { get; set; }
        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }//Fecha del sistema
        //[Required]
        public string Usuario { get; set; } //Usuario que crea o modifica el recibo

        
        public virtual Familia Familia{ get; set; }               
        
       public virtual MovimientoCuentaTipo MovimientoCuentaTipo { get; set; }

        //public virtual MovimientoPago MovimientoPago { get; set; }
        //public virtual MovimientoPago MovimientoCuota { get; set; }
        //public virtual MovimientoPago MovimientoComedor { get; set; }

    }
}