using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminNG.ViewModels.Pagos
{
    public class VMPagoItemCheque
    {
        [Display(Name = "Importe")]
        public double Importe { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Detalle")]
        public string Detalle { get; set; }

    }
}