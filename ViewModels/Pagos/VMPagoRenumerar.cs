using AdminNG.Models;
using AdminNG.Models.Pagos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminNG.ViewModels
{
    public class VMPagoRenumerar 
    {
        public int ID { get; set; }
        public Pago Pago { get; set; }
        public int ResponsableNuevoID { get; set; }
        [Required]
        [Display(Name = "Numero Nuevo")]
        [Range(1,int.MaxValue, ErrorMessage= "Debe ingresar un Valor Mayor o igual a  {1}" )]
        public int NumeroNuevo { get; set; }        
        [Display(Name = "Responsable Nuevo")]
        public virtual Responsable ResponsableNuevo { get; set; }        

    }
}