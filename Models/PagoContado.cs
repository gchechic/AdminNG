using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AdminNG.Models
{    
    public class PagoContado : Pago
    {        
        public double ImporteEfectivo { get; set; }     
        public double ImporteCheques { get; set; }
        [DataType(DataType.MultilineText)]
        public string DetalleCheques { get; set; }    
    }
}