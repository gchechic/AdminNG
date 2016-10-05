using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("MovimientoCuentaTipos")]
    public class MovimientoCuentaTipo
    {
        public enum IDS : int
        {
            Cuota = 1,
            PrimeraMora=2,
            SegundaMora=3,
            Matricula=4,
            Comedor=10,
            Pago=20,
            Bonificacion=30,
            Redondeo=100
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        public string Descripcion { get; set; } 
    }
}