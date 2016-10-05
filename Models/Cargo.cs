using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{

     [Table("Cargos")]
    public class Cargo
    {
        public int ID { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        [Range(1,12)]
        public int Mes { get; set; } 
        [Required]
        public double Importe { get; set; }
        [Required]
        public double Saldo { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }//Fecha del sistema
        [Required]
        public string UserName { get; set; } //Usuario que crea o modifica el recibo
               
        [Required]
        public virtual CargoTipo CargoTipo{ get; set; }
        [Required]
        public virtual Inscripcion Inscripcion{ get; set; }


    }
}