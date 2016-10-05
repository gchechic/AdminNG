using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminNG.Models.Cargos
{
    public class Cargo
    {
        public int ID { get; set; }
        [Required]
        public int InscripcionID { get; set; }
        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }

        public int CargoCodigoValorID { get; set; }///TODO: Ver si usar CargoValor en lugar de esto o ambos

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Required]
        public double Importe { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }//Fecha del sistema
        [Required]
        public string UsuarioNombre { get; set; } //Usuario que crea o modifica el recibo

        public virtual Inscripcion Inscripcion { get; set; }
    }
}