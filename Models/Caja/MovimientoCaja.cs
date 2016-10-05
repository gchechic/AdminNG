using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models.Caja
{
    [Table("MovCajas")]
    public class MovimientoCaja
    {
        public int ID { get; set; }
        public int MovimientoCajaTipoID { get; set; }
        public int SedeID { get; set; }       
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Required]
        public double Importe { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Descripcion{ get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }//Fecha del sistema
        [Required]
        public string UsuarioNombre { get; set; } //Usuario que crea o modifica el recibo

        public virtual MovimientoCajaTipo MovimientoCajaTipo { get; set; }
        public virtual Sede Sede { get; set; }

    }
}