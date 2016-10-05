using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminNG.ViewModels.Pagos
{
    public class VMPagoItemTransferencia
    {
        [Display(Name = "Importe")]
        public double Importe { get; set; }
        [Display(Name = "Numero")]
        public string Numero { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
    }
}