using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    public class PagoBancario : Pago
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Detalle")]
        public string DetalleBancario{get;set;}
        [Required]
        public bool EsDeposito { get; set; }
    }
}