using AdminNG.Models;
using AdminNG.Models.Pagos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminNG.ViewModels
{
    public class VMPagoCrear 
    {
        public int ID { get; set; }
        public int ResponsableID { get; set; }
        public int FamiliaID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Required]
        [Display(Name = "Numero")]      
        public int ComprobanteNumero { get; set; }
        public bool Anulado { get; set; } 
      
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFactura { get; set; }
        public virtual Responsable Responsable { get; set; }     

    }
}