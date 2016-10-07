using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminNG.ViewModels.Pagos
{
    public class VMPagoItemCheque
    {
        public int ID { get; set; }
        public int VMPagoCrearID { get; set; }
        [Display(Name = "Importe")]
        public double Importe { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Detalle")]
        public string Detalle { get; set; }

        public bool deleteItem { get; set; }
    }
}