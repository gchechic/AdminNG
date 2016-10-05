using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models.Caja
{
    public enum DebeHaber
    {
        Debe,
        Haber
    }
    [Table("MovCajaTipos")]
    public class MovimientoCajaTipo
    {
        public enum IDS : int
        {
            Gasto= 1,
            Sueldo=2,
            Extraccion = 3,
            Deposito=4,            
            SaldoInicial=10,
            Pago=20,            
            Redondeo=100
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Descripcion { get; set; } 
        [Required]
        public DebeHaber DebeHaber { get; set; } 

    }
}