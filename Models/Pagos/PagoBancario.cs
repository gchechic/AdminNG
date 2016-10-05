using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminNG.Models.Pagos
{
    public class PagoBancario : Pago
    {
        public PagoBancario ()
        {
            this.FormaPagoID = (int)FormaPago.IDS.Bancario;
        }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Detalle")]
        public string DetalleBancario{get;set;}
        [Required]
        [Display(Name = "Es Deposito")]
        public bool EsDeposito { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFactura { get; set; }
    }
}