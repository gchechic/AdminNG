using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AdminNG.Models
{
    [Table("Recibos")]
    public class Recibo : ComprobantePago
    {
        //[Required]
        //public int NumeroRecibo { get; set; }//es igual al de comprobante salvo que se haya generado factura AFIP
        public double ImporteEfectivo { get; set; }
        public double ImporteCheques { get; set; }
        public string DetalleCheques { get; set; }

        //public Recibo()
        //{
        //    this.FormaPagoID = (int)FormaPago.IDS.Contado;
        //}
    }
}