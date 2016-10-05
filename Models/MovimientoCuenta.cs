using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("MovimientoCuentas")]
    public class MovimientoCuenta
    {
        public int ID { get; set; }
        public int MovimientoCuentaTipoID { get; set; }
        public int FamiliaID { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Required]
        public double Importe { get; set; }
        [Required]
        public double Saldo { get; set; }

        public string Observaciones { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }//Fecha del sistema
        [Required]
        public string UsuarioNombre { get; set; } //Usuario que crea o modifica el recibo

        
        public virtual Familia Familia{ get; set; }               
        
        public virtual MovimientoCuentaTipo MovimientoCuentaTipo { get; set; }

        //public virtual MovimientoPago MovimientoPago { get; set; }
        //public virtual MovimientoPago MovimientoCuota { get; set; }
        //public virtual MovimientoPago MovimientoComedor { get; set; }


    }
}