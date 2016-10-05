using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("Procesos")]
    public class Proceso
    {
        public int ID { get; set; }
        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }
        public DateTime Fecha { get; set; }
        public string Dummys { get; set; }
    }
}