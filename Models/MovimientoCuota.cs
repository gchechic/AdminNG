using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
     [Table("MovimientoCuotas")]
    public class MovimientoCuota: MovimientoCuenta
    {
        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }
        public int InscripcionID { get; set; }
        public virtual Inscripcion Inscripcion { get; set; }

    }
}