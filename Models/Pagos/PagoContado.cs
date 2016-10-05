using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AdminNG.Models.Pagos
{    
    public class PagoContado : Pago
    {
        //private const string cMaxDouble = "79228162514264337593543950335";
        public PagoContado()
        {         
            this.FormaPagoID = (int)FormaPago.IDS.Contado;
        }
         [Display(Name = "Importe Efectivo")]
        public double ImporteEfectivo { get; set; }
         [Display(Name = "Importe Cheques")]
        public double ImporteCheques { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Detalle Cheques")]
        public string DetalleCheques { get; set; }


        //[Range(typeof(double), "0,1", cMaxDouble, ErrorMessage = "Debe ser mayor a cero")]
        //public override double Importe { get; set; }
    }
}