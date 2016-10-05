using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
     [Table("ComprobantePagos")]
    public abstract class ComprobantePago
    {
        public int ID { get; set; }
        
        public int FamiliaID { get; set; }
        public int FormaPagoID { get; set; }
        public int ResponsableID { get; set; }
        public int SedeID { get; set; }
        [Required]
        public int Numero { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Required]
        public double Importe { get; set; }
        [Required]
        public double Saldo { get; set; }
        public string Observaciones { get; set; }
        public bool Anulado { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }//Fecha del sistema
        //[Required] ???? esto trajo problemas
        public string UsuarioNombre { get; set; } //Usuario que crea o modifica el comproblante

        public virtual Familia Familia { get; set; }
        public virtual FormaPago FormaPago { get; set; }        
        public virtual Responsable Responsable { get; set; }
        public virtual Sede Sede { get; set; }

        public ComprobantePago()
        {
            this.Fecha = DateTime.Today;
        }
    }
}