using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models.CtaCte
{
     [Table("MovimientoCargos")]
    public abstract class MovimientoCargo: MovimientoCuenta
    {
        
        public MovimientoCargo()
        { 
          //  this.MovimientoCuentaTipoID = (int)MovimientoCuentaTipo.IDS.Cargo;
        }

        [Index("IX_Mes_Inscripcion_Codigo", IsUnique = true, Order = 1)]
        [Range(1, 12)]
        public int Mes { get; set; }
        //[ForeignKey("Inscripcion")]
        [Index("IX_Mes_Inscripcion_Codigo", IsUnique = true, Order = 2)]
        public int InscripcionID { get; set; }
        //public int CargoTipoID { get; set; }
        [Index("IX_Mes_Inscripcion_Codigo", IsUnique = true, Order = 3)]
        public int CargoCodigoValorID { get; set; }///TODO: Ver si usar CargoValor en lugar de esto o ambos

        public virtual CargoCodigoValor CargoCodigoValor { get; set; }
        //public virtual CargoTipo CargoTipo { get; set; }
        public virtual Inscripcion Inscripcion { get; set; }

        public int Cantidad { get; set; }
    }

    public class CargoCuota: MovimientoCargo
    { 
        public CargoCuota()
        {
            this.MovimientoCuentaTipoID = (int)MovimientoCuentaTipo.IDS.Cuota;
        }
        public bool Impaga
        {
            get
            {
                return this.Importe != 0 && ((this.Importe-this.Saldo ) / this.Importe < AdminNG.Helpers.Configuracion.PorcCuotaImpaga);
            }
        }
    }
   
    public class CargoGtoAdm:MovimientoCargo
    {
        public CargoGtoAdm()       
        {
            this.MovimientoCuentaTipoID = (int)MovimientoCuentaTipo.IDS.GtoAdm;
        }
        ///TODO. Foreing key                                           
        [ForeignKey("Cuota")]
        public int CuotaID { get; set; } //Cargo Relacionado
        //[ForeignKey("MovimientoCargo")]
        public virtual CargoCuota Cuota { get; set; }
    }

    public class CargoComedor : MovimientoCargo
    {
        public CargoComedor()
        {
            this.MovimientoCuentaTipoID = (int)MovimientoCuentaTipo.IDS.Comedor;
        }

    }
}