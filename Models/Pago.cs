using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    
    public class Pago
    {
        public int ID { get; set; }

        public int FamiliaID { get; set; }
         
        public int FormaPagoID { get; set; }
        public int ResponsableID { get; set; }
        public int SedeID { get; set; }
        [Required]
        [Display(Name = "Numero")]
        public int ComprobanteNumero { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Required]
        //[Range(0.01, int.MaxValue, ErrorMessage = "Debe ingresar un Valor Mayor que 0")]  
        public double Importe { get; set; }
        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }
        public bool Anulado { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }//Fecha del sistema
        
        public string UsuarioNombre { get; set; } //Usuario que crea o modifica el comproblante

        public virtual Familia Familia { get; set; }
        [Display(Name = "Forma de Pago")]
        public virtual FormaPago FormaPago { get; set; }
        public virtual Responsable Responsable { get; set; }
        public virtual Sede Sede { get; set; }


    }
}