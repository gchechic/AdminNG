using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    public class MovimientoMora: MovimientoCuenta
    {
        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }
        [Required]
        public int InscripcionID { get; set; }
        [Required]
        public int MovimientoCuotaID { get; set; } // Cuota sobre la que se aplica la mora
        [Required]
        public int CargoCodigoID { get; set; } // Primera o segunda mora
    }
}