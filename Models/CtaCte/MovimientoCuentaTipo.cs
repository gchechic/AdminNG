using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models.CtaCte
{
    [Table("MovCuentaTipos")]
    public class MovimientoCuentaTipo
    {
        public enum IDS : int
        {
            Cuota = 1,
            GtoAdm =2,
            Comedor = 3,
            Matricula = 4,
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