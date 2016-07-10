using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
     [Table("Feriados")]
    public class Feriado
    {
        public int ID { get; set; }
         [Required]
        public DateTime Fecha { get; set; }
        [Required]
        [StringLength(250)]
        public string Descripcion { get; set; }       
    }
}