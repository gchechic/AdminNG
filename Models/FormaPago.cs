using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("FormaPagos")]
    public class FormaPago
    {
        public enum IDS:int
        {
            Contado = 1,
            Transferencia
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
          [Required]
        public string Descripcion { get; set; } 
    }
}