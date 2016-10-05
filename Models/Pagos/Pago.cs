using AdminNG.Models.CtaCte;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models.Pagos
{

    public abstract class Pago : MovimientoCuenta
    {
        public Pago()
        { this.MovimientoCuentaTipoID = (int)Models.CtaCte.MovimientoCuentaTipo.IDS.Pago; }
        public int FormaPagoID { get; set; }       
        public int SedeID { get; set; }
        [Index( "IX_Responsable_Numero", IsUnique= true, Order=1  )]
        public int ResponsableID { get; set; }
        [Required]
        [Display(Name = "Numero")]
        [Index("IX_Responsable_Numero", IsUnique = true, Order = 2)]
        public int ComprobanteNumero { get; set; }
        public bool Anulado { get; set; }

        [Display(Name = "Forma de Pago")]
        public virtual FormaPago FormaPago { get; set; }
        public virtual Responsable Responsable { get; set; }
        public virtual Sede Sede { get; set; }


    }
}