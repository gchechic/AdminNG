using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
     [Table("MovimientoComedores")]
    public class MovimientoComedor : MovimientoCuenta
    {
        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }
        public int AlumnoID { get; set; }
        public virtual Alumno Alumno{ get; set; }
    }
}