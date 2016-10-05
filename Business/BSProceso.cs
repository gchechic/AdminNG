using AdminNG.DAL;
using AdminNG.Models.CtaCte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Business
{
    public class BSProceso : BSBase
    {
        BSGtoAdm bsGtoAdm;
        BSCuota bsCuota;
        BSComedor bsComedor;
        BSMovimientoCuenta bsMovimientoCuenta;
        public BSProceso(AdminNGContext db)
        {
            this.db = db;
            bsGtoAdm = new BSGtoAdm(db);
            bsCuota = new BSCuota(db);
            bsComedor = new BSComedor(db);
            bsMovimientoCuenta = new BSMovimientoCuenta(db);
        }
        public void ProcesarMes( int Mes)
        {           
            List<CargoGtoAdm> listGtoAdms= bsGtoAdm.GetCargoGtoAdmsMes(Mes ); //Generar gtoadms para cuotas de meses anteriores con ultimo dia de mes como fecha
            List<CargoCuota> listCuotas = bsCuota.GetCargoCuotasMes(Mes).Where(c=>c.Importe>0).ToList(); //Generar cuotas del mes
            List<CargoComedor> listComedores = bsComedor.GetCargoComedoresMes(Mes).Where(c => c.Importe > 0).ToList(); ; //Generar comedores vigentes al mes anterior con mes actual
            listGtoAdms.Where(c => c.ID != 0).ToList().ForEach(c => db.Entry(c).State = System.Data.Entity.EntityState.Modified);
            db.CargoGtoAdms.AddRange(listGtoAdms.Where( c => c.ID == 0 && c.Importe >0));
            db.CargoCuotas.AddRange(listCuotas);
            db.CargoComedores.AddRange(listComedores);
            db.SaveChanges();
            //imputar cargos por si quedaron pagos a cuenta u otros cargos negativos
            bsMovimientoCuenta.ImputarMovimientos();
        }
    }
}