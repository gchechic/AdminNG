using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("CuotaCodigos")]
    public class CuotaCodigo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Descripcion { get; set; } 
    }
}